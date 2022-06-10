using BAL;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class VIEW_frmCustomerUpload : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        FileUpload1.Attributes.Add("onchange", "SetFileName('" + FileUpload1.ClientID + "','" + txtWayBill.ClientID + "')");
        try
        {
            if (!IsPostBack)
            {
                LoadBsSegment();
                LoadCountry();
                LoadState();
                td_export.Style["display"] = "none";
                td_state.Style["display"] = "";

            }

        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadBsSegment
    public void LoadBsSegment()
    {
        try
        {
            ClsCustomerMaster clscustomer = new ClsCustomerMaster();
            DataTable dt = new DataTable();
            dt = clscustomer.BindBusinessSegmentForUploadCustomer();
            ddlsegment.Items.Clear();
            ddlsegment.Items.Insert(0, new ListItem("Select", "0"));
            ddlsegment.DataSource = dt;
            ddlsegment.DataTextField = "BSNAME";
            ddlsegment.DataValueField = "BSID";
            ddlsegment.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region ddlsegment_SelectedIndexChanged
    protected void ddlsegment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadGroup(ddlsegment.SelectedValue.Trim());
            LoadAccGroup(ddlsegment.SelectedValue.Trim());
            if (ddlsegment.SelectedValue == "2E96A0A4-6256-472C-BE4F-C59599C948B0")  // EXPORTID
            {
                td_export.Style["display"] = "";
                td_state.Style["display"] = "none";
            }
            else
            {
                td_export.Style["display"] = "none";
                td_state.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadGroup
    public void LoadGroup(string BSID)
    {
        try
        {
            ClsCustomerMaster clscustomer = new ClsCustomerMaster();
            DataTable dt = new DataTable();
            dt = clscustomer.GetGroupID(BSID);
            ddlgroup.Items.Clear();
            ddlgroup.Items.Insert(0, new ListItem("Select", "0"));
            ddlgroup.DataSource = dt;
            ddlgroup.DataTextField = "DIS_CATNAME";
            ddlgroup.DataValueField = "DIS_CATID";
            ddlgroup.DataBind();
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadCurrency
    public void LoadCurrency(string GroupID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dtCurrency = new DataTable();
            dtCurrency = ClsCustomer.BindCurrency(GroupID);
            if (dtCurrency.Rows.Count > 0)
            {
                this.ddlcurrency.Items.Clear();
                this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
                this.ddlcurrency.DataSource = dtCurrency;
                this.ddlcurrency.DataTextField = "CURRENCYNAME";
                this.ddlcurrency.DataValueField = "CURRENCYID";
                this.ddlcurrency.DataBind();

                if (dtCurrency.Rows.Count == 1)
                {
                    this.ddlcurrency.SelectedValue = Convert.ToString(dtCurrency.Rows[0]["CURRENCYID"]).Trim();
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ddlgroup_SelectedIndexChanged
    protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadCurrency(ddlgroup.SelectedValue.Trim());
            //if (ddlsegment.SelectedValue == "2E96A0A4-6256-472C-BE4F-C59599C948B0")  // EXPORTID
            //{
            //    td_export.Style["display"] = "";
            //    td_state.Style["display"] = "none";
            //}
            //else
            //{
            //    td_export.Style["display"] = "none";
            //    td_state.Style["display"] = "";
            //}
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region LoadCountry
    public void LoadCountry()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlcountry.Items.Clear();
            ddlcountry.Items.Insert(0, new ListItem("Select Country", "0"));
            ddlcountry.DataSource = ClsCustomer.BindCountry();
            ddlcountry.DataTextField = "COUNTRYNAME";
            ddlcountry.DataValueField = "COUNTRYID";
            ddlcountry.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion    

    #region LoadState
    public void LoadState()
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            ddlstate.Items.Clear();
            ddlstate.Items.Insert(0, new ListItem("Select State", "0"));
            ddlstate.DataSource = ClsCustomer.BindState();
            ddlstate.DataTextField = "State_Name";
            ddlstate.DataValueField = "State_ID";
            ddlstate.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region LoadAccGroup
    public void LoadAccGroup(string BSID)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
            DataTable dt = ClsCustomer.GetLedgerbyBSID(BSID);
            ddlaccgroup.Items.Clear();
            ddlaccgroup.Items.Insert(0, new ListItem("Select Group", "0"));
            ddlaccgroup.DataSource = dt;
            ddlaccgroup.DataTextField = "GRPNAME";
            ddlaccgroup.DataValueField = "GROUPID";
            ddlaccgroup.DataBind();
            if (dt.Rows.Count == 1)
            {
                this.ddlaccgroup.SelectedValue = Convert.ToString(dt.Rows[0]["GROUPID"]).Trim();
                ddlaccgroup.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    #region CreateDataTableStructure
    protected void CreateDataTableStructure()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("CUSTOMERID", typeof(string));
        dt.Columns.Add("CODE", typeof(string));
        dt.Columns.Add("CUSTOMERNAME", typeof(string));
        dt.Columns.Add("CUSTYPE_ID", typeof(string));
        dt.Columns.Add("CUSTYPE_NAME", typeof(string));
        dt.Columns.Add("BUSINESSSEGMENTID", typeof(string));
        dt.Columns.Add("BUSINESSSEGMENTNAME", typeof(string));
        dt.Columns.Add("GROUPID", typeof(string));
        dt.Columns.Add("GROUPNAME", typeof(string));
        dt.Columns.Add("PARENT_CUST_ID", typeof(string));
        dt.Columns.Add("PARENT_CUST_NAME", typeof(string));
        dt.Columns.Add("PANCARDNO", typeof(string));
        dt.Columns.Add("GSTNO", typeof(string));/***GSTNO***/
        dt.Columns.Add("CSTNO", typeof(string));
        dt.Columns.Add("TINNO", typeof(string));
        dt.Columns.Add("CONTACTPERSON1", typeof(string));
        dt.Columns.Add("CONTACTPERSON2", typeof(string));
        dt.Columns.Add("EMAILID1", typeof(string));
        dt.Columns.Add("EMAILID2", typeof(string));
        dt.Columns.Add("MOBILE1", typeof(string));
        dt.Columns.Add("TELEPHONE1", typeof(string));
        dt.Columns.Add("MOBILE2", typeof(string));
        dt.Columns.Add("TELEPHONE2", typeof(string));
        dt.Columns.Add("ADDRESS", typeof(string));
        dt.Columns.Add("STATEID", typeof(string));
        dt.Columns.Add("STATENAME", typeof(string));
        dt.Columns.Add("DISTRICTID", typeof(string));
        dt.Columns.Add("DISTRICTNAME", typeof(string));
        dt.Columns.Add("CITYID", typeof(string));
        dt.Columns.Add("CITYNAME", typeof(string));
        dt.Columns.Add("PIN", typeof(string));
        dt.Columns.Add("ALTERNATEADDRESS", typeof(string));
        dt.Columns.Add("ALTERNATEPIN", typeof(string));
        dt.Columns.Add("CBU", typeof(string));
        dt.Columns.Add("DTOC", typeof(string));
        dt.Columns.Add("LMBU", typeof(string));
        dt.Columns.Add("LDTOM", typeof(string));
        dt.Columns.Add("STATUS", typeof(string));
        dt.Columns.Add("ISAPPROVED", typeof(string));
        dt.Columns.Add("ISACTIVE", typeof(string));
        dt.Columns.Add("REPORTTOROLEID", typeof(string));
        dt.Columns.Add("PERCENTAGE", typeof(string));
        dt.Columns.Add("SHOPID", typeof(string));
        dt.Columns.Add("SHOPNAME", typeof(string));
        dt.Columns.Add("LATITUDE", typeof(string));
        dt.Columns.Add("LONGITUDE", typeof(string));
        dt.Columns.Add("RETAILER_CAT_ID", typeof(string));
        dt.Columns.Add("RETAILER_CAT_NAME", typeof(string));
        dt.Columns.Add("AMOUNT", typeof(string));
        dt.Columns.Add("CURRENCYID", typeof(string));
        dt.Columns.Add("CURRENCYNAME", typeof(string));
        dt.Columns.Add("ACCGROUPID", typeof(string));
        dt.Columns.Add("ACCGROUPNAME", typeof(string));
        dt.Columns.Add("FILEID", typeof(string));
        dt.Columns.Add("FILENAME", typeof(string));
        dt.Columns.Add("FILEEXIST", typeof(string));
        dt.Columns.Add("COUNTRYID", typeof(string));
        dt.Columns.Add("COUNTRYNAME", typeof(string));
        dt.Columns.Add("SHORTNAME", typeof(string));
        dt.Columns.Add("VATLIC", typeof(string));
        dt.Columns.Add("IMAGE", typeof(string));
        dt.Columns.Add("USERNAME", typeof(string));

        HttpContext.Current.Session["DATAUPLOAD"] = dt;
    }
    #endregion

    protected DataTable CreateLedgerTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("ACCOUNTID", typeof(string)));
        dt.Columns.Add(new DataColumn("DEBITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("CREDITAMOUNT", typeof(string)));
        dt.Columns.Add(new DataColumn("OPENBALANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("BALANCETYPE", typeof(string)));
        dt.Columns.Add(new DataColumn("REGIONID", typeof(string)));


        HttpContext.Current.Session["LEDGERDETAILS"] = dt;

        return dt;
    }

    protected DataTable CreateLedgerInfoTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("CUSTOMERID", typeof(string)));
        dt.Columns.Add(new DataColumn("CUSTOMERNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("ACTGRPCODE", typeof(string)));
        HttpContext.Current.Session["LEDGERDETAILSINFO"] = dt;
        return dt;
    }

    #region btnUpload_Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            int retresult = 0;
            int result = 0;
            ClsCustomerMaster clscustomer = new ClsCustomerMaster();

            
            string FilenameExtension = Path.GetFileName(FileUpload1.FileName);

            if (FilenameExtension == "")
            {
                MessageBox1.ShowWarning("Please Uploaded CSV file");
                return;
            }
            else
            {
                string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
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
                    //DataTable dtregion = clscustomer.GetAddress(HttpContext.Current.Session["USERID"].ToString().Trim());
                    DataTable dt = GetDataTabletFromCSVFile(currentPath);
                    if (dt.Rows.Count > 0)
                    {
                        string ID = "";
                        CreateDataTableStructure();
                        if (HttpContext.Current.Session["DATAUPLOAD"] == null)
                        {
                            CreateDataTableStructure();
                        }

                        if (HttpContext.Current.Session["LEDGERDETAILS"] == null)
                        {
                            this.CreateLedgerTable();
                        }

                        if (HttpContext.Current.Session["LEDGERDETAILSINFO"] == null)
                        {
                            this.CreateLedgerInfoTable();
                        }

                        DataTable dtadd = (DataTable)HttpContext.Current.Session["DATAUPLOAD"];

                        DataTable dtledger = (DataTable)HttpContext.Current.Session["LEDGERDETAILS"];

                        DataTable dtledgerInfo = (DataTable)HttpContext.Current.Session["LEDGERDETAILSINFO"];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]) == "")
                            {
                                retresult = 1;
                                break;
                            }
                             
                           // DataTable dtcustomer = clscustomer.customercheck(dtadd.Rows);
                            //if (Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]) == Convert.ToString(dtcustomer.Rows[i]["CUSTOMERNAME"]))
                            //{
                            //    retresult = 10;
                            //    break;
                            //}
                            
                            if (Convert.ToString(dt.Rows[i]["ADDRESS"]) == "")
                            {
                                retresult = 2;
                                break;
                            }
                            //if(Convert.ToString(dt.Rows[i]["PIN"]) == "")
                            //{
                            //    retresult = 3;
                            //    break;
                            //}
                            //if(Convert.ToString(dt.Rows[i]["SHORTNAME"]) == "")
                            //{
                            //    retresult = 4;
                            //    break;
                            //}
                            //if (Convert.ToString(dt.Rows[i]["DISTRICTNAME"]) == "")
                            //{
                            //    retresult = 5;
                            //    break;
                            //}
                            //if (Convert.ToString(dt.Rows[i]["CITYNAME"]) == "")
                            //{
                            //    retresult = 6;
                            //    break;
                            //}

                            if (Convert.ToString(dt.Rows[i]["PANCARDNO"]) == "")
                            {
                                if (Convert.ToString(dt.Rows[i]["GSTNO"]) != "")
                                {
                                    retresult = 3;
                                    break;
                                }
                               
                            }
                            else
                            {
                                string ShortName = string.Empty;
                                bool SpaceExists;
                                SpaceExists = Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).Trim().Contains(" ");
                                if (SpaceExists == false)
                                {
                                    ShortName = Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).Trim();
                                }
                                else
                                {

                                    ShortName = Convert.ToString(Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).Substring(0, Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).IndexOf(" "))).ToString().Trim();
                                }
                                ID = Guid.NewGuid().ToString();

                                DataRow dr = dtadd.NewRow();
                                dr["CUSTOMERID"] = ID;
                                dr["CODE"] = Convert.ToString(dt.Rows[i]["CODE"]).Trim();
                                dr["CUSTOMERNAME"] = Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]).Trim();
                                dr["CUSTYPE_ID"] = "";
                                dr["CUSTYPE_NAME"] = "";
                                dr["BUSINESSSEGMENTID"] = "";
                                dr["BUSINESSSEGMENTNAME"] = "";
                                dr["GROUPID"] = "";
                                dr["GROUPNAME"] = "";
                                dr["PARENT_CUST_ID"] = HttpContext.Current.Session["USERID"].ToString().Trim();
                                dr["PARENT_CUST_NAME"] = HttpContext.Current.Session["USERNAME"].ToString().Trim();
                                dr["PANCARDNO"] = Convert.ToString(dt.Rows[i]["PANCARDNO"]).Trim();
                                dr["GSTNO"] = Convert.ToString(dt.Rows[i]["GSTNO"]).Trim();
                                dr["CSTNO"] = Convert.ToString(dt.Rows[i]["CSTNO"]).Trim();
                                dr["TINNO"] = Convert.ToString(dt.Rows[i]["TINNO"]).Trim();
                                dr["CONTACTPERSON1"] = Convert.ToString(dt.Rows[i]["CONTACTPERSON1"]).Trim();
                                dr["CONTACTPERSON2"] = Convert.ToString(dt.Rows[i]["CONTACTPERSON2"]).Trim();
                                dr["EMAILID1"] = Convert.ToString(dt.Rows[i]["EMAILID1"]).Trim();
                                dr["EMAILID2"] = Convert.ToString(dt.Rows[i]["EMAILID2"]).Trim();
                                dr["MOBILE1"] = Convert.ToString(dt.Rows[i]["MOBILE1"]).Trim();
                                dr["TELEPHONE1"] = Convert.ToString(dt.Rows[i]["TELEPHONE1"]).Trim();
                                dr["MOBILE2"] = Convert.ToString(dt.Rows[i]["MOBILE2"]).Trim();
                                dr["TELEPHONE2"] = Convert.ToString(dt.Rows[i]["TELEPHONE2"]).Trim();
                                dr["ADDRESS"] = Convert.ToString(dt.Rows[i]["ADDRESS"]).Trim();
                                //dr["DISTRICTNAME"] = Convert.ToString(dt.Rows[i]["DISTRICTNAME"]).Trim();
                                //dr["CITYNAME"] = Convert.ToString(dt.Rows[i]["CITYNAME"]).Trim();
                                if (Convert.ToString(dt.Rows[i]["PIN"]).Trim() == "")
                                {
                                    dr["PIN"] = "000000";
                                }

                                else
                                {
                                    if ((Convert.ToString(dt.Rows[i]["PIN"]).Trim().Length <= 6))
                                    {
                                        dr["PIN"] = Convert.ToString(dt.Rows[i]["PIN"]).Trim();
                                    }
                                    else
                                    {

                                        dr["PIN"] = Convert.ToString(dt.Rows[i]["PIN"]).Substring(0, 6).Trim();
                                    }
                                }
                                if (Convert.ToString(dt.Rows[i]["GSTNO"]).Trim() == "")
                                {
                                    dr["CSTNO"] = "N";/**********GST ABLICABLE************/
                                }
                                else
                                {
                                    dr["CSTNO"] = "Y";
                                }
                                dr["SHORTNAME"] = ShortName;

                                dtadd.Rows.Add(dr);
                                dtadd.AcceptChanges();


                                //===Opening Balance for ACCOUNTS======
                                DataRow dr1 = dtledger.NewRow();
                                dr1["ID"] = Guid.NewGuid();
                                dr1["ACCOUNTID"] = ID;
                                dr1["OPENBALANCE"] = 0;
                                dr1["BALANCETYPE"] = "D";
                                dr1["DEBITAMOUNT"] = 0;
                                dr1["CREDITAMOUNT"] = 0;
                                dr1["REGIONID"] = HttpContext.Current.Session["DEPOTID"];
                                dtledger.Rows.Add(dr1);
                                dtledger.AcceptChanges();

                                //===ACCOUNTS INFO FOR ACCOUNTS======
                                DataRow dr2 = dtledgerInfo.NewRow();
                                dr2["CUSTOMERID"] = ID;
                                dr2["CUSTOMERNAME"] = Convert.ToString(dt.Rows[i]["CUSTOMERNAME"]);
                                dr2["ACTGRPCODE"] = ddlaccgroup.SelectedValue.Trim();
                                dtledgerInfo.Rows.Add(dr2);
                                dtledgerInfo.AcceptChanges();
                            }
                        }
                        string xmlDistributorDetails = string.Empty;
                        string xmlOpeningBalance = string.Empty;
                        string xmlAccountInfo = string.Empty;

                        xmlDistributorDetails = ConvertDatatableToXML(dtadd);
                        xmlOpeningBalance = ConvertDatatableToXML(dtledger);
                        xmlAccountInfo = ConvertDatatableToXML(dtledgerInfo);

                        if (retresult == 1)
                        {
                            MessageBox1.ShowWarning("CUSTOMERNAME should not be blank in uploaded file!", 60, 400);
                            return;
                        }
                        else if (retresult == 2)
                        {
                            MessageBox1.ShowWarning("ADDRESS should not be blank in uploaded file!", 60, 400);
                            return;
                        }
                        //else if (retresult== 5)
                        //{s
                        //    MessageBox1.ShowWarning("DISTRICTNAME should not be blank in uploaded file!", 60, 400);
                        //    return;
                        //}
                        //else if (retresult == 6)
                        //{
                        //    MessageBox1.ShowWarning("CITYNAME should not be blank in uploaded file!", 60, 400);
                        //    return;
                        //}
                        else if (retresult == 3)
                        {
                            MessageBox1.ShowWarning("PANCARD should not be blank in uploaded file!", 60, 400);
                            return;
                        }
                        //else if (retresult == 4)
                        //{
                        //    MessageBox1.ShowWarning(" SHORTNAME should not be blank in uploaded file!", 60, 400);
                        //    return;
                        //}
                        else
                        {
                            result = clscustomer.SaveDistributor(UserID, this.ddlsegment.SelectedValue.Trim(), this.ddlsegment.SelectedItem.Text.Trim(),
                                                                        this.ddlgroup.SelectedValue.Trim(), this.ddlgroup.SelectedItem.Text.Trim(),
                                                                        this.ddlcountry.SelectedValue.Trim(), this.ddlcountry.SelectedItem.Text.Trim(),
                                                                        this.ddlcurrency.SelectedValue.Trim(), this.ddlcurrency.SelectedItem.ToString().Trim(),
                                                                        this.ddlstate.SelectedValue.Trim(), this.ddlstate.SelectedItem.Text.Trim(),
                                                                        this.ddlaccgroup.SelectedValue.Trim(), this.ddlaccgroup.SelectedItem.Text.Trim(),
                                                                        HttpContext.Current.Session["FINYEAR"].ToString().Trim(),
                                                                        HttpContext.Current.Session["DEPOTID"].ToString().Trim(),
                                                                        xmlDistributorDetails, xmlAccountInfo, xmlOpeningBalance);


                            if (result > 0)
                            {
                                MessageBox1.ShowSuccess("Records Saved Successfully");
                                HttpContext.Current.Session["DATAUPLOAD"] = null;
                                HttpContext.Current.Session["LEDGERDETAILS"] = null;
                                HttpContext.Current.Session["LEDGERDETAILSINFO"] = null;
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("Error...in saving");
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btngeneraltemp_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster clscust = new ClsCustomerMaster();
            DataTable dt = clscust.Bindtemplete();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=CustomerDetails.xls";
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
            //int i;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    tab = "";
            //    for (i = 0; i < dt.Columns.Count; i++)
            //    {
            //        Response.Write(tab + dr[i].ToString());
            //        tab = "\t";
            //    }
            //    Response.Write("\n");
            //}
            Response.End();
        }

        catch (Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}