using DAL;
using System;
using System.Data;
using System.Web;
using Utility;
namespace BAL
{
    public class ClsMenuActivity
    {
        DBUtils db = new DBUtils();

        public int CreateMenuInsert(string ParentPageID, int ChildPageID, string Pagename, string PageUrl)
        {
            int iSuccess = 0;

            try
            {

                //string sql = "insert into M_MENUACTIVITY (ParentID , ChildID , PageName , PageURL) values ('" + ParentPageID + "','" + ChildPageID + "','" + Pagename + "','" + PageUrl + "')";
                string sql = "insert into tblMenuList1 (ParentID , ChildID , PageName , PageURL,[Rank]) values ('" + ParentPageID + "','" + ChildPageID + "','" + Pagename + "','" + PageUrl + "',(select MAX([rank]) + 1 from tblMenuList1 where ParentID='" + ParentPageID + "' ))";
                iSuccess = db.HandleData(sql);
            }

            catch (Exception ex)
            {
                //Console.WriteLine("{0} Exception caught.", e);
                string msg = ex.Message;
            }

            return iSuccess;
        }

        public int CreateMenuUpdate(string ParentPageID, int ChildPageID, string Pagename, string PageUrl, int Id)
        {
            int iSuccess = 0;

            try
            {
                //string sql = "update M_MENUACTIVITY set ParentID = '" + ParentPageID + "', ChildID ='" + ChildPageID + "',PageName = '" + Pagename + "',PageURL='" + PageUrl + "' where ID=" + Id;
                string sql = "update tblMenuList1 set ParentID = '" + ParentPageID + "', ChildID ='" + ChildPageID + "',PageName = '" + Pagename + "',PageURL='" + PageUrl + "' where ID=" + Id;
                iSuccess = db.HandleData(sql);
            }

            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return iSuccess;
        }

        public int CreateMenuDelete(string Id)
        {
            int isuccess = 0;

            try
            {
                //string sql = "delete from M_MENUACTIVITY where ID=" + Id;
                string sql = "delete from tblMenuList1 where ID=" + Id;
                isuccess = db.HandleData(sql);

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            return isuccess;
        }

        public DataTable BindParentPageName()
        {
            //string sql = "select ChildID, Pagename from M_MENUACTIVITY order by Pagename";
            string sql = "select ChildID, DESCRIPTION from tblMenuList1 order by DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGridView()
        {
            //string sql = "select a.ID, a.ChildID, a.ParentID, ISNULL(b.Pagename,'Master') as 'ParentPageName', a.Pagename as 'ChildPageName' , a.PageURL from " +
            //             "M_MENUACTIVITY a left outer JOIN M_MENUACTIVITY b on a.ParentID= b.ChildID order by a.ID";
            string sql = "select a.ID, a.ChildID, a.ParentID, ISNULL(b.DESCRIPTION,'Master') as 'ParentPageName', a.DESCRIPTION as 'ChildPageName' , a.PageURL from " +
                         "tblMenuList1 a left outer JOIN tblMenuList1 b on a.ParentID= b.ChildID order by a.ID";

            // string sql = "select * from M_MENUACTIVITY";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SelectMenuRecord(string menuid)
        {
            //string sql = "select a.ID, a.ChildID, a.ParentID, ISNULL(b.Pagename,'Master') as 'ParentPageName', a.Pagename as 'ChildPageName' , a.PageURL from " +
            //             "M_MENUACTIVITY a left outer JOIN M_MENUACTIVITY b on a.ParentID= b.ChildID order by a.ID";
            string sql = "select a.ID, a.ChildID, a.ParentID, ISNULL(b.DESCRIPTION,'Master') as 'ParentPageName', a.DESCRIPTION as 'ChildPageName' , a.PageURL from " +
                         "tblMenuList1 a left outer JOIN tblMenuList1 b on a.ParentID= b.ChildID  where  a.ID='" + menuid + "'";

            // string sql = "select * from M_MENUACTIVITY";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUsertype()
        {
            string sql = "SELECT UTID,UTNAME FROM DBO.M_USERTYPE ORDER BY UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindChildIdTree(string NodeID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = " select ChildID,DESCRIPTION from tblMenuList1 where parentid='" + NodeID + "' order by DESCRIPTION";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return dt;
        }

        public DataTable BindParent()
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT ChildID,DESCRIPTION FROM dbo.tblMenuList1 WHERE ParentID='0' ORDER BY DESCRIPTION";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindParentTree(string typeid)
        {

            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "SELECT ChildID,DESCRIPTION FROM dbo.tblMenuList1 WHERE [parentId]='" + typeid + "' order by DESCRIPTION";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable Populate(string UserTypeID)
        {
            string sql = "SELECT Utype,MenuID FROM dbo.tblMenuRights WHERE Utype='" + UserTypeID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int DeleteUserType(string UserTypeID)
        {
            int result = 0;
            string sql = "DELETE FROM tblMenuRights WHERE Utype='" + UserTypeID + "'";
            result = db.HandleData(sql);
            return result;
        }

        public int InsertMapping(string MenuID, string UserTypeID)
        {
            int result = 0;
            try
            {

                string sql = "INSERT INTO tblMenuRights(Utype,MenuID) VALUES('" + UserTypeID + "','" + MenuID + "')";
                result = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }


            return result;
        }
    }
}
