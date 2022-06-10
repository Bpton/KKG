using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmReasonMaster : System.Web.UI.Page
{
    ClsReasonMaster ClsReasonMaster = new ClsReasonMaster();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                trDebitTo.Style["display"] = "none";
                trStoreLocation.Style["display"] = "none";
                if (Request.QueryString["MENU"] == "ReasonMaster")
                {
                    LoadReasonMaster();
                    btnaddhide.Style["display"] = "";
                    tdExcel.Style["display"] = "none";
                }
                else
                {
                    LoadReasonMaster();
                    btnaddhide.Style["display"] = "none";
                    tdExcel.Style["display"] = "";
                }

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


            dt = ClsReasonMaster.BindPageName();
            gvReasonmap.DataSource = ClsReasonMaster.BindPageName();
            gvReasonmap.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadReasonMaster()
    {
        try
        {
            if (Request.QueryString["MENU"] == "ReasonMaster")
            {
                gvReason.DataSource = ClsReasonMaster.BindReasonMaster();
                gvReason.DataBind();
            }
            else
            {
                gvReason.DataSource = ClsReasonMaster.BindReasonMaster();
                gvReason.Columns[10].Visible = false;
                gvReason.Columns[9].Visible = false;
                gvReason.DataBind();
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public DataTable LoadReasonMapbyid(string id)
    {
        DataTable dtreason = new DataTable();

        try
        {

            dtreason = ClsReasonMaster.BindPageNamebyid(id);

        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return dtreason;
    }
    protected void btnAddReason_Click(object sender, EventArgs e)
    {
        try
        {

            txtName.Text = "";
            Textdesc.Text = "";
            //ddlPageName.SelectedIndex = 0;
            this.LoadPageName();
            this.LoadStoreLocation();
            chkApproved.Checked = true;
            chkStockRelated.Checked = false;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            trDebitTo.Style["display"] = "none";
            trStoreLocation.Style["display"] = "none";
            this.ddlStoreLocation.SelectedValue = "0";
            this.ddlDebitTo.SelectedValue = "0";
            gvReason.ClearPreviousDataSource();
            gvReason.DataSource = null;
            gvReason.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnReasonSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Mode = "";
            string StockRelated = string.Empty;
            int count = 0;

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
                ClsReasonMaster.DeleteReasonBYID(Hdn_Fld.Value);
            }

            int ID = 0;
            int MappingID = 0;

            if (this.chkStockRelated.Checked == true)
            {
                StockRelated = "Y";
                //if (this.ddlDebitTo.SelectedValue == "0")
                //{
                //    MessageBox1.ShowInfo("Please Select Debit To");
                //    this.ddlDebitTo.Focus();
                //}
                if (this.ddlStoreLocation.SelectedValue == "0")
                {
                    MessageBox1.ShowInfo("Please Select Store Location");
                    this.ddlStoreLocation.Focus();
                }
                else if (this.ddlStoreLocation.SelectedValue != "0")
                {
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
                                ID = ClsReasonMaster.SaveReasonMaster(Hdn_Fld.Value.Trim(), txtName.Text.Trim(), Mode, chkApproved.Checked.ToString(),
                                                                        Textdesc.Text.Trim(), this.ddlDebitTo.SelectedValue, this.ddlDebitTo.SelectedItem.ToString().Trim(),
                                                                        this.ddlStoreLocation.SelectedValue, this.ddlStoreLocation.SelectedItem.ToString().Trim(),
                                                                        StockRelated);
                            }

                            if (count > 0)

                            {
                                MappingID = ClsReasonMaster.SaveUserReasoMapping(Convert.ToInt32(chk.ToolTip), txtName.Text, Mode, Hdn_Fld.Value);

                            }
                        }
                    }
                    if (count == 0)
                    {

                        //string message = "alert('Please Select atleast One Menu Option..')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowWarning("Please Select atleast One Menu Option..");
                    }

                    if (ID == 1)
                    {
                        //string message = "alert('Record Saved Successfully..')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowSuccess("Record Saved Successfully..");
                        LoadReasonMaster();
                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        btnaddhide.Style["display"] = "";
                        Hdn_Fld.Value = "";
                        trDebitTo.Style["display"] = "none";
                        trStoreLocation.Style["display"] = "none";
                        this.ddlStoreLocation.SelectedValue = "0";
                        this.ddlDebitTo.SelectedValue = "0";
                    }

                    else if (ID == 2)
                    {
                        //string message = "alert('Name already exist..')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowWarning("Name already exist..");

                    }
                    else if (ID == 3)
                    {
                        //string message = "alert('PageName already exist..')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowWarning("PageName already exist..");


                    }
                }
            }
            else
            {
                StockRelated = "N";
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
                            ID = ClsReasonMaster.SaveReasonMaster(Hdn_Fld.Value.Trim(), txtName.Text.Trim(), Mode, chkApproved.Checked.ToString(),
                                                                        Textdesc.Text.Trim(), this.ddlDebitTo.SelectedValue, this.ddlDebitTo.SelectedItem.ToString().Trim(),
                                                                        this.ddlStoreLocation.SelectedValue, this.ddlStoreLocation.SelectedItem.ToString().Trim(),
                                                                        StockRelated);
                        }
                        if (count > 0)
                        {
                            MappingID = ClsReasonMaster.SaveUserReasoMapping(Convert.ToInt32(chk.ToolTip), txtName.Text, Mode, Hdn_Fld.Value);

                        }
                    }
                }
                if (count == 0)
                {

                    //string message = "alert('Please Select atleast One Menu Option..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowWarning("Please Select atleast One Menu Option..");
                }

                if (ID == 1)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    LoadReasonMaster();
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    Hdn_Fld.Value = "";
                    trDebitTo.Style["display"] = "none";
                    trStoreLocation.Style["display"] = "none";
                    this.ddlStoreLocation.SelectedValue = "0";
                    this.ddlDebitTo.SelectedValue = "0";
                }

                else if (ID == 2)
                {
                    //string message = "alert('Name already exist..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowWarning("Name already exist..");

                }
                else if (ID == 3)
                {
                    //string message = "alert('PageName already exist..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowWarning("PageName already exist..");


                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnReasonCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //ddlPageName.SelectedValue = "0";
            //ddlActivity.SelectedValue = "0"; 
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            trDebitTo.Style["display"] = "none";
            trStoreLocation.Style["display"] = "none";
            this.ddlStoreLocation.SelectedValue = "0";
            this.ddlDebitTo.SelectedValue = "0";
            LoadReasonMaster();
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
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = ClsReasonMaster.DeleteReasonMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadReasonMaster();
                }
                else
                {
                    e.Record["Error"] = "Error On Deleting. ";
                }


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void LoadStoreLocation()
    {
        ClsReasonMaster ClsReasonMaster = new ClsReasonMaster();
        DataTable dt = new DataTable();
        dt = ClsReasonMaster.BindStoreLocation();
        if (dt.Rows.Count > 0)
        {
            this.ddlStoreLocation.Items.Clear();
            this.ddlStoreLocation.Items.Add(new ListItem("Select Store Location", "0"));
            this.ddlStoreLocation.AppendDataBoundItems = true;
            this.ddlStoreLocation.DataSource = dt;
            this.ddlStoreLocation.DataValueField = "ID";
            this.ddlStoreLocation.DataTextField = "NAME";
            this.ddlStoreLocation.DataBind();
        }
    }

    protected void btngridsave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClsReasonMaster.BindReasonMasterBYID(id);
            txtName.Text = dt.Rows[0]["NAME"].ToString();
            Textdesc.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            //ddlPageName.SelectedValue = dt.Rows[0]["MOID"].ToString();

            LoadPageName();
            this.LoadStoreLocation();

            if (dt.Rows[0]["ISAPPROVED"].ToString().Trim() == "Y")
            {
                chkApproved.Checked = true;
            }
            else
            {
                chkApproved.Checked = false;
            }

            if (dt.Rows[0]["STOCKRELATED"].ToString().Trim() == "Y")
            {
                this.chkStockRelated.Checked = true;
                this.ddlDebitTo.SelectedValue = dt.Rows[0]["DEBITEDTOID"].ToString().Trim();
                this.ddlStoreLocation.SelectedValue = dt.Rows[0]["STORELOCATIONID"].ToString().Trim();
                trDebitTo.Style["display"] = "";
                trStoreLocation.Style["display"] = "";
            }
            else
            {
                this.chkStockRelated.Checked = false;
                this.ddlDebitTo.SelectedValue = "0";
                this.ddlStoreLocation.SelectedValue = "0";
                trDebitTo.Style["display"] = "none";
                trStoreLocation.Style["display"] = "none";
            }

            //LoadReasonMapbyid(id);

            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }



    protected void gvReasonmap_RowDataBound(object sender, GridRowEventArgs e)
    {

        DataTable dtgvtermsmap = new DataTable();

        if (e.Row.RowType == GridRowType.DataRow && gvReasonmap.Rows.Count > 0)
        {
            dtgvtermsmap = LoadReasonMapbyid(Hdn_Fld.Value);
            GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
            CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
            HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;

            if (dtgvtermsmap.Rows.Count > 0)
            {
                for (int i = 0; i < dtgvtermsmap.Rows.Count; i++)
                {
                    if (dtgvtermsmap.Rows[i][1].ToString() == chk.ToolTip)
                    {
                        chk.Checked = true;

                    }

                }
            }

            //if (gvReasonmap.RowsInViewState.Count > e.Row.RowIndex)
            //{
            //    GridDataControlFieldCell cellProductID = e.Row.Cells[1] as GridDataControlFieldCell;
            //    CheckBox chkPID = cellProductID.FindControl("ChkIDTPU") as CheckBox;

            //    if (gvReasonmap.RowsInViewState != null)
            //    {
            //        if (gvReasonmap.RowsInViewState[e.Row.RowIndex].Cells.Count > 0)
            //        {

            //            GridDataControlFieldCell cellInViewState = gvReasonmap.RowsInViewState[e.Row.RowIndex].Cells[1] as GridDataControlFieldCell;
            //            CheckBox chkProductInViewState = cellInViewState.FindControl("ChkIDTPU") as CheckBox;
            //            chkPID.Checked = chkProductInViewState.Checked;
            //        }
            //    }
            //}
        }


        //if (Hdn_Fld.Value != "")
        //{
        //    LoadReasonMapbyid(Hdn_Fld.Value);

        //}

        //if (e.Row.RowType == GridRowType.DataRow && gvReasonmap.RowsInViewState.Count > 0)
        //{
        //    GridDataControlFieldCell cell = e.Row.Cells[1] as GridDataControlFieldCell;
        //    CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;

        //    GridDataControlFieldCell cellInViewState = gvReasonmap.RowsInViewState[e.Row.RowIndex].Cells[1] as GridDataControlFieldCell;
        //    CheckBox chkInViewState = cellInViewState.FindControl("ChkIDTPU") as CheckBox;

        //    if (cell.Value == chkInViewState.ToolTip)
        //    {
        //        chk.Checked = chkInViewState.Checked;
        //    }
        //}
    }

    protected void gvReason_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[8] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "YES")
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



    protected void chkStockRelated_CheckedChanged(object sender, EventArgs e)
    {
        if (chkStockRelated.Checked == true)
        {
            trDebitTo.Style["display"] = "";
            trStoreLocation.Style["display"] = "";
        }
        else
        {
            trDebitTo.Style["display"] = "none";
            trStoreLocation.Style["display"] = "none";
        }
    }

    #region gvReason_Exporting
    protected void gvReason_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 4;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Reason Details";
        cell.BackColor = Color.YellowGreen;

        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.RowSpan = 1;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);

    }
    #endregion

    #region gvReason_Exported
    protected void gvReason_Exported(object sender, Obout.Grid.GridExportEventArgs e)
    {
        foreach (TableRow item in e.Table.Rows)
        {
            int cellcount = item.Cells.Count;
            for (int i = 0; i < cellcount; i++)
            {
                item.Cells[i].Style.Add("border", "thin solid black");

            }

            //not using css.
            e.Table.GridLines = GridLines.None;

        }
    }
    #endregion
}