using DAL;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Utility
{
    public class ClsMenu
    {
        private string _ParentID;
        private string _ChildID;
        private string _PageName;
        private string _PageUrl;
        private string _ImageUrl;
        private string _FolderName;


        //public string FolderName
        //{
        //    get { return _FolderName; }
        //    set { _FolderName = value; }
        //}

        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        public ClsMenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string PageUrl
        {
            get
            {
                return this._PageUrl;
            }
            set
            {
                this._PageUrl = value;
            }
        }

        public string FolderName
        {
            get
            {
                return this._FolderName;
            }
            set
            {
                this._FolderName = value;
            }
        }

        public string ChildID
        {
            get
            {
                return this._ChildID;
            }
            set
            {
                this._ChildID = value;
            }
        }
        public string ParentID
        {
            get
            {
                return this._ParentID;
            }
            set
            {
                this._ParentID = value;
            }
        }
        public string PageName
        {
            get
            {
                return this._PageName;
            }
            set
            {
                this._PageName = value;
            }
        }

        DBUtils db = new DBUtils();
        DataTable dt = new DataTable();

        public ArrayList GetMenu(string parentid, string UType)
        {
            ArrayList objmenu = new ArrayList();
            DBUtils ObjDt = new DBUtils();
            try
            {
                string sql;
                sql = "select * from vw_menu_rights where ParentID='" + parentid + "' and UType='" + UType + "' order by [RANK]";
                dt = ObjDt.GetData(sql);
                if (dt.Rows.Count != 0)
                {
                    for (int i = 0; i <= dt.Rows.Count; i++)
                    {
                        ClsMenu menu = new ClsMenu();
                        menu.ParentID = dt.Rows[i]["ParentID"].ToString();
                        menu.ChildID = dt.Rows[i]["ChildID"].ToString();
                        HttpContext.Current.Session["topicid"] = menu.ChildID;
                        menu.PageName = dt.Rows[i]["PageName"].ToString();
                        menu.PageUrl = dt.Rows[i]["PageUrl"].ToString();
                        menu.ImageUrl = dt.Rows[i]["ImageUrls"].ToString();
                        menu._FolderName = dt.Rows[i]["Foldername"].ToString();
                        objmenu.Add(menu);
                    }
                }
                else
                {
                    ClsMenu menu = new ClsMenu();
                    menu.ParentID = "-1";
                    menu.ChildID = "0";
                    HttpContext.Current.Session["topicid"] = menu.ChildID;
                    menu.PageName = "";
                    menu.PageUrl = "";
                    menu.ImageUrl = "";
                    menu._FolderName = "";
                    objmenu.Add(menu);
                    // return null;
                }
            }
            catch (Exception exp)
            {
                HttpContext.Current.Trace.Warn("Error in GetCustomerByID()", exp.Message, exp);
            }
            finally
            {
                //dr.Close(); //by abhishek  
            }
            return objmenu;
        }

        public void Fil_Dropdownlist(DropDownList vendor)
        {
            try
            {
                string strSql = " select UStatusID,UstatusName from dbo.tblUserStatus where flag='U' and  UStatusID <> 0 and Status<>'D'";
                db.PopulateCombo(db.ExecuteDr(strSql), vendor, "UstatusName", "", "UStatusID", "");
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :public void FetchCircle(DropDownList lstItem)");
            }
        }

        public void Fil_Dropdownlistcorporate(DropDownList vendor)
        {
            try
            {
                string strSql = " select UStatusID,UstatusName from dbo.tblUserStatus where flag='C' and  UStatusID <> 0";
                db.PopulateCombo(db.ExecuteDr(strSql), vendor, "UstatusName", "", "UStatusID", "");
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :public void FetchCircle(DropDownList lstItem)");
            }
        }

        public string Seve_MenuPermission(ArrayList list)
        {
            string b_value = string.Empty;
            try
            {
                b_value = (string)db.ExecuteData(list, "proc_MenuRights");
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return b_value;
        }

        public string Delete_MenuPermission(ArrayList list)
        {
            string b_value = string.Empty;
            try
            {
                b_value = (string)db.ExecuteData(list, "proc_DeleteMenuRights");
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return b_value;
        }

        public bool check_menu(int menuId, int utype)
        {
            bool _bvalue = false;
            DataTable dt = new DataTable();
            dt = db.GetData("select MenuID from dbo.tblMenuRights where MenuID=" + menuId + " and Utype=" + utype);
            if (dt.Rows.Count != 0)
            {
                return _bvalue = true;
            }
            else
            {
                return _bvalue = false;
            }
        }
    }
}
