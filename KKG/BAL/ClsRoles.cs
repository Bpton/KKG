using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsRoles
    {
        DBUtils db = new DBUtils();

        public DataTable BindUserTypeMasterGrid()
        {
            string sql = "select UTID, UTCODE, UTNAME, UTDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',PARENTNAME,PREDEFINE,DISTRIBUTIONCHANNEL from M_USERTYPE ORDER BY UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindUserTypeMasterGridByID(string ID)
        {
            string sql = "select UTID, UTCODE, UTNAME, UTDESCRIPTION, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',PARENTID,PARENTNAME,PREDEFINE,APPLICABLETO,DISTRIBUTIONCHANNEL  from M_USERTYPE where  UTID='" + ID + "' ORDER BY UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindReportto()
        {
            string sql = "select UTID, UTNAME from M_USERTYPE ORDER BY UTNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable Bindroles()
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE ORDER BY UTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveUserTypeMaster(string ID, string Code, string Name, string Description, string Mode, string active, string parentid, string parentname, string ApplicableTOpre, string ApplicableTOdis, string Applicableto, string similarrole)
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
                        strcheck = "select top 1 * from M_USERTYPE where UTCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_USERTYPE where UTNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_USERTYPE where UTCODE='" + Code + "' and  UTID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_USERTYPE where UTNAME='" + Name + "' and  UTID<>'" + ID + "'";
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
                        string guid = Guid.NewGuid().ToString().Trim().ToUpper();
                        sqlstr = "insert into M_USERTYPE([UTID],[UTCODE],[UTNAME],[UTDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE],[PARENTID],[PARENTNAME],[APPLICABLETO],[PREDEFINE],[DISTRIBUTIONCHANNEL] )" +
                           " values('" + guid + "','" + Code + "', '" + Name + "','" + Description + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + active + "','" + parentid + "','" + parentname + "','" + Applicableto + "','" + ApplicableTOpre + "','" + ApplicableTOdis + "')";
                        result = db.HandleData(sqlstr);
                        string QUERY = "USP_SIMILAR_ROLE '" + guid + "','" + similarrole + "','A'";
                        db.HandleData(QUERY);
                    }
                    else
                    {
                        sqlstr = "update M_USERTYPE set UTCODE='" + Code + "',UTNAME='" + Name + "',UTDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "',PARENTID='" + parentid + "',PARENTNAME='" + parentname + "',PREDEFINE='" + ApplicableTOpre + "',DISTRIBUTIONCHANNEL='" + ApplicableTOdis + "',APPLICABLETO='" + Applicableto + "' where UTID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                        string QUERY = "UPDATE M_USER SET REPORTTOROLEID = (SELECT PARENTID FROM M_USERTYPE WHERE UTID='" + ID + "') where USERTYPE = '" + ID + "' ";
                        db.HandleData(QUERY);
                    }
                }
                else
                {
                    //result = 0;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public int DeleteUserTypeMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "Delete from M_USERTYPE where UTID= '" + ID + "' AND PREDEFINE='N'";
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
