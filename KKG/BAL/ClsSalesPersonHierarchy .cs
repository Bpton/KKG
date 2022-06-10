using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;

namespace BAL
{
    public class ClsSalesPersonHierarchy
    {
        DBUtils db = new DBUtils();

        public DataTable BindBusinessSegment()
        {
            string sql = "select BSID,BSNAME from M_BUSINESSSEGMENT WHERE ACTIVE='TRUE' ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindgvSalesPersonHierarchy()
        {
            string sql = "SELECT UTID,UTNAME,'0' AS [LEVEL] from M_USERTYPE where PREDEFINE='N' and DISTRIBUTIONCHANNEL='y' order by UTNAME";
           
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindgvSalesPersonHierarchy( string  Bsid)
        //{
        //    //string sql = "SELECT UTID,UTNAME from M_USERTYPE where PREDEFINE='N' and DISTRIBUTIONCHANNEL='y' order by UTNAME";
        //    string sql = "select A.BSID,B.UTNAME FROM M_USERTYPE_BS_MAP A INNER JOIN M_USERTYPE B ON A.UTID=B.UTID"+
        //                  " where B.PREDEFINE='N' and B.DISTRIBUTIONCHANNEL='y' AND A.BSID='" + Bsid + "' order by B.UTNAME";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}

        public DataTable fetchdatainGrid(string businessid)
        {
            //string sql = "select UTID,UTNAME,LEVEL,USERTYPEID,BUSINESSSEGMENTID from M_SAELSPERSONHIERARCHY SM " +
            //                " LEFT OUTER JOIN M_USERTYPE  MU ON MU.UTID = SM.USERTYPEID WHERE BUSINESSSEGMENTID='" + businessid + "' ";


            string sql = " SELECT A1.UTID,A1.UTNAME,A1.LEVEL,A1.USERTYPEID,A1.BUSINESSSEGMENTID" +
                         " FROM( " +
                         " 		  SELECT UTID,UTNAME,LEVEL,USERTYPEID,BUSINESSSEGMENTID FROM M_SAELSPERSONHIERARCHY SM" +
                         " 		  LEFT OUTER JOIN M_USERTYPE  MU ON MU.UTID = SM.USERTYPEID " +
                         " 		  WHERE BUSINESSSEGMENTID='" + businessid + "'" +
                         " UNION " +
                         " 		  SELECT UTID,UTNAME,'0' AS [LEVEL],UTID,'' AS BUSINESSSEGMENTID FROM M_USERTYPE " +
                         " 		  WHERE PREDEFINE='N' AND DISTRIBUTIONCHANNEL='y' " +
                         " 		  AND UTID NOT IN(  SELECT USERTYPEID FROM M_SAELSPERSONHIERARCHY" +
                         " 						    WHERE BUSINESSSEGMENTID='" + businessid + "')" +
                         " 	  ) A1" +
                         " ORDER BY A1.LEVEL DESC";



           // string sql = "select A.USERTYPEID,A.LEVEL, A.BUSINESSSEGMENTID ,B.UTNAME from M_SAELSPERSONHIERARCHY A inner join M_USERTYPE B on A.USERTYPEID=B.UTID where A.BUSINESSSEGMENTID ='" + businessid + "'";
           
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int DelectSalesPersonsHierarchy(string Id)
        {
            int result = 0;
            string sql = string.Empty;
            try
            {
             
                sql = "Delete from M_SAELSPERSONHIERARCHY where BUSINESSSEGMENTID='" + Id + "'";
                result = db.HandleData(sql);

                sql = "Delete from M_USERTYPE_BS_MAP where BSID='" + Id + "'";
                result = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public int SaveSalesPersonsHierarchy(int Level,string BusinessSegmentid,string Usertypeid,string BusniessSegmentname,string Mode)
        {
            int result = 0;
            string sqlstr;
           
            DataTable dt = new DataTable();
            try
            {
                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_SAELSPERSONHIERARCHY(LEVEL, BUSINESSSEGMENTID, USERTYPEID)" +
                           " values(" + Level + ",'" + BusinessSegmentid + "','" + Usertypeid + "')";
                        result = db.HandleData(sqlstr);

                         sqlstr = "insert into M_USERTYPE_BS_MAP(UTID, BSID, BSNAME)" +
                           " values('" + Usertypeid + "','" + BusinessSegmentid + "','" + BusniessSegmentname + "')";
                        result = db.HandleData(sqlstr);


                    }
                    else
                    {
                        sqlstr = "update M_SAELSPERSONHIERARCHY set LEVEL=" + Level + ",USERTYPEID='" + Usertypeid + "'  where BUSINESSSEGMENTID= '" + BusinessSegmentid + "'";
                        result = db.HandleData(sqlstr);

                        sqlstr = "UPDATE M_USERTYPE_BS_MAP SET UTID='" + Usertypeid + "',BSNAME='" + BusniessSegmentname + "' WHERE BSID= '" + BusinessSegmentid + "' ";
                        result = db.HandleData(sqlstr);

                    }
                }

                else
                {
                    // result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        
        }

        public DataTable BindAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE USERTYPE='4D766058-209D-4B90-BDA9-8049E591ABDB' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindCMD(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERID='3168' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindSaleDirector(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindNSM(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='D3A5BAFF-D5F5-44D7-86AE-5FEE1CAF5CF3' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindZSMEAST(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='FD238D66-FE6E-4244-A280-5AD9CC04C75D' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindZSMWEST(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='2B34E731-9BC4-453E-9492-636CDB6245C2' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindZSMNORTH(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='FE89F22F-CC62-4E7D-B68A-2FE460DC2470' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindZSMSOUTH(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='C92C0829-C288-46D3-B583-D114868BE890' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindRSM(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='A8274DED-8B5B-4A58-9E10-098F3BDF9F25' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindASM(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='B4BA9E16-7C68-42B4-B2F5-AE2DB8AABC86' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindSO(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='9BF42AA9-0734-4A6A-B835-0885FBCF26F5' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindTSI(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='DEB9A51E-8EED-469C-8E5C-D887C0F8C1FB' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindDISTRIBUTOR(string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = "SELECT USERID,FNAME + ' ' + MNAME + ' ' + LNAME + ' (' + UTNAME + ') - ' + ISNULL(HQNAME,'') AS NAME FROM Vw_USERLOGININFO WHERE REPORTINGTOID='" + UserID + "' AND USERTYPE='5E24E686-C9F4-4477-B84A-E4639D025135' ORDER BY NAME";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }


        //public DataTable BindParentTree(string typeid)
        //{

        //    string sql = string.Empty;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //sql = "SELECT usertypeid as [Code],usertype as [grpName] FROM M_WORKFLOW_SFA where usertypeid <> '4D766058-209D-4B90-BDA9-8049E591ABDB' AND LEVEL_UPPER IN(SELECT CAST(LEVEL_UPPER AS INT)+1 FROM M_WORKFLOW_SFA WHERE usertypeid='" + typeid + "') order by grpName";   // WHERE 
        //        sql = "SELECT distinct USERTYPE AS code,B.UTNAME as grpName FROM M_USER A INNER JOIN M_USERTYPE B ON A.USERTYPE=B.UTID WHERE REPORTTOROLEID='" + typeid + "'";
        //        dt = db.GetData(sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogFiles Errlog = new CreateLogFiles();
        //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
        //    }
        //    return dt;

        //}

        //public DataTable BindChildIdTree(string NodeID)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //string Sql = "SELECT TOP 2 USERTYPE AS [Code],FNAME + LNAME AS [grpName] FROM M_USER WHERE USERTYPE='" + NodeID + "' AND USERTYPE <> 'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' order by grpName";
        //        //string Sql = "SELECT TOP 2 usertypeid as [Code],usertype as [grpName] FROM M_WORKFLOW_SFA where usertypeid <> '4D766058-209D-4B90-BDA9-8049E591ABDB' AND LEVEL_UPPER IN(SELECT CAST(LEVEL_UPPER AS INT)+1 FROM M_WORKFLOW_SFA WHERE usertypeid='" + NodeID + "') order by grpName";   // WHERE 
        //        string Sql = " SELECT distinct USERTYPE AS code,B.UTNAME as grpName FROM M_USER A INNER JOIN M_USERTYPE B ON A.USERTYPE=B.UTID WHERE REPORTTOROLEID='" + NodeID + "'";
        //        dt = db.GetData(Sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogFiles Errlog = new CreateLogFiles();
        //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
        //    }
        //    return dt;
        //}

        //public DataTable BindLastChildIdTree(string NodeID)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        //string Sql = " select Code,grpName,typeId from Acc_AccountGroup where parentId ='" + child + "',typeId ='" + TypId + "'";
        //        string Sql = "SELECT top 2  ID AS [Code],FNAME + LNAME AS [grpName] FROM M_USER WHERE usertype='" + NodeID + "' AND USERTYPE <> 'B9343E49-D86B-49EA-9ACA-4F4F7315EC96' order by grpName";
        //        dt = db.GetData(Sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogFiles Errlog = new CreateLogFiles();
        //        Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
        //    }
        //    return dt;
        //}
        
    }
}
