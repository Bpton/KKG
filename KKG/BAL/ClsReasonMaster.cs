using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsReasonMaster
    {
        DBUtils db = new DBUtils();      
        public DataTable BindReasonMaster()
        {
            DataTable dt = new DataTable();
            string sql = " SELECT ID,NAME,MOID,MONAME, DTOC, LMBU, LDTOM, STATUS,"+
                         " STORELOCATIONID,STORELOCATIONNAME,CASE WHEN ISAPPROVED='Y' THEN 'YES' ELSE 'NO' END AS ISAPPROVED,DEBITEDTOID,"+
                         " CASE WHEN DEBITEDTONAME='Select' THEN 'Unspecified' ELSE DEBITEDTONAME  END AS DEBITEDTONAME,"+
                         " MAPPEDMENU=STUFF "+
                         "  ( "+
                         "  ( "+
                         "  SELECT DISTINCT ', '+ CAST(g.DESCRIPTION AS VARCHAR(MAX))"+
                         "  FROM tblMenuList1 g INNER JOIN M_REASON_MENU_MAPPING B ON g.ID=B.MENUID,M_REASON e "+
                         "  WHERE B.REASONID=e.ID and e.ID=t1.ID "+
                         "  FOR XMl PATH('')"+
                         "  ),1,1,''"+
                         " ) "+
                         " FROM M_REASON t1 " +
                         "  GROUP BY ID,NAME,MOID,MONAME, DTOC, LMBU, LDTOM, STATUS,STORELOCATIONID,STORELOCATIONNAME,ISAPPROVED,DEBITEDTOID,DEBITEDTONAME ORDER BY NAME";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStoreLocation()
        {

            string sql = " SELECT ID,NAME FROM M_STORELOCATION ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        
        public DataTable BindReasonMasterBYID(string ID)
        {

            string sql = " select ID,NAME,MOID,MONAME, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, ISDELETED,DESCRIPTION,"+
                         " DEBITEDTOID,ISNULL(DEBITEDTONAME,'') AS DEBITEDTONAME,STORELOCATIONID,ISNULL(STORELOCATIONNAME,'') AS STORELOCATIONNAME,STOCKRELATED" +
                         " from M_REASON WHERE ID='" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindPageName()
        {

            string sql = "SELECT ID, DESCRIPTION as PageName FROM tblMenuList1 WHERE PageURL IS NOT NULL AND TAG IN ('T','C')  AND ReasonTag='Y' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPageNamebyid(string id)
        {

            string sql = "SELECT REASONID,MENUID FROM M_REASON_MENU_MAPPING WHERE REASONID='" + id + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }     
        
        public int SaveReasonMaster(string ID, string Name, string Mode, string Approved,string Description,string DebitedToID,string DebitedToName,string StoreLocationID,string StoreLocationName,string StockRelated)
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
                        strcheck = "select top 1 * from M_REASON where NAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_REASON where NAME='" + Name + "' and  ID<>'" + ID + "'";
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
                        sqlstr = " insert into M_REASON([ID],[NAME],[DTOC],[CBU],[STATUS],[ISAPPROVED],[ISDELETED],[DESCRIPTION],DEBITEDTOID,DEBITEDTONAME,STORELOCATIONID,STORELOCATIONNAME,STOCKRELATED)" +
                                 " values(NEWID(),'" + Name + "',  GETDATE(),'" + HttpContext.Current.Session["UserID"].ToString() + "','" + Mode + "',"+
                                    "'" + Approved + "','N','" + Description + "','"+DebitedToID+"','"+DebitedToName+"','"+StoreLocationID+"','"+StoreLocationName+"','"+StockRelated+"')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = " update M_REASON set NAME='" + Name + "' ,LDTOM=GETDATE(),[LMBU]=" + HttpContext.Current.Session["UserID"].ToString() + ",STATUS='" + Mode + "',"+
                                 " ISAPPROVED='" + Approved + "',DESCRIPTION='" + Description + "',"+
                                 " DEBITEDTOID = '" + DebitedToID + "',DEBITEDTONAME='"+DebitedToName+"'," +
                                 " STORELOCATIONID='"+StoreLocationID+"',STORELOCATIONNAME='"+StoreLocationName+"',STOCKRELATED='"+StockRelated+"'"+
                                 " where ID= '" + ID + "'";
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
        public int DeleteReasonMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_REASON where ID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public int DeleteReasonBYID(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_REASON_MENU_MAPPING WHERE REASONID = '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public int SaveUserReasoMapping(int RID, string name,string Mode, string MID)
        {
            int result = 0;
            string sqlstr;
            string userid;

            try
            {
                if (Mode == "A")
                {
                    string sql = "SELECT ID FROM M_REASON where NAME='" + name + "' ";
                    DataTable dt = new DataTable();
                    dt = db.GetData(sql);
                    userid = dt.Rows[0]["ID"].ToString();

                    sqlstr = "insert into M_REASON_MENU_MAPPING([REASONID],[MENUID])" +
                      " values('" + userid + "'," + RID + ")";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "insert into  M_REASON_MENU_MAPPING([REASONID],[MENUID])" +
                     " values('" + MID + "'," + RID + ")";
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

