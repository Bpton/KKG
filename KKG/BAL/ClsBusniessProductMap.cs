using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsBusniessProductMap
    {
        DBUtils db = new DBUtils();

        public DataTable BindPBGrid(string divid,string catid)
        {
            string sql = "SELECT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT where DIVID='" + divid + "' AND CATID='" + catid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public void DeleteBSMapbyid(string id,string BRANDID,string CATID)
        {
            string sql = "DELETE FROM M_PRODUCT_BUSINESSSEGMENT_MAP WHERE BUSNESSSEGMENTID='" + id + "' AND BRANDID='"+BRANDID+"' AND CATID='"+CATID+"'";
            db.HandleData(sql);
            
        }

        public DataTable BindPSNameGridbyid(string id)
        {
            string sql = "SELECT PRODUCTID AS ID ,PRODUCTNAME AS NAME FROM M_PRODUCT_BUSINESSSEGMENT_MAP WHERE BUSNESSSEGMENTID='" + id + "' order by PRODUCTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public int SavePBMap(string BSID, string XML)
        {
            int result = 0;
            string sqlstr;            
            try
            {
                sqlstr = "EXEC SP_BUSINESSSEGMENT_PRODUCT_MAPPING '"+ BSID +"','"+ XML +"'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }

        public DataTable EditProductByID(string id)
        {
            string sql = "EXEC SP_BUSINESSSEGMENT_PRODUCT_EDIT '"+ id +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }
     
    }
}
