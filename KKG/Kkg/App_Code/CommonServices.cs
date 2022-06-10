using System.Data;
using System.Web.Services;
using BAL;
using Newtonsoft.Json;
using System.Runtime.Caching;
using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

/// <summary>
/// Summary description for CommonServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class CommonServices : System.Web.Services.WebService
{
    ObjectCache _MemoryCache = MemoryCache.Default;


    public CommonServices()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetSalesTaxGSTReport(string DateFrom, string DateTo, string DepotID, string Scheme, string Segment, string Type, string TaxTypeID, string PartyID, string InvoiceType, string Details)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();

        _MemoryCache.Remove("SalesTaxGSTReport_" + UserID);

        DataSet SalesTaxGSTReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        SalesTaxGSTReportDS = clsrpt.BindSalesTaxSummary_Details_GST(DateFrom, DateTo, DepotID, Scheme, Segment, Type, TaxTypeID, PartyID, InvoiceType, Details);

        if (SalesTaxGSTReportDS.Tables.Contains("Table"))
            SalesTaxGSTReportDS.Tables[0].TableName = "ParentData";

        if (SalesTaxGSTReportDS.Tables.Contains("Table1"))
            SalesTaxGSTReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("SalesTaxGSTReport_" + UserID, SalesTaxGSTReportDS, _CacheItemPolicy);

        return ConvertDatasetToDictionary(SalesTaxGSTReportDS, "ParentData");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetSalesTaxGSTReportDetail(string FilterValue)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet SalesTaxGSTReportDS = (DataSet)_MemoryCache.Get("SalesTaxGSTReport_" + UserID);
        return ConvertDatasetToDictionary(SalesTaxGSTReportDS, "ChildData", FilterValue);
    }

    [WebMethod(EnableSession = true)]
    public string GetSalesTaxGSTReportinExcel()
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet SalesTaxGSTReportDS = (DataSet)_MemoryCache.Get("SalesTaxGSTReport_" + UserID);

        string FileName = "Training Register " + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        //string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;

        string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/FileUpload\\") + FileName;

        DataTable parentTable = SalesTaxGSTReportDS.Tables[0];
        DataTable childTable = SalesTaxGSTReportDS.Tables[1];

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
                        if (dataTableColumn.ColumnName.Contains("Date"))
                        {
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        colIndex++;
                    }

                    string filter = "[VCH NO.]='" + dataTableRow["VCH NO."] + "'";
                    DataRow[] childRow = childTable.Select(filter);
                    if (childRow.Length > 0)
                    {
                        DataTable temp = childRow.CopyToDataTable();
                        ws.Cells[rowIndex + 1, 1].LoadFromDataTable(temp, true);
                        rowIndex = rowIndex + temp.Rows.Count + 1;
                    }
                }
                pack.SaveAs(new System.IO.FileInfo(filepath));
            }
        }
        return "../FileUpload/" + FileName;
    }

    private Dictionary<string, object> ConvertDatasetToDictionary(DataSet dataset, string TableName, string FilterValue = null)
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
                    DataRow[] result = table.Select("[VCH NO.]='" + FilterValue + "'");
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

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetPurchaseTaxGSTReport(string DateFrom, string DateTo, string DepotID, string PartyID, string InvoiceType,string VoucherType)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();

        _MemoryCache.Remove("PurchaseTaxGSTReport_" + UserID);

        DataSet PurchaseTaxGSTReportDS = new DataSet();
        ClsStockReport clsrpt = new ClsStockReport();
        /*if (InvoiceType == "0")
        {
            PurchaseTaxGSTReportDS = clsrpt.BindSummaryDetails_GST(DateFrom, DateTo, DepotID,PartyID);
        }
        else
        {*/
            PurchaseTaxGSTReportDS = clsrpt.BindSummaryDetails_GST(DateFrom, DateTo, DepotID,PartyID, InvoiceType, VoucherType);
        /*}*/

        if (PurchaseTaxGSTReportDS.Tables.Contains("Table"))
            PurchaseTaxGSTReportDS.Tables[0].TableName = "ParentData";

        if (PurchaseTaxGSTReportDS.Tables.Contains("Table1"))
            PurchaseTaxGSTReportDS.Tables[1].TableName = "ChildData";

        CacheItemPolicy _CacheItemPolicy = new CacheItemPolicy() { AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddMinutes(15)) };
        _MemoryCache.Add("PurchaseTaxGSTReport_" + UserID, PurchaseTaxGSTReportDS, _CacheItemPolicy);

        return ConvertPurchaseDatasetToDictionary(PurchaseTaxGSTReportDS, "ParentData");
    }

    [WebMethod(EnableSession = true)]
    public Dictionary<string, object> GetPurchaseTaxGSTReportDetail(string FilterValue)
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet PurchaseTaxGSTReportDS = (DataSet)_MemoryCache.Get("PurchaseTaxGSTReport_" + UserID);
        return ConvertPurchaseDatasetToDictionary(PurchaseTaxGSTReportDS, "ChildData", FilterValue);
    }

    [WebMethod(EnableSession = true)]
    public string GetPurchaseTaxGSTReportinExcel()
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet PurchaseTaxGSTReportDS = (DataSet)_MemoryCache.Get("PurchaseTaxGSTReport_" + UserID);

        string FileName = "Purchase Register " + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";
        //string filepath = Server.MapPath("../") + "FileUpload\\" + FileName;

        string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/FileUpload\\") + FileName;

        DataTable parentTable = PurchaseTaxGSTReportDS.Tables[0];
        DataTable childTable = PurchaseTaxGSTReportDS.Tables[1];

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
                        if (dataTableColumn.ColumnName.Contains("Date"))
                        {
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        colIndex++;
                    }

                    string filter = "[VOUCHER NO.]='" + dataTableRow["VOUCHER NO."] + "'";
                    DataRow[] childRow = childTable.Select(filter);
                    if (childRow.Length > 0)
                    {
                        DataTable temp = childRow.CopyToDataTable();
                        ws.Cells[rowIndex + 1, 1].LoadFromDataTable(temp, true);
                        rowIndex = rowIndex + temp.Rows.Count + 1;
                    }
                }
                pack.SaveAs(new System.IO.FileInfo(filepath));
            }
        }
        return "../FileUpload/" + FileName;
    }

    [WebMethod(EnableSession = true)]
    public string GetPurchaseTaxGSTReportinExcelHeader()
    {
        string UserID = HttpContext.Current.Session["USERID"].ToString().Trim();
        DataSet PurchaseTaxGSTReportDS = (DataSet)_MemoryCache.Get("PurchaseTaxGSTReport_" + UserID);

        string FileName = "Purchase Register " + DateTime.Now.ToString("ddMMMyyyyHHmmss") + ".xlsx";

        string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/FileUpload\\") + FileName;

        DataTable parentTable = PurchaseTaxGSTReportDS.Tables[0];

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
                        if (dataTableColumn.ColumnName.Contains("Date"))
                        {
                            cell.Style.Numberformat.Format = "dd/mm/yyyy";
                        }
                        colIndex++;
                    }
                }
                pack.SaveAs(new System.IO.FileInfo(filepath));
            }
        }
        return "../FileUpload/" + FileName;
    }



    private Dictionary<string, object> ConvertPurchaseDatasetToDictionary(DataSet dataset, string TableName, string FilterValue = null)
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
                    DataRow[] result = table.Select("[VOUCHER NO.]='" + FilterValue + "'");
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
    public string Main()
    {
        ClsStockReport clsrpt = new ClsStockReport();
        // Creating a new instance of class studentInfo
        List<Customer> customers = new List<Customer>();
        DataTable dt = clsrpt.FecthData();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Customer customer = new Customer
            {
                PRODUCTNAME = Convert.ToString(dt.Rows[i]["PRODUCTNAME"]),
                NAME = Convert.ToString(dt.Rows[i]["NAME"]),
                QTY = Convert.ToDecimal(dt.Rows[i]["QTY"]),
                VALUE = Convert.ToDecimal(dt.Rows[i]["VALUE"]),
            };
            customers.Add(customer);
        }
        string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
        string res = string.Empty;
        File.WriteAllText(Server.MapPath("~/data.json"), json);
        return res; 
    }

    public class Customer
    {
        public string PRODUCTNAME { get; set; }
        public string NAME { get; set; }
       
        public decimal QTY { get; set; }
        public decimal VALUE { get; set; }
        
    }


}
