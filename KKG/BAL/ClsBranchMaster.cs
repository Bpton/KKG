using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsBranchMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindBranchMasterGrid()
        {
            //string sql = "select BRID, BRCODE, PARENTID, CHILDID, BRNAME, BRDESCRIPTION, BRPREFIX, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CITYID, STATEID, COUNTRYID, DISTRICTID, BRANCHTAG, ISMOTHERDEPOT, ADDRESS, DELIVERYADDRESS, PHONENO, MOBILENO, PINZIP, DELIVERYPIN, EMAIL, PHONENO1, MOBILENO1 from M_BRANCH";
            string sql = " SELECT BRID, BRCODE, PARENTID, CHILDID, BRNAME, BRDESCRIPTION, BRPREFIX," +
                         " CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, CITYID, STATEID, COUNTRYID, DISTRICTID,BRANCHTAG," +
                         " CASE WHEN BRANCHTAG = 'D' THEN  CASE WHEN ISMOTHERDEPOT = 'True' THEN 'MOTHER DEPOT'" +
                                                         " ELSE 'DEPOT' END" +
                         " ELSE 'OFFICE'   END AS BRANCHTAGDESC," +
                         " ISMOTHERDEPOT, ADDRESS, DELIVERYADDRESS, PHONENO, MOBILENO, PINZIP, DELIVERYPIN," +
                         " EMAIL, PHONENO1, MOBILENO1 FROM M_BRANCH ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBranchMasterById(string id)
        {
            string sql = "select BRID, BRCODE, PARENTID, CHILDID, BRNAME, FULLNAME,BRDESCRIPTION, BRPREFIX, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED," +
                          " CITYID, STATEID, COUNTRYID, DISTRICTID, BRANCHTAG, ISMOTHERDEPOT, ADDRESS, DELIVERYADDRESS, PHONENO, MOBILENO, PINZIP, " +
                          " DELIVERYPIN, EMAIL, PHONENO1, MOBILENO1,ACCGRPNAME,ACCCODE,TIN,CIN,CST,ISNULL(ISUSEDFOREXPORT,'N') AS ISUSEDFOREXPORT, " +
                          " ISNULL(ISEXPORTTRANSFERTOHO,'N') AS ISEXPORTTRANSFERTOHO from M_BRANCH where BRID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string CheckHeadOffice()
        {

            string Sql = " IF  EXISTS(SELECT BRID FROM [M_BRANCH] WHERE BRANCHTAG='O' AND ISMOTHERDEPOT = 'True')BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public int SaveBranchMaster(string ID, int pid, int cid, string Code, string Name, string Description, string Prefix, string Mode, string Tag,
                                    string ismotherdepot, string address, string deliveryaddress, string phone, string mobile, int stateid, int districtid,
                                    int cityid, string pinzip, string deliverypin, string email, string phone1, string mobile1, string fullname, string ACCCODE,
                                    string ACCGRPNAME, string TIN, string CIN, string CST, string ISUSEDFOREXPORT, string ISEXPORTTRANSFERTOHO)
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
                        strcheck = "select top 1 * from M_BRANCH where BRCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_BRANCH where BRNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_BRANCH where BRCODE='" + Code + "' and BRID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_BRANCH where BRNAME='" + Name + "' and BRID <>'" + ID + "'";
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
                        sqlstr = "insert into M_BRANCH([BRID],[BRCODE],[PARENTID],[CHILDID],[BRNAME],[BRDESCRIPTION],[BRPREFIX],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS]," +
                                 " [ISAPPROVED],[BRANCHTAG],[ISMOTHERDEPOT],[ADDRESS],[DELIVERYADDRESS],[PHONENO],[MOBILENO],[STATEID],[DISTRICTID],[CITYID],[PINZIP], " +
                                  " [DELIVERYPIN],[EMAIL],[PHONENO1],[MOBILENO1],[FULLNAME],ACCGRPNAME,ACCCODE,TIN,CIN,CST,ISUSEDFOREXPORT,ISEXPORTTRANSFERTOHO)" +
                           " values(NEWID(),'" + Code + "', '" + pid + "','" + cid + "','" + Name + "','" + Description + "','" + Prefix + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + Tag + "','" + ismotherdepot + "'," +
                            " '" + address + "','" + deliveryaddress + "','" + phone + "','" + mobile + "','" + stateid + "','" + districtid + "','" + cityid + "'," +
                            " '" + pinzip + "','" + deliverypin + "','" + email + "','" + phone1 + "','" + mobile1 + "','" + fullname + "','" + ACCGRPNAME + "', " +
                            " '" + ACCCODE + "','" + TIN + "','" + CIN + "','" + CST + "','" + ISUSEDFOREXPORT + "','" + ISEXPORTTRANSFERTOHO + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_BRANCH set BRCODE='" + Code + "',BRNAME='" + Name + "',BRDESCRIPTION='" + Description + "',BRPREFIX='" + Prefix + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',STATUS='" + Mode + "'," +
                                 " BRANCHTAG='" + Tag + "',ISMOTHERDEPOT='" + ismotherdepot + "',ADDRESS='" + address + "',DELIVERYADDRESS='" + deliveryaddress + "'," +
                                 " PHONENO='" + phone + "',MOBILENO='" + mobile + "',STATEID='" + stateid + "',DISTRICTID='" + districtid + "'," +
                                 " CITYID='" + cityid + "',PINZIP='" + pinzip + "',DELIVERYPIN='" + deliverypin + "',EMAIL='" + email + "'," +
                                 " PHONENO1='" + phone1 + "',MOBILENO1='" + mobile1 + "',FULLNAME='" + fullname + "',ACCGRPNAME='" + ACCGRPNAME + "'," +
                                 " ACCCODE='" + ACCCODE + "',TIN='" + TIN + "',CIN='" + CIN + "',CST='" + CST + "' ,ISUSEDFOREXPORT='" + ISUSEDFOREXPORT + "' ," +
                                 " ISEXPORTTRANSFERTOHO='" + ISEXPORTTRANSFERTOHO + "',LDTOM=GETDATE() where BRID= '" + ID + "'";
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
        public int DeleteBranchMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC [SP_SALESLOCATION_DELETE] '" + ID + "'";
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
            string sql = "select District_ID, District_Name from M_DISTRICT where State_ID='" + stateid + "' order by District_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindParentBranch(string brtag)
        {
            string sql = "select [PARENTID], [BRNAME] from [M_BRANCH] where [BRANCHTAG]='" + brtag + "' order by PARENTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCity(int distid)
        {
            string sql = "select City_ID, City_Name from M_CITY where District_ID='" + distid + "' order by City_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCity(int stateid, int districtid, string citynm)
        {

            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {

                strcheck = "select top 1 * from M_CITY where City_Name='" + citynm + "' and District_ID='" + districtid + "'";
                dt = db.GetData(strcheck);
                if (dt.Rows.Count == 0)
                {
                    sqlstr = "INSERT INTO [dbo].[M_CITY]([State_ID] ,[District_ID] ,[City_Name]) VALUES('" + stateid + "','" + districtid + "','" + citynm + "')";
                    result = db.HandleData(sqlstr);
                    return result;
                }
                else
                {

                    return result;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindProductMapping(string taxId, string Vid)
        {
            string sql = " select PRODUCT_NAME as Name ,Product_id as Id,percentage,Taxid from [M_TAX_PRODUCT_MAPPING] " +
                         " where taxid='" + taxId + "' and VENDORID='" + Vid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAccountgrp()
        {

            string sql = "SELECT Code,grpName FROM Acc_AccountGroup order by grpName";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGridBranch()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " select [BRID] as REGIONID,[BRNAME] AS REGIONNAME from [M_BRANCH] order by BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string taxid, string mode)
        {
            int flag = 0;
            string sql = string.Empty;
            //string mode = string.Empty;
            try
            {
                if (accid == "")
                {
                    mode = "A";
                }
                else
                {
                    mode = "U";
                }
                sql = "exec [SP_ACC_LEDGER_INSERT] '" + Name + "','" + AccGroupCode + "','" + Finyear + "','" + mode + "','" + accid + "','" + xml + "','" + taxid + "','T'";
                flag = (int)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }
        public string Bindbranchid(string NAME)
        {
            string sql = "SELECT BRID FROM M_BRANCH where BRNAME='" + NAME + "'";
            string id = string.Empty;

            id = (string)db.GetSingleValue(sql);
            return id;
        }

        //==============ADD BY SUBHODIP DE ON 16.09.2016=========//
        public DataTable Bindcutomergrid(int STATEID, string DISTRICTID)
        {

            string sql = "select CUSTOMERID,CUSTYPE_ID,CUSTYPE_NAME,CUSTOMERNAME from M_CUSTOMER where STATEID= " + STATEID + " AND DISTRICTID in (" + DISTRICTID + ") " +
                        " ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindcutomergridCOUNTRY(string COUNTRYID)
        {

            string sql = "select CUSTOMERID,CUSTYPE_ID,CUSTYPE_NAME,CUSTOMERNAME from M_CUSTOMER where COUNTRYID= '" + COUNTRYID + "' ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveDepotCustMapping(string depotid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_DEPOT_CUSTOMER_MAPPING] '" + depotid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable Bindsaveddepotbycustid(string depotid)
        {
            string sql = "SELECT DEPOTID,DEPOTNAME,CUSTOMERTYPE , CUSTOMERID, [CUSTOMERNAME] FROM M_CUSTOMER_DEPOT_MAPPING WHERE DEPOTID='" + depotid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BINDCUSTOMER() // Don't Change because this is used by Auto Posting Leadger creation in account table
        {
            string sql = "SELECT [CUSTOMERTYPE], [CUSTOMERID], [CUSTOMERNAME] FROM M_CUSTOMER_DEPOT_MAPPING  ORDER BY CUSTOMERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //==============================//

        public DataTable BindOrderType()
        {
            string sql = "select OrderTYPEID,ORDERTYPENAME from M_ORDERTYPE order by ORDERTYPENAME desc ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCountry()
        {
            string sql = " SELECT COUNTRYID,COUNTRYNAME FROM M_COUNTRY" +
                         " WHERE COUNTRYID NOT IN (SELECT COUNTRYID FROM P_APPMASTER)" +
                         " ORDER BY COUNTRYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveDepotTransporterMapping(string depotid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [SP_DEPOT_TRANSPORTER_MAPPING] '" + depotid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable EditTransporter(string ID)
        {
            string sql = " SELECT ISNULL(TRANSPOTERID,'') AS TRANSPOTERID,ISNULL(TRANSPOTERNAME,'') AS TRANSPOTERNAME,ISNULL(TAG,'') AS TAG," +
                         " ISNULL(DEPOTID,'') AS DEPOTID,ISNULL(DEPOTNAME,'') AS DEPOTNAME FROM M_TRANSPOTER_DEPOT_MAP WHERE DEPOTID = '" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransporter()
        {
            string sql;

            sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER ORDER BY NAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStateTransporter(string stateid)
        {
            string sql;
            sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER WHERE STATEID IN('" + stateid + "') ORDER BY NAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTpuDepotFac(string moduleid)
        {
            string sql = "EXEC [USP_BIND_TPU_DEPOT_FACTORY] '" + moduleid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindDeliverySehedule(string id)
        {
            string sql = " EXEC [USP_DELIVERY_SEHEDULE_ADD] '" + id + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }

        public DataTable SaveDeliverySehedule(string ModuleId, string TpuDepotID, string Xml)
        {
            DataTable result = new DataTable();
            string sql = "EXEC [USP_DELIVERY_SEHEDULE_INSERT_UPDATE] '" + ModuleId + "','" + TpuDepotID + "','" + Xml + "'";
            result = db.GetData(sql);
            return result;
        }

        public DataTable LoadGridviewDelivarySehedule()
        {
            string sql = "EXEC [USP_GRIDVIEW_DELIVARY_SEHEDULE]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataSet CheckEntry(string Moduleid, string depotid)
        {
            string sql = " EXEC [USP_CHECK_DELIVERY_ENTRY] '" + Moduleid + "','" + depotid + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
    }
}
