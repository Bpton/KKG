using DAL;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using Utility;
using System.Collections.Generic;

namespace BAL
{
  
    public class ClsEinvoice
    {
        DBUtils db = new DBUtils();
        private SqlCommand cmd;
        public SqlConnection cvCon = new SqlConnection(ConfigurationManager.ConnectionStrings["constring"].ToString());
        public DataSet ds = new DataSet();
        public int i;
        private SqlDataAdapter da;

        public DataTable BindApprovedInvoice(string DepotID, string Fromdate, string Todate,string Finyear)
        {
            string sql = " EXEC USP_GET_APPROVED_INVOICE '"+ Fromdate + "','"+ Todate + "','"+ DepotID + "','"+ Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable updateeinvoice(string doctype, string invoiceno, string invoicedt, string ackno, string ackdate,string irn, string qrcode)
        {
            string sqlprocstock = "EXEC [Usp_Einvoice_responseupdate] '" + doctype + "','" + invoiceno + "','" + invoicedt + "','" + ackno + "','" + ackdate + "','" + irn + "','" + qrcode + "'";
            DataTable dtflag = db.GetData(sqlprocstock);
            return dtflag;
        }
        public DataTable CreateJson(string InvoiceID)
        {
            string sql = " EXEC USP_GST_GOV_V2 '" + InvoiceID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        protected string Convert_To_YYYYMMDD(string pDate)
        {

            string sDate = "";

            string dd = "";
            string mm = "";
            string yy = "";


            dd = pDate.Substring(0, 2);
            mm = pDate.Substring(3, 2);
            yy = pDate.Substring(6, 4);




            sDate = yy + "-" + mm + "-" + dd;




            return sDate;
        }


        

    }

    

}
