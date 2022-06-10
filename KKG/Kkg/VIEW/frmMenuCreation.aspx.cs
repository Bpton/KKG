using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Obout.Grid;

public partial class VIEW_frmMenuCreation : System.Web.UI.Page
{
    ClsMenuCreate clsMenu = new ClsMenuCreate(); 
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
         PageUrlUpload.Attributes.Add("onchange", "SetFileName('" + PageUrlUpload.ClientID + "','" + txtPageUrl.ClientID + "')");
           
            if (!IsPostBack)  
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                divadd.Style["display"] = "";
                LoadMenuMaster();
                LoadParentPageName();
                //LoadType();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadParentPageName()
    {
        ClsMenuCreate clsMenu = new ClsMenuCreate(); 
        try
        {
            DataTable dt = clsMenu.BindParentPageName();
            if (dt.Rows.Count > 0)
            {
                ddlParentPageName.Items.Clear();
                ddlParentPageName.Items.Insert(0, new ListItem("Select", "0"));
                ddlParentPageName.AppendDataBoundItems = true;
                ddlParentPageName.DataSource = dt;
                ddlParentPageName.DataTextField = "Pagename";
                ddlParentPageName.DataValueField = "ChildID";
                ddlParentPageName.DataBind();
            }
            else
            {
                ddlParentPageName.Items.Clear();
                ddlParentPageName.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
         catch (Exception ex)
         {
             string message = "alert('" + ex.Message.Replace("'", "") + "')";
         }
    }

    //public void LoadType()
    //{
    //    ClsMenuCreate clsMenu = new ClsMenuCreate(); 
    //    try
    //    {
    //        DataTable dt = clsMenu.BindType();
    //        if (dt.Rows.Count > 0)
    //        {
    //            ddltype.Items.Clear();
    //            ddltype.Items.Insert(0, new ListItem("A", "0"));
    //            ddltype.AppendDataBoundItems = true;
    //            ddltype.DataSource = dt;
    //            ddltype.DataTextField = "TAG";
    //            ddltype.DataValueField = "";
    //            ddltype.DataBind();
    //        }
    //        else
    //        {
    //            ddltype.Items.Clear();
    //            ddltype.Items.Insert(0, new ListItem("--Select Type--", "0"));
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
    //    }
    //}

    public void LoadMenuMaster()
    {
        try
        {
            ClsMenuCreate clsMenu = new ClsMenuCreate();
            DataTable dt = clsMenu.BindGridView();
            if (dt.Rows.Count > 0)
            {
                gvMenu.DataSource = clsMenu.BindGridView();
                gvMenu.DataBind();
            }
            else
            {
                gvMenu.ClearPreviousDataSource();
                gvMenu.DataSource = null;
                gvMenu.DataBind();
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
  
    private void ResetAll()
    {
        txtmenuname.Text = "";
        txtcode.Text = "";
        txtdescription.Text = "";
        txtPageUrl.Value = "";
        Hdn_Fld.Value = "";
        chkActive.Checked = true;
        lblMsg.Text = "";
        this.txtmark.Text = "?";
        this.txtquerystring.Text = "";

        ddlParentPageName.SelectedValue = "0";

        ddltype.SelectedValue = "M";


    }
   
    protected void btnAddMenu_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "";
            divadd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            Label1.Visible = false;
            Label1.Text = "";
            ddlreason.SelectedValue = "0";
            ResetAll();
            this.txtmark.Text = "?";
          

           
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
    }

  

    protected void btnMenuSubmit_Click(object sender, EventArgs e)
    {
       
        try
        {
            ClsMenuCreate clsMenu = new ClsMenuCreate();
            string url = string.Empty; 
            string MODE = string.Empty;
            string STATUS = string.Empty;
            string queryurl = string.Empty;
          
            int ID = 0;

            if (PageUrlUpload.HasFile)
            {
                url = PageUrlUpload.PostedFile.FileName.ToString();

            }


            if (txtmark.Text.Trim() == "?" && txtquerystring.Text.Trim() != "")
            {

                queryurl = url.Trim() + txtmark.Text.Trim() + txtquerystring.Text.Trim();
            }
            else
            {
                queryurl = url.Trim();
            }


            if (Hdn_Fld.Value == "")
            {
                MODE = "A";
            }
            else
            {
                MODE = "U";

                if (PageUrlUpload.HasFile)
                {
                    url = PageUrlUpload.PostedFile.FileName.ToString();

                    if (txtmark.Text.Trim() == "?" && txtquerystring.Text.Trim() != "")
                    {

                        queryurl = url.Trim() + txtmark.Text.Trim() + txtquerystring.Text.Trim();
                    }
                    else
                    {
                        queryurl = url.Trim();
                    }
                }
                else
                {

                    if (Label1.Text.Trim() != "")
                    {
                        queryurl = Label1.Text.Trim();
                    }
                    else
                    {
                        queryurl = "";
                    }
                }

            }

            if (chkActive.Checked)
            {
                STATUS = "Y";
            }
            else
            {
                STATUS = "N";
            }
           string reason=string.Empty;

           if (ddlreason.SelectedValue == "1")
            {
                reason="Y";
            }
           else if (ddlreason.SelectedValue == "2")
            {
                reason="N";
            }
           
           

           

            ID = clsMenu.CreateMenusave(Convert.ToString(Hdn_Fld.Value), txtmenuname.Text.ToString().Trim(), txtcode.Text.ToString().Trim(), Convert.ToString(txtdescription.Text.Trim()),
                                        Convert.ToInt32(ddlParentPageName.SelectedValue.Trim()), queryurl.Trim(),
                                        Convert.ToString(ddltype.SelectedValue),STATUS.Trim(),MODE.Trim(),reason.Trim());



            if (ID == 1)
            {

                LoadMenuMaster();
                //LoadParentPageName();
                //string message = "alert('Record Saved Successfully..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowSuccess("Record Saved Successfully..");
              
                
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                divadd.Style["display"] = "";
                Hdn_Fld.Value = "";
                ResetAll();
            }
            else if (ID == 3)
            {
                //string message = "alert('PageName Already Exist..')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("PageName Already exit..");

            }
           
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
   
   
    protected void btnMenuRefresh_Click(object sender, EventArgs e)
    {
        ResetAll();
        divadd.Style["display"] = "";
        pnlAdd.Style["display"] = "none";
        pnlDisplay.Style["display"] = "";
        LoadMenuMaster();
    }

   

    protected void DeleteRecord(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        ClsMenuCreate clsMenu = new ClsMenuCreate();
        try
        {
            if (e.Record["ID"] != "")
            {
                int ID = 0;
                ID = clsMenu.CreateMenuDelete(e.Record["ID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadMenuMaster();
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

    protected void btngridsave_Click(object sender, EventArgs e)
    {
        ClsMenuCreate clsMenu = new ClsMenuCreate();
        try
        {
             string urls = string.Empty;
            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            divadd.Style["display"] = "none";
            DataTable dt = clsMenu.gridedit(Hdn_Fld.Value);
            txtmenuname.Text = Convert.ToString(dt.Rows[0]["PageName"]);
            txtcode.Text = Convert.ToString(dt.Rows[0]["CODE"]);
            txtdescription.Text = Convert.ToString(dt.Rows[0]["DESCRIPTION"]);
            LoadParentPageName();
            ddlParentPageName.SelectedValue = Convert.ToString(dt.Rows[0]["ParentID"]);
            if (!PageUrlUpload.HasFile)
            {
                Label1.Visible = true;
                Label1.Text = Convert.ToString(dt.Rows[0]["PageURL"]);
                string url = Convert.ToString(dt.Rows[0]["PageURL"]);
                //url = PageUrlUpload.PostedFile.FileName.ToString();
                
            }
           
            //LoadType();
            ddltype.SelectedValue = Convert.ToString(dt.Rows[0]["Tag"]);
            if (Convert.ToString(dt.Rows[0]["STATUS"]) == "Y")
            {
                chkActive.Checked = true;
            }
            else
            {
                chkActive.Checked = false;
            }

        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

    protected void lnkmenurights_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("frmMenuRights.aspx");
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}