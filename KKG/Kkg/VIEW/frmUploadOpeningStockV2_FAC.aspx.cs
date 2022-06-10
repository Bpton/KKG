using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Utility;

public partial class VIEW_frmUploadOpeningStockV2_FAC : System.Web.UI.Page
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
                this.txtopeningdate.Text = dtcurr.ToString(date).Replace('-', '/');
                this.LoadStoreloacation();
                /*Calender Control Date Range*/
                CalendarExtender1.EndDate = DateTime.Now;
                /****************************/
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
        dtadd.Columns.Add("TYPE", typeof(string));
        dtadd.Columns.Add("BRAND_PRIMARYNAME", typeof(string));
        dtadd.Columns.Add("CATEGORY_SUBNAME", typeof(string));
        dtadd.Columns.Add("CODE", typeof(string));
        dtadd.Columns.Add("PRODUCTNAME", typeof(string));
        dtadd.Columns.Add("PACKSIZENAME", typeof(string));
        dtadd.Columns.Add("QTY", typeof(string));
        dtadd.Columns.Add("MRP", typeof(string));
        dtadd.Columns.Add("RATE", typeof(string));
        dtadd.Columns.Add("BATCHNO", typeof(string));
        dtadd.Columns.Add("MFGDATE", typeof(string));
        dtadd.Columns.Add("EXPDATE", typeof(string));
        dtadd.Columns.Add("ASSESMENTPERCENTAGE", typeof(string));
        dtadd.Columns.Add("STORELOCATIONID", typeof(string));
        dtadd.Columns.Add("STORELOCATIONNAME", typeof(string));
        dtadd.Columns.Add("UOMID", typeof(string));

        HttpContext.Current.Session["OPENINGSTOCK"] = dtadd;
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

    public void LoadStoreloacation()
    {
        ClsOpenStock clsopenstock = new ClsOpenStock();
        DataTable dt = clsopenstock.BindUploadStorelocation();
        if (dt.Rows.Count > 0)
        {
            ddlstorelocation.Items.Clear();
            ddlstorelocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
            ddlstorelocation.AppendDataBoundItems = true;
            ddlstorelocation.DataSource = dt;
            ddlstorelocation.DataValueField = "STOREID";
            ddlstorelocation.DataTextField = "STORENAME";
            ddlstorelocation.DataBind();
            if(dt.Rows.Count==1)
            {
                this.ddlstorelocation.SelectedValue = Convert.ToString(dt.Rows[0]["STOREID"]);
            }
        }
        else
        {
            ddlstorelocation.Items.Clear();
            ddlstorelocation.Items.Add(new ListItem("SELECT STORE LOCATION", "0"));
        }
    }

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

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string rowNumber = "0";
        try
        {
           
            ClsEntryLock objLock = new ClsEntryLock();
            bool ObjDate = objLock.EntryLock(this.txtopeningdate.Text.Trim(), Convert.ToString(HttpContext.Current.Session["FINYEAR"]).Trim());
            if (ObjDate == true)
            {
                ClsOpenStock clsstock = new ClsOpenStock();
                int id = 0;
                string xml = string.Empty;
                  string produtname = string.Empty;
                string TOTALPRODUCTID = string.Empty;
                int RESULT = 0;

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
                        DataTable dtadd = (DataTable)HttpContext.Current.Session["OPENINGSTOCK"];

                        int flag = 1;
                        int count = 1;
                        #region INSERT DATATABLE
                        
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                rowNumber = Convert.ToString(i);
                                if (Convert.ToString(dt.Rows[i]["QTY"]) != "0" && Convert.ToString(dt.Rows[i]["MRP"]) != "")
                                {
                                if (Convert.ToString(dt.Rows[i]["PACKSIZENAME"]) == "")
                                {
                                    produtname=Convert.ToString(dt.Rows[i]["PRODUCTNAME"]);
                                    RESULT = 3;
                                    break;
                                }
                                
                                    DataRow dr = dtadd.NewRow();
                                    string UOMID = clsstock.GETUOMID(Convert.ToString(dt.Rows[i]["ID"]).ToUpper());
                                    dr["ID"] = Convert.ToString(dt.Rows[i]["ID"]);
                                    dr["PRODUCTNAME"] = Convert.ToString(dt.Rows[i]["PRODUCTNAME"]);
                                    dr["QTY"] = Convert.ToDecimal(dt.Rows[i]["QTY"]);
                                    dr["BATCHNO"] = "NA";
                                    dr["MRP"] = Convert.ToString(dt.Rows[i]["MRP"]);
                                    dr["RATE"] = Convert.ToString(dt.Rows[i]["RATE"]);
                                    dr["MFGDATE"] = "";
                                    dr["EXPDATE"] = "";
                                    dr["ASSESMENTPERCENTAGE"] = "0";
                                    dr["STORELOCATIONID"] = Convert.ToString(this.ddlstorelocation.SelectedValue.Trim());
                                    dr["STORELOCATIONNAME"] = Convert.ToString(this.ddlstorelocation.SelectedItem.Text.Trim());
                                    dr["PACKSIZENAME"] = Convert.ToString(dt.Rows[i]["PACKSIZENAME"]).ToUpper();
                                    dr["UOMID"] = Convert.ToString(UOMID);
                                    dtadd.Rows.Add(dr);
                                    dtadd.AcceptChanges();
                                }
                            }
                       

                        #endregion

                        #region FINAL SAVE
                        if (RESULT == 3)
                        {
                            MessageBox1.ShowWarning("packsize cannot be blank for this product:"+produtname);
                            return;
                        }
                        if (RESULT == 2)
                        {
                            MessageBox1.ShowWarning("Field should not be blank in uploaded file!");
                            return;
                        }
                        
                        else if (RESULT == 8)
                        {
                            //MessageBox1.ShowWarning("Product <b><font color='red'> " + Session["NAME"] + " </font></b> SHOULD BE UOM PROPER <b><font color='green'> " + Session["UOMNAME"] + " </font></b> FORMAT. in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>  ", 50, 750);
                            MessageBox1.ShowWarning(" Mismatch UOM <b><font color='red'> " + Session["UOMNAME"] + " </font></b> Corresponding to Product <b><font color='red'> " + Session["NAME"] + " </font></b> It will be <b><font color='green'> " + Session["CHKUOMNAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 50, 700);
                            Session["count"] = null;
                            return;
                        }

                        else
                        {
                            HttpContext.Current.Session["OPENINGSTOCK"] = dtadd;
                            xml = ConvertDatatableToXML(dtadd);
                            id = clsstock.SaveUploadOpeningstock(Session["FINYEAR"].ToString(), Session["DEPOTID"].ToString().Trim(), xml, this.txtopeningdate.Text.Trim(), TOTALPRODUCTID);
                            if (id > 0)
                            {
                                MessageBox1.ShowSuccess("Opening stock inserted successfully");
                                HttpContext.Current.Session["OPENINGSTOCK"] = null;
                                RESULT = 0;
                                this.ddlstorelocation.SelectedValue = "0";
                                flag = 1;
                                Session["NAME"] = null;
                                Session["MFGDTE"] = null;
                                Session["EXPDATE"] = null;
                                Session["count"] = null;
                                Session["UOMNAME"] = null;
                                Session["CHKUOMNAME"] = null;
                            }
                            else
                            {
                                MessageBox1.ShowWarning("Error on Saving record..");
                                RESULT = 0;
                                Session["NAME"] = null;
                                Session["MFGDTE"] = null;
                                Session["EXPDATE"] = null;
                                Session["count"] = null;
                                Session["UOMNAME"] = null;
                                Session["CHKUOMNAME"] = null;
                            }
                        }
                        #endregion
                    }
                }
            }
            else
            {
                MessageBox1.ShowInfo("Entry Date is Locked, Please Contact to Admin", 60, 500);
            }
        }
        catch (ArgumentNullException ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "")  +" Please Check RowNumber : "+ rowNumber + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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
            if(this.ddlstorelocation.SelectedValue=="0")
            {
                MessageBox1.ShowWarning("Please Select Storelocation");
                return;
            }
            ClsProductMaster clsproduct = new ClsProductMaster();
            DataTable dt = clsproduct.BindGenerateTemp(this.ddlstorelocation.SelectedValue);
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