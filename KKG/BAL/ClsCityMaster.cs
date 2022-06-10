using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsCityMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindCityGrid()
        {
            string sql = "select a.State_ID,b.State_Name,a.District_ID,c.District_Name,a.City_ID,a.City_Name from M_CITY a " +
                         "inner join M_REGION b on a.State_ID=b.State_ID inner join M_DISTRICT c on a.District_ID=c.District_ID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCityGrid(int stateid, int distid)
        {
            string sql = " SELECT a.State_ID,b.State_Name,a.District_ID,c.District_Name,a.City_ID,a.City_Name FROM M_CITY a " +
                         " INNER JOIN M_REGION b on a.State_ID=b.State_ID INNER JOIN M_DISTRICT c ON a.District_ID=c.District_ID WHERE a.State_ID=" + stateid + " AND a.District_ID=" + distid + " ORDER BY City_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSateGrid(int stateid)
        {
            string sql = " SELECT a.State_ID,b.State_Name,a.District_ID,c.District_Name,a.City_ID,a.City_Name FROM M_CITY a " +
                         " INNER JOIN M_REGION b on a.State_ID=b.State_ID INNER JOIN M_DISTRICT c ON a.District_ID=c.District_ID WHERE a.State_ID=" + stateid + "  ORDER BY City_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindState()
        {
            string sql = "select State_ID, State_Name from M_REGION order by State_ID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDistrict(int stateid)
        {
            string sql = "select District_ID, District_Name from M_DISTRICT where State_ID='" + stateid + "' order by District_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Bindcityinformation(int cityid)
        {
            string sqlfill = "SELECT [State_ID],[District_ID],[City_Name] FROM [M_City] where City_ID=" + cityid + "";

            DataTable dtfill = new DataTable();
            dtfill = db.GetData(sqlfill);
            return dtfill;
        }

        public int SaveCityMaster(string ID, int StateID, int DistrictID, string CityName, string Mode)
        {
            int result = 0;
            string sqlstr;
            string strcheck = "";

            DataTable dt = new DataTable();

            try
            {
                if (ID == "")
                {

                    strcheck = "select top 1 * from M_CITY where State_ID=" + StateID + " and District_ID=" + DistrictID + " and City_Name='" + CityName + "'";
                    dt = db.GetData(strcheck);
                    if (dt.Rows.Count != 0)
                    {
                        result = 2;
                        return result;
                    }
                }
                else
                {
                    strcheck = "select top 1 * from M_CITY where State_ID=" + StateID + " and District_ID=" + DistrictID + " and City_Name='" + CityName + "' and City_ID<> '" + ID + "' ";
                    dt = db.GetData(strcheck);
                    if (dt.Rows.Count != 0)
                    {
                        result = 2;
                        return result;
                    }
                }


                //dt = db.GetData(strcheck);
                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_CITY([State_ID],[District_ID],[City_Name],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED])" +
                           " values(" + StateID + "," + DistrictID + ",'" + CityName + "'," +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_CITY set State_ID=" + StateID + ",District_ID=" + DistrictID + ",City_Name='" + CityName + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "' where City_ID= " + ID + "";
                        result = db.HandleData(sqlstr);
                    }
                }

                else
                {
                    result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }


        public int DeleteCityMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_CITY where City_ID= " + ID + "";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        /*storelocation*/
        public DataTable savestorelocation(string Mode,string storelocation)
        {
            string sql = "exec USP_SAVE_STORELOCATION '"+ Mode + "','"+ storelocation + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable loadstorelocation(string Mode)
        {
            string sql = "exec USP_SAVE_STORELOCATION '" + Mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable fetchProductFromStoreLocation(string primaryItem,string subItemType,string storeId)
        {
            string sql = "exec USP_FETCH_STOREWISE_PRODUCT '"+ primaryItem + "','"+ subItemType + "','" + storeId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable InsertUpdateStoreWiseProduct(string storeId,string StoreName,string xml,string userid,string finyear)
        {
            string sql = "exec USP_INSERT_UPDATE_STOREWISE_PRODUCT '" + storeId + "','" + StoreName + "','" + xml + "','" + userid + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable fetchProductStoreWise(string storeId)
        {
            string sql = "exec FETCH_PRODUCT_STORE_WISE '" + storeId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable loadSubItem(string primaryItem,string mode)
        {
            string sql = "exec get_sub_item '" + primaryItem + "','"+mode+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveData(DataTable dtdetails)
        {
            string sql = "exec usp_save_data '" + dtdetails + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
