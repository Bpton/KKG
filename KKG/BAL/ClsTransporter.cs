using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsTransporter
    {
        DBUtils db = new DBUtils();

        public DataTable BindTransporterGrid()
        {
            string sql = " SELECT	ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,T.ID,T.CODE, T.NAME, CITYID, STATEID, " +
                         "   COUNTRYID, DISTRICTID, CITYNAME, STATENAME, DISTRICTNAME, ADDRESS, PHONENO, MOBILENO,PHONENO1, MOBILENO1," +
                         "   PINZIP, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID, " +
                         "   BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, T.CBU, T.DTOC, T.LMBU, T.LDTOM, " +
                         "   STATUS, ISTDSDECLARE,ISAPPROVED, ISDELETED,ACCGROUPID,GRPNAME AS ACCGROUPNAME,ADDRESS2,PINZIP2,R.State_Name AS State_Name,SERVICETAX,GSTNO," +
                         "   ISNULL(T.ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO, ISNULL(REVERSECHARGE,'N') AS REVERSECHARGE,ISNULL(T.TDSLIMIT,0) AS TDSLIMIT," +
                         "   CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                         "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                         "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                         "   ELSE  'Active' END AS ACTIVE_STATUS," +
                         "   CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY" +
                         "   FROM [M_TPU_TRANSPORTER] T " +
                         "   INNER JOIN M_REGION R ON T.STATEID=R.State_ID " +
                         "   LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON CAST(T.ID AS VARCHAR(50)) = C.ID " +
                         "   LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE"+
                         "   WHERE T.ISERP IN('Y','U') " +
                         "  ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterGridByState(int stateid)
        {
            DataTable dt = new DataTable();
            if (stateid == 0)
            {
                string sql = "  SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO, T.ID, T.CODE, T.NAME,CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, STATENAME, DISTRICTNAME, ADDRESS, PHONENO, MOBILENO,PHONENO1," +
                             " MOBILENO1, PINZIP, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID, BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU," +
                             " T.DTOC, LMBU, LDTOM, STATUS, ISTDSDECLARE,ISAPPROVED, ISDELETED,ACCGROUPID,ACCGROUPNAME,ADDRESS2,PINZIP2,R.State_Name AS State_Name,SERVICETAX,GSTNO," +
                             " ISNULL(T.ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO, ISNULL(REVERSECHARGE,'N') AS REVERSECHARGE," +
                             " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active'" +
                             " 				WHEN ISAPPROVED ='' THEN 'In-Active'" +
                             " 				WHEN ISAPPROVED ='N'	THEN 'In-Active'" +
                             " ELSE  'Active' END AS ACTIVE_STATUS," +
                             " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY"+
                             " FROM [M_TPU_TRANSPORTER] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID " +
                             "   LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON CAST(T.ID AS VARCHAR(50)) = C.ID " +
                             "   LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE"+
                             " WHERE T.ISERP IN('Y','U') " +
                             " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
                dt = db.GetData(sql);
            }
            else
            {
                string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO, T.ID, T.CODE, T.NAME, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, STATENAME, DISTRICTNAME, ADDRESS, PHONENO, MOBILENO,PHONENO1," +
                             " MOBILENO1, PINZIP, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID, BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU," +
                             " T.DTOC, LMBU, LDTOM, STATUS, ISTDSDECLARE,ISAPPROVED, ISDELETED,ACCGROUPID,ACCGROUPNAME,ADDRESS2,PINZIP2,R.State_Name AS State_Name,SERVICETAX,GSTNO ," +
                             " ISNULL(T.ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO, ISNULL(REVERSECHARGE,'N') AS REVERSECHARGE," +
                             " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active'" +
                             " 				WHEN ISAPPROVED ='' THEN 'In-Active'" +
                             " 				WHEN ISAPPROVED ='N'	THEN 'In-Active'" +
                             " ELSE  'Active' END AS ACTIVE_STATUS," +
                             " CASE WHEN T.COMPANYTYPEID='1' THEN 'Company' ELSE 'Non Company' END AS COMPANY" +
                             " FROM [M_TPU_TRANSPORTER] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID " +
                             "   LEFT OUTER JOIN ACC_ACCOUNTSINFO C ON CAST(T.ID AS VARCHAR(50)) = C.ID " +
                             "   LEFT OUTER JOIN ACC_ACCOUNTGROUP D ON C.ACTGRPCODE=D.CODE  " +
                             " WHERE STATEID=" + stateid + " "+
                             " AND T.ISERP IN('Y','U')"+
                             " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
                dt = db.GetData(sql);
            }
            return dt;
        }
        public DataTable BindTransporterById(string ID)
        {
            string sql = "EXEC [USP_TRANSPOTER_MASTER_EDIT] '"+ ID +"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPUMasterGrid()
        {
            //string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE SUPLIEDITEM='FG'  ORDER BY VENDORNAME";
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR   ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAccGroup()
        {
            string sql = "SELECT Code,grpName FROM Acc_AccountGroup ORDER BY grpName";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPUMasterGridbyid(string id)
        {
            string sql = "SELECT TPUID,TPUNAME FROM M_TRANSPOTER_TPU_MAP WHERE TRANSPOTERID='" + id + "' ORDER BY TPUNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BinddepotMasterGrid()
        {
            string sql = "SELECT BRID,BRNAME FROM M_BRANCH WHERE BRANCHTAG IN('D','O') ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BinddepotMasterGridbyid(string id)
        {
            string sql = "SELECT DEPOTID,DEPOTNAME FROM M_TRANSPOTER_DEPOT_MAP where TRANSPOTERID='" + id + "' order by DEPOTNAME";
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

        public string BindStateCode(int State_ID)
        {
            string code = string.Empty;
            string sql = "select State_Code from M_REGION where State_ID=" + State_ID + "";

            code = (string)db.GetSingleValue(sql);
            return code;
        }
        public DataTable BindDistrict(int stateid)
        {
            string sql = "select District_ID, District_Name from M_DISTRICT where State_ID='" + stateid + "' order by District_Name ";
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
            string sql = "select ID,BRANCH from M_BANKBRANCH order by BRANCH  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int BindtransporterByID(string name, string code, string stateid, string districtid, string cityid, string mobileno)
        {
            try
            {
                int id = 0;

                string sql = "select ID from M_TPU_TRANSPORTER where NAME='" + name + "' and CODE='" + code + "' and STATEID='" + stateid + "' and DISTRICTID='" + districtid + "' and CITYID='" + cityid + "' and MOBILENO='" + mobileno + "'";


                id = (int)db.GetSingleValue(sql);
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGridBranch()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " select [BRID] as REGIONID,[BRNAME] AS REGIONNAME,BRANCHTAG from [M_BRANCH] order by BRNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string TransporterID)
        {
            int flag = 0;
            string sql = string.Empty;
            string mode = string.Empty;
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
                sql = "exec [SP_ACC_LEDGER_INSERT] '" + Name + "','" + AccGroupCode + "','" + Finyear + "','" + mode + "','" + accid + "','" + xml + "','" + TransporterID + "','T'";
                flag = (int)db.GetSingleValue(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return flag;


        }

        public int SaveTPUMap(string ID, string transporterid, string transportername, string tpuid, string tpuname, string Mode, string Tag)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (transportername != "" && tpuname != "")
                    {
                        strcheck = "select * from [M_TRANSPOTER_TPU_MAP] WHERE TRANSPOTERID='" + transporterid + "' AND TPUID='" + tpuid + "'";
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

                    if (transportername != "" && tpuname != "")
                    {
                        //strcheck = "select top 1 * from M_DIVISION where DIVCODE='" + Code + "' and  DIVID<>'" + ID + "'";
                        //dt = db.GetData(strcheck);
                        //if (dt.Rows.Count != 0)
                        //{
                        //    result = 2;
                        //    return result;
                        //}
                    }
                }


                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_TRANSPOTER_TPU_MAP(TRANSPOTERID, TRANSPOTERNAME, TPUID, TPUNAME,TAG)" +
                           " values('" + transporterid + "', '" + transportername + "','" + tpuid + "','" + tpuname + "','" + Tag + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        //sqlstr = "update M_DIVISION set DIVCODE='" + Code + "',DIVNAME='" + Name + "',DIVDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "' where DIVID= '" + ID + "'";
                        //result = db.HandleData(sqlstr);
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
        public int SaveDepotMap(string ID, string transporterid, string transportername, string depotid, string depotname, string Mode, string Tag)
        {
            int result = 0;
            DataTable dt = new DataTable();
            try
            {
                string strsql = string.Empty;
                strsql = "EXEC [USP_TRANSPORTER_DEPOT_MAPPING] '" + transporterid + "','" + transportername + "','" + depotid + "','" + depotname + "'";
                result = db.HandleData(strsql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }

        public void DeleteTPUMapbyid(string id)
        {


            string sql1 = "DELETE FROM M_TRANSPOTER_TPU_MAP WHERE TRANSPOTERID='" + id + "' ";
            db.HandleData(sql1);

            string sql2 = "DELETE FROM M_TRANSPOTER_DEPOT_MAP WHERE TRANSPOTERID ='" + id + "' ";
            db.HandleData(sql2);

        }

        public DataTable GenerateTransporterCode()
        {
            string sql = "EXEC USP_TRANSPORTER_CODE_GENERATION ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveTransporter(string ID, string Code, string Name, int stateid, string Statename, int districtid, string Districtname,
                                int cityid, string Cityname, string Address, string Mobile, string Phone, string Mobile1, string Phone1, string Pin,
                                string Email, string Contractperson, string Cstno, string Vatno, string Tinno, string Panno, string stno, string Bankacno,
                    string bankid, string Bankname, string Branchid, string Branchname, string ifsccode, string Mode, string istds, string approve,
                    string AccGroupid, string AccGroupname, string Address2, string Pin2, string tax, string ISACCPOSTING_TOHO, string ISMORE_VEHICLE,
                    string LEDGER_REFERENCEID, string COMPANYTYPEID, string GSTNO, string APPLICABLEGST, string ISTRANSFERTOHO, string REVERSECHARGE, decimal TDSLIMIT, 
                    string ipaddress, string MacAddress,decimal creditlimit,int creditday, string MSMEF, string msmeno, string msmedate)
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

             
                    if (Mode == "A")
                    {
                     sqlstr="EXEC USP_TRANSPOTER_MASTER_INSERT_UPDATE '" + Code + "','" + Name + "'," + cityid + "," + stateid + ",0," + districtid + ",'" + 
                          Cityname + "','" + Statename + "','" + Districtname + "', '" + Address + "', '" + Phone + "','" + Mobile + "','" + Phone1 + "','" + 
                          Mobile1 + "','" + Pin + "', '" + Email + "','" + Contractperson + "','" + Cstno + "','" + Vatno + "','" + Tinno + "','" + Panno + "','" + 
                          Bankacno + "','" + bankid + "'," + " '" + Bankname + "','" + Branchid + "', '" + Branchname + "','" + ifsccode + "','" + stno + "','" + 
                          istds + "','" + HttpContext.Current.Session["UserID"].ToString() + "',0,'" + Mode + "','" + approve + "','N','" + 
                          AccGroupid + "','" + AccGroupname + "','" + Address2 + "','" + Pin2 + "','" + tax + "','" + ISACCPOSTING_TOHO + "','" + 
                          ISMORE_VEHICLE + "','" + LEDGER_REFERENCEID + "','" + COMPANYTYPEID + "','" + ISTRANSFERTOHO + "','" + GSTNO + "','" + 
                          APPLICABLEGST + "','" + REVERSECHARGE + "','" + TDSLIMIT + "','Y',0,'" + ipaddress + "','" + MacAddress + "','"+ creditlimit + "',"+ creditday + ",'" + MSMEF + "','" + msmeno + "','" + msmedate + "'";
		
                        result = db.HandleData(sqlstr);                      

                                                            
                    }
                    else
                    {
                        sqlstr = "EXEC USP_TRANSPOTER_MASTER_INSERT_UPDATE '" + Code + "','" + Name + "'," + cityid + "," + stateid + ",0," + districtid + ",'" +
                         Cityname + "','" + Statename + "','" + Districtname + "', '" + Address + "', '" + Phone + "','" + Mobile + "','" + Phone1 + "','" +
                         Mobile1 + "','" + Pin + "', '" + Email + "','" + Contractperson + "','" + Cstno + "','" + Vatno + "','" + Tinno + "','" + Panno + "','" +
                         Bankacno + "','" + bankid + "'," + " '" + Bankname + "','" + Branchid + "', '" + Branchname + "','" + ifsccode + "','" + stno + "','" +
                         istds + "','" + HttpContext.Current.Session["UserID"].ToString() + "',0,'" + Mode + "','" + approve + "','N','" +
                         AccGroupid + "','" + AccGroupname + "','" + Address2 + "','" + Pin2 + "','" + tax + "','" + ISACCPOSTING_TOHO + "','" +
                         ISMORE_VEHICLE + "','" + LEDGER_REFERENCEID + "','" + COMPANYTYPEID + "','" + ISTRANSFERTOHO + "','" + GSTNO + "','" +
                         APPLICABLEGST + "','" + REVERSECHARGE + "','" + TDSLIMIT + "','Y'," + ID + ",'" + ipaddress + "','" + MacAddress + "','" + creditlimit + "'," + creditday + ",'" + MSMEF + "','" + msmeno + "','" + msmedate + "'";

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

        public string GetstatusChecking(string TRANSPORTERID)
        {

            string Sql = " USP_TRANSPORTER_DELETE_CHECKING '" + TRANSPORTERID + "' ";
            return ((string)db.GetSingleValue(Sql));
        }
        public int DeleteTransporterMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_TRANSPORTER_DELETE '" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable BindStategst()
        {
            string sql = "select State_ID, State_Name,'' AS GSTNO,'' AS GSTADDRESS from M_REGION order by State_Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditSTATEWISEGSTMAPPING(string TRANSPORTERID)
        {
            string sql = "SELECT  TRANSPORTERID,TRANSPORTERNAME,State_ID,STATENAME,ISNULL(GSTNO,'') AS GSTNO," +
                          " ISNULL(GSTADDRESS,'') AS GSTADDRESS FROM M_TRANSPORTER_STATEWISE_GST_MAPPING WHERE TRANSPORTERID='" + TRANSPORTERID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public int SaveSateWiseGstMapped(string tranid, string xml)
        {
            int result = 0;
            string sql = "EXEC [USP_TRANSPORTER_STATEWISE_GST_MAPPING_INSERT] '" + tranid + "','" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }
        public DataTable BindTransporterbill(string CBU, string Checker, string fromData, string ToDate)
        {
            DataTable dt = new DataTable();

            string sql = string.Empty;
            if (Checker == "FALSE")
            {
                sql = " SELECT A.TRANSPORTERBILLID,TRANSPORTERBILLNO,CONVERT(VARCHAR(10),CAST(TRANSPORTERBILLDATE AS DATE),103) AS TRANSPORTERBILLDATE,TRANSPORTERID," +
                      " TRANSPORTERNAME,BILLTYPEID,REMARKS,CASE WHEN BILLTYPEID LIKE '%STKINV%' THEN 'SALE INVOICE' WHEN BILLTYPEID LIKE 'STKTRN' THEN 'STOCK TRANSFER' " +
                      " WHEN BILLTYPEID LIKE 'STKDES' THEN 'TPU DESPATCH' ELSE 'OTHERS' END AS 'BILLTYPE', " +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                      " ISNULL(F.TOTALGROSSWEIGHT,0.00) AS TOTALGROSSWEIGHT,ISNULL(F.TOTALBILLAMOUNT,0.00) AS TOTALBILLAMOUNT,ISNULL(F.TOTALTDS,0.00)AS TOTALTDS,ISNULL(F.TOTALSERVICETAX,0.00) AS TOTALSERVICETAX ," +
                      " ISNULL( F.TOTALNETAMOUNT,0.00)AS TOTALNETAMOUNT,(U.FNAME+''+U.MNAME+''+LNAME) AS APPROVALPERSONNAME,ISNULL((F.TOTALBILLAMOUNT-F.TOTALTDS),0) AS [DEDUCTABLETDSAMT] FROM T_TRANSPORTER_BILL_HEADER A " +
                      " INNER JOIN T_TRANSPORTER_BILL_FOOTER F ON A.TRANSPORTERBILLID=F.TRANSPORTERBILLID " +
                      " INNER JOIN M_USER U ON CAST(USERID AS VARCHAR(50))= NEXTLEVELID  " +
                      " WHERE  A.CBU='" + CBU + "' AND CONVERT(DATETIME, TRANSPORTERBILLDATE,103 )>=CONVERT(DATE,'01.07.2017',103) " +
                      " AND CONVERT(DATE,TRANSPORTERBILLDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromData + "') AND DBO.Convert_To_ISO('" + ToDate + "')" +
                      " ORDER BY CONVERT(DATETIME, TRANSPORTERBILLDATE ) DESC";
            }
            else
            {
                sql = " SELECT A.TRANSPORTERBILLID,TRANSPORTERBILLNO,CONVERT(VARCHAR(10),CAST(TRANSPORTERBILLDATE AS DATE),103) AS TRANSPORTERBILLDATE,TRANSPORTERID," +
                      " TRANSPORTERNAME,BILLTYPEID,REMARKS,CASE WHEN BILLTYPEID LIKE '%STKINV%' THEN 'SALE INVOICE' WHEN BILLTYPEID LIKE 'STKTRN' THEN 'STOCK TRANSFER' " +
                      " WHEN BILLTYPEID LIKE 'STKDES' THEN 'TPU DESPATCH' ELSE 'OTHERS' END AS 'BILLTYPE', " +
                      " CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                      " ISNULL(F.TOTALGROSSWEIGHT,0.00) AS TOTALGROSSWEIGHT,ISNULL(F.TOTALBILLAMOUNT,0.00) AS TOTALBILLAMOUNT,ISNULL(F.TOTALTDS,0.00)AS TOTALTDS,ISNULL(F.TOTALSERVICETAX,0.00) AS TOTALSERVICETAX ," +
                      " ISNULL(F.TOTALNETAMOUNT,0.00)AS TOTALNETAMOUNT,(U.FNAME+''+U.MNAME+''+LNAME) AS APPROVALPERSONNAME,ISNULL((F.TOTALBILLAMOUNT-F.TOTALTDS),0) AS [DEDUCTABLETDSAMT] FROM T_TRANSPORTER_BILL_HEADER A " +
                      " INNER JOIN T_TRANSPORTER_BILL_FOOTER F ON A.TRANSPORTERBILLID=F.TRANSPORTERBILLID " +
                      " INNER JOIN M_USER U ON CAST(USERID AS VARCHAR(50))= NEXTLEVELID " +
                      " WHERE ISVERIFIED <> 'Y'AND A.NEXTLEVELID IN('" + CBU + "','123') AND CONVERT(DATETIME, TRANSPORTERBILLDATE,103 )>=CONVERT(DATE,'01.07.2017',103)" +
                      " AND CONVERT(DATE,TRANSPORTERBILLDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromData + "') AND DBO.Convert_To_ISO('" + ToDate + "')" +
                      " ORDER BY CONVERT(DATETIME, TRANSPORTERBILLDATE ) DESC";
            }

            dt = db.GetData(sql);
            return dt;
        }
        public int UpdateLdomTpuTransporterMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {

                sqlstr = "UPDATE M_TPU_TRANSPORTER SET LDTOM=GETDATE() WHERE ID='" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        #region UPload Transporter

        public string Getsateid(string STATENAME)
        {

            string stateid = "SELECT  CAST(STATE_ID AS VARCHAR(50)) AS STATE_ID FROM M_REGION WHERE STATE_NAME='" + STATENAME + "'";
            string id = (string)db.GetSingleValue(stateid);
            return id;
        }

        public string Getdistrictid(string STATENAME, string DISTRICTNAME)
        {

            //string districtid = "SELECT CAST(DISTRICT_ID AS VARCHAR(50)) AS DISTRICT_ID FROM M_DISTRICT WHERE DISTRICT_NAME='" + DISTRICTNAME + "'";
            string districtid = "SELECT CAST(DISTRICT_ID AS VARCHAR(50)) AS DISTRICT_ID FROM M_DISTRICT AS A " +
                                "INNER JOIN M_REGION AS B ON A.State_ID=B.State_ID WHERE STATE_NAME='" + STATENAME + "' AND DISTRICT_NAME='" + DISTRICTNAME + "'";
            string id = (string)db.GetSingleValue(districtid);
            return id;
        }

        public string Getcityid(string STATENAME, string CITYNAME, string DISTRICTNAME)
        {

            //string cityid = "SELECT CAST(CITY_ID AS VARCHAR(50)) AS CITY_ID FROM M_CITY WHERE CITY_NAME='" + CITYNAME + "'";
            string cityid = "SELECT CAST(CITY_ID AS VARCHAR(50)) AS CITY_ID FROM M_CITY AS A " +
                            "INNER JOIN M_REGION AS B ON A.State_ID=B.State_ID " +
                            "INNER JOIN M_DISTRICT AS D ON D.District_ID=A.District_ID " +
                            "WHERE STATE_NAME='" + STATENAME + "'AND CITY_NAME='" + CITYNAME + "'AND DISTRICT_NAME='" + DISTRICTNAME + "' ";
            string id = (string)db.GetSingleValue(cityid);
            return id;
        }
        public string GetstatusforTransporter(string NAME)
        {
            string statusid = string.Empty;
            string status;
            try
            {
                {
                    status = "  IF  EXISTS(SELECT 1 FROM [M_TPU_TRANSPORTER] WHERE NAME='" + NAME + "') BEGIN " +
                                   " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";

                    statusid = (string)db.GetSingleValue(status);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return statusid;
        }
        public DataTable CreateTempOfUploadTransporter()
        {
            string SQL = "  SELECT  '' AS Name,'' AS CODE,''AS TINNO,'' AS Address,'' AS CITYNAME,'' AS DISTRICTNAME,State_ID AS STATEID,State_Name AS STATENAME,'' AS PINZIP,'' AS MOBILENO," +
                         " '' AS GSTNO,'' AS APPLYTDS,'' AS PANNO, '' AS category FROM M_REGION  ";

            DataTable dt = db.GetData(SQL);
            return dt;
        }

        public DataTable CreateTempOfUploadTpuvendor()
        {
            string SQL = "  SELECT  '' AS VENDORNAME,'' AS CODE,'' AS ADDRESS,State_ID AS STATEID,State_Name AS STATENAME,'' AS PINZIP,'' AS MOBILENO,'' AS PHONENO," +
                         " '' AS GSTNO,'' AS APPLICABLEGST,'' AS PANNO,'' AS COMPANYTYPE FROM M_REGION  ";

            DataTable dt = db.GetData(SQL);
            return dt;
        }

        public DataTable ChecktpuVendoe(string TpuVandorname)
        {
            string SQL = " SELECT TOP 1 VENDORNAME FROM M_TPU_VENDOR WHERE VENDORID='" + TpuVandorname + "'  ";

            DataTable dt = db.GetData(SQL);
            return dt;
        }

        public int SaveTransporterUpload(string finyear, string xmlTransporterDetails, string xmlAccountInfo, string xmlOpeningBalance)
        {
            int result = 0;
            string sqlstr;

            try
            {
                {
                    sqlstr = " EXEC [USP_TRANSPORTER_UPLOAD] '" + finyear + "','" + xmlTransporterDetails + "','" + xmlAccountInfo + "','" + xmlOpeningBalance + "'";
                    result = db.HandleData(sqlstr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public int SaveTPUVENDOREUpload(string finyear, string accgpid,string xmlTransporterDetails, string xmlAccountInfo, string xmlOpeningBalance)
        {
            int result = 0;
            string sqlstr;

            try
            {
                {
                    sqlstr = " EXEC [USP_TPUVENDOR_UPLOAD] '" + finyear + "','"+ accgpid + "','" + xmlTransporterDetails + "','" + xmlAccountInfo + "','" + xmlOpeningBalance + "'";
                    result = db.HandleData(sqlstr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        #endregion

        public DataTable insertTransporter(string Name)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            
            try
            {
                 sql = "USP_INSERT_TARNSPORTER_USING_NAME'"+ Name + "'";
                 dt = db.GetData(sql);
            }
            catch(Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;

        }

    }
}

