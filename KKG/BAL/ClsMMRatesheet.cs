using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsMMRatesheet
    {
        DBUtils db = new DBUtils();
        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID, BSNAME FROM M_BUSINESSSEGMENT WHERE ACTIVE='True ' ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomer(string BSID)
        {
            string sql = " SELECT A.CUSTOMERID AS CUSTOMERID,A.CUSTOMERNAME AS CUSTOMERNAME FROM M_CUSTOMER AS A " +
                         " INNER JOIN M_CUSTOMER_PURCHASETYPE B ON A.CUSTOMERID=B.CUSTOMERID /*AND B.PURCHASETYPE='PM,RM'*/ "+
                        // " WHERE A.BUSINESSSEGMENTID='" + BSID + "'" +
                         " ORDER BY A.CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCurrency()
        {
            string sql = " SELECT DISTINCT CURRENCYID,CURRENCYNAME FROM M_DISTRIBUTER_CATEGORY " ;
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPrimaryitemtype()
        {
            string sql = " USP_MM_LOADBRAND";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSubitemtype(string PTYPEID)
        {            
            string sql = "EXEC USP_MM_LOADCATEGORY '" + PTYPEID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchProductINFO(string DivID, string CategoryID, string BSID, string GroupID, string CurrencyID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC SP_FETCH_MM_CUSTOMER_ITEMDEATAILS   '" + DivID + "' ,'" + CategoryID + "' , '" + BSID + "' , '" + GroupID + "','" + CurrencyID + "' ";
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteRateMaster(string divid, string catid, string bid, string grpid, string pid)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from dbo.M_MM_CUSTOMER_RATESHEET where DIVISIONID='" + divid + "' AND CATAGORYID='" + catid + "' AND BSID='" + bid + "' AND CUSTOMERID='" + grpid + "' AND PRODUCTID='" + pid + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public int SavePrincipleRateSheetMap(string ID, string BSId, string BSName, string divid, string divName, string catid, string catName, string pname,
                                             string pid, decimal Rate, string Mode, decimal Pcs, string CurrencyId, string CurrencyName, string CUSTOMERID,
                                             string CUSTOMERNAME,string uomid,string uomname,string date,string finyear)
        {
            int result = 0;
            string sqlstr;
            try
            {
                if (Mode == "A")
                {
                    sqlstr = "USP_Save_PrincipleRate_SheetMap '"+ BSId + "','" + BSName + "','" + divid + "','" + divName + "','" + catid + "'," +
                        "'"+ catName + "','"+ pname + "','"+ pid + "','"+ Rate + "','"+ Mode + "','"+ Pcs + "','"+ CurrencyId + "'," +
                        "'"+ CurrencyName + "','"+ CUSTOMERID + "','"+ CUSTOMERNAME + "','"+ uomid + "','"+ uomname + "','"+ date + "','"+ finyear + "'";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    //sqlstr = "update M_CUSTOMER_PRODUCT_RATESHEET set PRODUCTID='" + pid + "', PRODUCTNAME='" + pname + "',RATE='" + Rate + "',PCS='" + Pcs + "',CURRENCYID='" + CurrencyId + "',CURRENCYNAME='" + CurrencyName + "'  where CPR_ID= '" + ID + "'";
                    //result = db.HandleData(sqlstr);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public int PrincipleRatesheetDelete(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "DELETE FROM M_MM_CUSTOMER_RATESHEET WHERE ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public DataTable BindprincipleGrid(string divid, string catid)
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT where DIVID='" + divid + "' AND CATID='" + catid + " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPrincipleRateSheetGrid(string CUSTOMERID)
        {
            string sql = " SELECT BSID,BSNAME,ID,PRODUCTID,PRODUCTNAME,RATE,PCS,UOMNAME,CASE WHEN ISAPPROVE='Y' THEN 'APPROVE' ELSE 'PENDING' END AS ISAPPROVE,FROMDATA AS ENTRYDATE  FROM M_MM_CUSTOMER_RATESHEET" +
                         " WHERE CUSTOMERID='" + CUSTOMERID + "'" +
                         " ORDER BY BSNAME,PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        
        /*EDIT*/
        public DataTable FetchProductDetails(string pid, string principleid, string bsid)
        {
            DataTable dt = new DataTable();
            string DivID = "";
            string CategoryID = "";

            string sql = "SELECT DIVID,CATID FROM M_PRODUCT WHERE ID='" + pid + "'";
            dt = db.GetData(sql);

            DivID = dt.Rows[0]["DIVID"].ToString();
            CategoryID = dt.Rows[0]["CATID"].ToString();

            sql = "sp_Fetch_PrincipleGroup_ItemDeatails '" + CategoryID + "' ,  '" + DivID + "' , '" + principleid + "', '" + bsid + "','" + pid + "'";

            dt = db.GetData(sql);
            return dt;
        }
        /*-------------*/
        public DataTable BindCustomerRSGridbyid(string divid, string catid, string bsid)
        {
            string sql = "SELECT PRODUCTID as ID,PRODUCTNAME AS NAME ,MRP from M_CUSTOMER_PRODUCT_RATESHEET where DIVISIONID='" + divid + "' AND CATAGORYID='" + catid + "' AND BSID='" + bsid + "' ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable TradingSaleRateApprove(string pid, string bsid)
        {
            string sql = "UPDATE M_MM_CUSTOMER_RATESHEET SET ISAPPROVE='Y' WHERE PRODUCTID='" + pid + "'  AND BSID='" + bsid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable RateSheetGridBusinesssegmentWise(string bsid) /*new add by p.basu on 24-02-2021*/
        {
            string sql = "USP_FETCH_RATESHEET_BY_BSWISE '"+ bsid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
