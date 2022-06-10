using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsRateSheetMaster
    {
        DBUtils db = new DBUtils();
        public DataTable BindRateMasterGrid()
        {
            string sql = "SELECT * FROM [M_TPU__PRODUCT_RATESHEET] ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRateMasterById(string id)
        {
            string sql = "SELECT * FROM [M_PRODUCT] WHERE ID='" + id + "' ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCurrency(string GroupID)
        {
            string sql = " SELECT CURRENCYID,CURRENCYNAME FROM M_DISTRIBUTER_CATEGORY" +
                         " WHERE DIS_CATID='" + GroupID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveRateMaster(string ID, string VenId, string VenName, string Mode)
        {
            int result = 0;
            string sqlstr;
            //string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_TPU__PRODUCT_RATESHEET(ID,VENDORID,VENDORNAME)" +
                           " values(NEWID(),'" + VenId + "','" + VenName + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_TPU__PRODUCT_RATESHEET set VENDORID='" + VenId + "',VENDORNAME='" + VenName + "'  where ID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }
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
        public int DeleteRateMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from [M_TPU__PRODUCT_RATESHEET] where ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable BindVendor()
        {
            string sql = "SELECT VENDORID, VENDORNAME FROM M_TPU_VENDOR where TAG='T' ORDER BY VENDORNAME";
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
        public DataTable BindSuppliedItem()
        {
            string sql = "SELECT ID,ITEMDESC FROM M_SUPLIEDITEM ORDER BY ITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSubitemtype(string id)
        {
            string sql = "SELECT SUBTYPEID,SUBITEMDESC FROM M_PRIMARY_SUB_ITEM_TYPE WHERE ACTIVE='Y' AND PRIMARYITEMTYPEID='" + id + "' ORDER BY SUBITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPrimaryitemtype()
        {
            string sql = "SELECT ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ACTIVE='Y' AND ID <> 1 ORDER BY ITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        /*Added By Sunil Das On 30.07.18*/
        public DataTable Brand()
        {
            string sql = "SELECT DIVID AS ID,DIVNAME AS ITEMDESC FROM M_DIVISION ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Category()
        {
            string sql = "SELECT CATID AS SUBTYPEID,CATNAME AS SUBITEMDESC FROM M_CATEGORY  ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindActualPurchaseRate(string TPUID, string Finyear)
        {
            string sql = "EXEC USP_TPU_PURCHASE_RATE 'D','" + TPUID + "','" + Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindActualPurchaseRateDifference(string TPUID, string Finyear)
        {
            string sql = "EXEC USP_RPT_TPU_PURCHASE_RATE '" + TPUID + "','" + Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveActualPurchaseRate(string TPUID, string Finyear, string Xml, string CreatedBy)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC USP_TPU_PURCHASE_RATE 'I','" + TPUID + "','" + Finyear + "','" + Xml + "','" + CreatedBy + "'";
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
