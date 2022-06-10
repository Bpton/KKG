using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Obout.Grid;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class VIEW_frmSalesPersonHierarchy : System.Web.UI.Page
{

 
    ClsSalesPersonHierarchy clssaleshierchy = new ClsSalesPersonHierarchy();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
          

            if (!IsPostBack)  
            {
                LoadBusinessSegment();
               LoadgvSalesPersonHierarchy();

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadBusinessSegment()
    {
        ddlBusinessSegment.Items.Clear();
        ddlBusinessSegment.Items.Insert(0, new ListItem("--Select Business Segment--","0"));
        ddlBusinessSegment.AppendDataBoundItems = true;
        ddlBusinessSegment.DataSource = clssaleshierchy.BindBusinessSegment();
        ddlBusinessSegment.DataTextField = "BSNAME";
        ddlBusinessSegment.DataValueField = "BSID";
        ddlBusinessSegment.DataBind();
    }

    public void LoadgvSalesPersonHierarchy()
    {
        DataTable dt = clssaleshierchy.BindgvSalesPersonHierarchy();
        gvSalesPersonHierarchy.DataSource = dt;
        gvSalesPersonHierarchy.DataBind();
       
    }

    public void BindSalesPerson()
    {
        DataTable dt = clssaleshierchy.fetchdatainGrid(ddlBusinessSegment.SelectedValue.ToString());
        if (dt.Rows.Count > 0)
        {
            gvSalesPersonHierarchy.DataSource = dt;
            gvSalesPersonHierarchy.DataBind();
        }
        else
        {
            LoadgvSalesPersonHierarchy();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int count = 0;
            int ID = 0;
            string Mode = "A";
            clssaleshierchy.DelectSalesPersonsHierarchy(ddlBusinessSegment.SelectedValue.ToString());
          
            for (int i = 0; i < gvSalesPersonHierarchy.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = gvSalesPersonHierarchy.RowsInViewState[i].Cells[2] as GridDataControlFieldCell;
                HiddenField hiddenField = cell.FindControl("hdnUTID") as HiddenField;
                TextBox txtlevel = cell.FindControl("txtLevel") as TextBox;

                if (!string.IsNullOrEmpty(txtlevel.Text) && Convert.ToDecimal(txtlevel.Text)>0)
                {
                        if (Mode == "A")
                        {
                            ID = clssaleshierchy.SaveSalesPersonsHierarchy(Convert.ToInt32(txtlevel.Text.Trim()), ddlBusinessSegment.SelectedValue.ToString(),
                                                                            hiddenField.Value, ddlBusinessSegment.SelectedItem.Text.Trim(), Mode);
                            count = count + 1;
                        }  
                }
            }
            if (count == 0)
            {
                //string message = "alert('Please level value must be more than 0')";
                //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                MessageBox1.ShowInfo("Please level value must be more than 0",80,400);
            }
            else
            {
                if (ID > 0)
                {
                    BindSalesPerson();
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowSuccess("Record Saved Successfully..");
                }
                else
                {
                    //string message = "alert('Record Saved Successfully..')";
                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    MessageBox1.ShowError("Record Saved Successfully..");
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }
    

   
    //protected void ddlBusinessSegment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlBusinessSegment.SelectedValue.ToString() != "")
    //        {

               

    //            DataTable dt = clssaleshierchy.fetchdatainGrid(ddlBusinessSegment.SelectedValue.ToString());
    //            if (dt.Rows.Count > 0)
    //            {
    //                ddlBusinessSegment.SelectedValue = dt.Rows[0]["BUSINESSSEGMENTID"].ToString();
    //               // LoadgvSalesPersonHierarchy(dt.Rows[0]["BUSINESSSEGMENTID"].ToString());
                    

    //            }
    //            else
    //            {
    //                gvSalesPersonHierarchy.DataSource = null;
    //                gvSalesPersonHierarchy.DataBind();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //    }

        
    //}
    protected void ddlBusinessSegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gvSalesPersonHierarchy.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = gvSalesPersonHierarchy.RowsInViewState[i].Cells[2] as GridDataControlFieldCell;
                HiddenField hiddenField = cell.FindControl("hdnUTID") as HiddenField;
                TextBox txtlevel = cell.FindControl("txtLevel") as TextBox;
                BindSalesPerson();
           }
        }
        catch (Exception ex)
        {
           string message = "alert('" + ex.Message.Replace("'", "") + "')";
           ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
       }

    }

    
}