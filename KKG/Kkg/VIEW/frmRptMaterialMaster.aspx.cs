using BAL;
using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FACTORY_frmRptMaterialMaster : System.Web.UI.Page
{
    ClsProductMaster ClsProduct = new ClsProductMaster();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadProductMaster(ddlFactory.SelectedValue.ToString());
                LoadDepotName();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    public void LoadProductMaster(string FactoryID)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClsProduct.RptBindMaterialGrid(FactoryID);
            this.gvMiscellaneousProduct.DataSource = dt;
            this.gvMiscellaneousProduct.DataBind();
            Session["Material"] = dt;
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #region LoadDepotName
    public void LoadDepotName()
    {
        try
        {
            ClsCommonFunction ClsCommon = new ClsCommonFunction();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = ClsCommon.BindDepot_Primary();
                ddlFactory.Items.Clear();
                ddlFactory.Items.Add(new ListItem("Select", "0"));
                ddlFactory.AppendDataBoundItems = true;
                ddlFactory.DataSource = dt;
                ddlFactory.DataTextField = "BRNAME";
                ddlFactory.DataValueField = "BRID";
                ddlFactory.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = ClsCommon.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddlFactory.Items.Clear();
                ddlFactory.Items.Add(new ListItem("Select", "0"));
                ddlFactory.AppendDataBoundItems = true;
                ddlFactory.DataSource = dt;
                ddlFactory.DataTextField = "BRNAME";
                ddlFactory.DataValueField = "BRID";
                ddlFactory.DataBind();
            }
            else
            {
                DataTable dt = ClsCommon.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());                
                ddlFactory.Items.Clear();
                ddlFactory.Items.Insert(0, new ListItem("All", "0"));
                ddlFactory.AppendDataBoundItems = true;
                ddlFactory.DataSource = dt;
                ddlFactory.DataTextField = "BRNAME";
                ddlFactory.DataValueField = "BRID";
                ddlFactory.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    protected void ddlFactory_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProductMaster(ddlFactory.SelectedValue.ToString());
    }
    protected void btnExportExcel_click(object sender, EventArgs e)
    {
       
        DataTable parentTable = new DataTable();
        parentTable = (DataTable)Session["Material"];
        

        string FileName = "Material_master" +DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".csv";


        string filepath = Server.MapPath("../") + "fileupload\\" + FileName;

        if (parentTable.Rows.Count > 0)
        {

            StreamWriter sw = new StreamWriter(filepath, false);
            //headers    
            for (int i = 0; i < parentTable.Columns.Count; i++)
            {
                sw.Write(parentTable.Columns[i]);
                if (i < parentTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in parentTable.Rows)
            {
                for (int i = 0; i < parentTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < parentTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();

        }
        Response.Clear();
        Response.ContentType = "application/csv";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
        Response.TransmitFile(filepath);
        Response.End();

       

    }

        //protected void gvMiscellaneousProduct_RowDataBound(object sender, GridRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == GridRowType.DataRow)
        //        {
        //            GridDataControlFieldCell cell = e.Row.Cells[5] as GridDataControlFieldCell;
        //            string status = cell.Text;

        //            if (status == "Active")
        //            {
        //                cell.ForeColor = Color.Green;
        //            }
        //            else
        //            {
        //                cell.ForeColor = Color.Red;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    }
        //}

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    /* Verifies that the control is rendered */
        //}

        //#region Export to Excel
        //protected void btnExport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Modified by HP
        //        Response.Clear();
        //        Response.Buffer = true;
        //        Response.ClearContent();
        //        Response.ClearHeaders();
        //        Response.Charset = "";
        //        string FileName = "Material_Master" + DateTime.Now + ".xls";
        //        StringWriter strwritter = new StringWriter();
        //        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.ContentType = "application/vnd.ms-excel";
        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //        gvMiscellaneousProduct.GridLines = GridLines.Both;
        //        gvMiscellaneousProduct.HeaderStyle.Font.Bold = true;
        //        gvMiscellaneousProduct.RenderControl(htmltextwrtter);
        //        Response.Write(strwritter.ToString());
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //    }
        //}
        //#endregion       


    }