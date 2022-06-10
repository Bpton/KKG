using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsQualityAssurance
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
        public DataTable LoadProduct(string tpuid)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT A.ID,A.PRODUCTALIAS AS NAME FROM M_PRODUCT AS A " +
                      " INNER JOIN [M_PRODUCT_TPU_MAP] AS B ON A.ID=B.PRODUCTID " +
                      " WHERE B.VENDORID='" + tpuid + "'" +
                      " AND A.TYPE='FG' " +
                      " ORDER BY A.PRODUCTALIAS";
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public DataTable LoadPurchaseOrder(string PUID, string prodid)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = " SELECT A.PRODUCTION_ORDERID AS POID,(A.PRORDER_PREFIX+'/'+A.PRORDERNUM+'/'+A.PRORDERSUFFIX) AS PONO " +
                      " FROM T_PRODUCTION_ORDER_HEADER AS A" +
                      " INNER JOIN T_PRODUCTION_ORDER_ITEM AS B ON B.PRODUCTION_ORDERID=A.PRODUCTION_ORDERID" +
                      " WHERE A.PRODUCTION_ORDERID='" + PUID + "' and  B.MATERIALID='" + prodid + "'" +
                      " ORDER BY A.PRORDERNUM DESC ";
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
            //sql = "EXEC sp_COMBO_QC_QTY_DETAILS '" + ProductID + "','" + VendorID + "','" + Finyear + "'";
            sql = "EXEC SP_COMBO_QC_QTY_DETAILS_MM '" + ProductID + "','" + VendorID + "','" + Finyear + "'";
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
        public DataTable BindMaxProductionUpdateDate(string puid)
        {            
            string sql = " SELECT CONVERT(VARCHAR(10),CAST(MAX(ENTRY_DATE) AS DATE),103) AS SHOWPUDATE,CONVERT(VARCHAR(8),MAX(ENTRY_DATE),112) AS PUDATE  FROM T_PRODUCTION_ORDER_HEADER WHERE PRODUCTION_ORDERID IN(" + puid + ")";
            DataTable PUDATE = db.GetData(sql);
            return PUDATE;
        }

        public DataTable BindProductionNo(string poID)
        {
            string sql = " SELECT DISTINCT ID,PUNO" +
                         " FROM T_TPU_PRODUCTION_HEADER AS A " +
                         " INNER JOIN T_TPU_PRODUCTION_DETAILS AS B ON A.ID = B.PUID" +
                         " WHERE B.POID='" + poID + "'" +
                         " AND A.CLOSEDDATE IS NULL" +
                         " ORDER BY PUNO DESC";
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
        public DataTable BindQualityControl(string fromdate, string todate, string UserID, string finyear, string TPUID, string CheckerFlag)
        {
            string sql = "EXEC USP_BIND_QUALITY_ASSURANCE_FAC '" + fromdate + "','" + todate + "','" + finyear + "','" + TPUID + "','" + UserID + "','" + CheckerFlag + "'";
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
            string sql = "EXEC USP_BIND_QC_PRODUCTIONWISE '" + qcID + "'";
            DataTable dt = new DataTable();
            dtQCRecord = db.GetData(sql);
            HttpContext.Current.Session["ENTERQCQTY"] = dtQCRecord;
            return dtQCRecord;
        }
        public DataTable BindQCUpdate(string poID, string productionID, string qcID, string finyear, string ProductID)
        {
            dtQCRecord = (DataTable)HttpContext.Current.Session["ENTERQCQTY"];
            dtProductionRecord = (DataTable)HttpContext.Current.Session["PRODUCTIONQTY"];
            string sql = "EXEC USP_BIND_PRODUCTION_DETAILS '" + poID + "','" + productionID + "','" + qcID + "','" + finyear + "','" + ProductID + "'";
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
        public string InsertQCDetails(string hdnvalue, string tpuid, string tpuname, string createdby,
                                      string finyear, string qcDate, string xml, string fileuploadtag,
                                      decimal TotalAcceptedCase, decimal TotalRejectedCase, string Remarks,
                                      string xmlupload, string QCRefNo)
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
                    string sqlprocqc = " EXEC [CRUD_QUALITYCONTROLHEADER_FAC] '','" + flag + "','" + tpuid + "','" + tpuname + "', " + createdby + "," +
                                       " '" + finyear + "','" + qcDate + "','" + xml + "','" + fileuploadtag + "'," + TotalAcceptedCase + "," +
                                       " " + TotalRejectedCase + ",'" + Remarks + "','" + xmlupload + "','" + QCRefNo + "'";
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
                    string sqlprocqc = " EXEC [CRUD_QUALITYCONTROLHEADER_FAC] '" + hdnvalue + "','" + flag + "','" + tpuid + "','" + tpuname + "', " + createdby + "," +
                                       " '" + finyear + "','" + qcDate + "','" + xml + "','" + fileuploadtag + "'," + TotalAcceptedCase + "," +
                                       " " + TotalRejectedCase + ",'" + Remarks + "','" + xmlupload + "','" + QCRefNo + "'";
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
        public int UpdateQA(string QAID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE T_TPU_QUALITYCONTROL_HEADER SET ISVARIFIED='Y', VARIFIEDDATE=GETDATE() WHERE QCID='" + QAID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public int RejectQA(string QAID, string Remarks)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "UPDATE T_TPU_QUALITYCONTROL_HEADER SET ISVARIFIED='R', VARIFIEDDATE=GETDATE(),REMARKS='" + Remarks + "' WHERE QCID='" + QAID + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public string CheckQAVerify(string QAID)
        {
            string Flag = "";
            string sql = "SELECT ISVARIFIED FROM T_TPU_QUALITYCONTROL_HEADER WHERE QCID = '" + QAID + "' ";
            Flag = (string)db.GetSingleValue(sql);
            return Flag;
        }
    }
}
