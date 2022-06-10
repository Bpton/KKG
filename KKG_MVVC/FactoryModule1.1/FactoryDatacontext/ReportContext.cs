using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Dapper;
using FactoryModel;
using System.Web.UI;

namespace FactoryDatacontext
{
    public class ReportContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<ReportBranch> BindDepot_Primary(string mode,string id)
        {
            List<ReportBranch> saleOrders = new List<ReportBranch>();
            try
            {
                saleOrders = _db.Query<ReportBranch>("USP_BIND_BRANCH_MASTER", new {@p_mode= mode,@p_id= id }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return saleOrders;
        }
        //public List<Branch> BindDeptName()
        //{
        //    List<Branch> saleOrders = new List<Branch>();
        //    try
        //    {
        //        saleOrders = _db.Query<Branch>("USP_BIND_LOAD_V2", new {}, commandType: CommandType.StoredProcedure).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //    }
        //    return saleOrders;
        //}
        //public List<Branch> Depot_Accounts()
        //{
        //    List<Branch> saleOrders = new List<Branch>();
        //    try
        //    {
        //        saleOrders = _db.Query<Branch>("USP_BIND_LOAD_V2", new {}, commandType: CommandType.StoredProcedure).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = "alert('" + ex.Message.Replace("'", "") + "')";
        //    }
        //    return saleOrders;
        //}
        public List<ReportProductlias> LoadProductALIAS()
        {
            List<ReportProductlias> saleOrders = new List<ReportProductlias>();
            try
            {
                saleOrders = _db.Query<ReportProductlias>("USP_PRODUCT_MASTER_V2", new {}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return saleOrders;
        }
        public List<ReportBranch> LoadRegion_foraccounts(string mode,string userid)
        {
            List<ReportBranch> reportBranches = new List<ReportBranch>();
            try
            {
                reportBranches = _db.Query<ReportBranch>("USP_BIND_BRANCH_MASTER", new { @p_mode = mode, @P_userid = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return reportBranches;
        }
        public List<ReportStorelocation> LoadStorelocation()
        {
            List<ReportStorelocation> saleOrders = new List<ReportStorelocation>();
            try
            {
                saleOrders = _db.Query<ReportStorelocation>("usp_bind_storelocation_v2", new {}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return saleOrders;
        }
        public List<ReportJcMaster> LoadJc(string mode)
        {
            List<ReportJcMaster> ReportJcMaster = new List<ReportJcMaster>();
            try
            {
                ReportJcMaster = _db.Query<ReportJcMaster>("USP_PRODUCT_MASTER_V2", new { @p_mode=mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return ReportJcMaster;
        }
        public List<ReportTimeSpan> LoadTimeSpan(string mode, string Tag, string Finyear)
        {
            List<ReportTimeSpan> ReportTimeSpan = new List<ReportTimeSpan>();
            try
            {
                ReportTimeSpan = _db.Query<ReportTimeSpan>("USP_PRODUCT_MASTER_V2", new { @p_mode=mode, p_id= Tag, @P_userid= Finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return ReportTimeSpan;
        }
        public List<ReportTimeSpan> FetchDateRange(string span, string Tag, string _FinYear)
        {
            List<ReportTimeSpan> ReportTimeSpan = new List<ReportTimeSpan>();
            try
            {
                ReportTimeSpan = _db.Query<ReportTimeSpan>("USP_FETCH_DATERANGE_NEW", new { @p_SPANDATE = span, @p_TAG= Tag, @P_FINYEAR= _FinYear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return ReportTimeSpan;
        }
        public List<ReportModel> BindItemLedgerExport(string fromDate, string toDate, string depot, string product, string storeLocation)
        {
            List<ReportModel> saleOrders = new List<ReportModel>();
            try
            {
                saleOrders = _db.Query<ReportModel>("USP_RPT_STOCK_Ledger_Export_v2", new { FRMDATE = fromDate , TODATE = toDate , DEPOTID = depot , PARODUCTID = product , P_STOCKLOCATION = storeLocation }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return saleOrders;
        }
        public List<ReportModel> BindItemLedger(string fromDate, string toDate, string depot, string product, string storeLocation)
        {
            List<ReportModel> saleOrders = new List<ReportModel>();
            try
            {
                saleOrders = _db.Query<ReportModel>("USP_RPT_STOCK_Ledger_v2", new { FRMDATE = fromDate, TODATE = toDate, DEPOTID = depot, PARODUCTID = product, P_STOCKLOCATION = storeLocation }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return saleOrders;
        }
        public List<LedgerReportModel> BindLedgerReport(string fromDate, string toDate, string ledgerId, string depotId, string FINYEAR)
        {
            List<LedgerReportModel> LedgerReportModel = new List<LedgerReportModel>();
            try
            {
                LedgerReportModel = _db.Query<LedgerReportModel>("USP_RPT_LEDGER_DETAILS", new { @P_FRMDATE = fromDate, @P_TODATE = toDate, @P_LEDGERID = ledgerId, @P_REGIONID = depotId, @Finyr = FINYEAR }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }

        //Added By Aisha Pain for E-invoice report(04-12-2020)
        public List<EinvoicePendingReportModel> BindPendingEinvoiceGrid(string FromDate, string ToDate, string DepotID)
        {
            List<EinvoicePendingReportModel> pendingEinvoiceGrid = new List<EinvoicePendingReportModel>();
            try
            {
                pendingEinvoiceGrid = _db.Query<EinvoicePendingReportModel>("USP_EINVOICE_UPLOAD_MCWORLD_V2",
                    new
                    {
                        FRMDATE = FromDate,
                        TODATE = ToDate,
                        Depotid = DepotID
                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return pendingEinvoiceGrid;
        }
    }
}
