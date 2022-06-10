using BAL;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmCityMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";

                LoadState();
                ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
                if (Request.QueryString["MENUID"] == "24")
                {
                    LoadCityMaster();
                    this.Excelid.Style["display"] = "none";
                }
                else
                {
                    LoadCityMaster();
                    this.Excelid.Style["display"] = "";
                    this.btnaddhide.Style["display"] = "none";
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadState()
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            ddlState1.Items.Clear();
            ddlState1.Items.Insert(0, new ListItem("Select", "0"));
            ddlState1.DataSource = ClsCityMaster.BindState();
            ddlState1.DataTextField = "State_Name";
            ddlState1.DataValueField = "State_ID";
            ddlState1.DataBind();

            ddlsearchstate.Items.Clear();
            ddlsearchstate.Items.Insert(0, new ListItem("ALL", "0"));
            ddlsearchstate.DataSource = ClsCityMaster.BindState();
            ddlsearchstate.DataTextField = "State_Name";
            ddlsearchstate.DataValueField = "State_ID";
            ddlsearchstate.DataBind();

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadDistrict(int stateid)
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ClsCityMaster.BindDistrict(stateid);
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCityMaster()
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            if (Request.QueryString["MENUID"] == "24")
            {
                gvCity.DataSource = ClsCityMaster.BindCityGrid();
                gvCity.DataBind();
            }
            else
            {
                gvCity.DataSource = ClsCityMaster.BindCityGrid();
                gvCity.Columns[4].Visible = false;
                gvCity.Columns[5].Visible = false;
                gvCity.DataBind();

            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnAddCity_Click(object sender, EventArgs e)
    {
        try
        {
            ddlState1.SelectedValue = "0";
            ddlDistrict.SelectedValue = "0";
            txtcity.Text = "";
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

    protected void btnSaveCity_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
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

            ID = ClsCityMaster.SaveCityMaster(Hdn_Fld.Value, Convert.ToInt32(ddlState1.SelectedValue), Convert.ToInt32(ddlDistrict.SelectedValue), txtcity.Text.ToString(), Mode);

            if (ID == 1)
            {

                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                ddlState1.SelectedValue = "0";
                ddlDistrict.SelectedValue = "0";
                txtcity.Text = "";

                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                LoadCityMaster();
            }
            else if (ID == 2)
            {
                //string message = "alert('City already exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowError("City already exist..");

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnCancelCity_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDistrict.SelectedValue = "0";
            txtcity.Text = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadCityMaster();
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
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            string cityid = Hdn_Fld.Value.ToString();
            DataTable dt = new DataTable();
            dt = ClsCityMaster.Bindcityinformation(Convert.ToInt32(cityid));

            ddlState1.SelectedValue = dt.Rows[0]["State_ID"].ToString();

            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ClsCityMaster.BindDistrict(Convert.ToInt32(ddlState1.SelectedValue));
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();

            ddlDistrict.SelectedValue = dt.Rows[0]["District_ID"].ToString();
            txtcity.Text = dt.Rows[0]["City_Name"].ToString();

            Hdn_Fld.Value = Convert.ToString(cityid);
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

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
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            if (e.Record["City_ID"] != "")
            {
                int ID = 0;
                ID = ClsCityMaster.DeleteCityMaster(e.Record["City_ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadCityMaster();
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

    protected void ddlState1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select", "0"));
            ddlDistrict.DataSource = ClsCityMaster.BindDistrict(Convert.ToInt32(ddlState1.SelectedValue));
            ddlDistrict.DataTextField = "District_Name";
            ddlDistrict.DataValueField = "District_ID";
            ddlDistrict.DataBind();

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void ddlsearchstate_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            DataTable dt = ClsCityMaster.BindDistrict(Convert.ToInt32(this.ddlsearchstate.SelectedValue));
            ddlsearchdistrict.Items.Clear();
            ddlsearchdistrict.Items.Insert(0, new ListItem("ALL", "0"));
            ddlsearchdistrict.DataSource = dt;
            ddlsearchdistrict.DataTextField = "District_Name";
            ddlsearchdistrict.DataValueField = "District_ID";
            ddlsearchdistrict.DataBind();
            if (this.ddlsearchstate.SelectedValue == "0" && this.ddlsearchdistrict.SelectedValue == "0")
            {
                this.gvCity.DataSource = ClsCityMaster.BindCityGrid();
                this.gvCity.DataBind();
            }
            else
            {
                this.gvCity.DataSource = ClsCityMaster.BindSateGrid(Convert.ToInt32(this.ddlsearchstate.SelectedValue.Trim()));
                this.gvCity.DataBind();
            }


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ddlsearchdistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ClsCityMaster ClsCityMaster = new ClsCityMaster();
            // DataTable dt = ClsCityMaster.BindCityGrid(Convert.ToInt32(ddlsearchstate.SelectedValue.Trim()), Convert.ToInt32(ddlsearchdistrict.SelectedValue.Trim()));
            if (this.ddlsearchstate.SelectedValue == "0" && this.ddlsearchdistrict.SelectedValue == "0")
            {
                this.gvCity.DataSource = ClsCityMaster.BindCityGrid();
                this.gvCity.DataBind();
            }
            else if (this.ddlsearchstate.SelectedValue != "0" && this.ddlsearchdistrict.SelectedValue == "0")
            {
                this.gvCity.DataSource = ClsCityMaster.BindSateGrid(Convert.ToInt32(this.ddlsearchstate.SelectedValue.Trim()));
                this.gvCity.DataBind();

            }

            else
            {
                this.gvCity.DataSource = ClsCityMaster.BindCityGrid(Convert.ToInt32(this.ddlsearchstate.SelectedValue.Trim()), Convert.ToInt32(this.ddlsearchdistrict.SelectedValue.Trim()));
                this.gvCity.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region gvCity_Exporting
    protected void gvCity_Exporting(object sender, Obout.Grid.GridExportEventArgs e)
    {
        TableRow row = new TableRow();
        TableCell cell = new TableCell();

        cell.ColumnSpan = 3;
        cell.BorderStyle = BorderStyle.None;

        cell.Text = "City Details";
        //TableHeaderRow row1 = new TableHeaderRow();
        //row1.Cells[1].Visible = false;
        cell.HorizontalAlign = HorizontalAlign.Center;
        cell.Font.Bold = true;
        cell.BackColor = Color.Yellow;
        e.Table.GridLines = GridLines.None;
        row.Cells.Add(cell);
        e.Table.Rows.Add(row);
    }
    #endregion

    #region gvCity_Exported
    protected void gvCity_Exported(object sender, Obout.Grid.GridExportEventArgs e)
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