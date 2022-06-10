using BAL;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.Caching;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Ledger_Report_Services
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Ledger_Report_Services : System.Web.Services.WebService
{
    ObjectCache _MemoryCache = MemoryCache.Default;
    public Ledger_Report_Services()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetTimeSpan(string CalendarType)
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataSet SpanDS = new DataSet();
        DataTable SpanTable = new DataTable();
        string Tag = string.Empty;

        if (CalendarType == "5")
        {
            SpanTable = clsreport.BindJC();
        }
        else
        {
            if (CalendarType == "1")
            {
                Tag = "Y";
            }
            else if (CalendarType == "2")
            {
                Tag = "Q";
            }
            else if (CalendarType == "3")
            {
                Tag = "M";
            }
            else
            {
                Tag = "P";
            }
            string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            SpanTable = clsreport.BindTimeSpan_New(Tag,FinYear);
        }
        SpanTable.TableName = "span";
        SpanDS.Tables.Add(SpanTable);
        return ConvertDatasetToDictionary(SpanDS, "span");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetCalendarDates(string TimeSpan, string CalendarType)
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataSet TimeSpanDS = new DataSet();
        DataTable TimeSpanTable = new DataTable();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        TimeSpanTable = clsreport.FetchDateRange(TimeSpan.Trim(), CalendarType, FinYear);
        TimeSpanTable.TableName = "TimeSpan";
        TimeSpanDS.Tables.Add(TimeSpanTable);
        return ConvertDatasetToDictionary(TimeSpanDS, "TimeSpan");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Bind_sundry_creditors_group()
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataSet sundry_creditors = new DataSet();
        DataTable sundry_creditors1 = new DataTable();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        sundry_creditors1 = clsreport.BindSundryCreditors_bind("","","","1");
        sundry_creditors1.TableName = "sundry_creditors";
        sundry_creditors.Tables.Add(sundry_creditors1);
        return ConvertDatasetToDictionary(sundry_creditors, "sundry_creditors");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Bind_AllSundryGroupList()
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataTable dt_sundry_creditor = new DataTable();
        DataSet ds_sundry_creditor = new DataSet();
        dt_sundry_creditor = clsreport.BindSundryGroupList();
        dt_sundry_creditor.TableName = "sundry_creditors";
        ds_sundry_creditor.Tables.Add(dt_sundry_creditor);
        return ConvertDatasetToDictionary(ds_sundry_creditor, "sundry_creditors");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Bind_sundry_creditors_group_ledger()
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataSet sundry_creditors = new DataSet();
        DataTable sundry_creditors1 = new DataTable();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        sundry_creditors1 = clsreport.BindSundryCreditors_bind("", "", "", "4");
        sundry_creditors1.TableName = "sundry_creditors";
        sundry_creditors.Tables.Add(sundry_creditors1);
        return ConvertDatasetToDictionary(sundry_creditors, "sundry_creditors");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Bind_sundry_creditors_group_ledger_filter(string Groupid)
    {
        ClsStockReport clsreport = new ClsStockReport();
        DataSet sundry_creditors = new DataSet();
        DataTable sundry_creditors1 = new DataTable();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        sundry_creditors1 = clsreport.BindSundryCreditors_bind("", Groupid, "", "3");
        sundry_creditors1.TableName = "sundry_creditors";
        sundry_creditors.Tables.Add(sundry_creditors1);
        return ConvertDatasetToDictionary(sundry_creditors, "sundry_creditors");
    }

    [WebMethod(EnableSession = true)]
    public string Bind_sundry_creditors_Report(string Asondate, string Groupid, string Ledgerid)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        //string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet sundry_creditors = new DataSet();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        sundry_creditors = clsrpt.BindSundryCreditors(Asondate, Groupid, Ledgerid, "2", FinYear);
        string FileName = "sundry_creditors_closing_balance" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/
        DataTable parentTable = new DataTable();
        // DataTable childTable = new DataTable();

        parentTable = sundry_creditors.Tables[0];
        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];

                        colIndex++;
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Bind_Performance_Report(string month, string year, string reporttype)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string UserType = HttpContext.Current.Session["USERTYPE"].ToString().Trim();



        DataSet Performance = new DataSet();
        ClsTargetForecast clsrpt = new ClsTargetForecast();
        Performance = clsrpt.PerformanceReport(month, year, UserID, reporttype, UserType);

        if (Performance.Tables.Contains("Table"))
            Performance.Tables[0].TableName = "ParentData";



        return ConvertDatasetToDictionary_ForGrid(Performance, "ParentData");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Bind_Freesale_Report(string fromdate, string todate, string type,string productType)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();


        _MemoryCache.Remove("Freesale_" + UserID);
        DataSet Freesale = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        Freesale = clsrpt.Bindfreesalereport(fromdate, todate, type, productType);

        if (Freesale.Tables.Contains("Table"))
        Freesale.Tables[0].Columns["zzzzzzzzz"].ColumnName = "TOTAL";
        Freesale.Tables[0].TableName = "ParentData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("Freesale_" + UserID, Freesale, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(Freesale, "ParentData");
    }

    [WebMethod(EnableSession = true)]
    public string GetFreesaleReportinExcel()
    {
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet SalesTaxGSTReportDS = (DataSet)_MemoryCache.Get("Freesale_" + UserID);

        string FileName = "Free Sale Report_" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        //string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;

        //string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/FileUpload\\") + FileName;
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/


        DataTable parentTable = SalesTaxGSTReportDS.Tables[0];
        
        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];
                        colIndex++;
                    }

                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetRegion()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet RegionDS = new DataSet();
        DataTable Depot = new DataTable();
        Depot = clsrpt.Region_foraccounts(Convert.ToString(Session["UTNAME"]).ToLower().Trim(), Convert.ToString(Session["USERID"]));
        Depot.TableName = "Regions";
        RegionDS.Tables.Add(Depot);
      
        return ConvertDatasetToDictionary(RegionDS, "Regions");

    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetReportType()
    {
        ClsTargetForecast clsrpt = new ClsTargetForecast();
        DataSet ReportTypeDS = new DataSet();
        DataTable ReportType = new DataTable();
        ReportType = clsrpt.ReportType_Bind(Convert.ToString(Session["USERTYPE"]).ToLower().Trim());
        ReportType.TableName = "ReportType";
        ReportTypeDS.Tables.Add(ReportType);

        return ConvertDatasetToDictionary(ReportTypeDS, "ReportType");

    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetRegion_V1()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet RegionDS = new DataSet();
        DataTable Depot = new DataTable();
        Depot = clsrpt.BindDepot_Primary_CUMULATIVE();
        Depot.TableName = "Regions";
        RegionDS.Tables.Add(Depot);

        return ConvertDatasetToDictionary(RegionDS, "Regions");

    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetLedgerName(string Depotid)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet LedgerDS = new DataSet();
        DataTable Ledger = new DataTable();
        Ledger = clsrpt.BindForLedgerReport_DepotWise(Depotid.Trim());
        Ledger.TableName = "Ledgers";
        LedgerDS.Tables.Add(Ledger);

        return ConvertDatasetToDictionary(LedgerDS, "Ledgers");

    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Getbusinessegment()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet BsDS = new DataSet();
        DataTable BS = new DataTable();
        BS = clsrpt.BindBusinessegment_PrimarySales();
        BS.TableName = "BsDS";
        BsDS.Tables.Add(BS);

        return ConvertDatasetToDictionary(BsDS, "BsDS");

    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Getstate()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet StateDS = new DataSet();
        DataTable State = new DataTable();
        State = clsrpt.BindState();
        State.TableName = "StateDS";
        StateDS.Tables.Add(State);

        return ConvertDatasetToDictionary(StateDS, "StateDS");

    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetDepotwisesale(string fromdate, string todate, string stateid, string bsid, int amtin)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("Depotwisesales_" + UserID);

        DataSet LedgerReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        LedgerReportDS = clsrpt.Binddepotwisesales_JSON(fromdate, todate, stateid, bsid, amtin);

        if (LedgerReportDS.Tables.Contains("Table"))
            LedgerReportDS.Tables[0].TableName = "ParentData";



        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("Depotwisesales_" + UserID, LedgerReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(LedgerReportDS, "ParentData");
    }


   

    [WebMethod(EnableSession = true)]
    public string GetClosing_Stock_Hirerchy_Wise(string fromdate, string todate,string mode)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet ClosingStockDS = new DataSet(); ;
        ClosingStockDS = clsrpt.BindClosing_Stock_Hirerchy_Wise(fromdate, todate, UserID,mode);
        string FileName = string.Empty;
        string MonthName = string.Empty;
        FileName = "Closing Stock" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/

        DataTable parentTable = new DataTable();
        DataTable childtable = new DataTable();
        DataTable childtable_1 = new DataTable();
        parentTable = ClosingStockDS.Tables[0];
        childtable= ClosingStockDS.Tables[1];
        childtable_1 = ClosingStockDS.Tables[2];
        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet_Extra_header_closing_stock(ws, childtable, ref rowIndex);
                AddHeaderInExcelSheet_Extra_header_grid_closing_stock(ws, parentTable, childtable_1, ref rowIndex);
                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];

                        colIndex++;
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/

    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Getproductnameforproductiofac(string Depotid)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet ProductDS = new DataSet();
        DataTable Product = new DataTable();
        Product = clsrpt.Bind_productfor_production_report(Depotid);
        Product.TableName = "Product";
        ProductDS.Tables.Add(Product);

        return ConvertDatasetToDictionary(ProductDS, "Product");

    }
    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetExpensesLedgerName()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet LedgerDS = new DataSet();
        DataTable Ledger = new DataTable();
        Ledger = clsrpt.BindExpesesLeader();
        Ledger.TableName = "Ledgers";
        LedgerDS.Tables.Add(Ledger);

        return ConvertDatasetToDictionary(LedgerDS, "Ledgers");

    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetLedgerReport(string DateFrom, string DateTo, string LedgerID, string DepotID, string ViewMode)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("LedgerDetailsReport_" + UserID);

        DataSet LedgerReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        LedgerReportDS = clsrpt.BindLedgerReport_JSON(DateFrom, DateTo, LedgerID,DepotID, ViewMode,FinYear);

        if (LedgerReportDS.Tables.Contains("Table"))
            LedgerReportDS.Tables[0].TableName = "ParentData";

        if (LedgerReportDS.Tables.Contains("Table1"))
            LedgerReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("LedgerDetailsReport_" + UserID, LedgerReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(LedgerReportDS, "ParentData");
    }

    /*new report*/
    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetProductExceptFGWP()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        DataSet ProductDS = new DataSet();
        DataTable Product = new DataTable();
        Product = clsrpt.Bind_product();
        Product.TableName = "Product";
        ProductDS.Tables.Add(Product);
        return ConvertDatasetToDictionary(ProductDS, "Product");

    }




    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> bindConSumptionReport(string productid, string fromdate, string todate)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("bindConSumptionReport_" + UserID);

        DataSet LedgerReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        LedgerReportDS = clsrpt.bindConSumptionReport_JSON(productid, fromdate, todate);

        if (LedgerReportDS.Tables.Contains("Table"))
            LedgerReportDS.Tables[0].TableName = "ParentData";

        if (LedgerReportDS.Tables.Contains("Table1"))
            LedgerReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("bindConSumptionReport_" + UserID, LedgerReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(LedgerReportDS, "ParentData");
    }

    /*new report*/


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetDistributorChallanReport(string DateFrom, string DateTo, string DepotID, string ViewMode)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("ChallanReport_" + UserID);

        DataSet LedgerReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        LedgerReportDS = clsrpt.BindDistributorChallanReport(DateFrom, DateTo, DepotID, ViewMode, FinYear);

        if (LedgerReportDS.Tables.Contains("Table"))
            LedgerReportDS.Tables[0].TableName = "ParentData";

        if (LedgerReportDS.Tables.Contains("Table1"))
            LedgerReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("ChallanReport_" + UserID, LedgerReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid1(LedgerReportDS, "ParentData");
    }

    //[WebMethod(EnableSession = true)]
    //public Dictionary<string, object> Getcommisionreport(string finyear, string quarter)
    //{
    //    string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
    //    string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

    //    _MemoryCache.Remove("CommisionReport_" + UserID);

    //    DataSet Commisionds = new DataSet();
    //    ClsOrganisationChart clsrpt = new ClsOrganisationChart();
    //    Commisionds = clsrpt.Bind_Comnmision_json(finyear, quarter);

    //    if (Commisionds.Tables.Contains("Table"))
    //        Commisionds.Tables[0].TableName = "ParentData";

    //    if (Commisionds.Tables.Contains("Table1"))
    //        Commisionds.Tables[1].TableName = "ChildData";

    //    CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
    //    _MemoryCache.Add("CommisionReport_" + UserID, Commisionds, _CacheItemPolicy);

    //    return ConvertDatasetToDictionary_ForGrid(Commisionds, "ParentData");
    //}

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetExpensesLedgerReport(string DateFrom, string DateTo, string LedgerID, string DepotID, string Type)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("ExpensesLedgerDetailsReport_" + UserID);

        DataSet LedgerReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        LedgerReportDS = clsrpt.BindExpensesLedgerReport_JSON(DateFrom, DateTo,  DepotID, LedgerID,  FinYear, Type);

        if (LedgerReportDS.Tables.Contains("Table"))
            LedgerReportDS.Tables[0].TableName = "ParentData";

        if (LedgerReportDS.Tables.Contains("Table1"))
            LedgerReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("ExpensesLedgerDetailsReport_" + UserID, LedgerReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(LedgerReportDS, "ParentData");
    }


    [WebMethod(EnableSession = true)]
    public string GetGstrReport(string fromdate, string todate,string reporttype)
    {
       
        ClsStockReport clsrpt = new ClsStockReport();
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet TargetvsStockDS = new DataSet();
        string FileName = "";
        if (reporttype.Trim() == "G")
        {
            TargetvsStockDS = clsrpt.Bind_Gstr_2A(fromdate, todate);
             FileName = "GSTR_2A_Report_" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        }
        else
        {
            TargetvsStockDS = clsrpt.Bind_Gstr_2A_ISD(fromdate, todate);
             FileName = "GSTR_2A_Report_ISD" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        }

        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/
        DataTable parentTable = new DataTable();
       
        parentTable = TargetvsStockDS.Tables[0];
       
        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);


                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];

                        colIndex++;
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetDistributorChallanDetailsReport(string FilterValue)
    {

        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet LedgerReportDS = (DataSet)_MemoryCache.Get("ChallanReport_" + UserID);
        return ConvertDatasetToDictionary_ForGrid1(LedgerReportDS, "ChildData", FilterValue);
    }





    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetLedgerDetailsReport(string FilterValue)
    {
       
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet LedgerReportDS = (DataSet)_MemoryCache.Get("LedgerDetailsReport_" + UserID);
        return ConvertDatasetToDictionary_ForGrid(LedgerReportDS, "ChildData", FilterValue);
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetLedgerDetailsCostCenterReport(string LedgerID, string AccEntryID)
    {
        ClsStockReport clsrpt = new ClsStockReport();
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();

        DataSet LedgerReportDS_Parent = (DataSet)_MemoryCache.Get("LedgerDetailsReport_" + UserID);
       
        DataSet LedgerReportDS = new DataSet();
        DataTable Ledger = new DataTable();
        LedgerReportDS = clsrpt.BindAccVoucher_Ledger_Voucher(LedgerID.Trim(), AccEntryID.Trim());
        LedgerReportDS.Tables[0].TableName = "ChildData_2";
       

        return ConvertDatasetToDictionary_ForGrid(LedgerReportDS, "ChildData_2", LedgerID);
    }
    [WebMethod(EnableSession = true)]
    public string GetDistributorReportinExcel()
    {
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet LedgerReportDS = (DataSet)_MemoryCache.Get("ChallanReport_" + UserID);

        string FileName = "DistributorWiseChallanReport " + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/
        DataTable parentTable = new DataTable();
        DataTable childTable = new DataTable();
        if (LedgerReportDS.Tables.Count > 1)
        {
            parentTable = LedgerReportDS.Tables[0];
            childTable = LedgerReportDS.Tables[1];
        }
        else
        {
            parentTable = LedgerReportDS.Tables[0];
        }

        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet_challan(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];
                        if (dataTableColumn.ColumnName.Contains("SALEINVOICEDATE"))
                        {
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        colIndex++;
                    }
                    if (childTable.Rows.Count > 1)
                    {
                        string filter = "[SALEINVOICENO]='" + dataTableRow["SALEINVOICENO"] + "'";
                        DataRow[] childRow = childTable.Select(filter);
                        if (childRow.Length > 0)
                        {
                            DataTable temp = childRow.CopyToDataTable();
                            ws.Cells[rowIndex +1, 1].LoadFromDataTable(temp, true);
                            rowIndex = rowIndex + temp.Rows.Count + 1;
                        }
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }

    [WebMethod(EnableSession = true)]
    public string GetLedgerReportinExcel()
    {
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet LedgerReportDS = (DataSet)_MemoryCache.Get("LedgerDetailsReport_" + UserID);

        string FileName = "Ledger Register " + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/
        DataTable parentTable = new DataTable();
        DataTable childTable = new DataTable();
        if (LedgerReportDS.Tables.Count > 1)
        {
             parentTable = LedgerReportDS.Tables[0];
             childTable = LedgerReportDS.Tables[1];
        }
        else {
             parentTable = LedgerReportDS.Tables[0];
        }

        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];
                        if (dataTableColumn.ColumnName.Contains("VoucherDate"))
                        {
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        colIndex++;
                    }

                    string filter = "[VoucherNo]='" + dataTableRow["VoucherNo"] + "'";
                    DataRow[] childRow = childTable.Select(filter);
                    if (childRow.Length > 0)
                    {
                        DataTable temp = childRow.CopyToDataTable();
                        ws.Cells[rowIndex + 1, 1].LoadFromDataTable(temp, true);
                        rowIndex = rowIndex + temp.Rows.Count + 1;
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }

    [WebMethod(EnableSession = true)]
    public string GetExpensesLedgerReportinExcel(string DateFrom, string DateTo, string LedgerID, string DepotID, string Type)
    {
        //ClsStockReport clsrpt = new ClsStockReport();
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        //DataSet DSsecondary_dump = new DataSet(); ;
        //DSsecondary_dump = clsrpt.Bind_secondarydump_JSON(fromdate, todate, depotid);
        DataSet ExpenseLedgerReportDS = (DataSet)_MemoryCache.Get("ExpensesLedgerDetailsReport_" + UserID);
        string FileName = "Expenses of Statements " + (Convert.ToDateTime(DateFrom)).ToString("ddMMMyyyy") + " _ " + (Convert.ToDateTime(DateTo)).ToString("ddMMMyyyy") + ".xlsx";
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/
        DataTable parentTable = new DataTable();
        // DataTable childTable = new DataTable();

        parentTable = ExpenseLedgerReportDS.Tables[0];


        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];

                        colIndex++;
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }

    private Dictionary<string, object> ConvertDatasetToDictionary(DataSet dataset, string TableName = null, int PageNo = 1)
    {
        Dictionary<string, object> ssvalue = new Dictionary<string, object>();

        if (string.IsNullOrEmpty(TableName))
        {
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            TableName = "Users";
            foreach (DataTable table in dataset.Tables)
            {
                if (table.TableName == "UserData")
                {
                    foreach (DataRow row in table.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
            }
            ssvalue.Add(TableName, parentRow);
        }
        else
        {

            foreach (DataTable table in dataset.Tables)
            {
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;
                Dictionary<string, object> FooterRow;
                Dictionary<string, object> TotalRowCount;

                if (table.TableName == "ChildData")
                {
                    int RowNo = 15;

                    if (PageNo > 1)
                    {
                        PageNo = (PageNo - 1) * 15;
                        RowNo = PageNo + 15;
                        if (RowNo > table.Rows.Count)
                            RowNo = table.Rows.Count;
                    }
                    for (int i = PageNo - 1; i < RowNo; i++)
                    {
                        DataRow row = table.Rows[i];
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }


                    FooterRow = new Dictionary<string, object>();
                    List<KeyValuePair<string, string>> Total = new List<KeyValuePair<string, string>>();
                    int colno = 11;
                    foreach (DataColumn col in table.Columns)
                    {
                        double sum = 0;
                        if (col.Ordinal > 10)
                        {
                            foreach (DataRow row in table.Rows)
                            {
                                sum += Convert.ToDouble(row[table.Columns[colno].ToString()]);
                            }
                            Total.Add(new KeyValuePair<string, string>("Total", sum.ToString()));
                            colno++;
                        }
                        else if (col.ColumnName != "ZSM_HQ" && col.ColumnName != "DISTID")
                        {
                            Total.Add(new KeyValuePair<string, string>("TOTAL", ""));
                        }
                        else if (col.ColumnName == "DISTID")
                        {
                            Total.Add(new KeyValuePair<string, string>("DISTID", ""));
                        }
                    }
                    FooterRow.Add("Total", Total);
                    parentRow.Add(FooterRow);

                    TotalRowCount = new Dictionary<string, object>
                    {
                        { "TotalRowCount", table.Rows.Count }
                    };
                    parentRow.Add(TotalRowCount);
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                ssvalue.Add(table.TableName, parentRow);

            }
        }
        return ssvalue;
    }

    private Dictionary<string, object> ConvertDatasetToDictionary_fac(DataSet dataset, string TableName, string FilterValue = null)
    {
        Dictionary<string, object> ssvalue = new Dictionary<string, object>();

        foreach (DataTable table in dataset.Tables)
        {
            if (table.TableName == TableName)
            {
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                if (!string.IsNullOrEmpty(FilterValue))
                {
                    DataRow[] result = table.Select("[PRODUCTNAME]='" + FilterValue + "'");
                    foreach (DataRow row in result)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                ssvalue.Add(TableName, parentRow);
            }
        }
        return ssvalue;
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Getledgerreport_factory(string DateFrom, string DateTo, string DepotID, string LedgerID)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("ledger_fac_" + UserID);

        DataSet Commisionds = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        Commisionds = clsrpt.Bind_ledger_cr_dr_factory(DateFrom, DateTo, DepotID, LedgerID, FinYear);

        if (Commisionds.Tables.Contains("Table"))
            Commisionds.Tables[0].TableName = "ParentData";

        if (Commisionds.Tables.Contains("Table1"))
            Commisionds.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("ledger_fac_" + UserID, Commisionds, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(Commisionds, "ParentData");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> Getbswise_closing(string DateTo, string Depot, string Bsid, string Month, string Finyear)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
      

        _MemoryCache.Remove("bswise_closing_" + UserID);

        DataSet bswiseclosing = new DataSet();
        ClsBusinessSegmentMaster clsrpt = new ClsBusinessSegmentMaster();
        bswiseclosing = clsrpt.Bind_Bswise_closing(DateTo, Depot, Bsid, Month, Finyear);

        if (bswiseclosing.Tables.Contains("Table"))
            bswiseclosing.Tables[0].TableName = "ParentData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("ledger_fac_" + UserID, bswiseclosing, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(bswiseclosing, "ParentData");
    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetProduction_report_factory(string DateFrom, string DateTo, string finyear, string productid, string quater, string depotid, string packsize)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("ledger_fac_" + UserID);

        DataSet Commisionds = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        Commisionds = clsrpt.Bind_Production_report_fac(DateFrom, DateTo, finyear, productid, quater, depotid, packsize);

        if (Commisionds.Tables.Contains("Table"))
            Commisionds.Tables[0].TableName = "ParentData";

        if (Commisionds.Tables.Contains("Table1"))
            Commisionds.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("ledger_fac_" + UserID, Commisionds, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(Commisionds, "ParentData");
    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetProduction_report_factory_details(string FilterValue)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet GetProduction_details = (DataSet)_MemoryCache.Get("ledger_fac_" + UserID);
        return ConvertDatasetToDictionary_fac(GetProduction_details, "ChildData", FilterValue);
    }


    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetAgeingReport(string AsOnDate, string DepotID, string GroupID, string Type, string Showby, string BasedON)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

        _MemoryCache.Remove("AgeingReport_" + UserID);

        DataSet AgeingReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        AgeingReportDS = clsrpt.DebtorsOutstanding(AsOnDate, DepotID, FinYear, GroupID, Type, Showby, BasedON);

        if (AgeingReportDS.Tables.Contains("Table"))
            AgeingReportDS.Tables[0].TableName = "ParentData";

        if (AgeingReportDS.Tables.Contains("Table1"))
            AgeingReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("AgeingReport_" + UserID, AgeingReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(AgeingReportDS, "ParentData");
    }

    //[WebMethod(EnableSession = true)]
    //public Dictionary<string, object> GetDebtorAgeing_Ledger_Branch_Summary_Report(string AsOnDate, string DepotID, string GroupID, string Type, string Showby)
    //{
    //    string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
    //    string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

    //    _MemoryCache.Remove("AgeingReport_" + UserID);

    //    DataSet AgeingReportDS = new DataSet();
    //    ClsStockReport clsrpt = new ClsStockReport();
    //    AgeingReportDS = clsrpt.Debtors_Branch_Ledger_Summary(AsOnDate, DepotID, FinYear, GroupID, Type, Showby);

    //    if (AgeingReportDS.Tables.Contains("Table"))
    //        AgeingReportDS.Tables[0].TableName = "ParentData";

    //    if (AgeingReportDS.Tables.Contains("Table1"))
    //        AgeingReportDS.Tables[1].TableName = "ChildData";

    //    CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
    //    _MemoryCache.Add("AgeingReport_" + UserID, AgeingReportDS, _CacheItemPolicy);

    //    return ConvertDatasetToDictionary_ForGrid(AgeingReportDS, "ParentData");
    //}

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetAgeingBillDetailsReport(string FilterValue)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet AgeingReportDS = (DataSet)_MemoryCache.Get("AgeingReport_" + UserID);
        return ConvertDatasetToDictionary_ForChildGrid(AgeingReportDS, "ChildData", "LedgerName", FilterValue.Trim());
    }

    [WebMethod(EnableSession = true)]

    public Dictionary<string, object> Bind_Production_Report_Factory(string selectdate,string depot)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
        string DepotId = HttpContext.Current.Session["DEPOTID"].ToString().Trim();


        _MemoryCache.Remove("Freesale_" + UserID);
        DataSet Freesale = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        Freesale = clsrpt.BindProductionReport(selectdate, DepotId);

        if (Freesale.Tables.Contains("Table"))
            Freesale.Tables[0].TableName = "ParentData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("Freesale_" + UserID, Freesale, _CacheItemPolicy);

        return ConvertDatasetToDictionary_ForGrid(Freesale, "ParentData");
    }

    [WebMethod(EnableSession = true)]

    public string GetAgeingReportinExcel()
    {
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet AgeingReportDS = (DataSet)_MemoryCache.Get("AgeingReport_" + UserID);

        string FileName = "Ageing Analysis " + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/
        DataTable parentTable = new DataTable();
        DataTable childTable = new DataTable();
        if (AgeingReportDS.Tables.Count > 1)
        {
            parentTable = AgeingReportDS.Tables[0];
            childTable = AgeingReportDS.Tables[1];
        }
        else
        {
            parentTable = AgeingReportDS.Tables[0];
        }

        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];
                        if (dataTableColumn.ColumnName.Contains("BILLDATE") || dataTableColumn.ColumnName.Contains("InvoiceDate"))
                        {
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        colIndex++;
                    }

                    if (childTable.Rows.Count > 0)
                    {
                        string filter = "[LedgerName]='" + dataTableRow["PARTYNAME"] + "'";
                        DataRow[] childRow = childTable.Select(filter);
                        if (childRow.Length > 0)
                        {
                            DataTable temp = childRow.CopyToDataTable();
                            ws.Cells[rowIndex + 1, 1].LoadFromDataTable(temp, true);
                            rowIndex = rowIndex + temp.Rows.Count + 1;
                        }
                    }
                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }

    private Dictionary<string, object> ConvertDatasetToDictionary_ForChildGrid(DataSet dataset, string TableName, string PrimaryKeyName, string FilterValue = null)
    {
        Dictionary<string, object> ssvalue = new Dictionary<string, object>();

        foreach (DataTable table in dataset.Tables)
        {
            if (table.TableName == TableName)
            {
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                if (!string.IsNullOrEmpty(FilterValue))
                {
                    DataRow[] result = table.Select("" + PrimaryKeyName + " = '" + FilterValue + "'");
                    foreach (DataRow row in result)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                ssvalue.Add(TableName, parentRow);
            }
        }
        return ssvalue;
    }


    private Dictionary<string, object> ConvertDatasetToDictionary_ForGrid1(DataSet dataset, string TableName, string FilterValue = null)
    {
        Dictionary<string, object> ssvalue = new Dictionary<string, object>();

        foreach (DataTable table in dataset.Tables)
        {
            if (table.TableName == TableName)
            {
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                if (!string.IsNullOrEmpty(FilterValue))
                {
                    DataRow[] result = table.Select("[SALEINVOICENO]='" + FilterValue + "'");
                    foreach (DataRow row in result)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                ssvalue.Add(TableName, parentRow);
            }
        }
        return ssvalue;
    }
    private Dictionary<string, object> ConvertDatasetToDictionary_ForGrid(DataSet dataset, string TableName, string FilterValue = null)
    {
        Dictionary<string, object> ssvalue = new Dictionary<string, object>();

        foreach (DataTable table in dataset.Tables)
        {
            if (table.TableName == TableName)
            {
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                if (!string.IsNullOrEmpty(FilterValue))
                {
                    DataRow[] result = table.Select("[VoucherNo]='" + FilterValue + "'");
                    foreach (DataRow row in result)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                else
                {
                    foreach (DataRow row in table.Rows)
                    {
                        childRow = new Dictionary<string, object>();
                        foreach (DataColumn col in table.Columns)
                        {
                            childRow.Add(col.ColumnName, row[col]);
                        }
                        parentRow.Add(childRow);
                    }
                }
                ssvalue.Add(TableName, parentRow);
            }
        }
        return ssvalue;
    }
    private void AddHeaderInExcelSheet(ExcelWorksheet ws, DataTable dataTable, ref int rowIndex)
    {
        rowIndex++;
        int colIndex = 1;
        foreach (DataColumn dataTableColumn in dataTable.Columns)
        {
            var cell = ws.Cells[rowIndex, colIndex];
            cell.Style.Font.Bold = true;
            cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            cell.Value = dataTableColumn.ColumnName;
            colIndex++;
        }

    }

    private void AddHeaderInExcelSheet_challan(ExcelWorksheet ws, DataTable dataTable, ref int rowIndex)
    {
        rowIndex++;
        int colIndex = 1;
        foreach (DataColumn dataTableColumn in dataTable.Columns)
        {
            var cell = ws.Cells[rowIndex, colIndex];
            cell.Style.Font.Bold = true;
            cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            cell.Style.Font.Color.SetColor(Color.Blue);
            cell.Value = dataTableColumn.ColumnName;
            colIndex++;
        }

    }


    private void AddHeaderInExcelSheet_Extra_header_closing_stock(ExcelWorksheet ws, DataTable dataTable, ref int rowIndex)
    {
        rowIndex++;
        int colIndex = 5;
        foreach (DataColumn dataTableColumn in dataTable.Columns)
        {
            var cell = ws.Cells[rowIndex, colIndex];
            cell.Style.Font.Bold = true;
            cell.Style.Font.Color.SetColor(Color.Red);
            cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            if (dataTableColumn.ColumnName == "zzzzzzz")
            {
                cell.Value = "Total";
            }
            else
            {
                cell.Value = dataTableColumn.ColumnName;
            }
            colIndex+=7;
        }

    }
    private void AddHeaderInExcelSheet_Extra_header_grid_closing_stock(ExcelWorksheet ws, DataTable parentTable, DataTable childtable_1, ref int rowIndex)
    {
        rowIndex++;
        int colIndex1 = 5;

        for (int i = 0; i < (parentTable.Columns.Count - 4) / 7; i++)
        {
            foreach (DataColumn dataTableColumn in childtable_1.Columns)
            {
                var cell = ws.Cells[rowIndex, colIndex1];
                cell.Style.Font.Bold = true;
                cell.Style.Font.Color.SetColor(Color.Blue);
                cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                cell.Value = dataTableColumn.ColumnName;
                colIndex1++;
            }
        }

      
            var cell1 = ws.Cells[rowIndex,1];
            cell1.Style.Font.Bold = true;
            cell1.Style.Font.Color.SetColor(Color.Blue);
            cell1.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            cell1.Value = "Brand";

            var cell2 = ws.Cells[rowIndex,2];
            cell2.Style.Font.Bold = true;
            cell2.Style.Font.Color.SetColor(Color.Blue);
            cell2.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            cell2.Value = "Category";

            var cell3 = ws.Cells[rowIndex,3];
            cell3.Style.Font.Bold = true;
            cell3.Style.Font.Color.SetColor(Color.Blue);
            cell3.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            cell3.Value = "Product";

            var cell4 = ws.Cells[rowIndex,4];
            cell4.Style.Font.Bold = true;
            cell4.Style.Font.Color.SetColor(Color.Blue);
            cell4.Style.Border.BorderAround(ExcelBorderStyle.Medium);
            cell4.Value = "Sku";
    }

    [WebMethod(EnableSession = true)]  
    public string GetProductionReportFactoeyExportExcel() /*new added by p.basu on 17/12/2019*/
    {
        /*For FTP Start*/
        Clsstockreportnew clsrptnew = new Clsstockreportnew();
        string[] param = clsrptnew.fileparameters();
        string uploadpath = param[0];
        string downloadpath = param[1];
        string ftphost = param[3];
        string ftpuserid = param[4];
        string ftppasseprd = param[5];
        string uploadfolder = param[6];
        ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
        /*For FTP End*/
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet SalesTaxGSTReportDS = (DataSet)_MemoryCache.Get("Freesale_" + UserID);

        string FileName = "Production Report_" + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        //string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;

        //string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/FileUpload\\") + FileName;
        /*For FTP Start*/
        string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
        //string filepath = "\\\\" + uploadpath + FileName;
        /*For FTP End*/

        DataTable parentTable = SalesTaxGSTReportDS.Tables[0];

        if (parentTable.Rows.Count > 0)
        { // File Name of Excel. 
            using (ExcelPackage pack = new ExcelPackage())
            {
                ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
                ////*************** Row by row data

                int rowIndex = 0;
                AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

                foreach (DataRow dataTableRow in parentTable.Rows)
                {
                    int colIndex = 1;
                    rowIndex++;
                    foreach (DataColumn dataTableColumn in parentTable.Columns)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        cell.Value = dataTableRow[dataTableColumn.ColumnName];
                        colIndex++;
                    }

                }
                /*For FTP Start*/
                uploadfolder = uploadfolder + FileName;
                pack.SaveAs(new System.IO.FileInfo(filepath));
                ftpClient.upload(uploadfolder, filepath);
                /*For FTP End*/
            }
        }
        /*For FTP Start*/
        //return "../FileUpload/" + FileName;
        return downloadpath + FileName;
        /*For FTP End*/
    }

    //[WebMethod(EnableSession = true)]
    //public string GetCreditorBillTracker(string REGIONID, string GROUPID)
    //{
    //    string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
    //    string FinYear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
    //    ClsStockReport clsrpt = new ClsStockReport();
    //    /*For FTP Start*/
    //    Clsstockreportnew clsrptnew = new Clsstockreportnew();
    //    string[] param = clsrptnew.fileparameters();
    //    string uploadpath = param[0];
    //    string downloadpath = param[1];
    //    string ftphost = param[3];
    //    string ftpuserid = param[4];
    //    string ftppasseprd = param[5];
    //    string uploadfolder = param[6];
    //    ftp ftpClient = new ftp(ftphost, ftpuserid, ftppasseprd);
    //    /*For FTP End*/
    //    DataSet ReportDS = new DataSet();
    //    ReportDS = clsrpt.BindCreditorBillTracker(REGIONID, GROUPID, FinYear, UserID);
    //    string FileName = string.Empty;
    //    FileName = "BillwiseTracker_" + DateTime.Now.ToString("ddMMMyyyy") + ".xlsx";

    //    string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;
    //    DataTable parentTable = new DataTable();
    //    parentTable = ReportDS.Tables[0];
    //    if (parentTable.Rows.Count > 0)
    //    { // File Name of Excel. 
    //        using (ExcelPackage pack = new ExcelPackage())
    //        {
    //            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(FileName);
    //            ////*************** Row by row data

    //            int rowIndex = 0;
    //            AddHeaderInExcelSheet(ws, parentTable, ref rowIndex);

    //            foreach (DataRow dataTableRow in parentTable.Rows)
    //            {
    //                int colIndex = 1;
    //                rowIndex++;
    //                foreach (DataColumn dataTableColumn in parentTable.Columns)
    //                {
    //                    var cell = ws.Cells[rowIndex, colIndex];
    //                    cell.Style.Border.BorderAround(ExcelBorderStyle.Medium);
    //                    cell.Value = dataTableRow[dataTableColumn.ColumnName];

    //                    colIndex++;
    //                }
    //            }
    //            /*For FTP Start*/
    //            uploadfolder = uploadfolder + FileName;
    //            pack.SaveAs(new System.IO.FileInfo(filepath));
    //            ftpClient.upload(uploadfolder, filepath);
    //            /*For FTP End*/
    //        }
    //    }
    //    /*For FTP Start*/
    //    //return "../FileUpload/" + FileName;
    //    return downloadpath + FileName;
    //    /*For FTP End*/
    //}
}
