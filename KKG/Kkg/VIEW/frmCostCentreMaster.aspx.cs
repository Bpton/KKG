using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmCostCentreMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadCostCatagoryMaster();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCostCatagoryMaster()
    {
        try
        {
            ClsCostCentre clscostcentre = new ClsCostCentre();
            grdcostcentre.DataSource = clscostcentre.BindCostCentreMastergrid();
            grdcostcentre.DataBind();
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
            ClsCostCentre clscostcentre = new ClsCostCentre();
            this.txtcostcentrecode.Text = "";
            this.txtcostcentreDescription.Text = "";
            this.txtcostcentrename.Text = "";
            this.chkActive.Checked = true;
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btncentresubmit_click(object sender, EventArgs e)
    {
        try
        {
            ClsCostCentre clscostcentre = new ClsCostCentre();
            string Mode = string.Empty;
            if (Hdn_Fld.Value == "")
            {
                Mode = "A";
            }
            else
            {
                Mode = "U";
            }
            int ID = 0;

            ID = clscostcentre.SaveCostCentreMaster(Hdn_Fld.Value, txtcostcentrecode.Text.Trim(), txtcostcentrename.Text.Trim(), txtcostcentreDescription.Text.Trim(), HttpContext.Current.Session["USERID"].ToString().Trim(), Mode, chkActive.Checked.ToString().Trim());

            if (ID == 1)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadCostCatagoryMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 2)
            {
                MessageBox1.ShowInfo("</b> Code already exist..");
            }
            else if (ID == 3)
            {
                MessageBox1.ShowInfo("</b> Name already exist..");
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btncentrecancel_click(object sender, EventArgs e)
    {
        try
        {
            ClsCostCentre clscostcentre = new ClsCostCentre();

            this.txtcostcentrecode.Text = "";
            this.txtcostcentrename.Text = "";
            this.txtcostcentreDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
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
            ClsCostCentre clscostcentre = new ClsCostCentre();

            if (e.Record["COSTCENTREID"] != "")
            {
                int ID = 0;
                ID = clscostcentre.DeleteCostCentreMaster(e.Record["COSTCENTREID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadCostCatagoryMaster();
                }
                else if (ID < 0)
                {
                    e.Record["Error"] = "The CostCentre exists in the transaction voucher..";
                    LoadCostCatagoryMaster();
                }
                else if (ID == 0)
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

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        ClsCostCentre clscostcentre = new ClsCostCentre();

        try
        {

            if (Hdn_Fld.Value != "")
            {
                string id = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();
                dt = clscostcentre.BindCostCentreMastergrideditbyid(id);
                if (dt.Rows.Count > 0)
                {
                    this.txtcostcentrecode.Text = dt.Rows[0]["COSTCENTRECODE"].ToString().Trim();
                    this.txtcostcentrename.Text = dt.Rows[0]["COSTCENTRENAME"].ToString().Trim();
                    this.txtcostcentreDescription.Text = dt.Rows[0]["COSTCENTREDESCRIPTION"].ToString().Trim();
                    if (dt.Rows[0]["ACTIVE"].ToString().Trim() == "Active")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    pnlAdd.Style["display"] = "";
                    pnlDisplay.Style["display"] = "none";
                    btnaddhide.Style["display"] = "none";
                }
                else
                {
                    pnlAdd.Style["display"] = "none";
                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                }

            }
            else
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
            }

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void grdcostcentre_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;
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

    #region grdcostcentre_Exporting
    protected void grdcostcentre_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 4;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "Cost Centre Details";
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        cell.BackColor = Color.Gray;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region grdcostcentre_Exported
    protected void grdcostcentre_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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