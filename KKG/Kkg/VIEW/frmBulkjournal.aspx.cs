using PPBLL;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

public partial class VIEW_frmBulkjournal : System.Web.UI.Page
{
    ClsStockJournal ClsStockadjustment = new ClsStockJournal();
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        FileUpload1.Attributes.Add("onchange", "SetFileName('" + FileUpload1.ClientID + "','" + txtWayBill.ClientID + "')");
        try
        {
            if (!IsPostBack)
            {
                string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
                string startyear = finyear.Substring(0, 4);
                int startyear1 = Convert.ToInt32(startyear);
                string endyear = finyear.Substring(5);
                int endyear1 = Convert.ToInt32(endyear);
                DateTime oDate = new DateTime(startyear1, 04, 01);
                DateTime cDate = new DateTime(endyear1, 03, 31);
                CalendarExtender3.StartDate = oDate;
                DateTime today1 = DateTime.Now;
                if (today1 >= new DateTime(startyear1, 04, 01) && today1 <= new DateTime(endyear1, 03, 31))
                {
                    this.txtJournalDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    CalendarExtender3.EndDate = today1;
                }
                else
                {
                    this.txtJournalDate.Text = new DateTime(endyear1, 03, 31).ToString("dd/MM/yyyy");
                    CalendarExtender3.EndDate = cDate;
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

    #region CreateDataTableStructure
    protected void CreateDataTableStructure()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("PRODUCTID", typeof(string));
        dt.Columns.Add("PRODUCTNAME", typeof(string));
        dt.Columns.Add("PACKINGSIZEID", typeof(string));
        dt.Columns.Add("PACKINGSIZENAME", typeof(string));
        dt.Columns.Add("BATCHNO", typeof(string));
        dt.Columns.Add("ADJUSTMENTQTY", typeof(string));
        dt.Columns.Add("PRICE", typeof(string));
        dt.Columns.Add("AMOUNT", typeof(string));
        dt.Columns.Add("REASONID", typeof(string));
        dt.Columns.Add("REASONNAME", typeof(string));
        dt.Columns.Add("STORELOCATIONID", typeof(string));
        dt.Columns.Add("STORELOCATIONNAME", typeof(string));
        dt.Columns.Add("MFDATE", typeof(string));
        dt.Columns.Add("EXPRDATE", typeof(string));
        dt.Columns.Add("ASSESMENTPERCENTAGE", typeof(string));
        dt.Columns.Add("MRP", typeof(string));
        dt.Columns.Add("WEIGHT", typeof(string));
        dt.Columns.Add("JCID", typeof(string));
        dt.Columns.Add("JCNAME", typeof(string));
        dt.Columns.Add("STOCK_IN_HAND_IN_PCS", typeof(string));

        HttpContext.Current.Session["DATAUPLOAD"] = dt;
    }
    #endregion

    #region btnUpload_Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string id = string.Empty;

            string date = this.txtJournalDate.Text;
            string FilenameExtension = Path.GetFileName(FileUpload1.FileName);

            if (FilenameExtension == "")
            {
                MessageBox1.ShowInfo("<b>Please Uploaded Retailer CSV file.</b>");
                return;
            }
            else
            {
                string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
                string currentPath = Server.MapPath("~/FileUpload/") + Path.GetFileName(FileUpload1.FileName);
                string Extension = Path.GetExtension(currentPath);
                if (Extension.Trim() != ".csv")
                {
                    MessageBox1.ShowInfo("Please Convert uploaded file to CSV file");
                    return;
                }
                else
                {
                    FileUpload1.SaveAs(currentPath);
                    DataTable dt = GetDataTabletFromCSVFile(currentPath);



                    if (dt.Rows.Count > 0)
                    {


                        if (HttpContext.Current.Session["DATAUPLOAD"] == null)
                        {
                            CreateDataTableStructure();
                        }
                        DataTable dtadd = (DataTable)HttpContext.Current.Session["DATAUPLOAD"];

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            decimal rate = 0;
                                //ClsStockadjustmentDMS.BindDepotRate(Convert.ToString(dt.Rows[i]["PRODUCT_ID"]).Trim(), Convert.ToDecimal(dt.Rows[i]["MRP"]), HttpContext.Current.Session["UserID"].ToString());
                            decimal amount = 0;
                            decimal journalqty = Convert.ToDecimal(dt.Rows[i]["PHYSICAL_STOCK_IN_PCS"]) - Convert.ToDecimal(dt.Rows[i]["STOCK_IN_HAND_IN_PCS"]);
                            amount = rate * journalqty;
                            if (Convert.ToDecimal(dt.Rows[i]["PHYSICAL_STOCK_IN_PCS"]) < 0)
                            {
                                MessageBox1.ShowError("Physical stock should be positive.");
                                return;
                            }
                            else
                            {
                                if (journalqty != 0)
                                {
                                    DataRow dr = dtadd.NewRow();

                                    dr["PRODUCTID"] = Convert.ToString(dt.Rows[i]["PRODUCT_ID"]).Trim();
                                    dr["PRODUCTNAME"] = Convert.ToString(dt.Rows[i]["PRODUCT_NAME"]).Trim();
                                    dr["PACKINGSIZEID"] = Convert.ToString(dt.Rows[i]["PACKINGSIZEID"]).Trim();
                                    dr["PACKINGSIZENAME"] = Convert.ToString(dt.Rows[i]["PACKINGSIZENAME"]).Trim();
                                    dr["BATCHNO"] = "";
                                    dr["ADJUSTMENTQTY"] = journalqty;
                                    dr["PRICE"] = Convert.ToString(dt.Rows[i]["RATE"]).Trim();
                                    dr["AMOUNT"] = amount;
                                    dr["REASONID"] = "NA";
                                    dr["REASONNAME"] = "NA";
                                    dr["STORELOCATIONID"] = Convert.ToString(dt.Rows[i]["STORELOCATIONID"]).Trim();
                                    dr["STORELOCATIONNAME"] = Convert.ToString(dt.Rows[i]["STORELOCATIONNAME"]).Trim();
                                    dr["MFDATE"] = "01/01/1900";
                                    dr["EXPRDATE"] = "01/01/1900";
                                    dr["ASSESMENTPERCENTAGE"] = "0.00";
                                    dr["MRP"] = Convert.ToString(dt.Rows[i]["MRP"]).Trim();
                                    dr["WEIGHT"] = "";
                                    dr["JCID"] = "";
                                    dr["JCNAME"] = "";
                                    dr["STOCK_IN_HAND_IN_PCS"] = Convert.ToDecimal(dt.Rows[i]["STOCK_IN_HAND_IN_PCS"]);

                                    dtadd.Rows.Add(dr);
                                    dtadd.AcceptChanges();
                                }
                            }


                        }

                        string xmlbulkjournal = string.Empty;
                        xmlbulkjournal = ConvertDatatableToXML(dtadd);
                        //string Checking_stock = string.Empty;
                        //Checking_stock = ClsStockadjustmentDMS.BulkJournalchecking(HttpContext.Current.Session["UserID"].ToString(),xmlbulkjournal);
                        //if(Checking_stock =="1")
                        //{
                        //    MessageBox1.ShowError("Please Download New Excel For Upload Bulk Jounal.");
                        //    return;
                        //}
                        
                        id = ClsStockadjustment.InsertBulkJournalDetails(date, HttpContext.Current.Session["DEPOTID"].ToString(), HttpContext.Current.Session["UserName"].ToString(), HttpContext.Current.Session["UserID"].ToString(), HttpContext.Current.Session["FINYEAR"].ToString(), "", xmlbulkjournal, "Bulk journal upload");

                        if (id != "")
                        {
                            MessageBox1.ShowSuccess("Journal No :  <b><font color='green'>" + id + "</font></b>  saved successfully", 60, 550);
                            HttpContext.Current.Session["DATAUPLOAD"] = null;
                            this.FileUpload1 = null;
                            this.btnUpload.Style["display"] = "";
                        }

                        else
                        {
                            this.btnUpload.Style["display"] = "";
                            HttpContext.Current.Session["DATAUPLOAD"] = null;
                            MessageBox1.ShowError("Error on Saving record..");
                        }

                    }
                    else
                    {
                        this.btnUpload.Style["display"] = "";
                        HttpContext.Current.Session["DATAUPLOAD"] = null;
                        MessageBox1.ShowError("Error in saving....");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            this.btnUpload.Style["display"] = "";
            HttpContext.Current.Session["DATAUPLOAD"] = null;
            string message = ex.Message.Replace("'", "");
            MessageBox1.ShowError(message);
        }
    }
    #endregion

    #region GetDataTabletFromCSVFile
    public DataTable GetDataTabletFromCSVFile(string csv_file_path)
    {
        DataTable csvData = new DataTable();
        try
        {
            using (TextFieldParser csvReader = new TextFieldParser(csv_file_path))
            {
                csvReader.SetDelimiters(new string[] { "," });
                csvReader.HasFieldsEnclosedInQuotes = true;
                string[] colFields = csvReader.ReadFields();
                foreach (string column in colFields)
                {
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }
                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();
                    //Making empty value as null
                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }
                    csvData.Rows.Add(fieldData);
                }
            }


        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
        return csvData;
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

    #region btngeneratetemp_Click
    protected void btngeneraltemp_Click(object sender, EventArgs e)
    {
        try
        {
            
            DataTable dt = ClsStockadjustment.BindbulkproductStoreWise(HttpContext.Current.Session["UserID"].ToString());

            Response.ClearContent();
            Response.Buffer = true;
            //string attachment = "attachment; filename=Target_Template_";
            string attachment = "attachment; filename=Bulkjournal_details_";
            string Extension = string.Empty;
            Extension = ".csv";
            attachment = attachment + Extension;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.Charset = "";
            Response.ContentType = "application/text";

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }

            Response.Output.Write(sb.ToString());
            Response.Flush();
            Response.End();

        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    #endregion
}