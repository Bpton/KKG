using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using FactoryModel;
using System.Collections.Generic;
using System;

namespace FactoryDatacontext
{
    public class FactoryFGCostingContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<BranchList> LoadBranch(string UserID)
        {
            List<BranchList> branchlist = new List<BranchList>();
            try
            {
                branchlist = _db.Query<BranchList>("USP_FACTORY_LIST", new { P_USERID = UserID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return branchlist;
        }

        public List<DivisionList> LoadDivision()
        {
            List<DivisionList> divisionlist = new List<DivisionList>();
            try
            {
                divisionlist = _db.Query<DivisionList>("USP_DIVISION_LIST", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return divisionlist;
        }

        public List<CategoryList> LoadCategory(string DIVID)
        {
            List<CategoryList> categorylist = new List<CategoryList>();
            try
            {
                categorylist = _db.Query<CategoryList>("USP_CATEGORY_LIST", new { P_DIVID = DIVID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return categorylist;
        }

        public List<ProductList> LoadFGItem(string DIVID, string CATID, string BRANCHID)
        {
            List<ProductList> fgitemlist = new List<ProductList>();
            try
            {
                fgitemlist = _db.Query<ProductList>("USP_FG_ITEM_LIST",
                            new
                            {
                                P_DIVID = DIVID,
                                P_CATID = CATID,
                                P_BRANCHID = BRANCHID
                            }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fgitemlist;
        }

        public List<FGRateSheetModel> LoadFGRateSheetList(string BRANCHID, string DIVID, string CATID, string FROMDATE, string TODATE, char FLAG)
        {
            List<FGRateSheetModel> fgratesheetlist = new List<FGRateSheetModel>();
            try
            {
                fgratesheetlist = _db.Query<FGRateSheetModel>("USP_FACTORY_FG_RATESHEET",
                                    new
                                    {
                                        P_BRANCHID = BRANCHID,
                                        P_DIVID = DIVID,
                                        P_CATID = CATID,
                                        P_FROMDATE = FROMDATE,
                                        P_TODATE = TODATE,
                                        P_FLAG = FLAG
                                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fgratesheetlist;
        }

        public DataTable ratesheetdetails(List<FGRateSheetMasterList> RatesheetDetails)
        {
            DataTable dtrate = new DataTable();
            dtrate.Columns.Add("PRODUCTID", typeof(string));
            dtrate.Columns.Add("PRODUCTNAME", typeof(string));
            dtrate.Columns.Add("UNITVALUE", typeof(string));
            dtrate.Columns.Add("UOMNAME", typeof(string));
            dtrate.Columns.Add("RMCOST", typeof(decimal));
            dtrate.Columns.Add("PMCOST", typeof(decimal));
            dtrate.Columns.Add("CONVERSIONCOST", typeof(decimal));
            dtrate.Columns.Add("OVERHEADCOST", typeof(decimal));
            dtrate.Columns.Add("OTHERCOST", typeof(decimal));
            dtrate.Columns.Add("TOTALCOST", typeof(decimal));
            dtrate.Columns.Add("PCS", typeof(int));

            foreach (var item in RatesheetDetails)
            {
                dtrate.Rows.Add(item.PRODUCTID,
                                    item.PRODUCTNAME,
                                    item.UNITVALUE,
                                    item.UOMNAME,
                                    item.RMCOST,
                                    item.PMCOST,
                                    item.CONVERSIONCOST,
                                    item.OVERHEADCOST,
                                    item.OTHERCOST,
                                    item.TOTALCOST,
                                    item.PCS
                            );
            }
            return dtrate;
        }

        public List<MessageModel> FGRateSheetInsertUpdate(FGRateSheetModel ratesheetmodel)
        {
            DataTable dtrate = new DataTable();
            dtrate = ratesheetdetails(ratesheetmodel.FGRateSheet);

            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("USP_FACTORY_FG_RATESHEET_INSERT_UPDATE",
                            new
                            {
                                P_BRANCHID = ratesheetmodel.BRID,
                                P_BRANCHNAME = ratesheetmodel.BRNAME,
                                P_DIVID = ratesheetmodel.DIVID,
                                P_DIVNAME = ratesheetmodel.DIVNAME,
                                P_CATID = ratesheetmodel.CATID,
                                P_CATNAME = ratesheetmodel.CATNAME,
                                P_FROMDATE = ratesheetmodel.FROMDATE,
                                P_TODATE = ratesheetmodel.TODATE,
                                P_USERID = ratesheetmodel.USERID,
                                P_CHECKER = ratesheetmodel.CHECKER,
                                P_FGRateSheetDetails = dtrate.AsTableValuedParameter("Type_Factory_FGRatesheet")
                            },
                            commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtrate = null;
            }
            return response;
        }

        public List<FGBOMList> LoadFGBOMReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            List<FGBOMList> fgratesheetlist = new List<FGBOMList>();
            try
            {
                fgratesheetlist = _db.Query<FGBOMList>("USP_RPT_FG_BOM_REPORT",
                                    new
                                    {
                                        P_ITEMID = ITEMID,
                                        P_BRANCHID = BRANCHID,
                                        P_FROMDATE = FROMDATE,
                                        P_TODATE = TODATE
                                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fgratesheetlist;
        }

        public List<FGBulkCostList> LoadFGBulkCostReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            List<FGBulkCostList> fgbulkcostlist = new List<FGBulkCostList>();
            try
            {
                fgbulkcostlist = _db.Query<FGBulkCostList>("USP_RPT_FG_BULK_COST_REPORT",
                                    new
                                    {
                                        P_ITEMID = ITEMID,
                                        P_BRANCHID = BRANCHID,
                                        P_FROMDATE = FROMDATE,
                                        P_TODATE = TODATE
                                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fgbulkcostlist;
        }

        public List<FGBulkBOMList> LoadFGBulkBOMReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE)
        {
            List<FGBulkBOMList> fgbulkbomlist = new List<FGBulkBOMList>();
            try
            {
                fgbulkbomlist = _db.Query<FGBulkBOMList>("USP_RPT_FG_BULK_BOM_REPORT",
                                    new
                                    {
                                        P_ITEMID = ITEMID,
                                        P_BRANCHID = BRANCHID,
                                        P_FROMDATE = FROMDATE,
                                        P_TODATE = TODATE
                                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return fgbulkbomlist;
        }

        public List<MaterialRateChartList> LoadMaterialRateChartReport(string ITEMID, string BRANCHID, string FROMDATE, string TODATE, string FINYEAR)
        {
            List<MaterialRateChartList> materiallist = new List<MaterialRateChartList>();
            try
            {
                materiallist = _db.Query<MaterialRateChartList>("USP_RPT_MATERIAL_RATE_CHART_REPORT",
                                    new
                                    {
                                        P_ITEMID = ITEMID,
                                        P_BRANCHID = BRANCHID,
                                        P_FROMDATE = FROMDATE,
                                        P_TODATE = TODATE,
                                        P_FINYEAR = FINYEAR
                                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return materiallist;
        }

    }
}
