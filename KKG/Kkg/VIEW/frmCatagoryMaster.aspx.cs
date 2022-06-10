using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

public partial class VIEW_frmCatagoryMaster : System.Web.UI.Page
{
    ClsCatagoryMaster ClscatMaster = new ClsCatagoryMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadCatagoryMaster();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCatagoryMaster()
    {
        try
        {
            this.gvCATAGORY.DataSource = ClscatMaster.BindCatagoryMastergrid();
            this.gvCATAGORY.DataBind();
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
            this.txtcatcode.Text = "";
            this.txtcatname.Text = "";
            this.txtDescription.Text = "";
            this.chkActive.Checked = true;
            this.Hdn_Fld.Value = "";
            this.txthsv.Text = "";
            this.txtipsc.Text = "";
            this.pnlAdd.Style["display"] = "";
            this.pnlDisplay.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "none";
            this.txthsncodedescript.Text = "";
            btnCATsubmit.Enabled = true;

            this.txtcatcode.Enabled = true;
            this.txtcatname.Enabled = true;
            this.txtDescription.Enabled = true;
            this.chkActive.Enabled = true;
            txtipsc.Enabled = true;
            txthsv.Enabled = true;
            this.txthsncodedescript.Enabled = true;
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnCATsubmit_Click(object sender, EventArgs e)
    {
        try
        {
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

            ID = ClscatMaster.SaveCatagoryMaster(Hdn_Fld.Value, txtcatcode.Text.ToString(), txtcatname.Text.ToString(), txtDescription.Text.ToString(), txtipsc.Text.Trim(), txthsv.Text.Trim(), Mode, chkActive.Checked.ToString(), this.txthsncodedescript.Text.Trim());

            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                this.LoadCatagoryMaster();
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.btnaddhide.Style["display"] = "";
                this.Hdn_Fld.Value = "";
                this.txthsv.Text = "";
                this.txtipsc.Text = "";
                this.txthsncodedescript.Text = "";
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
            string msg = ex.Message;
        }
    }
    protected void btnCATcancel_click(object sender, EventArgs e)
    {
        try
        {
            this.txtcatcode.Text = "";
            this.txtcatname.Text = "";
            this.txtDescription.Text = "";
            this.Hdn_Fld.Value = "";
            this.txthsv.Text = "";
            this.txtipsc.Text = "";
            this.pnlAdd.Style["display"] = "none";
            this.pnlDisplay.Style["display"] = "";
            this.btnaddhide.Style["display"] = "";
            this.LoadCatagoryMaster();
            this.txthsncodedescript.Text = "";
            btnCATsubmit.Enabled = true;

            this.txtcatcode.Enabled = true;
            this.txtcatname.Enabled = true;
            this.txtDescription.Enabled = true;
            this.chkActive.Enabled = true;
            txtipsc.Enabled = true;
            txthsv.Enabled = true;
            this.txthsncodedescript.Enabled = true;
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
            if (e.Record["CATID"] != "")
            {
                int ID = 0;
                ID = ClscatMaster.DeleteCatagoryMaster(e.Record["CATID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    this.LoadCatagoryMaster();
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

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        ClsCatagoryMaster ClscatMaster = new ClsCatagoryMaster();
        try
        {
            btnCATsubmit.Enabled = true;
            this.txtcatcode.Enabled = false;
            this.txtcatname.Enabled = false;
            this.txtDescription.Enabled = true;
            this.chkActive.Enabled = true;
            txtipsc.Enabled = true;
            txthsv.Enabled = true;
            this.txthsncodedescript.Enabled = true;

            if (Hdn_Fld.Value != "")
            {
                string id = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();
                dt = ClscatMaster.BindCatagoryMastergrideditbyid(id);
                if (dt.Rows.Count > 0)
                {
                    this.txtcatcode.Text = dt.Rows[0]["CATCODE"].ToString().Trim();
                    this.txtcatname.Text = dt.Rows[0]["CATNAME"].ToString().Trim();
                    this.txtDescription.Text = dt.Rows[0]["CATDESCRIPTION"].ToString().Trim();
                    this.txtipsc.Text = dt.Rows[0]["IPSC"].ToString().Trim();
                    this.txthsv.Text = dt.Rows[0]["HSN"].ToString().Trim();
                    if (dt.Rows[0]["ACTIVE"].ToString().Trim() == "Active")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
                    this.txthsncodedescript.Text = dt.Rows[0]["HSNCODEDESCRIPTION"].ToString().Trim();
                    this.pnlAdd.Style["display"] = "";
                    this.pnlDisplay.Style["display"] = "none";
                    this.btnaddhide.Style["display"] = "none";
                    this.LoadCatagoryMaster();
                }
                else
                {
                    this.pnlAdd.Style["display"] = "none";
                    this.pnlDisplay.Style["display"] = "";
                    this.btnaddhide.Style["display"] = "";
                    this.LoadCatagoryMaster();
                }

            }
            else
            {
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.btnaddhide.Style["display"] = "";
                this.LoadCatagoryMaster();
            }

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void gvCATAGORY_RowDataBound(object sender, GridRowEventArgs e)
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
}