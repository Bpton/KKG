using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;

namespace BAL
{
    public class ClsQualityControl
    {
        DBUtils db = new DBUtils();

        DataTable dtQCRecord = new DataTable();  // Datatable for holding QC record
        DataTable dtProductionRecord = new DataTable();  // Datatable for holding Production record for first time when production no select from dropdown
        DataTable dtPODepoQty = new DataTable();
        DataTable dtDespatchQtyDetails = new DataTable();  // Datatable for holding current qty status
        DataTable dttemprecord = new DataTable();

        public DataTable BindTPU(string UserID)
        {
            string sql = " SELECT * FROM (SELECT '-1' AS VENDORID,'TPU' AS VENDORNAME,'-1' AS SEQUENCE UNION select VENDORID, VENDORNAME,'-3' AS SEQUENCE from M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "')   AND TAG ='T' " +
                    " UNION  SELECT '-3' AS VENDORID,'FACTORY' AS VENDORNAME,'-5' AS SEQUENCE UNION   select VENDORID, VENDORNAME,'-7' AS SEQUENCE from M_TPU_VENDOR  where VENDORID in (select TPUID from M_TPU_USER_MAPPING where USERID ='" + UserID + "' ) AND TAG ='F') F6 ORDER BY  SEQUENCE ";
               
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable LoadProduct(string TPUID)/*ONLY THOSE PRODUCTS ARE COME WHOSE REMANING QTY ARE GREATER THAN ZERO*/
        {
            string FINYEAR = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            string DEPOTID = HttpContext.Current.Session["DEPOTID"].ToString().Trim();
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "USP_BIND_PRODUCT_FOR_QC '"+ TPUID + "','"+ FINYEAR + "','"+ DEPOTID + "'";
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public DataTable LoadPurchaseOrder(string PUID,string prodid) /*NEW CREATE BY P.BASU FOR GETTING PRODUCTION NO 02122020*/
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "USP_GET_PRODUCTION_NO_KKG '"+ PUID + "','"+ prodid + "'";
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public DataTable GetPU_QtyCombo(string ProductID, string VendorID, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC sp_COMBO_QC_QTY_DETAILS '" + ProductID + "','" + VendorID + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable GetPU_QtyCombo(string ProductID, string VendorID, string Finyear,string type)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC sp_COMBO_QC_QTY_DETAILS '" + ProductID + "','" + VendorID + "','" + Finyear + "','"+ type + "'";
            dt = db.GetData(sql);
            return dt;

        }

        public DataTable BindReason(string MenuID)
        {
            string sql = " SELECT DISTINCT A.ID,A.NAME AS DESCRIPTION FROM M_REASON A" +
                         " INNER JOIN M_REASON_MENU_MAPPING B ON" +
                         " A.ID=B.REASONID" +
                         " WHERE A.ISAPPROVED = 'Y' AND A.ISDELETED = 'N' AND B.MENUID='" + MenuID + "' ORDER BY DESCRIPTION";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPO(string tpuid)
        {
            string sql = " SELECT  PONO,POID  FROM ( SELECT DISTINCT PONO,POID FROM T_TPU_POHEADER WHERE PONO IN(SELECT DISTINCT PONO FROM T_TPU_PRODUCTION_DETAILS WHERE PUID IN(SELECT DISTINCT ID FROM [T_TPU_PRODUCTION_HEADER] WHERE TPUID='" + tpuid + "')) " 
                           + " AND CLOSEDDATE IS NULL " 
                           + " UNION " 
                           +" SELECT DISTINCT PONO,POID FROM T_FACTORY_POHEADER WHERE PONO IN(SELECT DISTINCT PONO FROM T_TPU_PRODUCTION_DETAILS WHERE PUID IN(SELECT DISTINCT ID FROM [T_TPU_PRODUCTION_HEADER] WHERE TPUID='" + tpuid + "')) " 
                           + " AND CLOSEDDATE IS NULL "+  ")F6 order by PONO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        
        public DataTable BindMaxProductionUpdateDate(string puid)
        {
            
            string sql = " SELECT CONVERT(VARCHAR(10),CAST(MAX(PUDATE) AS DATE),103) AS SHOWPUDATE,CONVERT(VARCHAR(8),MAX(PUDATE),112) AS PUDATE FROM T_TPU_PRODUCTION_DETAILS WHERE PUID IN(" + puid + ")";
            DataTable PUDATE = db.GetData(sql);
            return PUDATE;
        }

        public DataTable BindProductionNo(string poID)
        {
            string sql = " SELECT DISTINCT ID,PUNO" +
                         " FROM T_TPU_PRODUCTION_HEADER AS A INNER JOIN" +
                         " T_TPU_PRODUCTION_DETAILS AS B ON A.ID = B.PUID" +
                         " WHERE B.POID='" + poID + "'" +
                         " AND A.CLOSEDDATE IS NULL" +
                         " ORDER BY PUNO DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable GenerateQCNo()
        {
            DataTable dt = new DataTable();
            string sql = "exec [sp_TPU_QUALITYCONTROLNOGENERATE]";
            dt = db.GetData(sql); ;

            return dt;
        }

        public DataTable BindQualityControl(string fromdate, string todate, string UserID, string finyear, string TPUID)/*FOR BIND QUALITY CONTROL BY P.BASU*/
        {
            string sql = "USP_BIND_QUALITY_CONTROL_KKG '"+ fromdate + "','"+todate+"','"+UserID+"','"+finyear+"','"+TPUID+"'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindQCProduct()
        {
            dtQCRecord.Columns.Add("GUID");
            dtQCRecord.Columns.Add("POID");
            dtQCRecord.Columns.Add("PONO");
            dtQCRecord.Columns.Add("PUNO");
            dtQCRecord.Columns.Add("PUID");
            dtQCRecord.Columns.Add("PRODUCTID");
            dtQCRecord.Columns.Add("PRODUCTNAME");
            dtQCRecord.Columns.Add("BATCHNO");
            dtQCRecord.Columns.Add("PACKINGSIZEID");
            dtQCRecord.Columns.Add("PACKINGSIZENAME");
            dtQCRecord.Columns.Add("PRODUCTIONQTY");
            dtQCRecord.Columns.Add("ALREADYQCQTY");
            dtQCRecord.Columns.Add("CURRENTQCQTY");
            dtQCRecord.Columns.Add("REJECTEDQCQTY");
            dtQCRecord.Columns.Add("REMAININGQCQTY");
            dtQCRecord.Columns.Add("TOTALQCQTY");
            dtQCRecord.Columns.Add("REASONID");
            dtQCRecord.Columns.Add("REASONNAME");

            HttpContext.Current.Session["ENTERQCQTY"] = dtQCRecord;
            return dtQCRecord;
        }


        public DataTable BindQCProductBasedOnQC(string qcID)
        {
            string sql = " SELECT NEWID() as GUID,P.POID,P.PONO,P.PUID AS PUID,P.PUNO AS PUNO,P.PRODUCTID,P.PRODUCTNAME,P.BATCHNO,P.PACKINGSIZEID,P.PACKINGSIZENAME," +
                         " Q.FILEUPLOADTAG AS FILEUPLOADTAG,ISNULL(Q.TOTALACCEPTEDCASE,0) AS TOTALACCEPTEDCASE,ISNULL(Q.TOTALREJECTEDCASE,0) AS TOTALREJECTEDCASE,Q.REMARKS, " +
                         " ISNULL(P.PRODUCTIONQTY,0) AS PRODUCTIONQTY,ISNULL(P.ALREADYQCQTY,0) AS ALREADYQCQTY,CURRENTQCQTY,ISNULL(P.REJECTEDQCQTY,0) AS REJECTEDQCQTY," +
                         " ISNULL(P.REMAININGQCQTY,0) AS REMAININGQCQTY,(CAST(ALREADYQCQTY AS DECIMAL) + CAST(CURRENTQCQTY AS DECIMAL) + CAST(REJECTEDQCQTY AS DECIMAL)) AS TOTALQCQTY," +
                         " REASONID,REASONNAME,CONVERT(VARCHAR(10),CAST(Q.QCDATE AS DATE),103) AS QCDATE,Q.TPUID,Q.QCNO,ISNULL(Q.QCREFNO,'') AS QCREFNO," +
                         " CASE WHEN ACCEPTEDQAQTY=0 THEN CURRENTQCQTY ELSE ACCEPTEDQAQTY END AS ACCEPTEDQAQTY,REJECTEDQAQTY,REJECTEDQAREASON " +
                         " FROM [T_TPU_QUALITYCONTROL_DETAILS]    AS P " +
                         " INNER JOIN T_TPU_QUALITYCONTROL_HEADER AS Q ON P.QCID = Q.QCID" +
                         " WHERE Q.QCID = '" + qcID + "'" +
                         " ORDER BY P.PRODUCTNAME DESC";
            DataTable dt = new DataTable();
            dtQCRecord = db.GetData(sql);
            HttpContext.Current.Session["ENTERQCQTY"] = dtQCRecord;
            return dtQCRecord;
        }

        public DataTable BindQCUpdate(string poID, string productionID, string qcID, string finyear,string ProductID)/*INLINE TO SP WITH ALREADY REJECTQTY ADD BY P.BASU ON 02-01-2021*/
        {
            dtQCRecord = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
            dtProductionRecord = (DataTable)HttpContext.Current.Session["PRODUCTIONQTY"];

            string sql = "USP_GET_PRODUCTION_QTY_BASED_ON_REJECETEDQTY '"+ poID + "','"+ productionID + "','"+ qcID + "','"+ finyear + "','"+ ProductID + "'";
            
            dtProductionRecord = db.GetData(sql);

            HttpContext.Current.Session["PRODUCTIONQTY"] = dtProductionRecord;

            return dtProductionRecord;
        }   

        public void ResetDataTables()
        {
            HttpContext.Current.Session["PRODUCTDEPOQTY"] = null;
            HttpContext.Current.Session["PRODUCTIONQTY"] = null;
            HttpContext.Current.Session["ENTERQCQTY"] = null;
            dtQCRecord.Clear();
            dtProductionRecord.Clear();
            dtPODepoQty.Clear();
            dtDespatchQtyDetails.Clear();
            
        }

        public DataTable BindPOGrid(string instructionno)
        {
            if (instructionno == "")
            {
                dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];
                return dtPODepoQty;
            }
            else
            {
                dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];
                return dtPODepoQty;
            }
        }

        public int Saveuploadfiles(string cfileid,  string filename)
        {
            int result = 0;
            string sqlstr;
           

            try
            {
                

                    sqlstr = "INSERT INTO [M_QCFiles](Name,FILEID)" +
                      " values('" + filename + "','" + cfileid + "')";
                    result = db.HandleData(sqlstr);
                
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable GetFile(string id)
        {

            string sql = "SELECT FILENAME as filename FROM [T_TPU_QUALITYCONTROL_UPLOADFILES] WHERE QCID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable FileDelete(string id)
        {

            string sql = "DELETE FROM [T_TPU_QUALITYCONTROL_UPLOADFILES] WHERE QCID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable ShowProductDetails(string QCID)
        {
            string sql = " EXEC USP_QC_PRODUCT_DETAILS '" + QCID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string InsertQCDetails(  string hdnvalue, string tpuid, string tpuname, string createdby, string finyear, 
                                        string qcDate, string xml,string fileuploadtag, decimal TotalAcceptedCase,
                                        decimal TotalRejectedCase, string Remarks, string xmlupload, string QCRefNo,string entryFrom)
        {
            string flag = "";
            string qcID = string.Empty;
            string qcNo = string.Empty;

            if (hdnvalue == "")
            {
                flag = "A";
                dtQCRecord = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
                try
                {
                    string sqlprocqc = " EXEC [sp_TPU_QUALITYCONTROLHEADER] '','" + flag + "','" + tpuid + "','" + tpuname + "', " + createdby + "," +
                                       " '" + finyear + "','" + qcDate + "','" + xml + "','" + fileuploadtag + "'," + TotalAcceptedCase + "," +
                                       " " + TotalRejectedCase + ",'" + Remarks + "','" + xmlupload + "','" + QCRefNo + "','"+ entryFrom + "'";
                    DataTable dtQC = db.GetData(sqlprocqc);

                    if (dtQC.Rows.Count > 0)
                    {
                        qcID = dtQC.Rows[0]["QCID"].ToString();
                        qcNo = dtQC.Rows[0]["QCNO"].ToString();
                    }
                    else
                    {
                        qcNo = "";
                    }
                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            else
            {
                dtPODepoQty = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
                flag = "U";
                try
                {
                    string sqlprocqc = " EXEC [sp_TPU_QUALITYCONTROLHEADER] '" + hdnvalue + "','" + flag + "','" + tpuid + "','" + tpuname + "', " + createdby + "," +
                                       " '" + finyear + "','" + qcDate + "','" + xml + "','" + fileuploadtag + "'," + TotalAcceptedCase + "," +
                                       " " + TotalRejectedCase + ",'" + Remarks + "','" + xmlupload + "','" + QCRefNo + "','" + entryFrom + "'";
                    DataTable dtQC = db.GetData(sqlprocqc);

                    if (dtQC.Rows.Count > 0)
                    {
                        qcID = dtQC.Rows[0]["QCID"].ToString();
                        qcNo = dtQC.Rows[0]["QCNO"].ToString();
                    }
                    else
                    {
                        qcNo = "";
                    }
                }
                catch (Exception ex)
                {
                    Convert.ToString(ex);
                }
            }
            return qcNo;
        }

        public decimal CheckRemainingDespatchQy(string ProductID, string BatchNo, string POID, string QCID)
        {
            decimal remainingdespatchqty = 0;
            string sql = "EXEC SP_QA_DESPATCH_QTY_CHECK '" + ProductID + "','" + BatchNo + "','" + POID + "','" + QCID + "'";
            remainingdespatchqty = (decimal)db.GetSingleValue(sql);
            return remainingdespatchqty;
        }

        public DataTable TotalQCRecords(string QCID)
        {
            try
            {
                string sql = "SELECT DISTINCT POID,PRODUCTID,BATCHNO FROM T_TPU_QUALITYCONTROL_DETAILS WHERE QCID='" + QCID + "'";
                DataTable dt = new DataTable();
                dt = db.GetData(sql);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int DeleteQC(string QCID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "EXEC SP_QC_DELETE '" + QCID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public string approveQc(string mode,string qcid)/*for qc approved by p.basu*/
        {
            string approved = string.Empty;
            try
            {

                string FINYEAR = HttpContext.Current.Session["FINYEAR"].ToString();
                string returnSqlValue = string.Empty;
                returnSqlValue = "USP_UPDATE_STATUS_KKG '" + mode + "','" + qcid + "','','','" + FINYEAR + "'";
                DataTable dtQC = db.GetData(returnSqlValue);
                if (dtQC.Rows.Count > 0)
                {
                    approved = dtQC.Rows[0]["Done"].ToString();
                   
                }
                else
                {
                    approved = "";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return approved;
        }
        public string approveQc(string mode, string qcid,string ACCEPTEDQADTLS)/*for qa accepted qty dtls update by Rajeev*/
        {
            string approved = string.Empty;
            try
            {
                string FINYEAR = HttpContext.Current.Session["FINYEAR"].ToString();
                string USERID = HttpContext.Current.Session["USERID"].ToString();

                string returnSqlValue = string.Empty;
                returnSqlValue = "USP_UPDATE_STATUS_KKG '" + mode + "','" + qcid + "','" + ACCEPTEDQADTLS + "','"+ USERID + "','" + FINYEAR + "'";
                DataTable dtQC = db.GetData(returnSqlValue);
                if (dtQC.Rows.Count > 0)
                {
                    approved = dtQC.Rows[0]["Done"].ToString();
                }
                else
                {
                    approved = "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return approved;
        }
    }
}
