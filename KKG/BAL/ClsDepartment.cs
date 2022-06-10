using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsDepartment
    {
        DBUtils db = new DBUtils();

        public DataTable Bindgrid()
        {
            string str = " SELECT DEPTID, DEPTCODE, DEPTNAME, DEPTDESCRIPTION,ISAPPROVE  FROM M_DEPARTMENT ORDER BY DEPTNAME";
            DataTable dt = db.GetData(str);
            return dt;
        }

        public DataTable BindgridEdit(string DEPTID  )
        {
            string str = " SELECT DEPTID, DEPTCODE, DEPTNAME, DEPTDESCRIPTION,  CBU, STATUS,ISAPPROVE FROM M_DEPARTMENT WHERE DEPTID='" + DEPTID + "'  ";
            DataTable dt = db.GetData(str);
            return dt;
        }



        public int Savedepartment(string DEPTID, string DEPTCODE, string DEPTNAME, string DEPTDESCRIPTION, string CBU, string STATUS, string ISAPPROVE)
        {
            int result = 0;
            try
            {
                DataTable dt = new DataTable();
               
                string strinsert=string.Empty;
                if (DEPTID == "")
                {
                    if (DEPTCODE != "")
                    {
                        string checkcode = "SELECT * FROM M_DEPARTMENT WHERE DEPTCODE='" + DEPTCODE + "'";
                        dt = db.GetData(checkcode);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (DEPTNAME != "")
                    {
                        string checkcode = "SELECT * FROM M_DEPARTMENT WHERE DEPTNAME='" + DEPTNAME + "'";
                        dt = db.GetData(checkcode);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }


                }
                else
                {
                    if (DEPTCODE != "")
                    {
                        string checkcode = "SELECT * FROM M_DEPARTMENT WHERE DEPTCODE='" + DEPTCODE + "' AND DEPTID<>'"+DEPTID+"' ";
                         dt = db.GetData(checkcode);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (DEPTNAME != "")
                    {
                        string checkcode = "SELECT * FROM M_DEPARTMENT WHERE DEPTNAME='" + DEPTNAME + "' AND DEPTID<>'" + DEPTID + "'";
                         dt = db.GetData(checkcode);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    if (STATUS == "A")
                    {
                        strinsert = " INSERT INTO M_DEPARTMENT(DEPTID, DEPTCODE, DEPTNAME, DEPTDESCRIPTION, COD,  CBU, STATUS,ISAPPROVE) VALUES " +
                                         " ( NEWID(),'" + DEPTCODE + "','" + DEPTNAME + "','" + DEPTDESCRIPTION + "',GETDATE(),'" + CBU + "','" + STATUS + "','" + ISAPPROVE + "')";
                        result = db.HandleData(strinsert);
                    }
                    else
                    {
                        strinsert = "UPDATE M_DEPARTMENT SET DEPTCODE='" + DEPTCODE + "',DEPTNAME='" + DEPTNAME + "',DEPTDESCRIPTION='" + DEPTDESCRIPTION + "', " +
                                  " MOD=GETDATE(),CBU='" + CBU + "',STATUS='" + STATUS + "',ISAPPROVE='" + ISAPPROVE + "' WHERE DEPTID='" + DEPTID + "' ";
                        result = db.HandleData(strinsert);
                    }
                }
              
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int DeleteDepartmentMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                //sqlstr = "Delete from M_DEPARTMENT where DEPTID= '" + ID + "'";
                sqlstr = "Delete from M_DEPARTMENT where DEPTID= '" + ID + "' and DEPTID not in(select DEPTID from M_Budget_Applicable_Department) ";
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
