using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsDepotRatesheet_RMPM
    {
        DBUtils db = new DBUtils();
        public DataTable BindRSGrid(string divid, string catid)
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT where DIVID='" + divid + "' AND CATID='" + catid + "' ORDER BY NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCustomerRateSheetGrid(string BrandID,string categoryID)
        {
            string sql = " SELECT PRODUCTID,PRODUCTNAME,RATE,"+
                         " CONVERT(VARCHAR(10),FROMDATE,103) AS FROMDATE,CONVERT(VARCHAR(10),TODATE,103) AS TODATE " +
                         " FROM M_DEPOT_TRANSFER_RATESHEET "+
                         " WHERE DIVISIONID= '" + BrandID + "' AND CATAGORYID='" + categoryID + "' "+
                         " ORDER BY PRODUCTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    
     
       

       

        public DataTable FetchProductDetails(string DivID, string CategoryID)
        {
            DataTable dt = new DataTable();
            string sql = "sp_Fetch_Depot_ItemDeatails '" + CategoryID + "' ,  '" + DivID + "'";

            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindCustomerRSGridbyid(string divid, string catid)
        {
            string sql = "SELECT PRODUCTID as ID,PRODUCTNAME AS NAME ,RATE from M_DEPOT_TRANSFER_RATESHEET where DIVISIONID='" + divid + "' AND CATAGORYID='" + catid + "' ORDER BY PRODUCTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeleteRateMaster(string divid, string catid)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from dbo.M_DEPOT_TRANSFER_RATESHEET where DIVISIONID='" + divid + "' AND CATAGORYID='" + catid + "' ";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        //public int SaveCustomerRateSheetMap(string divid, string divName, string catid, string catName, string pname, string pid, decimal rate, string Mode)
        //{
        //    int result = 0;
        //    string sqlstr;
        //    try
        //    {
        //        if (Mode == "A")
        //        {
        //            sqlstr = "insert into M_DEPOT_TRANSFER_RATESHEET(BSID,BSNAME,DIVISIONID,DIVISIONNAME,CATAGORYID,CATAGORYNAME,RATE,PRODUCTID,PRODUCTNAME)" +
        //                " values('-1','-1','" + divid + "','" + divName + "','" + catid + "','" + catName + "','" + rate + "','" + pid + "','" + pname + "')";
        //            result = db.HandleData(sqlstr);
        //        }
        //        else
        //        {
        //            sqlstr = "update M_DEPOT_TRANSFER_RATESHEET set PRODUCTID='" + pid + "', PRODUCTNAME='" + pname + "',RATE='" + rate + "'  where PRODUCTID= '" + pid + "'";
        //            result = db.HandleData(sqlstr);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogFiles Errlog = new CreateLogFiles();
        //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
        //    }

        //    return result;
        //}

        public int SaveCustomerRateSheetMap(string xml, string fromdate, string todate)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = " exec [USP_INSERT_DEPOT_RATESHEET] '" + xml + "','" + fromdate + "','" + todate + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public int CustomerRatesheetDelete(string ID, string rate, string fromdate, string todate)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from M_DEPOT_TRANSFER_RATESHEET where PRODUCTID= '" + ID + "' AND RATE='" + rate + "' AND " +
                         " FROMDATE=CONVERT(DATETIME,'" + fromdate + "',103) AND TODATE=CONVERT(DATETIME,'" + todate + "',103)";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }


        public DataTable BindPrimaryitemtype()
        {
            string sql = "SELECT ITEM_NAME+'~'+CAST(ID AS VARCHAR(5)) AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ACTIVE='Y' AND ID <> 1 ORDER BY ITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSubitemtype(string PTYPEID)
        {
            string sql = "SELECT SUBTYPEID,SUBITEMDESC FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID='" + PTYPEID + "' AND ACTIVE='Y' ORDER BY SUBITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



    }

    }

