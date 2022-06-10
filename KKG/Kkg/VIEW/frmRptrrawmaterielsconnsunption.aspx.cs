using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;

public partial class VIEW_frmRptrrawmaterielsconnsunption : System.Web.UI.Page
{
    #region VARIABLES
    DateTime today1 = DateTime.Now;
    string date = "dd/MM/yyyy";
    DataTable objDt = new DataTable();
    #endregion

    #region PAGE_lOAD
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.DateLock();
                this.LoadProduct();
                //date = Convert.ToString(today1);
                //this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                //this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
            }

        }
        catch (Exception ex)
        {
            string msg = string.Empty;
            msg = Convert.ToString(ex);
            MessageBox1.ShowError(msg);
        }

    }

    #endregion

    #region Add FinYear Wise Date Lock
    public void DateLock()
    {
        try
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string startyear = finyear.Substring(0, 4);
            int startyear1 = Convert.ToInt32(startyear);
            string endyear = finyear.Substring(5);
            int endyear1 = Convert.ToInt32(endyear);
            DateTime oDate = new DateTime(startyear1, 04, 01);
            DateTime cDate = new DateTime(endyear1, 03, 31);
            DateTime today1 = DateTime.Now;


            CalendarExtender1.StartDate = oDate;
            CalendarExtenderToDate.StartDate = oDate;


            if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
            {

                this.txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy").Replace('-', '/');


                CalendarExtender1.EndDate = today1;
                CalendarExtenderToDate.EndDate = today1;

            }
            else
            {

                this.txtFromDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');
                this.txtToDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy").Replace('-', '/');


                CalendarExtenderToDate.EndDate = cDate;
                CalendarExtender1.EndDate = cDate;

            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.Replace("'", "");
        }
    }

    #endregion

    #region Pdoduct_list

    public void LoadProduct()
    {
        try
        {
            ClsStockReport ClsBeat = new ClsStockReport();
            DataTable dt = ClsBeat.Bind_product();
            ddlproduct.Items.Clear();
            ddlproduct.Items.Insert(0, new ListItem("Select", "0"));
            ddlproduct.DataSource = dt;
            ddlproduct.DataTextField = "NAME";
            ddlproduct.DataValueField = "ID";
            ddlproduct.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }

    #endregion

    #region SEARCH
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataSet DS = new DataSet();
        ClsStockReport CLS = new ClsStockReport();
        DS = CLS.bindConSumptionReport_JSON(this.ddlproduct.SelectedValue,this.txtFromDate.Text,this.txtToDate.Text);
        if(DS.Tables[0].Rows.Count > 0)
        {
            grdRpt.DataSource = DS.Tables[0];
            grdRpt.DataBind();
            Session["consumption"] = DS.Tables[0];
        }
        else
        {
            grdRpt.DataSource = null;
            grdRpt.DataBind();
            Session["consumption"] = null;
            return;
        }
    }
    #endregion

    #region EXPOERT EXCEL
    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable parentTable = new DataTable();
        parentTable = (DataTable)Session["consumption"];


        string FileName = "Raw_Material_Conspmption_Report" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".csv";


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
    #endregion
}