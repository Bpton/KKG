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
    public  class ClsUploadOpeningBalance
    {
        DBUtils db = new DBUtils();

        public DataTable CreateTempOfDepotWiseCustomer(string DEPOTID)
        {
            //string sql = " SELECT A.CUSTOMERID AS CUSTOMERID,B.CUSTOMERNAME AS CUSTOMERNAME ,'' AS CREDIT,'' AS DEBIT  FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN " +
            //             " M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE A.DEPOTID='" + DEPOTID + "'";
            string SQL = "USP_ACCOUNTSINFO_TEMPLATE_GENERATE '"+ DEPOTID + "'";

            DataTable dt = db.GetData(SQL);
            return dt;
        }

        public DataTable BindDepot(string USERID)
        {
            //string sql = " IF EXISTS( " +
            //             " SELECT B.BRID AS BRID ,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "')  " +
            //             " BEGIN " +
            //             " SELECT B.BRID AS BRID ,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "' AND B.BRANCHTAG='D'  ORDER BY B.BRNAME " +
            //             " END " +
            //             " ELSE " +
            //             " BEGIN " +
            //             " SELECT DISTINCT BRID AS BRID ,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'  ORDER BY BRNAME " +
            //             " END ";

            string sql = "SELECT BRID, BRPREFIX AS BRNAME,'2' AS SEQUENCE FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'O' AND ISSHOWACCOUNTS='Y' " +
                        " UNION " +
                        " SELECT BRID,BRPREFIX AS BRNAME,'4' AS SEQUENCE   FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE'" +
                        " UNION " +
                        " SELECT BRID,BRPREFIX AS BRNAME,'6' AS SEQUENCE FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' AND ISSHOWACCOUNTS='Y' ORDER BY SEQUENCE,BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public int SaveCustomerOpeningBalance(string DEPOTID,string FINYEAR,string OpeningBalanceDetails)
        {
            int result = 0;
            string sql = "USP_CUSTOMER_OPENINGBALANCE_UPLOAD '" + DEPOTID + "','" + FINYEAR + "','" + OpeningBalanceDetails + "'";
            result = db.HandleData(sql);
            return result;
        }

        public string Checkingledger(string periodId, string accountId, string regionId)
        {
            string revalue = string.Empty;
            string sql = " IF EXISTS(select 1 from ACC_OPENINGBALANCES where periodId='" + periodId + "' and accountId='" + accountId + "' and regionId='" + regionId + "')" +
                         " BEGIN SELECT '1' END ELSE BEGIN SELECT '0' END";
           
            revalue = (string)db.GetSingleValue(sql);
            return revalue;
        }

        public string GetFinyearid(string periodId)
        {
            string revalue = string.Empty;
            string sql = " SELECT CAST(ID AS VARCHAR(50)) as ID FROM M_FINYEAR WHERE FinYear='" + periodId + "'";
            revalue = (string)db.GetSingleValue(sql);
            return revalue;
        }
        public string Getledgerid(string GNGNAME, string depotid)/* SELECT PRAYAASLEDGERID FROM  GNGMAPPRAYAS WHERE GNGNAME='" + GNGNAME + "'AND DEPOTID='" + depotid + "' change made by p.basu*/
        {
            string PRAYAASLEDGERID = string.Empty;
            string sql = " select id from Acc_AccountsInfo a with(nolock)  inner join Acc_Branch_Mapping b with(nolock) on a.id=b.LedgerID  where a.name = '" + GNGNAME + "' and b.BranchID = '" + depotid + "'";
            PRAYAASLEDGERID = (string)db.GetSingleValue(sql);
            return PRAYAASLEDGERID;
        }
        public string GetledgerName(string GNGNAME)  /*SELECT PRAYAASLEDGERNAME FROM  GNGMAPPRAYAS WHERE GNGNAME='" + GNGNAME + "' change made by p.basu*/
        {
            string PRAYAASLEDGERNAME = string.Empty;
            string sql = "SELECT NAME FROM  Acc_AccountsInfo WHERE name='" + GNGNAME + "'";
            PRAYAASLEDGERNAME = (string)db.GetSingleValue(sql);
            return PRAYAASLEDGERNAME;
        }

        #region Upload BRS


        public DataTable CreateTempOfUploadBRS()
        {
            //string sql = " SELECT A.CUSTOMERID AS CUSTOMERID,B.CUSTOMERNAME AS CUSTOMERNAME ,'' AS CREDIT,'' AS DEBIT  FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN " +
            //             " M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE A.DEPOTID='" + DEPOTID + "'";
            string SQL = "  SELECT '' AS GNGLEDGERNAME,'' AS DEPOTNAME,'' AS CREDIT,'' AS DEBIT,'' AS VOUCHERDATE,'' AS CHEQUENO,'' AS CHEQUEDATE,'' AS CHEQUEREALISEDNO," +
                         " '' AS CHEQUEREALISEDDATE,'' AS NARRATION";

            DataTable dt = db.GetData(SQL);
            return dt;
        }
        public int SaveBRSOpening(string DEPOTID, string BANKID, string BANKNAME, string OpeningBalanceDetails)
        {
            int result = 0;
            string sql = "USP_UPLOAD_ACC_BRS_OPENING '" + DEPOTID + "','" + BANKID + "','" + BANKNAME + "','" + OpeningBalanceDetails + "'";
            result = db.HandleData(sql);
            return result;
        }

        public string Getdepotid(string DEPOTNAME)
        {
            string sql = " EXEC USP_ACC_COSTCENTER_UPLOAD '" + DEPOTNAME + "'";
            string BRID = (string)db.GetSingleValue(sql);
            return BRID;
        }

        #endregion

        #region Upload BILL

        public DataTable CreateTempOfUploadBILL()
        {
            //string sql = " SELECT A.CUSTOMERID AS CUSTOMERID,B.CUSTOMERNAME AS CUSTOMERNAME ,'' AS CREDIT,'' AS DEBIT  FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN " +
            //             " M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE A.DEPOTID='" + DEPOTID + "'";
            string SQL = " SELECT '' AS LEDGERNAME,'' AS INVOICENO,'' AS CREDIT,'' AS DEBIT,'' AS INVOICEDATE,'' AS INVOICEOTHERS";

            DataTable dt = db.GetData(SQL);
            return dt;
        }
        public int SaveBill(string DEPOTID, string OpeningBalanceDetails)
        {
            int result = 0;
            string sql = "[USP_UPLOAD_ACCOUNT_INVOICE_DETAILS] '" + DEPOTID + "','" + OpeningBalanceDetails + "'";
            result = db.HandleData(sql);
            return result;
        }

        #endregion

        #region Upload CostCenter

        public string GETCOSTCATAGORYID(string BSNAME)
        {
            string BSID = string.Empty;
            string sql = " SELECT BSID FROM M_BUSINESSSEGMENT WHERE BSNAME='" + BSNAME + "'";
            BSID = (string)db.GetSingleValue(sql);
            return BSID;
        }

        public string GETCOSTCATAGORYNAME(string COSTCENTRENAME)
        {
            string COSTCENTREID = string.Empty;
            string sql = " SELECT COSTCENTREID FROM M_COST_CENTRE WHERE COSTCENTRENAME='" + COSTCENTRENAME + "'";
            COSTCENTREID = (string)db.GetSingleValue(sql);
            return COSTCENTREID;
        }

        public string GETDEPARTMENTNAME(string DEPTNAME)
        {
            string DEPTID = string.Empty;
            string sql = " SELECT DEPTID FROM M_DEPARTMENT WHERE DEPTNAME='" + DEPTNAME + "'";
            DEPTID = (string)db.GetSingleValue(sql);
            return DEPTID;
        }

        public DataTable CreateTempOfUploadCostCenter()
        {
            //string sql = " SELECT A.CUSTOMERID AS CUSTOMERID,B.CUSTOMERNAME AS CUSTOMERNAME ,'' AS CREDIT,'' AS DEBIT  FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN " +
            //             " M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE A.DEPOTID='" + DEPOTID + "'";
            string SQL = "  SELECT  '' AS GNGLEDGERNAME,''AS DEPOTNAME,'' AS COSTCATAGORYNAME,'' AS COSTCENTERNAME,'' AS CREDIT,'' AS DEBIT,'' AS DEPARTMENTNAME,'' AS NARRATION";

            DataTable dt = db.GetData(SQL);
            return dt;
        }

        public int SaveCOSTCENTER(string OpeningBalanceDetails)
        {
            int result = 0;
            string sql = "[USP_UPLOAD_COSTCENTER] '" + OpeningBalanceDetails + "'";
            result = db.HandleData(sql);
            return result;
        }

       
        #endregion



        #region Upload BRS v2


        public DataTable CreateTempOfUploadBRSV2()
        {
            //string sql = " SELECT A.CUSTOMERID AS CUSTOMERID,B.CUSTOMERNAME AS CUSTOMERNAME ,'' AS CREDIT,'' AS DEBIT  FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN " +
            //             " M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID WHERE A.DEPOTID='" + DEPOTID + "'";
            string SQL = "  SELECT ID  AS ID,NAME AS PRAYAASLEDGERNAME,'' AS CREDIT,'' AS DEBIT,'' AS VOUCHERDATE,'' AS CHEQUENO,'' AS CHEQUEDATE,'' AS CHEQUEREALISEDNO," +
                         " '' AS CHEQUEREALISEDDATE,'' AS NARRATION FROM ACC_ACCOUNTSINFO";

            DataTable dt = db.GetData(SQL);
            return dt;
        }
       
        public DataTable BindBankname()
        {
            DataTable dt = new DataTable();
            string Sql = " SELECT REFERENCE_LEDGERID as ID,BANKNAME  FROM M_BANKMASTER WHERE HOTAG='Y' ORDER BY BANKNAME";
            dt = db.GetData(Sql);
            return dt;
        }

        #endregion


        #region Upload MisMatch Ledger

        public DataTable CreateTempOfMisMatchLedger()
        {

            string SQL = " SELECT '' AS GNGNAME,'' AS PRAYAASLEDGERNAME,'' AS PRAYAASLEDGERID";

            DataTable dt = db.GetData(SQL);
            return dt;
        }
        public DataTable BindgGngMapPrayaasGridView(string DEPOTID)
        {

            string SQL = " SELECT  GNGNAME, PRAYAASLEDGERNAME, PRAYAASLEDGERID,BRID,BRPREFIX FROM GNGMAPPRAYAS INNER JOIN M_BRANCH ON GNGMAPPRAYAS.DEPOTID=M_BRANCH.BRID" +
                        " WHERE DEPOTID='" + DEPOTID + "'";
            DataTable dt = db.GetData(SQL);
            return dt;
        }

        public int SaveMismatcheLedger(string DEPOTID, string Mismatchledger)
        {
            int result = 0;
            string sql = "USP_UPLOAD_MISMATCH_LEDGER '" + DEPOTID + "','" + Mismatchledger + "'";
            result = db.HandleData(sql);
            return result;
        }

        public int DeleteGngMapPrayaas(string DEPOTID, string PRAYAASLEDGERID)
        {
            int result = 0;
            string sql = "DELETE FROM GNGMAPPRAYAS WHERE PRAYAASLEDGERID='" + PRAYAASLEDGERID + "' AND DEPOTID='" + DEPOTID + "'";
            result = db.HandleData(sql);
            return result;
        }

        #endregion

        #region COSTCENTER UPLOADV2

        public string GetledgeridV2(string GNGNAME, string DEPOTID)
        {
            string PRAYAASLEDGERID = string.Empty;
            string sql = " SELECT PRAYAASLEDGERID FROM  GNGMAPPRAYAS WHERE GNGNAME='" + GNGNAME + "'AND DEPOTID='" + DEPOTID + "'";
            PRAYAASLEDGERID = (string)db.GetSingleValue(sql);
            return PRAYAASLEDGERID;
        }

        public int SaveCOSTCENTERV2(string OpeningBalanceDetails)
        {
            int result = 0;
            string sql = "[USP_UPLOAD_COSTCENTER] '" + OpeningBalanceDetails + "'";
            result = db.HandleData(sql);
            return result;
        }
        #endregion

        public int SaveBRSOpeningV2(string DEPOTID, string BANKID, string BANKNAME, string VOUCHERTPYE, string OpeningBalanceDetails)
        {
            int result = 0;
            string sql = "USP_UPLOAD_ACC_BRS_OPENINGV2 '" + DEPOTID + "','" + BANKID + "','" + BANKNAME + "','" + VOUCHERTPYE + "','" + OpeningBalanceDetails + "'";
            result = db.HandleData(sql);
            return result;
        }
    }
}
