using BAL;
using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using Microsoft.VisualBasic.FileIO;

public partial class VIEW_frmVendorUpload : System.Web.UI.Page
{
    ClsTPUVendor ClsTPUMaster = new ClsTPUVendor();
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
        dt.Columns.Add("PartyName", typeof(string));
        dt.Columns.Add("SuppliedItemType", typeof(string));
        dt.Columns.Add("State", typeof(string));
        dt.Columns.Add("District", typeof(string));
        dt.Columns.Add("City", typeof(string));
        dt.Columns.Add("ContactAddress", typeof(string));
        dt.Columns.Add("Pincode", typeof(string));
        dt.Columns.Add("AddressInfo2", typeof(string));
        dt.Columns.Add("Pincode2", typeof(string));       
        dt.Columns.Add("ContactPerson", typeof(string));
        dt.Columns.Add("EmailID", typeof(string));
        dt.Columns.Add("MobileNo", typeof(string));
        dt.Columns.Add("LandLineNo", typeof(string));
        dt.Columns.Add("MobileNo2", typeof(string));
        dt.Columns.Add("LandLineNo2", typeof(string));
        dt.Columns.Add("PANNo", typeof(string));
        dt.Columns.Add("CSTNO", typeof(string));
        dt.Columns.Add("VATNo", typeof(string));
        dt.Columns.Add("TINNo", typeof(string));
        dt.Columns.Add("STNo", typeof(string));
        dt.Columns.Add("GSTNo", typeof(string));
        dt.Columns.Add("BankName", typeof(string));
        dt.Columns.Add("BankBranch", typeof(string));
        dt.Columns.Add("BankAccountNo", typeof(string));
        dt.Columns.Add("IFSCCode", typeof(string));
        dt.Columns.Add("AccountsGroup", typeof(string));
        dt.Columns.Add("TDSLimit", typeof(string));
        dt.Columns.Add("TDSDeclaration", typeof(string));
        dt.Columns.Add("Active", typeof(string));
        dt.Columns.Add("ServiceTaxApplication", typeof(string));
        dt.Columns.Add("CreditLimit", typeof(string));
        dt.Columns.Add("CreditDays", typeof(string));
        
