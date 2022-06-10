using DAL;
using System;
using System.Data;

namespace Utility
{
    public class ClsParam
    {
        private string _GT;
        private string _MT;
        private string _CPC;
        private string _CSD;
        private string _INCS;
        private string _Corporate;
        private string _Ecommerce;
        private string _Paramilitary;
        private string _Export;
        private string _UPGovt;
        private string _Institutional;
        private string _Trading;
        private string _KolkataDepotID;
        private string _CASE;
        private string _PCS;
        private string _DEEVA;
        private string _SEWREEID;
        private string _DISTRIBUTOR;
        private string _TSI;
        private string _SO;
        private string _ASM;
        private string _RSM;
        private string _ZSM;
        private string _NSM;

        public string GT
        {
            get { return _GT; }
        }
        public string MT
        {
            get { return _MT; }
        }
        public string CPC
        {
            get { return _CPC; }
        }
        public string CSD
        {
            get { return _CSD; }
        }
        public string INCS
        {
            get { return _INCS; }
        }
        public string Corporate
        {
            get { return _Corporate; }
        }
        public string Ecommerce
        {
            get { return _Ecommerce; }
        }
        public string Paramilitary
        {
            get { return _Paramilitary; }
        }
        public string Export
        {
            get { return _Export; }
        }
        public string UPGovt
        {
            get { return _UPGovt; }
        }
        public string Institutional
        {
            get { return _Institutional; }
        }
        public string Trading
        {
            get { return _Trading; }
        }
        public string KolkataDepot
        {
            get { return _KolkataDepotID; }
        }
        public string CASE
        {
            get { return _CASE; }
        }
        public string PCS
        {
            get { return _PCS; }
        }
        public string DEEVA
        {
            get { return _DEEVA; }
        }
        public string SEWREE
        {
            get { return _SEWREEID; }
        }
        public string DISTRIBUTOR
        {
            get { return _DISTRIBUTOR; }
        }
        public string TSI
        {
            get { return _TSI; }
        }
        public string SO
        {
            get { return _SO; }
        }
        public string ASM
        {
            get { return _ASM; }
        }
        public string RSM
        {
            get { return _RSM; }
        }
        public string ZSM
        {
            get { return _ZSM; }
        }
        public string NSM
        {
            get { return _NSM; }
        }
        
        public ClsParam()
        {
            //
            // TODO: Add constructor logic here
            //
            DBUtils db = new DBUtils();
            DataSet BSegment = new DataSet();
            try
            {
                string sql;
                sql = " EXEC USP_GETPARAM ";
                BSegment = db.GetDataInDataSet(sql);
                BSegment.Tables[0].TableName = "PARAM";
                BSegment.Tables[1].TableName = "USERTYPE";
                if (BSegment.Tables[0].Rows.Count > 0)
                {

                    _GT = BSegment.Tables["PARAM"].Rows[0]["BS_GT"].ToString().Trim();
                    _MT = BSegment.Tables["PARAM"].Rows[0]["BS_MT"].ToString().Trim();
                    _CPC = BSegment.Tables["PARAM"].Rows[0]["BS_CPC"].ToString().Trim();
                    _CSD = BSegment.Tables["PARAM"].Rows[0]["BS_CSD"].ToString().Trim();
                    _INCS = BSegment.Tables["PARAM"].Rows[0]["BS_INCS"].ToString().Trim();
                    _Corporate = BSegment.Tables["PARAM"].Rows[0]["BS_CORPORATE"].ToString().Trim();
                    _Ecommerce = BSegment.Tables["PARAM"].Rows[0]["BS_ECOMM"].ToString().Trim();
                    _Paramilitary = BSegment.Tables["PARAM"].Rows[0]["BS_EXPARA"].ToString().Trim();
                    _Export = BSegment.Tables["PARAM"].Rows[0]["EXPORTBSID"].ToString().Trim();
                    _UPGovt = BSegment.Tables["PARAM"].Rows[0]["BS_GOVT"].ToString().Trim();
                    _Institutional = BSegment.Tables["PARAM"].Rows[0]["BS_INST"].ToString().Trim();
                    _Trading = BSegment.Tables["PARAM"].Rows[0]["BS_TRADING"].ToString().Trim();
                    _KolkataDepotID = BSegment.Tables["PARAM"].Rows[0]["KOLKATADEPOTID"].ToString().Trim();
                    _CASE = BSegment.Tables["PARAM"].Rows[0]["CASEPACKSIZEID"].ToString().Trim();
                    _PCS = BSegment.Tables["PARAM"].Rows[0]["PACKSIZE"].ToString().Trim();
                    _DEEVA = BSegment.Tables["PARAM"].Rows[0]["DEEVA"].ToString().Trim();
                    _SEWREEID = BSegment.Tables["PARAM"].Rows[0]["DEEVA"].ToString().Trim();
                }
                else
                {
                   
                    _GT = "";
                    _MT = "";
                    _CPC = "";
                    _CSD = "";
                    _INCS = "";
                    _Corporate = "";
                    _Ecommerce = "";
                    _Paramilitary = "";
                    _Export = "";
                    _UPGovt = "";
                    _Institutional = "";
                    _Trading = "";
                    _KolkataDepotID = "";
                    _CASE = "";
                    _PCS = "";
                    _DEEVA = "";
                    _SEWREEID = "";
                }

                if (BSegment.Tables[1].Rows.Count > 0)
                {

                    _DISTRIBUTOR = BSegment.Tables["USERTYPE"].Rows[0]["DISTRIBUTOR"].ToString().Trim();
                    _TSI = BSegment.Tables["USERTYPE"].Rows[0]["TSI"].ToString().Trim();
                    _SO = BSegment.Tables["USERTYPE"].Rows[0]["SO"].ToString().Trim();
                    _ASM = BSegment.Tables["USERTYPE"].Rows[0]["ASM"].ToString().Trim();
                    _RSM = BSegment.Tables["USERTYPE"].Rows[0]["RSM"].ToString().Trim();
                    _ZSM = BSegment.Tables["USERTYPE"].Rows[0]["ZSM"].ToString().Trim();
                    _NSM = BSegment.Tables["USERTYPE"].Rows[0]["NSM"].ToString().Trim();
                    
                }
                else
                {

                    _DISTRIBUTOR = "";
                    _TSI = "";
                    _SO = "";
                    _ASM = "";
                    _RSM = "";
                    _ZSM = "";
                    _NSM = "";
                }
            }
            catch (Exception exp)
            {
                string msg = exp.Message.Replace("'", "");
            }
            finally
            {
                //dr.Close(); 
            }
        }
        
    }
}
