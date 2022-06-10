using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Account;
using System.Data;
using Obout.Grid;
using System.Drawing;

public partial class VIEW_frmBackDateSettings : System.Web.UI.Page
{

    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldepot').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlparty').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlstatus').multiselect({ includeSelectAllOption: true  });$('#ContentPlaceHolder1_ddlbsname').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                LoadListOfDateSettings();
                LoadFinYear();
                LoadDepot();
                LoadUser();
               
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

            ClsRptAccount clstxmaster = new ClsRptAccount();
            dt = clstxmaster.LoadListOfDateSettings(Convert.ToString(Session["FINYEAR"]).Trim());
            if (dt.Rows.Count > 0)
            {
                gvCity.DataSource = dt;
                gvCity.DataBind();
            }
            else
            {
                gvCity.DataSource = dt; 
                gvCity.DataBind();
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
            this.LoadDepot();
            this.pnlDisplay.Style["display"] = "none";
            this.pnlAdd.Style["display"] = "";
            this.btnaddhide.Style["display"] = "none";
            this.ddldepot.SelectedValue = "0";
            this.ddlUser.SelectedValue = "0";
            Hdn_Fld.Value = "";

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region btnSaveCity_Click
    protected void btnSaveCity_Click(object sender, EventArgs e)
    {
        try
        {                      
      

            ClsRptAccount clstxmaster = new ClsRptAccount();

            int ID = 0;
          
            ID = clstxmaster.SaveDateSetting(txtFromDate.Text, txtToDate.Text,txtAllowDate.Text, ddlUser.SelectedValue.Trim(),ddldepot.SelectedValue.Trim(),ddlYear.SelectedValue.Trim());

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
                MessageBox1.ShowInfo("Already exists");
            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #endregion

    #region btnCancelCity_Click
    protected void btnCancelCity_Click(object sender, EventArgs e)
    {
        try
        {   
            //pnlAdd.Style["display"] = "none";
            //pnlDisplay.Style["display"] = "";
            //btnaddhide.Style["display"] = "";

            this.pnlDisplay.Style["display"] = "";
            this.pnlAdd.Style["display"] = "none";
            this.btnaddhide.Style["display"] = "";
            this.Hdn_Fld.Value = "";
            this.ddldepot.SelectedValue = "";
            this.ddlUser.SelectedValue = "";
            LoadListOfDateSettings();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadYear
    public void LoadFinYear()
    {
        try
        {
            ClsRptAccount clsreport = new ClsRptAccount();
            DataTable dt = clsreport.BindFinYear(Convert.ToString(Session["FINYEAR"]).Trim());
            ddlYear.Items.Clear();
            ddlYear.AppendDataBoundItems = true;
            ddlYear.DataSource = dt;
            ddlYear.DataTextField = "FINYEAR";
            ddlYear.DataValueField = "FINYEAR";
            ddlYear.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region LoadDepot
    public void LoadDepot()
    {
        try
        {
            ClsRptAccount clsreport = new ClsRptAccount();
            DataTable dt = clsreport.BindDepotForAgingReport();
            this.ddldepot.Items.Clear();
            this.ddldepot.Items.Add(new ListItem("All Depot", "0"));
            this.ddldepot.AppendDataBoundItems = true;
            this.ddldepot.DataSource = dt;
            this.ddldepot.DataTextField = "BRNAME";
            this.ddldepot.DataValueField = "BRID";
            this.ddldepot.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region LoadUser
    public void LoadUser()
    {
        try
        {
            ClsRptAccount clsreport = new ClsRptAccount();
            DataTable dt = clsreport.BindAccountUser();
            this.ddlUser.Items.Clear();
            this.ddlUser.Items.Add(new ListItem("All User", "0"));
            this.ddlUser.AppendDataBoundItems = true;
            this.ddlUser.DataSource = dt;
            this.ddlUser.DataTextField = "USERNAME";
            this.ddlUser.DataValueField = "USERID";
            this.ddlUser.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region ddldepot_OnSelectedIndexChanged

    protected void ddldepot_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            ClsRptAccount clsreport = new ClsRptAccount();
            DataTable dt = new DataTable();
            dt = clsreport.BindDEPOTBYUSER(ddldepot.SelectedValue);
            this.ddlUser.Items.Clear();
            this.ddlUser.Items.Add(new ListItem("Select", "0"));
            this.ddlUser.AppendDataBoundItems = true;
            this.ddlUser.DataSource = dt;
            this.ddlUser.DataTextField = "USERNAME";
            this.ddlUser.DataValueField = "USERID";
            this.ddlUser.DataBind();

        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }


    }

    #endregion

    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ClsRptAccount clstxmaster = new ClsRptAccount();           
            if (e.Record["ID"].ToString() != "")
            {
                int ID = 0;
                ID = clstxmaster.DeletDateSetting(e.Record["ID"].ToString());
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