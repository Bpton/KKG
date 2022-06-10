using BAL;
using Obout.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmRoles : System.Web.UI.Page
{
    ClsRoles ClsUTMaster = new ClsRoles();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadUTMaster();
                LoadReport();
                LoadRoles();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LoadUTMaster()
    {
        try
        {
            gvUserType.DataSource = ClsUTMaster.BindUserTypeMasterGrid();
            gvUserType.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadReport()
    {


        ClsRoles ClsUTMaster = new ClsRoles();
        ddlReportto.Items.Clear();
        //ddlReportto.Items.Insert(0, new ListItem("Select Reporting To", "0"));
        //ddlReportto.AppendDataBoundItems = true;
        ddlReportto.DataSource = ClsUTMaster.BindReportto();
        ddlReportto.DataTextField = "UTNAME";
        ddlReportto.DataValueField = "UTID";
        ddlReportto.DataBind();

    }

    protected void btnAddUserType_Click(object sender, EventArgs e)
    {
        try
        {
            ddlSimilaras.SelectedValue = "0";
            txtUTCode.Text = "";
            txtUTName.Text = "";
            chkActive.Checked = true;
            txtDescription.Text = "";
            LoadReport();
            ddlApplicableto.SelectedValue = "C";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            Similaras.Visible = true;
            LoadRoles();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region LoadRoles
    public void LoadRoles()
    {
        ClsRoles ClsUTMaster = new ClsRoles();
        ddlSimilaras.Items.Clear();
        ddlSimilaras.Items.Add(new ListItem("Select", "0"));
        ddlSimilaras.DataSource = ClsUTMaster.Bindroles();
        ddlSimilaras.DataTextField = "UTNAME";
        ddlSimilaras.DataValueField = "UTID";
        ddlSimilaras.DataBind();

    }
    #endregion
    protected void btnUTSubmit_Click(object sender, EventArgs e)
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
            string applicableDistribution = string.Empty;
            string applicablepredefine = string.Empty;
            if (ddlApplicableto.SelectedValue == "D")
            {
                applicablepredefine = "N";
                applicableDistribution = "Y";
            }
            else
            {
                applicablepredefine = "Y";
                applicableDistribution = "N";

            }

            string ReportToID = "";
            string ReportToName = "";
            // LINQ over Items collection (must cast Items)
            var query = from ListItem item in ddlReportto.Items where item.Selected select item;
            foreach (ListItem item in query)
            {
                // item ...
                ReportToID += item.Value + ",";
                ReportToName += item.Text + ",";
            }

            ReportToID = ReportToID.Substring(0, ReportToID.Length - 1);
            ReportToName = ReportToName.Substring(0, ReportToName.Length - 1);

            ID = ClsUTMaster.SaveUserTypeMaster(Hdn_Fld.Value, txtUTCode.Text.ToString(), txtUTName.Text.ToString(), txtDescription.Text.ToString(), Mode, chkActive.Checked.ToString(), ReportToID.ToString(), ReportToName, applicablepredefine, applicableDistribution, ddlApplicableto.SelectedValue, this.ddlSimilaras.SelectedValue.Trim());

            if (ID == 1)
            {
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                LoadUTMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 2)
            {
                //string message = "alert('Code already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Code already exist..");
            }
            else if (ID == 3)
            {
                // string message = "alert('Name already exist..')";
                // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Name already exist..");
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btnUTCancel_Click(object sender, EventArgs e)
    {
        try
        {

            txtUTCode.Text = "";
            txtUTName.Text = "";
            txtDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            ddlReportto.SelectedValue = "-1";
            LoadUTMaster();
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
            if (e.Record["UTID"] != "")
            {
                int ID = 0;
                ID = ClsUTMaster.DeleteUserTypeMaster(e.Record["UTID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadUTMaster();
                }
                else
                {
                    e.Record["Error"] = "Sorry, you can not delete this role!";
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

            string userid = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClsUTMaster.BindUserTypeMasterGridByID(userid);
            if (dt.Rows.Count > 0)
            {
                txtUTName.Text = dt.Rows[0]["UTNAME"].ToString();
                txtDescription.Text = dt.Rows[0]["UTDESCRIPTION"].ToString();
                txtUTCode.Text = dt.Rows[0]["UTCODE"].ToString();
                ddlApplicableto.SelectedValue = dt.Rows[0]["APPLICABLETO"].ToString().Trim();
                var myList = new List<string>(dt.Rows[0]["PARENTID"].ToString().Split(','));
                ddlReportto.Items.Clear();
                LoadReport();
                if (dt.Rows.Count > 0)
                {
                    for (int counter = 0; counter < myList.Count; counter++)
                    {
                        for (int innercounter = 0; innercounter < ddlReportto.Items.Count; innercounter++)
                        {
                            if (myList[counter] == ddlReportto.Items[innercounter].Value)
                            {
                                ddlReportto.Items[innercounter].Selected = true;

                            }


                        }
                    }


                }

                if (dt.Rows[0]["ACTIVE"].ToString() == "Active")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }

            }
            this.LoadUTMaster();
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            Similaras.Visible = false;
            btnaddhide.Style["display"] = "none";
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }


    protected void gvUserType_RowDataBound(object sender, GridRowEventArgs e)
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
}