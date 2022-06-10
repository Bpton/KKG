using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmDepotTransferRateSheet_RMPM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadPrimarySubItem();
            LoadCustomerRatesheetMaster();
        }
    }
    public void LoadCustomerRatesheetMaster()
    {
        try
        {
            ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
            string ID = string.Empty;
            String[] PrimaryID = this.ddlSearchprimaryitem.SelectedValue.Trim().Split('~');
            ID = PrimaryID[1].Trim();
            string BrandID = ID;
            string CategoryID = ddlSearchsubitem.SelectedValue;
            gvCustomerRatesheet.DataSource = clsdepot.BindCustomerRateSheetGrid(BrandID, CategoryID);
            gvCustomerRatesheet.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void Bindgrid()
    {
        ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
        gvCustomerRSMap.DataSource = clsdepot.BindRSGrid(ddlprimaryitem.SelectedValue, ddlsubitem.SelectedValue);
        gvCustomerRSMap.DataBind();
    }

    public void LoadPrimarySubItem()
    {
        try
        {
            ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
            DataTable dt = clsdepot.BindPrimaryitemtype();
            if (dt.Rows.Count > 0)
            {
                ddlprimaryitem.Items.Clear();
                ddlprimaryitem.Items.Insert(0, new ListItem("Select Primary", "0"));
                ddlprimaryitem.DataSource = dt;
                ddlprimaryitem.DataTextField = "ITEMDESC";
                ddlprimaryitem.DataValueField = "ID";
                ddlprimaryitem.DataBind();

                ddlSearchprimaryitem.Items.Clear();
                ddlSearchprimaryitem.Items.Insert(0, new ListItem("Select ", "0"));
                ddlSearchprimaryitem.DataSource = dt;
                ddlSearchprimaryitem.DataTextField = "ITEMDESC";
                ddlSearchprimaryitem.DataValueField = "ID";
                ddlSearchprimaryitem.DataBind();
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void LoadSubItem(string PTYPEID)
    {
        ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
        DataTable dt = clsdepot.BindSubitemtype(PTYPEID);
        if (dt.Rows.Count > 0)
        {
            ddlsubitem.Items.Clear();
            ddlsubitem.Items.Insert(0, new ListItem("Select Sub Item", "0"));
            ddlsubitem.DataSource = dt;
            ddlsubitem.DataTextField = "SUBITEMDESC";
            ddlsubitem.DataValueField = "SUBTYPEID";
            ddlsubitem.DataBind();
        }
    }

    protected void LoadsearchSubItem(string PTYPEID)
    {
        ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
        DataTable dt = clsdepot.BindSubitemtype(PTYPEID);
        if (dt.Rows.Count > 0)
        {
            ddlSearchsubitem.Items.Clear();
            ddlSearchsubitem.Items.Insert(0, new ListItem("Select Sub Item", "0"));
            ddlSearchsubitem.DataSource = dt;
            ddlSearchsubitem.DataTextField = "SUBITEMDESC";
            ddlSearchsubitem.DataValueField = "SUBTYPEID";
            ddlSearchsubitem.DataBind();
        }
    }

    protected void ddlprimaryitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ID = string.Empty;
        String[] PrimaryID = this.ddlprimaryitem.SelectedValue.Trim().Split('~');
        ID = PrimaryID[1].Trim();
        this.LoadSubItem(ID.Trim());
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

    #region Create DataTable Structure
    public void CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("BSID", typeof(string)));
        dt.Columns.Add(new DataColumn("BSNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("DIVISIONID", typeof(string)));
        dt.Columns.Add(new DataColumn("DIVISIONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("CATAGORYID", typeof(string)));
        dt.Columns.Add(new DataColumn("CATAGORYNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTID", typeof(string)));
        dt.Columns.Add(new DataColumn("PRODUCTNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("RATE", typeof(string)));
        dt.Columns.Add(new DataColumn("FROMDATE", typeof(string)));
        dt.Columns.Add(new DataColumn("TODATE", typeof(string)));
        dt.Columns.Add(new DataColumn("MRP", typeof(string)));
        HttpContext.Current.Session["DEPOTPRODUCTDETAILS"] = dt;


    }
    #endregion

    protected void btnCustomerSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string xml = string.Empty;
            ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
            DataTable dtadd = new DataTable();
            int ID = 0;
            string Mode = "A";
            string PID = string.Empty;
            string PNAME = string.Empty;

            String[] PrimaryID = this.ddlprimaryitem.SelectedValue.Trim().Split('~');
            PID = PrimaryID[1].Trim();
            PNAME = PrimaryID[0].Trim();

            if (HttpContext.Current.Session["DEPOTPRODUCTDETAILS"] == null)
            {
                CreateDataTable();
            }

            dtadd = (DataTable)HttpContext.Current.Session["DEPOTPRODUCTDETAILS"];
            int count = 0;

            for (int i = 0; i < gvCustomerRSMap.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cellproductid = gvCustomerRSMap.RowsInViewState[i].Cells[0] as GridDataControlFieldCell;
               GridDataControlFieldCell cell = gvCustomerRSMap.RowsInViewState[i].Cells[2] as GridDataControlFieldCell;
                GridDataControlFieldCell cellmrp = gvCustomerRSMap.RowsInViewState[i].Cells[2] as GridDataControlFieldCell;

                CheckBox chk = cellproductid.FindControl("ChkID") as CheckBox;
                HiddenField hdnproductid = cellproductid.FindControl("hdnPRODUCTID") as HiddenField;/*Product ID*/

                HiddenField hdnproductname = cell.FindControl("hdnPS") as HiddenField;/*Product Name*/
                TextBox txtmrp = cell.FindControl("txtmrpcost") as TextBox;


                TextBox updatemrp = cellmrp.FindControl("txtmrp_cost") as TextBox;

                if (chk.Checked)
                {
                    if (txtmrp.Text.Trim() != "")
                    {
                        //if ((Convert.ToDecimal(txtmrp.Text.Trim()) != 0))
                        //{
                        DataRow dr = dtadd.NewRow();
                        dr["DIVISIONID"] = Convert.ToString(PID.Trim());
                        dr["DIVISIONNAME"] = Convert.ToString(PNAME).Trim();
                        dr["CATAGORYID"] = Convert.ToString(this.ddlsubitem.SelectedValue.Trim());
                        dr["CATAGORYNAME"] = Convert.ToString(this.ddlsubitem.SelectedItem).Trim();
                        dr["PRODUCTID"] = hdnproductid.Value.Trim();
                        dr["PRODUCTNAME"] = hdnproductname.Value.Trim();
                        dr["RATE"] = Convert.ToDecimal(txtmrp.Text.ToString());
                        dr["FROMDATE"] = Convert.ToString(this.txtFromDate.Text).Trim();
                        dr["TODATE"] = Convert.ToString(this.txtToDate.Text).Trim();
                        dr["MRP"] = "0";//Convert.ToDecimal(updatemrp.Text.ToString());
                        dtadd.Rows.Add(dr);
                        dtadd.AcceptChanges();
                        count = count + 1;
                        //}
                    }
                }
            }

            if (dtadd.Rows.Count > 0)
            {
                HttpContext.Current.Session["DEPOTPRODUCTDETAILS"] = dtadd;
                xml = ConvertDatatable(dtadd);

            }
            if (count == 0)
            {
                MessageBox1.ShowInfo("Please Insert atleast 1 Rate!");
                Session["DEPOTPRODUCTDETAILS"] = null;
                return;
            }
            else
            {
                ID = clsdepot.SaveCustomerRateSheetMap(xml, this.txtFromDate.Text.Trim(), this.txtToDate.Text.Trim());
                if (ID > 0)
                {
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    LoadCustomerRatesheetMaster();
                    Hdn_Fld.Value = "";
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";

                    gvCustomerRSMap.DataSource = null;
                    gvCustomerRSMap.DataBind();
                    gvCustomerRatesheet.DataSource = null;
                    gvCustomerRatesheet.DataBind();
                    this.ddlSearchprimaryitem.SelectedValue = "0";
                    this.ddlSearchsubitem.SelectedValue = "0";
                    Session["DEPOTPRODUCTDETAILS"] = null;
                }
            }

            //clsdepot.DeleteRateMaster(PID.Trim(), ddlsubitem.SelectedValue);
            //for (int i = 0; i < gvCustomerRSMap.RowsInViewState.Count; i++)
            //{
            //    GridDataControlFieldCell cell = gvCustomerRSMap.RowsInViewState[i].Cells[2] as GridDataControlFieldCell;
            //    HiddenField hiddenField = cell.FindControl("hdnPS") as HiddenField;
            //    TextBox txtmrp = cell.FindControl("txtmrpcost") as TextBox;


            //    if (txtmrp.Text.Trim() != "")
            //    {
            //        if ((Convert.ToDecimal(txtmrp.Text.Trim()) != 0))
            //        {
            //            ID = clsdepot.SaveCustomerRateSheetMap(PID.Trim(), PNAME.Trim(), ddlsubitem.SelectedValue.ToString(), ddlsubitem.SelectedItem.ToString(), hiddenField.Value, txtmrp.ToolTip, Convert.ToDecimal(txtmrp.Text.ToString()), Mode);
            //            txtmrp.Text = "0";
            //        }
            //        //else
            //        //{
            //        //    ID = 0;
            //        //}
            //    }
            //}

            //if (ID == 1)
            //{
           
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
            ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
            if (e.Record["PRODUCTID"] != "")
            {
                int ID = 0;
                ID = clsdepot.CustomerRatesheetDelete(e.Record["PRODUCTID"].ToString().Trim(), e.Record["RATE"].ToString().Trim(),
                                                      e.Record["FROMDATE"].ToString().Trim(), e.Record["TODATE"].ToString().Trim());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }
                LoadCustomerRatesheetMaster();
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

    protected void btnAddRateSheet_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            LoadCustomerRatesheetMaster();
            LoadPrimarySubItem();
            ddlprimaryitem.SelectedValue = "0";
            ddlsubitem.SelectedValue = "0";
            gvCustomerRSMap.DataSource = null;
            gvCustomerRSMap.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCustomerCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";

            gvCustomerRatesheet.DataSource = null;
            gvCustomerRatesheet.DataBind();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlSearchsubitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCustomerRatesheetMaster();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void ddlSearchprimaryitem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string ID = string.Empty;
            String[] PrimaryID = this.ddlSearchprimaryitem.SelectedValue.Trim().Split('~');
            ID = PrimaryID[1].Trim();
            this.LoadsearchSubItem(ID.Trim());
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    private void PoPulateGrid(string DIVISIONID, string CATAGORYID)
    {
        try
        {
            ClsDepotRatesheet_RMPM clsdepot = new ClsDepotRatesheet_RMPM();
            DataTable dtratesheet = new DataTable();
            dtratesheet = clsdepot.FetchProductDetails(DIVISIONID, CATAGORYID);
            gvCustomerRSMap.DataSource = dtratesheet;
            gvCustomerRSMap.DataBind();
            gvCustomerRSMap.Columns[2].HeaderText = @"TRANSFER RATE(Rs.)";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)/*Show Btn*/
    {
        try
        {
            String[] PrimaryID = this.ddlprimaryitem.SelectedValue.Trim().Split('~');
            ID = PrimaryID[1].Trim();
            PoPulateGrid(ID.Trim(), this.ddlsubitem.SelectedValue);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}