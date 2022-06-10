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
    public class ClsInstructionSheet
    {
        DBUtils db = new DBUtils();

        DataTable dtPODepoQty = new DataTable();
        DataTable dtDespatchQtyDetails = new DataTable();  // Datatable for holding current qty status

        //***** Modified By Avishek On 04/01/2014 ************************//
        public DataTable BindPO( string TPUID)
        {
            string sql = "";
            string TOUTAG = (string)db.GetSingleValue("SELECT  [TAG] FROM [M_TPU_VENDOR] WHERE  VENDORID='" + TPUID + "'");

            if (TOUTAG.Trim() == "T")
            {

                //sql = "select PONO,POID from [T_TPU_POHEADER] where CONVERT(date,PODATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') order by PONO asc";
                //sql = " SELECT A.POID,A.PONO " +
                //      " FROM T_TPU_POHEADER AS A WITH (NOLOCK) INNER JOIN T_TPU_PODETAILS AS B WITH (NOLOCK)" +
                //      " ON A.POID=B.POID" +
                //      " AND A.TPUID = '" + TPUID + "'" +
                //      " AND B.PRODUCTID='" + ProductID + "'";

                sql = " SELECT A.POID,A.PONO " +
                     " FROM T_TPU_POHEADER AS A "+
                     " AND A.TPUID = '" + TPUID + "'";
                     

            }

            if (TOUTAG.Trim() == "F")
            {

                //sql = "select PONO,POID from [T_Factory_POHEADER] where CONVERT(date,PODATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') order by PONO asc";
                //sql = " SELECT A.POID,A.PONO " +
                //      " FROM T_FACTORY_POHEADER AS A WITH (NOLOCK) INNER JOIN T_FACTORY_PODETAILS AS B WITH (NOLOCK)" +
                //      " ON A.POID=B.POID" +
                //      " AND A.TPUID = '" + TPUID + "'" +
                //      " AND B.PRODUCTID='" + ProductID + "'";


                sql = " SELECT A.POID,A.PONO " +
                      " FROM T_FACTORY_POHEADER AS A" +

                      " AND A.TPUID = '" + TPUID + "'";
                     
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //***************************************************************//
        public DataTable BindMaxQCDate(string poid)
        {
            //string sql = "SELECT CONVERT(VARCHAR(10),CAST(MAX(QCDATE) AS DATE),103) AS QCDATE FROM T_TPU_QUALITYCONTROL_HEADER AS A " +
            //            "INNER JOIN T_TPU_QUALITYCONTROL_DETAILS AS B ON A.QCID = B.QCID WHERE B.POID IN(" + poid + ")";
            string sql = "SELECT CONVERT(VARCHAR(8),MAX(QCDATE),112) AS QCDATE,CONVERT(VARCHAR(10),CAST(MAX(QCDATE) AS DATE),103) AS SHOWQCDATE FROM T_TPU_QUALITYCONTROL_HEADER AS A " +
                                    "INNER JOIN T_TPU_QUALITYCONTROL_DETAILS AS B ON A.QCID = B.QCID WHERE B.POID IN(" + poid + ")";
            DataTable QCDATE = db.GetData(sql);
            return QCDATE;
        }

        public DataTable BindTPU_Transporter(string tpuid)
        {
            string sql = " SELECT ID,NAME" +
                         " FROM M_TPU_TRANSPORTER AS A INNER JOIN" +
                              " M_TRANSPOTER_TPU_MAP AS B ON" +
                              " A.ID=B.TRANSPOTERID" +
                         " WHERE A.ISDELETED = 'N'" +
                         " AND  B.TPUID='" + tpuid + "'" +
                         " ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable PackingListDetails(string AllocationID,string TPUID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_TPU_PACKINGLIST_DETAILS '" + AllocationID + "','" + TPUID + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Productdetails(string POID)
        {
            DataTable dt = new DataTable();
            string sql = "EXEC USP_ALLOCATION_GRID_DETAILS '" + POID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable Productdetails(string FromDt, string DepotID, string ProductID)
        {
            DataTable dt = new DataTable();
            //string sql = "EXEC USP_ALLOCATION_GRID_DETAILS_FAC '" + SALEORDERID + "','" + FINYEAR + "'";
            string sql = "EXEC USP_RPT_STOCK_SALEORDERWISE '" + FromDt + "','" + DepotID + "','" + ProductID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindTPU()
        {
            string sql = "select VENDORID, VENDORNAME from M_TPU_VENDOR order by VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOrderType()
        {
            string sql = "SELECT OrderTYPEID,ORDERTYPENAME FROM M_ORDERTYPE WHERE OrderTYPEID='2E96A0A4-6256-472C-BE4F-C59599C948B0'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCountry()
        {
            string sql = " SELECT COUNTRYID,COUNTRYNAME FROM M_COUNTRY"+
                         " WHERE COUNTRYID NOT IN (SELECT COUNTRYID FROM P_APPMASTER)"+
                         " ORDER BY COUNTRYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindSaleOrder(string CountryID)
        {
            string sql = "EXEC USP_EXPORT_SALEORDER_DETAILS '" + CountryID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        

        public DataTable BindOrderProduct(string SaleOrderID,string VendorID)
        {
            string sql = " SELECT A.PRODUCTID AS ID,A.PRODUCTNAME AS NAME FROM T_SALEORDER_DETAILS AS A "+
                         " INNER JOIN M_PRODUCT_TPU_MAP AS B ON A.PRODUCTID=B.PRODUCTID"+
                         " WHERE A.SALEORDERID='" + SaleOrderID + "' AND B.VENDORID='" + VendorID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindOrderProduct_FAC(string SaleOrderID, string VendorID)
        {
            string sql = " SELECT PRODUCTID=STUFF((SELECT ', '+ CAST(A.PRODUCTID AS VARCHAR(MAX)) " +
                         " FROM T_SALEORDER_DETAILS AS A" +
                         " INNER JOIN M_PRODUCT_TPU_MAP AS B ON A.PRODUCTID=B.PRODUCTID" +
                         " WHERE A.SALEORDERID='" + SaleOrderID + "' AND B.VENDORID='" + VendorID + "'" +
                         " FOR XMl PATH('') "+
						 " ),1,1,'')";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetPO_Qty(string POID, string PRODUCTID, string TOPACKSIZEID, string VendorID)
        {
            decimal RETURNVALUE = 0;

            string TPU = (string)db.GetSingleValue("SELECT TAG FROM M_TPU_VENDOR where VENDORID='" + VendorID + "'");
            string SQL = "";

            if (TPU.Trim() == "T")
            {
                SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "',[PACKINGSIZEID],'" + TOPACKSIZEID
                           + "',qty )),0) from [T_TPU_PODETAILS] WITH (NOLOCK) where [POID]='" + POID + "' and [PRODUCTID]='" + PRODUCTID + "'";
            }

            if (TPU.Trim() == "F")
            {
                SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "',[PACKINGSIZEID],'" + TOPACKSIZEID
                           + "',qty )),0) from [T_FACTORY_PODETAILS] WITH (NOLOCK) where [POID]='" + POID + "' and [PRODUCTID]='" + PRODUCTID + "'";

            }

            //string SQL = "SELECT CAST(ISNULL(SUM(QTY),0) AS DECIMAL(18,2)) FROM dbo.T_TPU_PODETAILS WHERE [POID]='" + POID + "' and [PRODUCTID]='" + PRODUCTID + "'";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);




            return RETURNVALUE;

        }

        public DataTable GetPO_QtyCombo(string VendorID, string Finyear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_COMBO_PO '" + VendorID + "','" + Finyear + "'";
            dt = db.GetData(sql);
            return dt;

        }
        public DataTable GetPO_QtyCombo(string VendorID, string Finyear,string SaleOrderID)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_COMBO_PO_FAC '" + VendorID + "','" + Finyear + "','" + SaleOrderID + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public decimal GetFORECAST_Qty(string Depotid, string ProductID, string PackSize, string Month)
        {
            decimal forecastqty = 0;
            string sql = string.Empty;
            sql = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + ProductID + "',[UOMID],'" + PackSize + "',QTY)),0)" +
                         " FROM [T_SALESFORCAST_DETAILS] A WITH (NOLOCK) INNER JOIN [T_SALESFORCAST_HEADER] B WITH (NOLOCK) ON A.SALESFORCASTID=B.SALESFORCASTID " +
                         " where [BRANCHID]='" + Depotid + "' and [PRODUCTID]='" + ProductID + "' AND B.MONTH=" + Month + "";
            forecastqty = (decimal)db.GetSingleValue(sql);
            return forecastqty;
        }

        public decimal GetQO_Qty(string POID, string PRODUCTID, string TOPACKSIZEID)
        {
            decimal RETURNVALUE = 0;

            string SQL = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "',[PACKINGSIZEID],'" + TOPACKSIZEID + "',CURRENTQCQTY )),0)" +
                         " FROM [T_TPU_QUALITYCONTROL_DETAILS]  WITH (NOLOCK) where [POID]='" + POID + "' and [PRODUCTID]='" + PRODUCTID + "'";
            
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;

        }

        public decimal GetRemaining_Qty(string POID, string PRODUCTID, string TOPACKSIZEID)
        {
            decimal RETURNVALUE = 0;

            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "',[PACKINGSIZEID],'" + TOPACKSIZEID + "',DESPATCHQTY )),0)" +
                         " FROM [T_TPU_INSTRUCTION_DETAILS]  WITH (NOLOCK) WHERE [POID]='" + POID + "' and [PRODUCTID]='" + PRODUCTID + "'";
            
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;

        }

        public decimal GetEditedRemaining_Qty(string POID, string PRODUCTID, string TOPACKSIZEID, string INSTRUCTIONID)
        {
            decimal RETURNVALUE = 0;

            string SQL = " Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "',[PACKINGSIZEID],'" + TOPACKSIZEID + "',DESPATCHQTY )),0)" +
                         " FROM [T_TPU_INSTRUCTION_DETAILS] WITH (NOLOCK) WHERE [POID]='" + POID + "' and [PRODUCTID]='" + PRODUCTID + "' AND [INSTRUCTIONID]<>'" + INSTRUCTIONID + "'";
            
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;

        }

        public int ResetPackingList(string AllocationID,string SaleOrderID,string TPUID)
        {
            int delflag = 0;

            string sqleditedprocdetail = " DELETE FROM T_TPU_PACKINGLIST"+
                                         " WHERE SALEORDERID ='" + SaleOrderID + "'"+
                                         " AND ALLOCATIONID ='" + AllocationID + "'" +
                                         " AND TPUID ='" + TPUID + "'";

            int e = db.HandleData(sqleditedprocdetail);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }

        public string GetStartPosition(string SaleOrderID,  decimal Position)
        {
            string RETURNVALUE = string.Empty;

            string SQL = " IF EXISTS (SELECT 1 FROM T_TPU_PACKINGLIST WHERE SALEORDERID='" + SaleOrderID + "'  AND STARTPOSITION <=" + Position + " AND ENDPOSITION > = " + Position + ") " +
                         " BEGIN"+
                         " 	SELECT CAST(CAST(ISNULL(MAX(ENDPOSITION),0) AS INT) AS VARCHAR(5))FROM T_TPU_PACKINGLIST WHERE SALEORDERID='" + SaleOrderID + "'" +
                         " END"+
                         " ELSE"+
                         " BEGIN"+
                         " 	SELECT 'NA'"+
                         " END";

            RETURNVALUE = (string)db.GetSingleValue(SQL);

            return RETURNVALUE;

        }

        public string GetDespatchStatus(string SaleOrderID, string AllocationID,string TPUID)
        {
            string RETURNVALUE = string.Empty;

            string SQL = " IF EXISTS (SELECT 1 FROM T_TPU_PACKINGLIST WHERE SALEORDERID='" + SaleOrderID + "' " +
                         "				   AND ALLOCATIONID='"+AllocationID+"'" +
                         "				   AND TPUID='"+TPUID+"'" +
                         "				   AND DESPATCHFLAG = 'Y' )" +
                         " BEGIN " +
                         " 		SELECT TOP (1) DESPATCHFLAG FROM T_TPU_PACKINGLIST  WHERE SALEORDERID='" + SaleOrderID + "' " +
                         "										   AND ALLOCATIONID='"+AllocationID+"'" +
                         "										   AND TPUID='" + TPUID + "'" +
                         "										   AND DESPATCHFLAG = 'Y'" +
                         " END" +
                         " ELSE" +
                         " BEGIN" +
                         " SELECT 'NA'" +
                         " END";

            RETURNVALUE = (string)db.GetSingleValue(SQL);

            return RETURNVALUE;

        }

        public DataTable BindSaleOrderID(string AllocationID)
        {

            string sql = " SELECT SALEORDERID FROM T_TPU_INSTRUCTION_DETAILS WHERE INSTRUCTIONID='" + AllocationID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public void ResetDataTables()
        {
            HttpContext.Current.Session["PRODUCTDEPOQTY"] = null;
            HttpContext.Current.Session["DESPATCHQTYDETAILS"] = null;
            dtPODepoQty.Clear();
            dtDespatchQtyDetails.Clear();
        }

        public DataTable BindPOGrid(string instructionno)
        {
            dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];
            return dtPODepoQty;
        }

        public DataTable BindInstruction(string fromdate, string todate, string finyear,string UserID,string TAG)
        {
            string sql = string.Empty;
            if (TAG == "N")
            {
                sql = " SELECT DISTINCT STUFF((SELECT distinct '/ ' + CAST(DEPONAME + '~' AS VARCHAR(30))[text()],COUNT(PRODUCTID) FROM T_TPU_INSTRUCTION_DETAILS WHERE INSTRUCTIONID = H.INSTRUCTIONID GROUP BY DEPONAME " +
                         " FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,'') DEPOTNAME,TPUID,TPUNAME," +
                         " INSTRUCTIONID,CONVERT(VARCHAR(10),CAST(INSTRUCTIONDATE AS DATE),103) AS INSTRUCTIONDATE,INSTRUCTIONNO,ISNULL(TOTALCASEPACK,0) AS TOTALCASEPACK FROM T_TPU_INSTRUCTION_HEADER H WITH (NOLOCK) " +
                         " WHERE CONVERT(date,INSTRUCTIONDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "'" +
                         " AND CREATEDBY = '" + UserID + "' AND ORDERTYPEID='9A555D40-5E12-4F5C-8EE0-E085B5BAB169'" +
                         " ORDER BY INSTRUCTIONDATE DESC";
            }
            else if (TAG == "Y")
            {
                sql = " SELECT DISTINCT STUFF((SELECT distinct '/ ' + CAST(DEPONAME + '~' AS VARCHAR(30))[text()],COUNT(PRODUCTID) FROM T_TPU_INSTRUCTION_DETAILS WHERE INSTRUCTIONID = H.INSTRUCTIONID GROUP BY DEPONAME " +
                         " FOR XML PATH(''), TYPE) .value('.','NVARCHAR(MAX)'),1,2,'') DEPOTNAME,TPUID,TPUNAME," +
                         " INSTRUCTIONID,CONVERT(VARCHAR(10),CAST(INSTRUCTIONDATE AS DATE),103) AS INSTRUCTIONDATE,INSTRUCTIONNO,ISNULL(TOTALCASEPACK,0) AS TOTALCASEPACK FROM T_TPU_INSTRUCTION_HEADER H WITH (NOLOCK) " +
                         " WHERE CONVERT(date,INSTRUCTIONDATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + finyear + "'" +
                         " AND CREATEDBY = '" + UserID + "' AND ORDERTYPEID='2E96A0A4-6256-472C-BE4F-C59599C948B0'" +
                         " ORDER BY INSTRUCTIONDATE DESC";
            }
            
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepo()
        {
            
            string sql = "SELECT '-1' as BRID,'---- MOTHERDEPOT ----' as BRNAME,'0' AS SEQUENCE  UNION  SELECT BRID,BRNAME,'2' AS SEQUENCE   FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'TRUE' UNION SELECT '-3' as BRID,'---- DEPOT ----' as BRNAME ,'3' AS SEQUENCE UNION SELECT BRID,BRNAME,'5' AS SEQUENCE FROM M_BRANCH " +
                        " WHERE  BRANCHTAG = 'D' AND ISMOTHERDEPOT = 'FALSE' ORDER BY SEQUENCE,BRNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindDepotBasedOnUser(string UserID)
        {
            string sql = " SELECT BRID,BRPREFIX AS BRNAME FROM M_BRANCH INNER JOIN M_TPU_USER_MAPPING " +
                         " ON M_BRANCH.BRID=M_TPU_USER_MAPPING.TPUID " +
                         " AND M_TPU_USER_MAPPING.USERID='" + UserID + "'" +
                         //" AND M_BRANCH.BRANCHTAG='D'                " +
                         //" AND M_BRANCH.ISFACTORY = 'N'              " +
                         " ORDER BY BRNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string POID)
        {
            //string sql = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM [T_TPU_PRODUCTION_UPDATE] WHERE PONO='" + PONO + "'";
            string sql = "SELECT DISTINCT PRODUCTID,PRODUCTNAME FROM [T_TPU_PRODUCTION_DETAILS] WITH (NOLOCK) WHERE PUID IN(SELECT ID FROM T_TPU_PRODUCTION_HEADER WITH (NOLOCK)  WHERE POID='" + POID + "') ORDER BY PRODUCTNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "' ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable FetchProductInfo(string PID, string PackSize)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "' ORDER BY PSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindInstructionNO()
        {

            string sql = "SELECT ISNULL(MAX(SUBSTRING(INSTRUCTIONNO,4,6)),0) AS MAXCOUNT FROM [T_TPU_INSTRUCTION_HEADER]";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            return dt;
        }

        public DataTable GenerateINstructionNo()
        {
            DataTable dt = new DataTable();
            string sql = "exec [sp_TPU_INSTRUCTIONNOGENERATE]";
            dt = db.GetData(sql); ;

            return dt;
        }

        public DataTable BindInstructionPrefix()
        {
            string sql = "SELECT INSTRUCTION_PREFIX,INSTRUCTION_SUFFIX FROM P_APPMASTER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            return dt;
        }


        public DataTable BindPRODUCTDEPO()
        {
            dtPODepoQty.Columns.Add("GUID");
            dtPODepoQty.Columns.Add("DEPOID");
            dtPODepoQty.Columns.Add("DEPONAME");
            dtPODepoQty.Columns.Add("POID");
            dtPODepoQty.Columns.Add("PONO");
            dtPODepoQty.Columns.Add("PRODUCTID");
            dtPODepoQty.Columns.Add("PRODUCTNAME");
            dtPODepoQty.Columns.Add("DESPATCHQTY");
            dtPODepoQty.Columns.Add("PACKINGSIZEID");
            dtPODepoQty.Columns.Add("PACKINGSIZENAME");
            dtPODepoQty.Columns.Add("DESPATCHDATE");
            dtPODepoQty.Columns.Add("TRANSPORTERID");
            dtPODepoQty.Columns.Add("TRANSPORTERNAME");

            HttpContext.Current.Session["PRODUCTDEPOQTY"] = dtPODepoQty;
            return dtPODepoQty;
        }


        public DataTable BindDespatchQtyDetails()
        {
            dtDespatchQtyDetails.Columns.Add("POID");
            dtDespatchQtyDetails.Columns.Add("PONO");
            //dtDespatchQtyDetails.Columns.Add("PRODUCTID");
            //dtDespatchQtyDetails.Columns.Add("PRODUCTNAME");
            dtDespatchQtyDetails.Columns.Add("ORDERQTY");
            dtDespatchQtyDetails.Columns.Add("PRODUCTIONQTY");
            dtDespatchQtyDetails.Columns.Add("DESPATCHQTY");
            dtDespatchQtyDetails.Columns.Add("REMAININGQTY");
            //dtDespatchQtyDetails.Columns.Add("PACKINGSIZEID");
            //dtDespatchQtyDetails.Columns.Add("PACKINGSIZENAME");

            HttpContext.Current.Session["DESPATCHQTYDETAILS"] = dtDespatchQtyDetails;
            return dtDespatchQtyDetails;
        }

        public DataTable BIndProductDetailsBasedOnInstruction(string instruction, string finyear)
        {
            string sql = " SELECT NEWID() AS GUID,A.DEPOID,A.DEPONAME,A.PONO,A.POID,A.PRODUCTID,A.PRODUCTNAME,A.DESPATCHQTY," +
                         " B.TPUID,B.TPUNAME,CONVERT(VARCHAR(10),CAST(B.INSTRUCTIONDATE AS DATE),103) AS INSTRUCTIONDATE," +
                         " B.ORDERTYPEID,B.ORDERTYPENAME,A.SALEORDERID,A.SALEORDERNO,B.COUNTRYID," +
                         " A.PACKINGSIZEID,A.PACKINGSIZENAME,CONVERT(VARCHAR(10),CAST(A.DESPATCHDATE AS DATE),103) AS DESPATCHDATE,"+
                         " B.INSTRUCTIONNO,A.TRANSPORTERID,A.TRANSPORTERNAME"+
                         " FROM T_TPU_INSTRUCTION_DETAILS AS A WITH (NOLOCK) INNER JOIN" +
                         " T_TPU_INSTRUCTION_HEADER AS B WITH (NOLOCK) ON A.INSTRUCTIONID = B.INSTRUCTIONID" +
                         " WHERE A.INSTRUCTIONID ='" + instruction + "'";
            DataTable dt = new DataTable();

            dtPODepoQty = db.GetData(sql);

            HttpContext.Current.Session["PRODUCTDEPOQTY"] = dtPODepoQty;
            return dtPODepoQty;
        }

        public string FETCHTAG(string TAG)
        {
            string SQL = "SELECT TAG FROM M_TPU_VENDOR WHERE VENDORID='" + TAG + "'";
            string tag = (string)db.GetSingleValue(SQL);
            return tag;
        }

        public string FETCHPONO(string POID, string TAG)
        {
            string sql = string.Empty;
            if (TAG == "T")
            {
                sql = "SELECT PONO FROM T_TPU_POHEADER  WITH (NOLOCK) WHERE POID='" + POID + "'";
            }
            else
            {
                sql = "SELECT PONO FROM T_Factory_POHEADER  WITH (NOLOCK) WHERE POID='" + POID + "'";
            }
            string pono = (string)db.GetSingleValue(sql);
            return pono;
        }

        public decimal GetPackingSize_OnCall(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;

            string sql = "SELECT PACKSIZE FROM P_APPMASTER";/*CASEPACKSIZEID TO PACKSIZE CHANGE BY P.BASU FOR KKG BECAUSE ALL PRODUCTS ARE IN PCS*/
            string PackSizeTo = (string)db.GetSingleValue(sql);

            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);

            return RETURNVALUE;
        }


        public DataTable BindDEPOWISEQTY(string depoid, string deponame, string pono, string poid, string pid, string pname, decimal despatchqty, string packingsizeid, string packingsizename, string despatchdate, string transporterid, string transportername)
        {
            dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];

            DataRow dr = dtPODepoQty.NewRow();
            dr["GUID"] = Guid.NewGuid();
            dr["DEPOID"] = depoid;
            dr["DEPONAME"] = deponame;
            dr["POID"] = poid;
            dr["PONO"] = pono;
            dr["PRODUCTID"] = pid;
            dr["PRODUCTNAME"] = pname;
            dr["DESPATCHQTY"] = despatchqty;
            dr["PACKINGSIZEID"] = packingsizeid;
            dr["PACKINGSIZENAME"] = packingsizename;
            dr["DESPATCHDATE"] = despatchdate;
            dr["TRANSPORTERID"] = transporterid;
            dr["TRANSPORTERNAME"] = transportername;

            dtPODepoQty.Rows.Add(dr);

            HttpContext.Current.Session["PRODUCTDEPOQTY"] = dtPODepoQty;
            return dtPODepoQty;
        }


        public int InstructionEditedRecordDELETE(string instructionno)
        {
            int delflag = 0;

            string sqleditedprocdetail = "exec [sp_TPU_TotalInstructionDelete] '" + instructionno + "'";

            int e = db.HandleData(sqleditedprocdetail);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else
            {
                delflag = 1;  // delete successfull
            }

            return delflag;
        }


        public int InstructionSheetRecordsDelete(string GUID, string instructionno)
        {
            int delflag = 0;
            dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];

            int i = dtPODepoQty.Rows.Count - 1;

            while (i >= 0)
            {
                if (Convert.ToString(dtPODepoQty.Rows[i]["GUID"]) == GUID)
                {

                    dtPODepoQty.Rows[i].Delete();
                    dtPODepoQty.AcceptChanges();
                    delflag = 1;
                    break;
                }
                i--;
            }

            HttpContext.Current.Session["PRODUCTDEPOQTY"] = dtPODepoQty;
            return delflag;
        }
        public DataTable FetchSaleForeCasteQty(string pid, int month, int year, string ToPacksize)
        {
            int mm = 0;
            int yyyy = 0;
            if (month == 12)
            {
                mm = 1;
                yyyy = year + 1;
            }
            else
            {
                mm = month + 1;
                yyyy = year;
            }

            string sql = " SELECT ISNULL(SUM(dbo.GetPackingSize_OnCall('" + pid + "',PACKINGSIZEID,'" + ToPacksize + "',QTY)),0) AS SALEFORECATEQTY FROM [T_SALESFORCAST_DETAILS] WITH (NOLOCK) WHERE SALESFORCASTID IN(SELECT SALESFORCASTID FROM dbo.T_SALESFORCAST_HEADER WITH (NOLOCK) WHERE MONTH=" + month + " AND YEAR=" + year + ") AND PRODUCTID='" + pid + "'" +
                         " UNION ALL" +
                         " SELECT ISNULL(SUM(dbo.GetPackingSize_OnCall('" + pid + "',PACKINGSIZEID,'" + ToPacksize + "',QTY)),0) AS SALEFORECATEQTY FROM [T_SALESFORCAST_DETAILS] WITH (NOLOCK) WHERE SALESFORCASTID IN(SELECT SALESFORCASTID FROM dbo.T_SALESFORCAST_HEADER WITH (NOLOCK) WHERE MONTH=" + mm + " AND YEAR=" + yyyy + ") AND PRODUCTID='" + pid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal CheckPOwiseItemSumInGrid(string poid, string pid, string pcksizeid)
        {
            string sql = "";
            decimal qty_sum = 0;
            dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];

            if (dtPODepoQty != null)
            {
                foreach (DataRow dr in dtPODepoQty.Rows)
                {
                    if (dr["POID"].ToString() == poid && dr["PRODUCTID"].ToString() == pid)
                    {
                        if (dr["PACKINGSIZEID"].ToString() == pcksizeid)
                        {
                            qty_sum = qty_sum + Convert.ToDecimal(dr["DESPATCHQTY"].ToString());
                        }
                        else
                        {
                            sql = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + pid + "', '" + dr["PACKINGSIZEID"].ToString() + "','" + pcksizeid + "', " + Convert.ToDecimal(dr["DESPATCHQTY"].ToString()) + ")),0)";

                            qty_sum = qty_sum + (decimal)db.GetSingleValue(sql); ;


                        }

                    }
                }
            }
            return qty_sum;
        }

        public int CheckPOwiseItemStatus(string poid, string depoid, string pid, string despatchdate)
        {
            int flag = 0;
            dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];

            if (dtPODepoQty != null)
            {
                foreach (DataRow dr in dtPODepoQty.Rows)
                {
                    if (dr["POID"].ToString() == poid && dr["DEPOID"].ToString() == depoid && dr["PRODUCTID"].ToString() == pid && dr["DESPATCHDATE"].ToString() == despatchdate)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            return flag;
        }

        public string InsertInstructionSheet(   string createdby, string hdnvalue, string finyear, string tpuID, 
                                                string tpuName, string allocationDate, string xml, string TotalCasePack,
                                                string OrderTypeID,string OrderTypeName,string SaleOrderID,string SaleOrderNo,
                                                string CountryID,string CountryName,string DepotID,string DepotName,
                                                string TransporterID,string TransporterName,string DespatchDate)
        {
            string flag = "";
            string InstructionID = string.Empty;
            string InstructionNo = string.Empty;

            if (hdnvalue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            dtPODepoQty = (DataTable)HttpContext.Current.Session["PRODUCTDEPOQTY"];

            try
            {
                string sqlprocinstruction = " EXEC [sp_TPU_INSTRUCTIONHEADER] '" + hdnvalue + "','" + flag + "','" + hdnvalue + "','" + createdby + "','" + finyear + "'," +
                                            " '" + tpuID + "','" + tpuName + "','" + allocationDate + "','" + xml + "'," + Convert.ToDecimal(TotalCasePack) + "," +
                                            " '" + OrderTypeID + "','" + OrderTypeName + "','" + SaleOrderID + "','" + SaleOrderNo + "','" + CountryID + "','" + CountryName + "'," +
                                            " '" + DepotID + "','" + DepotName + "','" + TransporterID + "','" + TransporterName + "','" + DespatchDate + "'";
                DataTable dtInstruction = db.GetData(sqlprocinstruction);

                if (dtInstruction.Rows.Count > 0)
                {
                    InstructionID = dtInstruction.Rows[0]["INSTRUCTIONID"].ToString();
                    InstructionNo = dtInstruction.Rows[0]["INSTRUCTIONNO"].ToString();
                }
                else
                {
                    InstructionNo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return InstructionNo;
        }

        public DataTable LoadProduct(string tpuid)
        {
            DataTable dtProduct = new DataTable();
            string sql = string.Empty;
            try
            {
                sql = "SELECT A.ID,A.NAME FROM M_PRODUCT A INNER JOIN [M_PRODUCT_TPU_MAP] B ON A.ID=B.PRODUCTID " +
                      "WHERE B.VENDORID='" + tpuid + "' ORDER BY A.NAME";
                dtProduct = db.GetData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return dtProduct;
        }

        public int InsertPackingList(string AllocationID,  string TPUID, string xml)
        {
            int e = 0;
            string sql = " EXEC USP_TPU_PACKINGLIST_INSERT '" + AllocationID + "','" + TPUID + "','" + xml + "'";
            e = db.HandleData(sql);
            return e;
        }
    }
}
