using Account;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class VIEW_frmUploadOpeningBalance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlAdd.Style["dispaly"] = "";
                DateTime dtcurr = DateTime.Now;
                string date = "dd/MM/yyyy";

                this.LoadDepot();
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    protected void CreateOpeningTable()
    {
        DataTable dtadd = new DataTable();
        dtadd.Columns.Add("ID", typeof(string));
        dtadd.Columns.Add("LEDGERNAME", typeof(string));
        dtadd.Columns.Add("CREDIT", typeof(string));
        dtadd.Columns.Add("DEBIT", typeof(string));
        dtadd.Columns.Add("TOTALAMT", typeof(string));

        dtadd.Columns.Add("BALANCETYPE", typeof(string));

        HttpContext.Current.Session["UPLOADOPENINGBALANCE"] = dtadd;
    }

    protected void CreateMisGNGLedgernameTable()
    {
        DataTable dtadd = new DataTable();

        dtadd.Columns.Add("LEDGERNAME", typeof(string));
        dtadd.Columns.Add("BRANCHNAME", typeof(string));
        dtadd.Columns.Add("CREDIT", typeof(string));
        dtadd.Columns.Add("DEBIT", typeof(string));
        dtadd.Columns.Add("TOTALAMT", typeof(string));

        dtadd.Columns.Add("BALANCETYPE", typeof(string));

        HttpContext.Current.Session["MISMATCHGNGNAMEOB"] = dtadd;
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



    #region Conver_To_ISO
    public string Conver_To_ISO(string dt)
    {

        string strOpenDate = dt;
        string day = strOpenDate.Substring(0, strOpenDate.IndexOf("/"));
        string month = strOpenDate.Substring(strOpenDate.IndexOf("/"));
        month = month.Substring(1, month.Length - 1);
        string year = month.Substring(month.IndexOf("/"));
        month = month.Substring(0, month.IndexOf("/"));
        year = year.Substring(1, year.Length - 1);
        dt = year + month + day;
        return dt;

    }
    #endregion


    private void LoadDepot()
    {
        ClsUploadOpeningBalance Clsupload = new ClsUploadOpeningBalance();
        DataTable dt = Clsupload.BindDepot(HttpContext.Current.Session["UserID"].ToString().Trim());
        if (dt.Rows.Count > 0)
        {
            this.ddldepot.Items.Clear();
            this.ddldepot.Items.Insert(0, new ListItem("Select", "0"));
            this.ddldepot.DataSource = dt;
            this.ddldepot.DataTextField = "BRNAME";
            this.ddldepot.DataValueField = "BRID";
            this.ddldepot.DataBind();


            if (dt.Rows.Count == 1)
            {
                this.ddldepot.SelectedValue = dt.Rows[0]["BRID"].ToString().Trim();
                this.ddldepot.Enabled = false;
            }


        }
        else
        {
            this.ddldepot.Items.Clear();
            this.ddldepot.Items.Insert(0, new ListItem("Select", "0"));
            this.ddldepot.Enabled = true;
        }
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            ClsUploadOpeningBalance Clsupload = new ClsUploadOpeningBalance();
            int id = 0;
            string xml = string.Empty;
            string TOTALPRODUCTID = string.Empty;
            int RESULT = 0;
            string unchkprayaasname = string.Empty;
            string FilenameExtension = Path.GetFileName(FileUpload1.FileName);
            HttpContext.Current.Session["MISMATCHGNGNAMEOB"] = null;

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
                    CreateOpeningTable();
                    CreateMisGNGLedgernameTable();
                    DataTable dtadd = (DataTable)HttpContext.Current.Session["UPLOADOPENINGBALANCE"];
                    DataTable dtadd1 = (DataTable)HttpContext.Current.Session["MISMATCHGNGNAMEOB"];

                    int count = 1;

                    string finyearid = Clsupload.GetFinyearid(Session["FINYEAR"].ToString().Trim());

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        count = count + 1;
                        Session["COUNT"] = count;
                        if (Convert.ToString(dt.Rows[j]["CREDIT"]) != "" || Convert.ToString(dt.Rows[j]["DEBIT"]) != "")
                        {
                            Session["NAME"] = Convert.ToString(dt.Rows[j]["LEDGERNAME"]).Trim();

                            string credit = Convert.ToString(dt.Rows[j]["CREDIT"]).Trim();
                            string debit = Convert.ToString(dt.Rows[j]["DEBIT"]).Trim();
                            if (credit != "" && debit != "")
                            {
                                RESULT = 1;
                                break;
                            }

                        }


                    }


                    #region INSERT DATATABLE
                    if (RESULT == 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt.Rows[i]["CREDIT"]) != "" || Convert.ToString(dt.Rows[i]["DEBIT"]) != "")
                            {

                                DataRow dr = dtadd.NewRow();
                                string prayaasname = Clsupload.GetledgerName(Convert.ToString(dt.Rows[i]["LEDGERNAME"]));
                                dr["LEDGERNAME"] = Convert.ToString(prayaasname);
                                string guid = Clsupload.Getledgerid(Convert.ToString(dt.Rows[i]["LEDGERNAME"]), this.ddldepot.SelectedValue.Trim());


                                if (!String.IsNullOrEmpty(guid))
                                {
                                    dr["ID"] = Convert.ToString(guid);


                                    if (Convert.ToString(dt.Rows[i]["CREDIT"]) != "")
                                    {
                                        dr["BALANCETYPE"] = "C";
                                        dr["TOTALAMT"] = Convert.ToString(dt.Rows[i]["CREDIT"]).Trim();
                                    }
                                    else
                                    {
                                        dr["BALANCETYPE"] = "D";
                                        dr["TOTALAMT"] = Convert.ToString(dt.Rows[i]["DEBIT"]).Trim();
                                    }


                                    dtadd.Rows.Add(dr);
                                    dtadd.AcceptChanges();
                                }
                                else
                                {
                                    DataRow dr1 = dtadd1.NewRow();
                                    dr1["BRANCHNAME"] = this.ddldepot.SelectedItem.Text.Trim();
                                    dr1["LEDGERNAME"] = Convert.ToString(dt.Rows[i]["LEDGERNAME"]).Trim();

                                    if (Convert.ToString(dt.Rows[i]["CREDIT"]) != "")
                                    {
                                        dr1["BALANCETYPE"] = "C";
                                        dr1["TOTALAMT"] = Convert.ToString(dt.Rows[i]["CREDIT"]).Trim();
                                    }
                                    else
                                    {
                                        dr1["BALANCETYPE"] = "D";
                                        dr1["TOTALAMT"] = Convert.ToString(dt.Rows[i]["DEBIT"]).Trim();
                                    }

                                    dtadd1.Rows.Add(dr1);
                                    dtadd1.AcceptChanges();
                                    RESULT = 6;
                                }
                            }
                        }
                    }

                    #endregion

                    #region FINAL SAVE
                    if (RESULT == 1)
                    {

                        MessageBox1.ShowWarning("Please put a value either credit or debit cell of  <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["COUNT"] + " </font></b>!", 60, 500);
                        Session["COUNT"] = null;
                        return;
                    }
                    //if (RESULT == 2)
                    //{

                    //    MessageBox1.ShowWarning("<b><font color='red'> " + Session["NAME"] + " </font></b> already exit in  Finance year in ROW NO:<b><font color='red'> " + Session["COUNT"] + " </font></b>!", 60, 500);
                    //    Session["COUNT"] = null;
                    //    return;
                    //}

                    else if (RESULT == 6)
                    {
                        // MessageBox1.ShowError("Because Of Having Mismatch LedgerName");
                        RESULT = 0;
                        FileUpload1.Attributes.Clear();
                        // lblmgs.Text = "Because Of Having Mismatch LedgerName";
                        HttpContext.Current.Session["MISMATCHGNGNAMEOB"] = dtadd1;
                        if (dtadd1.Rows.Count > 0)
                        {
                            //CreateLogFiles Errlog = new CreateLogFiles();
                            //Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", " Path :Page_Load()");

                            //StreamWriter sw = new StreamWriter(HttpContext.Current.Request.PhysicalApplicationPath + @"FileUpload\MisMatchOpeningBalanceLedger_" + this.ddldepot.SelectedItem.Text + ".xls", false);

                            //sw.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                            //sw.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                            //sw.Write("<BR><BR><BR>");
                            //sw.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
                            //int columnscount = dtadd1.Columns.Count;

                            //for (int j = 0; j < columnscount; j++)
                            //{
                            //    sw.Write("<Td>");
                            //    sw.Write("<B>");
                            //    sw.Write(dtadd1.Columns[j].ToString());
                            //    sw.Write("</B>");
                            //    sw.Write("</Td>");
                            //}
                            //sw.Write("</TR>");
                            //foreach (DataRow row in dtadd1.Rows)
                            //{
                            //    sw.Write("<TR>");
                            //    for (int i = 0; i < dtadd1.Columns.Count; i++)
                            //    {
                            //        sw.Write("<Td>");
                            //        sw.Write(row[i].ToString());
                            //        sw.Write("</Td>");
                            //    }
                            //    sw.Write("</TR>");
                            //}
                            //sw.Write("</Table>");
                            //sw.Write("</font>");
                            //sw.Close();

                            string attachment = "attachment; filename=MisMatchLedger_" + this.ddldepot.SelectedItem.Text + ".xls";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", attachment);
                            Response.ContentType = "application/vnd.ms-excel";
                            string tab = "";
                            foreach (DataColumn dc in dtadd1.Columns)
                            {
                                Response.Write(tab + dc.ColumnName);
                                tab = "\t";
                            }
                            Response.Write("\n");
                            int i;
                            foreach (DataRow dr in dtadd1.Rows)
                            {
                                tab = "";
                                for (i = 0; i < dtadd1.Columns.Count; i++)
                                {
                                    Response.Write(tab + dr[i].ToString());
                                    tab = "\t";
                                }

                                Response.Write("\n");
                            }

                            Response.End();

                        }



                    }
                    else
                    {

                        HttpContext.Current.Session["UPLOADOPENINGBALANCE"] = dtadd;

                        xml = ConvertDatatableToXML(dtadd);
                        id = Clsupload.SaveCustomerOpeningBalance(this.ddldepot.SelectedValue.Trim(), Session["FINYEAR"].ToString().Trim(), xml);

                        if (id > 0)
                        {
                            MessageBox1.ShowSuccess("Opening Balance inserted successfully ");
                            //this.lbluploadstatus.Text = "Opening Balance inserted successfully";
                            HttpContext.Current.Session["UPLOADOPENINGBALANCE"] = null;
                            RESULT = 0;


                            Session["COUNT"] = null;
                        }
                        else
                        {
                            MessageBox1.ShowError("Error on Saving record..");
                            //this.lbluploadstatus.Text = "Error on Saving record..";
                            RESULT = 0;

                            Session["COUNT"] = null;
                        }



                        FileUpload1.Attributes.Clear();
                        this.ddldepot.SelectedValue = "0";


                    }
                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnrest_Click(object sender, EventArgs e)
    {
        this.ddldepot.SelectedValue = "0";
        FileUpload1.Attributes.Clear();
        Session.Remove("UPLOADOPENINGBALANCE");
    }

    #region GetDataTabletFromCSVFile
    public DataTable ConvertCSVtoDataTable(string csv_file_path)
    {
        DataTable csvData = new DataTable();
        try
        {
            using (TextFieldParser csvReader = new TextFieldParser(csv_file_path.Trim()))
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btngeneraltemp_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.ddldepot.SelectedValue == "0")
            {
                MessageBox1.ShowInfo("Please Select Depot...");
                return;
            }
            else
            {
                ClsUploadOpeningBalance Clsupload = new ClsUploadOpeningBalance();
                DataTable dt = Clsupload.CreateTempOfDepotWiseCustomer(this.ddldepot.SelectedValue.Trim());
                Response.ClearContent();
                Response.Buffer = true;
                string attachment = "attachment; filename=OpeningBalance.xls";
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
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}