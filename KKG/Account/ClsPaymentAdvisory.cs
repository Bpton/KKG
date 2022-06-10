using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DAL;
using System.Data;
using Utility;
using System.Globalization;

namespace Account
{
    public class ClsPaymentAdvisory
    {
        DBUtils db = new DBUtils();

        public DataTable BindVendor(string BranchID)
        {
            string sql = "EXEC USP_CREDITORS_PARTY_BRANCHWISE '" + BranchID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPartyBankDetails(string PartyID)
        {
            string sql = "EXEC USP_PARTY_BANKDETAILS '" + PartyID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable PaymentAdvisoryRegion()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_PAYMENTADVISORY_BRANCHLIST";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable PaymentAddPulateGrid(string vendorid, string branchid,string frmdate,string toddate)
        {
            string sql = "EXEC USP_PAYMENTADVISORY_VENDOR_BILLDETAILS '" + vendorid + "','" + branchid + "','" + frmdate + "','" + toddate + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string InsertPaymentAdvisoryDetails(string PaymentAdvisoryId, string Mode, string PaymentAdvisoryDate, string BranchID, string Remarks, string FinYear, string UserID, decimal BillTotal, decimal TotalAmount, string xml, string Narration)
        {
            string PaymentID = "";
            string sql = "EXEC [USP_PAYMENT_ADVISERY_INSERT_UPDATE] '" + PaymentAdvisoryId + "','" + Mode + "','" + PaymentAdvisoryDate + "','" + BranchID + "','" + Remarks + "','" + FinYear + "','" + UserID + "'," + BillTotal + "," + TotalAmount + ",'" + xml + "','" + Narration + "'";
            PaymentID = (string)db.GetSingleValue(sql);

            return PaymentID;
        }

        public DataTable PaymentAddHeaderGrid(string vendorid, string fromdate, string todate, string Ischecker)
        {
            string sql = "EXEC USP_PAYMENTADVISORY_HEADER '" + vendorid + "','" + fromdate + "','" + todate + "','" + Ischecker + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet PaymentDetails(string PaymentID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_PAYMENTADVISERY_DETAILS '" + PaymentID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string HDFCBankID()
        {
            string HDFCId = "";
            string sql = "SELECT ISNULL(HDFCBANKID,'') AS HDFCBANKID FROM P_APPMASTER";
            HDFCId = (string)db.GetSingleValue(sql);

            return HDFCId;
        }

        public DataTable PaymentView(string PaymentID)
        {
            string sql = " SELECT PAYMENTADVISERYID,PAYMENTADVISERYNO,VENDORID,AMOUNT FROM Vw_ACC_PAYMENT_ADVISERY WHERE PAYMENTADVISERYID='" + PaymentID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string InsertPaymentAdvisoryFileContent(string PaymentAdvisoryId, string FileContent, string FileTextName)
        {
            string PaymentID = "";
            string sql = "EXEC USP_ACC_PAYMENT_ADVISERY_FILECONTENT_INSERT '" + PaymentAdvisoryId + "','" + FileContent + "','" + FileTextName + "'";
            PaymentID = (string)db.GetSingleValue(sql);

            return PaymentID;
        }

        public string PaymentAdvisoryDelete(string PaymentAdvisoryId)
        {
            string _status = "";
            string sql = "EXEC USP_PAYMENTADVISORY_DELETE '" + PaymentAdvisoryId + "'";
            _status = (string)db.GetSingleValue(sql);

            return _status;
        }
    }
}
