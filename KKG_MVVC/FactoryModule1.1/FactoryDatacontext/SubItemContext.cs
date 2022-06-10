using Dapper;
using FactoryModel;
using SubItemModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace FactoryDatacontext
{
    public class SubItemContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<BindSubItemGrid> BindSubItem()
        {
            List<BindSubItemGrid> SubItemGrid = new List<BindSubItemGrid>();
            try
            {
                SubItemGrid = _db.Query<BindSubItemGrid>("USP_BIND_SUBITEMTYPE ", new { P_SUBTYPEID = "" }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SubItemGrid;
        }

        public List<MessageModel> SubItemInsertUpdate(CRUDSubItem Subitem)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("CRUD_SUBITEMTYPE ",
                                                        new
                                                        {
                                                            P_SUBID = Subitem.SUBTYPEID,
                                                            P_PRIMARYID = Subitem.PRIMARYITEMTYPEID,
                                                            P_CODE = Subitem.SUBITEMCODE,
                                                            P_NAME = Subitem.SUBITEMNAME,
                                                            P_DESCRIPTION = Subitem.SUBITEMDESC,
                                                            P_MODE = Subitem.MODE,
                                                            P_ACTIVE = Subitem.ACTIVE,
                                                            P_HSN = Subitem.HSE,
                                                            P_ITEMOWNER = Subitem.ITEMOWNER,
                                                            P_BRANCHID = Subitem.BRID
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
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

        public SubItemEditList EditSubItem(string SubItemID)
        {
            SubItemEditList SubItem = new SubItemEditList();
            try
            {
                var reader = _db.QueryMultiple("USP_BIND_SUBITEMTYPE", new { P_SUBTYPEID = SubItemID }, commandType: CommandType.StoredProcedure);
                var vheader = reader.Read<EditList>().ToList();

                SubItem.EditList = vheader;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return SubItem;
        }
    }
}