        HttpContext.Current.Session["ProductUpload"] = dt;
    }
    #endregion


    #region btnUpload_Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {           
            ClsTPUVendor ClsTPUMaster = new ClsTPUVendor();
            DataTable dtclone = new DataTable();

            string SLNO = string.Empty;
            string PartyName = string.Empty;
            string SuppliedItemType = string.Empty;
            string State = string.Empty;
            string District = string.Empty;
            string City = string.Empty;
            string ContactAddress = string.Empty;
            string Pincode = string.Empty;
            string AddressInfo2 = string.Empty;
            string Pincode2 = string.Empty;

            string ContactPerson = string.Empty;
            string EmailID = string.Empty;
            string MobileNo = string.Empty;
            string LandLineNo = string.Empty;
            string MobileNo2 = string.Empty;
            string LandLineNo2 = string.Empty;
            string PANNo = string.Empty;
            string CSTNO = string.Empty;
            string VATNo = string.Empty;
            string TINNo = string.Empty;
            string STNo = string.Empty;
            string GSTNo = string.Empty;

            string BankName = string.Empty;
            string BankBranch = string.Empty;
            string BankAccountNo = string.Empty;
            string IFSCCode = string.Empty;
            string AccountsGroup = string.Empty;
            string TDSLimit = string.Empty;
            string TDSDeclaration = string.Empty;
            string Active = string.Empty;
            string ServiceTaxApplication = string.Empty;
            
            string CreditLimit = string.Empty;
            string CreditDays = string.Empty;
            

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
                        if (HttpContext.Current.Session["ProductUpload"] == null)
                        {
                            CreateDataTableStructure();
                        }

                        string DepartmentName = string.Empty;

                        string BSID1 = string.Empty;
                        string BSNAME1 = string.Empty;

                        DataTable dtCheck = (DataTable)HttpContext.Current.Session["ProductUpload"];

                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            if (dt.Rows[k]["PARTY NAME*"].ToString().Trim() != "")
                            {
                                SLNO = dt.Rows[k]["SL.NO.*"].ToString().Trim();
                                PartyName = dt.Rows[k]["PARTY NAME*"].ToString().Trim();
                                SuppliedItemType = dt.Rows[k]["Supplied Item / Type*"].ToString().Trim();
                                State = dt.Rows[k]["State*"].ToString().Trim();
                                District = dt.Rows[k]["District*"].ToString().Trim();
                                City = dt.Rows[k]["City*"].ToString().Trim();
                                ContactAddress = dt.Rows[k]["Contact Address*"].ToString().Trim();
                                Pincode = dt.Rows[k]["Pin code*"].ToString().Trim();
                                AddressInfo2 = dt.Rows[k]["Address Info 2"].ToString().Trim();
                                Pincode2 = dt.Rows[k]["Pin code2"].ToString().Trim();
                                ContactPerson = dt.Rows[k]["Contact Person"].ToString().Trim();
                                EmailID = dt.Rows[k]["E-mail ID"].ToString().Trim();
                                MobileNo = dt.Rows[k]["Mobile No"].ToString().Trim();
                                LandLineNo = dt.Rows[k]["Land Line No"].ToString().Trim();
                                MobileNo2 = dt.Rows[k]["Mobile No2"].ToString().Trim();
                                LandLineNo2 = dt.Rows[k]["Land Line No2"].ToString().Trim();
                                PANNo = dt.Rows[k]["PAN No*"].ToString().Trim();
                                CSTNO = dt.Rows[k]["CST NO"].ToString().Trim();
                                VATNo = dt.Rows[k]["VAT No"].ToString().Trim();
                                TINNo = dt.Rows[k]["TIN No"].ToString().Trim();
                                STNo = dt.Rows[k]["ST No"].ToString().Trim();
                                GSTNo = dt.Rows[k]["GST No"].ToString().Trim();
                                BankName = dt.Rows[k]["Bank Name"].ToString().Trim();
                                BankBranch = dt.Rows[k]["Bank Branch"].ToString().Trim();
                                BankAccountNo = dt.Rows[k]["Bank Account No"].ToString().Trim();
                                IFSCCode = dt.Rows[k]["IFSC Code"].ToString().Trim();

                                AccountsGroup = dt.Rows[k]["ACCOUNTS GROUP*"].ToString().Trim();
                                TDSLimit = dt.Rows[k]["TDS LIMIT"].ToString().Trim();
                                TDSDeclaration = dt.Rows[k]["TDS DECLARATION"].ToString().Trim();
                                Active = dt.Rows[k]["ACTIVE"].ToString().Trim();
                                ServiceTaxApplication = dt.Rows[k]["Service TAX APPLICABLE"].ToString().Trim();
                    
                                CreditLimit = dt.Rows[k]["Credit Limit"].ToString().Trim();
                                CreditDays = dt.Rows[k]["Credit Days"].ToString().Trim();
                                

                                if (PartyName == "")
                                {
                                    MessageBox1.ShowInfo("<b>Party Name can't allow space ..!! <font color=red> '" + PartyName.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (SuppliedItemType == "" || SuppliedItemType=="Select Supplied Item")
                                {
                                    MessageBox1.ShowInfo("<b>Select Valid Supplied Item Type ..!! <font color=red> '" + SuppliedItemType.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (State == "" || State =="Select State Name")
                                {
                                    MessageBox1.ShowInfo("<b>Select valid State !! <font color=red> '" + State.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (District == "")
                                {
                                    MessageBox1.ShowInfo("<b>District can't allow space ..!! <font color=red> '" + District.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (City == "")
                                {
                                    MessageBox1.ShowInfo("<b>City can't allow space ..!! <font color=red> '" + City.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (ContactAddress == "")
                                {
                                    MessageBox1.ShowInfo("<b>Contact Address can't allow space ..!! <font color=red> '" + ContactAddress.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (Pincode == "")
                                {
                                    MessageBox1.ShowInfo("<b>Pin code can't allow space ..!! <font color=red> '" + Pincode.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (PANNo == "")
                                {
                                    MessageBox1.ShowInfo("<b>PAN No can't allow space ..!! <font color=red> '" + PANNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                //else if (BestBeforeUse == "")
                                //{
                                //    MessageBox1.ShowInfo("<b>Best Before Use can't allow space ..!! <font color=red> '" + BestBeforeUse.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                //    return;
                                //}

                                if (TDSLimit == "")
                                {
                                   TDSLimit = "0";
                                }
                                if (CreditLimit == "")
                                {                                   
                                    CreditLimit = "0";
                                }
                                if (CreditDays == "")
                                {
                                    CreditDays = "0";
                                }
                                if (TDSDeclaration == "")
                                {
                                    TDSDeclaration = "N";
                                }
                                if (Active == "")
                                {
                                    Active = "N";
                                }
                                if (ServiceTaxApplication == "")
                                {
                                    ServiceTaxApplication = "N";
                                }
                                

                                if (!string.IsNullOrEmpty(TDSLimit.ToString()))
                                {
                                    Decimal valueTDSLimit;
                                    if (Decimal.TryParse(TDSLimit, out valueTDSLimit))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for TDS Limit !! <font color=red> '" + TDSLimit.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;

                                    }
                                }

                                if (!string.IsNullOrEmpty(CreditLimit.ToString()))
                                {
                                    Decimal valueCreditLimit;
                                    if (Decimal.TryParse(CreditLimit, out valueCreditLimit))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for Credit Limit !! <font color=red> '" + CreditLimit.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;

                                    }
                                }
                                if (!string.IsNullOrEmpty(CreditDays.ToString()))
                                {
                                    Decimal valueCreditDays;
                                    if (Decimal.TryParse(CreditDays, out valueCreditDays))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for Credit Days !! <font color=red> '" + CreditDays.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;

                                    }
                                }

                                if (PANNo.Length == 10)
                                {
                                    string str = PANNo.Substring(0, 5);
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (!char.IsLetter(str[i]))
                                        {
                                            MessageBox1.ShowInfo("<b>1st 5 digit must be string. !! <font color=red> '" + PANNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                            return;
                                        }
                                            
                                    }
                                    str = PANNo.Substring(5, 4);
                                    for (int i = 0; i < 4; i++)
                                    {
                                        if (!char.IsDigit(str[i]))
                                        {
                                            MessageBox1.ShowInfo("<b>6th to 9th must be number. !! <font color=red> '" + PANNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                            return;
                                        }
                                    }
                                    str = PANNo.Substring(9, 1);
                                    for (int i = 0; i < 1; i++)
                                    {
                                        if (!char.IsLetter(str[i]))
                                        {
                                            MessageBox1.ShowInfo("<b>10th digit must be string. !! <font color=red> '" + PANNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                            return;
                                        }
                                    }

                                }
                                else
                                {
                                    MessageBox1.ShowInfo("<b>PAN No. must be 10 digit. !! <font color=red> '" + PANNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;

                                }

                                if (GSTNo.Length == 3)
                                {
                                    string str = GSTNo.Substring(0, 1);
                                    if (!char.IsDigit(str[0]))
                                    {
                                        MessageBox1.ShowInfo("<b>Enter Only Number for 1st digit !! <font color=red> '" + GSTNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }


                                    str = GSTNo.Substring(1, 1);
                                    if (!char.IsLetter(str[0]))
                                    {
                                        MessageBox1.ShowInfo("<b>Enter Only Letter for 2nd digit. !! <font color=red> '" + GSTNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }

                                    str = GSTNo.Substring(2, 1);
                                    if ((char.IsLetter(str[0])) || (char.IsDigit(str[0])))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Enter only Number or letter for 3rd digit !! <font color=red> '" + GSTNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                        return;

                                    }


                                }
                                else
                                {
                                    MessageBox1.ShowInfo("<b>GST No. must be 3 digit. !! <font color=red> '" + GSTNo.ToString() + "' </font> Line nimber " + SLNO.ToString() + "</b>", 60, 500);
                                    return;

                                }



                                DataRow dr = dtCheck.NewRow();
                                dr["SLNO"] = SLNO;
                                dr["PartyName"] = PartyName;
                                dr["SuppliedItemType"] = SuppliedItemType;
                                dr["State"] = State;
                                dr["District"] = District;
                                dr["City"] = City;
                                dr["ContactAddress"] = ContactAddress;
                                dr["Pincode"] = Pincode;
                                dr["AddressInfo2"] = AddressInfo2;
                                dr["Pincode2"] = Pincode2;
                                dr["ContactPerson"] = ContactPerson;

                                dr["EmailID"] = EmailID;
                                dr["MobileNo"] = MobileNo;
                                dr["LandLineNo"] = LandLineNo;
                                dr["MobileNo2"] = MobileNo2;
                                dr["LandLineNo2"] = LandLineNo2;
                                dr["PANNo"] = PANNo;
                                dr["CSTNO"] = CSTNO;
                                dr["VATNo"] = VATNo;

                                dr["TINNo"] = TINNo;
                                dr["STNo"] = STNo;
                                dr["GSTNo"] = GSTNo;
                                dr["BankName"] = BankName;
                                dr["BankBranch"] = BankBranch;
                                dr["BankAccountNo"] = BankAccountNo;
                                dr["IFSCCode"] = IFSCCode;
                                dr["AccountsGroup"] = AccountsGroup;
                                dr["TDSLimit"] = TDSLimit;
                                dr["TDSDeclaration"] = TDSDeclaration;
                                dr["Active"] = Active;
                                dr["ServiceTaxApplication"] = ServiceTaxApplication;
                                dr["CreditLimit"] = CreditLimit;
                                dr["CreditDays"] = CreditDays;

                                dtCheck.Rows.Add(dr);
                                dtCheck.AcceptChanges();
                            }
                        }

                        /* Checking Segment & Department more then one or not in same ledger */
                        string xmlTable = string.Empty;
                        xmlTable = ConvertDatatableToXML(dtCheck);
                        string ip_address = Request.UserHostAddress.ToString().Trim();
                        DataTable CheckResult = new DataTable();
                        CheckResult = ClsTPUMaster.SaveUploadVendor(xmlTable, this.Session["IUSERID"].ToString(), ip_address);

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