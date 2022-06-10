using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsCostCategory
    {      
       DBUtils db = new DBUtils();

       public DataTable BindCostCatagoryMastergrid()
       {
           string sql = "SELECT COSTCATID, COSTCATNAME, COSTCATCODE, COSTCATDESRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CASE WHEN ACTIVE='True' then 'Active'  else 'InActive' END AS 'ACTIVE' FROM M_COST_CATEGORY";
           DataTable dt = new DataTable();
           dt = db.GetData(sql);
           return dt;
       }
       public DataTable BindCostCatagoryMastergrideditbyid(string catid)
       {
           string sql = "SELECT COSTCATID, COSTCATNAME, COSTCATCODE, COSTCATDESRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  CASE WHEN ACTIVE='True' then 'Active'  else 'InActive' END AS 'ACTIVE' from M_COST_CATEGORY WHERE COSTCATID='" + catid + "' ";
           DataTable dt = new DataTable();
           dt = db.GetData(sql);
           return dt;
       }

       public int SaveCostCatagoryMaster(string ID, string Code, string Name, string Description,string userid,string Mode, string Active)
       {
           int result = 0;
           string sqlstr;
           string strcheck;
           DataTable dt = new DataTable();
           try
           {
               if (ID == "")
               {
                   if (Code != "")
                   {
                       strcheck = "select top 1 * from M_COST_CATEGORY where COSTCATCODE='" + Code + "'";
                       dt = db.GetData(strcheck);
                       if (dt.Rows.Count != 0)
                       {
                           result = 2;
                           return result;
                       }
                   }
                   if (Name != "")
                   {
                       strcheck = "select top 1 * from M_COST_CATEGORY where COSTCATNAME='" + Name + "'";
                       dt = db.GetData(strcheck);
                       if (dt.Rows.Count != 0)
                       {
                           result = 3;
                           return result;
                       }

                   }

               }
               else
               {

                   if (Code != "")
                   {
                       strcheck = "select top 1 * from M_COST_CATEGORY where COSTCATCODE='" + Code + "' and  COSTCATID<>'" + ID + "'";
                       dt = db.GetData(strcheck);
                       if (dt.Rows.Count != 0)
                       {
                           result = 2;
                           return result;
                       }
                   }
                   if (Name != "")
                   {
                       strcheck = "select top 1 * from M_COST_CATEGORY where COSTCATNAME='" + Name + "' and  COSTCATID<>'" + ID + "'";
                       dt = db.GetData(strcheck);
                       if (dt.Rows.Count != 0)
                       {
                           result = 3;
                           return result;
                       }

                   }
               }

               if (dt.Rows.Count == 0)
               {

                   if (Mode == "A")
                   {
                       sqlstr = "INSERT INTO M_COST_CATEGORY ([COSTCATID],[COSTCATCODE],[COSTCATNAME],[COSTCATDESRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE])" +
                          " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "'," +
                           " '" + userid + "',GETDATE(),'','','" + Mode + "','N','" + Active + "')";
                       result = db.HandleData(sqlstr);
                   }
                   else
                   {
                       sqlstr = " UPDATE M_COST_CATEGORY SET COSTCATCODE='" + Code + "',COSTCATNAME='" + Name + "',COSTCATDESRIPTION='" + Description + "'," +
                                " LMBU='" + userid + "',LDTOM=GETDATE(),STATUS='" + Mode + "'," +
                                " ACTIVE='" + Active + "' WHERE COSTCATID= '" + ID + "'";
                       result = db.HandleData(sqlstr);
                   }
               }

               else
               {                  

               }
           }
           catch (Exception ex)
           {
               CreateLogFiles Errlog = new CreateLogFiles();
               Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
           }

           return result;

       }
       public int DeleteCostCatagoryMaster(string ID)
       {
           int result = 0;
           string sqlstr;
           try
           {

               sqlstr = "DELETE FROM M_COST_CATEGORY WHERE COSTCATID= '" + ID + "'";
               result = db.HandleData(sqlstr);
           }

           catch (Exception ex)
           {
               CreateLogFiles Errlog = new CreateLogFiles();
               Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
           }

           return result;
       }

       public DataTable BindCostCentre()
       {
           string sql = string.Empty;
           sql = "SELECT COSTCENTREID,COSTCENTRENAME FROM M_COST_CENTRE";
           DataTable dt = db.GetData(sql);
           return dt;

       }

       public int SaveCostCategoryMapping(string COSTCATID, string XML)
       {
           string strsql = string.Empty;
           int Result = 0;

           strsql = "EXEC [USP_COST_CENTRE_MAPPING]'" + COSTCATID + "','" + XML + "'";
           Result = db.HandleData(strsql);
           return Result;
       }

       public DataTable EditCostCentre(string COSTCATID)
       {
           string sql = string.Empty;
           sql = "SELECT COSTCATID,COSTCATNAME,COSTCENTREID,COSTCENTRENAME FROM M_COST_CENTER_CATEGORY_MAPPING WHERE COSTCATID='" + COSTCATID + "'  ";
           DataTable dt = db.GetData(sql);
           return dt;

       }

    }
}
