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
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for MasterService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class MasterService : System.Web.Services.WebService
{
    public MasterService()
    {
        //Uncomment the following line if using designed components  
        //InitializeComponent(); 
    }

    //[WebMethod]
    [WebMethod(EnableSession = true)]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod(EnableSession = true)]
    public List<ListItem> GetDepot()
    {
        List<ListItem> _Depot = new List<ListItem>();
        ClsStockReport clsreport = new ClsStockReport();
        DataTable Depot = new DataTable();
        if (HttpContext.Current.Session["TPU"].ToString() == "ADMIN") // COMPANY USER
        {
            Depot = clsreport.BindDepot_Primary();
            foreach (DataRow dataTableRow in Depot.Rows)
            {
                _Depot.Add(new ListItem
                {
                    Value = dataTableRow["BRID"].ToString(),
                    Text = dataTableRow["BRNAME"].ToString()
                });
            }
        }
        else if (HttpContext.Current.Session["TPU"].ToString() == "D")
        {
            Depot = clsreport.BindDeptName(HttpContext.Current.Session["DEPOTID"].ToString());
            foreach (DataRow dataTableRow in Depot.Rows)
            {
                _Depot.Add(new ListItem
                {
                    Value = dataTableRow["BRID"].ToString(),
                    Text = dataTableRow["BRNAME"].ToString()
                });
            }
        }
        return _Depot;
    }

    [WebMethod(EnableSession = true)]
    public List<ListItem> GetBusinessSegment()
    {
        List<ListItem> _BusinessSegment = new List<ListItem>();
        ClsStockReport clsreport = new ClsStockReport();
        DataTable BS = new DataTable();
        BS = clsreport.BindBusinessegment();
        foreach (DataRow dataTableRow in BS.Rows)
        {
            _BusinessSegment.Add(new ListItem
            {
                Value = dataTableRow["ID"].ToString(),
                Text = dataTableRow["NAME"].ToString()
            });
        }
        return _BusinessSegment;
    }

    [WebMethod(EnableSession = true)]
    public List<ListItem> GetPoNo(string BSID)
    {
        List<ListItem> _PONO = new List<ListItem>();
        ClsStockReport clsreport = new ClsStockReport();
        DataTable dtPoNo = new DataTable();
        dtPoNo = clsreport.BindPO(BSID);
        foreach (DataRow dataTableRow in dtPoNo.Rows)
        {
            _PONO.Add(new ListItem
            {
                Text = dataTableRow["REFERENCESALEORDERNO"].ToString()                
            });
        }
        return _PONO;
    }    
}
