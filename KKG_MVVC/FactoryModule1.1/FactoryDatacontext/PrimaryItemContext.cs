using Dapper;
using FactoryModel;
using PrimaryItemModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FactoryDatacontext
{
    public class PrimaryItemContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<BindPrimaryItemGrid> BindPrimaryItem()
        {
            List<BindPrimaryItemGrid> PrimaryItemGrid = new List<BindPrimaryItemGrid>();
            try
            {
                PrimaryItemGrid = _db.Query<BindPrimaryItemGrid>("BIND_PRIMARYITEM ", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PrimaryItemGrid;
        }

        public List<MessageModel> PrimaryItemInsertUpdate(CRUDPrimaryItem Primary)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("CRUD_PRIMARYITEM ",
                                                        new
                                                        {
                                                            P_MODE = Primary.MODE,
                                                            P_ID = Primary.ID,
                                                            P_ITEMCODE = Primary.ITEMCODE,
                                                            P_ITEM_NAME = Primary.ITEM_Name,
                                                            P_ITEMDESC = Primary.ITEMDESC,
                                                            P_PREDEFINE = Primary.PREDEFINE,
                                                            P_ACTIVE = Primary.ACTIVE,
                                                            P_ISSERVICE = Primary.ISSERVICE,
                                                            P_ITEMOWNER = Primary.ITEMOWNER
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public PrimaryEditList EditPrimary(string PrimaryID)
        {
            PrimaryEditList Primary = new PrimaryEditList();
            try
            {
                var reader = _db.QueryMultiple("EDIT_PRIMARYITEM", new { P_ID = PrimaryID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<HeaderEditList>().ToList();

                Primary.HeaderEditList = vheader;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return Primary;
        }


        public List<MessageModel> IsExists(string Name, string ID, string PageID)
        {
            List<MessageModel> exists = new List<MessageModel>();
            try
            {
                exists = _db.Query<MessageModel>("USP_COMMON_MASTER_NAME_CHECKING ", new { P_NAME = Name, P_ID = ID, P_PAGEID = PageID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exists;
        }
    }
}
