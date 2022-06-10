using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;

public partial class VIEW_frmDepartmentMaster : System.Web.UI.Page
{
    ClsTermsMaster clsterms = new ClsTermsMaster();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadDeparmentGrid();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadDeparmentGrid()
    {
        try
        {
            ClsDepartment clsdept = new ClsDepartment();
            DataTable dt = clsdept.Bindgrid();
            if (dt.Rows.Count > 0)
            {
                gvDepartment.DataSource = dt;
                gvDepartment.DataBind();
            }
            else
            {
                gvDepartment.DataSource = null;
                gvDepartment.DataBind();
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtcode.Text = "";
            this.txtName.Text = "";
            this.txtdescription.Text = "";
            this.chkApproved.Checked = true;
            this.Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

            this.txtcode.Text = "";
            this.txtName.Text = "";
            this.txtdescription.Text = "";
            this.chkApproved.Checked = true;
            this.Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadDeparmentGrid();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ClsDepartment clsdept = new ClsDepartment();
        try
        {
            string ISAPPROVE = string.Empty;
            string STATUS = string.Empty;
            string status = string.Empty;
            if (Hdn_Fld.Value == "")
            {
                STATUS = "A";
            }
            else
            {
                STATUS = "U";

            }
            if (chkApproved.Checked)
            {
                ISAPPROVE = "Y";
            }
            else
            {
                ISAPPROVE = "N";
            }

            string cbu = HttpContext.Current.Session["UserID"].ToString();

            int ID = 0;
            ID = clsdept.Savedepartment(Hdn_Fld.Value, Convert.ToString(this.txtcode.Text).Trim(), Convert.ToString(this.txtName.Text).Trim(),
                                        Convert.ToString(txtdescription.Text).Trim(), cbu.Trim(), STATUS.Trim(), ISAPPROVE.Trim());


            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadDeparmentGrid();
                this.txtcode.Text = "";
                this.txtName.Text = "";
                this.txtdescription.Text = "";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }

            else if (ID == 2)
            {
                //string message = "alert('Name already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowWarning("Code already exist..");

            }
            else if (ID == 3)
            {
                //string message = "alert('PageName already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowWarning("Name already exist..");

            }
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
            ClsDepartment clsdept = new ClsDepartment();
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = clsdept.DeleteDepartmentMaster(e.Record["DEPTID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadDeparmentGrid();
                }
                else
                {
                    e.Record["Error"] = "Delete not possible. ";
                }


            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsDepartment clsdept = new ClsDepartment();
            string id = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = clsdept.BindgridEdit(id);


            if (dt.Rows.Count > 0)
            {
                txtcode.Text = dt.Rows[0]["DEPTCODE"].ToString();
                txtName.Text = dt.Rows[0]["DEPTNAME"].ToString();
                txtdescription.Text = dt.Rows[0]["DEPTDESCRIPTION"].ToString();

                if (dt.Rows[0]["ISAPPROVE"].ToString() == "Y")
                {
                    chkApproved.Checked = true;
                }
                else
                {
                    chkApproved.Checked = false;
                }
            }
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void gvDepartment_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Y")
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
}