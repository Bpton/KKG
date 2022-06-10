using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;

public partial class VIEW_frmCustomerExcelUpload : System.Web.UI.Page
{
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

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
        dt.Columns.Add("SLNO", typeof(string));
        dt.Columns.Add("REGCUSTOMERTYPE", typeof(string));
        dt.Columns.Add("Depot", typeof(string));
        dt.Columns.Add("CustomerName", typeof(string));
        dt.Columns.Add("SHORTNAME", typeof(string));
        dt.Columns.Add("BusniessSegment", typeof(string));
        dt.Columns.Add("Group", typeof(string));
        //dt.Columns.Add("CPCCUSTOMERCODE", typeof(string));
        dt.Columns.Add("CustomerType", typeof(string));
        dt.Columns.Add("PurchaseType", typeof(string));
        dt.Columns.Add("FIRSTCONTACTPERSON", typeof(string));
        dt.Columns.Add("SECONDCONTACTPERSON", typeof(string));
        dt.Columns.Add("MOBILENO1", typeof(string));
        dt.Columns.Add("LANDLINENO1", typeof(string));
        dt.Columns.Add("MOBILENO2", typeof(string));
        dt.Columns.Add("LANDLINENO2", typeof(string));
        dt.Columns.Add("FIRSTEMAILID", typeof(string));
        dt.Columns.Add("SECONDEMAILID", typeof(string));
        dt.Columns.Add("STATE", typeof(string));
        dt.Columns.Add("DISTRICT", typeof(string));
        dt.Columns.Add("CITY", typeof(string));
        dt.Columns.Add("ADDRESS1", typeof(string));
        dt.Columns.Add("PINZIPCODE1", typeof(string));
        dt.Columns.Add("ADDRESS2", typeof(string));
        dt.Columns.Add("PINZIPCODE2", typeof(string));
        dt.Columns.Add("DOB", typeof(string));
        dt.Columns.Add("ANVDATE", typeof(string));
        dt.Columns.Add("ACCOUNTSGROUP", typeof(string));
        //dt.Columns.Add("CreditLimitDays", typeof(string));
        //dt.Columns.Add("CreditLimitAmount", typeof(string));
        dt.Columns.Add("Currency", typeof(string));
        dt.Columns.Add("PANNO", typeof(string));
        dt.Columns.Add("TINNO", typeof(string));
        dt.Columns.Add("GSTIN", typeof(string));
        dt.Columns.Add("Active", typeof(string));
        dt.Columns.Add("AdditinalSuperStockistMargin", typeof(string));


