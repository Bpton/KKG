using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;

namespace Account
{
    public class ClsVoucherEntry
    {
        DBUtils db = new DBUtils();

        public DataTable Region()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT '-2' as BRID,'---- OFFICE ----' as BRNAME,'1' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'O' AND EXPORT<>'Y' AND ISSHOWACCOUNTS='Y' UNION " +
                         "SELECT '-3' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'3' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' AND EXPORT<>'Y' AND ISSHOWACCOUNTS='Y' UNION SELECT '-4' as BRID,'---- DEPOT ----' as BRNAME ,'5' AS SEQUENCE UNION SELECT BRID,BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                         " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND EXPORT <> 'Y' AND ISSHOWACCOUNTS='Y' ORDER BY SEQUENCE,BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BussinessSegment()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select COSTCATID,COSTCATNAME from Vw_BUSINESSSEGMENT order by COSTCATNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable LoadPurchaseOrder(string fromdate,string todate,string userid,string finyear,string depot)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "EXEC [USP_LOAD_PURCHASE_FOR_UNLOCK]'"+ fromdate + "','"+ todate + "','"+ userid + "','"+ finyear + "','"+ depot + "' ";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable Brand()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select BRANDID,BRANDNAME from Vw_BRAND order by BRANDNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }


        public DataTable ProductMaster()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select PRODUCTID,PRODUCTNAME from Vw_PRODUCTMASTER order by PRODUCTNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable Department()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select DEPTID,DEPTNAME from Vw_DEPARTMENT order by DEPTNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }


        public DataTable Region(string UserType, string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";
                if (UserType == "admin")
                {
                    sql = " SELECT BRID,BRNAME FROM M_BRANCH WHERE EXPORT<>'Y' AND ISSHOWACCOUNTS='Y' ORDER BY BRANCHTAG DESC,BRNAME";
                }
                else
                {
                    sql = "SELECT BRID,BRNAME FROM M_BRANCH A INNER JOIN M_TPU_USER_MAPPING B ON A.BRID=B.TPUID WHERE B.USERID='" + UserID + "' AND A.EXPORT<>'Y' AND ISSHOWACCOUNTS='Y' ORDER BY BRNAME";
                }
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }


        public DataTable VoucherType()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "Select Id ,VoucherName from Acc_VoucherTypes Order by VoucherName";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }


        public DataTable PaymentType()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "Select PAYMENTTYPEID ,PAYMENTTYPENAME from Acc_PaymentType Order by PAYMENTTYPENAME";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }


        public DataTable Receipt_Type_Account(string VoucherTypeID, string PaymentMode)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "SELECT Id,name FROM Acc_AccountsInfo where actGrpCode IN " +
                      "(SELECT [AccGrpID] FROM Acc_CashBankID WHERE Type='" + PaymentMode + "') AND ID NOT IN(SELECT KOLKATADEPOTID FROM P_APPMASTER) order by name";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable VoucherType_Account(string VoucherTypeID, string Type, string RegionId)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " SELECT A.ID,NAME FROM ACC_ACCOUNTSINFO A INNER JOIN ACC_BRANCH_MAPPING B ON A.ID=B.LEDGERID INNER JOIN ACC_PARAM C ON C.ACCGRPID=A.ACTGRPCODE " +
                      " WHERE C.VOUCHERTYPEID='" + VoucherTypeID + "' AND C.TYPE_DR_CR='" + Type + "' AND A.ID NOT IN('" + RegionId + "',(SELECT KOLKATADEPOTID FROM P_APPMASTER)) AND B.BRANCHID='" + RegionId + "' ORDER BY NAME";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable BankNameForReceipt()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " select ID,BANKNAME from M_BANKMASTER order by BANKNAME ";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

      /*  public DataTable VoucherHeaderDetails(string FromDate, string ToDate, string UserID, string FinYear, string VoucherTypeID, string Checker, string DepotID, string FromUnlockVoucher)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " EXEC USP_VOUCHER_HEADERDETAILS '" + FromDate + "','" + ToDate + "','" + UserID + "','" + FinYear + "','" + VoucherTypeID + "','" + Checker + "','" + DepotID + "','" + FromUnlockVoucher + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }*/

        public DataSet VoucherDetails(string AccEntryID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_ACC_VOUCHERDETAILS '" + AccEntryID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public int VoucherDelete(string AccEntryID)
        {
            int delflag = 0;
            string sql = "EXEC SP_ACC_VOUCHER_DELETE '" + AccEntryID + "'";
            int e = db.HandleData(sql);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else if (e == -1)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }        

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string IsVoucherApproved)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string IsVoucherApproved, string IsFromPage)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + IsFromPage + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string DebitCreditxml)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "','" + DebitCreditxml + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string BillNo, string BillDate, string GRNo, string GRDate, string VehicleNo, string Transport, string WayBillNo, string WayBillDate)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "',NULL,'" + BillNo + "','" + BillDate + "','" + GRNo + "','" + GRDate + "','" + VehicleNo + "','" + Transport + "','" + WayBillNo + "','" + WayBillDate + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string IsVoucherApproved,
                                            string InvoiceVoucherDetailsxml, string CostCenterDetailsxml, string ISFROMPAGE, string InvoiceDebitInvoicexml, string BILLNO, string BILLDATE, string GRNO, string GRDATE, string VECHILENO, string TRANSPORT, string WAYBILLNO, string WAYBILLDATE,
                                            string IsClosed, string ISFORAUTOBANK, string BANKSERVERNAME)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = " EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "'," +
                         " '" + IsVoucherApproved + "','" + InvoiceVoucherDetailsxml + "','" + CostCenterDetailsxml + "','" + ISFROMPAGE + "','" + InvoiceDebitInvoicexml + "','" + BILLNO + "','" + BILLDATE + "','" + GRNO + "','" + GRDATE + "','" + VECHILENO + "','" + TRANSPORT + "','" + WAYBILLNO + "'," +
                         " '" + WAYBILLDATE + "','" + IsClosed + "','" + ISFORAUTOBANK + "','" + BANKSERVERNAME + "'";

            Voucherdetails = (string)db.GetSingleValue(sql);
            return Voucherdetails;
        }

        public int VoucherDetails(string AccEntryID, string InvoiceID)
        {
            int result = 0;
            string sql = "INSERT INTO Acc_Voucher_Invoice_Map(AccEntryID,InvoiceID) VALUES('" + AccEntryID + "','" + InvoiceID + "')";
            result = db.HandleData(sql);

            return result;
        }

        public DataTable InvoiceDetails(string VoucherID, string LeadgerID, string VoucherTYpe, string BranchID)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "EXEC [SP_ACC_INVOICEDETAILS] '" + VoucherID + "','" + LeadgerID + "','" + VoucherTYpe + "', '" + BranchID + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable InvoiceDetails(string VoucherID, string LeadgerID, string VoucherTYpe, string BranchID,string date)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "EXEC [SP_ACC_INVOICEDETAILS] '" + VoucherID + "','" + LeadgerID + "','" + VoucherTYpe + "', '" + BranchID + "','"+ date + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable InvoiceDetails_Debit_Credit(string VoucherID, string LeadgerID, string VoucherTYpe)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "EXEC [USP_ACC_INVOICEDETAILS_DEBIT_CREDIT] '" + VoucherID + "','" + LeadgerID + "','" + VoucherTYpe + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public string VoucherDetailsInsert(string AccEntryID, string tAG, string InvoiceXML)
        {
            string sql = "EXEC [SP_ACC_INVOICE_ENTRY] '" + AccEntryID + "','" + tAG + "','" + InvoiceXML + "'";
            string result = (string)db.GetSingleValue(sql);

            return result;
        }

        public string LeadgerID(string AccEntryID, string TXNTYPE)
        {
            string LeadgerId = string.Empty;

            string sql = "SELECT TOP 1 LedgerId FROM Acc_Posting WHERE AccEntryID='" + AccEntryID + "' AND TXNTYPE='" + TXNTYPE + "'";
            LeadgerId = (string)db.GetSingleValue(sql);

            return LeadgerId;
        }

        public int CostCenterApplicable(string AccountID)
        {
            int flag = 0;

            string sql = "IF exists(SELECT ID,name FROM Acc_AccountsInfo WHERE costcenter='Y' AND ID='" + AccountID + "') begin select 1 end else begin select 0 end";
            flag = (int)db.GetSingleValue(sql);

            return flag;
        }

        public DataTable TaxApplicable(string AccountID, string VoucherDate, string Finyear, decimal Amount, string BranchID)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "EXEC USP_ACC_TAXAPPLICALE '" + AccountID + "','" + VoucherDate + "','" + Finyear + "'," + Amount + ",'" + BranchID + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :QUERY Error");
            }
            return dt1;
        }

        public DataTable TaxApplicable(string AccountID)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = "SELECT ID,NAME FROM M_TAX WHERE ID IN(SELECT * FROM dbo.fnSplit((SELECT TAXID FROM Acc_AccountsInfo WHERE ID= '" + AccountID + "'),',')) ORDER BY NAME";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :QUERY Error");
            }
            return dt1;
        }

        public DataTable BindCostCenterCatagory()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " select COSTCATID,COSTCATNAME from M_COST_CATEGORY order by COSTCATNAME ";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable BindCostCenter()
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " select COSTCENTREID,COSTCENTRENAME from M_COST_CENTRE order by COSTCENTRENAME ";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public decimal Outstanding(string LedgerID, string RegionID, string FromDate, string ToDate, string Finyear)
        {
            string sql = string.Empty;
            decimal outstanding = 0;
            try
            {
                sql = "EXEC USP_ACC_LEDGER_OUTSTANDING '" + LedgerID + "','" + RegionID + "','" + FromDate + "','" + ToDate + "','" + Finyear + "'";
                outstanding = (decimal)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return outstanding;
        }

        public decimal TaxAmount(string ID, decimal Value, string LedgerID)
        {
            string sql = string.Empty;
            decimal tax = 0;

            /* 9A555D40-5E12-4F5C-8EE0-E085B5BAB169 = Get from M_OrderType  */
            sql = " SELECT CEILING(CAST(ISNULL(dbo.fn_TaxEvalute_Other('" + ID + "','" + LedgerID + "','0','0','9A555D40-5E12-4F5C-8EE0-E085B5BAB169'," + Value.ToString() + " )* " + Value + "/100,0) AS DECIMAL(18,2))) AS AMOUNT ";

            tax = (decimal)db.GetSingleValue(sql);
            return tax;
        }

        public int VoucherApproved(string AccEntryID, string VoucherType)
        {
            string sql = string.Empty;
            int result = 0;
            try
            {
                if (VoucherType == "13")
                {
                    sql = "EXEC SP_CREDIT_DEBIT_NOTE_APPROVE '" + AccEntryID + "','1'";     // CREDIT
                }
                else if (VoucherType == "14")
                {
                    sql = "EXEC SP_CREDIT_DEBIT_NOTE_APPROVE '" + AccEntryID + "','0'";     // DEBIT
                }
                else
                {
                    /*When Approve Ho then auto ap[proved Depot : By D.Mondal on 03/12/2018 */
                    sql = " UPDATE Acc_Entry SET IsVoucherApproved = 'Y', SYNCTAG = 'N', ApprovedDateTime = GETDATE() WHERE AccEntryID='" + AccEntryID + "' or " +
                            " AccEntryID in ( SELECT VM.AccEntryID FROM Acc_Voucher_Invoice_Map AS VM INNER JOIN Acc_Entry AS AE ON VM.InvoiceID = AE.AccEntryID " + 
                            " WHERE AE.ISAUTOPOPUP = 'Y' and VM.InvoiceID =  '" + AccEntryID + "')";
                }
                result = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        #region RejectionAccount
        public int RejectionAccount(string AccEntryID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            sql = " UPDATE Acc_Entry SET IsVoucherApproved = 'R',ApprovedDateTime = GETDATE(),RejectionNote = '" + Note + "'" +
                  " WHERE AccEntryID ='" + AccEntryID + "'";
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        #endregion

        public string DepotFlag(string DepotID)
        {
            string flag = "0";
            string sql = string.Empty;
            sql = " IF EXISTS (SELECT 1 FROM M_BRANCH WHERE BRID='" + DepotID + "' AND BRANCHTAG='D') BEGIN SELECT '1' END ELSE BEGIN SELECT '0' END";
            flag = (string)db.GetSingleValue(sql);

            return flag;
        }

        public string TaxAction(string TaxID)
        {
            string Sql = "SELECT ISNULL(WITHEFFECT,'+')+'|'+ISNULL(TxnTYPE,'') AS WITHEFFECT FROM M_TAX WHERE ID='" + TaxID + "'";
            string Taxeffect = "";
            Taxeffect = (string)db.GetSingleValue(Sql);

            return Taxeffect;
        }

        public string Invoiceinsert(string xml)
        {
            string Sql = "EXEC SP_ACC_VOUCHER_JOURNAL '" + xml + "'";
            string status = "";
            status = (string)db.GetSingleValue(Sql);

            return status;
        }

        public string InsertVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string BillNo, string BillDate, string GRNo, string GRDate, string VehicleNo, string Transport, string WayBillNo, string WayBillDate, string isclosed)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "',NULL,'" + BillNo + "','" + BillDate + "','" + GRNo + "','" + GRDate + "','" + VehicleNo + "','" + Transport + "','" + WayBillNo + "','" + WayBillDate + "','" + isclosed + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }

        public string IsTransferToHO_Ledger(string AccentryID)
        {
            string Sql = "EXEC USP_VOUCHER_EXCEPTION_TRANSFERHO '" + AccentryID + "'";
            string status = "";
            status = (string)db.GetSingleValue(Sql);
            return status;
        }
        /* Add overload method for FROMBULKJOURNA_APPROVAL by D.Mondal on 05/11/2018*/
        public string IsTransferToHO_Ledger(string AccentryID,string FROMBULKJOURNA_APPROVAL)
        {
            string Sql = "EXEC USP_VOUCHER_EXCEPTION_TRANSFERHO '" + AccentryID + "','" + FROMBULKJOURNA_APPROVAL + "'";
            string status = "";
            status = (string)db.GetSingleValue(Sql);
            return status;
        }

        public DataSet PaymentVoucherDetails(string AccEntryID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC USP_ACC_PAYMENT_DETAILS '" + AccEntryID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string HO_LedgerID() /*logic change for kkg branchtag in O & D*/
        {
            string HOID = "";
            string Sql = "SELECT TOP 1 BRID FROM [M_BRANCH] where BRANCHTAG in('O','D') and ISMOTHERDEPOT ='True' ";
            HOID = (string)db.GetSingleValue(Sql);
            return HOID;
        }

        public int ParticulrasInsert(string AccEntryID, string InvoiceID, string VoucherTypeId)
        {
            int result = 0;
            string sql = "EXEC USP_ACC_PARTICULARS_INSERT '" + AccEntryID + "','" + InvoiceID + "','" + VoucherTypeId + "'";
            result = db.HandleData(sql);

            return result;
        }

        public string IsNegativeBal(string AccEntryID, string VoucherTypeID, string TxnType, string Mode)
        {
            string IsNegativeBal = "N";
            string sql = "EXEC USP_ACC_NEGATIVEBAL_CHECKING '" + AccEntryID + "','" + VoucherTypeID + "','" + TxnType + "','" + Mode + "'";
            IsNegativeBal = (string)db.GetSingleValue(sql);

            return IsNegativeBal;
        }
        public DataTable UnlockCreditNote(string FromDate, string ToDate, string UserID, string FinYear, string VoucherTypeID, string Checker, string DepotID, string FromUnlockVoucher)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " EXEC USP_UNLOCK_CREDITNOTE '" + FromDate + "','" + ToDate + "','" + UserID + "','" + FinYear + "','" + VoucherTypeID + "','" + Checker + "','" + DepotID + "','" + FromUnlockVoucher + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable BindDepot(string USERID)
        {
            string sql = " IF EXISTS( " +
                         " SELECT B.BRID AS BRID ,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "')  " +
                         " BEGIN " +
                         " SELECT B.BRID AS BRID ,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "'   ORDER BY B.BRNAME " +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " SELECT DISTINCT BRID AS BRID ,BRPREFIX AS BRNAME FROM M_BRANCH   ORDER BY BRNAME " +
                         " END ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string AutoAccEntryID(string VoucherID)
        {
            string sql = string.Empty;
            string AccEntryID = "";
            try
            {
                //sql = "SELECT TOP 1 ACCENTRYID FROM ACC_VOUCHER_INVOICE_MAP WHERE INVOICEID='" + VoucherID + "'";
                sql = "EXEC SP_GET_AUTOACCENTRYID '" + VoucherID + "'"; /* Modify D.Mondal on 04/12/2018 */
                AccEntryID = (string)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return AccEntryID;
        }

        public DataTable VoucherHeaderDetails(string FromDate, string ToDate, string UserID, string FinYear, string VoucherTypeID, string Checker, string DepotID, string FromUnlockVoucher, string vouchertype)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " EXEC USP_VOUCHER_HEADERDETAILS '" + FromDate + "','" + ToDate + "','" + UserID + "','" + FinYear + "','" + VoucherTypeID + "','" + Checker + "','" + DepotID + "','" + FromUnlockVoucher + "','" + vouchertype + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }

        public DataTable VoucherHeaderDetails_CreditNote(string FromDate, string ToDate, string UserID, string FinYear, string VoucherTypeID, string Checker, string DepotID, string FromUnlockVoucher)
        {
            string Sql = "";
            DataTable dt1 = new DataTable();
            try
            {
                Sql = " EXEC USP_APPROVE_CREDITDEBITNOTE '" + FromDate + "','" + ToDate + "','" + UserID + "','" + FinYear + "','" + VoucherTypeID + "','" + Checker + "','" + DepotID + "','" + FromUnlockVoucher + "'";
                dt1 = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt1;
        }
        /* This logic used only bill taging at save ON 27/10/2018 BY D.Mondal */
        public string InsertBillTagingVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string DebitCreditxml,string OnlyBillTagFromPage, string placeofsupply, string partyname, string gstno, string invdate, string invocieno, string taxpercent, string taxvalue, string hsncode, string vouchertype)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "','" + DebitCreditxml + "', @p_OnlyBillTagFromPage='" + OnlyBillTagFromPage + "',@p_placeofsupply='" + placeofsupply + "'," +
                "@p_partyname='" + partyname + "',@p_gstno='" + gstno + "',@p_invdate='" + invdate + "',@p_invocieno='" + invocieno + "',@p_taxpercent='" + taxpercent + "',@p_taxvalue='" + taxvalue + "',@p_hsncode='" + hsncode + "',@p_voucehertype='" + vouchertype + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }
        /* Add OnlyBillTagFromPage : This logic used only bill taging (Journal) at save ON 20/11/2018 BY D.Mondal */
    
        public string InsertBillTagingJournalVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string BillNo, string BillDate, string GRNo, string GRDate, string VehicleNo, string Transport, string WayBillNo, string WayBillDate, string OnlyBillTagFromPage, string placeofsupply,string partyname,string gstno,string invdate,string invocieno,string taxpercent,string taxvalue,string hsncode,string vouchertype)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "',NULL,'" + BillNo + "','" + BillDate + "','" + GRNo + "','" + GRDate + "','" + VehicleNo + "','" + Transport + "','" + WayBillNo + "','" + WayBillDate + "', @p_OnlyBillTagFromPage='" + OnlyBillTagFromPage + "',@p_placeofsupply='" + placeofsupply + "'," +
                "@p_partyname='"+partyname+ "',@p_gstno='"+gstno+ "',@p_invdate='"+invdate+ "',@p_invocieno='"+invocieno+ "',@p_taxpercent='"+taxpercent+ "',@p_taxvalue='"+taxvalue+ "',@p_hsncode='"+hsncode+ "',@p_voucehertype='"+vouchertype+"'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }
        public string InsertBillTagingJournalVoucherDetails(string VoucherTypeId, string VoucherTypeName, string BranchID, string BranchName, string PayementMode, string VoucherDate, string FinYear, string Narration, string UserID, string Tag, string OnEditedAccEntryID, string xml, string Invoicexml, string CostCenterxml, string IsVoucherApproved, string IsFromPage, string BillNo, string BillDate, string GRNo, string GRDate, string VehicleNo, string Transport, string WayBillNo, string WayBillDate, string OnlyBillTagFromPage, string placeofsupply,string partyname,string gstno,string invdate,
            string invocieno,string taxpercent,string taxvalue,string hsncode,string vouchertype,string xmlGstDetails)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + VoucherTypeId + "','" + VoucherTypeName + "','" + BranchID + "','" + BranchName + "','" + PayementMode + "','" + VoucherDate + "','" + FinYear + "','" + Narration + "','" + UserID + "','" + Tag + "','" + OnEditedAccEntryID + "','" + xml + "','" + IsVoucherApproved + "','" + Invoicexml + "','" + CostCenterxml + "','" + IsFromPage + "',NULL,'" + BillNo + "','" + BillDate + "','" + GRNo + "','" + GRDate + "','" + VehicleNo + "','" + Transport + "','" + WayBillNo + "','" + WayBillDate + "', @p_OnlyBillTagFromPage='" + OnlyBillTagFromPage + "',@p_placeofsupply='" + placeofsupply + "'," +
                "@p_partyname='"+partyname+ "',@p_gstno='"+gstno+ "',@p_invdate='"+invdate+ "',@p_invocieno='"+invocieno+ "',@p_taxpercent='"+taxpercent+ "',@p_taxvalue='"+taxvalue+ "',@p_hsncode='"+hsncode+ "',@p_voucehertype='"+vouchertype+ "',@p_gstDetails='" + xmlGstDetails + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }
        /* This logic used only Back Date Entry ON 27/11/2018 BY D.Mondal */
        public int CheckDate()
        {
            string sql = string.Empty;
            int flag = 0;
           
            sql = "Select count(*)  from tblBackEntryDate where AllowDate=CONVERT(DATE, GETDATE(), 103)";
            flag = (int)db.GetSingleValue(sql);
           
            return flag;
        }

        /* Add logic : when delete the record then insert in backup history  By D.Mondal on 26/11/2018 */
        public int VoucherDelete(string AccEntryID, string DELETEDDBY, string DELETED_IP_ADDRESS)
        {
            int delflag = 0;
            string sql = "EXEC SP_ACC_VOUCHER_DELETE '" + AccEntryID + "','" + DELETEDDBY + "','" + DELETED_IP_ADDRESS + "'";
            int e = db.HandleData(sql);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else if (e == -1)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull

            }

            return delflag;
        }
        public int UpdateAccEntry_For_ISAUTOPOPUP(string AccEntryID)
        {
            int delflag = 0;
            string sql = "EXEC SP_ACC_ENTRY_UPDATE_FOR_ISAUTOPOPUP '" + AccEntryID + "'";
            int e = db.HandleData(sql);

            //if (e == 0)
            //{
            //    delflag = 0;  // delete unsuccessfull
            //}
            //else if (e == -1)
            //{
            //    delflag = 0;  // delete unsuccessfull
            //}
            //else
            //{
            //    delflag = 1;  // delete successfull

            //}

            return delflag;
        }

        /* Add for Isautovoucher yes or No By D.Mondal on 04/01/2018  */
        public int CheckIsAutovoucher(string AccentryId)
        {
            string sql = string.Empty;
            int flag = 0;

            sql = "select count(*) from Acc_Entry where Isautovoucher='Y' and AccEntryID='" + AccentryId + "'";
            flag = (int)db.GetSingleValue(sql);

            return flag;
        }

        public DataTable BindBudgetApplicableDepartmentList()
        {
            string sql = "EXEC [SP_GET_DEPARTMENT]";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable GetFinYear()
        {
            string sql = "select id,FinYear from M_FINYEAR where ISClosed='N'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public string SaveCompanyBudgetMaster(string SEARCHTAG, string CALENDERDETAILS, string SPAMID, string FROMDATE, string TODATE, decimal BUDGETAMOUNT, string FINYEAR, string USERID, string BudgetNo)
        {
            string flag = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_Save_CompanyBudgetMaster] @P_SEARCHTAG='" + SEARCHTAG + "',  @P_CALENDERDETAILS='" + CALENDERDETAILS + "',@P_SPAMID='" + SPAMID + "',@P_FROMDATE='" + FROMDATE + "',@P_TODATE='" + TODATE + "',@P_BUDGETAMOUNT='" + BUDGETAMOUNT + "',@P_FINYEAR='" + FINYEAR + "',@P_USERID='" + USERID + "',@P_BudgetNo='" + BudgetNo + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);
            return Voucherdetails;

        }
        public string DeleteCompanyBudgetMaster(string BudgetNo)
        {
            string flag = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_Delete_CompanyBudgetMaster] @P_BudgetNo='" + BudgetNo + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);
            return Voucherdetails;

        }
        public DataTable GetAllCompanyBudgetData()
        {
            string sql = "select CompanyBudgetMasterID,CALENDERDETAILS,SEARCHTAG,SPAMID,FINYEAR,convert(varchar(10),FROMDATE,103) as FROMDATE,convert(varchar(10),TODATE,103) as TODATE,BUDGETAMOUNT,BUDGETNO from M_CompanyBudgetMaster";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetBudgetNo()
        {
            string sql = "select CompanyBudgetMasterID,BUDGETNO from M_CompanyBudgetMaster";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindApplicableDepartmentDetails(int CompanyBudgetMasterID)
        {
            string sql = "EXEC [SP_Search_Budget_Applicable_Department] @P_CompanyBudgetMasterID=" + CompanyBudgetMasterID + "";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string SaveBudgetApplicable_Department(int BudgetID, string UserID, string XML)

        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [SP_Save_Budget_Applicable_Department] @P_CompanyBudgetMasterID=" + BudgetID + ",@P_UserID='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;

        }
        public string GetStartDateOfFinYear(string Session)
        {
            string STARTDATE = string.Empty;
            string sql1 = "SELECT STARTDATE FROM [M_FINYEAR] WHERE FinYear='" + Session + "'";
            STARTDATE = Convert.ToString(db.GetSingleValue(sql1));

            return STARTDATE;
        }
        public DataTable BindApplicableDepartmentList(string DeptID)
        {
            string sql = "EXEC [SP_GET_ApplicableDepartment] @P_DEPTID='" + DeptID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public string SaveBudgetComponent_Department(string DEPTID, string DEPTNAME, string UserID, string XML)
        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [SP_Save_DepartmentalComponenet] @P_DEPTID='" + DEPTID + "',@P_DEPTNAME='" + DEPTNAME + "',@P_UserID ='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;

        }
        public DataTable BindBudgetApplicableDepartment(int BudgetID)
        {
            string sql = "EXEC [SP_GET_BUDGET_DEPARTMENT] @P_CompanyBudgetMasterID=" + BudgetID + "";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataSet BindApplicableDepartmentComponentDetails(int CompanyBudgetMasterID, string DeptID)
        {
            string sql = "EXEC [SP_Search_Budget_Department_Component] @P_CompanyBudgetMasterID=" + CompanyBudgetMasterID + ",@P_DeptID='" + DeptID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public string SaveBudgetApplicable_Component_Department(int BudgetID, string DeptID, string UserID, string XML)
        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [SP_Save_Budget_Applicable_Component_Department] @P_CompanyBudgetMasterID=" + BudgetID + ", @P_DEPTID='" + DeptID + "',@P_UserID='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;

        }
        public DataTable BindBudgetComponentList()
        {
            string sql = "EXEC [SP_GET_COMPONENT]";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindSubComponent(int DeptID)
        //{
        //    string sql = "EXEC [SP_GET_ApplicableComponent] @P_DepartmentalComponenetID=" + DeptID + "";
        //    DataTable dt = db.GetData(sql);
        //    return dt;
        //}
        public DataTable BindDepartmentComponent(string DeptID)
        {
            string sql = "EXEC [SP_Get_Department_Component] @P_DeptID='" + DeptID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindSubComponent(int ComponenetID)
        //{
        //    string sql = "EXEC [SP_GET_SubComponent] @P_DepartmentalComponenetID=" + ComponenetID + "";
        //    DataTable dt = db.GetData(sql);
        //    return dt;
        //}
        public DataTable BindSubComponent(int ComponenetID, string DEPTID)
        {
            string sql = "EXEC [SP_GET_SubComponent] @P_DepartmentalComponenetID=" + ComponenetID + ",@P_DEPTID='" + DEPTID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public string SaveSubComponent(string DEPT, int SubComponentID, string ComponentName, string UserID, string XML)
        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [SP_Save_SubComponenet] @P_DEPTID='" + DEPT + "', @P_ComponentID='" + SubComponentID + "',@P_ComponentNAME='" + ComponentName + "',@P_UserID ='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;
        }
        public DataSet BindApplicableDepartmentSubComponentDetails(int CompanyBudgetMasterID, string DeptID, int ComponentID)
        {
            string sql = "EXEC [SP_Search_Budget_Department_SubComponent] @P_CompanyBudgetMasterID=" + CompanyBudgetMasterID + ",@P_DeptID='" + DeptID + "',@P_DepartmentalComponenetID=" + ComponentID + "";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
        public int SaveBudgetApplicable_SubComponent_Department(int BudgetID, string DeptID, int ComponentID, string UserID, string XML)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC [SP_Save_Budget_Applicable_SubComponent_Department] @P_CompanyBudgetMasterID=" + BudgetID + ", @P_DEPTID='" + DeptID + "',@P_ComponentID=" + ComponentID + ",@P_UserID='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = db.HandleData(sql);
            return result;

        }

        /*******************************************************************/
        public string SaveBudgetComponent(string UserID, string XML)
        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [SP_Save_Componenet] @P_UserID ='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;

        }
        //public string SaveBudgetSubComponent(int SubComponentID,string UserID, string XML)
        //{
        //    string sql = string.Empty;
        //    string result = "";
        //    sql = "EXEC [SP_Save_ManualSubComponent] @P_ComponentID='" + SubComponentID + "',@P_UserID ='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
        //    result = Convert.ToString(db.GetSingleValue(sql));
        //    return result;
        //}

        public DataTable BindBudgetSubComponent(int ComponenetID)
        {
            string sql = "EXEC [SP_GET_BudgetSubComponent] @P_DepartmentalComponenetID=" + ComponenetID + "";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSubcomponentList()
        {
            string sql = "EXEC [SP_Fill_Component]";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable GetSubComponentList(int ComponenetID)
        {
            string sql = "EXEC [SP_GET_SubComponentList] @P_DepartmentalComponenetID=" + ComponenetID + "";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGETCOMPONENT()
        {
            string sql = "EXEC [SP_GETCOMPONENT]";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public string SaveBudgetSubComponent(int ComponentID, string ComponentName, string UserID, string XML)
        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [SP_Save_ManualSubComponent] @P_ComponentID='" + ComponentID + "',@P_ComponentName='" + ComponentName + "',@P_UserID ='" + UserID + "',@p_InvoiceVoucherDetails='" + XML + "'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;
        }
        public DataTable GetBudgetReport()
        {
            string sql = "EXEC [SP_BUDGET_REPORT]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int VOUCHER_DELETE_PREAPPROVED(string DELETEDDBY,  string INVOICEID, string IP_ADDRESS)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "EXEC [SP_ACC_VOUCHER_DELETE_PREAPPROVED] @P_DELETEDDBY='" + DELETEDDBY + "', @P_INVOICEID='" + INVOICEID + "',@P_DELETED_IP_ADDRESS='" + IP_ADDRESS + "'";
            result = db.HandleData(sql);
            return result;

        }

        public string CheckVoucherType(string debit,string credit)  /*new add by p.basu for check voucher type*/
        {
            string sql = string.Empty;
            string result = "";
            sql = "EXEC [USP_VOUCHER_TYPE_CHECKING] '"+ debit + "','"+credit+"'";
            result = Convert.ToString(db.GetSingleValue(sql));
            return result;
        }

        public DataTable BindStateParty(string mode,string userid,string customerid) /*new add by p.basu for bind state tpu vendor customer*/
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_STATE_TPU_FACTORY '" + mode + "','"+ userid + "','"+ customerid + "'";
            dt = db.GetData(sql);
            return dt;
        }


       
        public DataTable BindmoduleName(String USERTYPE, String offline)
        {
            string sql = "EXEC [FETCH_MODULENAME] @P_USERTYPE='" + USERTYPE + "',@P_offline='" + offline + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #region Cheque Booklet
        public DataTable Bind_Bank_Booklet(char MODE, string USERID, string BANKID, string BOOKLETNO, string BRANCH)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC [USP_BANK_BOOKLET_DETAILS] '" + MODE + "','" + USERID + "','" + BANKID + "','" + BOOKLETNO + "','" + BRANCH + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public int SAVEandValidation(string BankID, string BookletNo, string FromCheckNo, string ToCheckNo,
        string DEPARTMENTID, string DEPARTMENT, string userid, string Date, string MODE)
        {
            string sql = "EXEC USP_Insert_AccBooklet @P_BankID='" + BankID + "',@P_BookletNo='" + BookletNo + "', " +
                " @P_FromChequeNo='" + FromCheckNo + "',@P_ToChequeNo='" + ToCheckNo + "',@P_DEPARTMENTID='" + DEPARTMENTID + "', " +
                " @P_DEPARTMENT='" + DEPARTMENT + "',@P_userid='" + userid + "',@P_Date='" + Date + "', " +
                " @P_MODE='" + MODE + "'";
            int value = Convert.ToInt32(db.GetSingleValue(sql));
            return value;
        }
        public string Acc_Booklet_Insert(string Date, string BankID, string userid, string xml, string ch)
        {
            string sql = "EXEC USP_AccBooklet_Insert_Update '" + Date + "','" + BankID + "','" + userid + "','" + xml + "','" + ch + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }

        public string Acc_Booklet_Insert_vouter(string BankID, string userid, string xml)
        {
            string sql = "EXEC USP_AccBooklet_vouter_Insert_Update '" + BankID + "','" + userid + "','" + xml + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        public DataTable LoadBankName(string Mode, string BankID)
        {
            string sql = "EXEC [USP_GET_BANK_BOOKTEL] @P_MODE='" + Mode + "',@P_BANKID='" + BankID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChequeBooklet(string BankID, string Type, string BookLetNo)
        {
            string sql = "exec [USP_BOOKLET_DETAILS] @P_BANKID='" + BankID + "',@P_Type='" + Type + "',@P_BOOKLETNO='" + BookLetNo + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchInvoiceId(string ACCENTRYID)
        {
            string sql = " EXEC USP_FETCHINVOICE '" + ACCENTRYID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string InsertChequeCancel(string userid, string xml, string CanDis)
        {
            string sql = "EXEC USP_Insert_Acc_ChequeBooklet_Cancel '" + userid + "','" + xml + "','" + CanDis + "'";
            string value = Convert.ToString(db.GetSingleValue(sql));
            return value;
        }
        public int CheckBANKID(string ACCENTRYID)
        {
            int result = 0;
            string sql = "EXEC CHECK_BANKID @P_ACCENTRYID='" + ACCENTRYID + "'";
            result = (int)db.GetSingleValue(sql);

            return result;
        }

        public DataTable BindCHEQUENOLIST(String BANKID, String BranchID)
        {
            string sql = "EXEC [FETCH_CHEQUEBOOKLET] @P_BANKID='" + BANKID + "',@BranchID = '" + BranchID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        #endregion

    }

}
