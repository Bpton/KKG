using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsTPUVendor
    {
        DBUtils db = new DBUtils();

        public DataTable BindTPUGrid()
        {
            string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,T.VENDORID, T.CODE, VENDORNAME," +
                         " CASE WHEN VENDOROWNER='R' THEN 'KKG' WHEN VENDOROWNER='M' THEN 'KKG' ELSE 'KKG' END AS VENDOROWNER," +
                         " SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID," +
                         " CASE WHEN CITYNAME='Select' THEN '' ELSE CITYNAME END AS CITYNAME, R.State_Name AS STATENAME," +
                         " CASE WHEN DISTRICTNAME='Select' THEN '' ELSE DISTRICTNAME END AS DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1," +
                         " CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO," +
                         " VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID," +
                         " CASE WHEN  BANKNAME='Select' THEN '' ELSE BANKNAME END AS BANKNAME, BRANCHID," +
                         " CASE WHEN BRANCHNAME='Select' THEN '' ELSE BRANCHNAME END AS BRANCHNAME,IFSCCODE, T.CBU, T.DTOC, T.LMBU, T.LDTOM, STATUS,ISTDSDECLARE,ISDELETED," +
                         " TAG,GSTNO,ISNULL(ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO,GRPNAME AS ACCGRPNAME," +
                         " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                         "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                         "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                         " ELSE  'Active' END AS ACTIVE_STATUS , " +
                         " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY " +
                         " FROM [M_TPU_VENDOR] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID" +
                         " LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON T.LEDGER_REFERENCEID=C.ID " +
                         " LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE" +
                         " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPUGridByType(string ID, string AccountsGroup)
        {
            DataTable dt = new DataTable();
            if (ID == "0" && AccountsGroup == "0")
            {
                string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,VENDORID,T.CODE, VENDORNAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, R.State_Name AS STATENAME, " +
                             " DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1, CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID,GSTNO, " +
                             " BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU, T.DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE,ISDELETED,TAG,GRPNAME AS ACCGRPNAME," +
                             " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                             "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                             "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                             " ELSE  'Active' END AS ACTIVE_STATUS, " +
                             " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY " +
                             " FROM [M_TPU_VENDOR] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID" +
                             " LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON T.LEDGER_REFERENCEID=C.ID" +
                             " LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE" +
                             " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
                dt = db.GetData(sql);
            }
            else if (ID == "0" && AccountsGroup != "0")
            {
                string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,VENDORID,T.CODE, VENDORNAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, R.State_Name AS STATENAME, " +
                             " DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1, CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID,GSTNO, " +
                             " BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU, T.DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE,ISDELETED,TAG,GRPNAME AS ACCGRPNAME," +
                             " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                             "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                             "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                             " ELSE  'Active' END AS ACTIVE_STATUS, " +
                             " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY " +
                             " FROM [M_TPU_VENDOR] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID" +
                             " LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON T.LEDGER_REFERENCEID=C.ID " +
                             " LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE" +
                             " WHERE D.Code='" + AccountsGroup + "' " +
                             " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
                dt = db.GetData(sql);
            }
            else if (ID != "0" && AccountsGroup == "0")
            {
                string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,VENDORID,T.CODE, VENDORNAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, R.State_Name AS STATENAME, " +
                             " DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1, CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID,GSTNO, " +
                             " BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU, T.DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE,ISDELETED,TAG,GRPNAME AS ACCGRPNAME," +
                             " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                             "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                             "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                             " ELSE  'Active' END AS ACTIVE_STATUS,  " +
                             " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY " +
                             " FROM [M_TPU_VENDOR] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID" +
                             " LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON T.LEDGER_REFERENCEID=C.ID " +
                             " LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE " +
                             " WHERE SUPLIEDITEMID='" + ID + "'" +
                             " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
                dt = db.GetData(sql);
            }
            else if (ID != "0" && AccountsGroup != "0")
            {
                string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,VENDORID, T.CODE, VENDORNAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, R.State_Name AS STATENAME, " +
                             " DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1, CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID,GSTNO, " +
                             " BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU, T.DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE,ISDELETED,TAG,GRPNAME AS ACCGRPNAME," +
                             " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                             "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                             "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                             " ELSE  'Active' END AS ACTIVE_STATUS, " +
                             " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY " +
                             " FROM [M_TPU_VENDOR] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID " +
                             " LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON T.LEDGER_REFERENCEID=C.ID " +
                             " LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE " +
                             " WHERE SUPLIEDITEMID='" + ID + "' AND D.Code='" + AccountsGroup + "'" +
                             " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
                dt = db.GetData(sql);
            }

            return dt;
        }
        public DataTable BindTPUMasterById(string ID)
        {
            //string sql = "SELECT VENDORID, CODE, VENDORNAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID,CITYID1, STATEID1, COUNTRYID, DISTRICTID, CITYNAME,"+
            //              " STATENAME, DISTRICTNAME, ADDRESS,DISTRICTID1, CITYNAME1, STATENAME1, DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, "+
            //              " MOBILENO1, PINZIP, PINZIP1,EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID, BANKNAME, BRANCHID,"+
            //              " BRANCHNAME,IFSCCODE, CBU, DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE, ISAPPROVED,TAG,ACCCODE,ACCGRPNAME,SERVICETAX,DISTRICTNAME,CITYNAME,BRANCHNAME," +
            //              " ISNULL(COMPANYTYPEID,'1') AS COMPANYTYPEID,ISNULL(LEDGER_REFERENCEID,'0') AS LEDGER_REFERENCEID,GSTNO,APPLICABLEGST,ISNULL(ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO," +
            //              "ISNULL(TDSLIMIT,0) AS TDSLIMIT ,ISNULL(CREDIT_LIMIT,0) AS CREDIT_LIMIT,ISNULL(CREDIT_DAY,0) AS CREDIT_DAY,ISNULL(MSME_TAG,'N') AS MSME_TAG," +
            //              "ISNULL(MSME_NO,0) AS MSME_NO,CONVERT(VARCHAR,MSME_DATE,103) AS MSME_DATE,VENDOROWNER,TCS_PERCENT,TCS_LIMIT FROM [M_TPU_VENDOR] where VENDORID='" + ID + "' ";
            string sql= "EXEC USP_Bind_TPUMaster_By_Id '" + ID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSupliedItem()
        {
            string sql = "select ID,ITEM_NAME from M_SUPLIEDITEM order by ITEM_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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
            string sql = "select District_ID, District_Name from M_DISTRICT where State_ID='" + stateid + "'  order by District_Name ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCity(int distid)
        {
            string sql = "select City_ID, City_Name from M_CITY where District_ID='" + distid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBankName()
        {
            string sql = "select ID,BANKNAME from M_BANKMASTER order by BANKNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrachName()
        {
            string sql = "select ID, BRANCH from M_BANKBRANCH order by BRANCH ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDivision()
        {
            string sql = "select DIVID,DIVNAME from M_DIVISION order by DIVNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDivisionbySuppliedid(string Suppliedid)
        {
            string sql = "select DIVID,DIVNAME from M_DIVISION order by DIVNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            string sql = "select CATID,CATNAME from M_CATEGORY order by CATNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProductGrid(string divid, string catid)
        {
            string sql = "SELECT ID,PRODUCTALIAS AS NAME FROM M_PRODUCT where DIVID='" + divid + "' AND CATID='" + catid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductGridbyid(string id)
        {
            string sql = "SELECT PRODUCTID,PRODUCTNAME FROM M_PRODUCT_TPU_MAP WHERE VENDORID='" + id + "' ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveTPUMaster(string ID, string Code, string Name, int Supliedid, string Suplieditem, int stateid, string Statename,
                                    int districtid, string Districtname, int cityid, string Cityname, string Address,int stateid1, string Statename1, int districtid1,
                                    string Districtname1, int cityid1, string Cityname1, string Address1, string Mobile, string Phone, string Mobile1, string Phone1,
                                    string Pin,string Pin1, string Email, string Contractperson, string Cstno, string Vatno, string Tinno, string Panno, string stno
                                    , string Bankacno, string bankid, string Bankname, string Branchid, string Branchname, string ifsccode,
                                    string Mode, string istds, string approve, string Tag, string ACCCODE, string ACCGRPNAME, string tax, string COMPANYTYPEID, string LEDGER_REFERENCEID, string GSTNO, string GSTAPPLICABLE,
                                    string ISTRANSFERTOHO, decimal TDSLIMIT, string ipaddress, string MacAddress,decimal creditlimit, int creditday,string MSMEF,string msmeno,string msmedate,string VendorOwner, decimal TCS_PERCENT, decimal TCS_LIMIT) //TCS_PERCENT & TCS_LIMIT added 24/2/2021
        {
            int result = 0;          
            string sqlstr;         
            DataTable dt = new DataTable();
            try
            {
             
                if (istds == "True")
                {
                    istds = "Y";
                }
                else
                {
                    istds = "N";
                }
                if (approve == "True")
                {
                    approve = "Y";
                }
                else
                {
                    approve = "N";
                }
                string sqlvendoeid = string.Empty;
                string UPSQL = string.Empty;
                string strcheck;
                string GUID = Convert.ToString(Guid.NewGuid()).ToUpper();
                if (ID == "")
                {
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_TPU_VENDOR where VENDORNAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = -2;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Name != "")
                    {
                        
                        strcheck = "select top 1 * from M_TPU_VENDOR where   VENDORNAME='" + Name + "' and VENDORID <>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = -2;
                            return result;
                        }
                    }
                }

                #region Add by Rajeev(Check VENDOR Group)
                string GroupChk, GroupUpdate, GroupInsert, HeaderInsert, HeaderUpdate;
                int GroupStatus = 0, GroupOP = 0;
                GroupChk = " IF EXISTS(SELECT 1 FROM M_VENDOR_GROUP_DETAILS WHERE VENDORID='" + ID + "')  " +
                           " BEGIN " +
                           " SELECT 1 END  ELSE BEGIN SELECT 0   END ";
                GroupStatus = (Int32)db.GetSingleValue(GroupChk);
                if (GroupStatus > 0)
                {
                    GroupUpdate = "UPDATE M_VENDOR_GROUP_DETAILS SET GROUPID='" + COMPANYTYPEID + "',VENDORID='" + LEDGER_REFERENCEID + "',VENDORNAME='" + Name + "' WHERE VENDORID='" + ID + "'";
                    GroupOP = db.HandleData(GroupUpdate);
                    HeaderUpdate = "UPDATE M_VENDOR_GROUP_HEADER SET DTOC=GETDATE() WHERE GROUPID='" + COMPANYTYPEID + "'";
                    db.HandleData(HeaderUpdate);
                }
                else
                {                    
                    GroupInsert = " INSERT INTO M_VENDOR_GROUP_DETAILS(GROUPID,VENDORID,VENDORNAME)" +
                                  " VALUES('" + COMPANYTYPEID + "','" + GUID + "','" + Name + "')";
                    GroupOP = db.HandleData(GroupInsert);
                    HeaderInsert = "UPDATE M_VENDOR_GROUP_HEADER SET DTOC=GETDATE() WHERE GROUPID='" + COMPANYTYPEID + "'";
                    db.HandleData(HeaderInsert);
                }
                #endregion

                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {        
                        
                        sqlstr = "EXEC USP_TPUVENDOR_MASTER_INSERT_UPDATE '" + GUID + "','" + Code + "','" + Name + "','" + Supliedid + "','" + Suplieditem + "'," +
                                " '" + cityid + "','" + cityid1 + "','" + stateid + "','" + stateid1 + "',0,'" + districtid1 + "','" + districtid + "','" + Cityname + "'," +
                                " '" + Cityname1 + "','" + Statename + "','" + Statename1 + "','" + Districtname + "','" + Districtname1 + "','" + Address + "', " +
                                " '" + Address1 + "','" + Phone + "','" + Mobile + "','" + Phone1 + "','" + Mobile1 + "','" + Pin + "','" + Pin1 + "','" + Email + "', " +
                                " '" + Contractperson + "','" + Cstno + "','" + Vatno + "','" + Tinno + "','" + Panno + "','" + Bankacno + "','" + bankid + "'," + 
                                " '" + Bankname + "','" + Branchid + "','" + Branchname + "','" + ifsccode + "','" + stno + "','" + istds + "'," + 
                                " '" + HttpContext.Current.Session["UserID"].ToString() + "',0,'" + Mode + "','" + approve + "','N', '" + Tag + "','" + ACCCODE + "', " +
                                " '" + ACCGRPNAME + "','" + tax + "','" + COMPANYTYPEID + "','" + LEDGER_REFERENCEID + "','" + GSTNO + "','" + GSTAPPLICABLE + "', " +
                                " '" + ISTRANSFERTOHO + "'," + TDSLIMIT + " ,'" + ipaddress + "','" + MacAddress + "','" + creditlimit + "'," + creditday + ",'"+ MSMEF +"','"+ msmeno + "','"+ msmedate + "','"+ VendorOwner + "','" + TCS_PERCENT + "','" + TCS_LIMIT+ "' "; //TCS_PERCENT & TCS_LIMIT Added 24/02/2021

                        result = db.HandleData(sqlstr);
                   
                    }
                    else
                    {
                        sqlstr = "EXEC USP_TPUVENDOR_MASTER_INSERT_UPDATE '" + ID + "','" + Code + "','" + Name + "','" + Supliedid + "','" + Suplieditem + "'," +
                              " '" + cityid + "','" + cityid1 + "','" + stateid + "','" + stateid1 + "',0,'" + districtid1 + "','" + districtid + "','" + Cityname + "'," +
                              " '" + Cityname1 + "','" + Statename + "','" + Statename1 + "','" + Districtname + "','" + Districtname1 + "','" + Address + "', " +
                              " '" + Address1 + "','" + Phone + "','" + Mobile + "','" + Phone1 + "','" + Mobile1 + "','" + Pin + "','" + Pin1 + "','" + Email + "', " +
                              " '" + Contractperson + "','" + Cstno + "','" + Vatno + "','" + Tinno + "','" + Panno + "','" + Bankacno + "','" + bankid + "'," +
                              " '" + Bankname + "','" + Branchid + "','" + Branchname + "','" + ifsccode + "','" + stno + "','" + istds + "'," +
                              " '" + HttpContext.Current.Session["UserID"].ToString() + "',0,'" + Mode + "','" + approve + "','N', '" + Tag + "','" + ACCCODE + "', " +
                              " '" + ACCGRPNAME + "','" + tax + "','" + COMPANYTYPEID + "','" + LEDGER_REFERENCEID + "','" + GSTNO + "','" + GSTAPPLICABLE + "', " +
                              " '" + ISTRANSFERTOHO + "'," + TDSLIMIT + " ,'" + ipaddress + "','" + MacAddress + "','" + creditlimit + "'," + creditday + ",'" + MSMEF + "','" + msmeno + "','" + msmedate + "','"+ VendorOwner + "','" + TCS_PERCENT + "','" + TCS_LIMIT + "' "; //TCS_PERCENT & TCS_LIMIT Added 24/02/2021


                        result = db.HandleData(sqlstr);                  
                        
                    }
                }
                else
                {

                }                
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public string GetstatusChecking(string ID)
        {

            string Sql = " EXEC USP_VENDOR_DELETE_CHECKING '" + ID + "' ";
            return ((string)db.GetSingleValue(Sql));
        }

        public int DeleteTPUMaster(string ID)
        {
            int result = 0;
            string sqlstr=string.Empty;
            try
            {
                sqlstr = "EXEC UPS_TPU_FACTORY_VENDOR_DELETE'" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int SaveProductMapping(string TPUID,string TPUNAME,string TAG,string xml)
        {
            int d = 0;
            try
            { 
                string sql = string.Empty;
                sql = "EXEC SP_TPU_PRODUCT_MAPPING '" + TPUID + "','" + TPUNAME + "','" + TAG + "','" + xml + "'";
                d = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return d;
        }

        public DataTable BindProductNameGridbyid(string id)
        {
            string sql = "SELECT PRODUCTID,PRODUCTNAME FROM M_PRODUCT_TPU_MAP WHERE VENDORID='" + id + "' order by PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;

        }

        public DataSet ProductMappingDetails(string TPUID)
        {
            string sql = "EXEC SP_PRODUCT_TPU_DETAILS '" + TPUID + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;

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
        public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string vendorid, string mode, string CostCenter, string TaxId)
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
                sql = "exec [SP_ACC_LEDGER_INSERT] '" + Name + "','" + AccGroupCode + "','" + Finyear + "','" + mode + "','" + accid + "','" + xml + "','" + vendorid + "','T','" + CostCenter + "','" + TaxId + "'";
                flag = (int)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }

        public string Bindvendorid(string NAME)
        {

            string sql = "SELECT VENDORID FROM M_TPU_VENDOR where VENDORNAME='" + NAME + "'";
            string id = string.Empty;

            id = (string)db.GetSingleValue(sql);
            return id;
        }
        public DataTable BindPrimaryitemtype()
        {
            string sql = "SELECT ITEM_NAME+'~'+CAST(ID AS VARCHAR(5)) AS ID,ITEMDESC FROM M_SUPLIEDITEM WHERE ACTIVE='Y' AND ID <> 1 ORDER BY ITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSubitemtype(string PTYPEID)
        {
            string sql = "SELECT SUBTYPEID,SUBITEMDESC FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID='" + PTYPEID + "' AND ACTIVE='Y' ORDER BY SUBITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindFactory()
        {
            string sql = "SELECT VENDORID,VENDORNAME,CODE FROM M_TPU_VENDOR WHERE TAG='F'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchTaxId(string VendorId)
        {
            string sql = "SELECT ISNULL(COSTCENTER,'N') AS COSTCENTER,ISNULL(TAXID,'') AS TAXID FROM ACC_ACCOUNTSINFO WHERE ID='" + VendorId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveDepotWiseGstMapped(string Vendorid, string xml)
        {
            int result = 0;
            string sql = "EXEC [USP_VENDOR_DEPOTWISE_MAPPING_INSERT] '" + Vendorid + "','" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable EditDEPOTWISEGSTMAPPING(string VENDORID)
        {
            string sql = "SELECT  VENDORID,VENDORNAME,BRID,BRNAME " +
                          " FROM M_TPU_VENDOR_DEPOT_MAPPING WHERE VENDORID='" + VENDORID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveUploadVendor(string xml, string UserID, string IPAddress)
        {

            DataTable flag = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "EXEC [USP_Insert_Upload_TPUVENDOR] '" + xml + "','" + UserID + "','" + IPAddress + "'";
                flag = db.GetData(sql);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;
        }


    }
}
