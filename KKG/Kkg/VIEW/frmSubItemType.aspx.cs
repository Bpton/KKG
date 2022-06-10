using BAL;
using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmSubItemType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                this.LoadPrimaryItemType();
                this.LoadSubItemType();
                //this.LoadBranch();
                this.LoadDepotName();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadSubItemType()
    {
        try
        {
            ClsItemType clsItemType = new ClsItemType();
            gvDivision.DataSource = clsItemType.BindSubItemType();
            gvDivision.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void LoadPrimaryItemType()
    {
        ClsItemType clsItemType = new ClsItemType();
        DataTable dt = new DataTable();
        dt = clsItemType.LoadPrimaryItemType();
        if (dt.Rows.Count > 0)
        {
            this.ddlPrimaryItemType.Items.Clear();
            this.ddlPrimaryItemType.Items.Add(new ListItem("Select Item Type", "0"));
            this.ddlPrimaryItemType.AppendDataBoundItems = true;
            this.ddlPrimaryItemType.DataSource = dt;
            this.ddlPrimaryItemType.DataValueField = "ID";
            this.ddlPrimaryItemType.DataTextField = "ITEMDESC";
            this.ddlPrimaryItemType.DataBind();
        }
        else
        {
            this.ddlPrimaryItemType.Items.Clear();
            this.ddlPrimaryItemType.Items.Add(new ListItem("Select Item Type", "0"));
            this.ddlPrimaryItemType.AppendDataBoundItems = true;
        }
    }

    protected void btnAddDivision_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtItemTypeName.Text = "";
            this.txtTypeCode.Text = "";
            this.chkActive.Checked = true;
            this.txtDescription.Text = "";
            this.txthsn.Text = "";
            this.ddlPrimaryItemType.SelectedValue = "0";
            this.Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";

            txtItemTypeName.Enabled = true;
            txtDescription.Enabled = true;
            chkActive.Enabled = true;
            ddlPrimaryItemType.Enabled = true;
            txthsn.Enabled = true;
            ddlfacmap.Enabled = true;
            btnDIVSubmit.Enabled = true;
            ddlitemowner.Enabled = true;
            this.txtTypeCode.Enabled = true;

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnDIVSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsItemType clsItemType = new ClsItemType();
            DataTable dtSubCode = new DataTable();
            if (Hdn_Fld.Value == "")
            {
                ViewState["mode"] = "A";
                //dtSubCode = clsItemType.GenerateSubItemCode();
                //this.txtTypeCode.Text = dtSubCode.Rows[0]["CODE"].ToString().Trim();
            }
            else
            {
                ViewState["mode"] = "U";
            }
            string Mode = ViewState["mode"].ToString();
            int ID = 0;
            string Active = string.Empty;
            if (this.chkActive.Checked == true)
            {
                Active = "Y";
            }
            else
            {
                Active = "N";
            }
            ID = clsItemType.SaveSubItemTypeMaster(this.Hdn_Fld.Value, Convert.ToInt32(this.ddlPrimaryItemType.SelectedValue.Trim()), txtTypeCode.Text.Trim(), txtItemTypeName.Text.ToString(), txtDescription.Text.ToString(), Mode, Active, txthsn.Text.ToString(), ddlitemowner.SelectedValue.ToString(), ddlfacmap.SelectedValue.ToString());
            if (ID == 1)
            {
                MessageBox1.ShowSuccess("</b> Record Saved Successfully!");
                this.LoadSubItemType();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 3)
            {
                MessageBox1.ShowInfo("</b> Name already exist..");
            }
            else if (ID == 4)
            {
                MessageBox1.ShowInfo("</b> Code already exist..");
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btnDIVCancel_Click(object sender, EventArgs e)
    {
        try
        {
            this.txtItemTypeName.Text = "";
            this.txtTypeCode.Text = "";
            this.txtDescription.Text = "";
            this.Hdn_Fld.Value = "";
            this.txthsn.Text = "";
            this.ddlPrimaryItemType.SelectedValue = "0";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadSubItemType();

            txtItemTypeName.Enabled = true;
            txtDescription.Enabled = true;
            chkActive.Enabled = true;
            ddlPrimaryItemType.Enabled = true;
            txthsn.Enabled = true;
            ddlfacmap.Enabled = true;

            btnDIVSubmit.Enabled = true;
            ddlitemowner.Enabled = true;


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
            if (e.Record["SUBTYPEID"] != "")
            {
                ClsItemType clsItemType = new ClsItemType();
                int ID = 0;
                ID = clsItemType.DeleteSubItemType(e.Record["SUBTYPEID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";

                    LoadPrimaryItemType();
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
    protected void gvDivision_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[8] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "ACTIVE")
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

    protected void btngrdedit_Click(object sender, EventArgs e)
    {
        try
        {

            pnlDisplay.Style["display"] = "none";
            pnlAdd.Style["display"] = "";
            btnaddhide.Style["display"] = "none";
            this.txtTypeCode.Enabled = false;
            string ID = Convert.ToString(Hdn_Fld.Value).Trim();
            ClsItemType clsItemType = new ClsItemType();
            DataTable dt = new DataTable();
            dt = clsItemType.EditSubItemType(ID);
            if (dt.Rows.Count > 0)
            {
                this.LoadPrimaryItemType();
                this.ddlPrimaryItemType.SelectedValue = Convert.ToString(dt.Rows[0]["PRIMARYITEMTYPEID"]).Trim();

                this.txtTypeCode.Text = Convert.ToString(dt.Rows[0]["SUBITEMCODE"]).Trim();
                this.txtItemTypeName.Text = Convert.ToString(dt.Rows[0]["SUBITEMNAME"]).Trim();
                this.txtDescription.Text = Convert.ToString(dt.Rows[0]["SUBITEMDESC"]).Trim();
                this.txthsn.Text = Convert.ToString(dt.Rows[0]["HSE"]).Trim();
                if (Convert.ToString(dt.Rows[0]["ACTIVE"]).Trim() == "Y")
                {
                    this.chkActive.Checked = true;
                }
                else
                {
                    this.chkActive.Checked = false;
                }
                //this.LoadBranch();
                LoadDepotName();
                ListItem item = ddlfacmap.Items.FindByText(dt.Rows[0]["BRANCHID"].ToString().Trim());
                if (item != null)
                {
                    this.ddlfacmap.SelectedValue = Convert.ToString(dt.Rows[0]["BRANCHID"]).Trim();
                }

                checkSubItemTypeUsed(ID);
                //txtItemTypeName.Enabled = false;
                //txtDescription.Enabled = false;
                //chkActive.Enabled = false;
                //ddlPrimaryItemType.Enabled = false;
                //txthsn.Enabled = false;
                //ddlfacmap.Enabled = false;
                //ddlitemowner.Enabled = false;
                //btnDIVSubmit.Enabled = false;
                LOADITEMWNER();
                this.ddlitemowner.SelectedValue = Convert.ToString(dt.Rows[0]["ITEMOWNER"]).Trim();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    public void LOADITEMWNER()
    {
        ClsItemType Obj = new ClsItemType();
        DataTable dt = Obj.Itemwnoerload();
        ddlitemowner.Items.Clear();
        ddlitemowner.AppendDataBoundItems = true;
        ddlitemowner.DataSource = dt;
        ddlitemowner.DataTextField = "ITEMOWNERNAME";
        ddlitemowner.DataValueField = "ITEMOWNER";
        ddlitemowner.DataBind();

    }
    #region LoadDepotName
    public void LoadDepotName()
    {
        try
        {
            ClsItemType Obj = new ClsItemType();
            if (Session["DEPOTID"].ToString() != "15E687A6-CD85-412A-ABD4-B52AB91CADE0" && Session["DEPOTID"].ToString() != "14857CFC-2450-4D52-B93A-486D9507A1BE")
            {
                ddlfacmap.Items.Clear();
                ddlfacmap.Items.Add(new ListItem("None", "0"));
            }
            else
            {
                DataTable dt = Obj.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                ddlfacmap.Items.Clear();
                ddlfacmap.AppendDataBoundItems = true;
                ddlfacmap.DataSource = dt;
                ddlfacmap.DataTextField = "BRNAME";
                ddlfacmap.DataValueField = "BRID";
                ddlfacmap.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion   

    public void checkSubItemTypeUsed(string SubItemTypeID)
    {
        ClsItemType clsItemType = new ClsItemType();
        DataTable dt = new DataTable();
        dt = clsItemType.Check_Product_Use_in_Transaction(SubItemTypeID);
        if (dt.Rows.Count > 0)
        {
            
            if (this.ddlPrimaryItemType.SelectedValue.Trim() != "0")
            {
                ddlPrimaryItemType.Enabled = false;
            }
            txtItemTypeName.Enabled = false;
            txtDescription.Enabled = false;
            txthsn.Enabled = true;/* as per discussion with mr. bakula on 15/09/2021 hsn Code should change 4 to 8 degit */
            chkActive.Enabled = false;
            ddlfacmap.Enabled = false;
            //txthsn.Enabled = false;
            ddlitemowner.Enabled = false;
            ddlitemowner.Enabled = false;
            //btnDIVSubmit.Enabled = false;
            txtTypeCode.Enabled = false;
        }

     
        else
        {
            
            txtItemTypeName.Enabled = true;
            txtDescription.Enabled = true;
            chkActive.Enabled = true;
            txthsn.Enabled = true;
            ddlfacmap.Enabled = false;
            ddlPrimaryItemType.Enabled = true;
            txtTypeCode.Enabled = true;
        }
        
         


    }
}