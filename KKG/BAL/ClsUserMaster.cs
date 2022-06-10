using DAL;
using System;
using System.Data;
using System.Web;
using Utility;


namespace BAL
{
    public class ClsUserMaster
    {
        DBUtils db = new DBUtils();

        public int CheckUserName(string UserName)
        {
            int flag = 0;
            try
            {
                string sql = "EXEC spCheckUserNameAvailability '" + UserName + "'";
                flag = (int)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        public DataTable BindUserGrid(string USERTYPE, string DEPOTID, string SearchName)
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_USERMASTER '" + USERTYPE + "','" + DEPOTID + "','" + SearchName + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindType()
        {
            string sql = " SELECT USERTYPEID AS UTID, USERTYPE as UTNAME FROM M_WORKFLOW_SFA WHERE USERTYPEID <>'4D766058-209D-4B90-BDA9-8049E591ABDB' ORDER BY USERTYPE";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSearchDepot()
        {
            string sql = "SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH ORDER BY BRPREFIX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*sp add for edit in  usermaster by arnab*/
        public DataTable BindUserinformation(string USERID)
        {
            string sqlfill = "USP_EDIT_USER_MASTER'" + USERID + "'";

            DataTable dtfill = new DataTable();
            dtfill = db.GetData(sqlfill);
            return dtfill;
        }
        /*sp add for edit in  usermaster*/

        public DataTable BindDepartment()
        {
            string sqlfill = "SELECT DEPTID,DEPTNAME FROM M_DEPARTMENT ORDER BY DEPTNAME";

            DataTable dtfill = new DataTable();
            dtfill = db.GetData(sqlfill);
            return dtfill;
        }



        public DataTable BindUserType(string channel)
        {
            string sqltype = "select UTID,UTNAME from M_USERTYPE WHERE APPLICABLETO='" + channel + "' ORDER BY UTNAME";
            DataTable dttype = new DataTable();
            dttype = db.GetData(sqltype);
            return dttype;
        }

        public DataTable BindUserType()
        {
            string sqltype = "select UTID,UTNAME from M_USERTYPE ORDER BY UTNAME";
            DataTable dttype = new DataTable();
            dttype = db.GetData(sqltype);
            return dttype;
        }


        public DataTable BindParamUserType()
        {
            string sqltype = " SELECT TPU_USERTYPEID AS USERTYPEID,'TPU MAPPING' AS USERTYPENAME,'T' AS TPU FROM [P_APPMASTER] " +
                             " UNION ALL SELECT QC_USERTYPEID AS USERTYPEID,'TPU MAPPING' AS USERTYPENAME,'T' AS TPU FROM [P_APPMASTER] " +
                             " UNION ALL SELECT FACTORY_USERTYPEID AS USERTYPEID,'FACTORY MAPPING' AS USERTYPENAME,'F' AS TPU FROM [P_APPMASTER] " +
                             " UNION ALL SELECT DEPOT_USERTYPEID AS USERTYPEID,'DEPOT MAPPING' AS USERTYPENAME,'D' AS TPU FROM [P_APPMASTER] " +
                             " UNION ALL SELECT EXPORT_USERTYPEID AS USERTYPEID,'EXPORT MAPPING' AS USERTYPENAME,'EXPU' AS TPU FROM [P_APPMASTER] " +
                             " UNION ALL SELECT FACTORY_QC_USERTYPEID AS USERTYPEID,'FACTORY MAPPING' AS USERTYPENAME,'FQA' AS TPU FROM [P_APPMASTER] ";
            DataTable dttype = new DataTable();
            dttype = db.GetData(sqltype);
            return dttype;
        }

        public DataTable BindVendor(string tputag)
        {
            if (tputag == "QC")
            {
                tputag = "T";
            }

            if (tputag == "FQA")
            {
                tputag = "F";
            }

            string sqltype = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEMID=1 AND TAG='" + tputag + "' ORDER BY VENDORNAME";
            DataTable dttype1 = new DataTable();
            dttype1 = db.GetData(sqltype);
            return dttype1;
        }

        public DataTable BindDepot(string depottag)
        {
            string sqltype = "SELECT BRID AS VENDORID,BRNAME AS VENDORNAME FROM M_BRANCH WHERE BRANCHTAG='" + depottag + "' ORDER BY BRNAME";
            DataTable dttype1 = new DataTable();
            dttype1 = db.GetData(sqltype);
            return dttype1;
        }

        public DataTable BindHeadquater()
        {
            string sql = "SELECT HQID,HQNAME FROM M_HEADQUARTER ORDER BY HQNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*ADDED BY SAYAN DEY FOR TSI CODE*/
        public DataTable GenerateuserCode()
        {
            string sql = "EXEC USP_USER_CODE_GENERATION ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string SaveUserMaster(string Code, string UID, string UName, string Pasword, string FName, string MName, string LName, string Email,
                                    string Mobile, string Telephone, string Gender, string Address, string Pin, string UserType, int UserStatus,
                                    string Mode, string departmentid, string departmentname, string reporttoid, string reporttoname,
                                    string applicableto, string reporttoRole, string hqid, string hqname, string DOB, string ANVDATE,
                                    string IP_ADDRESS, string EmployeeCode, string THIRDPARTY_NAME) /* remove tsi type */
        {
            string result = "";
            string sqlstr;
            sqlstr = " EXEC USP_USER_MASTER_INSERT_UPDATE '" + UID + "','" + Code + "','" + UName + "','" + Pasword + "','" + FName + "','" + MName + "','" + LName + "','" + Email + "'," +
                        "'" + Mobile + "','" + Telephone + "','" + Gender + "','" + Address + "','" + Pin + "','" + UserType + "','" + HttpContext.Current.Session["UserID"].ToString() + "'," +
                        "'" + Mode + "'," + UserStatus + ",'" + departmentid + "','" + departmentname + "','" + reporttoid + "','" + reporttoname + "','" + applicableto + "'," +
                        " '" + reporttoRole + "','" + hqid + "','" + hqname + "','" + DOB + "','" + ANVDATE + "','" + IP_ADDRESS + "','" + EmployeeCode + "','" + THIRDPARTY_NAME + "'";  /* remove tsi type */
            result = (string)db.GetSingleValue(sqlstr);

            return result;
        }

        public DataTable FetchTPUMappping(string UID)
        {
            string sql = "SELECT TPUID,USERID FROM M_TPU_USER_MAPPING WHERE  USERID='" + UID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveUserTPUMapping(string tpuid, string UName, string Pasword, string Mode, string UID)
        {
            int result = 0;
            string sqlstr;
            string userid;

            try
            {
                if (Mode == "A")
                {
                    string sql = "SELECT USERID FROM M_USER WHERE USERNAME='" + UName + "' and PASSWORD=dbo.fnMIME64Encode(1,'" + Pasword + "')";
                    DataTable dt = new DataTable();
                    dt = db.GetData(sql);
                    userid = dt.Rows[0]["USERID"].ToString();

                    sqlstr = "insert into M_TPU_USER_MAPPING([TPUID],[USERID])" +
                      " values('" + tpuid + "','" + userid + "')";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "insert into M_TPU_USER_MAPPING([TPUID],[USERID])" +
                      " values('" + tpuid + "','" + UID + "')";
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

        public int DeleteUserMaster(string UID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "[SP_USER_MASTER_DELETE] '" + UID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }


        public int EditUserMaster(int UID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "SELECT [USERNAME],[PASSWORD],[FNAME],[MNAME],[LNAME],[EMAIL],[MOBILE],[TELEPHONE],[GENDER],[ADDRESS],[PIN],[USERTYPE],[ISACTIVE] from M_USER where USERID='" + UID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindState()
        {

            string sql = "select State_ID, State_Name from M_REGION order by State_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistrict(int stateid)
        {
            string sql = "select District_ID, District_Name from M_DISTRICT where State_ID='" + stateid + "'  order by District_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBeatGridMap(int districtid)
        {
            string sql = " SELECT B.CUSTOMERNAME,A.BEAT_ID,A.NAME AS BEAT_NAME FROM M_BEAT A LEFT JOIN M_CUSTOMER B ON A.CBU=B.CUSTOMERID WHERE DISTRICT_ID='" + districtid + "' " +
                         " ORDER BY B.CUSTOMERNAME,A.NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveBeatMap(string ID, string USERID, string USERNAME, int STEATEID, string STATENAME, int DISTRICTID, string DISTRICTNAME, string BEATID, string BEATNAME, string Mode)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC USP_BEAT_TSI_MAPPING '" + BEATID + "','" + BEATNAME + "','" + USERID + "','" + USERNAME + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public void DeleteUSMapbyid(string Userid)
        {
            string sql = "EXEC USP_BEAT_TSI_MAPPING_DETAILS '" + Userid + "','D'";
            db.HandleData(sql);
        }
        public DataTable BindBeatNameGridbyid(string Userid)
        {
            string sql = "EXEC USP_BEAT_TSI_MAPPING_DETAILS '" + Userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStoreLocationInfo(string Userid)
        {
            string sql = "EXEC USP_BIND_STORELOCATION_USERWISE '" + Userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindReporintToRole(string utid)
        {
            //string sqltype = " SELECT '0' AS UTID,'Select Report To Role' AS UTNAME "+
            //                 " UNION"+
            //                 " SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN(select *  from dbo.fnSplit((SELECT PARENTID FROM M_USERTYPE WHERE UTID='" + utid + "'),','))";
            string sqltype = " SELECT UTID,     UTNAME FROM M_USERTYPE WHERE UTID IN(select *  from dbo.fnSplit((SELECT PARENTID FROM M_USERTYPE WHERE UTID='" + utid + "'),','))";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindReportRoleExpmap()
        {

            string sqltype = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE APPLICABLETO='C' ORDER BY UTNAME";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }
        public DataTable BindReporintTo(string utid)
        {
            string sqltype = "SELECT USERID,RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ',''))) AS USERNAME FROM M_USER WHERE  USERTYPE='" + utid + "'";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindReporintTo()
        {
            string sqltype = "SELECT USERID,RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ',''))) AS USERNAME FROM M_USER ORDER BY RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ','')))";

            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindReporintToEdit()
        {
            string sqltype = "SELECT USERID,RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ',''))) AS USERNAME FROM M_USER";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public DataTable BindReporintTo(string utid, string IUerID, string BSID)
        {
            string sqltype = "SELECT USERID,RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ',''))) AS USERNAME FROM M_USER WHERE REFERENCEID IN (SELECT CUSTOMERID FROM M_CUSTOMER WHERE CUSTOMERID IN(SELECT REFERENCEID FROM M_USER WHERE USERTYPE='" + utid + "' " +
                             " AND USERID IN(SELECT USERID FROM M_USER WHERE REPORTINGTOID='" + IUerID + "')) AND BUSINESSSEGMENTID like '%" + BSID + "%')";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }

        public int DeleteUserBYID(string UID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "Delete from M_TPU_USER_MAPPING where USERID= '" + UID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindBrand()
        {
            //string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION ORDER BY DIVNAME ";
            string sql = "select ITID AS DIVID,ITNAME AS DIVNAME from M_ITEMTYPE ORDER BY ITNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveBrandMapping(string TSIID, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_TSI_BRAND_MAP] '" + TSIID + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }
        public DataTable EditBrand(string tsiid)
        {
            string sql = "SELECT TSIID,TSINAME,BRANDID,BRANDNAME FROM M_TSI_BRAND_MAPPING WHERE TSIID='" + tsiid + "' ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindMenuName()
        {

            string sql = "SELECT DISTINCT ID,DESCRIPTION AS PAGENAME FROM TBLMENULIST1 WHERE PageURL IS NOT NULL ORDER BY PAGENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindReporintToRolemap(string utname)
        {

            string sqltype = " SELECT UTID,UTNAME FROM M_USERTYPE WHERE UTID IN(select *  from dbo.fnSplit((SELECT PARENTID FROM M_USERTYPE WHERE UTNAME='" + utname + "'),','))";
            DataTable dtreprtingto = new DataTable();
            dtreprtingto = db.GetData(sqltype);
            return dtreprtingto;
        }


        public int SaveMenuMapping(string USERID, string USERNAME, string FROMDATE, string TODATE, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_USER_EXCEPTION_MAPPING] '" + USERID + "','" + USERNAME + "','" + FROMDATE + "','" + TODATE + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }
        public DataTable exceptionedit(string userid)
        {

            string sqltype = " SELECT USERID, USERNAME, CONVERT(VARCHAR(10),CAST(FROMDATE AS DATE),103) AS FROMDATE," +
                                " CONVERT(VARCHAR(10),CAST(TODATE AS DATE),103) AS TODATE, MENUID, MENUNAME, " +
                            " REPORTINGROLEID, REPOTINGROLENAME, REPORTINGTOID, REPORTINGTONAME " +
                              " FROM M_USER_EXCEPTION_REPORTINGTO_MAPPING WHERE USERID='" + userid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }

        #region BS MAPPING


        public DataTable BindBusinessSegment()
        {
            string sql = "SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT ORDER BY BSNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }


        public int SaveBusinessSegmentMapping(string userid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_USER_BUSINESSSEGMENT_MAPPING] '" + userid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable BindsavedBusinessSegmentbyuserid(string USERID)
        {
            /*string sql = "SELECT USERID,RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ',''))) AS USERNAME,BSID,BSNAME FROM M_USER_BS_MAP WHERE USERID='" + USERID + "'";*/
            string sql = "SELECT USERID,USERNAME,BSID,BSNAME FROM M_USER_BS_MAP WHERE USERID='" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        #endregion

        #region Brand
        public DataTable BindBrandMapping()
        {
            string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION ORDER BY DIVNAME";
            DataTable dt = db.GetData(sql);
            return dt;
        }


        public int SaveUserBrandMapping(string userid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_USER_BRAND_MAPPING] '" + userid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable BindRowdataboundbrandbyuserid(string USERID)
        {
            /*string sql = "SELECT USERID,RTRIM(LTRIM(REPLACE(FNAME + ' ' + MNAME + ' ' + LNAME,'  ',''))) AS USERNAME,BRANDID,BRANDNAME FROM M_USER_BRAND_MAPPING WHERE USERID='" + USERID + "'";*/
            string sql = "SELECT USERID, USERNAME,BRANDID,BRANDNAME FROM M_USER_BRAND_MAPPING WHERE USERID='" + USERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }



        #endregion

        public DataTable BindDepot()
        {

            string sqltype = "SELECT BRID,BRNAME FROM M_BRANCH ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }

        public DataTable BindDistributor(string DEPOTID)
        {
            /*string sqltype = " SELECT A.CUSTOMERID AS CUSTOMERID ,A.CUSTOMERNAME AS CUSTOMERNAME FROM M_CUSTOMER_DEPOT_MAPPING A INNER JOIN "+
                             " M_CUSTOMER B ON A.CUSTOMERID=B.CUSTOMERID "+
                             " WHERE A.DEPOTID='" + DEPOTID + "' ORDER BY CUSTOMERNAME  ";*/

            string sqltype = "EXEC USP_CUSTOMER_SO_TSI_MAPPED '" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }

        //public int SaveDistributor( string USERID, string USERNAME,string DEPOTID ,string DEPOTNAME ,string XML)
        public int SaveDistributor(string USERID, string USERNAME, string XML)
        {
            string strsql = string.Empty;
            int Result = 0;
            // strsql = "EXEC [USP_DISTRIBUTOR_HQ_MAPPING] '" + USERID + "','" + USERNAME + "','"+DEPOTID+"','"+DEPOTNAME+"','"+XML+"'";
            strsql = "EXEC [USP_DISTRIBUTOR_HQ_MAPPING] '" + USERID + "','" + USERNAME + "','" + XML + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable EidtDistributor(string userid)
        {

            string sqltype = "SELECT  CUSTOMERID,CUSTOMERNAME,TSI_ID,TSI_NAME   FROM M_CUSTOMER_TSI_MAPPING WHERE TSI_ID='" + userid + "'  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }

        public DataTable EidtDistributorDepot(string userid)
        {

            string sqltype = "EXEC USP_TSI '" + userid + "'  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }
        public int UpdateLdomUserMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE M_USER SET  LDTOM=GETDATE() WHERE USERID='" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindBeatGridMap(int districtid, string Userid, int stateid)
        {
            string sql = "EXEC USP_BEAT_DISTRIBUTOR_TSI_MAPPING '" + Userid + "','" + districtid + "','" + stateid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string savestorelocation(string USERID, string USERNAME, string storeId, string stroeName,string menuId)
        {
            string strsql = string.Empty;
            string msg = "0";
            strsql = "EXEC [USP_STORE_LOCATION_USER_MAPPING] '" + USERID + "','" + USERNAME + "','" + storeId + "','"+ stroeName + "','"+ menuId + "'";
            msg = (string)db.GetSingleValue(strsql);
            return msg;
        }

        public string InactiveUserStorelocation(string USERID)
        {
            string strsql = string.Empty;
            string msg = "0";
            strsql = "EXEC [USP_INACTIVE_USER_STORE] '" + USERID + "'";
            msg = (string)db.GetSingleValue(strsql);
            return msg;
        }

        public string SaveDistributorNew(string USERID, string USERNAME, string XML)
        {
            string strsql = string.Empty;
            string msg = "0";
            strsql = "EXEC [USP_DISTRIBUTOR_HQ_MAPPING] '" + USERID + "','" + USERNAME + "','" + XML + "'";
            msg = (string)db.GetSingleValue(strsql);
            return msg;
        }

        /*ADDED BY PRITAM BASU FOR HQ BIND DEPOT WISE */
        public DataTable BindHeadquaterUserWise(string userid)
        {
            string sql = " SELECT DISTINCT A.HQNAME,A.HQID FROM  M_HEADQUARTER A INNER JOIN  M_CUSTOMER_DEPOT_MAPPING B " +
                            " ON A.DEPOTID = B.DEPOTID LEFT JOIN M_CUSTOMER_TSI_MAPPING C ON C.CUSTOMERID = B.CUSTOMERID " +
                            " LEFT OUTER JOIN M_USER D ON  D.HQID = A.HQID WHERE D.USERID = '" + userid + "'" +
                            " OR C.TSI_ID = '" + userid + "' ORDER BY A.HQNAME ASC ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //public DataTable BindTsiType()
        //{
        //    string sql = "select 'Non Red Champ' As TSITYPE union all Select 'Red Champ'  As TSITYPE";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}

        public DataTable BindMcREDUser(string DepotID)
        {
            string sqltype = "EXEC USP_McRED_UESD '" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }
        public DataTable userInfoExcelDownload() /*new add for export excel*/
        {
            string sqltype = "EXEC USP_RPT_USER_DETAILS";
            DataTable dt = new DataTable();
            dt = db.GetData(sqltype);
            return dt;
        }

        public int UpdateMcREDstatus(string XML)
        {
            int result = 0;
            string sql = "EXEC [USP_McRED_UESD_UPDATE] '" + XML + "'";
            DataSet ds = new DataSet();
            result = db.HandleData(sql);
            return result;
        }

    }
}
