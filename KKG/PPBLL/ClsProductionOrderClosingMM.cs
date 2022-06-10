using DAL;
using System.Data;
using Utility;

namespace PPBLL
{
    public class ClsProductionOrderClosingMM
    {
        DBUtils db = new DBUtils();
        public DataTable BindProductionOrder(string fromdate, string todate, string FINYEAR, string DepotID, string ProductID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_FETCH_PRODUCTION_ORDER_FORCLOSING '" + fromdate + "','" + todate + "','" + FINYEAR + "','" + DepotID + "','" + ProductID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public int saveproductionOrderclosing(string xml)
        {
            int result = 0;
            string sql = "EXEC [USP_PRODUCTION_ORDER_CLOSING] '" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable BindBulkProduct(string DepotId)
        {
            string sql = " EXEC [USP_BIND_BULKPRODUCT] '" + DepotId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindBulkProduct_WithOutBatchno(string DepotId, string Finyear)
        {
            string sql = "EXEC [USP_BIND_BULKPRODUCT_WITHOUT_BATCHNO] '" + DepotId + "','" + Finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductionOrderUnlock(string fromdate, string todate, string FINYEAR, string DepotID, string ProductID)
        {
            string sql = string.Empty;
            sql = "EXEC [USP_FETCH_PRODUCTION_ORDER_UNLOCK] '" + fromdate + "','" + todate + "','" + FINYEAR + "','" + DepotID + "','" + ProductID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int saveproductionOrderclosingUnloak(string xml)
        {
            int result = 0;
            string sql = "EXEC [USP_PRODUCTION_ORDER_UNLOCK] '" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable LoadWastegePono(string fromdate, string todate, string Finyear, string DepotID, string ProductID)
        {
            string sql = string.Empty;
            sql = "EXEC [USP_BIND_WASTEGE_PONO] '" + fromdate + "','" + todate + "','" + Finyear + "','" + DepotID + "','" + ProductID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable LoadWastegeBatchNO(string PoID)
        {
            string sql = string.Empty;
            sql = "SELECT BATCHNO FROM T_PRODUCTION_ORDER_HEADER WHERE PRODUCTION_ORDERID='" + PoID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindWastageDtls(string fromdate, string todate, string Finyear, string DepotID, string ProductID, string ProductionID)
        {
            string sql = string.Empty;
            sql = "EXEC [USP_BIND_WASTEGE_DETAILS_POWISE] '" + fromdate + "','" + todate + "','" + Finyear + "','" + DepotID + "','" + ProductID + "','" + ProductionID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int SaveReworkingWastage(string ProductionID, string Batchno, string BulkID, string BulkName, string FinYear, string Xml, string DepotID)
        {
            int result = 0;
            string sql = "EXEC [CRUD_REWORKING_WASTAGE] '" + ProductionID + "','" + Batchno + "','" + BulkID + "','" + BulkName + "','" + FinYear + "','" + Xml + "','" + DepotID + "'";
            result = db.HandleData(sql);
            return result;
        }
    }
}
