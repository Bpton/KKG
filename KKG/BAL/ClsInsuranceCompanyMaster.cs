using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DAL;
using System.Data;
using Utility;
using System.Globalization;


namespace BAL
{
     public class ClsInsuranceCompanyMaster
      {
         DBUtils db = new DBUtils();


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
             string sql = "select City_ID, City_Name from M_CITY where District_ID='" + distid + "' order by City_Name";
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

         public DataTable BindinsuranceGrid()
         {
             string sql = " SELECT ID,COMPANY_NAME ,CODE,  CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, STATENAME, " +
                          " DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1, CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO,  TINNO, PANNO, BANKACNO,BANKID, " +
                          " BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU, DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE, ISAPPROVED, ISDELETED" +
                          " FROM [M_INSURANCECOMPANY] ORDER BY COMPANY_NAME";
             DataTable dt = new DataTable();
             dt = db.GetData(sql);
             return dt;
         }

         public DataTable BindInsuranceById(string ID)
         {
             string sql = "SELECT ID, COMPANY_NAME, CODE, CITYID, CITYID1, STATEID, STATEID1, COUNTRYID, DISTRICTID1, DISTRICTID, CITYNAME, CITYNAME1, STATENAME, STATENAME1, DISTRICTNAME, DISTRICTNAME1, ADDRESS, ADDRESS1, PHONENO, MOBILENO, PHONENO1, MOBILENO1, PINZIP, PINZIP1, EMAILID, CONTACTPERSON, CSTNO, TINNO, PANNO, BANKACNO, BANKID, BANKNAME, BRANCHID, BRANCHNAME, IFSCCODE, ISTDSDECLARE, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, ISDELETED, ACCCODE, ACCGRPNAME FROM [M_INSURANCECOMPANY] where ID='" + ID + "' ";
             DataTable dt = new DataTable();
             dt = db.GetData(sql);
             return dt;
         }

