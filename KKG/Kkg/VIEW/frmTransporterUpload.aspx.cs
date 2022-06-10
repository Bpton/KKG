using BAL;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using Utility;
using Microsoft.VisualBasic.FileIO;

public partial class VIEW_frmTransporterUpload : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        FileUpload1.Attributes.Add("onchange", "SetFileName('" + FileUpload1.ClientID + "')");
        try
        {
            if (!IsPostBack)
            {
                //LoadBsSegment();
                //LoadCountry();
                //LoadState();
                //td_export.Style["display"] = "none";
                //td_state.Style["display"] = "";
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message;
        }
    }
    #endregion

    //#region LoadBsSegment
    //public void LoadBsSegment()
    //{
    //    try
    //    {
    //        ClsCustomerMaster clscustomer = new ClsCustomerMaster();
    //        DataTable dt = new DataTable();
    //        dt = clscustomer.BindBusinessSegmentForUploadCustomer();
    //        ddlsegment.Items.Clear();
    //        ddlsegment.Items.Insert(0, new ListItem("Select", "0"));
    //        ddlsegment.DataSource = dt;
    //        ddlsegment.DataTextField = "BSNAME";
    //        ddlsegment.DataValueField = "BSID";
    //        ddlsegment.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }
    //} 
    //#endregion

    //#region ddlsegment_SelectedIndexChanged
    //protected void ddlsegment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LoadGroup(ddlsegment.SelectedValue.Trim());
    //        LoadAccGroup(ddlsegment.SelectedValue.Trim());
    //        if (ddlsegment.SelectedValue == "2E96A0A4-6256-472C-BE4F-C59599C948B0")  // EXPORTID
    //        {
    //            td_export.Style["display"] = "";
    //            td_state.Style["display"] = "none";
    //        }
    //        else
    //        {
    //            td_export.Style["display"] = "none";
    //            td_state.Style["display"] = "";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }
    //} 
    //#endregion

    //#region LoadGroup
    //public void LoadGroup(string BSID)
    //{
    //    try
    //    {
    //        ClsCustomerMaster clscustomer = new ClsCustomerMaster();
    //        DataTable dt = new DataTable();
    //        dt = clscustomer.GetGroupID(BSID);
    //        ddlgroup.Items.Clear();
    //        ddlgroup.Items.Insert(0, new ListItem("Select", "0"));
    //        ddlgroup.DataSource = dt;
    //        ddlgroup.DataTextField = "DIS_CATNAME";
    //        ddlgroup.DataValueField = "DIS_CATID";
    //        ddlgroup.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }
    //} 
    //#endregion

    ////#region LoadCurrency
    ////public void LoadCurrency(string GroupID)
    ////{
    ////    try
    ////    {
    ////        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
    ////        DataTable dtCurrency = new DataTable();
    ////        dtCurrency = ClsCustomer.BindCurrency(GroupID);
    ////        if (dtCurrency.Rows.Count > 0)
    ////        {
    ////            this.ddlcurrency.Items.Clear();
    ////            this.ddlcurrency.Items.Insert(0, new ListItem("Select Currency", "0"));
    ////            this.ddlcurrency.DataSource = dtCurrency;
    ////            this.ddlcurrency.DataTextField = "CURRENCYNAME";
    ////            this.ddlcurrency.DataValueField = "CURRENCYID";
    ////            this.ddlcurrency.DataBind();

    ////            if (dtCurrency.Rows.Count == 1)
    ////            {
    ////                this.ddlcurrency.SelectedValue = Convert.ToString(dtCurrency.Rows[0]["CURRENCYID"]).Trim();
    ////            }

    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    ////#endregion

    ////#region ddlgroup_SelectedIndexChanged
    ////protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
    ////{
    ////    try
    ////    {
    ////        LoadCurrency(ddlgroup.SelectedValue.Trim());
    ////        //if (ddlsegment.SelectedValue == "2E96A0A4-6256-472C-BE4F-C59599C948B0")  // EXPORTID
    ////        //{
    ////        //    td_export.Style["display"] = "";
    ////        //    td_state.Style["display"] = "none";
    ////        //}
    ////        //else
    ////        //{
    ////        //    td_export.Style["display"] = "none";
    ////        //    td_state.Style["display"] = "";
    ////        //}
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        string msg = ex.Message;
    ////    }
    ////} 
    ////#endregion

    ////#region LoadCountry
    ////public void LoadCountry()
    ////{
    ////    try
    ////    {
    ////        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
    ////        ddlcountry.Items.Clear();
    ////        ddlcountry.Items.Insert(0, new ListItem("Select Country", "0"));
    ////        ddlcountry.DataSource = ClsCustomer.BindCountry();
    ////        ddlcountry.DataTextField = "COUNTRYNAME";
    ////        ddlcountry.DataValueField = "COUNTRYID";
    ////        ddlcountry.DataBind();
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        throw ex;
    ////    }
    ////}
    ////#endregion    

    //#region LoadState
    //public void LoadState()
    //{
    //    try
    //    {
    //        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
    //        ddlstate.Items.Clear();
    //        ddlstate.Items.Insert(0, new ListItem("Select State", "0"));
    //        ddlstate.DataSource = ClsCustomer.BindState();
    //        ddlstate.DataTextField = "State_Name";
    //        ddlstate.DataValueField = "State_ID";
    //        ddlstate.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}
    //#endregion

    //#region LoadAccGroup
    //public void LoadAccGroup(string BSID)
    //{
    //    try
    //    {
    //        ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();
    //        DataTable dt = ClsCustomer.GetLedgerbyBSID(BSID);
    //        ddlaccgroup.Items.Clear();
    //        ddlaccgroup.Items.Insert(0, new ListItem("Select Group", "0"));
    //        ddlaccgroup.DataSource = dt;
    //        ddlaccgroup.DataTextField = "GRPNAME";
    //        ddlaccgroup.DataValueField = "GROUPID";
    //        ddlaccgroup.DataBind();
    //        if(dt.Rows.Count==1)
    //        {
    //            this.ddlaccgroup.SelectedValue = Convert.ToString(dt.Rows[0]["GROUPID"]).Trim();
    //            ddlaccgroup.Enabled = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        string msg = ex.Message;
    //    }
    //} 
    //#endregion

    #region CreateDataTableStructure
    protected void CreateDataTableStructure()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("TRANSPORTERID", typeof(string));
        dt.Columns.Add("CODE", typeof(string));
        dt.Columns.Add("NAME", typeof(string));
        dt.Columns.Add("STATEID", typeof(string));
        dt.Columns.Add("STATENAME", typeof(string));
        dt.Columns.Add("DISTRICTID", typeof(string));
        dt.Columns.Add("DISTRICTNAME", typeof(string));
        dt.Columns.Add("CITYID", typeof(string));
        dt.Columns.Add("CITYNAME", typeof(string));
        dt.Columns.Add("ADDRESS", typeof(string));
        dt.Columns.Add("MOBILENO", typeof(string));
        dt.Columns.Add("PANNO", typeof(string));
        dt.Columns.Add("PINZIP", typeof(string));
        dt.Columns.Add("GSTNO", typeof(string));
        dt.Columns.Add("TINNO", typeof(string));
        dt.Columns.Add("APPLYTDS", typeof(string));
        dt.Columns.Add("APPLICABLEGST", typeof(string));
        dt.Columns.Add("ISTDSDECLARE", typeof(string));
        dt.Columns.Add("category", typeof(string));
        dt.Columns.Add("COMPANYTYPEID", typeof(string));
        //dt.Columns.Add("CODE", typeof(string));

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
        dt.Columns.Add(new DataColumn("REGIONNAME", typeof(string)));
        dt.Columns.Add(new DataColumn("BRANCHTAG", typeof(string)));



        HttpContext.Current.Session["LEDGERDETAILS"] = dt;

        return dt;
    }



    protected DataTable CreateLedgerInfoTable()
    {
        DataTable dt = new DataTable();
        dt.Clear();
        dt.Columns.Add(new DataColumn("TRANSPORTERID", typeof(string)));
        dt.Columns.Add(new DataColumn("NAME", typeof(string)));
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
            int status = 0;
            //int statusforname = 0;
            string statusid = string.Empty;
            ClsTransporter ClsTransporter = new ClsTransporter();
            HttpContext.Current.Session["DATAUPLOAD"] = null;
            HttpContext.Current.Session["LEDGERDETAILS"] = null;
            HttpContext.Current.Session["LEDGERDETAILSINFO"] = null;

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



                        DataTable dtledgerInfo = (DataTable)HttpContext.Current.Session["LEDGERDETAILSINFO"];

                        int count = 1;

                        #region checking
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            count = count + 1;
                            Session["COUNT"] = count;

                            if (Convert.ToString(dt.Rows[i]["NAME"]) == "")
                            {
                                retresult = 1;
                                break;
                            }


                            //if (Convert.ToString(dt.Rows[i]["NAME"]) != "" || Convert.ToString(dt.Rows[i]["NAME"]).Contains("'")==false)
                            else
                            {

                                #region Transporter name

                                statusid = ClsTransporter.GetstatusforTransporter(Convert.ToString(dt.Rows[i]["NAME"]).Trim().Replace("'", ""));
                                this.statusid = Convert.ToInt32(statusid);
                                if (this.statusid == 1)
                                {
                                    retresult = 7;
                                    break;

                                }


                                #endregion

                                #region ADDRESS CHEACK

                                if (Convert.ToString(dt.Rows[i]["ADDRESS"]) == "")
                                {
                                    retresult = 2;
                                    break;
                                }
                                #endregion

                                #region state CHEACK
                                if (Convert.ToString(dt.Rows[i]["STATENAME"]) == "")
                                {
                                    retresult = 3; // required state
                                    break;
                                }
                                if (Convert.ToString(dt.Rows[i]["STATEID"]) == "")
                                {
                                    retresult = 4; // required state
                                    break;
                                }

                                else
                                {
                                    string sateid = ClsTransporter.Getsateid(Convert.ToString(dt.Rows[i]["STATENAME"]).Trim());

                                    //if (String.IsNullOrEmpty(sateid))
                                    //{
                                    //    retresult = 4; // not found STATE NAME
                                    //    break;
                                    //}
                                }

                                #endregion

                                //#region PANCARDNO CHEACK
                                //if (Convert.ToString(dt.Rows[i]["PANNO"]) == "")
                                //{
                                //    retresult = 5;
                                //    break;
                                //}


                                //else if ((Convert.ToString(dt.Rows[i]["PANNO"]).Trim().Length != 10))
                                //{
                                //    retresult = 6;
                                //    break;
                                //}

                                //#endregion
                            }

                        }
                        #endregion


                        // #region INSERT DATATABLE and save

                        if (retresult == 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                DataRow dr = dtadd.NewRow();

                                dr["NAME"] = Convert.ToString(dt.Rows[i]["NAME"]).Trim().Replace("'", "");
                                dr["STATEID"] = ClsTransporter.Getsateid(Convert.ToString(dt.Rows[i]["STATENAME"]).Trim());
                                dr["STATENAME"] = Convert.ToString(dt.Rows[i]["STATENAME"]).Trim();
                                //string sateid = ClsTransporter.Getsateid(Convert.ToString(dt.Rows[i]["STATENAME"]).Trim());    

                                string districtid = ClsTransporter.Getdistrictid(Convert.ToString(dt.Rows[i]["STATENAME"]).Trim(), Convert.ToString(dt.Rows[i]["DISTRICTNAME"]).Trim());
                                //string districtid = ClsTransporter.Getdistrictid(string.);

                                if (!String.IsNullOrEmpty(districtid))
                                {
                                    dr["DISTRICTID"] = districtid;
                                    dr["DISTRICTNAME"] = Convert.ToString(dt.Rows[i]["DISTRICTNAME"]).Trim();
                                }
                                else
                                {
                                    dr["DISTRICTID"] = "0";
                                    dr["DISTRICTNAME"] = "Select";
                                }


                                string cityid = ClsTransporter.Getcityid(Convert.ToString(dt.Rows[i]["STATENAME"]).Trim(), Convert.ToString(dt.Rows[i]["CITYNAME"]).Trim(), Convert.ToString(dt.Rows[i]["DISTRICTNAME"]).Trim());

                                if (!String.IsNullOrEmpty(cityid))
                                {
                                    dr["CITYID"] = cityid;
                                    dr["CITYNAME"] = Convert.ToString(dt.Rows[i]["CITYNAME"]).Trim();
                                }
                                else
                                {
                                    dr["CITYID"] = "0";
                                    dr["CITYNAME"] = "Select";
                                }

                                //dr["GROUPID"] = "";
                                //dr["GROUPNAME"] = "";

                                dr["PANNO"] = Convert.ToString(dt.Rows[i]["PANNO"]).Trim();
                                dr["TINNO"] = Convert.ToString(dt.Rows[i]["TINNO"]).Trim();
                                dr["MOBILENO"] = Convert.ToString(dt.Rows[i]["MOBILENO"]).Trim();
                                dr["ADDRESS"] = Convert.ToString(dt.Rows[i]["ADDRESS"]).Trim().Replace("'", "");
                                dr["GSTNO"] = Convert.ToString(dt.Rows[i]["GSTNO"]).Trim();
                                dr["CODE"] = Convert.ToString(dt.Rows[i]["CODE"]).Trim();

                                if (Convert.ToString(dt.Rows[i]["PINZIP"]).Trim() == "")
                                {
                                    dr["PINZIP"] = "000000";
                                }
                                else
                                {
                                    if ((Convert.ToString(dt.Rows[i]["PINZIP"]).Trim().Length <= 6))
                                    {
                                        dr["PINZIP"] = Convert.ToString(dt.Rows[i]["PINZIP"]).Trim();
                                    }
                                    else
                                    {

                                        dr["PINZIP"] = Convert.ToString(dt.Rows[i]["PINZIP"]).Substring(0, 6).Trim();
                                    }
                                }
                                if (Convert.ToString(dt.Rows[i]["GSTNO"]).Trim() == "")
                                {
                                    dr["APPLICABLEGST"] = "N";
                                }
                                else
                                {
                                    dr["APPLICABLEGST"] = "Y";
                                }

                                if (Convert.ToString(dt.Rows[i]["APPLYTDS"]).Trim() == "")
                                {
                                    dr["ISTDSDECLARE"] = "N";
                                }
                                else
                                {
                                    dr["ISTDSDECLARE"] = "Y";
                                }

                                if (Convert.ToString(dt.Rows[i]["category"]).Trim() == "Company")
                                {
                                    dr["COMPANYTYPEID"] = "1";
                                }
                                else
                                {
                                    dr["COMPANYTYPEID"] = "2";
                                }

                                dtadd.Rows.Add(dr);
                                dtadd.AcceptChanges();



                                //===Opening Balance for ACCOUNTS======

                                //DataRow dr1 = dtledger.NewRow();
                                //dr1["ID"] = Guid.NewGuid();
                                ////dr1["ACCOUNTID"] = ID;
                                //dr1["OPENBALANCE"] = 0;
                                //dr1["BALANCETYPE"] = "D";
                                //dr1["DEBITAMOUNT"] = 0;
                                //dr1["CREDITAMOUNT"] = 0;
                                //dr1["REGIONID"] = HttpContext.Current.Session["DEPOTID"];
                                //dtledger.Rows.Add(dr1);
                                //dtledger.AcceptChanges();

                                this.CreateLedgerTable();
                                DataTable dtOpeningBalance = (DataTable)HttpContext.Current.Session["LEDGERDETAILS"];
                                DataTable dtled = ClsTransporter.BindGridBranch();
                                int m = 0;
                                for (m = 0; m < dtled.Rows.Count; m++)
                                {
                                    DataRow drled = dtOpeningBalance.NewRow();
                                    drled["ID"] = Guid.NewGuid();
                                    drled["OPENBALANCE"] = 0;
                                    drled["BALANCETYPE"] = "D";
                                    drled["DEBITAMOUNT"] = 0;
                                    drled["CREDITAMOUNT"] = 0;
                                    drled["REGIONID"] = dtled.Rows[m]["REGIONID"].ToString();
                                    drled["REGIONNAME"] = dtled.Rows[m]["REGIONNAME"].ToString();
                                    drled["BRANCHTAG"] = dtled.Rows[m]["BRANCHTAG"].ToString();

                                    dtOpeningBalance.Rows.Add(drled);
                                    dtOpeningBalance.AcceptChanges();
                                }

                                //===ACCOUNTS INFO FOR ACCOUNTS======

                                DataRow dr2 = dtledgerInfo.NewRow();
                                //dr2["TRANSPORTERID"] = ID;
                                dr2["NAME"] = Convert.ToString(dt.Rows[i]["NAME"]).Trim().Replace("'", "");
                                dr2["ACTGRPCODE"] = "7b80c1a8-c6a6-4a25-a7b1-332ca9a25503";
                                dtledgerInfo.Rows.Add(dr2);
                                dtledgerInfo.AcceptChanges();

                                string xmlTransporterDetails = string.Empty;
                                string xmlOpeningBalance = string.Empty;
                                string xmlAccountInfo = string.Empty;

                                xmlTransporterDetails = ConvertDatatableToXML(dtadd);
                                xmlOpeningBalance = ConvertDatatableToXML(dtOpeningBalance);
                                xmlAccountInfo = ConvertDatatableToXML(dtledgerInfo);
                                dr.Delete();
                                dr2.Delete();


                                status = ClsTransporter.SaveTransporterUpload(HttpContext.Current.Session["FINYEAR"].ToString().Trim(), xmlTransporterDetails, xmlAccountInfo, xmlOpeningBalance);

                                if (status > 0)
                                {
                                    MessageBox1.ShowSuccess("Records Saved Successfully");
                                    HttpContext.Current.Session["DATAUPLOAD"] = null;
                                    HttpContext.Current.Session["LEDGERDETAILS"] = null;
                                    HttpContext.Current.Session["LEDGERDETAILSINFO"] = null;
                                }
                                else
                                {
                                    MessageBox.Show("Error...in saving");
                                }
                            }
                        }

                        else if (retresult == 1)
                        {
                            //MessageBox1.ShowWarning(" Please Put A NAME </font></b>  <b><font color='red'></font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            //Session["COUNT"] = null;
                            //Session["NAME"] = null;
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["NAME"] + "</font></b> NAME is required OR contains (') <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            Session["COUNT"] = null;
                            Session["NAME"] = null;

                            return;
                        }
                        else if (retresult == 2)
                        {
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["ADDRESS"] + "</font></b> ADDRESS is required   <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            Session["COUNT"] = null;
                            Session["ADDRESS"] = null;
                            return;
                        }
                        else if (retresult == 3)
                        {
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["STATENAME"] + "</font></b>  STATENAME is required <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            Session["STATENAME"] = null;
                            Session["STATENAME"] = null;
                            return;
                        }
                        else if (retresult == 4)
                        {
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["STATEID"] + "</font></b>  STATENAME is required of Checking   <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            Session["STATEID"] = null;
                            Session["STATEID"] = null;
                            return;
                        }
                        else if (retresult == 5)
                        {
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["PANNO"] + "</font></b> PANNO is required  <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            Session["PANNO"] = null;
                            Session["PANNO"] = null;
                            return;
                        }
                        else if (retresult == 6)
                        {
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["PANNO"] + "</font></b> PANNO is required of Checking  <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            Session["PANNO"] = null;
                            Session["PANNO"] = null;
                            return;
                        }
                        else if (retresult == 7)
                        {
                            MessageBox1.ShowWarning("<b><font color='red'>" + Session["NAME"] + "</font></b> Name Is Already In Databse  <b><font color='red'> " + Session["NAME"] + " </font></b> in ROW NO:<b><font color='red'> " + Session["count"] + " </font></b>!", 60, 500);
                            return;
                        }
                    }
                    else
                    {

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
            ClsTransporter clstpu = new ClsTransporter();
            DataTable dt = clstpu.CreateTempOfUploadTransporter();
            Response.ClearContent();
            Response.Buffer = true;
            string attachment = "attachment; filename=TransporterDetails.xls";
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

    public int statusid { get; set; }
}