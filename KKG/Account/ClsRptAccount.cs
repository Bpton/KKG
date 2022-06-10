using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace Account
{
    public class ClsRptAccount
    {
        DBUtils db = new DBUtils();

        public DataTable BindLedgeraccount()
        {
            DataTable DT = new DataTable();
            try
            {
                string Sql = " SELECT REFERENCE_LEDGERID,BANKNAME FROM M_BANKMASTER  ORDER BY BANKNAME";
                DT = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return DT;
        }
        public DataTable BindVoucherType()
        {
            DataTable DT = new DataTable();
            try
            {
                string Sql = " SELECT ID,NAME FROM ACC_BALANCETYPES ORDER BY ID";
                DT = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return DT;
        }
        public string GetFinYearID(string Session)
        {
            string sql = "SELECT id FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }

        public DataSet BindBankReport(string fromdate, string todate, string ledgerid, string Sessionid, int vouchertype, string ReconciledVoucher, string depotid)
        {
            string sql = "EXEC [USP_RPT_BANK_CONCILIATION_DETAILS] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + Sessionid + "'," + vouchertype + ",'" + ReconciledVoucher + "','" + depotid + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        /* This logic add for Narration search modify, (logic change ) By Dhananjoy Mondal on 24/11/2018 */
        public DataSet BindBankReport(string fromdate, string todate, string ledgerid, string Sessionid, int vouchertype, string ReconciledVoucher, string depotid, string SearchExpression, string Search)
        {
            string sql = "EXEC [USP_RPT_BANK_CONCILIATION_DETAILS] '" + fromdate + "','" + todate + "','" + ledgerid + "','" + Sessionid + "'," + vouchertype + ",'" + ReconciledVoucher + "','" + depotid + "','" + SearchExpression + "','" + Search + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public int SaveBankConcilation(string xml)
        {
            int result = 0;
            string sql = "EXEC [USP_BANK_RECONCILATION] '" + xml + "'";
            DataSet ds = new DataSet();
            result = db.HandleData(sql);
            return result;
        }
        /* Add for fill Party for TDS Report by D.Mondal on 08/10/2018 */
        public DataSet BindAccountsPartyName(string PartyType)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sql = "exec [SP_ACC_FILL_PARTY] '" + PartyType + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return ds;
        }
        public DataTable TDSReport(string fromdate, string todate,string PartyType, string PartyId,string DepotId)
        { 
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_TDS] '" + fromdate + "','" + todate + "','" + PartyType + "','" + PartyId + "','" + DepotId + "'";
            dt = db.GetData(sql);
            return dt;
        }
        /* Add for Collection Report By D.Mondal On 25/02/2019 */
        public DataTable GetListofBank()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_GET_BANK_LIST]";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetListofBUSINESSSEGMENT()
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_GET_BUSINESSSEGMENT_LIST]";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetListofParty(string depotid,string BusinessSegmentID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_GET_PARTY_LIST] @P_BUSINESSSEGMENTID='" + BusinessSegmentID + "',@P_DEPOTID='" + depotid + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPartyCollection(string FromDate, string ToDate,string TransactionType, string VoucherMode, string BankName, string BusinessSegment, string Depot, string Party)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_RPT_COLLECTION] @P_FROMDATE='" + FromDate + "',@P_TODATE='" + ToDate + "',@P_TransactionType='" + TransactionType + "',@P_VoucherMode='" + VoucherMode + "',@P_BankName='" + BankName + "',@P_BUSINESSSEGMENTID='" + BusinessSegment + "',@P_DEPOTID='" + Depot + "',@P_Party='" + Party + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindPartyCollection_test(string FromDate, string ToDate, string TransactionType, string VoucherMode, string BankName, string BusinessSegment, string Depot, string Party)
        {
            DataSet dt = new DataSet();
            string sql = "EXEC [USP_RPT_COLLECTION] @P_FROMDATE='" + FromDate + "',@P_TODATE='" + ToDate + "',@P_TransactionType='" + TransactionType + "',@P_VoucherMode='" + VoucherMode + "',@P_BankName='" + BankName + "',@P_BUSINESSSEGMENTID='" + BusinessSegment + "',@P_DEPOTID='" + Depot + "',@P_Party='" + Party + "'";
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        /* End */

        public string GetStartDateOfFinYear(string Session)
        {
            string STARTDATE = string.Empty;
            string sql1 = "EXEC [USP_GET_LASTDATE_NONCURRENT_FY] @P_FINYEAR='" + Session + "'";
            STARTDATE = Convert.ToString(db.GetSingleValue(sql1));

            return STARTDATE;
        }
        public string GetEndDateOfFinYear(string Session)
        {
            string Endate = string.Empty;
            string sql1 = "SELECT Endate FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            Endate = Convert.ToString(db.GetSingleValue(sql1));

            return Endate;
        }
        public DataTable LoadListOfDateSettings(string FINYEAR)
        {

            string sql = "EXEC [USP_LOADLISTOF_DATE_SETTINGS] '" + FINYEAR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveDateSetting(string FromDate, string ToDate, string AllowDate, string USERID, string DEPOTID, string FINYEAR)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                sqlstr = "EXEC [USP_BACKDATE_ENTRY_SETTING_INSERT_DATERANGE]'" + AllowDate + "','" + USERID + "','" + DEPOTID + "','" + FINYEAR + "','" + FromDate + "','" + ToDate + "'";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }
        public DataTable BindFinYear(string FinYear)  
        {
            string sql = "SELECT TOP 1 * FROM M_TIMEBREAKUP WHERE  FINYEAR='" + FinYear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotForAgingReport()
        {
            string sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH ORDER BY BRNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAccountUser()
        {
            string sql = "EXEC [USP_LOAD_ACCOUNT_USER]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDEPOTBYUSER(string DEPOTID)
        {
            string sql = "EXEC [USP_DEPOT_SELECTINDEXCHANGE_ACCOUNT_USER]'" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DeletDateSetting(string ID)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                sqlstr = "DELETE tblBackEntryDate WHERE ID='" + ID + "' ";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }
        public DataTable BindTDSLEDGER()
        {
            string sql = "EXEC [USP_GET_TDS_LEDGER]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable TDSSUMMARYReport(string fromdate, string todate, string LDSLEDGERID, string DepotId,string FINYEARID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [SP_TDS_REPORT] @P_TDSLEDGERID='" + LDSLEDGERID + "',@P_DEPOTID='" + DepotId + "',@P_PERIODID='" + FINYEARID + "',@P_FROMDATE='" + fromdate + "',@P_TODATE='" + todate + "'";
            dt = db.GetData(sql);
            return dt;
        }


        public DataSet BindGridBudget(string FinYear)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sql = "exec [USP_BUDGET_REPORT] @P_FINYEAR='" + FinYear + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return ds;
        }

        public DataTable BindDepot()
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='DEPOT'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessegment()
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='Businessegment'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepartment()
        {
            string sqlfill = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='Department'";
            DataTable dtfill = new DataTable();
            dtfill = db.GetData(sqlfill);
            return dtfill;
        }

        public DataTable BindUserRole()
        {
            string sqlUR = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='UserRole'";
            DataTable dtur = new DataTable();
            dtur = db.GetData(sqlUR);
            return dtur;
        }

        public DataTable BindGridNarration(string FinYear)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "exec [USP_GET_EXPENSES_NARRATION] @P_FINYEAR='" + FinYear + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public int Save_ExpensesNaration(string ID,string Code,string name,string FromDate,string ToDate,string DepotID,string BusinessSegment,string DepartmentID,string UserRoleID,string Description)
        {
            int result = 0;
            string sql = string.Empty;
            sql = "EXEC[USP_SAVE_EXPENSES_NARRATION]'" + ID + "','" + Code + "','" + name + "','" + FromDate + "','" + ToDate + "','" + DepotID + "','" + BusinessSegment + "','" + DepartmentID + "','" + UserRoleID + "','" + Description + "'";
            result = db.HandleData(sql);
            return result;
           
        }
        public DataSet FetchNarrationInfo(string NarrationID)
        {
            string sql = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                sql = "exec [SP_ACC_NARRATION_EDITDETAILS] '" + NarrationID + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return ds;
        }

        public int Delete_ExpensesNaration(string ID)
        {
            int result = 0;
            string sql = string.Empty;
            sql = "EXEC[USP_DELATE_EXPENSES_NARRATION]'" + ID + "'";
            result = db.HandleData(sql);
            return result;

        }
        public DataTable BindBusinessSegmentByUserID(string UserId)
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='BusinessSegmentByUserID',@P_USERID='" + UserId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUserType()
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='UserType'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindEmployeeCode(string usertypeID)
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='EmployeeCode',@P_USERID='" + usertypeID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNarrationMaster()
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='NarrationMaster'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindNarrationMasterDetails(string NarrationID)
        {
            string sql = "exec [SP_GET_MULTIPLE_INFO] @P_TYPE='NarrationMasterDetails',@P_USERID='" + NarrationID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


    }

}