         public int SaveinsuranceMaster(string ID, string Code, string Name, int cityid, string Cityname, int districtid, string Districtname,
                                         int stateid, string Statename, string Address, string Address1, string Mobile, string Phone, string Mobilealt, 
                                        string Phonealt, string Pin, string Pinalt, string Email, string Contractperson, string Cstno,  string Tinno,
                                        string Panno, string Bankacno, string ifsccode, string bankid, string Bankname, string Branchid, string Branchname, 
                                          string Mode, string istds, string approve,  string ACCCODE, string ACCGRPNAME)
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
                         strcheck = "select top 1 * from M_INSURANCECOMPANY where CODE='" + Code + "'";
                         dt = db.GetData(strcheck);
                         if (dt.Rows.Count != 0)
                         {
                             result = 2;
                             return result;
                         }
                     }
                     if (Name != "")
                     {
                         strcheck = "select top 1 * from M_INSURANCECOMPANY where COMPANY_NAME='" + Name + "'";
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
                         strcheck = "select top 1 * from M_INSURANCECOMPANY where CODE='" + Code + "' and ID <>'" + ID + "'";
                         dt = db.GetData(strcheck);
                         if (dt.Rows.Count != 0)
                         {
                             result = 2;
                             return result;
                         }
                     }
                     if (Name != "")
                     {
                         strcheck = "select top 1 * from M_INSURANCECOMPANY where COMPANY_NAME='" + Name + "' and ID <>'" + ID + "'";
                         dt = db.GetData(strcheck);
                         if (dt.Rows.Count != 0)
                         {
                             result = 3;
                             return result;
                         }
                     }
                 }
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

                 if (dt.Rows.Count == 0)
                 {
                     if (Mode == "A")
                     {
                         sqlstr = "insert into M_INSURANCECOMPANY( ID,  CODE,COMPANY_NAME, CITYID, CITYNAME,DISTRICTID,DISTRICTNAME ,STATEID,  STATENAME,"+   
                                  " ADDRESS, ADDRESS1, PHONENO, MOBILENO, PHONENO1, MOBILENO1, PINZIP, PINZIP1, EMAILID, CONTACTPERSON, CSTNO, TINNO, PANNO,"+
                                  " BANKACNO, BANKID, BANKNAME, BRANCHID, BRANCHNAME, IFSCCODE, ISTDSDECLARE, CBU, DTOC,  STATUS, ISAPPROVED, ISDELETED,"+
                                  " ACCCODE, ACCGRPNAME)" +
                                  " values(NEWID(),'" + Code + "', '" + Name + "','" + cityid + "','" + Cityname + "','" + districtid + "','" + Districtname + "','" + stateid + "','" + Statename + "',"+
                                  "'" + Address + "','" + Address1 + "','" + Phone + "', '" + Mobile + "','" + Phonealt + "','" + Mobilealt + "',"+
                                  "'" + Pin + "','" + Pinalt + "', '" + Email + "','" + Contractperson + "','" + Cstno + "','" + Tinno + "','" + Panno + "',"+
                                  "'" + Bankacno + "','" + bankid + "','" + Bankname + "','" + Branchid + "','" + Branchname + "','" + ifsccode + "','" + istds + "',"+
                                  "'" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'" + Mode + "','" + approve + "','N','" + ACCCODE + "','" + ACCGRPNAME + "')";
                         result = db.HandleData(sqlstr);
                     }
                     else
                     {
                         sqlstr = "update M_INSURANCECOMPANY set CODE='" + Code + "', COMPANY_NAME='" + Name + "',CITYID='" + cityid + "', STATEID='" + stateid + "', DISTRICTID='" + districtid + "',CITYNAME='" + Cityname + "',STATENAME='" + Statename + "',DISTRICTNAME='" + Districtname + "',ADDRESS='" + Address + "',PHONENO='" + Phone + "',MOBILENO='" + Mobile + "',PHONENO1='" + Phonealt + "',MOBILENO1='" + Mobilealt + "'," +
                             " ADDRESS1='" + Address1 + "'," +
                             "  PINZIP='" + Pin + "',PINZIP1='" + Pinalt + "',EMAILID='" + Email + "',CONTACTPERSON='" + Contractperson + "',CSTNO='" + Cstno + "',TINNO='" + Tinno + "',PANNO='" + Panno + "',BANKACNO='" + Bankacno + "',BANKID='" + bankid + "',BANKNAME='" + Bankname + "',BRANCHID='" + Branchid + "',BRANCHNAME='" + Branchname + "',IFSCCODE='" + ifsccode + "', LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ISTDSDECLARE='" + istds + "',ISAPPROVED='" + approve + "',ACCCODE='" + ACCCODE + "',ACCGRPNAME='" + ACCGRPNAME + "' where ID= '" + ID + "'";
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

         public int DeleteinsuranceMaster(string ID)
         {
             int result = 0;
             string sqlstr = string.Empty;
             try
             {
                 sqlstr = "EXEC [SP_INSURANCECOMPANY_ACOUNTINFO_OPENINGBALANCE_DELETE] '" + ID + "'";

                 result = db.HandleData(sqlstr);
                 

             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
             }
             return result;
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
         public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string vendorid, string mode)
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
                 sql = "exec [SP_ACC_LEDGER_INSERT] '" + Name + "','" + AccGroupCode + "','" + Finyear + "','" + mode + "','" + accid + "','" + xml + "','" + vendorid + "','T'";
                 flag = (int)db.GetSingleValue(sql);
             }
             catch (Exception ex)
             {
                 CreateLogFiles Errlog = new CreateLogFiles();
                 Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
             }
             return flag;


         }

         public string Bindinsuranceid(string NAME)
         {

             string sql = "SELECT ID FROM M_INSURANCECOMPANY where COMPANY_NAME='" + NAME + "'";
             string id = string.Empty;

             id = (string)db.GetSingleValue(sql);
             return id;
         }

      }
}
