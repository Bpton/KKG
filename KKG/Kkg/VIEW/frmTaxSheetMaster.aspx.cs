using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmTaxSheetMaster : System.Web.UI.Page
{
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                PlntateWise.Style["display"] = "none";
                plnException.Style["display"] = "none";
                PlnProduct.Style["display"] = "none";
                btnaddhide.Style["display"] = "";
                pnlsalvemapping.Style["display"] = "none";
                pnlcategory.Style["display"] = "none";
                pnlvendorgroupmapping.Style["display"] = "none";
                LoadTaxMaster();
                LoadRegionTax();
                LoadMTaxRelatedto();
                ResetControls();
                this.LoadCategory_SuppliedItem();
                rdbList.Enabled = true;
                HttpContext.Current.Session["grdexp"] = null;
                HttpContext.Current.Session["grdexpdel"] = null;
                this.LoadAccountGroup();
                this.LoadLedger();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region Method

    public void LoadTaxMaster()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            gvPercentage.DataSource = clstxmaster.BindTaxMasterGrid();
            gvPercentage.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void BindException(string vendorid, string taxid)
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            DataTable dtexpdrd = clstxmaster.BindEcxptnIGridTF(vendorid, taxid);
            if (dtexpdrd.Rows.Count > 0)
            {
                gvexception.DataSource = dtexpdrd;
                gvexception.DataBind();
                HttpContext.Current.Session["grdexp"] = dtexpdrd;
            }
            else
            {
                this.gvexception.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.gvexception.ClearPreviousDataSource();
                this.gvexception.DataSource = null;
                this.gvexception.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void BindState()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            gvState.DataSource = clstxmaster.BindStateGrid();
            gvState.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void BindStateper(string id)
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            dt = clstxmaster.BindStateper(id);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void BindProductper(string id, string Vid)
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            dt = clstxmaster.BindProductMapping(id, Vid);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadRegionTax()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlappliedto.Items.Clear();
            ddlappliedto.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlappliedto.DataSource = clstxmaster.BindRegionTax();
            ddlappliedto.DataTextField = "REGIONTAXNAME";
            ddlappliedto.DataValueField = "REGIONTAXID";
            ddlappliedto.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadDivision()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlDivision.Items.Clear();
            ddlDivision.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlDivision.DataSource = clstxmaster.BindDivision();
            ddlDivision.DataTextField = "DIVNAME";
            ddlDivision.DataValueField = "DIVID";
            ddlDivision.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadMaterial()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlDivision.Items.Clear();
            ddlDivision.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlDivision.DataSource = clstxmaster.BindMaterial();
            ddlDivision.DataTextField = "ITEMDESC";
            ddlDivision.DataValueField = "ID";
            ddlDivision.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCategory()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlCategory.DataSource = clstxmaster.BindCategory();
            ddlCategory.DataTextField = "CATNAME";
            ddlCategory.DataValueField = "CATID";
            ddlCategory.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadSubMaterial()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlCategory.Items.Clear();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlCategory.DataSource = clstxmaster.BindSubMaterial(ddlDivision.SelectedValue);
            ddlCategory.DataTextField = "SUBITEMDESC";
            ddlCategory.DataValueField = "SUBTYPEID";
            ddlCategory.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCategory_SuppliedItem()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlsupplieditem.Items.Clear();
            ddlsupplieditem.Items.Insert(0, new ListItem("SELECT TYPE", "0"));
            ddlsupplieditem.Items.Insert(1, new ListItem("TRANSPORTER", "-1"));
            ddlsupplieditem.DataSource = clstxmaster.BindCategory_SuppliedItem();
            ddlsupplieditem.DataTextField = "CATNAME";
            ddlsupplieditem.DataValueField = "CATID";
            ddlsupplieditem.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void BindExectionRadiobtn()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            dt = clstxmaster.taxExceptionrdb();
            if (dt.Rows.Count > 0)
            {
                rdbList.DataSource = dt;
                rdbList.DataTextField = "UTNAME";
                rdbList.DataValueField = "UTID";
                rdbList.DataBind();

                rdbExpProduct.DataSource = dt;
                rdbExpProduct.DataTextField = "UTNAME";
                rdbExpProduct.DataValueField = "UTID";
                rdbExpProduct.DataBind();
            }


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadPageName()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            dt = clstxmaster.BindPageName();
            gvReasonmap.DataSource = clstxmaster.BindPageName();
            gvReasonmap.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadMTaxRelatedto()
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            ddlRelatedto.Items.Clear();
            ddlRelatedto.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlRelatedto.DataSource = clstxmaster.BindTaxRelatedtoGrid();
            ddlRelatedto.DataTextField = "NAME";
            ddlRelatedto.DataValueField = "ID";
            ddlRelatedto.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void PoPulateGrid(string DIVISIONID, string CATAGORYID)
    {

        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            DataTable dtratesheet = new DataTable();
            dtratesheet = clstxmaster.FetchProductDetails(DIVISIONID, CATAGORYID);
            if (dtratesheet.Rows.Count > 0)
            {
                grdProduct.DataSource = dtratesheet;
                grdProduct.DataBind();
                ddlDivision.Enabled = false;
                ddlCategory.Enabled = false;
                rdbExpProduct.Enabled = false;
                ddlExpProduct.Enabled = false;
                rdbbtn.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadExcepddlItem(string UTID)
    {
        ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
        ddlexception.Items.Clear();
        ddlexception.Items.Insert(0, new ListItem("Select", "0"));

        ddlExpProduct.Items.Clear();
        ddlExpProduct.Items.Insert(0, new ListItem("Select", "0"));
        DataTable dt = clstxmaster.BindEcxptnItem(UTID);
        if (dt.Rows.Count > 0)
        {
            ddlexception.Items.Clear();
            ddlexception.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlexception.DataSource = dt;
            ddlexception.DataTextField = "VENDORNAME";
            ddlexception.DataValueField = "VENDORID";
            ddlexception.DataBind();

            ddlExpProduct.Items.Clear();
            ddlExpProduct.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlExpProduct.DataSource = dt;
            ddlExpProduct.DataTextField = "VENDORNAME";
            ddlExpProduct.DataValueField = "VENDORID";
            ddlExpProduct.DataBind();


        }
    }

    public void ResetControls()
    {
        Session["dtexcp"] = null;
        this.gvexception.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
        this.gvexception.ClearPreviousDataSource();
        this.gvexception.DataSource = null;
        this.gvexception.DataBind();
        txtexception.Text = "";
    }

    public void LoadReasonMapbyid(string id)
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            dt = clstxmaster.BindPageNamebyid(id);
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlDivision_SelectedIndexChanged
    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbExpProduct.SelectedValue == "V")
            {
                LoadSubMaterial();
                rdbbtn.ClearSelection();
            }
            else
            {
                LoadCategory();
            }

            if (rdbbtn.SelectedValue == "0")
            {
                lblDiv.Text = "PRIMARY ITEM";
                lblCat.Text = "SUB ITEM";
                LoadSubMaterial();

            }
            else if (rdbbtn.SelectedValue == "1")
            {
                lblDiv.Text = "BRAND";
                lblCat.Text = "CATEGORY";
                LoadCategory();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadAccountGroup
    public void LoadAccountGroup()
    {
        ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
        ddlaccountgroup.Items.Clear();
        ddlaccountgroup.Items.Insert(0, new ListItem("-Select Account Group-", "0"));
        ddlaccountgroup.DataSource = clstxmaster.BindAccountgrp();
        ddlaccountgroup.DataTextField = "grpName";
        ddlaccountgroup.DataValueField = "Code";
        ddlaccountgroup.DataBind();
    }
    #endregion

    #region btnAddDivision_Click
    protected void btnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {

            txtName.Text = "";
            txtCode.Text = "";
            chkActive.Checked = true;
            txtPercentage.Text = "";
            ddlRelatedto.SelectedValue = "0";
            Hdn_Fld.Value = "";
            txteffectivefrom.Text = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            LoadPageName();
            HttpContext.Current.Session["grdexp"] = null;
            HttpContext.Current.Session["grdexpdel"] = null;
            //this.ddlTargetMenuOption.Items.Clear();
            //this.ddlSourceMenuOption.SelectedValue = "0";
            this.txtdescription.Text = "";

            this.LoadAccountGroup();
            //ddlaccountgroup.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

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

    #region CreateLedgerTable
    protected DataTable CreateLedgerTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEBITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("CREDITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("OPENBALANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("BALANCETYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONNAME", typeof(string)));

        HttpContext.Current.Session["LEDGERDETAILS"] = dt;

        return dt;
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

    #region TaxMaster save/edit/delete
    protected void btnTaxSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            string Mode = "";
            int count = 0;
            string ip_address = Request.UserHostAddress.ToString().Trim();
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
                clstxmaster.DeleteReasonBYID(Hdn_Fld.Value);
            }

            int ID = 0;
            int MappingID = 0;
            int ledger = 0;
            string bindtaxid = string.Empty;
            if (Session["LEDGERDETAILS"] == null)
            {
                this.CreateLedgerTable();
            }
            DataTable dtledger = (DataTable)Session["LEDGERDETAILS"];
            string xmlledger = string.Empty;

            DataTable dt = clstxmaster.BindGridBranch();


            for (int i = 0; i < gvReasonmap.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = gvReasonmap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;
                if (chk.Checked == true)
                {
                    count = count + 1;

                    if (count == 1)
                    {
                        ID = clstxmaster.SaveTaxMaster(Hdn_Fld.Value, txtName.Text.Trim(), txtPercentage.Text.ToString(), Mode, ddlRelatedto.SelectedValue,
                                    ddlappliedto.SelectedValue, chkActive.Checked.ToString(), txtCode.Text.Trim(), (this.txteffectivefrom.Text.Trim()),
                                    ddlaccountgroup.SelectedValue.Trim(), ddlaccountgroup.SelectedItem.Text.ToString().Trim(), this.txtdescription.Text.Trim(), ip_address);

                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            DataRow dr = dtledger.NewRow();
                            dr["ID"] = Guid.NewGuid();
                            dr["OPENBALANCE"] = 0;
                            dr["BALANCETYPE"] = "D";
                            dr["DEBITAMOUNT"] = 0;
                            dr["CREDITAMOUNT"] = 0;
                            dr["REGIONID"] = dt.Rows[j]["REGIONID"].ToString();
                            dr["REGIONNAME"] = dt.Rows[j]["REGIONNAME"].ToString();

                            dtledger.Rows.Add(dr);
                            dtledger.AcceptChanges();
                        }

                        xmlledger = ConvertDatatableToXML(dtledger);

                        bindtaxid = clstxmaster.BindTaxid(txtName.Text.Trim());

                        if (Hdn_Fld.Value == "")
                        {
                            ViewState["modeinsurance"] = "A";
                        }
                        else
                        {
                            ViewState["modeinsurance"] = "U";
                        }
                        string Modedelete = ViewState["modeinsurance"].ToString();

                        ledger = clstxmaster.SaveAccInfo(txtName.Text.Trim(), ddlaccountgroup.SelectedValue.Trim(), Session["FINYEAR"].ToString(), Hdn_Fld.Value.Trim(), xmlledger, bindtaxid.Trim(), Modedelete);
                    }

                    if (count > 0)
                    {
                        MappingID = clstxmaster.SaveUserTaxMapping(Convert.ToInt32(chk.ToolTip), txtName.Text.Trim(), Mode, Hdn_Fld.Value);
                    }
                }
            }

            if (count == 0)
            {
                //string message = "alert('Please select atleast 1 Menu..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                //break;
                MessageBox1.ShowInfo("Please select atleast 1 Menu..");
            }

            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadTaxMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                PlntateWise.Style["display"] = "none";
                PlnProduct.Style["display"] = "none";
                pnlcategory.Style["display"] = "none";
                pnlvendorgroupmapping.Style["display"] = "none";
                Hdn_Fld.Value = "";
                LoadTaxMaster();

                //this.ddlTargetMenuOption.Items.Clear();
                //this.ddlSourceMenuOption.SelectedValue = "0";
            }

            else if (ID == 2)
            {
                string message = "alert('Name already exist..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
            // }
            Session["LEDGERDETAILS"] = null;
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnTaxCancel_Click(object sender, EventArgs e)
    {
        try
        {

            txtName.Text = "";
            txtPercentage.Text = "";
            Hdn_Fld.Value = "";
            this.txteffectivefrom.Text = "";

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            PlntateWise.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            //this.ddlTargetMenuOption.Items.Clear();
            //this.ddlSourceMenuOption.SelectedValue = "0";
            LoadTaxMaster();
            ddlaccountgroup.SelectedValue = "0";
            this.txtdescription.Text = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
                ID = clstxmaster.DeleteTAXMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadTaxMaster();

                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btngridEDIT_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            DataTable dt = new DataTable();
            dt = clstxmaster.BindTaxMasterGridEdit(Hdn_Fld.Value.Trim());
            this.txtName.Text = Convert.ToString(dt.Rows[0]["NAME"]);
            this.txtPercentage.Text = Convert.ToString(dt.Rows[0]["PERCENTAGE"]);
            this.txtCode.Text = Convert.ToString(dt.Rows[0]["CODE"]);
            string activeValue = Convert.ToString(dt.Rows[0]["ACTIVE"]).Trim();
            ddlRelatedto.SelectedValue = Convert.ToString(dt.Rows[0]["RELATEDTO"]);
            ddlappliedto.SelectedValue = Convert.ToString(dt.Rows[0]["APPLICABLETO"]);
            //string effectivedate = Conver_To_ISO(this.txteffectivefrom.Text);
            this.txtdescription.Text = Convert.ToString(dt.Rows[0]["DESCRIPTION"]);

            if (Convert.ToString(dt.Rows[0]["EFFECTIVEFROM"]).Trim() == "01/01/1900")
            {
                this.txteffectivefrom.Text = "";
            }
            else
            {
                this.txteffectivefrom.Text = Convert.ToString(dt.Rows[0]["EFFECTIVEFROM"]).Trim();
            }

            if (activeValue == "True")
            {
                this.chkActive.Checked = true;
            }
            else
            {
                this.chkActive.Checked = false;
            }
            LoadPageName();
            LoadAccountGroup();
            if (Convert.ToString(dt.Rows[0]["ACCCODE"]) != "")
            {
                ddlaccountgroup.SelectedValue = Convert.ToString(dt.Rows[0]["ACCCODE"]);
            }
            else
            {
                ddlaccountgroup.SelectedValue = "0";
            }

        }


        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gvReasonmap_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (Hdn_Fld.Value != "")
            {
                LoadReasonMapbyid(Hdn_Fld.Value);

            }
            if (e.Row.RowType == GridRowType.DataRow && gvReasonmap.Rows.Count > 0)
            {
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][1].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;

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
    #endregion

    #region Sate wise save/edite/delete
    protected void btnStateSave_Click(object sender, EventArgs e)
    {

        try
        {
            string Mode = "A";
            string Tag = "";
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            clstxmaster.DeleteStatebyid(hdnPid.Value);

            if (gvState.TotalRowCount > 0)
            {
                for (int i = 0; i < gvState.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell cell = gvState.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                    TextBox txtperAge = cell.FindControl("TxtperAge") as TextBox;
                    HiddenField hiddenField = cell.FindControl("SName") as HiddenField;
                    if (txtperAge.Text != "")
                    {
                        int ID = 0;

                        ID = clstxmaster.SaveStatePer(hdnPid.Value, Convert.ToInt32(hiddenField.Value), Convert.ToDecimal(txtperAge.Text.Trim()), Mode);

                        if (ID == 1)
                        {
                        }
                        else if (ID == 2)
                        {
                        }
                    }
                }
            }

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            PlntateWise.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            LoadTaxMaster();
            LoadMTaxRelatedto();
            grdProduct.DataSource = null;
            grdProduct.DataBind();

            //string message = "alert('Record Saved Successfully..')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            ClsTaxSheetMaster clstax = new ClsTaxSheetMaster();
            int updateid = clstax.UpdateLdomTaxMaster(hdnPid.Value.Trim());

            MessageBox1.ShowSuccess("Record Saved Successfully!");
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnStateCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            LoadTaxMaster();
            LoadMTaxRelatedto();
            HttpContext.Current.Session["grdexp"] = null;
            HttpContext.Current.Session["grdexpdel"] = null;
            //hdnPid1.Value = "";
            //hdnPid.Value = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnGridState_Click(object sender, EventArgs e)
    {
        try
        {

            if (hdnPid1.Value == "BS")
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                plnException.Style["display"] = "none";
                pnlcategory.Style["display"] = "none";
                pnlvendorgroupmapping.Style["display"] = "none";
                PlntateWise.Style["display"] = "";
                BindState();

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gvState_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (hdnPid.Value != "")
            {
                BindStateper(hdnPid.Value);

            }
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                TextBox txtperAge = cell.FindControl("TxtperAge") as TextBox;
                HiddenField hiddenField = cell.FindControl("SName") as HiddenField;
                //HiddenField hiddenFieldstateId = cell.FindControl("SName") as HiddenField;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["State_ID"].ToString().Trim() == hiddenField.Value.Trim())
                    {
                        txtperAge.Text = Convert.ToString(dt.Rows[i]["percentage"]);

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
    #endregion

    #region EXception Save/edite/delete
    protected void btnSaveEcetion_Click(object sender, EventArgs e)
    {
        string xml = string.Empty;

        //xml = ConvertDatatableToXML(dtCityRecordsCheck);

        try
        {
            string Mode = "A";
            string Tag = "";
            string guid = "";
            string vendor = "";
            decimal percentage = 0;
            string Ordertypeid = "";
            string Ordertypename = string.Empty;
            int ID = 0;
            DataTable dtexp = new DataTable();
            DataTable dtexpdel = new DataTable();
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            clstxmaster.DeleteExcptnbyid(hdnPid.Value, rdbList.SelectedValue);

            if (HttpContext.Current.Session["grdexp"] != null)
            {
                dtexp = (DataTable)HttpContext.Current.Session["grdexp"];
            }
            if (HttpContext.Current.Session["grdexpdel"] != null)
            {
                dtexpdel = (DataTable)HttpContext.Current.Session["grdexpdel"];
            }

            //DataTable  = (DataTable)Session["grdexp"];
            if (dtexpdel.Rows.Count > 0)
            {
                for (int i = 0; i < dtexpdel.Rows.Count; i++)
                {
                    guid = dtexpdel.Rows[i]["TAXID"].ToString();
                    vendor = dtexpdel.Rows[i]["VENDORID"].ToString();
                    percentage = Convert.ToDecimal(dtexpdel.Rows[i]["PERCENTAGE"].ToString());
                    Ordertypeid = Convert.ToString(dtexpdel.Rows[i]["ORDERTYPEID"]).Trim();
                    Ordertypename = Convert.ToString(dtexpdel.Rows[i]["ORDERTYPENAME"]).Trim();
                    //if(vendor)
                    ID = clstxmaster.SaveExcetion(guid, vendor, percentage, Mode, Ordertypeid, Ordertypename);
                }


            }
            else if (dtexp.Rows.Count > 0)
            {
                for (int i = 0; i < dtexp.Rows.Count; i++)
                {
                    guid = dtexp.Rows[i]["TAXID"].ToString();
                    vendor = dtexp.Rows[i]["VENDORID"].ToString();
                    percentage = Convert.ToDecimal(dtexp.Rows[i]["PERCENTAGE"].ToString());
                    Ordertypeid = Convert.ToString(dtexp.Rows[i]["ORDERTYPEID"]).Trim();
                    Ordertypename = Convert.ToString(dtexp.Rows[i]["ORDERTYPENAME"]).Trim();

                    //if(vendor)
                    ID = clstxmaster.SaveExcetion(guid, vendor, percentage, Mode, Ordertypeid, Ordertypename);
                }

            }
            if (ID == 1)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                PlntateWise.Style["display"] = "none";
                plnException.Style["display"] = "none";
                PlnProduct.Style["display"] = "none";
                pnlcategory.Style["display"] = "none";
                pnlvendorgroupmapping.Style["display"] = "none";
                LoadTaxMaster();
                LoadMTaxRelatedto();
                ClsTaxSheetMaster clstax = new ClsTaxSheetMaster();
                int updateid = clstax.UpdateLdomTaxMaster(hdnPid.Value.Trim());

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                HttpContext.Current.Session["grdexp"] = null;
                HttpContext.Current.Session["grdexpdel"] = null;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnGridExp_Click(object sender, EventArgs e)
    {
        try
        {

            if (hdnPid1.Value == "PS")
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                PlntateWise.Style["display"] = "none";
                PlnProduct.Style["display"] = "none";
                pnlcategory.Style["display"] = "none";
                pnlvendorgroupmapping.Style["display"] = "none";
                plnException.Style["display"] = "";
                ddlexception.Items.Clear();
                ddlexception.Items.Insert(0, new ListItem("Select", "0"));
                ddlexception.DataSource = null;
                ddlexception.DataBind();
                BindExectionRadiobtn();
                this.LoadOrderType();
                ResetControls();
                rdbList.Enabled = true;
                HttpContext.Current.Session["grdexp"] = null;
                HttpContext.Current.Session["grdexpdel"] = null;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void rdbList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.gvexception.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
            this.gvexception.ClearPreviousDataSource();
            this.gvexception.DataSource = null;
            this.gvexception.DataBind();
            HttpContext.Current.Session["grdexp"] = null;
            LoadExcepddlItem(rdbList.SelectedValue);
            BindException(rdbList.SelectedValue, hdnPid.Value);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }





    public void LoadOrderType()
    {
        try
        {
            ClsTaxSheetMaster clstax = new ClsTaxSheetMaster();
            DataTable dt = new DataTable();
            dt = clstax.BindOrderType();
            if (dt.Rows.Count > 0)
            {
                this.ddlordertype.Items.Clear();
                this.ddlordertype.DataSource = dt;
                this.ddlordertype.DataValueField = "BSID";
                this.ddlordertype.DataTextField = "BSNAME";
                this.ddlordertype.DataBind();
            }
            else
            {
                this.ddlordertype.Items.Clear();
                this.ddlordertype.Items.Add(new ListItem("-- SELECT ORDER TYPE --", "0"));
                this.ddlordertype.AppendDataBoundItems = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int CountProduct = 0;
            this.rdbList.Enabled = false;
            string VendorID = "";
            string TaxId = "";
            string OrderTypeid = "";
            bool FLAG = false;
            ViewState["Mode"] = "A";
            DataTable dtgrd = (DataTable)HttpContext.Current.Session["grdexp"];
            DataTable dtexpnull = new DataTable();
            if (HttpContext.Current.Session["grdexp"] == null)
            {
                CountProduct = CountProduct + 1;
                dtexpnull.Columns.Add("TAXID", typeof(string));
                dtexpnull.Columns.Add("VENDORID", typeof(string));
                dtexpnull.Columns.Add("VENDORNAME", typeof(string));
                dtexpnull.Columns.Add("PERCENTAGE", typeof(string));
                dtexpnull.Columns.Add("ORDERTYPEID", typeof(string));
                dtexpnull.Columns.Add("ORDERTYPENAME", typeof(string));
                HttpContext.Current.Session["grdexp"] = dtexpnull;

                if (CountProduct == 0)
                {
                    MessageBox1.ShowInfo("Please select atleast 1 Item");
                }
                else
                {
                    DataRow dr = dtexpnull.NewRow();
                    dr["TAXID"] = hdnPid.Value;
                    dr["VENDORID"] = Convert.ToString(this.ddlexception.SelectedValue).Trim();
                    dr["VENDORNAME"] = Convert.ToString(this.ddlexception.SelectedItem).Trim();
                    dr["PERCENTAGE"] = Convert.ToString(this.txtexception.Text).Trim();
                    dr["ORDERTYPEID"] = Convert.ToString(this.ddlordertype.SelectedValue).Trim();
                    dr["ORDERTYPENAME"] = Convert.ToString(this.ddlordertype.SelectedItem).Trim();

                    dtexpnull.Rows.Add(dr);
                    dtexpnull.AcceptChanges();
                    this.gvexception.DataSource = dtexpnull;
                    this.gvexception.DataBind();
                }
            }
            else
            {
                if (dtgrd.Rows.Count > 0)
                {
                    HttpContext.Current.Session["dtexcp"] = dtgrd;
                    for (int k = 0; k < dtgrd.Rows.Count; k++)
                    {
                        VendorID = Convert.ToString(dtgrd.Rows[k]["VENDORID"]);
                        TaxId = Convert.ToString(dtgrd.Rows[k]["TAXID"]);
                        OrderTypeid = Convert.ToString(dtgrd.Rows[k]["ORDERTYPEID"]);
                        if (VendorID == Convert.ToString(this.ddlexception.SelectedValue).Trim() && TaxId == Convert.ToString(this.hdnPid.Value.Trim()) && OrderTypeid == Convert.ToString(this.ddlordertype.SelectedValue).Trim())
                        {
                            FLAG = true;
                        }
                    }
                }

                if (FLAG == true)
                {
                    MessageBox1.ShowInfo("Record Already Exist");
                    this.gvexception.DataSource = dtgrd;
                    this.gvexception.DataBind();
                }
                else
                {
                    DataRow dr = dtgrd.NewRow();
                    dr["TAXID"] = hdnPid.Value;
                    dr["VENDORID"] = Convert.ToString(this.ddlexception.SelectedValue).Trim();
                    dr["VENDORNAME"] = Convert.ToString(this.ddlexception.SelectedItem).Trim();
                    dr["PERCENTAGE"] = Convert.ToString(this.txtexception.Text).Trim();
                    dr["ORDERTYPEID"] = Convert.ToString(this.ddlordertype.SelectedValue).Trim();
                    dr["ORDERTYPENAME"] = Convert.ToString(this.ddlordertype.SelectedItem).Trim();

                    dtgrd.Rows.Add(dr);
                    dtgrd.AcceptChanges();
                    this.gvexception.DataSource = dtgrd;
                    this.gvexception.DataBind();

                    Session["grdexp"] = dtgrd;
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    #region btngrddelete_Click
    protected void btngriddelete_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["Mode"] = "D";
            string GUID = Convert.ToString(hdndLoacationDelete.Value);
            DataTable dtdeleteSchemerecord = new DataTable();
            dtdeleteSchemerecord = (DataTable)HttpContext.Current.Session["grdexp"];

            DataRow[] drr = dtdeleteSchemerecord.Select("VENDORID='" + GUID + "' and TAXID='" + hdnPid.Value + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dtdeleteSchemerecord.AcceptChanges();
            }
            this.gvexception.DataSource = dtdeleteSchemerecord;
            this.gvexception.DataBind();

            Session["dtexcp"] = null;
            //Session["RECORDS"] = null;
            //this.gvexception.ClearPreviousDataSource();
            //this.gvexception.DataSource = null;
            //this.gvexception.DataBind();
            HttpContext.Current.Session["grdexpdel"] = dtdeleteSchemerecord;

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
    #endregion

    #region Product wise save/edite/delete
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnGridProduct_Click(object sender, EventArgs e)
    {
        try
        {

            if (hdnPid1.Value == "PM")
            {
                tdlblothers.Style["display"] = "none";
                tdother.Style["display"] = "none";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";
                plnException.Style["display"] = "none";
                PlntateWise.Style["display"] = "none";
                PlnProduct.Style["display"] = "";
                pnlcategory.Style["display"] = "none";
                pnlvendorgroupmapping.Style["display"] = "none";
                ddlDivision.Enabled = true;
                ddlCategory.Enabled = true;
                rdbExpProduct.Enabled = true;
                rdbbtn.Enabled = true;
                ddlExpProduct.Enabled = true;
                LoadDivision();
                LoadCategory();
                this.grdProduct.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.grdProduct.ClearPreviousDataSource();
                this.grdProduct.DataSource = null;
                this.grdProduct.DataBind();
                BindExectionRadiobtn();
                ddlExpProduct.Items.Clear();
                ddlExpProduct.Items.Insert(0, new ListItem("Select", "0"));
                rdbbtn.ClearSelection();
                lblDiv.Text = "BRAND";
                lblCat.Text = "CATEGORY";

            }
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridcategory_Click(object sender, EventArgs e)
    {
        try
        {
            //pnlAdd.Style["display"] = "none";
            //pnlDisplay.Style["display"] = "none";
            //btnaddhide.Style["display"] = "none";
            //plnException.Style["display"] = "none";
            //PlntateWise.Style["display"] = "none";
            //PlnProduct.Style["display"] = "none";
            //pnlcategory.Style["display"] = "";

            //this.txtFromDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //this.txtToDate1.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            //DataTable dt = new DataTable();
            //dt = clstxmaster.BindCategoryDetails(this.Hdn_Fld.Value);
            //if (dt.Rows.Count > 0)
            //{
            //    this.grdcategorydetails.DataSource = dt;
            //    this.grdcategorydetails.DataBind();
            //}
            //else
            //{
            //    this.grdcategorydetails.DataSource = null;
            //    this.grdcategorydetails.DataBind();
            //}
            string TaxId = this.Hdn_Fld.Value;
            string TaxName = this.hdnTaxName.Value;
            try
            {
                string upath = "frmTaxCategoryMapping.aspx?TaxId=" + TaxId + "&TaxName=" + TaxName + "";
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open( '" + upath + "', 'Archive', 'channelmode,width=800,height=900,directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars,resizable,left=300,top=100' );", true);
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void grdProduct_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (hdnPid.Value != "")
            {
                BindProductper(hdnPid.Value, ddlExpProduct.SelectedValue.Trim());

            }
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
                TextBox txtperAgePro = cell.FindControl("TxtperAgeProduct") as TextBox;

                HiddenField hiddenField = cell.FindControl("SName") as HiddenField;
                //HiddenField hiddenFieldstateId = cell.FindControl("SName") as HiddenField;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ID"].ToString().Trim() == hiddenField.Value.Trim())
                    {
                        txtperAgePro.Text = Convert.ToString(dt.Rows[i]["percentage"]);


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
    protected void BtnProSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (ddlExpProduct.SelectedValue != "0")
            {
                string Mode = "A";
                ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
                clstxmaster.DeleteProductbyid(hdnPid.Value, ddlExpProduct.SelectedValue.Trim());

                if (grdProduct.TotalRowCount > 0)
                {
                    for (int i = 0; i < grdProduct.RowsInViewState.Count; i++)
                    {
                        GridDataControlFieldCell cell = grdProduct.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                        TextBox txtAgePro = cell.FindControl("TxtperAgeProduct") as TextBox;
                        //Column Pname = cell.FindControl("NAME") as Column;
                        HiddenField hiddenField = cell.FindControl("SName") as HiddenField;
                        string Name = grdProduct.Rows[i].Cells[0].Text.Trim();
                        if (txtAgePro.Text != "")
                        {
                            int ID = 0;

                            ID = clstxmaster.SaveProductMapping(hdnPid.Value, hiddenField.Value, Convert.ToDecimal(txtAgePro.Text.Trim()), Name, ddlExpProduct.SelectedValue.Trim(), Mode);

                            if (ID == 1)
                            {
                            }

                        }
                    }

                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    PlntateWise.Style["display"] = "none";
                    PlnProduct.Style["display"] = "none";
                    LoadTaxMaster();
                    LoadMTaxRelatedto();
                    ClsTaxSheetMaster clstax = new ClsTaxSheetMaster();
                    int updateid = clstax.UpdateLdomTaxMaster(hdnPid.Value.Trim());

                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully!");
                }
            }
            else
            {
                //string message = "alert('Please select Type! ')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Please select Type! ");

                ddlDivision.Enabled = true;
                ddlCategory.Enabled = true;
                rdbExpProduct.Enabled = true;
                rdbbtn.Enabled = true;
                ddlExpProduct.Enabled = true;
                LoadDivision();
                LoadCategory();
                this.grdProduct.ViewStateMode = System.Web.UI.ViewStateMode.Disabled;
                this.grdProduct.ClearPreviousDataSource();
                this.grdProduct.DataSource = null;
                this.grdProduct.DataBind();
                BindExectionRadiobtn();
                ddlExpProduct.Items.Clear();
                ddlExpProduct.Items.Insert(0, new ListItem("Select", "0"));
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnProShow_Click(object sender, EventArgs e)
    {
        try
        {
            this.PoPulateGrid(ddlDivision.SelectedValue, ddlCategory.SelectedValue);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
    protected void rdbExpProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbExpProduct.SelectedValue == "V")
            {
                lblDiv.Text = "PRIMARY ITEM";
                lblCat.Text = "SUB ITEM";
                LoadMaterial();
                LoadSubMaterial();

            }
            else
            {
                lblDiv.Text = "BRAND";
                lblCat.Text = "CATEGORY";
                LoadDivision();
                LoadCategory();
                tdlblothers.Style["display"] = "none";
                tdother.Style["display"] = "none";
            }

            if (rdbExpProduct.SelectedValue.ToString().Trim() == "33FDAA0C-71DB-49F7-A686-3939EF295AAB")
            {
                tdlblothers.Style["display"] = "";
                tdother.Style["display"] = "";
                rdbbtn.Items[1].Selected = true;

            }
            else
            {
                tdlblothers.Style["display"] = "none";
                tdother.Style["display"] = "none";
            }


            LoadExcepddlItem(rdbExpProduct.SelectedValue);

            //BindException(rdbList.SelectedValue, hdnPid.Value);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void rdbbtn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rdbbtn.SelectedValue == "0")
            {
                lblDiv.Text = "PRIMARY ITEM";
                lblCat.Text = "SUB ITEM";
                LoadMaterial();
                LoadSubMaterial();
            }
            else
            {
                lblDiv.Text = "BRAND";
                lblCat.Text = "CATEGORY";
                LoadDivision();
                LoadCategory();
            }


            LoadExcepddlItem(rdbExpProduct.SelectedValue);

            //BindException(rdbList.SelectedValue, hdnPid.Value);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion

    #region gvPercentage_RowDataBound
    protected void gvPercentage_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[6] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Active")
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
    #endregion

    #region SlabWISETAXMAPPING

    #region CreateTable
    protected void CreateSlabMapping()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("GUID", typeof(string));
        dt.Columns.Add("TAXID", typeof(string));
        dt.Columns.Add("TAXNAME", typeof(string));
        dt.Columns.Add("TYPEID", typeof(string));
        dt.Columns.Add("TYPENAME", typeof(string));
        dt.Columns.Add("FROMAMOUNT", typeof(string));
        dt.Columns.Add("TOAMOUNT", typeof(string));
        dt.Columns.Add("VALUE", typeof(string));
        dt.Columns.Add("PERCENTAGE", typeof(string));

        dt.Columns.Add("LEDGERID", typeof(string));
        dt.Columns.Add("LEDGERNAME", typeof(string));
        dt.Columns.Add("FROMDATE", typeof(string));
        dt.Columns.Add("TODATE", typeof(string));
        dt.Columns.Add("ADDITIONALINFO", typeof(string));

        HttpContext.Current.Session["SLABWISETAXMAPPING"] = dt;
    }
    #endregion

    # region Slab Mapping Click
    protected void btnSlabwiseTaxmapping_Click(object sender, EventArgs e)
    {
        try
        {

            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";

            pnlsalvemapping.Style["display"] = "";
            this.CreateSlabMapping();
            pnlvendorgroupmapping.Style["display"] = "none";
            this.txtfromamount.Text = "";
            this.txttoamount.Text = "";
            this.txtPercentage.Text = "0";
            this.txtvalue.Text = "0";
            this.txtaddninfo.Text = "";
            //this.ddlsupplieditem.SelectedValue = "0";

            this.gvSlabwisetaxmapping.ClearPreviousDataSource();
            this.gvSlabwisetaxmapping.DataSource = null;
            this.gvSlabwisetaxmapping.DataBind();


            ClsTaxSheetMaster clstaxsheet = new ClsTaxSheetMaster();
            DataTable dtedit = clstaxsheet.EditSlabWiseTaxMapping(this.Hdn_Fld.Value);

            DataTable dtcreate = new DataTable();
            if (HttpContext.Current.Session["SLABWISETAXMAPPING"] != null)
            {
                dtcreate = (DataTable)HttpContext.Current.Session["SLABWISETAXMAPPING"];
            }
            ////if (dtadd.Rows.Count > 0)
            ////{

            for (int i = 0; i < dtedit.Rows.Count; i++)
            {
                DataRow dr = dtcreate.NewRow();
                dr["GUID"] = Guid.NewGuid();
                dr["TAXID"] = Convert.ToString(dtedit.Rows[i]["TAXID"]).Trim();
                dr["TAXNAME"] = Convert.ToString(dtedit.Rows[i]["TAXNAME"]).Trim();
                dr["TYPEID"] = Convert.ToString(dtedit.Rows[i]["TYPEID"]).Trim();
                dr["TYPENAME"] = Convert.ToString(dtedit.Rows[i]["TYPENAME"]).Trim();
                dr["FROMAMOUNT"] = Convert.ToDecimal(dtedit.Rows[i]["FROMAMOUNT"]).ToString().Trim();
                dr["TOAMOUNT"] = Convert.ToDecimal(dtedit.Rows[i]["TOAMOUNT"]).ToString().Trim();
                dr["VALUE"] = Convert.ToDecimal(dtedit.Rows[i]["VALUE"]).ToString().Trim();
                dr["PERCENTAGE"] = Convert.ToDecimal(dtedit.Rows[i]["PERCENTAGE"]).ToString().Trim();

                dr["FROMDATE"] = Convert.ToString(dtedit.Rows[i]["FROMDATE"]).Trim();
                dr["TODATE"] = Convert.ToString(dtedit.Rows[i]["TODATE"]).Trim();
                dr["ADDITIONALINFO"] = Convert.ToString(dtedit.Rows[i]["ADDITIONALINFO"]).Trim();
                dr["LEDGERID"] = Convert.ToString(dtedit.Rows[i]["LEDGERID"]).Trim();
                dr["LEDGERNAME"] = Convert.ToString(dtedit.Rows[i]["LEDGERNAME"]).Trim();
                dtcreate.Rows.Add(dr);
                dtcreate.AcceptChanges();
            }

            HttpContext.Current.Session["SLABWISETAXMAPPING"] = dtcreate;
            gvSlabwisetaxmapping.DataSource = dtcreate;
            gvSlabwisetaxmapping.DataBind();
            ////}


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region Cancel
    protected void btnSlabMappingCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            btnaddhide.Style["display"] = "";
            pnlsalvemapping.Style["display"] = "none";
            this.txtfromamount.Text = "";
            this.txttoamount.Text = "";
            this.txtPercentage.Text = "";
            this.txtvalue.Text = "";
            this.gvSlabwisetaxmapping.ClearPreviousDataSource();
            this.gvSlabwisetaxmapping.DataSource = null;
            this.gvSlabwisetaxmapping.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region ADD
    protected void btnSlabAdd_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dtadd = new DataTable();
            int numberOfRecords = 0;

            if (HttpContext.Current.Session["SLABWISETAXMAPPING"] != null)
            {
                dtadd = (DataTable)HttpContext.Current.Session["SLABWISETAXMAPPING"];
            }

            decimal FROMAMT = Convert.ToDecimal(this.txtfromamount.Text.ToString().Trim());
            decimal TOAMT = Convert.ToDecimal(this.txttoamount.Text.ToString().Trim());
            decimal percentage = Convert.ToDecimal(this.txtslbpercentage.Text.ToString().Trim());
            decimal value = Convert.ToDecimal(this.txtvalue.Text.ToString().Trim());


            if (TOAMT > FROMAMT)
            {
                if (percentage != 0 && value == 0 || percentage == 0 && value != 0 || percentage == 0 && value == 0)
                {
                    numberOfRecords = dtadd.Select("FROMAMOUNT = '" + Convert.ToDecimal(this.txtfromamount.Text).ToString().Trim() + "' AND TOAMOUNT= '" + Convert.ToDecimal(this.txttoamount.Text).ToString().Trim() + "' AND VALUE= '" + Convert.ToDecimal(this.txtvalue.Text).ToString().Trim() + "' AND PERCENTAGE='" + Convert.ToDecimal(this.txtslbpercentage.Text).ToString().Trim() + "'").Length;
                    if (numberOfRecords > 0)
                    {
                        MessageBox1.ShowInfo("Value and Percentage  are already exist with same FromAmount and same ToAmount ", 60, 700);
                    }
                    else
                    {

                        DataRow dr = dtadd.NewRow();
                        dr["GUID"] = Guid.NewGuid();
                        dr["TAXID"] = Convert.ToString(this.Hdn_Fld.Value).Trim();
                        dr["TAXNAME"] = Convert.ToString(this.txttaxname.Text).Trim();
                        dr["TYPEID"] = Convert.ToString(this.ddlsupplieditem.SelectedValue).Trim();
                        dr["TYPENAME"] = Convert.ToString(this.ddlsupplieditem.SelectedItem.Text).Trim();
                        dr["FROMAMOUNT"] = Convert.ToDecimal(this.txtfromamount.Text).ToString().Trim();
                        dr["TOAMOUNT"] = Convert.ToDecimal(this.txttoamount.Text).ToString().Trim();
                        dr["VALUE"] = Convert.ToDecimal(this.txtvalue.Text).ToString().Trim();
                        dr["PERCENTAGE"] = Convert.ToDecimal(this.txtslbpercentage.Text).ToString().Trim();

                        dr["LEDGERID"] = Convert.ToString(this.ddlledger.SelectedValue.ToString().Trim());
                        dr["LEDGERNAME"] = Convert.ToString(this.ddlledger.SelectedItem.Text.Trim());
                        dr["FROMDATE"] = Convert.ToString(this.txtfromdate.Text.Trim());
                        dr["TODATE"] = Convert.ToString(this.txttodate.Text.Trim());
                        dr["ADDITIONALINFO"] = Convert.ToString(this.txtaddninfo.Text.Trim());

                        dtadd.Rows.Add(dr);
                        dtadd.AcceptChanges();


                    }
                }
                else
                {
                    MessageBox1.ShowInfo(" <b>Please enter a value either on  Percentage or Value  </b>", 60, 500);
                    return;
                }

            }
            else
            {
                MessageBox1.ShowInfo(" <b>FromAmount must be greater than  Toamount </b>", 60, 400);
                return;
            }


            if (dtadd.Rows.Count > 0)
            {
                this.gvSlabwisetaxmapping.DataSource = dtadd;
                this.gvSlabwisetaxmapping.DataBind();
            }
            else
            {
                this.gvSlabwisetaxmapping.ClearPreviousDataSource();
                this.gvSlabwisetaxmapping.DataSource = null;
                this.gvSlabwisetaxmapping.DataBind();
            }
            this.txtPercentage.Text = "0";
            this.txtvalue.Text = "0";
            this.txtfromamount.Text = "";
            this.txttoamount.Text = "";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    # region Submit
    protected void btnSlabMappingSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTaxSheetMaster clstaxsheet = new ClsTaxSheetMaster();
            DataTable dtSlab = new DataTable();
            dtSlab = (DataTable)HttpContext.Current.Session["SLABWISETAXMAPPING"];
            string XML = string.Empty;
            XML = ConvertDatatableToXML(dtSlab);
            int ID = 0;
            if (dtSlab.Rows.Count > 0)
            {
                ID = clstaxsheet.SaveSlabWiseTaxMapping(this.Hdn_Fld.Value, XML);
                if (ID > 0)
                {
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    PlntateWise.Style["display"] = "none";
                    plnException.Style["display"] = "none";
                    PlnProduct.Style["display"] = "none";
                    pnlcategory.Style["display"] = "none";
                    pnlvendorgroupmapping.Style["display"] = "none";
                    btnaddhide.Style["display"] = "";
                    pnlsalvemapping.Style["display"] = "none";
                    this.gvSlabwisetaxmapping.ClearPreviousDataSource();
                    gvSlabwisetaxmapping.DataSource = null;
                    gvSlabwisetaxmapping.DataBind();
                    this.txtfromamount.Text = "";
                    this.txttoamount.Text = "";
                    this.txtPercentage.Text = "";
                    this.txtvalue.Text = "";
                    this.ddlsupplieditem.SelectedValue = "0";
                }
                else
                {
                    MessageBox1.ShowError("Error on saving");
                }

            }
            else
            {
                MessageBox1.ShowInfo("Please Save atleast 1 record");
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btndelete_Click
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            string GUID = Convert.ToString(hdn_Slabtax.Value);
            DataTable dt = new DataTable();
            dt = (DataTable)Session["SLABWISETAXMAPPING"];

            DataRow[] drr = dt.Select("GUID='" + GUID + "'");
            for (int i = 0; i < drr.Length; i++)
            {
                drr[i].Delete();
                dt.AcceptChanges();
            }
            this.gvSlabwisetaxmapping.DataSource = dt;
            this.gvSlabwisetaxmapping.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadLedger
    public void LoadLedger()
    {
        ClsAppMaster clsappmaster = new ClsAppMaster();
        ddlledger.Items.Clear();
        ddlledger.Items.Insert(0, new ListItem("ALL", "-1"));
        ddlledger.DataSource = clsappmaster.BindLeadger();
        ddlledger.DataTextField = "name";
        ddlledger.DataValueField = "Id";
        ddlledger.DataBind();
    }
    #endregion

    #endregion

    #region CategoryMapping

    #region CreateTable
    protected void CreateCategoryMapping()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("CATEGORYID", typeof(string));
        dt.Columns.Add("PERCENTAGE", typeof(string));
        dt.Columns.Add("FROMDATE", typeof(string));
        dt.Columns.Add("TODATE", typeof(string));

        HttpContext.Current.Session["CATEGORYTAXMAPPING"] = dt;
    }
    #endregion

    #region btncategoryMappingsubmit_Click
    protected void btncategoryMappingsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTaxSheetMaster clstaxsheet = new ClsTaxSheetMaster();
            DataTable dtCategory = new DataTable();
            int COUNT = 0;
            this.CreateCategoryMapping();

            if (HttpContext.Current.Session["CATEGORYTAXMAPPING"] != null)
            {
                dtCategory = (DataTable)HttpContext.Current.Session["CATEGORYTAXMAPPING"];
            }

            foreach (GridViewRow row in grdcategorydetails.Rows)
            {
                Label lblcategoryid = row.FindControl("lblcatid") as Label;
                TextBox txtpercent = row.FindControl("txtpercentage") as TextBox;
                TextBox txtfromdate = row.FindControl("grdtxtfromdate") as TextBox;
                TextBox txttodate = row.FindControl("grdtxttodate") as TextBox;
                CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;

                if (chkSelect.Checked)
                {
                    if (!String.IsNullOrEmpty(txtpercent.Text.Trim()))
                    {
                        //if (Convert.ToDecimal(txtpercent.Text.Trim()) != 0)
                        //{
                        DataRow dr = dtCategory.NewRow();
                        dr["CATEGORYID"] = lblcategoryid.Text;
                        dr["PERCENTAGE"] = Convert.ToString(txtpercent.Text.Trim());
                        dr["FROMDATE"] = Convert.ToString(txtFromDate1.Text.Trim());
                        dr["TODATE"] = Convert.ToString(txtToDate1.Text.Trim());

                        dtCategory.Rows.Add(dr);
                        dtCategory.AcceptChanges();
                        //}
                        COUNT = COUNT + 1;
                    }
                }
            }

            string XML = string.Empty;
            XML = ConvertDatatableToXML(dtCategory);
            int ID = 0;
            if (COUNT > 0)
            {

                if (dtCategory.Rows.Count > 0)
                {
                    ID = clstaxsheet.SaveCategoryWiseTaxMapping(this.Hdn_Fld.Value, XML, this.txtFromDate1.Text.Trim(), this.txtToDate1.Text.Trim());
                    if (ID > 0)
                    {
                        ClsTaxSheetMaster clstax = new ClsTaxSheetMaster();
                        int updateid = clstax.UpdateLdomTaxMaster(Hdn_Fld.Value.Trim());
                        MessageBox1.ShowSuccess("Record Saved Successfully..");
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        PlntateWise.Style["display"] = "none";
                        plnException.Style["display"] = "none";
                        PlnProduct.Style["display"] = "none";
                        pnlcategory.Style["display"] = "none";
                        pnlvendorgroupmapping.Style["display"] = "none";
                        btnaddhide.Style["display"] = "";
                        pnlsalvemapping.Style["display"] = "none";
                        this.gvSlabwisetaxmapping.DataSource = null;
                        this.gvSlabwisetaxmapping.DataBind();
                        this.txtopentaxname.Text = "";
                        this.Session.Remove("CATEGORYTAXMAPPING");
                        COUNT = 0;
                    }
                    else
                    {
                        MessageBox1.ShowError("Error on saving");
                    }

                }
                else
                {
                    MessageBox1.ShowInfo("Please Save atleast 1 record");
                    return;
                }
            }
            else
            {
                MessageBox1.ShowInfo("Please Select atleast 1 record");
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btncategoryMappingcancel_Click
    protected void btncategoryMappingcancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            btnaddhide.Style["display"] = "";
            pnlsalvemapping.Style["display"] = "none";
            this.grdcategorydetails.DataSource = null;
            this.grdcategorydetails.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #endregion

    #region VendorGroupMapping

    #region CreateTable
    protected void CreateGroupMapping()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("GROUPID", typeof(string));
        dt.Columns.Add("PERCENTAGE", typeof(string));
        dt.Columns.Add("TYPE", typeof(string));

        HttpContext.Current.Session["GROUPTAXMAPPING"] = dt;
    }
    #endregion

    #region ddlgrouptype_SelectedIndexChanged
    protected void ddlgrouptype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.BindVendorGroup(this.ddlgrouptype.SelectedValue.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion


    public void BindVendorGroup(string Type)
    {
        try
        {
            ClsTaxSheetMaster clstxmaster = new ClsTaxSheetMaster();
            DataTable dt = new DataTable();
            dt = clstxmaster.BindVendorDetails(this.Hdn_Fld.Value, Type);
            if (dt.Rows.Count > 0)
            {
                this.grdvendormapping.DataSource = dt;
                this.grdvendormapping.DataBind();
            }
            else
            {
                this.grdvendormapping.DataSource = null;
                this.grdvendormapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #region btngridGroup_Click
    protected void btngridGroup_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlntateWise.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "";

            this.BindVendorGroup(this.ddlgrouptype.SelectedValue.Trim());

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion


    #region btngroupsubmit_Click
    protected void btngroupsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsTaxSheetMaster clstaxsheet = new ClsTaxSheetMaster();
            DataTable dtGroup = new DataTable();

            this.CreateGroupMapping();

            if (HttpContext.Current.Session["GROUPTAXMAPPING"] != null)
            {
                dtGroup = (DataTable)HttpContext.Current.Session["GROUPTAXMAPPING"];
            }

            foreach (GridViewRow row in grdvendormapping.Rows)
            {
                Label lblcategoryid = row.FindControl("lblgroupid") as Label;
                TextBox txtpercent = row.FindControl("txtgrouppercentage") as TextBox;

                if (!String.IsNullOrEmpty(txtpercent.Text.Trim()))
                {
                    if (Convert.ToDecimal(txtpercent.Text.Trim()) != 0)
                    {
                        DataRow dr = dtGroup.NewRow();
                        dr["GROUPID"] = lblcategoryid.Text;
                        dr["PERCENTAGE"] = Convert.ToString(txtpercent.Text.Trim());
                        dr["TYPE"] = Convert.ToString(this.ddlgrouptype.SelectedValue.Trim());

                        dtGroup.Rows.Add(dr);
                        dtGroup.AcceptChanges();
                    }
                }
            }

            string XML = string.Empty;
            XML = ConvertDatatableToXML(dtGroup);
            int ID = 0;
            if (dtGroup.Rows.Count > 0)
            {
                ID = clstaxsheet.SaveVendorWiseTaxMapping(this.Hdn_Fld.Value, this.ddlgrouptype.SelectedValue.Trim(), XML);
                if (ID > 0)
                {
                    ClsTaxSheetMaster clstax = new ClsTaxSheetMaster();
                    int updateid = clstax.UpdateLdomTaxMaster(Hdn_Fld.Value.Trim());
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    PlntateWise.Style["display"] = "none";
                    plnException.Style["display"] = "none";
                    PlnProduct.Style["display"] = "none";
                    pnlcategory.Style["display"] = "none";
                    pnlvendorgroupmapping.Style["display"] = "none";
                    btnaddhide.Style["display"] = "";
                    pnlsalvemapping.Style["display"] = "none";
                    pnlvendorgroupmapping.Style["display"] = "none";
                    this.gvSlabwisetaxmapping.DataSource = null;
                    this.gvSlabwisetaxmapping.DataBind();
                    this.txtopentaxname.Text = "";
                    this.Session.Remove("GROUPTAXMAPPING");
                }
                else
                {
                    MessageBox1.ShowError("Error on saving");
                }

            }
            else
            {
                MessageBox1.ShowInfo("Please Save atleast 1 record");
                return;
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region btngroupcancel_Click
    protected void btngroupcancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            PlntateWise.Style["display"] = "none";
            plnException.Style["display"] = "none";
            PlnProduct.Style["display"] = "none";
            pnlcategory.Style["display"] = "none";
            btnaddhide.Style["display"] = "";
            pnlsalvemapping.Style["display"] = "none";
            pnlvendorgroupmapping.Style["display"] = "none";
            this.grdcategorydetails.DataSource = null;
            this.grdcategorydetails.DataBind();
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