using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
namespace BAL
{
    public class ClsDepotOrderClosing
    {
        DBUtils db = new DBUtils();
        public int DayEndProcess()
        {
            int result = 0;
            string sql = " EXEC USP_CLOSESALEORDER ";
            result = db.HandleData(sql);
            return result;
        }

        public string LastDayEnd()
        {
            string result = string.Empty;
            string sql = "SELECT ISNULL(CONVERT(VARCHAR(10),CAST(MAX( CLOSEDDATE ) AS DATE),103),'0') AS LASTDATE FROM T_SALEORDER_HEADER";
            result = (string)db.GetSingleValue(sql);
            return result;

        }

        public string LastDayEndOfflineProcess()
        {
            string result = string.Empty;
            string sql = "SELECT ISNULL(CONVERT(VARCHAR(10),CAST(MAX( DAYENDDATE ) AS DATE),103),'0') AS LASTDATE FROM T_DAYEND_DETAILS";
            result = (string)db.GetSingleValue(sql);
            return result;

        }
     
        public int USPDayEndProcess(string DAYENDDATE, string ModuleID, string BSID, string InvoiceID,string Depotid)
        {
            int result = 0;
            string sql = " EXEC [USP_DAYEND_OFFLINE] '" + DAYENDDATE + "','" + ModuleID + "','" + BSID + "','" + InvoiceID + "','"+ Depotid+ "'";
            result = db.HandleData(sql);
            return result;
        }


        #region Add By Rajeev 04-10-2017
        public string LastDayEndPoOrder()
        {
            string result = string.Empty;
            string sql = "SELECT ISNULL(CONVERT(VARCHAR(10),CAST(MAX( CLOSEDDATE ) AS DATE),103),'0') AS LASTDATE FROM T_MM_POHEADER";
            result = (string)db.GetSingleValue(sql);
            return result;
        }
        public DataTable FetchPoOrder(string Fromdt, string Todt,string TPUID,string STATUS)
        {
            string sql = " EXEC [USP_FETCH_TPU_PO_OPEN_CLOSE] '" + Fromdt + "','" + Todt + "','" + TPUID + "','"+ STATUS +"'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int SaveTpuPoOrderClosing(string xml, string STATUS, string UserID)
        {
            int result = 0;
            string sql = "EXEC [USP_TPU_PO_CLOSING_UPDATE] '" + xml + "','" + STATUS + "','" + UserID + "'";
            result = db.HandleData(sql);
            return result;
        }

        #endregion
        public DataTable BindTPU()
        {
            string sql = "EXEC [SP_LOAD_TPU] ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindModule()
        {
            string sql = " SELECT '0' AS MODULEID , 'ALL' AS MODULENAME " +
                         " UNION " +
                         " SELECT MODULEID,MODULENAME FROM P_MODULE_MASTER " +
                         " WHERE MODULEID NOT IN ('1','10','99','3')" +
                         " ORDER BY MODULEID,MODULENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindInvoice(string InvoiceType, string DepotID, string BSID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();

            if (InvoiceType == "7") /*SALE INVOICE*/
            {
                if (BSID == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5") /*OTHERS (TRADING INVOICE)*/
                {
                    sql = " SELECT SALEINVOICEID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO) AS SALEINVOICENO FROM T_MM_SALEINVOICE_HEADER " +
                          " WHERE DAYENDTAG='N' AND DEPOTID='" + DepotID + "' AND BSID='" + BSID + "'";
                }
                else
                {
                    sql = " SELECT SALEINVOICEID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO) AS SALEINVOICENO FROM T_SALEINVOICE_HEADER " +
                          " WHERE DAYENDTAG='N' AND DEPOTID='" + DepotID + "' AND BSID='" + BSID + "'";
                }
            }
            else if (InvoiceType == "8") /*SALE ORDER*/
            {
                if (BSID == "A0A1E83E-1993-4FB9-AF53-9DD595D09596" || BSID == "C5038911-9331-40CF-B7F9-583D50583592" || BSID == "8AE0E8C9-F4F7-4382-B8AB-870A64ABF996") /*CPC,CSD,INCS*/
                {
                    sql = " SELECT SALEORDERID AS SALEINVOICEID,REFERENCESALEORDERNO AS SALEINVOICENO FROM T_SALEORDER_HEADER " +
                          " WHERE DAYENDTAG='N' AND BSID='" + BSID + "'";
                }
                else if (BSID == "33F6AC5E-1F37-4B0F-B959-D1C900BB43A5")  /*OTHERS (TRADING INVOICE)*/
                {
                    sql = " SELECT SALEORDERID AS SALEINVOICEID,(SALEORDERNO+'(REF PO-'+REFERENCESALEORDERNO)+')' AS SALEINVOICENO FROM T_MM_SALEORDER_HEADER " +
                          " WHERE DAYENDTAG='N' AND BSID='" + BSID + "'";
                }
                else
                {
                    sql = " SELECT SALEORDERID AS SALEINVOICEID,(SALEORDERNO+'(REF PO-'+REFERENCESALEORDERNO)+')' AS SALEINVOICENO FROM T_SALEORDER_HEADER " +
                         " WHERE DAYENDTAG='N' AND BSID='" + BSID + "'";
                }
            }
            else if (InvoiceType == "4") /*SALE RETURN*/
            {
                sql = " SELECT SALERETURNID AS SALEINVOICEID,(SALERETURNPREFIX+'/'+SALERETURNNO) AS SALEINVOICENO FROM T_SALERETURN_HEADER " +
                      " WHERE DAYENDTAG='N' AND DEPOTID='" + DepotID + "' AND BSID='" + BSID + "'";
            }

            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPurchaseReturn(string DepotID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = " SELECT PURCHASERETURNID AS SALEINVOICEID,(PURCHASERETURNPREFIX+'/'+PURCHASERETURNNO) AS SALEINVOICENO FROM T_PURCHASERETURN_HEADER " +
                  " WHERE DAYENDTAG ='N' AND SYNCTAG ='N' AND DEPOTID='" + DepotID + "'";
            dt = db.GetData(sql);
            return dt;
        }

    }
}
