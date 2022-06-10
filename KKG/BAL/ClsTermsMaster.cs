using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsTermsMaster
    {
        DBUtils db = new DBUtils();
        public DataTable BindTermsMaster()
        {

            string sql = " SELECT ID,NAME,MOID,MONAME, DTOC, LMBU, LDTOM, STATUS,"+
                         "  CASE WHEN ISAPPROVED='Y' THEN 'YES' ELSE 'NO' END AS ISAPPROVED,"+
                         "  MAPPEDMENU=STUFF "+
                         "  ( "+
                         "  ( "+
                         "  SELECT DISTINCT ', '+ CAST(g.DESCRIPTION AS VARCHAR(MAX))"+
                         "  FROM tblMenuList1 g INNER JOIN M_TERMS_MENU_MAPPING B ON g.ID=B.MENUID,M_TERMSANDCONDITIONS e "+
                         "  WHERE B.TERMSID=e.ID and e.ID=t1.ID "+
                         "  FOR XMl PATH('')"+
                         "  ),1,1,'' "+
                         "  ) "+
                         "  FROM M_TERMSANDCONDITIONS t1 "+
                         "  GROUP BY ID,NAME,MOID,MONAME, DTOC, LMBU, LDTOM,ISAPPROVED, STATUS ORDER BY NAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;


        }
        public DataTable BindTermsMasterBYID(string ID)
        {

           // string sql = "select ID,NAME,MOID,MONAME, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, ISDELETED,DESCRIPTION from M_TERMSANDCONDITIONS WHERE ID='" + ID + "'";
            string sql = " SELECT A.NAME,A.DESCRIPTION,A.ISAPPROVED,B.MENUID,C.PageName FROM M_TERMSANDCONDITIONS AS A " +
                         " LEFT OUTER JOIN M_TERMS_MENU_MAPPING B ON A.ID = B.TERMSID " +
                         " LEFT OUTER JOIN tblMenuList1 C  ON B.MENUID = C.ID" +
                         " WHERE A.ID='" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindPageName()
        {

            string sql = "SELECT ID,DESCRIPTION as PageName FROM tblMenuList1 WHERE PageURL IS NOT NULL AND TAG='T' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPageNamebyid(string id)
        {

            string sql = "SELECT TERMSID,MENUID FROM M_TERMS_MENU_MAPPING WHERE TERMSID='" + id + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }     
        
        public int SaveTermsMaster(string ID, string Name, string Mode, string Approved,string Description)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_TERMSANDCONDITIONS where NAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    
                    

                }
                else
                {

                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_TERMSANDCONDITIONS where NAME='" + Name + "' and  ID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                   
                }
                if (Approved=="True")
                {
                    Approved = "Y";
                }
                else
                {
                    Approved = "N";
                }

                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_TERMSANDCONDITIONS([ID],[NAME],[DTOC],[CBU],[STATUS],[ISAPPROVED],[ISDELETED],[DESCRIPTION])" +
                           " values(NEWID(),'" + Name + "', GETDATE(),'" + HttpContext.Current.Session["UserID"].ToString() + "','" + Mode + "','" + Approved + "','N','" + Description + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_TERMSANDCONDITIONS set NAME='" + Name + "' ,LDTOM=GETDATE(),[LMBU]='" + HttpContext.Current.Session["UserID"].ToString() + "',STATUS='" + Mode + "',ISAPPROVED='" + Approved + "',DESCRIPTION='" + Description + "' where ID= '" + ID + "'";
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
        public int DeleteTermsMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_TERMSANDCONDITIONS where ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public int DeleteTermsBYID(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_TERMS_MENU_MAPPING WHERE TERMSID = '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public int SaveUsertermsMapping(int menuid, string name,string Mode, string termid)
        {
            int result = 0;
            string sqlstr;
            string tid;

            try
            {
                if (Mode == "A")
                {
                    string sql = "SELECT ID FROM M_TERMSANDCONDITIONS where NAME='" + name + "' ";
                    DataTable dt = new DataTable();
                    dt = db.GetData(sql);
                    tid = dt.Rows[0]["ID"].ToString();

                    sqlstr = "insert into M_TERMS_MENU_MAPPING([TERMSID],[MENUID])" +
                      " values('" + tid + "'," + menuid + ")";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "insert into M_TERMS_MENU_MAPPING([TERMSID],[MENUID])" +
                     " values('" + termid + "'," + menuid + ")";
                    result = db.HandleData(sqlstr);
                }
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

    

