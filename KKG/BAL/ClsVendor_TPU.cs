using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsVendor_TPU
    {
        DBUtils db = new DBUtils();

        public DataTable BindTPUGrid()
        {
            string sql = "EXEC BIND_THIREDPARTY_TPU ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPUGridByType(string ID)
        {
            DataTable dt = new DataTable();
            string sql = " SELECT ROW_NUMBER() OVER(ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)) AS SLNO,VENDORID, T.CODE, VENDORNAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, R.State_Name AS STATENAME, " +
                         " DISTRICTNAME, ADDRESS,CITYID1, STATEID1,DISTRICTID1, CITYNAME1, STATENAME1,DISTRICTNAME1, ADDRESS1, PHONENO, MOBILENO,PHONENO1, MOBILENO1, PINZIP,PINZIP1, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO,STNO, BANKACNO,BANKID,GSTNO, " +
                         " BANKNAME, BRANCHID, BRANCHNAME,IFSCCODE, CBU, T.DTOC, LMBU, LDTOM, STATUS,ISTDSDECLARE,ISDELETED,TAG,GRPNAME AS ACCGRPNAME," +
                         " CASE	WHEN ISAPPROVED IS NULL THEN 'In-Active' " +
                         "   		WHEN ISAPPROVED ='' THEN 'In-Active'" +
                         "   		WHEN ISAPPROVED ='N'	THEN 'In-Active' " +
                         " ELSE  'Active' END AS ACTIVE_STATUS, " +
                         " FROM [M_THIREDPARTY_VENDOR] T INNER JOIN M_REGION R ON T.STATEID=R.State_ID " +
                         " WHERE SUPLIEDITEMID='" + ID + "'" +
                         " ORDER BY SUBSTRING(T.CODE,CHARINDEX('-',T.CODE)+1,4)";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTPUMasterById(string Vendorid)
        {
            string sql = "EXEC EDIT_THIREDPARTY_TPU_MASTER '" + Vendorid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindSupliedItem()
        {
            string sql = "SELECT ID,ITEM_NAME FROM M_SUPLIEDITEM ORDER BY ITEM_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindState()
        {
            string sql = "SELECT STATE_ID, STATE_NAME FROM M_REGION ORDER BY STATE_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDistrict(int STATEID)
        {
            string sql = "SELECT DISTRICT_ID, DISTRICT_NAME FROM M_DISTRICT WHERE STATE_ID='" + STATEID + "'  ORDER BY DISTRICT_NAME ";
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
            string sql = "SELECT ID,BANKNAME FROM M_BANKMASTER ORDER BY BANKNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBrachName()
        {
            string sql = "SELECT ID, BRANCH FROM M_BANKBRANCH ORDER BY BRANCH ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDivision()
        {
            string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION ORDER BY DIVNAME  ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDivisionbySuppliedid(string Suppliedid)
        {
            string sql = "SELECT DIVID,DIVNAME FROM M_DIVISION ORDER BY DIVNAME  ";
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
        public int SaveThiredPartyVendorMaster(string ID, string Code, string Name, int Supliedid, string Suplieditem, int stateid, string Statename,
                                    int districtid, string Districtname, int cityid, string Cityname, string Address, int stateid1, string Statename1, int districtid1,
                                    string Districtname1, int cityid1, string Cityname1, string Address1, string Mobile, string Phone, string Mobile1, string Phone1,
                                    string Pin, string Pin1, string Email, string Contractperson, string Cstno, string Vatno, string Tinno, string Panno, string stno,
                                    string Bankacno, string bankid, string Bankname, string Branchid, string Branchname, string ifsccode,
                                    string Mode, string istds, string approve, string Tag, string ACCCODE, string ACCGRPNAME, string tax, string COMPANYTYPEID, string LEDGER_REFERENCEID, string GSTNO, string GSTAPPLICABLE,
                                    string ISTRANSFERTOHO, decimal TDSLIMIT, string ipaddress, string MacAddress, string TPUVendorXml, string TPUGSTXml)
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
                string GUID = Convert.ToString(Guid.NewGuid()).ToUpper();

                #region Add by Rajeev(Check VENDOR Group)
                /*string GroupChk, GroupUpdate, GroupInsert, HeaderInsert, HeaderUpdate;
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
                }*/
                #endregion

                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        sqlstr = "EXEC CRUD_THIREDPARTY_VENDOR '" + GUID + "','" + Code + "','" + Name + "','" + Supliedid + "','" + Suplieditem + "'," +
                                 " '" + cityid + "','" + cityid1 + "','" + stateid + "','" + stateid1 + "',0,'" + districtid1 + "','" + districtid + "','" + Cityname + "'," +
                                 " '" + Cityname1 + "','" + Statename + "','" + Statename1 + "','" + Districtname + "','" + Districtname1 + "','" + Address + "', " +
                                 " '" + Address1 + "','" + Phone + "','" + Mobile + "','" + Phone1 + "','" + Mobile1 + "','" + Pin + "','" + Pin1 + "','" + Email + "', " +
                                 " '" + Contractperson + "','" + Cstno + "','" + Vatno + "','" + Tinno + "','" + Panno + "','" + Bankacno + "','" + bankid + "'," +
                                 " '" + Bankname + "','" + Branchid + "','" + Branchname + "','" + ifsccode + "','" + stno + "','" + istds + "'," +
                                 " '" + HttpContext.Current.Session["UserID"].ToString() + "',0,'" + Mode + "','" + approve + "','N', '" + Tag + "','" + ACCCODE + "', " +
                                 " '" + ACCGRPNAME + "','" + tax + "','" + COMPANYTYPEID + "','" + LEDGER_REFERENCEID + "','" + GSTNO + "','" + GSTAPPLICABLE + "', " +
                                 " '" + ISTRANSFERTOHO + "'," + TDSLIMIT + " ,'" + ipaddress + "','" + MacAddress + "','" + TPUVendorXml + "','" + TPUGSTXml + "'";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "EXEC CRUD_THIREDPARTY_VENDOR '" + ID + "','" + Code + "','" + Name + "','" + Supliedid + "','" + Suplieditem + "'," +
                                 " '" + cityid + "','" + cityid1 + "','" + stateid + "','" + stateid1 + "',0,'" + districtid1 + "','" + districtid + "','" + Cityname + "'," +
                                 " '" + Cityname1 + "','" + Statename + "','" + Statename1 + "','" + Districtname + "','" + Districtname1 + "','" + Address + "', " +
                                 " '" + Address1 + "','" + Phone + "','" + Mobile + "','" + Phone1 + "','" + Mobile1 + "','" + Pin + "','" + Pin1 + "','" + Email + "', " +
                                 " '" + Contractperson + "','" + Cstno + "','" + Vatno + "','" + Tinno + "','" + Panno + "','" + Bankacno + "','" + bankid + "'," +
                                 " '" + Bankname + "','" + Branchid + "','" + Branchname + "','" + ifsccode + "','" + stno + "','" + istds + "'," +
                                 " '" + HttpContext.Current.Session["UserID"].ToString() + "',0,'" + Mode + "','" + approve + "','N', '" + Tag + "','" + ACCCODE + "', " +
                                 " '" + ACCGRPNAME + "','" + tax + "','" + COMPANYTYPEID + "','" + LEDGER_REFERENCEID + "','" + GSTNO + "','" + GSTAPPLICABLE + "', " +
                                 " '" + ISTRANSFERTOHO + "'," + TDSLIMIT + " ,'" + ipaddress + "','" + MacAddress + "','" + TPUVendorXml + "','" + TPUGSTXml + "'";
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
            string sqlstr = string.Empty;
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

        public int SaveProductMapping(string TPUID, string TPUNAME, string TAG, string xml)
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

        public DataTable BindTpu()
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE TAG!='F' AND SUPLIEDITEM='FG' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGroupName()
        {
            string sql = "SELECT GROUP_TYPEID AS GROUPID,GROUP_TYPENAME AS GROUPNAME FROM M_GROUP_TYPE ORDER BY GROUP_TYPENAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindStategst(string StateID)
        {
            string sql = "SELECT STATE_ID, STATE_NAME,'' AS GSTNO,'' AS GSTADDRESS FROM M_REGION WHERE STATE_ID NOT IN('" + StateID + "') ORDER BY STATE_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditSTATEWISEGSTMAPPING(string VENDORID)
        {
            string sql = " SELECT VENDORID,VENDORNAME,State_ID,STATENAME,ISNULL(GSTNO,'') AS GSTNO," +
                         " ISNULL(GSTADDRESS,'') AS GSTADDRESS FROM M_THIREDPARTY_STATEWISE_GST_MAPPING WHERE VENDORID='" + VENDORID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllTpuForGateEntry() /*'Sample issue(Returnable)' ONLY FOR RGP Invoice by P.basu on 15-04-2021*/
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WITH(NOLOCK) ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable SaveGetEntry(string mode, string getEntryId, string vendorId, string vendorName, string poId, string poNo,string billNo, string billlDate, decimal totalValue,string reamrks,string entryDate) /*entry date new add by p.basu*/
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string cbu = HttpContext.Current.Session["USERID"].ToString().Trim();
            string sql = "EXEC[USP_INSERT_UPDATE_GATE_ENTRY]'"+mode+"','"+getEntryId+"','"+vendorId+"','"+vendorName+"','"+ poId + "','"+poNo+"','"+ billNo + "','"+ billlDate + "','"+totalValue+"','"+ reamrks + "','"+ finyear + "','"+ cbu + "','"+ entryDate + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string editGateEntryCheck(string mode,string ID)
        {
            string sql = "USP_CHECK_STATUS '"+mode+"','"+ID+"'";
            string id = string.Empty;
            id = (string)db.GetSingleValue(sql);
            return id;
        }
        public string BillStatusCheck(string mode,string ID,string billno)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "USP_CHECK_STATUS '"+mode+"','"+ID+"','"+billno+"','"+ finyear + "'";
            string id = string.Empty;
            id = (string)db.GetSingleValue(sql);
            return id;
        }
        public DataTable BindGateEntry(string mode,string ID)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "USP_CHECK_STATUS '"+mode+"','"+ID+"','','"+ finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPoFromTpu(string mode,string ID)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "USP_CHECK_STATUS '"+mode+"','"+ID+"','','"+ finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable editGateEntry(string id)
        {
            string sql = "SELECT ENTRYID,ENTRYPREFIX+'/'+ENTRYNO+'/'+ENTRYSUFFIX ENTNUMBER, ENTRYDATE,VENDORID,BILLDATE,VENDORNAME,POID,BILLNO, "+
                         " PONO,ISNULL(TOTOALVALUE, 0) TOTOALVALUE,REMARKS FROM TBL_GATE_ENTRY WITH(NOLOCK) WHERE  ENTRYID='"+ id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable SearchGateEntry(string mode, string fromdate, string toDate, string status)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string sql = "EXEC[USP_SEARCH_GATE_ENTRY] '"+ mode + "','"+ fromdate + "','"+ toDate + "','"+ status + "','"+ finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        /*gate entry outward*/
        public DataTable SaveGaetEntryOutWard(string mode, string getEntryId, string customerId, string customerName, string invId, string invNo, string InvDate, string transporterName, string vechileNo,decimal totalValue, string reamrks)
        {
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string cbu = HttpContext.Current.Session["USERID"].ToString().Trim();
            string sql = "EXEC[USP_INSERT_UPDATE_GATE_ENTRY_OUWARD]'" + mode + "','" + getEntryId + "','" + customerId + "','" + customerName + "','" + invId + "','" + invNo + "','" + InvDate + "','" + transporterName + "','" + vechileNo + "','" + totalValue + "','" + reamrks + "','" + cbu + "','" + finyear + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable editGateEntryOutWard(string id)
        {
            string sql = "SELECT ENTRYID,ENTRYPREFIX+'/'+ENTRYNO+'/'+ENTRYSUFFIX ENTNUMBER, cast(ENTRYDATE as date) ENTRYDATE,CUSTOMERID,CUSTOMERNAME,INVOICEID, "+
                         " INVOICENO,CAST(INVOICEDATE AS DATE) INVOICEDATE,TOTOALVALUE,VECHILENO,TRANSPOTERNAME,ISNULL(TOTOALVALUE, 0) TOTOALVALUE,ISSTATUS "+
                         "  [STATUS],REMARKS FROM TBL_GATE_ENTRY_OUTWARD WITH(NOLOCK)  WHERE ENTRYID ='" + id + "'";
             DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable VendorProductWisePoGrnQty(string fromdate,string todate , string vendorid)
        {
            string sql = "EXEC[USP_RPT_VENDOR_PRODUCT_WISE_PO_VS_GRN_QTY]'" + fromdate + "','" + todate + "','" + vendorid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

    }
}
