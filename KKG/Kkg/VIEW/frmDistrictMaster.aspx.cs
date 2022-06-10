using BAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmDistrictMaster : System.Web.UI.Page
{
    ClsDistrictMaster clsdistrict = new ClsDistrictMaster();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.LoadState();
                this.LoadDistrictgrid();

            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadDistrictgrid()
    {
        DataTable dt = clsdistrict.BindDistrictGrid();
        if (dt.Rows.Count > 0)
        {
            gvDistrict.DataSource = dt;
            gvDistrict.DataBind();
        }
        else
        {
            gvDistrict.DataSource = null;
            gvDistrict.DataBind();
        }
    }

    public void LoadState()
    {

        ddlState.Items.Clear();
        ddlState.Items.Insert(0, new ListItem("--Select Sate--", "0"));
        ddlState.DataSource = clsdistrict.BindState();
        ddlState.DataTextField = "State_Name";
        ddlState.DataValueField = "State_ID";
        ddlState.DataBind();



    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            ddlState.SelectedValue = "0";

            txtdistrict.Text = "";
            Hdn_Fld.Value = "";
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
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

            ID = clsdistrict.SaveDistrictMaster(Hdn_Fld.Value, Convert.ToInt32(ddlState.SelectedValue), txtdistrict.Text.Trim(), Mode);


            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");


                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                LoadDistrictgrid();
            }
            else if (ID == 2)
            {
                //string message = "alert('District Name already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("District already exist..");

            }
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
            ddlState.SelectedValue = "0";
            txtdistrict.Text = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";

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
            if (Hdn_Fld.Value != "")
            {
                string districtid = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();

                dt = clsdistrict.Grideditbyid(Convert.ToInt32(districtid));
                if (dt.Rows.Count > 0)
                {
                    this.LoadState();
                    ddlState.SelectedValue = dt.Rows[0]["State_ID"].ToString();

                    txtdistrict.Text = dt.Rows[0]["District_Name"].ToString();

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
                MessageBox1.ShowInfo("Record is not save properly");
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
            if (e.Record["DISTRICT_ID"] != "")
            {
                int ID = 0;
                ID = clsdistrict.DeleteDistrictMaster(e.Record["DISTRICT_ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadDistrictgrid();
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
}