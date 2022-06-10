using BAL;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class VIEW_frmProductUpload : System.Web.UI.Page
{
    ClsProductMaster ClsProduct = new ClsProductMaster();
    DataTable dtclone = new DataTable();

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
        dt.Columns.Add("ShortName", typeof(string));
        //dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("BRANDSTAGE1", typeof(string));
        dt.Columns.Add("CATNAMESTAGE2", typeof(string));
        dt.Columns.Add("MainGroup", typeof(string));
        dt.Columns.Add("UOM", typeof(string));
        dt.Columns.Add("SWEEPSTAGE4", typeof(string));
        dt.Columns.Add("COLORSTAGE5", typeof(string));
        dt.Columns.Add("TYPESTAGE3", typeof(string));
        dt.Columns.Add("BestBeforeUse", typeof(string));
        dt.Columns.Add("NR", typeof(string));
        dt.Columns.Add("BrandCategoryName", typeof(string));        
        dt.Columns.Add("Assessable", typeof(string));        
        dt.Columns.Add("Returnable", typeof(string));
        dt.Columns.Add("Active", typeof(string));        
        dt.Columns.Add("PowerSKU", typeof(string));
        dt.Columns.Add("Barcode", typeof(string));
        dt.Columns.Add("MINSTOCKLEVEL", typeof(string));
        dt.Columns.Add("GROSSWEIGHT", typeof(string));
        dt.Columns.Add("PACKSIZE", typeof(string));
        HttpContext.Current.Session["ProductUpload"] = dt;
    }
    #endregion    

    #region btnUpload_Click
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {            
            //string VoucherTypeID = string.Empty;
            string SLNO = string.Empty;
            string ShortName = string.Empty;
            //string Name = string.Empty;
            string Brand = string.Empty;
            string CatName = string.Empty;
            string MainGroup = string.Empty;
            string UOM = string.Empty;
            string UnitCapacity = string.Empty;
            string Color = string.Empty;
            string ItemType = string.Empty;
            string BestBeforeUse = string.Empty;
            string NR = string.Empty;            
            string Barcode = string.Empty;
            string PowerSKU = string.Empty;            
            string Active = string.Empty;
            string Returnable = string.Empty;            
            string Assessable = string.Empty;            
            string BrandCategoryName = string.Empty;
            string MINSTOCKLEVEL = string.Empty;
            string GrossWeight = string.Empty;
            string PACKSIZE = string.Empty;

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
                            if (dt.Rows[k]["SHORT NAME*"].ToString().Trim() != "")
                            {
                                SLNO = dt.Rows[k]["SL NO*"].ToString().Trim();
                                ShortName = dt.Rows[k]["SHORT NAME*"].ToString().Trim();
                                //Name = dt.Rows[k]["NAME"].ToString().Trim();
                                Brand = dt.Rows[k]["BRAND STAGE 1*"].ToString().Trim();
                                CatName = dt.Rows[k]["CATNAME STAGE 2*"].ToString().Trim();
                                MainGroup = dt.Rows[k]["MainGroup*"].ToString().Trim();
                                UOM = dt.Rows[k]["UOM*"].ToString().Trim();
                                UnitCapacity = dt.Rows[k]["SWEEP STAGE 4*"].ToString().Trim();
                                Color = dt.Rows[k]["COLOR STAGE 5*"].ToString().Trim();
                                ItemType = dt.Rows[k]["TYPE STAGE 3*"].ToString().Trim();
                                BestBeforeUse = dt.Rows[k]["BEST BEFORE USE(Days)*"].ToString().Trim();
                                NR = dt.Rows[k]["INR (RS)*"].ToString().Trim();                                
                                BrandCategoryName = dt.Rows[k]["BRAND CATEGORY NAME"].ToString().Trim();                                
                                Assessable = dt.Rows[k]["ASSESSABLE(Per)"].ToString().Trim();                                
                                Returnable = dt.Rows[k]["RETURNABLE"].ToString().Trim();
                                Active = dt.Rows[k]["ACTIVE"].ToString().Trim();                                
                                PowerSKU = dt.Rows[k]["POWER SKU"].ToString().Trim();
                                Barcode = dt.Rows[k]["BARCODE"].ToString().Trim();
                                MINSTOCKLEVEL = dt.Rows[k]["MIMIMUM STOCK LEVEL*"].ToString().Trim();
                                GrossWeight = dt.Rows[k]["GROSS WEIGHT*"].ToString().Trim();
                                PACKSIZE = dt.Rows[k]["PACK SIZE*"].ToString().Trim();

                                if (ShortName == "")
                                {
                                    MessageBox1.ShowInfo("<b>Short NAME can't allow space ..!! <font color=red> '" + ShortName.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (Brand == "")
                                {
                                    MessageBox1.ShowInfo("<b>Brand Stage 1 can't allow space ..!! <font color=red> '" + Brand.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (CatName == "")
                                {
                                    MessageBox1.ShowInfo("<b>Catname Stage 2 can't allow space ..!! <font color=red> '" + CatName.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (MainGroup == "")
                                {
                                    MessageBox1.ShowInfo("<b>MainGroup can't allow space ..!! <font color=red> '" + MainGroup.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (UOM == "")
                                {
                                    MessageBox1.ShowInfo("<b>UOM can't allow space ..!! <font color=red> '" + UOM.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (UnitCapacity == "")
                                {
                                    MessageBox1.ShowInfo("<b>Sweep Stage 4 can't allow space ..!! <font color=red> '" + UnitCapacity.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (Color == "")
                                {
                                    MessageBox1.ShowInfo("<b>Color Stage 5 can't allow space ..!! <font color=red> '" + Color.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (ItemType == "")
                                {
                                    MessageBox1.ShowInfo("<b>TYPE STAGE 3 can't allow space ..!! <font color=red> '" + ItemType.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (BestBeforeUse == "")
                                {
                                    MessageBox1.ShowInfo("<b>Best Before Use can't allow space ..!! <font color=red> '" + BestBeforeUse.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                else if (NR == "")
                                {
                                    MessageBox1.ShowInfo("<b>INR (RS) can't allow space ..!! <font color=red> '" + NR.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                if (MINSTOCKLEVEL == "")
                                {
                                    //MessageBox1.ShowInfo("<b>MIN STOCK LEVEL can't allow space ..!! <font color=red> '" + MINSTOCKLEVEL.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    //return;
                                    MINSTOCKLEVEL = "1";
                                }
                                if (!string.IsNullOrEmpty(NR.ToString()))
                                {
                                    Decimal valueINR;
                                    if (Decimal.TryParse(NR, out valueINR))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for INR !! <font color=red> '" + NR.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }
                                }

                                if (!string.IsNullOrEmpty(BestBeforeUse.ToString()))
                                {
                                    int valueBestBeforeUse;
                                    if (Int32.TryParse(BestBeforeUse, out valueBestBeforeUse))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for BEST BEFORE USE(Days) !! <font color=red> '" + BestBeforeUse.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }
                                }
                                if (!string.IsNullOrEmpty(Assessable.ToString()))
                                {
                                    decimal valueAssessable;
                                    if (Decimal.TryParse(Assessable, out valueAssessable))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for ASSESSABLE(Per) !! <font color=red> '" + Assessable.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }
                                }

                                decimal valueGrossWeight;
                                if (Decimal.TryParse(GrossWeight, out valueGrossWeight))
                                {

                                }
                                else
                                {
                                    MessageBox1.ShowInfo("<b>Please valid input for Gross Weight !! <font color=red> '" + GrossWeight.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                decimal valueMINSTOCKLEVEL;
                                if (Decimal.TryParse(MINSTOCKLEVEL, out valueMINSTOCKLEVEL))
                                {

                                }
                                else
                                {
                                    MessageBox1.ShowInfo("<b>Please valid input for MIN STOCK LEVEL !! <font color=red> '" + MINSTOCKLEVEL.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                    return;
                                }
                                if (!string.IsNullOrEmpty(PACKSIZE.ToString()))
                                {
                                    Decimal valuePACKSIZE;
                                    if (Decimal.TryParse(PACKSIZE, out valuePACKSIZE))
                                    {

                                    }
                                    else
                                    {
                                        MessageBox1.ShowInfo("<b>Please valid input for PACK SIZE !! <font color=red> '" + PACKSIZE.ToString() + "' </font> Line Number " + SLNO.ToString() + "</b>", 60, 500);
                                        return;
                                    }
                                }

                                DataRow dr = dtCheck.NewRow();
                                dr["SLNO"] = SLNO;
                                dr["ShortName"] = ShortName;
                                //dr["Name"] = Name;
                                dr["BRANDSTAGE1"] = Brand;
                                dr["CATNAMESTAGE2"] = CatName;
                                dr["MainGroup"] = MainGroup;
                                dr["UOM"] = UOM;
                                dr["SWEEPSTAGE4"] = UnitCapacity;
                                dr["COLORSTAGE5"] = Color;
                                dr["TYPESTAGE3"] = ItemType;
                                dr["BestBeforeUse"] = BestBeforeUse;
                                dr["NR"] = NR;                                
                                dr["BrandCategoryName"] = BrandCategoryName;                                
                                dr["Assessable"] = Assessable;                                
                                dr["Returnable"] = Returnable;
                                dr["Active"] = Active;                                
                                dr["PowerSKU"] = PowerSKU;
                                dr["Barcode"] = Barcode;
                                dr["MINSTOCKLEVEL"] = MINSTOCKLEVEL;
                                dr["GROSSWEIGHT"] = GrossWeight;
                                dr["PACKSIZE"] = PACKSIZE;

                                dtCheck.Rows.Add(dr);
                                dtCheck.AcceptChanges();
                            }
                        }

                        /* Checking Segment & Department more then one or not in same ledger */
                        string xmlTable = string.Empty;
                        xmlTable = ConvertDatatableToXML(dtCheck);
                        string ip_address = Request.UserHostAddress.ToString().Trim();
                        DataTable CheckResult = new DataTable();
                        CheckResult = ClsProduct.SaveUploadProduct(xmlTable, this.Session["IUSERID"].ToString(), ip_address);

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