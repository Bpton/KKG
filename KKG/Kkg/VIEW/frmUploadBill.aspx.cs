 using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Account;
using Obout.Grid;
using System.Drawing;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;


public partial class VIEW_frmUploadBill : System.Web.UI.Page
{
   

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                
                pnlAdd.Style["dispaly"]="";
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
        dtadd.Columns.Add("ACCENTRYID", typeof(string));
        dtadd.Columns.Add("LEDGERID", typeof(string));
        dtadd.Columns.Add("LEDGERNAME", typeof(string));
        dtadd.Columns.Add("INVOICEID", typeof(string));
        dtadd.Columns.Add("INVOICENO", typeof(string));
        dtadd.Columns.Add("INVOICEAMT", typeof(string));
        dtadd.Columns.Add("VOUCHERTYPE", typeof(string));
        dtadd.Columns.Add("BRANCHID", typeof(string));
        dtadd.Columns.Add("INVOICEDATE", typeof(string));
        dtadd.Columns.Add("INVOICEOTHERS", typeof(string));
        dtadd.Columns.Add("TYPE", typeof(string));

        dtadd.Columns.Add("CREDIT", typeof(string));
        dtadd.Columns.Add("DEBIT", typeof(string));
      
        


//ACCENTRYID,LEDGERID,LEDGERNAME,INVOICEID,INVOICENO,INVOICEAMT,VOUCHERTYPE,BRANCHID,
//INVOICEDATE,INVOICEOTHERS,TYPE

