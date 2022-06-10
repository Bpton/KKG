using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmVendorGroupMapping : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";

                this.LoadVendorGroupMapping();


            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    public void LoadVendorGroupMapping()
    {
        try
        {
            ClsGroupMapping clsgroup = new ClsGroupMapping();
            DataTable dt = clsgroup.BindVendorGroupGrid();
            if (dt.Rows.Count > 0)
            {
                //gvVendorGroupMapping.ClearPreviousDataSource();
                this.gvVendorGroupMapping.DataSource = dt;
                this.gvVendorGroupMapping.DataBind();
            }
            else
            {
                this.gvVendorGroupMapping.DataSource = null;
                this.gvVendorGroupMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    protected void btnAddnewRecord_Click(object sender, EventArgs e)
    {
        try
        {

            txtCode.Text = "";
            txtName.Text = "";

            pnlAdd.Style["display"] = "";
            pnlDisplay.Style["display"] = "none";
            btnaddhide.Style["display"] = "none";
            Hdn_Fld.Value = "";
            this.LoadVendor();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #region load Vendor
    private void LoadVendor()
    {
        ClsGroupMapping clsgroup = new ClsGroupMapping();
        DataTable dt = clsgroup.BindVendor();
        if (dt.Rows.Count > 0)
        {
            gvVendor.DataSource = dt;
            gvVendor.DataBind();
        }
        else
        {
            gvVendor.DataSource = null;
            gvVendor.DataBind();
        }
    }
    #endregion





    #region Convert DataTable To XML
    public string ConvertDatatableToXML(DataTable dt)
    {
        MemoryStream str = new MemoryStream();
        dt.TableName = "XMLData";
        dt.WriteXml(str, true);
        str.Seek(0, SeekOrigin.Begin);
        StreamReader sr = new StreamReader(str);
        string xmlstr;
        xmlstr = sr.ReadToEnd();
        return (xmlstr);
    }
    #endregion

    #region Create DataTable Structure
    public DataTable CreateDataTable()
    {
        DataTable dt = new DataTable();

        dt.Clear();
        dt.Columns.Add(new DataColumn("VENDORID", typeof(string)));
        dt.Columns.Add(new DataColumn("VENDORNAME", typeof(string)));

        HttpContext.Current.Session["VENDORGROUPMAPPING"] = dt;

        return dt;
    }
    #endregion


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            ClsGroupMapping clsgroup = new ClsGroupMapping();
            int ID = 0;
            int count = 0;
            string xml = string.Empty;
            if (HttpContext.Current.Session["VENDORGROUPMAPPING"] == null)
            {
                this.CreateDataTable();
            }

            DataTable dtadd = (DataTable)HttpContext.Current.Session["VENDORGROUPMAPPING"];

            string cbu = HttpContext.Current.Session["UserID"].ToString();

            foreach (GridViewRow gv in gvVendor.Rows)
            {
                Label lblVENDORID = (Label)gv.FindControl("lblVENDORID");
                CheckBox chk = (CheckBox)gv.FindControl("chkSelect");
                Label lblVENDORNAME = (Label)gv.FindControl("lblVENDORNAME");
                if (chk.Checked)
                {
                    count = count + 1;
                    DataRow dr = dtadd.NewRow();
                    dr["VENDORID"] = lblVENDORID.Text.Trim();
                    dr["VENDORNAME"] = lblVENDORNAME.Text.Trim();
                    dtadd.Rows.Add(dr);
                    dtadd.AcceptChanges();
                }
            }
            if (count == 0)
            {
                HttpContext.Current.Session["VENDORGROUPMAPPING"] = null;
                MessageBox1.ShowInfo("Plese select 1 record");

                return;
            }
            else
            {
                HttpContext.Current.Session["VENDORGROUPMAPPING"] = dtadd;

                xml = ConvertDatatableToXML(dtadd);
                ID = clsgroup.SaveVendorGroup(Hdn_Fld.Value, this.txtCode.Text.Trim(), this.txtName.Text.Trim(), cbu, xml);
                if (ID > 0)
                {
                    MessageBox1.ShowSuccess("Record Saved Successfully..");

                    pnlAdd.Style["display"] = "none";

                    pnlDisplay.Style["display"] = "";
                    btnaddhide.Style["display"] = "";
                    Hdn_Fld.Value = "";
                    HttpContext.Current.Session["VENDORGROUPMAPPING"] = null;
                    this.LoadVendorGroupMapping();
                }
                else
                {

                    MessageBox1.ShowInfo("Error Saving Record");
                    HttpContext.Current.Session["VENDORGROUPMAPPING"] = null;
                    return;
                }
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
            txtCode.Text = "";
            txtName.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";

            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
            LoadVendorGroupMapping();
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
            if (e.Record["GROUPID"] != "")
            {
                int ID = 0;
                ClsGroupMapping clsgroup = new ClsGroupMapping();
                ID = clsgroup.DeleteVendorGroup(e.Record["GROUPID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadVendorGroupMapping();
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




    protected void btngridedit_Click(object sender, EventArgs e)
    {

        try
        {
            ClsGroupMapping clsgroup = new ClsGroupMapping();
            //this.CreateDataTable();
            this.LoadVendor();
            DataSet ds = new DataSet();
            ds = clsgroup.VendorGroupEdit(Hdn_Fld.Value);
            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (GridViewRow gv in gvVendor.Rows)
                    {
                        Label lblVENDORID = (Label)gv.FindControl("lblVENDORID");
                        CheckBox chk = (CheckBox)gv.FindControl("chkSelect");
                        Label lblVENDORNAME = (Label)gv.FindControl("lblVENDORNAME");
                        string vendorid = Convert.ToString(ds.Tables[1].Rows[0]["VENDORID"]).Trim();

                        for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                        {
                            if (ds.Tables[1].Rows[i]["VENDORID"].ToString() == lblVENDORID.Text)
                            {
                                chk.Checked = true;

                            }
                        }



                    }
                }




                this.txtCode.Text = Convert.ToString(ds.Tables[0].Rows[0]["GROUPCODE"]).Trim();
                this.txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["GROUPNAME"]).Trim();


                pnlAdd.Style["display"] = "";
                pnlDisplay.Style["display"] = "none";
                btnaddhide.Style["display"] = "none";


            }
            else
            {
                txtCode.Text = "";
                txtName.Text = "";
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}