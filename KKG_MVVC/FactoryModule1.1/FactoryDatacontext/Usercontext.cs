using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
  public  class Usercontext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public users ValidateUser(string userName, string passWord)
        {
            users user = new users();
            try
            {
                user = _db.QueryFirstOrDefault<users>("USP_USERLOGIN_INFO_V2 ", new { P_USERNAME = userName, P_PASSWORD = passWord }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
            }
            return user;
        }
        public List<Finyear> Bindfinyear()
        {
            List<Finyear> finyr = new List<Finyear>();
            try
            {
                finyr = _db.Query<Finyear>("usp_genfinyr", commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return finyr;
        }

    }
}