        HttpContext.Current.Session["UPLOADBILL"] = dtadd;
    }

    protected void CreateMisGNGLedgernameTable()
    {
        DataTable dtadd = new DataTable();

        dtadd.Columns.Add("BRANCHNAME", typeof(string));
        dtadd.Columns.Add("LEDGERNAME", typeof(string));
        dtadd.Columns.Add("INVOICENO", typeof(string));
        dtadd.Columns.Add("INVOICEDATE", typeof(string));
        dtadd.Columns.Add("VOUCHERTYPE", typeof(string));

        HttpContext.Current.Session["MISMATCHGNGNAMEBILL"] = dtadd;
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
            int flag = 0;
            string xml = string.Empty;
            string TOTALPRODUCTID = string.Empty;
            int RESULT = 0;
            HttpContext.Current.Session["MISMATCHGNGNAMEBILL"] = null;
            string FilenameExtension = Path.GetFileName(FileUpload1.FileName);

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
                    DataTable dtadd = (DataTable)HttpContext.Current.Session["UPLOADBILL"];
                    DataTable dtadd1 = (DataTable)HttpContext.Current.Session["MISMATCHGNGNAMEBILL"];
                    int count = 1;

                    //string finyearid = Clsupload.GetFinyearid(Session["FINYEAR"].ToString().Trim());

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        count = count + 1;
                        Session["COUNT"] = count;
                        if (Convert.ToString(dt.Rows[j]["LEDGERNAME"]) != "")
                        {
                            Session["NAME"] = Convert.ToString(dt.Rows[j]["LEDGERNAME"]).Trim();

                            string CREDIT = Convert.ToString(dt.Rows[j]["CREDIT"]).Trim();
                            string DEBIT = Convert.ToString(dt.Rows[j]["DEBIT"]).Trim();

                            string credit = Convert.ToString(dt.Rows[j]["CREDIT"]).Trim();
                            string debit = Convert.ToString(dt.Rows[j]["DEBIT"]).Trim();
                            if (credit != "" && debit != "")
                            {
                                RESULT = 1;
                                break;
                            }

                            

                            #region INVOICEDATE CHEACK(DD-MM-YY)

                            if (Convert.ToString(dt.Rows[j]["INVOICEDATE"]).Trim() != "")
                            {
                                string dateString = Convert.ToString(dt.Rows[j]["INVOICEDATE"]);
                                flag = ValidateDate(dateString);
                                //if (flag == 0)
                                //{
                                //    RESULT = 3;// insert DATATABLE
                                //}
                                if (flag == 1)
                                {

                                    RESULT = 2; //Date formate should be dd-mm-yyyy!
                                    break;
                                }
                                if (flag == 2)
                                {
                                    RESULT = 2; //Date formate should be dd-mm-yyyy!
                                    break;
                                }
                            }
                            else
                            {
                                Session["DATE"] = "INVOICE DATE";
                                RESULT = 3; 
                                break;
                            }
                            #endregion

                            #region ChequeNo

                            if (Convert.ToString(dt.Rows[j]["INVOICENO"]).Trim() == "")
                            {
                                Session["CHEQUENO"] = "INVOICE NO";
                                RESULT = 4; 
                                break;
                            }
                            #endregion

                        }
                    }

                   
                    #region INSERT DATATABLE
                      if (RESULT == 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if ( Convert.ToString(dt.Rows[i]["LEDGERNAME"]) != "")
                            {

                                DataRow dr = dtadd.NewRow();
                                string prayaasname = Clsupload.GetledgerName(Convert.ToString(dt.Rows[i]["LEDGERNAME"]).Replace("'", "''"));

                                dr["LEDGERNAME"] = Convert.ToString(prayaasname);
                                string guid = Clsupload.Getledgerid(Convert.ToString(dt.Rows[i]["LEDGERNAME"]).Replace("'", "''"),this.ddldepot.SelectedValue.Trim());
                                if (!String.IsNullOrEmpty(guid))
                                {
                                    dr["LEDGERID"] = Convert.ToString(guid);
                                    dr["INVOICEID"] = Convert.ToString(Guid.NewGuid()).ToUpper();
                                    dr["INVOICENO"] = Convert.ToString(dt.Rows[i]["INVOICENO"]).Trim();
                                    dr["INVOICEDATE"] = Convert.ToString(dt.Rows[i]["INVOICEDATE"]).Trim().Replace('-', '/');
                                    dr["VOUCHERTYPE"] = this.ddlvoucher.SelectedValue.Trim();/*9 & 10*/
                                    if (Convert.ToString(dt.Rows[i]["CREDIT"]).Trim() != "")
                                    {
                                        dr["INVOICEAMT"] = Convert.ToString(dt.Rows[i]["CREDIT"]).Trim();
                                        dr["TYPE"] = "Cr";
                                    }
                                    else
                                    {
                                        dr["INVOICEAMT"] = Convert.ToString(dt.Rows[i]["DEBIT"]).Trim();
                                        dr["TYPE"] = "Dr";
                                    }
                                    dr["INVOICEOTHERS"] = Convert.ToString(dt.Rows[i]["INVOICEOTHERS"]).Trim();
                                    dtadd.Rows.Add(dr);
                                    dtadd.AcceptChanges();
                                }
                                else
                                {
                                    DataRow dr1 = dtadd1.NewRow();
                                    dr1["BRANCHNAME"] = this.ddldepot.SelectedItem.Text.Trim();
                                    dr1["LEDGERNAME"] = Convert.ToString(dt.Rows[i]["LEDGERNAME"]).Trim();
                                    dr1["INVOICENO"] = Convert.ToString(dt.Rows[i]["INVOICENO"]).Trim();
                                    dr1["INVOICEDATE"] = Convert.ToString(dt.Rows[i]["INVOICEDATE"]).Trim().Replace('-', '/');
                                    dr1["VOUCHERTYPE"] = this.ddlvoucher.SelectedItem.Text.Trim(); ;

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
                    else if (RESULT == 2)
                    {

                        MessageBox1.ShowWarning(" <b><font color='red'>" + Session["DATEFORMAT"] + "  </font></b>  formate should be dd-mm-yyyy or dd/mm/yyyy of    <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                        Session["COUNT"] = null;
                        Session["DATEFORMAT"] = null;
                        return;
                    }
                    else if (RESULT == 3)
                    {

                        MessageBox1.ShowWarning("<b><font color='red'>" + Session["DATE"] + "  </font></b> is required of    <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                        Session["COUNT"] = null;
                        Session["DATE"] = null;
                        return;
                    }
                    else if (RESULT == 4)
                    {

                        MessageBox1.ShowWarning("<b><font color='red'>" + Session["CHEQUENO"] + "</font></b> is required of    <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                        Session["COUNT"] = null;
                        Session["CHEQUENO"] = null;
                        return;
                    }
                    else if (RESULT == 6)
                    {

                        MessageBox1.ShowError("Because Of Having Mismatch LedgerName");
                        RESULT = 0;


                        HttpContext.Current.Session["MISMATCHGNGNAMEBILL"] = dtadd1;
                        if (dtadd1.Rows.Count > 0)
                        {


                            string attachment = "attachment; filename=MisMatchInvoiceLedger_" + this.ddldepot.SelectedItem.Text + ".xls";
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

                        HttpContext.Current.Session["UPLOADBILL"] = dtadd;
                        xml = ConvertDatatableToXML(dtadd);
                        id = Clsupload.SaveBill(this.ddldepot.SelectedValue.Trim(), xml);
                        if (id > 0)
                        {
                            MessageBox1.ShowSuccess("Save successfully");
                            HttpContext.Current.Session["UPLOADBILL"] = null;
                            RESULT = 0;
                           //this.ddldepot.SelectedValue = "0";

                           Session["COUNT"] = null;

                        }
                        else
                        {
                            MessageBox1.ShowWarning("Error on Saving record..");
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

    protected void btnrest_Click(object sender,EventArgs e)
    {
        this.ddldepot.SelectedValue = "0";
        FileUpload1.Attributes.Clear();
        HttpContext.Current.Session["MISMATCHGNGNAMEBILL"] = null;
        HttpContext.Current.Session["UPLOADBILL"] = null;
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
                //DataTable dt = Clsupload.CreateTempOfDepotWiseCustomer(this.ddldepot.SelectedValue.Trim());

                DataTable dt = Clsupload.CreateTempOfUploadBILL();
                Response.ClearContent();
                Response.Buffer = true;
                string attachment = "attachment; filename=Bill.xls";
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