        HttpContext.Current.Session["MaterialUpload"] = dt;
    }
    #endregion


    #region btnUpload_Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            ClsCustomerMaster ClsCustomer = new ClsCustomerMaster();

            DataTable dtclone = new DataTable();
            //string VoucherTypeID = string.Empty;

            string SLNO = string.Empty;
            string REGCUSTOMERTYPE = string.Empty;
            string Depot = string.Empty;
            string CustomerName = string.Empty;
            string SHORTNAME = string.Empty;
            string BusniessSegment = string.Empty;
            string Group = string.Empty;
            //string CPCCUSTOMERCODE = string.Empty;
            string CustomerType = string.Empty;
            string PurchaseType = string.Empty;
            string Assessable = string.Empty;
            string Returnable = string.Empty;
            string FIRSTCONTACTPERSON = string.Empty;
            string SECONDCONTACTPERSON = string.Empty;
            string MOBILENO1 = string.Empty;
            string LANDLINENO1 = string.Empty;
            string MOBILENO2 = string.Empty;
            string LANDLINENO2 = string.Empty;
            string FIRSTEMAILID = string.Empty;
            string SECONDEMAILID = string.Empty;
            string STATE = string.Empty;
            string DISTRICT = string.Empty;
            string CITY = string.Empty;
            string ADDRESS1 = string.Empty;
            string PINZIPCODE1 = string.Empty;
            string ADDRESS2 = string.Empty;
            string PINZIPCODE2 = string.Empty;
            string DOB = string.Empty;
            string ANVDATE = string.Empty;
            string ACCOUNTSGROUP = string.Empty;

            //string CreditLimitDays = string.Empty;
            //string CreditLimitAmount = string.Empty;
            string Currency = string.Empty;
            
            string PANNO = string.Empty;
            string TINNO = string.Empty;

            string GSTIN = string.Empty;
            string Active = string.Empty;
            string AdditinalSuperStockistMargin = string.Empty;

            string FilenameExtension = Path.GetFileName(FileUpload1.FileName);

            if (FilenameExtension == "")
            {
                MessageBox1.ShowWarning("Please Uploaded CSV file");
                return;
            }
            else
            {
                string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
                // string currentPath = "\\\\" + uploadpath + Path.GetFileName(FileUpload1.FileName); ;
                string currentPath = Server.MapPath("~/FileUpload/") +
                                 Path.GetFileName(FileUpload1.FileName);
                string Extension = Path.GetExtension(currentPath);
                if (Extension.Trim() != ".csv")
                {
                    MessageBox1.ShowInfo("Please Convert uploaded file to CSV file");
                    return;
                }
                else
                {
                    FileUpload1.SaveAs(currentPath);
                    //DataTable dtregion = clscustomer.GetAddress(HttpContext.Current.Session["USERID"].ToString().Trim());
                    DataTable dt = GetDataTabletFromCSVFile(currentPath);

                    //dtclone = dt.AsEnumerable().GroupBy(r => new { Col1 = r["Ledger Name"] }).Select(Sum( x => x.Field<int>( "MyColumn")).First()).CopyToDataTable(); 

                    if (dt.Rows.Count > 0)
                    {
                        string ID = string.Empty;
                        CreateDataTableStructure();
                        if (HttpContext.Current.Session["MaterialUpload"] == null)
                        {
                            CreateDataTableStructure();
                        }

                        string DepartmentName = string.Empty;

                        string BSID1 = string.Empty;
                        string BSNAME1 = string.Empty;

                        DataTable dtCheck = (DataTable)HttpContext.Current.Session["MaterialUpload"];

                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            if (dt.Rows[k]["Customer Name*"].ToString().Trim() != "")
                            {
                                SLNO = dt.Rows[k]["SL NO*"].ToString().Trim();
                                REGCUSTOMERTYPE = dt.Rows[k]["REG CUSTOMER TYPE*"].ToString().Trim();
                                Depot = dt.Rows[k]["Depot*"].ToString().Trim();
                                CustomerName = dt.Rows[k]["Customer Name*"].ToString().Trim();
                                SHORTNAME = dt.Rows[k]["SHORT NAME*"].ToString().Trim();
                                BusniessSegment = dt.Rows[k]["Busniess Segment*"].ToString().Trim();
                                Group = dt.Rows[k]["Group*"].ToString().Trim();
                                //CPCCUSTOMERCODE = dt.Rows[k]["CPC CUSTOMER CODE"].ToString().Trim();
                                CustomerType = dt.Rows[k]["Customer Type*"].ToString().Trim();
                                PurchaseType = dt.Rows[k]["Purchase Type*"].ToString().Trim();
                                FIRSTCONTACTPERSON = dt.Rows[k]["FIRST CONTACT PERSON*"].ToString().Trim();
                                SECONDCONTACTPERSON = dt.Rows[k]["SECOND CONTACT PERSON"].ToString().Trim();
                                MOBILENO1 = dt.Rows[k]["MOBILE NO1"].ToString().Trim();
                                LANDLINENO1 = dt.Rows[k]["LAND LINE NO1"].ToString().Trim();
                                MOBILENO2 = dt.Rows[k]["MOBILE NO2"].ToString().Trim();
                                LANDLINENO2 = dt.Rows[k]["LAND LINE NO2"].ToString().Trim();
                                FIRSTEMAILID = dt.Rows[k]["FIRST EMAIL ID*"].ToString().Trim();

                                SECONDEMAILID = dt.Rows[k]["SECOND EMAIL ID"].ToString().Trim();
                                STATE = dt.Rows[k]["STATE*"].ToString().Trim();
                                DISTRICT = dt.Rows[k]["DISTRICT*"].ToString().Trim();
                                CITY = dt.Rows[k]["CITY*"].ToString().Trim();
                                ADDRESS1 = dt.Rows[k]["ADDRESS1*"].ToString().Trim();
                                PINZIPCODE1 = dt.Rows[k]["PIN/ZIP CODE1*"].ToString().Trim();
                                ADDRESS2 = dt.Rows[k]["ADDRESS2"].ToString().Trim();
                                PINZIPCODE2 = dt.Rows[k]["PIN/ZIP CODE2"].ToString().Trim();
                                DOB = dt.Rows[k]["DOB"].ToString().Trim();
                                ANVDATE = dt.Rows[k]["ANV.DATE"].ToString().Trim();
                                ACCOUNTSGROUP = dt.Rows[k]["ACCOUNTS GROUP*"].ToString().Trim();

                                //CreditLimitDays = dt.Rows[k]["Credit Limit Days"].ToString().Trim();
                                //CreditLimitAmount = dt.Rows[k]["Credit Limit Amount"].ToString().Trim();

                                Currency = dt.Rows[k]["Currency*"].ToString().Trim();
                               
                                PANNO = dt.Rows[k]["PAN NO"].ToString().Trim();
                                TINNO = dt.Rows[k]["TIN NO"].ToString().Trim();

                                GSTIN = dt.Rows[k]["GSTIN"].ToString().Trim();
                                Active = dt.Rows[k]["Active"].ToString().Trim();
                                AdditinalSuperStockistMargin = dt.Rows[k]["Additinal Super Stockist Margin"].ToString().Trim();

                                if (REGCUSTOMERTYPE == "" || REGCUSTOMERTYPE == "SELECT CUSTOMER TYPE")
                                {
                                    MessageBox1.ShowInfo("<b> select REG CUSTOMER TYPE ..!! <font color=red> '" + REGCUSTOMERTYPE.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (Depot == "" || Depot == "SELECT DEPOT")
                                {
                                    MessageBox1.ShowInfo("<b>Select Depot  ..!! <font color=red> '" + Depot.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (CustomerName == "")
                                {
                                    MessageBox1.ShowInfo("<b>Enter Customer Name ..!! <font color=red> '" + CustomerName.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (SHORTNAME == "")
                                {
                                    MessageBox1.ShowInfo("<b>Enter SHORT NAME ..!! <font color=red> '" + SHORTNAME.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (BusniessSegment == "" || BusniessSegment == "SELECT BS NAME")
                                {
                                    MessageBox1.ShowInfo("<b>Select Busniess Segment  ..!! <font color=red> '" + BusniessSegment.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (Group == "")
                                {
                                    MessageBox1.ShowInfo("<b>Group can't allow space ..!! <font color=red> '" + Group.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (CustomerType == "" || CustomerType == "Select Customer Type")
                                {
                                    MessageBox1.ShowInfo("<b>Select Customer Type  ..!! <font color=red> '" + CustomerType.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (PurchaseType == "" || PurchaseType == "Select Purchase Type")
                                {
                                    MessageBox1.ShowInfo("<b>Select Purchase Type ..!! <font color=red> '" + PurchaseType.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (FIRSTCONTACTPERSON == "")
                                {
                                    MessageBox1.ShowInfo("<b>Enter FIRST CONTACT PERSON ..!! <font color=red> '" + FIRSTCONTACTPERSON.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (FIRSTEMAILID == "")
                                {
                                    MessageBox1.ShowInfo("<b>Enter FIRST EMAIL ID ..!! <font color=red> '" + FIRSTEMAILID.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (STATE == "" || STATE == "Select State Name")
                                {
                                    MessageBox1.ShowInfo("<b>Select State Name ..!! <font color=red> '" + STATE.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (DISTRICT == "" || DISTRICT == "Select District")
                                {
                                    MessageBox1.ShowInfo("<b>Select District ..!! <font color=red> '" + DISTRICT.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (CITY == "" || CITY == "Select City")
                                {
                                    MessageBox1.ShowInfo("<b>Select City ..!! <font color=red> '" + CITY.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (ADDRESS1 == "")
                                {
                                    MessageBox1.ShowInfo("<b>Enter ADDRESS1 ..!! <font color=red> '" + ADDRESS1.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (PINZIPCODE1 == "" || PINZIPCODE1.Length != 6)
                                {
                                    MessageBox1.ShowInfo("<b>Enter PIN/ZIP CODE1 ..!! <font color=red> '" + PINZIPCODE1.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (ACCOUNTSGROUP == "" || ACCOUNTSGROUP == "Select Group Name")
                                {
                                    MessageBox1.ShowInfo("<b>Select Group Name ..!! <font color=red> '" + ACCOUNTSGROUP.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (Currency == "" || Currency == "Select Currency")
                                {
                                    MessageBox1.ShowInfo("<b>Select Currency ..!! <font color=red> '" + Currency.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (MOBILENO1.Length != 10 )
                                {
                                    if (MOBILENO1 == "")
                                    {
                                        MOBILENO1 = "0000000000";
                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Enter 10 digit mobile no. ..!! <font color=red> '" + MOBILENO1.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }
                                }
                                else if (MOBILENO2.Length != 10)
                                {
                                    if (MOBILENO2 == "")
                                    {
                                        MOBILENO2 = "0000000000";
                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Enter 10 digit mobile no. ..!! <font color=red> '" + MOBILENO2.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }
                                }
                                                              
                                if(DOB=="")
                                {
                                    DOB = "01/01/1900";
                                }
                                string dateString = Convert.ToString(DOB);
                                int flag5 = ValidateDate(dateString);
                                if (flag5 == 1)
                                {
                                    MessageBox1.ShowInfo("<b> Date formate should be dd-mm-yyyy  !! <font color=red> '" + DOB.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                    //RESULT = 4; //Date formate should be dd-mm-yyyy!
                                    //break;
                                }
                                if (flag5 == 2)
                                {
                                    MessageBox1.ShowInfo("<b> Date formate should be dd-mm-yyyy  !! <font color=red> '" + DOB.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }

                                string dateString1 = Convert.ToString(ANVDATE);
                                if (dateString1 == "")
                                {
                                    dateString1 = "01/01/1900";
                                }
                                int flag6 = ValidateDate(dateString1);
                                if (flag6 == 1)
                                {
                                    MessageBox1.ShowInfo("<b> Date formate should be dd-mm-yyyy  !! <font color=red> '" + ANVDATE.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                    //RESULT = 4; //Date formate should be dd-mm-yyyy!
                                    //break;
                                }
                                if (flag6 == 2)
                                {
                                    MessageBox1.ShowInfo("<b> Date formate should be dd-mm-yyyy  !! <font color=red> '" + ANVDATE.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }

                                if (PANNO.Length == 10)
                                {
                                    string str = PANNO.Substring(0, 5);
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (!char.IsLetter(str[i]))
                                        {
                                            MessageBox1.ShowInfo("<b>1st 5 digit must be string. !! <font color=red> '" + PANNO.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                            return;
                                        }

                                    }
                                    str = PANNO.Substring(5, 4);
                                    for (int i = 0; i < 4; i++)
                                    {
                                        if (!char.IsDigit(str[i]))
                                        {
                                            MessageBox1.ShowInfo("<b>6th to 9th must be number. !! <font color=red> '" + PANNO.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                            return;
                                        }
                                    }
                                    str = PANNO.Substring(9, 1);
                                    for (int i = 0; i < 1; i++)
                                    {
                                        if (!char.IsLetter(str[i]))
                                        {
                                            MessageBox1.ShowInfo("<b>10th digit must be string. !! <font color=red> '" + PANNO.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                            return;
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox1.ShowInfo("<b>PAN No. must be 10 digit. !! <font color=red> '" + PANNO.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;

                                }

                                if (GSTIN.Length == 3)
                                {
                                    string str = GSTIN.Substring(0, 1);
                                    if (!char.IsDigit(str[0]))
                                    {
                                        MessageBox1.ShowInfo("<b>Enter Only Number for 1st digit !! <font color=red> '" + GSTIN.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }


                                    str = GSTIN.Substring(1, 1);
                                    if (!char.IsLetter(str[0]))
                                    {
                                        MessageBox1.ShowInfo("<b>Enter Only Letter for 2nd digit. !! <font color=red> '" + GSTIN.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }

                                    str = GSTIN.Substring(2, 1);
                                    if ((char.IsLetter(str[0])) || (char.IsDigit(str[0])))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Enter only Number or letter for 3rd digit !! <font color=red> '" + GSTIN.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;

                                    }


                                }
                                else
                                {
                                    MessageBox1.ShowInfo("<b>GST No. must be 3 digit. !! <font color=red> '" + GSTIN.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;

                                }


                                DataRow dr = dtCheck.NewRow();
                                dr["SLNO"] = SLNO;
                                dr["REGCUSTOMERTYPE"] = REGCUSTOMERTYPE;
                                dr["Depot"] = Depot;
                                dr["CustomerName"] = CustomerName;
                                dr["SHORTNAME"] = SHORTNAME;
                                dr["BusniessSegment"] = BusniessSegment;
                                dr["Group"] = Group;
                                //dr["CPCCUSTOMERCODE"] = CPCCUSTOMERCODE;
                                dr["CustomerType"] = CustomerType;
                                dr["PurchaseType"] = PurchaseType;
                                dr["FIRSTCONTACTPERSON"] = FIRSTCONTACTPERSON;
                                dr["SECONDCONTACTPERSON"] = SECONDCONTACTPERSON;
                                dr["MOBILENO1"] = MOBILENO1;
                                dr["LANDLINENO1"] = LANDLINENO1;
                                dr["MOBILENO2"] = MOBILENO2;
                                dr["LANDLINENO2"] = LANDLINENO2;
                                dr["FIRSTEMAILID"] = FIRSTEMAILID;
                                dr["SECONDEMAILID"] = SECONDEMAILID;
                                dr["STATE"] = STATE;
                                dr["DISTRICT"] = DISTRICT;
                                dr["CITY"] = CITY;
                                dr["ADDRESS1"] = ADDRESS1;
                                if (PINZIPCODE1 == "")
                                {
                                    dr["PINZIPCODE1"] = "000000";
                                }

                                else
                                {
                                    if ((Convert.ToString(PINZIPCODE1).Trim().Length <= 6))
                                    {
                                        dr["PINZIPCODE1"] = PINZIPCODE1;
                                    }
                                    else
                                    {

                                        dr["PINZIPCODE1"] = Convert.ToString(PINZIPCODE1).Substring(0, 6).Trim();
                                    }
                                }

                                dr["ADDRESS2"] = ADDRESS2;
                                dr["PINZIPCODE2"] = PINZIPCODE2;
                                dr["DOB"] = DOB;
                                dr["ANVDATE"] = ANVDATE;
                                dr["ACCOUNTSGROUP"] = ACCOUNTSGROUP;
                                //dr["CreditLimitDays"] = CreditLimitDays;
                                // dr["CreditLimitAmount"] = CreditLimitAmount;
                                dr["Currency"] = Currency;                               
                                dr["PANNO"] = PANNO;
                                dr["TINNO"] = TINNO;
                                dr["GSTIN"] = GSTIN;
                                dr["Active"] = Active;
                                dr["AdditinalSuperStockistMargin"] = AdditinalSuperStockistMargin;

                                dtCheck.Rows.Add(dr);
                                dtCheck.AcceptChanges();
                            }
                        }

                        /* Checking Segment & Department more then one or not in same ledger */
                        string xmlTable = string.Empty;
                        xmlTable = ConvertDatatableToXML(dtCheck);
                        string ip_address = Request.UserHostAddress.ToString().Trim();
                        DataTable CheckResult = new DataTable();
                        CheckResult = ClsCustomer.SaveUploadCustomer(xmlTable, this.Session["IUSERID"].ToString(), ip_address);

                        string Result = CheckResult.Rows[0][0].ToString();
                        if (Result.ToString() == "9")
                        {
                            MessageBox1.ShowInfo("<b>Save success..!!</b>", 60, 500);
                            return;
                        }
                        else if (Result != "")
                        {
                            MessageBox1.ShowInfo(Result.ToString());
                            return;
                        }
                        else
                        {
                            MessageBox1.ShowInfo("Please check your excel data  !!");
                            return;

                        }
                    }
                    else
                    {
                        MessageBox1.ShowError("Error...in saving");
                        return;
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

}