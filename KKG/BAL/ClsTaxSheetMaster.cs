using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsTaxSheetMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindTaxMasterGrid()
        {
            //string sql = " select M_TAX.ID, M_TAX.NAME,Code,DESCRIPTION, PERCENTAGE,M_TAX_RELATEDTO.NAME as RELATEDTO,[REGIONTAXNAME] as [APPLICABLETO]," +
            //             " case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE'  from M_TAX " +
            //             " inner join [M_REGIONTAX] on [REGIONTAXID]=[APPLICABLETO] " +
            //             " inner join M_TAX_RELATEDTO on [M_TAX].[RELATEDTO]=M_TAX_RELATEDTO.id" +
            //             " order by NAME";
            string sql = "EXEC [USP_LOAD_TAX]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTaxRelatedtoGrid()
        {
            string sql = "SELECT ID,NAME FROM dbo.M_TAX_RELATEDTO";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindRegionTax()
        {
            string sql = "SELECT REGIONTAXID,REGIONTAXNAME FROM M_REGIONTAX";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTransporter()
        {
            string sql = "SELECT ID,NAME FROM M_TPU_TRANSPORTER ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable taxExceptionrdb()
        {
            string sql = "  select  'T' as UTID   , 'TPU' as UTNAME,'0' as Lable from P_APPMASTER" +
                           "   union " +
                           "  select  'V' as UTID   , 'VENDOR' as UTNAME,'0' as Lable from P_APPMASTER" +
                           "  union" +
                           "  select 'F' as  UTID ,'Factory' as UTNAME,'0' as Lable from P_APPMASTER" +
                           " union" +
                           " SELECT 'TRANS' AS  UTID ,'TRANSPORTER' AS UTNAME,'0' AS LABLE FROM P_APPMASTER" +
                           " UNION" +
                           " select UTID,UTNAME,DISTRIBUTIONLEVEL as label from M_USERTYPE" +
                           " where PREDEFINE='N' and DISTRIBUTIONCHANNEL='Y'" +
                           " order by Lable";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStateGrid()
        {
            string sql = "SELECT [State_ID],[State_Name]  FROM [M_REGION] order by [State_Name]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        //public DataTable BindStateGrid()
        //{
        //    string sql = "SELECT [State_ID],[State_Name]  FROM [M_REGION] order by [State_Name]";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public DataTable BindStateper(string taxId)
        {
            string sql = " select * from [M_TAX_STATE] inner join [M_REGION] on [M_REGION].State_ID=[M_TAX_STATE].state_id" +
                         " where taxid='" + taxId + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTaxMasterGridEdit(string ID)
        {
            //string sql = "SELECT NAME,PERCENTAGE,ACTIVE FROM M_TAX WHERE ID='" + ID + "'";
            string sql = " SELECT A.NAME,A.DESCRIPTION,A.PERCENTAGE,A.ACTIVE,B.MENUID,C.PageName,A.RELATEDTO,A.APPLICABLETO,CONVERT(VARCHAR(10),CAST(A.EFFECTIVEFROM AS DATE),103) AS EFFECTIVEFROM,A.CODE,A.ACCCODE   FROM M_TAX AS A " +
                         " LEFT OUTER JOIN M_TAX_MENU_MAPPING B ON A.ID = B.TAXID " +
                         " LEFT OUTER JOIN tblMenuList1 C  ON B.MENUID = C.ID" +
                         " WHERE A.ID='" + ID + "'";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable BindPageName()
        {

            string sql = "SELECT ID,DESCRIPTION FROM tblMenuList1 WHERE PageURL IS NOT NULL AND TAG='T' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindTargetPageName(string id)
        //{

        //    string sql = "SELECT ID,PageName FROM tblMenuList1 WHERE PageURL IS NOT NULL AND TAG='T' ORDER BY PageName where ID='"+id+"'";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public int SaveTaxMaster(string ID, string Name, string Percentage, string Mode, string reltedid, string applicableid, string active, string Code,
                                string effectivefrom, string ACCCODE, string ACCGRPNAME, string DESCRIPTION, string IP_ADDRESS)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            string GUID = Convert.ToString(Guid.NewGuid()).ToUpper();

            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {

                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_TAX where NAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_TAX where NAME='" + Name + "' and  ID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }


                }


                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        sqlstr = "  insert into M_TAX(id,[NAME],[PERCENTAGE],[APPLICABLETO],[RELATEDTO],[ACTIVE],[CODE],[EFFECTIVEFROM],[ACCCODE],[ACCGRPNAME],DESCRIPTION,IP_ADDRESS,DTOC)" +
                                 " values('" + GUID + "','" + Name + "','" + Percentage + "', '" + applicableid + "','" + reltedid + "'," +
                                 "'" + active + "','" + Code + "',CONVERT(datetime,'" + effectivefrom + "',103),'" + ACCCODE + "','" + ACCGRPNAME + "','" + DESCRIPTION + "','" + IP_ADDRESS + "',GETDATE())";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_TAX set NAME='" + Name + "',PERCENTAGE='" + Percentage + "',RELATEDTO='" + reltedid + "',APPLICABLETO='" + applicableid + "'," +
                                    " ACTIVE='" + active + "',CODE='" + Code + "',EFFECTIVEFROM = CONVERT(datetime,'" + effectivefrom + "',103) ," +
                                    " ACCCODE='" + ACCCODE + "',ACCGRPNAME='" + ACCGRPNAME + "',DESCRIPTION='" + DESCRIPTION + "',IP_ADDRESS='" + IP_ADDRESS + "',DTOM=GETDATE() where ID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
                    }


                    //if (Mode == "A")
                    //{
                    //    sqlstr = "DELETE FROM M_TAX_MENUMAP WHERE TAXID='" + GUID + "'";
                    //    result = db.HandleData(sqlstr);

                    //    for (int counter = 0; counter < lst.Items.Count; counter++)
                    //    {
                    //        sqlstr = " insert into M_TAX_MENUMAP([TAXID],[MENUID])" +
                    //                 " values('" + GUID + "'," + lst.Items[counter].Value + ")";
                    //        result = db.HandleData(sqlstr);
                    //    }
                    //}

                    //else 
                    //{
                    //    sqlstr = "DELETE FROM M_TAX_MENUMAP WHERE TAXID='" + ID + "'";
                    //    result = db.HandleData(sqlstr);

                    //    for (int counter = 0; counter < lst.Items.Count; counter++)
                    //    {
                    //        sqlstr = " insert into M_TAX_MENUMAP([TAXID],[MENUID])" +
                    //                 " values('" + ID + "'," + lst.Items[counter].Value + ")";
                    //        result = db.HandleData(sqlstr);
                    //    }
                    //}

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
        public int DeleteTAXMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC [SP_TAXSHEET_DELETE]'" + ID + "'";
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

                sqlstr = "Delete from M_TAX_MENU_MAPPING WHERE TAXID = '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }

        public DataTable BindPageNamebyid(string id)
        {

            string sql = "SELECT TAXID,MENUID FROM M_TAX_MENU_MAPPING WHERE TAXID='" + id + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveUserTaxMapping(int RID, string name, string Mode, string MID)
        {
            int result = 0;
            string sqlstr;
            string userid;

            try
            {
                if (Mode == "A")
                {
                    string sql = "SELECT ID FROM M_TAX where NAME='" + name + "' ";
                    DataTable dt = new DataTable();
                    dt = db.GetData(sql);
                    userid = dt.Rows[0]["ID"].ToString();

                    sqlstr = "insert into M_TAX_MENU_MAPPING([TAXID],[MENUID])" +
                      " values('" + userid + "'," + RID + ")";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = "insert into  M_TAX_MENU_MAPPING([TAXID],[MENUID])" +
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

        public int SaveStatePer(string Taxid, int stateId, decimal Percentage, string Mode)
        {
            int result = 0;
            string sqlstr;
            try
            {
                if (Mode == "A")
                {

                    sqlstr = "insert into M_TAX_STATE([TAXID],[STATE_ID],[PERCENTAGE])" +
                      " values('" + Taxid + "'," + stateId + ",'" + Percentage + "')";
                    result = db.HandleData(sqlstr);
                }
                //else
                //{
                //    sqlstr = "insert into  M_TAX_MENU_MAPPING([TAXID],[MENUID])" +
                //     " values('" + MID + "'," + RID + ")";
                //    result = db.HandleData(sqlstr);
                //}
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public void DeleteStatebyid(string id)
        {
            string sql = "DELETE FROM [M_TAX_STATE] WHERE taxid='" + id + "'";
            db.HandleData(sql);

        }

        public int DeleteExcptnbyid(string id, string Utid)
        {
            string sql = string.Empty;
            int ProductCode = 0;
            try
            {
                sql = "EXEC sp_Tax_Exp_Dlt '" + Utid + "','" + id + "'";
                ProductCode = db.HandleData(sql);
                ProductCode = 0;
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return ProductCode;
        }

        //string SqlVid = "";
        //string sql = "";
        //if (Utid == "T" || Utid == "F")
        //{
        //    SqlVid = "select CONVERT(varchar(10), VENDORID) as VENDORID from M_TPU_VENDOR where TAG='" + Utid + "'";
        //}
        //else
        //{
        //    SqlVid = "select CUSTOMERID as VENDORID from M_CUSTOMER where CUSTYPE_ID= '" + Utid + "'";
        //}
        ////SqlVid = (string)db.GetSingleValue(SqlVid);
        //DataTable dt = db.GetData(SqlVid);

        //if (SqlVid != "")
        //{
        //     sql = "DELETE FROM [M_TAX_EXCEPTION] WHERE taxid='" + id + "' and VENDORID='" + SqlVid + "'";
        //}
        //db.HandleData(sql);        

        public DataTable BindEcxptnItem(string Utid)
        {
            string sql = "";
            DataTable dt = new DataTable();
            if (Utid == "T" || Utid == "F")
            {
                sql = "select VENDORID,VENDORNAME from M_TPU_VENDOR where TAG='" + Utid + "' AND SUPLIEDITEM='FG' ORDER BY VENDORNAME";

            }
            else if (Utid == "V")
            {
                sql = "select VENDORID,VENDORNAME from M_TPU_VENDOR where  SUPLIEDITEM<>'FG' ORDER BY VENDORNAME";
            }
            else if (Utid == "TRANS")
            {
                sql = "SELECT ID AS VENDORID,NAME AS VENDORNAME FROM M_TPU_TRANSPORTER ORDER BY NAME ";
            }
            else
            {
                sql = "select CUSTOMERID as VENDORID,CUSTOMERNAME as VENDORNAME from M_CUSTOMER where CUSTYPE_ID= '" + Utid + "'";
            }

            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindEcxptnIGridTF(string Utid, string TaxId)
        {
            string sql = "";
            DataTable dt = new DataTable();
            if (Utid == "T" || Utid == "F")
            {
                sql = " SELECT T.TAXID,T.VENDORID,T.PERCENTAGE,V.VENDORNAME,T.ORDERTYPEID,T.ORDERTYPENAME FROM M_TAX_EXCEPTION T INNER JOIN M_TPU_VENDOR V" +
                      " ON T.VENDORID=V.VENDORID WHERE" +
                      " V.TAG='" + Utid + "'" +
                      " AND " +
                      " TAXID='" + TaxId + "'";
            }
            else if (Utid == "V")
            {
                sql = " SELECT T.TAXID,T.VENDORID,T.PERCENTAGE,V.VENDORNAME,T.ORDERTYPEID,T.ORDERTYPENAME FROM M_TAX_EXCEPTION T INNER JOIN M_TPU_VENDOR V" +
                      " ON T.VENDORID=V.VENDORID WHERE" +
                      " TAXID='" + TaxId + "'";
            }
            else if (Utid == "TRANS")
            {
                sql = " SELECT DISTINCT [TAXID],[VENDORID],[PERCENTAGE],ORDERTYPEID,ORDERTYPENAME,B.NAME AS VENDORNAME FROM [M_TAX_EXCEPTION] A  INNER JOIN M_TPU_TRANSPORTER B ON " +
                        " A.[VENDORID]=CONVERT(VARCHAR(50), B.ID)  where A. TAXID='" + TaxId + "'";
            }
            else
            {
                sql = " SELECT  [TAXID],[M_TAX_EXCEPTION].[VENDORID],[M_TAX_EXCEPTION].[PERCENTAGE],CUSTOMERNAME as VENDORNAME,M_TAX_EXCEPTION.ORDERTYPEID,M_TAX_EXCEPTION.ORDERTYPENAME" +
                      " FROM [M_TAX_EXCEPTION]" +
                      " inner join [M_CUSTOMER] on [M_CUSTOMER].CUSTOMERID =[M_TAX_EXCEPTION].VENDORID where CUSTYPE_ID= '" + Utid + "' and TAXID='" + TaxId + "' ";
            }
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindEcxptnIGridCust()
        //{
        //    string sql = "";
        //    DataTable dt = new DataTable();
        //    sql = " SELECT  [TAXID],[M_TAX_EXCEPTION].[VENDORID],[PERCENTAGE],VENDORNAME" +
        //          " FROM [M_TAX_EXCEPTION]" +
        //          " inner join [M_TPU_VENDOR] on [M_TAX_EXCEPTION].VENDORID=CONVERT(varchar(10), [M_TPU_VENDOR].VENDORID)  ";

        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public int SaveExcetion(string Taxid, string vID, decimal Percentage, string Mode, string ORDERTYPEID, string ORDERTYPENAME)
        {
            int result = 0;
            string sqlstr;
            try
            {
                if (Mode == "A")
                {
                    sqlstr = "DELETE FROM M_TAX_EXCEPTION WHERE TAXID='" + Taxid + "' AND VENDORID='" + vID + "' AND ORDERTYPEID = '" + ORDERTYPEID + "'";
                    db.HandleData(sqlstr);

                    sqlstr = "insert into M_TAX_EXCEPTION([TAXID],[VENDORID],[PERCENTAGE],[ORDERTYPEID ],[ORDERTYPENAME])" +
                      " values('" + Taxid + "','" + vID + "','" + Percentage + "','" + ORDERTYPEID + "','" + ORDERTYPENAME + "')";
                    result = db.HandleData(sqlstr);
                }
                //else
                //{
                //    sqlstr = "insert into  M_TAX_MENU_MAPPING([TAXID],[MENUID])" +
                //     " values('" + MID + "'," + RID + ")";
                //    result = db.HandleData(sqlstr);
                //}
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;

        }
        public DataTable BindDivision()
        {
            string sql = "SELECT DIVID, DIVNAME, DIVCODE FROM M_DIVISION ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindMaterial()
        {
            string sql = "SELECT ID,ITEMDESC FROM M_SUPLIEDITEM ORDER BY ITEMDESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSubMaterial(string ID)
        {
            string sql = "SELECT SUBTYPEID,SUBITEMDESC FROM M_PRIMARY_SUB_ITEM_TYPE WHERE PRIMARYITEMTYPEID='" + ID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCategory()
        {
            string sql = "SELECT CATID, CATNAME, CATCODE FROM M_CATEGORY ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FetchProductDetails(string DivID, string CategoryID)
        {
            DataTable dt = new DataTable();

            string sql = " SELECT DISTINCT  P.ID,ISNULL(P.PRODUCTALIAS,NAME) AS NAME FROM M_PRODUCT  p " +
                        " INNER JOIN M_PRODUCT_TPU_MAP  MPTA ON MPTA.PRODUCTID=P.ID" +
                        " WHERE DIVID= '" + DivID + "' AND CATID='" + CategoryID + "' ";

            //string sql = "sp_FetchItemDeatails '" + CategoryID + "' ,  '" + DivID + "'  ";

            dt = db.GetData(sql);
            return dt;
        }

        public int SaveProductMapping(string Taxid, string PId, decimal Percentage, string PName, string Vid, string Mode)
        {
            int result = 0;
            string sqlstr;
            try
            {
                if (Mode == "A")
                {

                    sqlstr = "insert into [M_TAX_PRODUCT_MAPPING]([TAXID],[PRODUCT_ID],[PERCENTAGE],[PRODUCT_NAME],[VENDORID])" +
                      " values('" + Taxid + "','" + PId + "','" + Percentage + "','" + PName + "','" + Vid + "')";
                    result = db.HandleData(sqlstr);
                }
                //else
                //{
                //    sqlstr = "insert into  M_TAX_MENU_MAPPING([TAXID],[MENUID])" +
                //     " values('" + MID + "'," + RID + ")";
                //    result = db.HandleData(sqlstr);
                //}
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public void DeleteProductbyid(string ID, string VendorID)
        {
            string sql = "DELETE FROM [M_TAX_PRODUCT_MAPPING] WHERE taxid='" + ID + "' AND VENDORID='" + VendorID + "'";
            db.HandleData(sql);

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
        public int SaveAccInfo(string Name, string AccGroupCode, string Finyear, string accid, string xml, string taxid, string Modedelete)
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

        public string BindTaxid(string NAME)
        {
            //string ID = string.Empty;
            string sql = "SELECT ID FROM M_TAX where NAME='" + NAME + "'";
            string id = string.Empty;

            id = (string)db.GetSingleValue(sql);
            return id;
        }

        public int SaveSlabWiseTaxMapping(string taxid, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [USP_SLAVE_WISE_TAX_MAPPING_INSERT]'" + taxid + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }
        public DataTable EditSlabWiseTaxMapping(string taxid)
        {
            string strsql = string.Empty;

            strsql = "SELECT TAXID,TAXNAME,TYPEID,CASE WHEN TYPEID='-1' THEN 'TRANSPORTER' ELSE B.ITEM_Name END AS TYPENAME, ISNULL(FROMAMOUNT, '0.00') AS FROMAMOUNT ,ISNULL(TOAMOUNT,'0.00') AS TOAMOUNT ,ISNULL(VALUE,'0.00') AS VALUE," +
                     " ISNULL(PERCENTAGE,'0.00') AS PERCENTAGE,CONVERT(VARCHAR(20),CAST(FROMDATE AS DATE),103) AS FROMDATE,CONVERT(VARCHAR(20),CAST(TODATE AS DATE),103) AS TODATE,ADDITIONALINFO,LEDGERID,LEDGERNAME FROM M_SLAVEWISE_TAX_MAPPING A LEFT JOIN M_SUPLIEDITEM B ON A.TYPEID=B.ID " +
                     " WHERE  TAXID='" + taxid + "' ";
            DataTable dt = db.GetData(strsql);
            return dt;
        }
        public DataTable BindOrderType()
        {
            string sql = " SELECT BSID,BSNAME FROM M_BUSINESSSEGMENT WHERE BSID='2E96A0A4-6256-472C-BE4F-C59599C948B0'" +
                         " UNION" +
                         " SELECT '9A555D40-5E12-4F5C-8EE0-E085B5BAB169','GENERAL' ORDER BY BSNAME DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategoryDetails(string TaxID)
        {
            string sql = "EXEC USP_CATEGORYDETAILS '" + TaxID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVendorDetails(string TaxID, string Type)
        {
            string sql = "EXEC USP_VENDORGROUPDETAILS '" + TaxID + "','" + Type + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCategoryWiseTaxMapping(string taxid, string xml, string FROMDATE, string TODATE)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [USP_TAX_CATEGORY_MAPPING]'" + taxid + "','" + xml + "','" + FROMDATE + "','" + TODATE + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public int SaveVendorWiseTaxMapping(string taxid, string Type, string xml)
        {
            string strsql = string.Empty;
            int Result = 0;
            strsql = "EXEC [USP_TAX_VENDORGROUP_MAPPING]'" + taxid + "','" + Type + "','" + xml + "'";
            Result = db.HandleData(strsql);
            return Result;
        }

        public DataTable BindCategory_SuppliedItem()
        {
            string sql = "SELECT ID AS CATID,ITEM_NAME AS CATNAME FROM M_SUPLIEDITEM WHERE ISSERVICE='Y' ORDER BY ITEM_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int UpdateLdomTaxMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE M_TAX SET DTOM=GETDATE() WHERE ID='" + ID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }
        public DataTable BindAccountSundryCreditorsGroup()
        {
            string sql = "EXEC USP_ACC_FILL_SUNDRY_CREDITORS_GROUP";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //Added by D.Mondal Dated 27/09/2018
        public DataTable BindTaxSetting(string TaxID)
        {

            string sql = "SELECT TOP 1 SingleTracsAmt,MultiTransAmt FROM TDSSetting WHERE TAXID='" + TaxID + "' ";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public int SaveTDSSetting(string TaxID, decimal STAmt, decimal MTAmt, string IP_ADDRESS, string UserID)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                sqlstr = "IF EXISTS(SELECT COUNT(1) FROM  TDSSetting WHERE TaxID='" + TaxID + "') BEGIN " +
                   " UPDATE TDSSetting SET STATUS='N' WHERE TaxID='" + TaxID + "' " +
                   " INSERT INTO TDSSetting([TaxID],[SingleTracsAmt],[MultiTransAmt],[CBU],[IP_ADDRESS],[DTOC],[STATUS])" +
                        " VALUES('" + TaxID + "','" + STAmt + "','" + MTAmt + "','" + UserID + "','" + IP_ADDRESS + "',GETDATE(),'Y') END ELSE BEGIN" +
                   " INSERT INTO TDSSetting([TaxID],[SingleTracsAmt],[MultiTransAmt],[CBU],[IP_ADDRESS],[DTOC],[STATUS])" +
                        " VALUES('" + TaxID + "','" + STAmt + "','" + MTAmt + "','" + UserID + "','" + IP_ADDRESS + "',GETDATE(),'Y') END ";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }

        public DataTable BindTax_Name()
        {
            string sql = "Select ID,Name from M_TAX where name like '%TDS%' AND ID NOT IN (SELECT TaxID FROM TDSSetting WHERE STATUS='Y')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTax_Name_ForUpdate()
        {
            string sql = "Select ID,Name from M_TAX where name like '%TDS%'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable LoadTaxNameSetting()
        {

            string sql = "SELECT T.TaxID,M.NAME,T.SingleTracsAmt,T.MultiTransAmt FROM TDSSetting AS T " +
                        " LEFT OUTER JOIN M_TAX AS M ON T.TaxID = M.ID WHERE T.STATUS = 'Y' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable DeleteTax_Name()
        {
            string sql = "Select ID,Name from M_TAX where name like '%TDS%'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable LoadSelectedTax(string TAXID)
        {

            string sql = "SELECT T.TaxID,M.NAME,T.SingleTracsAmt,T.MultiTransAmt FROM TDSSetting AS T " +
                        " LEFT OUTER JOIN M_TAX AS M ON T.TaxID = M.ID WHERE T.STATUS = 'Y' AND T.TAXID='" + TAXID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeletTDSSetting(string TaxID, string IP_ADDRESS, string UserID)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                sqlstr = "UPDATE TDSSetting SET STATUS='N',[IP_ADDRESS]='" + IP_ADDRESS + "' , [DTOC] = GETDATE()  WHERE TaxID='" + TaxID + "' ";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }

        #region LoadListOfDateSettings
        public DataTable LoadListOfDateSettings(string FINYEAR)
        {

            string sql = "EXEC [USP_LOADLISTOF_DATE_SETTINGS] '" + FINYEAR + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveDateSetting(string AllowDate, string USERID, string DEPOTID, string FINYEAR)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                //sqlstr = "IF NOT EXISTS(SELECT 1 FROM  tblBackEntryDate WHERE AllowDate= CONVERT(DATETIME, '" +  AllowDate + "', 103)) BEGIN " +
                //   " INSERT INTO tblBackEntryDate([AllowDate])" +
                //        " VALUES(CONVERT(DATETIME, '" + AllowDate + "', 103)) END ";
                sqlstr = "EXEC [USP_BACKDATE_ENTRY_SETTING_INSERT]'" + AllowDate + "','" + USERID + "','" + DEPOTID + "','" + FINYEAR + "'";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }
        public int SaveDateSetting(string AllowDate, string ToDate, string USERID, string DEPOTID, string FINYEAR)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                sqlstr = "EXEC [USP_BACKDATE_ENTRY_SETTING_INSERT]'" + AllowDate + "','" + USERID + "','" + DEPOTID + "','" + FINYEAR + "','" + ToDate + "'";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }

        public DataTable BindAccountUser()
        {
            string sql = "EXEC [USP_LOAD_ACCOUNT_USER]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDEPOTBYUSER(string DEPOTID)
        {
            string sql = "EXEC [USP_DEPOT_SELECTINDEXCHANGE_ACCOUNT_USER]'" + DEPOTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeletDateSetting(string ID)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {

                sqlstr = "DELETE tblBackEntryDate WHERE ID='" + ID + "' ";
                result = db.HandleData(sqlstr);

            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path : Error");
            }

            return result;

        }
        #endregion
    }
}
