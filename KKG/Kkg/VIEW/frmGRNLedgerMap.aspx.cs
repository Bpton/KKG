 using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Drawing;
using System.Data;


public partial class VIEW_frmGRNLedgerMap : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ////Session["UserID"] = "1";
                this.pnlAdd.Style["display"] = "none";
                this.pnlDisplay.Style["display"] = "";
                this.Loadgrid();
                this.LoadLedger();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void Loadgrid()
    {
        try
        {
            clsGRN clsgrn = new clsGRN();
            gvDetails.DataSource = clsgrn.BindMainGrid();
            gvDetails.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadLedger()
    {
        clsGRN clsgrn = new clsGRN();
        DataTable dt = new DataTable();
        dt = clsgrn.BindLedger();
        if (dt.Rows.Count > 0)
        {
            this.ddlledger.Items.Clear();
            this.ddlledger.Items.Add(new ListItem("Select", "0"));
            this.ddlledger.DataSource = dt;
            this.ddlledger.DataTextField = "NAME";
            this.ddlledger.DataValueField = "ID";
            this.ddlledger.DataBind();
        }
        else
        {
            this.ddlledger.Items.Clear();
        }
    }

    protected void btnAddBank_Click(object sender, EventArgs e)
    {
        try
        {
           
            this.txtName.Text = "";
            this.txtpercent.Text = "";
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            clsGRN clsgrn = new clsGRN();

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

            ID = clsgrn.Save(Hdn_Fld.Value, txtName.Text.ToString(), Mode, Convert.ToDecimal(txtpercent.Text), this.ddlledger.SelectedValue.Trim(), HttpContext.Current.Session["USERID"].ToString().Trim(), HttpContext.Current.Session["USERID"].ToString().Trim(), "", "");

            if (ID == 1)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully..");
                Loadgrid();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 2)
            {
                MessageBox1.ShowWarning("Code already exist..");

            }
            else if (ID == 3)
            {
                MessageBox1.ShowWarning("Name already exist..");
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
          
            Hdn_Fld.Value = "";
            this.txtName.Text = "";
            this.txtpercent.Text = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            Loadgrid();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        try
        {
            clsGRN clsgrn = new clsGRN();
            DataTable dt = clsgrn.BindGridEdit(Hdn_Fld.Value);

            if(dt.Rows.Count>0)
            {
                this.txtName.Text = Convert.ToString(dt.Rows[0]["DETAILSNAME"]).Trim();
                this.txtpercent.Text = Convert.ToString(dt.Rows[0]["PERCENTAGE"]).Trim();
                this.LoadLedger();
                this.ddlledger.Text = Convert.ToString(dt.Rows[0]["REFERENCELEDGERID"]).Trim();
            }
            else
            {
                this.txtName.Text = "";
                this.txtpercent.Text = "";
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



    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            clsGRN clsgrn = new clsGRN();

            if (e.Record["DETAILSID"] != "")
            {
                int ID = 0;
                ID = clsgrn.Delete(e.Record["DETAILSID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";

                    Loadgrid();
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
  
}