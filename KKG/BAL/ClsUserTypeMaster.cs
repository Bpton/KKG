using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsUserTypeMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindUserTypeMasterGrid()
        {
            string sql = "select * from M_USERTYPE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveUserTypeMaster(string ID, string Code, string Name, string Description, string Mode)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            try
            {
                if (ID == "")
                {
                    strcheck = "select top 1 * from M_USERTYPE where UTNAME='" + Name + "'";
                }
                else
                {
                    strcheck = "select top 1 * from M_USERTYPE where UTNAME='" + Name + "' and  UTID <>'" + ID + "'";
                }

                DataTable dt = new DataTable();
                dt = db.GetData(strcheck);
                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_USERTYPE([UTID],[UTCODE],[UTNAME],[UTDESCRIPTION],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED])" +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_USERTYPE set UTCODE='" + Code + "',UTNAME='" + Name + "',UTDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "' where UTID= '" + ID + "'";
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
        public int DeleteUserTypeMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_USERTYPE where UTID= '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        #region Role Hierarchy

        public DataTable BindDepartment()
        {
            string sql = "SELECT DEPTID,DEPTNAME FROM M_DEPARTMENT ORDER BY DEPTNAME";
            DataTable dt = db.GetData(sql);

            return dt;

        }

        public DataTable BindRole(string deptid)
        {
            string sql = "SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN " +
                         " (SELECT USERTYPE FROM M_USER WHERE DEPARTMENTID='" + deptid + "')";
            DataTable dt = db.GetData(sql);

            return dt;

        }

        public DataTable BindReportingto(string utid)
        {
            string sql = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN" +
                         " (select *  from dbo.fnSplit((SELECT PARENTID FROM M_USERTYPE WHERE UTID='" + utid + "'),',')) " +
                         " order by UTNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int SaveRoleHeiychy(string DEPARTMENTID, string ROLEID, string REPORTTOROLEID)
        {
            int result = 0;

            string sqlchek = "SELECT TOP 1 * FROM M_ROLE_HEIRARCHY WHERE DEPARTMENTID='" + DEPARTMENTID + "' AND ROLEID='" + ROLEID + "' AND  REPORTTOROLEID='" + REPORTTOROLEID + "'  ";
            DataTable DTCHECK = db.GetData(sqlchek);
            if (DTCHECK.Rows.Count != 0)
            {
                result = 2;

            }
            if (DTCHECK.Rows.Count == 0)
            {
                string sql = "USP_ROLE_HEIRARCHY_INSERT_UPDATE '" + DEPARTMENTID + "','" + ROLEID + "','" + REPORTTOROLEID + "'";
                result = db.HandleData(sql);
            }
            return result;
        }

        public DataTable BindRoleheirchy()
        {
            string sql = " SELECT A.DEPARTMENTID,B.DEPTNAME,A.ROLEID,C.UTNAME AS ROLENAME ,A.REPORTTOROLEID,D.UTNAME AS REPORTINGNAME FROM M_ROLE_HEIRARCHY A INNER JOIN M_DEPARTMENT " +
                         " B ON A.DEPARTMENTID=B.DEPTID" +
                         " INNER JOIN M_USERTYPE C ON	A.ROLEID=C.UTID INNER JOIN  M_USERTYPE D ON A.REPORTTOROLEID=D.UTID";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditRoleheirchy(string DEPARTMENTID, string ROLEID, string REPORTTOROLEID)
        {
            string sql = " SELECT DEPARTMENTID,ROLEID,REPORTTOROLEID FROM M_ROLE_HEIRARCHY WHERE DEPARTMENTID='" + DEPARTMENTID + "' AND ROLEID='" + ROLEID + "' AND " +
                        "  REPORTTOROLEID='" + REPORTTOROLEID + "' ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public int DeleteRoleheirchy(string DEPARTMENTID, string ROLEID, string REPORTTOROLEID)
        {
            int result = 0;
            string sqlDelete = " DELETE FROM M_ROLE_HEIRARCHY WHERE DEPARTMENTID='" + DEPARTMENTID + "' AND ROLEID='" + ROLEID + "' AND " +
                        "  REPORTTOROLEID='" + REPORTTOROLEID + "' ";
            result = db.HandleData(sqlDelete);
            return result;
        }

        #endregion

        public int SaveDepotMapping(string userid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_USER_DEPOT_MAPPING] '" + userid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable Bindsaveddepotbyuserid(string USERID)
        {
            string sql = "SELECT TPUID,USERID FROM M_TPU_USER_MAPPING WHERE USERID='" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public string DepotMappingChking(string userid)
        {
            string Sql = " SP_USER_DEPOT_MAPPING_CHKING '" + userid + "' ";
            return ((string)db.GetSingleValue(Sql));
        }
    }
}
