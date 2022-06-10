using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
    public class Claimscontext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public List<Depomodel> BindDepotByUserID(string userid)
        {
            List<Depomodel> Depomodel = new List<Depomodel>();
            try
            {
                Depomodel = _db.Query<Depomodel>("USP_CLAIM_LOAD_DEPOT", new { CURRENT_USERID = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Businesssegment> BindBusinessSegment()
        {
            List<Businesssegment> Businesssegment = new List<Businesssegment>();
            try
            {
                Businesssegment = _db.Query<Businesssegment>("USP_GetBusinesssegmentclaims", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Businesssegment;
        }
        public List<Businesssegment> BindBusinessSegmentByUserID(string userid)
        {
            List<Businesssegment> Businesssegment = new List<Businesssegment>();
            try
            {
                Businesssegment = _db.Query<Businesssegment>("USP_GetBusinesssegmentclaims" , new { userid = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Businesssegment;
        }
        public List<LoadPrincipleGroup> BindPrincipleGroup()
        {
            List<LoadPrincipleGroup> LoadPrincipleGroup = new List<LoadPrincipleGroup>();
            try
            {
                LoadPrincipleGroup = _db.Query<LoadPrincipleGroup>("USP_GetGroup_claims", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return LoadPrincipleGroup;
        }
        public List<LoadPrincipleGroup> BindDropdownPrincipleGroup(string id)
        {
            List<LoadPrincipleGroup> LoadPrincipleGroup = new List<LoadPrincipleGroup>();
            try
            {
                LoadPrincipleGroup = _db.Query<LoadPrincipleGroup>("USP_GetGroup_claims", new { P_businesssegmentid = id }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return LoadPrincipleGroup;
        }
        public List<BindClaimNarration> BindClaimNarration(string CLAIM_TYPE, string USERTAG, string BSID, string DEPOTID, string FinYear)
        {
            List<BindClaimNarration> BindClaimNarration = new List<BindClaimNarration>();
            try
            {
                BindClaimNarration = _db.Query<BindClaimNarration>("USP_GET_NARRATION", new { p_CLAIM_TYPE = CLAIM_TYPE, p_USERTAG = USERTAG, p_BSID = BSID, p_DEPOTID = DEPOTID, p_FINYEAR = FinYear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return BindClaimNarration;
        }
        public List<LoadPrincipleGroup> BindGroupByDistributor(string DistributorID, string BSID)
        {
            List<LoadPrincipleGroup> LoadPrincipleGroup = new List<LoadPrincipleGroup>();
            try
            {
                LoadPrincipleGroup = _db.Query<LoadPrincipleGroup>("USP_GetGroup_claims", new { P_businesssegmentid = BSID, p_distributorid = DistributorID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return LoadPrincipleGroup;
        }
        public List<LoadDistributors> BindDistributorByDepot(string DepotID, string BSID)
        {
            List<LoadDistributors> LoadDistributors = new List<LoadDistributors>();
            try
            {
                LoadDistributors = _db.Query<LoadDistributors>("USP_GetDistributor_claims", new { P_businesssegmentid = BSID, p_depotid = DepotID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return LoadDistributors;
        }
        public List<LoadDistributors> BindDistributorClaim(string DepotID)
        {
            List<LoadDistributors> LoadDistributors = new List<LoadDistributors>();
            try
            {
                LoadDistributors = _db.Query<LoadDistributors>("USP_GetDistributor_claims", new { p_depotid = DepotID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return LoadDistributors;
        }
        public List<claimstatus> BindClaimStatus()
        {
            List<claimstatus> claimstatus = new List<claimstatus>();
            try
            {
                claimstatus = _db.Query<claimstatus>("USP_GetClaimstatus",   commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return claimstatus;
        }
        public List<LoadRetailers> BindRetailer(string DistributorID)
        {
            List<LoadRetailers> LoadDistributors = new List<LoadRetailers>();
            try
            {
                LoadDistributors = _db.Query<LoadRetailers>("USP_GetRetailer_claims", new { p_distributorid = DistributorID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return LoadDistributors;
        }
        public List<BindClaimNarration> BindClaimPeriod(string narrationid)
        {
            List<BindClaimNarration> BindClaimNarration = new List<BindClaimNarration>();
            try
            {
                BindClaimNarration = _db.Query<BindClaimNarration>("USP_Getnarrationperiod_claims", new { p_narrationid = narrationid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return BindClaimNarration;
        }
        public List<messageresponse> Saveqps(qpsmodel trans, string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_QVPS_CLAIM_INSERTv2",
                                                           new
                                                           {
                                                               C_CLAIMID = trans.ClaimID,
                                                               C_FLAG = trans.Mode,
                                                               C_BSID = trans.bsid,
                                                               C_BSNAME = trans.bsname,
                                                               C_GRID = trans.grid,
                                                               C_GRNAME = trans.grname,
                                                               C_DISTID = trans.distid,
                                                               C_DISTNAME = trans.distname,
                                                               C_RETRID = trans.retrid,
                                                               C_RETRNAME = trans.retrname,
                                                               C_TYPEID = trans.TypeID,
                                                               C_CREATEDBY = trans.UserID,
                                                               C_FINYEAR = trans.FinYear,
                                                               C_REMARKS = trans.remarks,
                                                               C_TAG = trans.tag,
                                                               C_AMOUNT = trans.Amount,
                                                               C_DEPOTID = trans.depotid,
                                                               C_DEPOTNAME = trans.depotname,
                                                               C_CLAIMDATE = trans.date,
                                                               C_CL_QVPS_CLAIM_DETAILS = xml,
                                                               C_CL_QVPS_CLAIM_DETAILS_SCHEME ="",
                                                             
                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch(Exception ex)
            {

            }
            return response;
        }
        public List<qpshdr> BindDamageClaim_Filtering(string claimtype, string userid, string depotid, string status, string fromdate, string todate, string party)
        {
            List<qpshdr> qpshdr = new List<qpshdr>();
            try
            {
                qpshdr = _db.Query<qpshdr>("USP_CLAIM_LOAD_DETAILS_FILTERING", new { CLAIM_TYPE_ID = claimtype, CURRENT_USERID = userid, DEPOTID = depotid, STATUS = status, FROMDATE = fromdate, TODATE = todate, PARTYID = party }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return qpshdr;
        }
        public List<qpshdr> BindQVPSClaim(string claimtype, string userid, string FinYear)
        {
            List<qpshdr> qpshdr = new List<qpshdr>();
            try
            {
                qpshdr = _db.Query<qpshdr>("USP_CLAIM_LOAD_DETAILS  ", new { CLAIM_TYPE_ID = claimtype, CURRENT_USERID = userid, P_FINYEAR = FinYear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return qpshdr;
        }

        /*Damage Claim*/
        public List<qpshdr> BindComplainGrid(string claimtype, string userid, string finyear)
        {
            List<qpshdr> qpshdr = new List<qpshdr>();
            try
            {
                qpshdr = _db.Query<qpshdr>("USP_CLAIM_LOAD_DETAILS", new { CLAIM_TYPE_ID = claimtype, CURRENT_USERID = userid, P_FINYEAR = finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return qpshdr;
        }

        public List<CategoryClaim> BindCategory(string mode,string catid,string distributorid)
        {
            List<CategoryClaim> productCategories = new List<CategoryClaim>();
            try
            {
                productCategories = _db.Query<CategoryClaim>("USP_BIND_PRODUCT_CAEGORY_V2", new { P_Mode = mode, p_catid= catid, p_distributorid = distributorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return productCategories;
        }

        public List<ProductClaim> BindProductCatwise(string mode, string catid,string distributorid)
        {
            List<ProductClaim> productCategories = new List<ProductClaim>();
            try
            {
                productCategories = _db.Query<ProductClaim>("USP_BIND_PRODUCT_CAEGORY_V2", new { P_Mode = mode, p_catid = catid, p_distributorid = distributorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return productCategories;
        }
        public List<ProductPacksize> BindPacksize(string mode, string productid, string distributorid)
        {
            List<ProductPacksize> productCategories = new List<ProductPacksize>();
            try
            {
                productCategories = _db.Query<ProductPacksize>("USP_BIND_PRODUCT_CAEGORY_V2", new { P_Mode = mode, p_catid = productid, p_distributorid = distributorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return productCategories;
        }

        public List<ProductBatchno> BindBatchno(string mode, string productid,string distributorid)
        {
            List<ProductBatchno> productCategories = new List<ProductBatchno>();
            try
            {
                productCategories = _db.Query<ProductBatchno>("USP_BIND_PRODUCT_CAEGORY_V2", new { P_Mode = mode, p_catid = productid,p_distributorid= distributorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return productCategories;
        }
        public List<DamageReason> BindReason(string mode, string productid, string distributorid)
        {
            List<DamageReason> productCategories = new List<DamageReason>();
            try
            {
                productCategories = _db.Query<DamageReason>("USP_BIND_PRODUCT_CAEGORY_V2", new { P_Mode = mode, p_catid = productid, p_distributorid = distributorid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return productCategories;
        }

        public List<messageresponse> SaveDamageClaim(qpsmodel trans, string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_DAMAGE_CLAIM_INSERT_V2",
                                                           new
                                                           {
                                                               C_CLAIMID = trans.ClaimID,
                                                               C_FLAG = trans.Mode,
                                                               C_BSID = trans.bsid,
                                                               C_BSNAME = trans.bsname,
                                                               C_GRID = trans.grid,
                                                               C_GRNAME = trans.grname,
                                                               C_DISTID = trans.distid,
                                                               C_DISTNAME = trans.distname,
                                                               C_RETRID = trans.retrid,
                                                               C_RETRNAME = trans.retrname,
                                                               C_TYPEID = trans.TypeID,
                                                               C_CREATEDBY = trans.UserID,
                                                               C_FINYEAR = trans.FinYear,
                                                               C_REMARKS = trans.remarks,
                                                               C_TAG = trans.tag,
                                                               C_CLAIM_AMT = trans.Amount,
                                                               C_DEPOTID = trans.depotid,
                                                               C_DEPOTNAME = trans.depotname,
                                                               C_CLAIMDATE = trans.date,
                                                               C_CL_DAMAGE_CLAIM_DETAILS = xml,

                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }


        public List<messageresponse> DamgeDeleteClaim(string CLAIMID, string Finyear)
        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
               response = _db.Query<messageresponse>("SP_DAMAGECLAIM_DELETE_V2", new
                {
                    @P_CLAIMID = CLAIMID,
                    p_finyear = Finyear,

               }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        /*Display Claim start 13.05.2020*/

        public List<qpshdr> BindDisplayClaim(string claimtype, string userid, string finyear)
        {
            List<qpshdr> qpshdr = new List<qpshdr>();
            try
            {
                qpshdr = _db.Query<qpshdr>("USP_CLAIM_LOAD_DETAILS", new { CLAIM_TYPE_ID = claimtype, CURRENT_USERID = userid, P_FINYEAR = finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return qpshdr;
        }

        public List<messageresponse> SaveDisplayClaim(qpsmodel trans, string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_DISPLAY_CLAIM_INSERT_V2",
                                                           new
                                                           {
                                                               C_CLAIMID = trans.ClaimID,
                                                               C_FLAG = trans.Mode,
                                                               C_BSID = trans.bsid,
                                                               C_BSNAME = trans.bsname,
                                                               C_GRID = trans.grid,
                                                               C_GRNAME = trans.grname,
                                                               C_DISTID = trans.distid,
                                                               C_DISTNAME = trans.distname,
                                                               C_RETRID = trans.retrid,
                                                               C_RETRNAME = trans.retrname,
                                                               C_TYPEID = trans.TypeID,
                                                               C_CREATEDBY = trans.UserID,
                                                               C_FINYEAR = trans.FinYear,
                                                               C_REMARKS = trans.remarks,
                                                               C_AMOUNT = trans.Amount,
                                                               C_DEPOTID = trans.depotid,
                                                               C_DEPOTNAME = trans.depotname,
                                                               C_CLAIMDATE = trans.date,
                                                               C_CL_DISPLAY_CLAIM_DETAILS = xml
                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public List<messageresponse> DisplayDeleteClaim(string CLAIMID, string Finyear)
        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("SP_DISPLAYCLAIM_DELETE_V2", new
                {
                    @P_CLAIMID = CLAIMID,
                    p_finyear = Finyear,

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }


        /*Gift Claim Start*/

        public List<messageresponse> SaveGiftClaim(qpsmodel trans, string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_MARGIN_CLAIM_INSERT_V2",
                                                           new
                                                           {
                                                               C_CLAIMID = trans.ClaimID,
                                                               C_FLAG = trans.Mode,
                                                               C_BSID = trans.bsid,
                                                               C_BSNAME = trans.bsname,
                                                               C_GRID = trans.grid,
                                                               C_GRNAME = trans.grname,
                                                               C_DISTID = trans.distid,
                                                               C_DISTNAME = trans.distname,
                                                               C_TYPEID = trans.TypeID,
                                                               C_CREATEDBY = trans.UserID,
                                                               C_FINYEAR = trans.FinYear,
                                                               C_REMARKS = trans.remarks,
                                                               C_CLAIM_AMT = trans.Amount,
                                                               C_DEPOTID = trans.depotid,
                                                               C_DEPOTNAME = trans.depotname,
                                                               C_CLAIMDATE = trans.date,
                                                               C_CL_MARGIN_CLAIM_DETAILS = xml
                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public List<messageresponse> GiftDeleteClaim(string CLAIMID, string Finyear)
        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("USP_GIFT_CLAIM_DELETE_V2", new
                {
                    @P_CLAIMID = CLAIMID,
                    p_finyear = Finyear,

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }
        /*------------distributor kyc----------*/
        public List<DistributorType> LoadDisttype(string USERID)
        {
            List<DistributorType> DISTRIBUTOtype = new List<DistributorType>();
            try
            {
                DISTRIBUTOtype = _db.Query<DistributorType>("usp_bind_distributor_type", new { p_soid = USERID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOtype;
        }
        public List<DistribuotrInfo> DistributorFromType(string USERID,string TYPE,string BSID)
        {
            List<DistribuotrInfo> DISTRIBUTOtype = new List<DistribuotrInfo>();
            try
            {
                DISTRIBUTOtype = _db.Query<DistribuotrInfo>("USP_SOWISE_PARTY", new { p_soid = USERID, p_usertype= TYPE, @P_mode="", @P_distid="", @P_BSID= BSID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOtype;
        }
        public List<DistribuotrInfo> BindDistributorDepot(string USERID,  string type,string mode, string distribut)
        {
            List<DistribuotrInfo> DISTRIBUTOdepot = new List<DistribuotrInfo>();
            try
            {
                DISTRIBUTOdepot = _db.Query<DistribuotrInfo>("USP_SOWISE_PARTY", new { p_soid = USERID, p_usertype= type, p_mode = mode, p_distid= distribut}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }
        public List<DistribuotrInfo> BindSubDistributor(string USERID, string type, string mode, string distribut)
        {
            List<DistribuotrInfo> DISTRIBUTOdepot = new List<DistribuotrInfo>();
            try
            {
                DISTRIBUTOdepot = _db.Query<DistribuotrInfo>("USP_SOWISE_PARTY", new { p_soid = USERID, p_usertype = type, p_mode = mode, p_distid = distribut }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }
        public List<DistribuotrInfo> BindDistributorstate(string USERID, string type, string mode, string distribut)
        {
            List<DistribuotrInfo> DISTRIBUTOdepot = new List<DistribuotrInfo>();
            try
            {
                DISTRIBUTOdepot = _db.Query<DistribuotrInfo>("USP_SOWISE_PARTY", new { p_soid = USERID, p_usertype= type, P_mode= mode, p_distid= distribut }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }
        public List<DistribuotrInfo> BindDistributoraddress(string USERID, string type, string mode, string distribut)
        {
            List<DistribuotrInfo> DISTRIBUTOdepot = new List<DistribuotrInfo>();
            try
            {
                DISTRIBUTOdepot = _db.Query<DistribuotrInfo>("USP_SOWISE_PARTY", new { p_soid = USERID, p_usertype= type, P_mode= mode, p_distid= distribut }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }
        public List<DistribuotrInfo> loadDistributorKycDetails(string distribut, string type, string mode)
        {
            List<DistribuotrInfo> DISTRIBUTOdepot = new List<DistribuotrInfo>();
            try
            {
                DISTRIBUTOdepot = _db.Query<DistribuotrInfo>("USP_SOWISE_PARTY", new { @P_distid = distribut, @p_usertype = type, @P_mode = mode}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }
         public List<DistributorCompany> LoadcompanyDetails()
        {
            List<DistributorCompany> DISTRIBUTOdepot = new List<DistributorCompany>();
            try
            {
                DISTRIBUTOdepot = _db.Query<DistributorCompany>("usp_bind_company", new {  }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }

        public List<messageresponse> InsertdistribuTorDetails(DistribuotrInfo _detail,string LMBU)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("[USP_SAVETASK_MASTER]",
                                                           new
                                                           {
                                                               P_DISTRIBUTOR_ID=_detail.DISTRIBUTORID,
                                                               P_DISTRIBUTOR_NAME = _detail.DISTRIBUTORNAME,
                                                               P_DISTRIBUTOR_CODE = _detail.DIST_CODE, 
                                                               P_CONTACT_PERSON = _detail.CONTACTPERSON,
                                                               P_CONTACT_PERSON2 = _detail.CONTACTPERSON2,
                                                               P_EMAIL = _detail.EMAILID1,
                                                               P_WHATSAPP_NO= _detail.MOBILE2,
                                                               P_DIST_ADDRESS= _detail.DIST_ADDRESS,
                                                               P_ADDRESS= _detail.ADDRESS2,
                                                               P_CITY =_detail.CITYNAME,
                                                               P_STATE_ID = _detail.STATEID,
                                                               P_STATE_NAME = _detail.STATE_NAME,
                                                               P_OTHERS_COMPANY =_detail.OTHERS_COMPANY,
                                                               P_REMARKS = _detail.REMARKS,
                                                               P_SOID = LMBU,
                                                               P_COMPANY_ID = _detail.OTHERCOMPANYID ,
                                                               P_DOB =_detail.DOB ,
                                                               P_ANVDATE=_detail.ANVDATE ,
                                                               P_Total_Godown_Size = _detail.Total_Godown_Size,
                                                               P_Godown_Size_McNROE =_detail.Godown_Size_McNROE ,
                                                               P_No_Sales_Person = _detail.No_Sales_Person,
                                                               P_No_Delivery_Person = _detail.No_Delivery_Person,
                                                               P_Total_Annual_Turnover_Lakh =_detail.Total_Annual_Turnover_Lakh ,
                                                               P_McNROE_Turnover_Lakh = _detail.McNROE_Turnover_Lakh,



                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }

        public List<DistribuotrInfo> UploadFileInfo(string DISTRIBUTORID, string filename,string fname)
        {
            List<DistribuotrInfo> upload = new List<DistribuotrInfo>();
            try
            {
                upload = _db.Query<DistribuotrInfo>("[USP_UPLOAD_FILE]", new
                {
                    P_DISTBUTOR_ID = DISTRIBUTORID,
                    P_FILE_PATH = filename,
                    
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return upload;
        }

        public List<DistribuotrInfo> BinddistributorKycReport(string soid, string fromDate, string toDate,string MTTYPE, string MTID )
        {
            List<DistribuotrInfo> LedgerReportModel = new List<DistribuotrInfo>();
            try
            {
                LedgerReportModel = _db.Query<DistribuotrInfo>("[USP_PAN_INDIA_KYC_REPORT_v2]", new { P_SOID= soid, P_STARTDATE = fromDate, P_ENDDATE = toDate, P_USERTYPE= MTTYPE, P_BSID=MTID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        /*-----asm------*/
        public List<DistribuotrInfo> BinddistributorKycAsmReport(string soid, string fromDate, string toDate, string MTTYPE, string MTID)
        {
            List<DistribuotrInfo> LedgerReportModel = new List<DistribuotrInfo>();
            try
            {
                LedgerReportModel = _db.Query<DistribuotrInfo>("[USP_PAN_INDIA_KYC_REPORT_asm_v2]", new { p_asmid = soid, P_STARTDATE = fromDate, P_ENDDATE = toDate, P_USERTYPE = MTTYPE, P_BSID = MTID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<DistribuotrInfo> LoadcompanyDetailsForddl(string distribut)
        {
            List<DistribuotrInfo> LedgerReportModel = new List<DistribuotrInfo>();
            try
            {
                LedgerReportModel = _db.Query<DistribuotrInfo>("[USP_BIND_SP_FOR_DDL]", new { @P_DISTRIBUTOR_ID = distribut}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<DistribuotrpanindiaInfo> BinddistributorKycpanReport(string BSID)
        {
            List<DistribuotrpanindiaInfo> LedgerReportModel = new List<DistribuotrpanindiaInfo>();
            try
            {
                LedgerReportModel = _db.Query<DistribuotrpanindiaInfo>("[usp_pan_india_kyc_report]", new { P_BSID = BSID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<DistribuotrpanindiaInfo> LoaddistributorKyc_asm_zsmReport(string USERID, string USERTYPE)
        {
            List<DistribuotrpanindiaInfo> LedgerReportModel = new List<DistribuotrpanindiaInfo>();
            try
            {
                LedgerReportModel = _db.Query<DistribuotrpanindiaInfo>("[USP_PARTY_KYC_ZSM_RSM_WISE_RPT]", new { @P_SMID = USERID, @P_USERTYPE= USERTYPE }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        /*-----------------------------------------------AUTOMAIL---------------------------------------------------------------*/
        public List<CustomerinfoAutomail> Loadcustomertype()
        {
            List<CustomerinfoAutomail> DISTRIBUTOdepot = new List<CustomerinfoAutomail>();
            try
            {
                DISTRIBUTOdepot = _db.Query<CustomerinfoAutomail>("[USP_BIND_USERTYPE_AUTOMAIL]", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOdepot;
        }

        public List<CustomerinfoAutomail> LoadDistributorFromCustomerType( string TYPE)
        {
            List<CustomerinfoAutomail> DISTRIBUTOtype = new List<CustomerinfoAutomail>();
            try
            {
                DISTRIBUTOtype = _db.Query<CustomerinfoAutomail>("[USP_BIND_USERTYPE_CUSTOMER_AUTOMAIL]", new { @p_usertype = TYPE }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return DISTRIBUTOtype;
        }

        public List<messageresponse> InsertAutomailDetails(ServiceproviderDetails _detail)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("[USP_SMS_MAIL_SERVICE_DETAILS]",
                                                           new
                                                           {
                                                               P_MESSEGETYPE = _detail.Messagetype,
                                                               P_SERVICEPROVIDER_NAME = _detail.ServiceProviderName,
                                                               P_SENDERID = _detail.SenderID,
                                                               P_SMS_USERID = _detail.SmsUserid,
                                                               P_SMS_PASSWORD = _detail.Smspwd,
                                                               P_SMTP = _detail.Smtp,
                                                               P_EMAIL_FRM = _detail.Emailfrm,
                                                               P_PORT = _detail.Port,
                                                               P_EMAIL_USERID = _detail.Emailuserid,
                                                               P_EMAIL_PASSWORD = _detail.Emailpwd,


                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }

        public List<ServiceproviderDetails> BindAutomaildetails()
        {
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            try
            {
                LedgerReportModel = _db.Query<ServiceproviderDetails>("[USP_BIND_AUTOMAIL_DETAILS]", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<ServiceproviderDetails> Loadserviceprovider( string mode)
        {
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            try
            {
                LedgerReportModel = _db.Query<ServiceproviderDetails>("[USP_BIND_AUTOMAIL_USERS]", new { @P_MODE=mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<ServiceproviderDetails> Loadservicemodule( string mode)
        {
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            try
            {
                LedgerReportModel = _db.Query<ServiceproviderDetails>("[USP_BIND_AUTOMAIL_USERS]", new { @P_MODE=mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<ServiceproviderDetails> Getpageurl(int moduleid)
        {
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            try
            {
                LedgerReportModel = _db.Query<ServiceproviderDetails>("[USP_BIND_Pageurlbymoduleid]", new { p_moduleid = moduleid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<messageresponse> InsertAutomailSchedulerDetails(ServiceproviderDetails _detail)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("[USP_INSERT_AUTOMAIL_DETAILS]",
                                                           new
                                                           {
                                                               P_Mode = _detail.mode,
                                                               P_MODULEID  = _detail.ID,
                                                               P_ID  = _detail.ID,
                                                               P_USERID  =_detail.USERID ,
                                                               P_ServiceproviderID  = _detail.ServiceproviderID,
                                                               P_MESSEGE  = _detail.MessageContent,
                                                               P_SchedulerType = _detail.SchedulerType,
                                                               P_Schedul_Monthly  =_detail.Monthly ,
                                                               P_Schedul_Daily  = _detail.Daily,
                                                               



                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }
        public List<ServiceproviderDetails> BindautomailGrid(string mode)
        {
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            try
            {
                LedgerReportModel = _db.Query<ServiceproviderDetails>("[USP_BIND_AUTOMAIL_USERS]", new { P_MODE = mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<ServiceproviderDetails> EditTemplate(string TemplateID)
        {
            List<ServiceproviderDetails> LedgerReportModel = new List<ServiceproviderDetails>();
            try
            {
                LedgerReportModel = _db.Query<ServiceproviderDetails>("[usp_fetch_template_details]", new {P_Templateid = TemplateID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }

        public List<AutomailEmailDetails> LoadReportmodulename(string mode)
        {
            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            try
            {
                LedgerReportModel = _db.Query<AutomailEmailDetails>("[USP_BIND_REPORTSUBSCRIBER]", new { P_MODE = mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }

        public List<AutomailEmailDetails> LoadserviceSubscriberEmail(string mode,string repoid)
        {
            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            try
            {
                LedgerReportModel = _db.Query<AutomailEmailDetails>("[USP_BIND_REPORTSUBSCRIBER]", new { P_MODE = mode , P_REPORTID = repoid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<AutomailEmailDetails> LoadserviceSubscriberUsers(string mode)
        {
            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            try
            {
                LedgerReportModel = _db.Query<AutomailEmailDetails>("[USP_BIND_REPORTSUBSCRIBER]", new { P_MODE = mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }

         public List<AutomailEmailDetails> GetuserMail(string userid)
         {
            List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
            try
            {
                LedgerReportModel = _db.Query<AutomailEmailDetails>("[usp_getuser_mail]", new { p_userid = userid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
         }
         //public List<AutomailEmailDetails> UpdateuserMail(string userid)
         //{
         //   List<AutomailEmailDetails> LedgerReportModel = new List<AutomailEmailDetails>();
         //   try
         //   {
         //       LedgerReportModel = _db.Query<AutomailEmailDetails>("[usp_getuser_mail]", new { P_MODE = userid, P_REPORTID=, P_USER_MAIL= }, commandType: CommandType.StoredProcedure).ToList();
         //   }
         //   catch (Exception ex)
         //   {
         //       string message = "alert('" + ex.Message.Replace("'", "") + "')";
         //   }
         //   return LedgerReportModel;
         //}

        public List<messageresponse> UpdateAutomailemailDetails(AutomailEmailDetails _detail)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("[USP_BIND_REPORTSUBSCRIBER]",
                                                           new
                                                           {
                                                               P_Mode = _detail.mode,
                                                               P_REPORTID = _detail.ReportID,
                                                               P_USER_MAIL = _detail.UserId,
                                                               P_SEND_TYPE = _detail.SendType,
                                                               
                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }
        public List<messageresponse> AutomailInsertEmaildetails(AutomailEmailDetails _detail)
        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("[USP_ADD_NEW_REPORTITEM]",
                                                           new
                                                           {
                                                               P_REPORT_NAME  = _detail.ReportName,
                                                               P_REPORT_CODE  = _detail.ReportCode,
                                                               P_STORE_PROCE  = _detail.StoreProcedure,
                                                               P_PARMETARS = _detail.Sp_Parameters,

                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;
        }
        /*--------saleinvoice updater--------*/
        public List<Invoiceupdater> LoadDistDepot(string mode)
        {
            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            try
            {
                LedgerReportModel = _db.Query<Invoiceupdater>("[USP_BIND_SALEINVOICE_DEPOT]", new { @P_MODE =mode}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<Invoiceupdater> LoadDistributorFromDepot(string mode, string type)
        {
            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            try
            {
                LedgerReportModel = _db.Query<Invoiceupdater>("[USP_BIND_SALEINVOICE_DEPOT]", new {@P_MODE =mode, @P_DEPOTID =type}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<Invoiceupdater> LoadSaleinvoicenoFromParty(string mode, string type,string cusid, string startdate, string enddate)
        {
            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            try
            {
                LedgerReportModel = _db.Query<Invoiceupdater>("[USP_BIND_SALEINVOICE_DEPOT]", new {@P_MODE =mode, @P_DEPOTID =type,
                    @p_cusid  = cusid,
                    @p_startdate= startdate,
                    @p_enddate= enddate,
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<Invoiceupdater> LoadTsiFromParty(string mode)
        {
            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            try
            {
                LedgerReportModel = _db.Query<Invoiceupdater>("[USP_BIND_SALEINVOICE_DEPOT]", new {@P_MODE =mode
                   
                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<messageresponse> LoadUpdateSaleinvoiceno(string mode,string saleinvoiceid, string sfid, string tsiname)
        {
            List<messageresponse> LedgerReportModel = new List<messageresponse>();
            try
            {
                LedgerReportModel = _db.Query<messageresponse>("[USP_UPDATE_SALEINVOICE_SFID]", new {
                    @p_mode=mode,
                    @P_SALEINVOICEID = saleinvoiceid,
                    @P_SFID= sfid,
                    @P_SFNAME= tsiname,

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<Invoiceupdater> BindsaleinvoiceGrid(string mode, string saleinvoiceid)
        {
            List<Invoiceupdater> LedgerReportModel = new List<Invoiceupdater>();
            try
            {
                LedgerReportModel = _db.Query<Invoiceupdater>("[USP_UPDATE_SALEINVOICE_SFID]", new {@p_mode= mode,
                    @P_SALEINVOICEID = saleinvoiceid,
                   

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        /*--------------USERMASTERDOC-----------*/
        public List<UsermasterDocument> bindUser(string MODE,string USERID)
        {
            List<UsermasterDocument> LedgerReportModel = new List<UsermasterDocument>();
            try
            {
                LedgerReportModel = _db.Query<UsermasterDocument>("[USP_BIND_USER_DOCUMENT]", new
                {
                    @P_MODE = MODE,
                    @P_USERID= USERID,

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<UsermasterDocument> UploadFileReportInfo(string mode,string USERID, string path, string filename)
        {
            string REMARKS = "";
            string FROMDATE = "";
            string TODATE = "";

            List<UsermasterDocument> upload = new List<UsermasterDocument>();
            try
            {
                upload = _db.Query<UsermasterDocument>("[USP_BIND_USER_DOCUMENT]", new
                {
                    @P_MODE = mode,
                    @P_USERID = USERID,
                    @P_REMARKS= REMARKS,
                    @P_FROMDATE= FROMDATE,
                    @P_TODATE = TODATE,
                    @P_filename = filename,

                }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return upload;
        }
        public List<messageresponse> InsertUserDetails( UsermasterDocument _detail )
        { 

            List<messageresponse> response = new List<messageresponse>();
            try
            {
                response = _db.Query<messageresponse>("[USP_BIND_USER_DOCUMENT]",
                                                           new
                                                           {
                                                               @P_MODE=_detail.MODE,
                                                               @P_USERID = _detail.USERID,
                                                               @P_REMARKS = _detail.REMARKS,
                                                              

                                                           }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return response;

        }
        public List<UsermasterDocument> BindUserDocReport(string MODE, string USERID,string fromDate, string toDate)
        {
           
            string remarks = "";

            List<UsermasterDocument> LedgerReportModel = new List<UsermasterDocument>();
            try
            {
                LedgerReportModel = _db.Query<UsermasterDocument>("[USP_BIND_USER_DOCUMENT]", new { @P_MODE = MODE, @P_USERID= USERID, @P_REMARKS= remarks, @P_FROMDATE = fromDate, @P_TODATE = toDate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }

        public List<UsermasterDocument> filname(string USERID)
        {
            List<UsermasterDocument> LedgerReportModel = new List<UsermasterDocument>();
            try
            {
                LedgerReportModel = _db.Query<UsermasterDocument>("[USP_GET_FILE_BYUSER]", new { @p_userid= USERID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        /*----------------pandoc--------------*/
        public List<UsermasterPanDocument> BindUserJdReport()
        {
            List<UsermasterPanDocument> LedgerReportModel = new List<UsermasterPanDocument>();
            try
            {
                LedgerReportModel = _db.Query<UsermasterPanDocument>("[USP_PAN_INDIA_JD]", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        /*----------------panCOC--------------*/
        public List<UsermasterPanDocument> BindUsercocReport()
        {
            List<UsermasterPanDocument> LedgerReportModel = new List<UsermasterPanDocument>();
            try
            {
                LedgerReportModel = _db.Query<UsermasterPanDocument>("[USP_PAN_INDIA_COC]", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        /*----------------attendence--------------*/
        public List<UserattendenceMaster> Loadusertype()
        {
            List<UserattendenceMaster> LedgerReportModel = new List<UserattendenceMaster>();
            try
            {
                LedgerReportModel = _db.Query<UserattendenceMaster>("[usp_bind_asm_so_tsi]", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<UserattendenceMaster> LoadUsername( string Mode)
        {
            List<UserattendenceMaster> LedgerReportModel = new List<UserattendenceMaster>();
            try
            {
                LedgerReportModel = _db.Query<UserattendenceMaster>("[usp_bind_asm_so_tsi]", new { p_mode= Mode }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
        public List<messageresponse> UpdateAttendence(string userid, string uname, string date,string intime,string outtime)
        {
            List<messageresponse> LedgerReportModel = new List<messageresponse>();
            try
            {
                LedgerReportModel = _db.Query<messageresponse>("[usp_attendence_correction]", new { p_userid = userid, p_username = uname, p_date = date, p_intime= intime, p_outtime= outtime }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                string message = "alert('" + ex.Message.Replace("'", "") + "')";
            }
            return LedgerReportModel;
        }
    }
    
}
