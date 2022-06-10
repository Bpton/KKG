using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;

public partial class VIEW_frmTaxCategoryMapping : System.Web.UI.Page
{
    public class TaxCategoryDetails
    {
       public string CATID { get; set; }
       public string CATNAME { get; set; }
       public string HSN { get; set; }
       public decimal PERCENTAGE { get; set; }
       public DateTime FROMDATE { get; set; }
       public DateTime TODATE { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    [System.Web.Services.WebMethod]
    public static string Main(string TaxId)
    {
        string ret = "";
        ClsTaxSheetMaster objClass = new ClsTaxSheetMaster();
        DataTable objDt = new DataTable();
        objDt = objClass.BindCategoryDetails(TaxId);
        return getdatatabletojson(objDt);
    }

    [System.Web.Services.WebMethod]
    public static string updateTax(string TaxId,string catid, decimal percentage, string fromdate,string todate)
    {
        MemoryStream str = new MemoryStream();
        ClsTaxSheetMaster objClass = new ClsTaxSheetMaster();
        DataTable dtMessage = new DataTable();
        dtMessage.Columns.Add("Messageid", typeof(string));
        dtMessage.Columns.Add("MessageText", typeof(string));

        try
        {

            int ret = 0;
            DataTable objDt = new DataTable();
            objDt.Columns.Add("CATEGORYID", typeof(string));
            objDt.Columns.Add("PERCENTAGE", typeof(string));
            objDt.Columns.Add("FROMDATE", typeof(string));
            objDt.Columns.Add("TODATE", typeof(string));
            DataRow dr = objDt.NewRow();
            dr["CATEGORYID"] = catid;
            dr["PERCENTAGE"] = percentage;
            dr["FROMDATE"] = fromdate;
            dr["TODATE"] = todate;

            objDt.Rows.Add(dr);
            objDt.AcceptChanges();
            string XML = string.Empty;
            objDt.TableName = "XMLData";
            objDt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            ret = objClass.SaveCategoryWiseTaxMapping(TaxId, xmlstr, fromdate, todate);
            DataRow dr1 = dtMessage.NewRow();
            dr1["Messageid"] = ret;
            dr1["MessageText"] = "Done";
            dtMessage.Rows.Add(dr1);
            dtMessage.AcceptChanges();
            
        }
        catch(Exception ex)
        {
            string message = "alert('" + ex.Message.Replace("'", "") + "')";
        }
        return getdatatabletojson(dtMessage);
    }

   

    public static string getdatatabletojson(DataTable dt)
    {
        string str = string.Empty;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        str = serializer.Serialize(rows);
        return str;

    }
}