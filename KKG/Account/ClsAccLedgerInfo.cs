using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;
using System.Data.SqlClient;

namespace Account
{
    public class ClsAccLedgerInfo
    {
         DBUtils db = new DBUtils();

         public DataTable BindGridBranch()
         {
             DataTable dt = new DataTable();
             try
             {
                 string sql = " select [BRID] as REGIONID,[BRNAME] AS REGIONNAME from [M_BRANCH] WHERE ISSHOWACCOUNTS='Y' order by BRNAME";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }

         public DataTable BindGroup(string Mode)
         {
             DataTable dt = new DataTable();
             try
             {
                //string sql = "SELECT Code,grpName FROM Acc_AccountGroup t1 WHERE NOT EXISTS(SELECT 1 FROM Acc_AccountGroup t2 " +
                //             "WHERE t2.ParentID = t1.Code) order by grpName";

                /* Modify for get data with out S.Creditors/Debors/Bank  By D.Mondal  on 31/10/2018  */
                string sql = string.Empty;              

                sql = "EXEC USP_FILL_GROUP @P_MODE = '" + Mode + "'";
                dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }


         public DataTable BindTax()
         {
             DataTable dt = new DataTable();
             try
             {  // ACTGRPCODE='AA69745E-EF90-440A-B3EB-908B17C6AB38' This is ID of Duties & Taces
                 string sql = "SELECT ID,NAME FROM M_TAX ORDER BY NAME";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }

         public DataTable BindGridView()
         {
             DataTable dt = new DataTable();
             try
             {
                 string sql = " SELECT A.Id,name,actGrpCode,B.grpName,CASE WHEN costcenter IS NULL THEN 'NO' WHEN costcenter='N' THEN 'NO' ELSE 'YES' END AS costcenter," +
                    "STUFF(	(SELECT   N', ' + CONVERT(Varchar(MAX), BRPREFIX) FROM Acc_Branch_Mapping M INNER JOIN M_BRANCH AS BR ON M.BranchID=BR.BRID AND LEDGERID=A.Id AND BR.ISSHOWACCOUNTS='Y' " +
                    "FOR XML PATH(''),TYPE ).value('text()[1]','nvarchar(max)'),1,2,N'') AS REGION FROM Acc_AccountsInfo A  inner join Acc_AccountGroup B on A.actGrpCode=B.Code order by name";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }

         public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string CostCenter, string Taxes, string xmlinvoice, string isallownegativebal)
         {
             int flag = 0;
             string sql = string.Empty;
             string mode = string.Empty;
             try
             {
                 string sqlcheck = string.Empty;
                 DataTable DT = new DataTable();

                 if (accid == "")
                 {
                     mode = "A";
                 }
                 else
                 {
                     mode = "U";
                 }
                 sql = "exec [SP_ACC_LEDGER_INSERT] '" + Name + "','" + AccGroupCode + "','" + Finyear + "','" + mode + "','" + accid + "','" + xml + "','','','" + CostCenter + "','" + Taxes + "','" + xmlinvoice + "' ,'" + isallownegativebal + "'";
                 flag = (int)db.GetSingleValue(sql);                 
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
             }
             return flag;
         }

         public DataSet FetchLedgerInfo(string accid,string finyear)
         {
             string sql = string.Empty;
             DataSet ds = new DataSet();
             try
             {
                 sql = "exec [SP_ACC_LEDGER_EDITDETAILS] '" + accid + "','" + finyear + "'";
                 ds = db.GetDataInDataSet(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return ds;
         }

         public string PeriodID(string Finyear)
         {
             string sql = string.Empty;
             string Periodid = string.Empty;
             try
             {
                 sql = "SELECT CAST(Id AS CHAR(4)) AS Id  FROM M_FINYEAR WHERE FinYear='" + Finyear + "'";
                 Periodid = (string)db.GetSingleValue(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return Periodid;
         }

         public int IsPredefine(string LeadgerID)
         {
             string sql = string.Empty;
             int id=0;
             try
             {
                 sql = "SELECT isPredefined FROM Acc_AccountsInfo WHERE Id='" + LeadgerID + "'";
                 id = (int)db.GetSingleValue(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return id;
         }

         public DataTable BindDepot()
         {
             DataTable dt = new DataTable();
             try
             {
                 string sql = " SELECT BRID,BRNAME FROM M_BRANCH WHERE ISSHOWACCOUNTS='Y' ORDER BY BRNAME";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }
         public int SaveDepotMapping(string LEDGERID, string XML)
         {
             string strsql = string.Empty;
             int Result = 0;

             strsql = "EXEC [USP_LEDGER_DEPOT_MAPPING] '" + LEDGERID + "','" + XML + "'";
             Result = db.HandleData(strsql);
             return Result;
         }

         public DataTable BindEditDepot(string LEDGERID)
         {
             DataTable dt = new DataTable();
             try
             {
                 string sql = " SELECT A.LEDGERID,A.LEDGERNAME,B.BRID AS BRANCHID,B.BRNAME AS BRANCHNAME FROM ACC_BRANCH_MAPPING A "+
                              " INNER JOIN M_BRANCH B ON B.BRID=A.BRANCHID WHERE A.LEDGERID='" + LEDGERID + "' AND B.ISSHOWACCOUNTS='Y'";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }
         public string LedgerDelete(string LedgerID)
         {
             string sql = string.Empty;
             string status = string.Empty;
             try
             {
                 sql = "EXEC USP_LEDGER_EXISTS '" + LedgerID + "'";
                 status = (string)db.GetSingleValue(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return status;
         }
         public DataTable BindVoucherTypes()
         {
             DataTable dt = new DataTable();
             try
             {
                 string sql = "SELECT Id,VoucherName FROM Acc_VoucherTypes WHERE Id IN(9,10) ORDER BY VoucherName";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }

         public DataTable BindLedger()
         {
             DataTable dt = new DataTable();
             try
             {
                 string sql = "SELECT ID,NAME FROM ACC_ACCOUNTSINFO ORDER BY NAME";
                 dt = db.GetData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return dt;
         }

         public DataTable BindExceptionLedger()
         {
             string sql = "SELECT LEDGERID,LEDGERNAME,TXNTYPE,CASE WHEN TXNTYPE='0' THEN 'Debit' ELSE 'Credit' END AS 'TXNTYPENAME',MODE,CASE WHEN MODE='P' THEN 'Partial' ELSE 'Full' END AS 'MODETYPE' FROM ACC_LEDGER_EXCEPTION_MAPPING ORDER BY LEDGERNAME";
             DataTable dt = db.GetData(sql);
             return dt;
         }

         public int SaveExceptionLedger(string LedgerID, string LedgerName, string Txntype, string Mode)
         {
             int result = 0;
             try
             {
                DataTable dt = new DataTable();
                string sql = string.Empty;

                sql = "INSERT INTO ACC_LEDGER_EXCEPTION_MAPPING (LEDGERID,LEDGERNAME,TXNTYPE,MODE) VALUES('" + LedgerID + "','" + LedgerName + "','" + Txntype + "','" + Mode + "') ";
                result = db.HandleData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return result;
         }

         public int DeleteExceptionLedger(string LedgerID, string Txntype, string Mode)
         {
             int result = 0;
             try
             {
                 DataTable dt = new DataTable();
                 string sql = string.Empty;

                 sql = "DELETE FROM ACC_LEDGER_EXCEPTION_MAPPING WHERE LEDGERID='" + LedgerID + "' AND TXNTYPE='" + Txntype + "' AND MODE='" + Mode + "'";
                 result = db.HandleData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return result;
         }

         /*********************ACC_INVOICEDETAILS_UPDATE ADD BY SUBHODIP DE ON 25.07.2018*************************/
         public DataTable BindAccInvoiceDetails(string ledgerid, string VoucherType, string BranchID)
         {
             string sql = "SP_ACC_INVOICEDETAILS_UPDATE '','" + ledgerid + "','" + VoucherType + "','" + BranchID + "'";
             DataTable dt = db.GetData(sql);
             return dt;
         }

         public int SaveAccInvoiceUpdate(string ledgerid, string ledgername, string XMLdtinvoicedetails, string XMLdtinvoiceEntryDetails)
         {
             int flag = 0;
             string sql = string.Empty;
             string mode = string.Empty;
             try
             {

                 sql = "exec [USP_ACC_INVOICEAMT_UPDATE] '" + ledgerid + "','" + ledgername + "','" + XMLdtinvoicedetails + "','" + XMLdtinvoiceEntryDetails + "'";
                 flag = (int)db.GetSingleValue(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
             }
             return flag;
         }
         public int DeleteAccInvoiceDetailsdelete(string ledgerid)
         {
             int flag = 0;
             string sql = string.Empty;
             string mode = string.Empty;
             try
             {

                 sql = "EXEC [USP_UPLOADED_ACC_INVOICE_DETAILS_DELETE] '" + ledgerid + "'";
                 flag = (int)db.HandleData(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
             }
             return flag;
         }

        /*--------------------------------------------*/
    }
}
