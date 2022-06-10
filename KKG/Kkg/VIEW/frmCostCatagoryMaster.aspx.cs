using Obout.Grid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
public partial class VIEW_frmCostCatagoryMaster : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                pnlCostCentreMapping.Style["display"] = "none";
                btnaddhide.Style["display"] = "";
                LoadCostCatagoryMaster();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    public void LoadCostCatagoryMaster()
    {
        try
        {
            ClsCostCategory clscostcat = new ClsCostCategory();
            grdcostcat.DataSource = clscostcat.BindCostCatagoryMastergrid();
            grdcostcat.DataBind();
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }

    }
    protected void btnAddCatagory_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCostCategory clscostcat = new ClsCostCategory();
            txtcostcatcode.Text = "";
            txtcostcatname.Text = "";
            txtcostDescription.Text = "";
            chkActive.Checked = true;
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
    protected void btncatsubmit_click(object sender, EventArgs e)
    {
        try
        {
            ClsCostCategory clscostcat = new ClsCostCategory();
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

            ID = clscostcat.SaveCostCatagoryMaster(Hdn_Fld.Value, txtcostcatcode.Text.Trim(), txtcostcatname.Text.Trim(), txtcostDescription.Text.Trim(), HttpContext.Current.Session["USERID"].ToString().Trim(), Mode, chkActive.Checked.ToString().Trim());

            if (ID == 1)
            {
                MessageBox1.ShowSuccess("Record Saved Successfully!");
                LoadCostCatagoryMaster();
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
                Hdn_Fld.Value = "";
            }
            else if (ID == 2)
            {
                MessageBox1.ShowInfo("</b> Code already exist..");
            }
            else if (ID == 3)
            {
                MessageBox1.ShowInfo("</b> Name already exist..");
            }
        }

        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void btncatcancel_click(object sender, EventArgs e)
    {
        try
        {
            ClsCostCategory clscostcat = new ClsCostCategory();

            txtcostcatcode.Text = "";
            txtcostcatname.Text = "";
            txtcostDescription.Text = "";
            Hdn_Fld.Value = "";
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            btnaddhide.Style["display"] = "";
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
            ClsCostCategory clscostcat = new ClsCostCategory();

            if (e.Record["COSTCATID"] != "")
            {
                int ID = 0;
                ID = clscostcat.DeleteCostCatagoryMaster(e.Record["COSTCATID"].ToString());
                if (ID > 0)
                {
                    e.Record["Error"] = "Record Deleted Successfully. ";
                    LoadCostCatagoryMaster();
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

    protected void btngridedit_Click(object sender, EventArgs e)
    {
        ClsCostCategory clscostcat = new ClsCostCategory();

        try
        {

            if (Hdn_Fld.Value != "")
            {
                string id = Hdn_Fld.Value.ToString();
                DataTable dt = new DataTable();
                dt = clscostcat.BindCostCatagoryMastergrideditbyid(id);
                if (dt.Rows.Count > 0)
                {
                    this.txtcostcatcode.Text = dt.Rows[0]["COSTCATCODE"].ToString().Trim();
                    this.txtcostcatname.Text = dt.Rows[0]["COSTCATNAME"].ToString().Trim();
                    this.txtcostDescription.Text = dt.Rows[0]["COSTCATDESRIPTION"].ToString().Trim();
                    if (dt.Rows[0]["ACTIVE"].ToString().Trim() == "Active")
                    {
                        chkActive.Checked = true;
                    }
                    else
                    {
                        chkActive.Checked = false;
                    }
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
                pnlAdd.Style["display"] = "none";
                pnlDisplay.Style["display"] = "";
                btnaddhide.Style["display"] = "";
            }

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void grdcostcat_RowDataBound(object sender, GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == GridRowType.DataRow)
            {
                GridDataControlFieldCell cell = e.Row.Cells[4] as GridDataControlFieldCell;
                string status = cell.Text;

                if (status == "Active")
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

    #region Cost Centre mapping


    private void LoadCostcentreGrid()
    {
        try
        {
            ClsCostCategory clscat = new ClsCostCategory();
            DataTable dt = clscat.BindCostCentre();
            if (dt.Rows.Count > 0)
            {
                this.gvCostCentreMapping.DataSource = dt;
                this.gvCostCentreMapping.DataBind();
            }
            else
            {
                this.gvCostCentreMapping.DataSource = null;
                this.gvCostCentreMapping.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";

        }
    }



    #region Create DataTable Structure
    public DataTable CreateDataTablecentreMapping()
    {
        DataTable dt = new DataTable();

        dt.Clear();

        dt.Columns.Add(new DataColumn("COSTCATID", typeof(string)));
        dt.Columns.Add(new DataColumn("COSTCATNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("COSTCENTREID", typeof(string)));
        dt.Columns.Add(new DataColumn("COSTCENTRENAME", typeof(string)));

        HttpContext.Current.Session["CENTREMAPPING"] = dt;

        return dt;
    }
    #endregion
    protected void btnCostCentremapping_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "none";
            pnlCostCentreMapping.Style["display"] = "";
            btnaddhide.Style["display"] = "none";

            CreateDataTablecentreMapping();

            LoadCostcentreGrid();

            ClsCostCategory clscat = new ClsCostCategory();

            DataTable dt = clscat.EditCostCentre(Hdn_Fld.Value);
            if (dt.Rows.Count > 0)
            {
                foreach (GridViewRow gvrow in gvCostCentreMapping.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    Label lblCOSTCENTREID = (Label)gvrow.FindControl("lblCOSTCENTREID");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][2].ToString() == lblCOSTCENTREID.Text)
                        {
                            chk.Checked = true;

                        }
                    }
                }
            }


        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }

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

    protected void btnCostCentreMappingSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCostCategory clscat = new ClsCostCategory();
            int id = 0;
            if (HttpContext.Current.Session["CENTREMAPPING"] == null)
            {
                CreateDataTablecentreMapping();
            }
            DataTable dtadd = new DataTable();
            dtadd = (DataTable)HttpContext.Current.Session["CENTREMAPPING"];

            int count = 0;
            foreach (GridViewRow gvrow in gvCostCentreMapping.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                Label lblCOSTCENTREID = (Label)gvrow.FindControl("lblCOSTCENTREID");


                if (chk != null & chk.Checked)
                {
                    count = count + 1;
                    DataRow dr = dtadd.NewRow();

                    dr["COSTCATID"] = Hdn_Fld.Value.Trim();
                    dr["COSTCATNAME"] = txtcostcategory.Text.Trim();
                    dr["COSTCENTREID"] = lblCOSTCENTREID.Text;
                    dr["COSTCENTRENAME"] = gvrow.Cells[1].Text;
                    dtadd.Rows.Add(dr);
                    dtadd.AcceptChanges();
                }
            }

            HttpContext.Current.Session["CENTREMAPPING"] = dtadd;

            if (count == 0)
            {
                MessageBox1.ShowInfo("Please select atleast 1 Cost Centre", 60, 400);
            }
            else
            {
                if (dtadd.Rows.Count > 0)
                {
                    string xml = ConvertDatatableToXML(dtadd);

                    id = clscat.SaveCostCategoryMapping(Hdn_Fld.Value.Trim(), xml);

                    if (id > 0)
                    {
                        MessageBox1.ShowSuccess("Record saved successfully..");

                        Hdn_Fld.Value = "";
                        txtcostcategory.Text = "";

                        pnlAdd.Style["display"] = "none";
                        pnlDisplay.Style["display"] = "";
                        pnlCostCentreMapping.Style["display"] = "none";
                        btnaddhide.Style["display"] = "";


                        HttpContext.Current.Session["CENTREMAPPING"] = null;
                        gvCostCentreMapping.DataSource = null;
                        gvCostCentreMapping.DataBind();
                    }
                    else
                    {
                        MessageBox1.ShowError("<b><font color='red'>Error saving records..!</font></b>");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }
    protected void btnCostCentreMappingCancel_Click(object sender, EventArgs e)
    {
        try
        {
            pnlAdd.Style["display"] = "none";
            pnlDisplay.Style["display"] = "";
            pnlCostCentreMapping.Style["display"] = "none";
            HttpContext.Current.Session["CENTREMAPPING"] = null;
            gvCostCentreMapping.DataSource = null;
            gvCostCentreMapping.DataBind();
            btnaddhide.Style["display"] = "";
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    #endregion
}