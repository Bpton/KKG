#region NmaeSpaces
using BAL;
using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#endregion

#region VIEW_frmrptcustomer_dump
public partial class VIEW_frmrptcustomer_dump : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'> $(function () { $('#ContentPlaceHolder1_ddldistributor').multiselect({ includeSelectAllOption: true  });});</script>", false);
            if (!IsPostBack)
            {
                this.LoadDepot();
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

    #region LoadDepot
    public void LoadDepot()
    {
        try
        {
            ClsStockReport clsreport = new ClsStockReport();
            if (Session["TPU"].ToString() == "ADMIN") // COMPANY USER
            {
                DataTable dt = clsreport.BindDepot_Primary();
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.Items.Add(new ListItem("All", "0"));
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else if (Session["TPU"].ToString() == "D")
            {
                DataTable dt = clsreport.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.Items.Add(new ListItem("All", "0"));
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
            else
            {
                DataTable dt = clsreport.Depot_Accounts(HttpContext.Current.Session["IUSERID"].ToString());
                ddldepot.Items.Clear();
                ddldepot.AppendDataBoundItems = true;
                ddldepot.Items.Add(new ListItem("All", "0"));
                ddldepot.DataSource = dt;
                ddldepot.DataTextField = "BRNAME";
                ddldepot.DataValueField = "BRID";
                ddldepot.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region btnExport_Click
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            ClsStockReport clsrpt = new ClsStockReport();
            DataSet Customer_dump = new DataSet();
            Customer_dump = clsrpt.get_customer_dump(ddldepot.SelectedValue.Trim()); ;
            DataTable dt = new DataTable();
            dt = Customer_dump.Tables[0];
            dt.TableName = "Customer_Dump";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(Customer_dump);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Customer_Dump" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion

}
#endregion