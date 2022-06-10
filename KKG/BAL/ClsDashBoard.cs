using DAL;
using System.Data;

namespace BAL
{
    public class ClsDashBoard
    {
        DBUtils db = new DBUtils();
        //DepoEntities PrayaasDB = new DepoEntities();

        /*public DataSet Bindchart(string DEPOTID, string StartDate,string EndDate,string UserID,string FINYEAR)
        {
            string sql = " EXEC USP_GRAPH '" + DEPOTID + "','"+ StartDate + "','"+ EndDate + "','"+ UserID + "','" + FINYEAR + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public DataSet BindchartSO_ASM(string StartDate, string EndDate, string UserID, string FINYEAR,string UserType)
        {
            string sql = " EXEC USP_GRAPH_SO_ASM '" + StartDate + "','" + EndDate + "','" + UserID + "','" + FINYEAR + "','"+ UserType + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }*/

        /*public DashboardGraph dashBoardChart(string DepotID, string StartDate, string EndDate, string UserID, string FinYear, string BSegment)
        {

            var command = PrayaasDB.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[USP_GRAPH]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@P_DEPOTID", DepotID));
            command.Parameters.Add(new SqlParameter("@P_STARTDATE", StartDate));
            command.Parameters.Add(new SqlParameter("@P_ENDDATE", EndDate));
            command.Parameters.Add(new SqlParameter("@P_USERID", UserID));
            command.Parameters.Add(new SqlParameter("@P_FINYEAR", FinYear));
            command.Parameters.Add(new SqlParameter("@P_BSEGMENT", BSegment));
            ObjectParameter[] param1 = new ObjectParameter[1];
            PrayaasDB.Database.Connection.Open();
            var reader = command.ExecuteReader();
            List<CategoryWiseSale> _listCategoryWiseSale = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<CategoryWiseSale>(reader).ToList();
            reader.NextResult();
            List<BrandWiseSale> _listBrandWiseSale = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<BrandWiseSale>(reader).ToList();
            reader.NextResult();
            List<StateWiseSale> _listStateWiseSale = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<StateWiseSale>(reader).ToList();
            reader.NextResult();
            List<BrandSale> _listBrandSale = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<BrandSale>(reader).ToList();
            reader.NextResult();
            List<DistributorSale> _listDistributorSale = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<DistributorSale>(reader).ToList();
            reader.NextResult();
            List<ProductSale> _listProductSale = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<ProductSale>(reader).ToList();
            reader.NextResult();
            List<TargetAch> _listTargetAch = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<TargetAch>(reader).ToList();
            reader.NextResult();
            List<MonthlySaleTrend> _listMonthlySaleTrend = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<MonthlySaleTrend>(reader).ToList();
            reader.NextResult();
            List<WeeklySaleTrend> _listWeeklySaleTrend = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<WeeklySaleTrend>(reader).ToList();


            DashboardGraph domainentity = new DashboardGraph();
            domainentity.CategoryMonthSale = _listCategoryWiseSale;
            domainentity.BrandMonthSale = _listBrandWiseSale;
            domainentity.StateMonthSale = _listStateWiseSale;
            domainentity.BrandBarSale = _listBrandSale;
            domainentity.DistributorMonthSale = _listDistributorSale;
            domainentity.ProductMonthSale = _listProductSale;
            domainentity.TargetAchValue = _listTargetAch;
            domainentity.MonthlySaleTrendValue = _listMonthlySaleTrend;
            domainentity.WeeklySaleTrendValue = _listWeeklySaleTrend;


            PrayaasDB.Database.Connection.Close();
            return domainentity;
        }

        public ForecastStockGraph forecastStockChart(string UserID, string Date, string Brand, string Category, string BSegment, string Product, string DepotID)
        {

            var command = PrayaasDB.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[USP_GRAPH_FORECAST_STOCK]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@P_USERID", UserID));
            command.Parameters.Add(new SqlParameter("@P_DATE", Date));
            command.Parameters.Add(new SqlParameter("@P_BRAND", Brand));
            command.Parameters.Add(new SqlParameter("@P_CATEGORY", Category));
            command.Parameters.Add(new SqlParameter("@P_BSEGMENT", BSegment));
            command.Parameters.Add(new SqlParameter("@P_PRODUCTID", Product));
            command.Parameters.Add(new SqlParameter("@P_DEPOTID", DepotID));
            ObjectParameter[] param1 = new ObjectParameter[1];
            PrayaasDB.Database.Connection.Open();
            var reader = command.ExecuteReader();
            List<ForeCastStock> _listForeCastStock = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<ForeCastStock>(reader).ToList();



            ForecastStockGraph domainentity = new ForecastStockGraph();
            domainentity.ForeCastStockStatus = _listForeCastStock;
            PrayaasDB.Database.Connection.Close();
            return domainentity;
        }

        public DashboardGraphFilter forecastStockChartFilter()
        {

            var command = PrayaasDB.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[USP_GET_BRAND_CATEGORY_BS_PRODUCT_GRAPH]";
            command.CommandType = CommandType.StoredProcedure;
            ObjectParameter[] param1 = new ObjectParameter[1];
            PrayaasDB.Database.Connection.Open();
            var reader = command.ExecuteReader();
            List<GraphBrand> _listGraphBrand = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<GraphBrand>(reader).ToList();
            reader.NextResult();
            List<GraphCategory> _listGraphCategory = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<GraphCategory>(reader).ToList();
            reader.NextResult();
            List<GraphBS> _listGraphBS = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<GraphBS>(reader).ToList();
            reader.NextResult();
            List<GraphProduct> _listGraphProduct = ((IObjectContextAdapter)PrayaasDB).ObjectContext.Translate<GraphProduct>(reader).ToList();


            DashboardGraphFilter domainentity = new DashboardGraphFilter();
            domainentity.GraphBrandItem = _listGraphBrand;
            domainentity.GraphCategoryItem = _listGraphCategory;
            domainentity.GraphBSItem = _listGraphBS;
            domainentity.GraphProductItem = _listGraphProduct;
            PrayaasDB.Database.Connection.Close();
            return domainentity;
        }*/

        public DataTable LoadNotification(string userid, string finyear) /*NEW ADDED FOR NOTIFICATION ON 06/12/2019 BY P.BASU*/
        {
            DataTable dt = new DataTable();
            string sql = "exec[USP_PROC_NOTIFY_PO] '" + userid + "','" + finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable LoadGrnNotification(string userid, string finyear) /*NEW ADDED FOR NOTIFICATION ON 11/12/2019 BY P.BASU*/
        {
            DataTable dt = new DataTable();
            string sql = "exec[USP_GRN_NOTIFICATION_FACTORY] '" + userid + "','" + finyear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ALERTSHOW(string userid) /*NEW ADDED FOR NOTIFICATION ON 11/12/2019 BY P.BASU*/
        {
            DataTable dt = new DataTable();
            string sql = "exec[USP_ALERT_SHOW_FACTORY] '" + userid + "'";
            dt = db.GetData(sql);
            return dt;
        }
    }
}
