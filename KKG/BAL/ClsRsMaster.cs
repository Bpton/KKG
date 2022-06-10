using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsRsMaster
    {

        DBUtils db = new DBUtils();
        public DataTable BindRSGrid(string divid, string catid)
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT where DIVID='" + divid + "' AND CATID='" + catid + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactory()
        {
            string sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR where TAG='F' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindVendorByItem(string ID)
        {
            //string sql = "SELECT * FROM M_TPU_VENDOR WHERE SUPLIEDITEMID='" + ID + "' AND TAG<>'F' ORDER BY VENDORNAME";

            string sql = " SELECT DISTINCT A.VENDORID , A.VENDORNAME  FROM M_TPU_VENDOR A " +
                         //"inner join M_PRODUCT_TPU_MAP B on A.VENDORID=B.VENDORID " +
                         //" INNER JOIN M_PRODUCT C ON C.ID=B.PRODUCTID INNER JOIN M_SUPLIEDITEM D ON D.ITEMCODE=C.TYPE " +
                         " WHERE  A.TAG<>'F' ORDER BY VENDORNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindVendor()
        {
            string sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR WHERE TAG<>'F' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRateSheetGrid(string VENDORID)
        {
            //string sql = "SELECT ID,PRODUCTID,PRODUCTNAME,RMCOST,TRANSFERCOST,PCS,CONVERT(VARCHAR(10),FROMDATE,103) AS FROMDATE,CONVERT(VARCHAR(10),TODATE,103) AS TODATE FROM M_TPU__PRODUCT_RATESHEET WHERE VENDORID= '" + VENDORID + "' ORDER BY PRODUCTID";
            string sql = "EXEC USP_RETRIVE_RATESHEET '" + VENDORID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindRateSheetGrid1(string VENDORID)
        {
            //string sql = "SELECT ID,PRODUCTID,PRODUCTNAME,RMCOST,TRANSFERCOST,PCS,CONVERT(VARCHAR(10),FROMDATE,103) AS FROMDATE,CONVERT(VARCHAR(10),TODATE,103) AS TODATE FROM M_TPU__PRODUCT_RATESHEET WHERE VENDORID= '" + VENDORID + "' ORDER BY PRODUCTID";
            string sql = "EXEC USP_RETRIVE_RATESHEET_FACTORY '" + VENDORID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVendorNameBasisOnProduct(string PRODUCTNAME)
        {
            string sql = "SELECT ID,PRODUCTID,PRODUCTNAME,VENDORID,vendorname,RMCOST,TRANSFERCOST,PCS FROM M_TPU__PRODUCT_RATESHEET WHERE PRODUCTNAME= '" + PRODUCTNAME + "' ORDER BY PRODUCTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRateBasisOnVendor(string VENDORID, string PRODUCTNAME)
        {
            string sql = "SELECT ID,PRODUCTID,PRODUCTNAME,RMCOST,TRANSFERCOST,PCS FROM M_TPU__PRODUCT_RATESHEET WHERE VENDORID= '" + VENDORID + "' AND PRODUCTNAME= '" + PRODUCTNAME + "'  ORDER BY PRODUCTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVendorName(string VENDORID)
        {
            string sql = "select [VENDORNAME] from [M_TPU_VENDOR] where  VENDORID='" + VENDORID + "' ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchProductDetails(string pid, string Vid)
        {
            DataTable dt = new DataTable();
            string DivID = "";
            string CategoryID = "";
            string UOMID = "";
            string UNITNAME = "";
            //string Name = "";

            string sql = "SELECT DIVID,CATID,UOMID,(UNITVALUE+' '+UOMNAME) AS UNITNAME FROM M_PRODUCT WHERE ID='" + pid + "'";
            dt = db.GetData(sql);

            DivID = dt.Rows[0]["DIVID"].ToString();
            CategoryID = dt.Rows[0]["CATID"].ToString();
            UOMID = dt.Rows[0]["UOMID"].ToString();
            UNITNAME = dt.Rows[0]["UNITNAME"].ToString();

            sql = "EXEC sp_FetchItemDeatails '" + CategoryID + "' ,  '" + DivID + "' , '" + Vid + "' ";

            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchSuppliedItem(string VendorID)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT SUPLIEDITEMID,SUPLIEDITEM FROM M_TPU_VENDOR WHERE VENDORID='" + VendorID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchProductDetails(string DivID, string CategoryID, string Vid)
        {
            DataTable dt = new DataTable();
            string sql = "SP_FETCHITEMDEATAILS '" + CategoryID + "' ,  '" + DivID + "' , '" + Vid + "' ";

            dt = db.GetData(sql);
            return dt;
        }
        public int SaveTpuRateSheet(string fromdate, string todate, string vendorid, string xml)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "USP_INSERT_TPU_FACTORY_RATESHEET '" + fromdate + "','" + todate + "','" + vendorid + "','" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable BindRSGridbyid(string divid, string catid)
        {
            string sql = "SELECT PRODUCTID AS ID,PRODUCTNAME AS NAME,RMCOST,TRANSFERCOST FROM dbo.M_TPU__PRODUCT_RATESHEET where DIVISIONID='" + divid + "' AND CATAGORYID='" + catid + "' ORDER BY PRODUCTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteRateMaster(string divid, string catid, string vid)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "DELETE FROM dbo.M_TPU__PRODUCT_RATESHEET where DIVISIONID='" + divid + "' AND CATAGORYID='" + catid + "' AND VENDORID='" + vid + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public int SaveRateSheetMap(string ID, string VenId, string VenName, string divid, string divName, string catid, string catName, string pname, string pid, decimal rmcost, decimal trnscost, string uomid, string uomname, decimal pcs)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                if (dt.Rows.Count == 0)
                {

                    //if (Mode == "A")
                    //{
                    sqlstr = "INSERT INTO M_TPU__PRODUCT_RATESHEET(ID,VENDORID,VENDORNAME,DIVISIONID,DIVISIONNAME,CATAGORYID,CATAGORYNAME,RMCOST,TRANSFERCOST,PRODUCTID,PRODUCTNAME,UOMID,UOMNAME,PCS)" +
                       " VALUES(NEWID(),'" + VenId + "','" + VenName + "','" + divid + "','" + divName + "','" + catid + "','" + catName + "','" + rmcost + "','" + trnscost + "','" + pid + "','" + pname + "','" + uomid + "','" + uomname + "','" + pcs + "')";
                    result = db.HandleData(sqlstr);
                    //}
                    //else
                    //{
                    //    sqlstr = "update M_TPU__PRODUCT_RATESHEET set PRODUCTID='" + pid + "', PRODUCTNAME='" + pname + "',RMCOST='" + rmcost + "',TRANSFERCOST='" + trnscost + "'  where ID= '" + ID + "'";
                    //    result = db.HandleData(sqlstr);
                    //}
                }

                else
                {
                    // result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public int RatesheetDelete(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "DELETE FROM M_TPU__PRODUCT_RATESHEET WHERE ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        /*Added By Sunil Das On 30.07.18*/
        public DataTable FetchACRProductDetails(string DivID, string CategoryID)
        {
            DataTable dt = new DataTable();
            string sql = "SP_FETCH_ACR_DEATAILS '" + CategoryID + "' ,  '" + DivID + "'";

            dt = db.GetData(sql);
            return dt;
        }

        public int SaveACRRate(string fromdate, string todate, string xml)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "USP_INSERT_ACR_RATESHEET '" + fromdate + "','" + todate + "','" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable BindACRGrid()
        {
            string sql = " SELECT ID,PRODUCTID,PRODUCTNAME,RMCOST,TRANSFERCOST,PCS,CONVERT(VARCHAR(10),FROMDATE,103) AS FROMDATE," +
                         " CONVERT(VARCHAR(10),TODATE,103) AS TODATE " +
                         " FROM M_ACR_RATESHEET " +
                         " ORDER BY PRODUCTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DeleteRateACR(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from dbo.M_ACR_RATESHEET WHERE ID='" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
    }
}
