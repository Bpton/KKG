
using BAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class FACTORY_frmBackDateSettings_FAC : System.Web.UI.Page
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
                txtvoucherdate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                LoadListOfDateSettings();
                LoadListOfMenu();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    public void LoadListOfMenu()
    {
        try
        {
            
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            dt = ClsCommon.LoadMenu();
            if (dt.Rows.Count > 0)
            {
                this.ddlMenu.Items.Add(new ListItem("Select Menu", "0"));
                this.ddlMenu.DataSource = dt;
                this.ddlMenu.DataValueField = "ID";
                this.ddlMenu.DataTextField = "MENUNAME";
                this.ddlMenu.DataBind();
                //this.ddlMenu.SelectedValue = HttpContext.Current.Session["DEPOTID"].ToString();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadListOfDateSettings()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            dt = ClsCommon.LoadListOfDateSettings();
            if (dt.Rows.Count > 0)
            {
                GvBackDt.DataSource = dt;
                GvBackDt.DataBind();
            }
            else
            {
                GvBackDt.DataSource = dt;
                GvBackDt.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btnAddSettings_Click(object sender, EventArgs e)
    {
        try
        {
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            int ID = 0;
            ID = ClsCommon.SaveDateSetting(ddlMenu.SelectedValue.ToString(), txtvoucherdate.Text);
            if (ID >= 1)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
                LoadListOfDateSettings();
            }
            else
            {
                MessageBox1.ShowSuccess("Record All Ready Exist...");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadListOfDateSettings();
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
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            if (e.Record["MENUID"].ToString() != "")
            {
                int ID = 0;
                ID = ClsCommon.DeletDateSetting(e.Record["MENUID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadListOfDateSettings();
                }
                else
                {
                    LoadListOfDateSettings();
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