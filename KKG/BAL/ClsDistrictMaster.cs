using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsDistrictMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindDistrictGrid()
        {
            string sql =" SELECT S.STATE_ID,S.STATE_NAME,D.DISTRICT_ID,D.DISTRICT_NAME FROM M_REGION AS S INNER JOIN M_DISTRICT AS D "+
                        " ON S.STATE_ID=D.STATE_ID ORDER BY S.STATE_NAME";
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

       

        public DataTable Grideditbyid(int districtid)
        {
            string sqlfill = "SELECT State_ID, District_Name FROM [M_DISTRICT] where  District_ID= " + districtid + " ";

            DataTable dtfill = new DataTable();
            dtfill = db.GetData(sqlfill);
            return dtfill;
        }

        public int SaveDistrictMaster(string ID, int StateID, string DistrictName, string Mode)
        {
            int result = 0;
            string sqlstr;
            string strcheck = "";
           
            DataTable dt = new DataTable();

            try
            {
                if (ID == "")
                {

                    strcheck = "select top 1 * from M_DISTRICT where State_ID=" + StateID + " and District_Name='" + DistrictName + "' ";
                    dt = db.GetData(strcheck);
                    if (dt.Rows.Count != 0)
                    {
                        result = 2;
                        return result;
                    }
                }
                else
                {
                    strcheck = "select top 1 * from M_DISTRICT where State_ID=" + StateID + " and District_Name='" + DistrictName + "' and District_ID<> '" + ID + " '";
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
                        sqlstr = "insert into M_DISTRICT(State_ID, District_Name) values(" + StateID + ",'" + DistrictName + "')";
                            
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_DISTRICT set State_ID=" + StateID + ",District_Name='" + DistrictName + "' where District_ID= '" + ID + "'";
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


        public int DeleteDistrictMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "DELETE FROM M_DISTRICT WHERE DISTRICT_ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
    }
}
