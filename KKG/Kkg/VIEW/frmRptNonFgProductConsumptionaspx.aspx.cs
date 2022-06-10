using PPBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmRptNonFgProductConsumptionaspx : System.Web.UI.Page
{
    ClsStockReport objStockReport = new ClsStockReport();
    DateTime today1 = DateTime.Now;
    DataTable objDt = new DataTable();
    string date = "dd/MM/yyyy";
    public enum MessageType { Success, Error, Info, Warning };
    
    public class consumptiondetails
    {
        public string slno { get; set; }
        public string itemName { get; set;}
        public string itemCode { get; set;}
        public string itemType { get; set;}
        public string department { get; set;}
        public string unit { get; set;}
        public decimal qty { get; set;}
    }
protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if(!IsPostBack)
            {
                this.txtReqFromDate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtender6.EndDate = DateTime.Now;
                this.txttoDate.Text = today1.ToString(date).Replace('-', '/');
                this.CalendarExtendertoDate.EndDate = DateTime.Now;
            }
        }
        catch(Exception ex)
        {
            string msg = ex.Message;
        }
    }

    protected void ShowMessage(string Message, MessageType type)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), System.Guid.NewGuid().ToString(), "ShowMessage('" + Message + "','" + type + "');", true);
    }
    protected void btnShowReport_Click(object sender, EventArgs e)
    {
        try
        {
           
            objDt = objStockReport.fetchNonFgProductConsumptionQty(this.txtReqFromDate.Text.ToString(),this.txttoDate.Text.ToString());
            if(objDt.Rows.Count>0)
            {
                grdRpt.DataSource = objDt;
                grdRpt.DataBind();
                Session["consumption"] = objDt;
            }
            else
            {
                grdRpt.DataSource = null;
                grdRpt.DataBind();
                Session["consumption"] = null;
                ShowMessage("No data found", MessageType.Info);
                return;
            }
            

            //List<consumptiondetails> consumptiondetails = new List<consumptiondetails>();
            //foreach (DataRow row in objDt.Rows)
            //{
            //    consumptiondetails.Add(new consumptiondetails
            //    {
            //        slno =row["slno"].ToString(),
            //        itemName = row["itemName"].ToString(),
            //        itemCode = row["itemCode"].ToString(),
            //        itemType = row["itemType"].ToString(),
            //        department = row["department"].ToString(),
            //        unit = row["unit"].ToString(),
            //        qty = Convert.ToDecimal(row["qty"])
            //    });
            //    grdRpt.DataSource = consumptiondetails;
            //    grdRpt.DataBind();
            //}
            

        }
        catch(Exception ex)
        {

        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        DataTable parentTable = new DataTable();
        parentTable = (DataTable)Session["consumption"];


        string FileName = "Conspmption_Report" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".csv";


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
}