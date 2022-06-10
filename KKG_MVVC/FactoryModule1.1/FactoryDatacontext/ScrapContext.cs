using Dapper;
using FactoryModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FactoryDatacontext
{
    public class ScrapContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public List<UserDepartmentList> GetUserDepartment(string FactoryID)
        {
            List<UserDepartmentList> UserDepartment = new List<UserDepartmentList>();
            try
            {
                UserDepartment = _db.Query<UserDepartmentList>("BIND_USERDEPARTMENT_ONLY_FACTORY ", new { P_FACTORYID = FactoryID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return UserDepartment;
        }

        public List<UserList> GetUserDepartmentWise(string UserTypeID)
        {
            List<UserList> UserDtls = new List<UserList>();
            try
            {
                UserDtls = _db.Query<UserList>("BIND_USER_DEPARTMENTWISE ", new { P_USERTYPEID = UserTypeID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return UserDtls;
        }

        public List<ScrapSubItem> GetScrapSubItem(string PrimaryID)
        {
            List<ScrapSubItem> SubitemDtls = new List<ScrapSubItem>();
            try
            {
                SubitemDtls = _db.Query<ScrapSubItem>("BIND_SCRAP_SUBITEM ", new { P_PRIMARYID = PrimaryID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return SubitemDtls;
        }

        public List<ScrapProduct> GetScrapProduct(string SubtypeID, string FactoryID)
        {
            List<ScrapProduct> ProductDtls = new List<ScrapProduct>();
            try
            {
                ProductDtls = _db.Query<ScrapProduct>("BIND_SCRAP_PRODUCT ", new { P_SUBTYPEID = SubtypeID, P_FACTORYID = FactoryID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return ProductDtls;
        }

        public List<ScrapProductUOM> GetScrapProductUOM(string ProductID)
        {
            List<ScrapProductUOM> UomDtls = new List<ScrapProductUOM>();
            try
            {
                UomDtls = _db.Query<ScrapProductUOM>("BIND_SCRAP_PRODUCT_UOM ", new { P_PRODUCTID = ProductID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return UomDtls;
        }

        public DataTable ScrapProductDetails(List<ScrapProductDetails> ScrapProductDetails)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PRIMARYID", typeof(string));
            dt.Columns.Add("PRIMARYNAME", typeof(string));
            dt.Columns.Add("SUBID", typeof(string));
            dt.Columns.Add("SUBITEMNAME", typeof(string));
            dt.Columns.Add("PRODUCTID", typeof(string));
            dt.Columns.Add("PRODUCTNAME", typeof(string));
            dt.Columns.Add("UOMID", typeof(string));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("SCRAPQTY", typeof(decimal));
            dt.Columns.Add("RECVDQTY", typeof(decimal));

            int count = 1;
            foreach (var item in ScrapProductDetails)
            {
                dt.Rows.Add(item.PRIMARYID,
                            item.PRIMARYNAME,
                            item.SUBID,
                            item.SUBITEMNAME,
                            item.PRODUCTID,
                            item.PRODUCTNAME,
                            item.UOMID,
                            item.UOM,
                            item.SCRAPQTY,
                            item.RECVDQTY
                            );
                count++;
            }
            return dt;
        }
        public List<MessageModel> CrudScrap(ScrapModel scrap)
        {
            DataTable dtDetails;
            dtDetails = ScrapProductDetails(scrap.ScrapProductDetails);
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("CRUD_SCRAP_REQUEST",
                                                        new
                                                        {
                                                            P_SCRAPID = scrap.SCRAPID,
                                                            P_MODE = scrap.MODE,
                                                            P_SCRAPDATE = scrap.SCRAPDATE,
                                                            P_USERTYPEID = scrap.USERTYPEID,
                                                            P_USERTYPE = scrap.USERTYPE,
                                                            P_USERID = scrap.USERID,
                                                            P_USERNAME = scrap.USERNAME,
                                                            P_BRANCHID = scrap.BRANCHID,
                                                            P_CREATEDBY = scrap.CREATEDBY,
                                                            P_FINYEAR = scrap.FINYEAR,
                                                            P_REMARKS = scrap.REMARKS,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_SCRAP_PRODUCT_DETAILS")
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
            }
            return response;
        }

        public List<BindScrapRequest> BindScrapRequest(string FromDate, string ToDate, string Checker, string Finyear, string DepotID)
        {
            List<BindScrapRequest> ScrapGrid = new List<BindScrapRequest>();
            try
            {
                ScrapGrid = _db.Query<BindScrapRequest>("BIND_SCRAP_REQUEST",
                    new
                    {
                        P_FROMDT = FromDate,
                        P_TODT = ToDate,
                        P_CHECKER = Checker,
                        P_FINYEAR = Finyear,
                        P_BRANCHID = DepotID
                    }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ScrapGrid;
        }

        public ScrapEdit ScrapEdit(string ScrapID)
        {
            ScrapEdit EditScrap = new ScrapEdit();
            try
            {
                var reader = _db.QueryMultiple("EDIT_SCRAP_REQUEST", new { P_SCRAPID = ScrapID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<ScrapHeaderRequestEdit>().ToList();
                var vdetails = reader.Read<ScrapDetailsRequestEdit>().ToList();
                EditScrap.ScrapHeaderRequestEdit = vheader;
                EditScrap.ScrapDetailsRequestEdit = vdetails;
            }
            catch (Exception ex)
            {

            }
            return EditScrap;
        }

        public List<MessageModel> SaveScrapRecvdData(ScrapModel scrap)
        {
            DataTable dtDetails;
            dtDetails = ScrapProductDetails(scrap.ScrapProductDetails);
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("CRUD_SCRAP_RECVED_REQUEST",
                                                        new
                                                        {
                                                            P_SCRAPID = scrap.SCRAPID,
                                                            P_SCRAPNO = scrap.SCRAPNO,
                                                            TempTableDetails = dtDetails.AsTableValuedParameter("Type_SCRAP_PRODUCT_DETAILS")
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtDetails = null;
            }
            return response;
        }
    }
}
