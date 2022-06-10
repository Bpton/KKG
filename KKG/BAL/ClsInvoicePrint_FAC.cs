using DAL;
using System.Data;

namespace BAL
{
    public class ClsInvoicePrint_FAC
    {
        DBUtils db = new DBUtils();
        public DataTable BindFactoryDespatch_Header(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_FACTORY_STOCKDESPATCH_HEADER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindST_Details_GST(string STNO, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCKDESPATCH_DETAILS_GST] '" + STNO + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_CompanyInfo()
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_COMPANY_INFO]";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet Bind_StockTransferHeader_Print(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "[dbo].[SP_RPT_STOCKDESPATCH_HEADER_PRINT] '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataTable Bind_InvoiceType(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_CHEAK_INVOICE_TYPE] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDetails_GST_Print(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_INVOICE_DETAILS_GST_PRINT_FAC] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet Bind_InvoiceHeader_Print(string InvoiceID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC [dbo].[USP_RPT_SALE_INVOICE_HEADER_GST_PRINT] '" + InvoiceID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        #region Add By Rajeev For PurchaseReturn Print

       /* public DataTable Bind_InvoiceDetails_PurchaseReturn(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PURCHASE_RETURN_DETAILS] '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }*/

        public DataTable Bind_InvoiceDetails_PurchaseReturn(string InvoiceID,string original, string duplicate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PURCHASE_RETURN_DETAILS] '" + InvoiceID + "','"+ original + "','"+ duplicate + "','"+ triplicate + "','"+ extra + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceHeader_PurchaseReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PURCHASE_RETURN_HEADER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceTax_PurchaseReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PURCHASE_RETURN_ITEMWISE_TAX] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceFooter_PurchaseReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_PURCHASE_RETURN_FOOTER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceVendor_PurchaseReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_SALE_RETURN_VENDORINFO] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #endregion

        #region Stock Journal Print
        public DataTable BindStockjournalheader(string ADJUSTMENTID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCKJOURNAL_HEADER]'" + ADJUSTMENTID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStockjournaldetails(string ADJUSTMENTID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_STOCKJOURNAL_DETAILS]'" + ADJUSTMENTID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region TRANSPORTERBILL
        public DataTable BindTransporterBillHeader(string BillID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_HEADER] '" + BillID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillDetails(string BillID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_DETAILS] '" + BillID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterBillFooter(string BillID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TRANSPORTER_BILL_FOOTER] '" + BillID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Depot Stock Transfer Print(Factory)
        public DataTable BindSR_Transfer(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_TRANSFER] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_FOOTER_GST(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_FOOTER_GST] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_Items_GST(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_ITEMS_GST] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_RecvdDepo(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_RECVDDEPO] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSR_SendDepo(string STNO)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_RPT_STOCK_RECEIPT_SENDDEPO] '" + STNO + "'";
            dt = db.GetData(sql);
            return dt;
        }

        #endregion


        #region salereturn
        public DataTable BindSalereturnDetails(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_GROSS_RETURN_DETAILS_GST_PRINT_MM]'" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindSalereturnHeader(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_GROSS_RETURN_HEADER_MM_GST_PRINT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }



        public DataTable BindSalereturnFooter(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALE_GROOS_RETURN_FOOTER_MM_PRINT_GST] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_InvoiceDistributorReturn(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[SP_RPT_DISTRIBUTORINFO_SALERETURN_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_ReturnFromDepot(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALERETURN_FROMDEPOT_DETAILS_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_Returntype(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_CHEAK_RETURN_TYPE_MM] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        #region Import Purchase Print
        public DataTable Bind_ImportPurchaseHeader(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_IMPORT_HEADER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_ImportPurchaseDetails(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_IMPORT_PURCHASE_PRINT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_ImportPurchaseTransporter(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_IMPORT_TRANSPORTER] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region joborder
        public DataTable Joborderdetails_print(string stockdespatchid)
        {
            DataTable ds = new DataTable();
            string sql = "[dbo].[USP_RPT_JOB_ORDER_DETAILS] '" + stockdespatchid + "'";
            ds = db.GetData(sql);
            return ds;
        }

        public DataTable Joborderheader_print(string stockdespatchid)
        {
            DataTable ds = new DataTable();
            string sql = "[dbo].[USP_RPT_JOB_ORDER_HEADER] '" + stockdespatchid + "'";
            ds = db.GetData(sql);
            return ds;
        }
        #endregion

        public DataTable Bind_InvoiceDetails_Corporate_GST_Print(string InvoiceID, string original, string duplcatate, string triplicate, string extra)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_RPT_CORPORATE_SALE_INVOICE_FAC_GST_PRINT '" + InvoiceID + "','" + original + "','" + duplcatate + "','" + triplicate + "','" + extra + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_Combo(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_SALEINVOICE_COMBO_PRODUCT] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_Headerexport(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_EXPORT_INVOICE_HEADER_DETAILS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bind_ExportInvoice_GST(string InvoiceID, string original, string duplicate, string triplicate)
        {
            DataTable dt = new DataTable();
            string sql = "[dbo].[USP_RPT_EXPORT_INVOICE_GST] '" + InvoiceID + "','" + original + "','" + duplicate + "','" + triplicate + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bind_ExportInvoice(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [dbo].[USP_RPT_EXPORT_INVOICE_FAC] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProforma(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PROFORMA_CREDITNOTE_FAC] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTerms_Proforma(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PROFORMA_INVOICE_TERMS_CONDITIONS] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTerms_PaclingList(string InvoiceID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_PACKINGLIST_FAC] '" + InvoiceID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRejectionHeader(string RejectionID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_GRNREJECTION_HEADER] '" + RejectionID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRejectionDetails(string RejectionID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_GRNREJECTION_DETAILS] '" + RejectionID + "'";
            dt = db.GetData(sql);
            return dt;
        }
    }
}
