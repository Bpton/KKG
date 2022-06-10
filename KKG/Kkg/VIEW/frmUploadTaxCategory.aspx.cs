using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VIEW_frmUploadTaxCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                this.pnlAdd.Style["dispaly"] = "";
                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";
                this.txtFromdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.txtTodate.Text = dtcurr.ToString(date).Replace('-', '/');
                /*Calender Control Date Range*/
                CalendarExtender1.EndDate = DateTime.Now;
               // CalendarExtender2.EndDate = DateTime.Now;
                /****************************/
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }


    protected void CreateTaxUploadTable()
    {
        DataTable dtadd = new DataTable();
        dtadd.Columns.Add("CATEGORYID", typeof(string));

        dtadd.Columns.Add("INPUTCGSTID", typeof(string));
        dtadd.Columns.Add("INPUT_CGST_PER", typeof(decimal));

        dtadd.Columns.Add("INPUTSGSTID", typeof(string));
        dtadd.Columns.Add("INPUT_SGST_PER", typeof(string));

        dtadd.Columns.Add("INPUTIGSTID", typeof(string));
        dtadd.Columns.Add("INPUT_IGST_PER", typeof(string));

        dtadd.Columns.Add("OUTPUTCGSTID", typeof(string));
        dtadd.Columns.Add("OUTPUT_CGST_PER", typeof(string));

        dtadd.Columns.Add("OUTPUTSGSTID", typeof(string));
        dtadd.Columns.Add("OUTPUT_SGST_PER", typeof(string));

        dtadd.Columns.Add("OUTPUTIGSTID", typeof(string));
        dtadd.Columns.Add("OUTPUT_IGST_PER", typeof(string)); 

            dtadd.Columns.Add("FROMDATE", typeof(string)); 
            dtadd.Columns.Add("TODATE", typeof(string)); 

        HttpContext.Current.Session["TAX_CATEGORY_UPLOAD"] = dtadd;
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

    #region DATEFORMAT CHEACK(DD-MM-YY)
    private int ValidateDate(string date)
    {
        string[] dateParts = date.Replace('-', '/').Split('/');
        if (dateParts.Length > 1)
        {
            int dd = Convert.ToInt32(dateParts[0]);
            int mm = Convert.ToInt32(dateParts[1]);
            int yyyy = Convert.ToInt32(dateParts[2]);

            int ddlength = Convert.ToInt32(dateParts[0].Length);
            int mmlength = Convert.ToInt32(dateParts[1].Length);
            int yyyylength = Convert.ToInt32(dateParts[2].Length);

            if (ddlength == 2 && mmlength == 2 && yyyylength == 4)
            {
                if (dd <= 31 && mm <= 12)
                {
                    DateTime testDate = new DateTime(yyyy, mm, dd);
                    return 0; // insert DATATABLE
                }
                else
                {
                    return 2; // not valid date 
                }
            }
            else
            {
                return 2; // not valid date 
            }
        }
        else
        {
            return 1; // not valid date 
        }
    }
    #endregion

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string rowNumber = "0";
        string xml = string.Empty;
        string FilenameExtension = Path.GetFileName(FileUpload1.FileName);
        int RESULT = 0;
        int id = 0;
        ClsOpenStock clsstock = new ClsOpenStock();
        try
        {
            if (FilenameExtension == "")
            {
                MessageBox1.ShowWarning("Please Uploaded CSV file");
                return;
            }
            else
            {
                string currentPath = Server.MapPath("~/FileUpload/") +
                                 Path.GetFileName(FileUpload1.FileName);
                string Extension = Path.GetExtension(currentPath);

                if (Extension.Trim() != ".csv")
                {
                    MessageBox1.ShowWarning("Please Convert uploaded file to CSV file");
                    return;
                }
                else
                {
                    FileUpload1.SaveAs(currentPath);
                    DataTable dt = ConvertCSVtoDataTable(currentPath);
                    CreateTaxUploadTable();
                    DataTable dtadd = (DataTable)HttpContext.Current.Session["TAX_CATEGORY_UPLOAD"];


                    #region INSERT DATATABLE

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        rowNumber = Convert.ToString(i);
                        if (Convert.ToString(dt.Rows[i]["CATEGORYID"]) != "")
                        {

                            DataRow dr = dtadd.NewRow();
                            if (Convert.ToDecimal(dt.Rows[i]["INPUT_CGST_PER"]) > 0 || Convert.ToDecimal(dt.Rows[i]["INPUT_SGST_PER"]) > 0
                                || Convert.ToDecimal(dt.Rows[i]["INPUT_IGST_PER"]) > 0 || Convert.ToDecimal(dt.Rows[i]["OUTPUT_CGST_PER"]) > 0
                                || Convert.ToDecimal(dt.Rows[i]["OUTPUT_SGST_PER"]) > 0 || Convert.ToDecimal(dt.Rows[i]["OUTPUT_IGST_PER"]) > 0)
                            {

                                dr["CATEGORYID"] = Convert.ToString(dt.Rows[i]["CATEGORYID"]);

                                dr["INPUTCGSTID"] = Convert.ToString(dt.Rows[i]["INPUTCGSTID"]);
                                dr["INPUT_CGST_PER"] = Convert.ToDecimal(dt.Rows[i]["INPUT_CGST_PER"]);

                                dr["INPUTSGSTID"] = Convert.ToString(dt.Rows[i]["INPUTSGSTID"]);
                                dr["INPUT_SGST_PER"] = Convert.ToDecimal(dt.Rows[i]["INPUT_SGST_PER"]);

                                dr["INPUTIGSTID"] = Convert.ToString(dt.Rows[i]["INPUTIGSTID"]);
                                dr["INPUT_IGST_PER"] = Convert.ToDecimal(dt.Rows[i]["INPUT_IGST_PER"]);

                                dr["OUTPUTCGSTID"] = Convert.ToString(dt.Rows[i]["OUTPUTCGSTID"]);
                                dr["OUTPUT_CGST_PER"] = Convert.ToDecimal(dt.Rows[i]["OUTPUT_CGST_PER"]);

                                dr["OUTPUTSGSTID"] = Convert.ToString(dt.Rows[i]["OUTPUTSGSTID"]);
                                dr["OUTPUT_SGST_PER"] = Convert.ToDecimal(dt.Rows[i]["OUTPUT_SGST_PER"]);

                                dr["OUTPUTIGSTID"] = Convert.ToString(dt.Rows[i]["OUTPUTIGSTID"]);
                                dr["OUTPUT_IGST_PER"] = Convert.ToDecimal(dt.Rows[i]["OUTPUT_IGST_PER"]);

                                dr["FROMDATE"] = Convert.ToString(this.txtFromdate.Text);
                                dr["TODATE"] = Convert.ToString(this.txtTodate.Text);

                                dtadd.Rows.Add(dr);
                                dtadd.AcceptChanges();
                            }
                        }
                    }


                    #endregion

                    #region FINAL SAVE
                    if (RESULT == 1)
                    {
                        MessageBox1.ShowWarning("Tax or category blank please Check on row number:" + rowNumber);
                        return;
                    }
                    else
                    {
                        HttpContext.Current.Session["TAX_CATEGORY_UPLOAD"] = dtadd;
                        xml = ConvertDatatableToXML(dtadd);
                        id = clsstock.UploadTaxCategory(xml);
                        if (id > 0)
                        {
                            MessageBox1.ShowSuccess("Tax mapping successfully");
                            HttpContext.Current.Session["TAX_CATEGORY_UPLOAD"] = null;
                            Session["TAX_CATEGORY_UPLOAD"] = null;

                        }
                        else
                        {
                            MessageBox1.ShowWarning("Error on Saving record..");
                            RESULT = 0;
                            Session["TAX_CATEGORY_UPLOAD"] = null;

                        }
                    }
                    #endregion
                }
            }
        }
        catch(Exception ex)
        {
            MessageBox1.ShowError("Please Check Row Number:" + rowNumber);
        }
    }
    public static DataTable ConvertCSVtoDataTable(string strFilePath)
    {
        DataTable dt = new DataTable();
        using (StreamReader sr = new StreamReader(strFilePath))
        {
            string[] headers = sr.ReadLine().Split(',');
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btngeneraltemp_Click(object sender, EventArgs e)
    {
        try
        {
           
            ClsOpenStock ClsOpenStock = new ClsOpenStock();
            DataTable dt = ClsOpenStock.BindGenerateTempForTaxCategory();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=ProductDetails.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }



}