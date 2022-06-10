using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmStoreLocation : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {

                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                Loadstorelocation();
            }  
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }




    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {

    }

    protected void btnAddnewRecord_Click(object sender, EventArgs e)
    {
        try
        {
            //this.LoadDepartment();
            // this.LoadDepartmentrole();
            pnlAdd.Style["display"] = "";
            
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            
            // this.divBtnShow.Style["display"] = "none";

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    public void Loadstorelocation()
    {
        try
        {
            string Mode = string.Empty;
            Mode = "S";
            ClsCityMaster clsobj = new ClsCityMaster();
            DataTable dtobj = new DataTable();
            dtobj = clsobj.loadstorelocation(Mode);
            if (dtobj.Rows.Count>0)
            {
                grvstorelocation.DataSource = dtobj;
                grvstorelocation.DataBind();
            }
            else
            {
                grvstorelocation.DataSource = null;
                grvstorelocation.DataBind();
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    } 

   

    protected void btngrddelete_Click(object sender, EventArgs e)
    {
        ClsCityMaster clsobj = new ClsCityMaster();
        DataTable dtobj = new DataTable();
        Button btnobj = (Button)sender;
        GridViewRow grvobj = (GridViewRow)btnobj.NamingContainer;
        Label lblid = (Label)grvobj.FindControl("lbltaskid");
        this.hdnstorelocation.Value = lblid.Text.Trim();


    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        try
        {
            string Mode = string.Empty;
            Mode = "I";
            ClsCityMaster clsobj = new ClsCityMaster();
            DataTable dtobj = new DataTable();
            if (this.txtstorelocation.Text == "")
            {
                MessageBox1.ShowError("Please Give A Store Location",40,550);
            }
            else
            {
                dtobj = clsobj.savestorelocation(Mode,this.txtstorelocation.Text.ToString());
                MessageBox1.ShowSuccess("Your Record Saved ", 40, 550);
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Loadstorelocation();
                return;

            }
            


        }
        catch
        {

        }

    }

    
    protected void btncancel_Click(object sender, EventArgs e)
    {
        this.txtstorelocation.Text = "";
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        btnaddhide.Style["display"] = "";
        Loadstorelocation();

    }
}