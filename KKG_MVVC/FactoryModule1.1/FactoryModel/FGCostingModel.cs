using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class FGCostingModel
    {
    }

    public class BranchList
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }

    public class DivisionList
    {
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
    }

    public class CategoryList
    {
        public string CATID { get; set; }
        public string CATNAME { get; set; }
    }

    public class FGRateSheetModel
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string UNITVALUE { get; set; }
        public string UOMNAME { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public decimal RMCOST { get; set; }
        public decimal PMCOST { get; set; }
        public decimal CONVERSIONCOST { get; set; }
        public decimal OVERHEADCOST { get; set; }
        public decimal OTHERCOST { get; set; }
        public decimal TOTALCOST { get; set; }
        public int PCS { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        public string USERID { get; set; }
        public string CHECKER { get; set; }
        public string REPORTTYPE { get; set; }
        public List<FGRateSheetMasterList> FGRateSheet { get; set; }
    }

    public class FGRateSheetMasterList
    {
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string UNITVALUE { get; set; }
        public string UOMNAME { get; set; }
        public decimal RMCOST { get; set; }
        public decimal PMCOST { get; set; }
        public decimal CONVERSIONCOST { get; set; }
        public decimal OVERHEADCOST { get; set; }
        public decimal OTHERCOST { get; set; }
        public decimal TOTALCOST { get; set; }
        public int PCS { get; set; }
        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
    }

    public class FGBOMList
    {
        public string SLNO { get; set; }
        public string PROCESSFRAMEWORKNAME { get; set; }
        public string BOMMATERIAL { get; set; }
        public string CATEGORY { get; set; }
        public string SUBCATEGORY { get; set; }
        public string TYPEOFMATERIAL { get; set; }
        public string UNIT { get; set; }
        public string ML { get; set; }
        public decimal DENSITY { get; set; }
        public decimal QTY { get; set; }
        public decimal REFQTY { get; set; }
        public decimal RMCOST { get; set; }
        public decimal PMCOST { get; set; }
        public decimal PMCOST_PC { get; set; }
        public decimal RMPMCOST { get; set; }
    }

    public class FGBulkCostList
    {
        public string SLNO { get; set; }
        public string FGDESCRIPTION { get; set; }
        public string BOMMATERIAL { get; set; }
        public string ML { get; set; }
        public decimal Density { get; set; }
        public decimal RMCost_Kg { get; set; }
        public decimal CostPerLtr { get; set; }
        public decimal BulkCost_Bottle { get; set; }
    }

    public class FGBulkBOMList
    {
        public string SLNO { get; set; }
        public string PROCESSFRAMEWORKNAME { get; set; }
        public string BOMMATERIAL { get; set; }
        public string TYPEOFMATERIAL { get; set; }
        public string UNIT { get; set; }
        public decimal QTY { get; set; }
        public decimal REFQTY { get; set; }
        public decimal Rate { get; set; }
        public decimal Value { get; set; }
        public string CostKg { get; set; }
        public string CostperKG_Wastage_2PERCENT { get; set; }
        public string Density { get; set; }
        public string Volume { get; set; }
        public decimal VV_Ltr { get; set; }
    }

    public class MaterialRateChartList
    {
        public string BOMMATERIAL { get; set; }
        public string TYPEOFMATERIAL { get; set; }
        public string UNIT { get; set; }
        public decimal RATE { get; set; }

        public decimal Jan_Qty { get; set; }
        public decimal Jan_BasicAmt { get; set; }
        public decimal Jan_Rate { get; set; }

        public decimal Feb_Qty { get; set; }
        public decimal Feb_BasicAmt { get; set; }
        public decimal Feb_Rate { get; set; }

        public decimal Mar_Qty { get; set; }
        public decimal Mar_BasicAmt { get; set; }
        public decimal Mar_Rate { get; set; }

        public decimal Apr_Qty { get; set; }
        public decimal Apr_BasicAmt { get; set; }
        public decimal Apr_Rate { get; set; }

        public decimal May_Qty { get; set; }
        public decimal May_BasicAmt { get; set; }
        public decimal May_Rate { get; set; }

        public decimal Jun_Qty { get; set; }
        public decimal Jun_BasicAmt { get; set; }
        public decimal Jun_Rate { get; set; }

        public decimal Jul_Qty { get; set; }
        public decimal Jul_BasicAmt { get; set; }
        public decimal Jul_Rate { get; set; }

        public decimal Aug_Qty { get; set; }
        public decimal Aug_BasicAmt { get; set; }
        public decimal Aug_Rate { get; set; }

        public decimal Sep_Qty { get; set; }
        public decimal Sep_BasicAmt { get; set; }
        public decimal Sep_Rate { get; set; }

        public decimal Oct_Qty { get; set; }
        public decimal Oct_BasicAmt { get; set; }
        public decimal Oct_Rate { get; set; }

        public decimal Nov_Qty { get; set; }
        public decimal Nov_BasicAmt { get; set; }
        public decimal Nov_Rate { get; set; }

        public decimal Dec_Qty { get; set; }
        public decimal Dec_BasicAmt { get; set; }
        public decimal Dec_Rate { get; set; }

        public decimal Total_Qty { get; set; }
        public decimal Total_BasicAmt { get; set; }
        public decimal Total_Rate { get; set; }

        public decimal CurrentRateorAvgRate_Higher { get; set; }
        public decimal Freight { get; set; }
        public decimal Unloading_Other_Charges { get; set; }
        public decimal LandedCost { get; set; }
    }
}
