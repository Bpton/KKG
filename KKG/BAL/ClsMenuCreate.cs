using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;
using System.Data.SqlClient;

namespace BAL
{
    public class ClsMenuCreate
    {
        DBUtils db = new DBUtils();

        public DataTable BindParentPageName()
        {

            string sql = "SELECT CHILDID,DESCRIPTION AS PAGENAME FROM tblMenuList1 ORDER BY PAGENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindType()
        {

            string sql = "SELECT DISTINCT TAG FROM TBLMENULIST1 ORDER BY TAG";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int CreateMenusave(string id,string pagename,string code,string description,int ParentPageID, string PageUrl,
                                    string tag,string status,string mode,string reason)
        {
            int result = 0;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (id == "")
                {
                    if (pagename != "")
                    {
                        strcheck = "select top 1 * from tblMenuList1 where PageName='" + pagename + "'";
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
                    if (pagename != "")
                    {
                        strcheck = "select top 1 * from tblMenuList1 where PageName='" + pagename + "' and  ID<>'" + id + "'";
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
                    string sql = "SP_MENU_CREATE '" + id + "','" + pagename + "','" + code + "','" + description + "'," + ParentPageID + "," +
                                " '" + PageUrl + "','" + tag + "','" + status + "','" + mode + "','" + reason + "'";
                    result = db.HandleData(sql);
                }
            }

            catch (Exception ex)
            {
                
                string msg = ex.Message;
            }

            return result;
        }

        public DataTable BindGridView()
        {

            string sql = "select a.ID, a.ChildID, a.ParentID, ISNULL(b.DESCRIPTION,'Master') as 'ParentPageName', a.Pagename as 'ChildPageName' , a.PageURL,a.DESCRIPTION from " +
                         "tblMenuList1 a left outer JOIN tblMenuList1 b on a.ParentID= b.ChildID order by a.Pagename ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable gridedit(string id)
        {

            string sql = "select  ID, ParentID, ChildID, PageName, PageURL, ImageUrls, Rank, Tag, ReasonTag, CODE, DESCRIPTION, STATUS  from tblMenuList1 where ID='"+id+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int CreateMenuDelete(string Id)
        {
            int isuccess = 0;

            try
            {
               
                string sql = "delete from tblMenuList1 where ID='" + Id+"'";
                isuccess = db.HandleData(sql);

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return isuccess;
        }
      

    }
}
