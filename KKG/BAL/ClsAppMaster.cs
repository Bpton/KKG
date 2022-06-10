using DAL;
using System.Data;

namespace BAL
{
    public class ClsAppMaster
    {
        DBUtils db = new DBUtils();
        public DataTable BindPackSize()
        {
            string sql = "SELECT UOMID,UOMNAME FROM M_UOM ORDER BY UOMNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFinyear()
        {
            string sql = "SELECT ID,FINYEAR FROM M_FINYEAR ORDER BY FINYEAR";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTpu()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='T'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindFactory()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='F'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindQC()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='QC'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepot()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='D'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCombocat()
        {
            string sql = "SELECT CATID,CATNAME FROM M_CATEGORY ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistributor()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='DI'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindStockist()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='ST'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindRetailer()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='RT'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSale()
        {
            string sql = "SELECT DEPTID,DEPTNAME FROM M_DEPARTMENT ORDER BY DEPTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindExcisedCST()
        {
            string sql = "SELECT ID,NAME FROM M_TAX ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLeadger()
        {
            string sql = "SELECT ID,NAME FROM ACC_ACCOUNTSINFO ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTSI()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTCODE='TSI'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindLastLevelDistributionChannel()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE order by UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable AppEdit()
        {
            string sql = " SELECT PACKSIZE, PACKSIZENAME, SALESFORECAST_PREFIX, SALES_FORECAST_SUFFIX, PRODUCTION_PREFIX, PRODUCTION_SUFFIX, "+
                         "INSTRUCTION_PREFIX, INSTRUCTION_SUFFIX, QUALITYCONTROL_PREFIX, QUALITYCONTROL_SUFFIX, PURCHASEORDER_SUFFIX, "+
                         "STOCKTRANSFER_PREFIX, STOCKTRANSFER_SUFFIX, DESPATCH_PREFIX, DESPATCH_SUFFIX, STOCKADJUSTMENT_PREFIX," +
                         "STOCKADJUSTMENT_SUFFIX, DEPORECEIVED_PREFIX, DEPORECEIVED_SUFFIX, STOCKRECEIVED_PREFIX, STOCKRECEIVED_SUFFIX," +
                         "FINYEAR, INDENT_PREFIX, INDENT_SUFFIX, TPU_USERTYPEID, FACTORY_USERTYPEID, QC_USERTYPEID, DEPOT_USERTYPEID,"+
                         "COMBO_DIVISION, COMBO_CATEGORY, BS_GT, BS_MT, BS_CPC, BS_CSD, SALEORDER_SUFFIX, RETAILER_SALEORDER_SUFFIX, "+
                         " DISTRIBUTORFORECAST_PREFIX, DISTRIBUTORFORECAST_SUFFIX, SUPERDISTRIBUTORID, DISTRIBUTORID, STOCKISTSID, "+
                         " SUPERSTOCKISTSID, RETAILERID, DIS_CLM_PREFIX, DIS_CLM_SUFFIX, SALEINVOICE_SUFFIX, SALESID, QV_CLM_PREFIX," +
                         " QV_CLM_SUFFIX, EXCISEID, CST, TRANS_CLM_PREFIX, TRANS_CLM_SUFFIX, CLAIM_DAMAGE_PERCENTAGE, DAMAGE_CLM_PREFIX, "+
                         " DAMAGE_CLM_SUFFIX, PURCHASE_ACC_LEADGER, ROUNDOFF_ACC_LEADGER, BRKG_DAMG_SHRTG_ACC_LEADGER," +
                         " INSURANCECLAIM_ACC_LEADGER, STKRECEIPT_ACC_LEADGER, STKTRANSFER_ACC_LEADGER, SALE_ACC_LEADGER," +
                         " DISCOUNT_ACC_LEADGER, SERVICETAX_ACC_LEDGER, TDS_ACC_LEADGER, SERVICETAX_PAYABLE_ACC_LEADGER," +
                         " CARRIAGE_INWARDS_ACC_LEADGER, CARRIAGE_OUTWARDS_ACC_LEADGER1, SSM_CLM_PREFIX, SSM_CLM_SUFFIX,"+
                         "  MARGIN_CLM_PREFIX, MARGIN_CLM_SUFFIX, INSURANCECLAIMPREFIX, INSURANCECLAIMSUFFIX, "+
                         "  STOCKADJUSTMENT_PREFIX_DMS, STOCKADJUSTMENT_SUFFIX_DMS, TSI_TYPEID, LASTLEVEL_DISTRIBUION_CHANNEL FROM P_APPMASTER_TEST";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveAppMasterOtherdtails(string PACKSIZE, string PACKSIZENAME, string FINYEAR,                                 
                                 string TPU_USERTYPEID,string FACTORY_USERTYPEID,string QC_USERTYPEID,
                                 string DEPOT_USERTYPEID, string COMBO_DIVISION, string COMBO_CATEGORY,
                                 string BS_GT,string BS_MT,string BS_CPC,
                                 string BS_CSD,string SUPERDISTRIBUTORID, string DISTRIBUTORID,
                                 string STOCKISTSID,string SUPERSTOCKISTSID,string RETAILERID,
                                 string SALESID,string EXCISEID,string CST,
                                 string PURCHASE_ACC_LEADGER,string ROUNDOFF_ACC_LEADGER,string BRKG_DAMG_SHRTG_ACC_LEADGER,
                                 string INSURANCECLAIM_ACC_LEADGER, string STKRECEIPT_ACC_LEADGER, string STKTRANSFER_ACC_LEADGER,
                                 string SALE_ACC_LEADGER,string DISCOUNT_ACC_LEADGER,string SERVICETAX_ACC_LEDGER,
                                 string TDS_ACC_LEADGER, string SERVICETAX_PAYABLE_ACC_LEADGER,string CARRIAGE_INWARDS_ACC_LEADGER,
                                 string CARRIAGE_OUTWARDS_ACC_LEADGER1,string TSI_TYPEID,string LASTLEVEL_DISTRIBUION_CHANNEL)
        {
            int result = 0;
            string sql = "update P_APPMASTER_TEST set PACKSIZE='" + PACKSIZE + "', PACKSIZENAME='" + PACKSIZENAME + "'," +
                        
                        " FINYEAR='" + FINYEAR + "', " +
                        " TPU_USERTYPEID='" + TPU_USERTYPEID + "', FACTORY_USERTYPEID='" + FACTORY_USERTYPEID + "', QC_USERTYPEID='" + QC_USERTYPEID + "'," +
                        " DEPOT_USERTYPEID='" + DEPOT_USERTYPEID + "', COMBO_DIVISION='" + COMBO_DIVISION + "', COMBO_CATEGORY='" + COMBO_CATEGORY + "', BS_GT='" + BS_GT + "'," +
                        " BS_MT='" + BS_MT + "', BS_CPC='" + BS_CPC + "',BS_CSD='" + BS_CSD + "', " +
                        
                        " SUPERDISTRIBUTORID='" + SUPERDISTRIBUTORID + "', DISTRIBUTORID='" + DISTRIBUTORID + "', STOCKISTSID='" + STOCKISTSID + "', SUPERSTOCKISTSID='" + SUPERSTOCKISTSID + "'," +
                        " RETAILERID='" + RETAILERID + "', " +
                        " SALESID='" + SALESID + "'," +
                        " EXCISEID='" + EXCISEID + "', CST='" + CST + "'," +
                        
                        " PURCHASE_ACC_LEADGER='" + PURCHASE_ACC_LEADGER + "', ROUNDOFF_ACC_LEADGER='" + ROUNDOFF_ACC_LEADGER + "', BRKG_DAMG_SHRTG_ACC_LEADGER='" + BRKG_DAMG_SHRTG_ACC_LEADGER + "'," +
                        " INSURANCECLAIM_ACC_LEADGER='" + INSURANCECLAIM_ACC_LEADGER + "', STKRECEIPT_ACC_LEADGER='" + STKRECEIPT_ACC_LEADGER + "', STKTRANSFER_ACC_LEADGER='" + STKTRANSFER_ACC_LEADGER + "'," +
                        " SALE_ACC_LEADGER='" + SALE_ACC_LEADGER + "', DISCOUNT_ACC_LEADGER='" + DISCOUNT_ACC_LEADGER + "', SERVICETAX_ACC_LEDGER='" + SERVICETAX_ACC_LEDGER + "', TDS_ACC_LEADGER='" + TDS_ACC_LEADGER + "', " +
                        " SERVICETAX_PAYABLE_ACC_LEADGER='" + SERVICETAX_PAYABLE_ACC_LEADGER + "', CARRIAGE_INWARDS_ACC_LEADGER='" + CARRIAGE_INWARDS_ACC_LEADGER + "', "+
                        " CARRIAGE_OUTWARDS_ACC_LEADGER1='" + CARRIAGE_OUTWARDS_ACC_LEADGER1 + "'," +
                        
                       
                        " TSI_TYPEID='" + TSI_TYPEID + "', LASTLEVEL_DISTRIBUION_CHANNEL='" + LASTLEVEL_DISTRIBUION_CHANNEL + "'";
            result = db.HandleData(sql);
            return result;
        }

        public int SaveAppMasterNoSeries( string SALESFORECAST_PREFIX, string SALES_FORECAST_SUFFIX, string PRODUCTION_PREFIX,
                                  string PRODUCTION_SUFFIX, string INSTRUCTION_PREFIX, string INSTRUCTION_SUFFIX, 
                                  string QUALITYCONTROL_PREFIX,string QUALITYCONTROL_SUFFIX, string PURCHASEORDER_SUFFIX,
                                  string STOCKTRANSFER_PREFIX, string STOCKTRANSFER_SUFFIX, string DESPATCH_PREFIX,
                                  string DESPATCH_SUFFIX, string STOCKADJUSTMENT_PREFIX, string STOCKADJUSTMENT_SUFFIX,
                                  string DEPORECEIVED_PREFIX, string DEPORECEIVED_SUFFIX, string STOCKRECEIVED_PREFIX,
                                  string STOCKRECEIVED_SUFFIX, string INDENT_PREFIX, string INDENT_SUFFIX,
                                  string SALEORDER_SUFFIX, string RETAILER_SALEORDER_SUFFIX, string DISTRIBUTORFORECAST_PREFIX,
                                  string DISTRIBUTORFORECAST_SUFFIX,string DIS_CLM_PREFIX,string DIS_CLM_SUFFIX,
                                  string SALEINVOICE_SUFFIX, string QV_CLM_PREFIX, string QV_CLM_SUFFIX, 
                                  string TRANS_CLM_PREFIX, string TRANS_CLM_SUFFIX, decimal CLAIM_DAMAGE_PERCENTAGE,
                                  string DAMAGE_CLM_PREFIX, string DAMAGE_CLM_SUFFIX,string SSM_CLM_PREFIX,
                                  string SSM_CLM_SUFFIX, string MARGIN_CLM_PREFIX, string MARGIN_CLM_SUFFIX,
                                  string INSURANCECLAIMPREFIX, string INSURANCECLAIMSUFFIX, string STOCKADJUSTMENT_PREFIX_DMS,
                                  string STOCKADJUSTMENT_SUFFIX_DMS)
        {
            int result = 0;
            string sql = "update P_APPMASTER_TEST set  SALESFORECAST_PREFIX='" + SALESFORECAST_PREFIX + "', " +
                        " SALES_FORECAST_SUFFIX='" + SALES_FORECAST_SUFFIX + "'," +
                        " PRODUCTION_PREFIX='" + PRODUCTION_PREFIX + "', PRODUCTION_SUFFIX='" + PRODUCTION_SUFFIX + "', INSTRUCTION_PREFIX='" + INSTRUCTION_PREFIX + "'," +
                        "  INSTRUCTION_SUFFIX='" + INSTRUCTION_SUFFIX + "'," +
                        " QUALITYCONTROL_PREFIX='" + QUALITYCONTROL_PREFIX + "',QUALITYCONTROL_SUFFIX='" + QUALITYCONTROL_SUFFIX + "', PURCHASEORDER_SUFFIX='" + PURCHASEORDER_SUFFIX + "'," +
                        " STOCKTRANSFER_PREFIX='" + STOCKTRANSFER_PREFIX + "', STOCKTRANSFER_SUFFIX='" + STOCKTRANSFER_SUFFIX + "',DESPATCH_PREFIX='" + DESPATCH_PREFIX + "'," +
                        " DESPATCH_SUFFIX='" + DESPATCH_SUFFIX + "', STOCKADJUSTMENT_PREFIX='" + STOCKADJUSTMENT_PREFIX + "', STOCKADJUSTMENT_SUFFIX='" + STOCKADJUSTMENT_SUFFIX + "', " +
                        " DEPORECEIVED_PREFIX='" + DEPORECEIVED_PREFIX + "', DEPORECEIVED_SUFFIX='" + DEPORECEIVED_SUFFIX + "', STOCKRECEIVED_PREFIX='" + STOCKRECEIVED_PREFIX + "', " +
                        " STOCKRECEIVED_SUFFIX='" + STOCKRECEIVED_SUFFIX + "', INDENT_PREFIX='" + INDENT_PREFIX + "', INDENT_SUFFIX='" + INDENT_SUFFIX + "'," +
                        " SALEORDER_SUFFIX='" + SALEORDER_SUFFIX + "'," +
                        " RETAILER_SALEORDER_SUFFIX='" + RETAILER_SALEORDER_SUFFIX + "', DISTRIBUTORFORECAST_PREFIX='" + DISTRIBUTORFORECAST_PREFIX + "'," +
                        " DISTRIBUTORFORECAST_SUFFIX='" + DISTRIBUTORFORECAST_SUFFIX + "', " +
                       
                        " DIS_CLM_PREFIX='" + DIS_CLM_PREFIX + "', DIS_CLM_SUFFIX='" + DIS_CLM_SUFFIX + "'," +
                        " SALEINVOICE_SUFFIX='" + SALEINVOICE_SUFFIX + "', QV_CLM_PREFIX='" + QV_CLM_PREFIX + "', QV_CLM_SUFFIX='" + QV_CLM_SUFFIX + "'," +
                        "  TRANS_CLM_PREFIX='" + TRANS_CLM_PREFIX + "', TRANS_CLM_SUFFIX='" + TRANS_CLM_SUFFIX + "'," +
                        " CLAIM_DAMAGE_PERCENTAGE='" + CLAIM_DAMAGE_PERCENTAGE + "', DAMAGE_CLM_PREFIX='" + DAMAGE_CLM_PREFIX + "', DAMAGE_CLM_SUFFIX='" + DAMAGE_CLM_SUFFIX + "'," +
                      
                        " SSM_CLM_PREFIX='" + SSM_CLM_PREFIX + "', SSM_CLM_SUFFIX='" + SSM_CLM_SUFFIX + "', MARGIN_CLM_PREFIX='" + MARGIN_CLM_PREFIX + "', " +
                        " MARGIN_CLM_SUFFIX='" + MARGIN_CLM_SUFFIX + "', " +
                        " INSURANCECLAIMPREFIX='" + INSURANCECLAIMPREFIX + "', INSURANCECLAIMSUFFIX='" + INSURANCECLAIMSUFFIX + "', STOCKADJUSTMENT_PREFIX_DMS='" + STOCKADJUSTMENT_PREFIX_DMS + "'," +
                        " STOCKADJUSTMENT_SUFFIX_DMS='" + STOCKADJUSTMENT_SUFFIX_DMS + "'";
            result = db.HandleData(sql);
            return result;
        }
        
    }
}
