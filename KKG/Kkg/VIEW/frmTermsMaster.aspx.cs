using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Data;
using System.Drawing;

public partial class VIEW_frmTermsMaster : System.Web.UI.Page
{
    ClsTermsMaster clsterms = new ClsTermsMaster();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //Session["UserID"] = "1";
                
                //LoadPageName();
                //LoadTermsMaster();
                if (Request.QueryString["MENUID"] == "45")
                {
                    LoadTermsMaster();
                    this.Excelid.Style["display"] = "none";
                }
                else
                {
                    LoadTermsMaster();
                    this.Excelid.Style["display"] = "";
                    this.btnaddhide.Style["display"] = "none";
                }
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
               
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
            DataTable dt = new DataTable();
            dt = clsterms.BindPageName();
            gvtermsmap.DataSource = clsterms.BindPageName();
            gvtermsmap.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadTermsMaster()
    {
        try
        {
            if (Request.QueryString["MENUID"] == "45")
            {
                gvTerms.DataSource = clsterms.BindTermsMaster();
                gvTerms.DataBind();
            }
            else
            {
                gvTerms.DataSource = clsterms.BindTermsMaster();
                gvTerms.Columns[8].Visible = false;
                gvTerms.Columns[7].Visible = false;
                gvTerms.DataBind();
                
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAddTerms_Click(object sender, EventArgs e)
    {
        try
        {

            txtName.Text = "";
            Textdesc.Text = "";
            chkApproved.Checked = true;           
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            LoadPageName();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public DataTable LoadtermsMapbyid(string id)
    {
        DataTable dtLoadtermsMapbyid = new DataTable();
        try
        {
            dtLoadtermsMapbyid = clsterms.BindPageNamebyid(id);

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return dtLoadtermsMapbyid;
    }
    protected void btnTermsSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string Mode = "";
            int count = 0;

            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
                clsterms.DeleteTermsBYID(Hdn_Fld.Value);
            }
            int ID = 0;
            int MappingID = 0;
           
            for (int i = 0; i < gvtermsmap.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = gvtermsmap.RowsInViewState[i].Cells[1] as GridDataControlFieldCell;

                CheckBox chk = cell.FindControl("ChkIDTPU") as CheckBox;
                HiddenField hiddenField = cell.FindControl("hdnTPUName") as HiddenField;
                if (chk.Checked == true)
                {
                    count = count + 1;
                    if (count == 1)
                    {
                        ID = clsterms.SaveTermsMaster(Hdn_Fld.Value, txtName.Text, Mode, chkApproved.Checked.ToString(), Textdesc.Text.ToString());
                    }
                     if (count > 0)
                    {
                        MappingID = clsterms.SaveUsertermsMapping(Convert.ToInt32(chk.ToolTip), txtName.Text, Mode, Hdn_Fld.Value);
                    }

                }

            }
                     if (count == 0)
                    {
                        //string message = "alert('Select atleast 1 menu option')";
                        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        MessageBox1.ShowWarning("Select atleast 1 menu option..");
                    }


                if (ID == 1)
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                    LoadTermsMaster();
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    Hdn_Fld.Value = "";
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
        
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnTermsCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //ddlPageName.SelectedValue = "0";
            //ddlActivity.SelectedValue = "0"; 
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadTermsMaster();
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
                ID = clsterms.DeleteTermsMaster(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadTermsMaster();
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
    protected void btngridsave_Click(object sender, EventArgs e)
    {
        try
        {
            string id = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = clsterms.BindTermsMasterBYID(id);
            txtName.Text = dt.Rows[0]["NAME"].ToString();
            Textdesc.Text = dt.Rows[0]["DESCRIPTION"].ToString();  
            //ddlPageName.SelectedValue = dt.Rows[0]["MOID"].ToString();
            if (dt.Rows[0]["ISAPPROVED"].ToString()=="Y")
            {
                chkApproved.Checked = true;
            }
            else
            {
                chkApproved.Checked = false;
            }
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            LoadPageName();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void gvtermsmap_RowDataBound(object sender, GridRowEventArgs e)
    {
        DataTable dtgvtermsmap = new DataTable();
        //if (Hdn_Fld.Value != "")
        //{
           // LoadtermsMapbyid(Hdn_Fld.Value);

        //}
        if (e.Row.RowType == GridRowType.DataRow && gvtermsmap.Rows.Count > 0)
        {
            dtgvtermsmap=LoadtermsMapbyid(Hdn_Fld.Value);
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
        }
    }

    protected void gvTerms_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[6] as GridDataControlFieldCell;
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

    #region gvTerms_Exporting
    protected void gvTerms_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 3;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Terms & Conditions Details";
        cell.BackColor = Color.YellowGreen;
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);

    }
    #endregion

    #region gvTerms_Exported
    protected void gvTerms_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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