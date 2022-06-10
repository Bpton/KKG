using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsWOOrder
    {
        DBUtils db = new DBUtils();

        decimal grosstotal = 0;
        decimal MRPtotal = 0;
        decimal Excisetotal = 0;
        decimal csttotal = 0;
        decimal total = 0;
        //decimal adjustment = 0;
        //decimal conversionqty = 0;
        DataTable dtPORecord = new DataTable();  // Datatable for adding gridproduct
        DataTable dtPOTotalValue = new DataTable();  // Datatable for holding current grosstotal
        DataTable dtpacking = new DataTable();  // Datatable for hold packing master details
        DataTable dteditporecord = new DataTable();  // Datatable for holding edited po details record and add extra po detail if added

        public decimal Get_CalculatePCSQty(string PRODUCTID, decimal QTY, string FROMPACKSIZEID)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT UOMID,UNITVALUE FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            string TOPACKSIZE = dt.Rows[0]["PACKSIZE"].ToString();
            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "','" + FROMPACKSIZEID + "','" + TOPACKSIZE + "','" + QTY + "')),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }
        public DataTable BindTPU()
        {
            string sql = "SELECT VENDORID, VENDORNAME ,SUPLIEDITEMID FROM M_TPU_VENDOR WHERE SUPLIEDITEMID=6 AND TAG='T'  ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAssesment(string PID)
        {
            string sql = "SELECT ISNULL(ASSESSABLEPERCENT,0) AS ASSESSABLEPERCENT,ISNULL(MRP,0) AS MRP FROM M_PRODUCT WHERE ID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public decimal BindMrp(string PID)
        {
            string sql = "SELECT ISNULL(MRP,0) AS MRP FROM M_PRODUCT WHERE ID='" + PID + "'";
            decimal mrp = (decimal)db.GetSingleValue(sql);
            return mrp;
        }

        public DataTable BindPO(string fromdate, string todate, string FinYear,string Factoryid)
        {
            /*string sql = " select A.POID,PONO,CONVERT(VARCHAR(10),CAST(PODATE AS DATE),103) AS PODATE,VENDORNAME,'0' AS TOTALCASEPACK,ORDERTYPENAME,COUNT(DISTINCT PRODUCTID) AS TOTALPRODUCT from [T_MM_POHEADER] A WITH (NOLOCK) " +
                         " INNER JOIN T_MM_PODETAILS B WITH (NOLOCK) ON A.POID=B.POID where CONVERT(date,PODATE,103) between dbo.Convert_To_ISO('" + fromdate + "') and dbo.Convert_To_ISO('" + todate + "') AND FINYEAR='" + FinYear + "' " + 
                         " AND ISWORKORDER='Y' " +
                         " GROUP BY A.POID,PONO,PODATE,VENDORNAME,ORDERTYPEID,ORDERTYPENAME ORDER BY dbo.ExtractDate_To_ISO(PODATE) DESC";*/
            string sql = " EXEC USP_BIND_WORKORDER '" + fromdate + "','" + todate + "','" + FinYear + "','"+ Factoryid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string BindRate(string ProductID, string VendorID)
        {
            string sql = string.Empty;
            string Rate = string.Empty;
            sql = " IF  EXISTS(SELECT * FROM [M_TPU__PRODUCT_RATESHEET] WHERE PRODUCTID='" + ProductID + "' AND VENDORID='" + VendorID + "')" +
                    " BEGIN" +
                    "       SELECT  CAST(ISNULL(R.RMCOST  + R.TRANSFERCOST,0)  AS VARCHAR(10)) AS RATE" +
                    " 	    FROM [M_TPU__PRODUCT_RATESHEET] R  WHERE R.VENDORID='" + VendorID + "' AND R.PRODUCTID='" + ProductID + "'" +
                    " END " +
                    " ELSE " +
                    " BEGIN	" +
                    " 	  SELECT  TOP 1 CAST('0.00'  AS VARCHAR(10)) AS RATE " +
                    " 	  FROM [M_TPU__PRODUCT_RATESHEET] " +
                    " END";
            Rate = (string)db.GetSingleValue(sql);
            return Rate;
        }

        public DataTable BindVendor()
        {
            string sql = "SELECT * FROM M_TPU_VENDOR WHERE SUPLIEDITEM <>'FG'  AND TAG='T'  ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindVendor(string DepotID)
        {
            DepotID = DepotID.Replace(",", "','");
            string sql = "SELECT * FROM M_TPU_VENDOR WHERE SUPLIEDITEM <>'FG'  AND TAG='T' AND DEPARTMENTID in ('" + DepotID + "')   ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
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

            string sql = " SELECT ISNULL(SUM(dbo.GetPackingSize_OnCall('" + pid + "',PACKINGSIZEID,'" + ToPacksize + "',QTY)),0) AS SALEFORECATEQTY FROM [T_SALESFORCAST_DETAILS] WHERE SALESFORCASTID IN(SELECT SALESFORCASTID FROM dbo.T_SALESFORCAST_HEADER WHERE MONTH=" + month + " AND YEAR=" + year + ") AND PRODUCTID='" + pid + "'" +
                         " UNION ALL" +
                         " SELECT ISNULL(SUM(dbo.GetPackingSize_OnCall('" + pid + "',PACKINGSIZEID,'" + ToPacksize + "',QTY)),0) AS SALEFORECATEQTY FROM [T_SALESFORCAST_DETAILS] WHERE SALESFORCASTID IN(SELECT SALESFORCASTID FROM dbo.T_SALESFORCAST_HEADER WHERE MONTH=" + mm + " AND YEAR=" + yyyy + ") AND PRODUCTID='" + pid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string GetPUstatus(string PONO)
        {
            //string Sql = "  IF  EXISTS(SELECT * FROM [T_TPU_PRODUCTION_DETAILS] where PONO='" + PONO + "') BEGIN " +
            //                " select '1'   END  else    begin	  select '0'   end ";
            //return ((string)db.GetSingleValue(Sql));

            string Sql = " IF EXISTS(SELECT * FROM [T_MM_STOCKDESPATCH_DETAILS] WHERE POID='" + PONO + "') BEGIN " +
                         " SELECT '1'  END  ELSE  BEGIN  SELECT '0' END ";
            return ((string)db.GetSingleValue(Sql));
        }

        public decimal GetPackingSize_OnCall(string productID, string packsizeFromID, decimal Qty)
        {
            decimal RETURNVALUE = 0;
            string sql = "SELECT CASEPACKSIZEID FROM P_APPMASTER";
            string PackSizeTo = (string)db.GetSingleValue(sql);
            string SQL = "SELECT ISNULL(SUM(DBO.GetPackingSize_OnCall('" + productID + "','" + packsizeFromID + "','" + PackSizeTo + "'," + Qty + " )),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            return RETURNVALUE;
        }

        public DataTable BindOrderType()
        {
            string sql = " SELECT OrderTYPEID AS BSID,ORDERTYPENAME AS BSNAME FROM M_ORDERTYPE WHERE OrderTYPEID<>'2E96A0A4-6256-472C-BE4F-C59599C948B0' ORDER BY ORDERTYPENAME DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindPONODetails(int TPUID)
        {
            string sql = "SELECT SUBSTRING(PONO,8,6) AS COUNTNUMBER,B.FINANCIALYEAR FROM [T_MM_POHEADER] A,[P_APP_PONO_MASTER] B WHERE PONO IN(SELECT MAX(PONO) AS PONO FROM dbo.T_MM_POHEADER WHERE TPUID=" + TPUID + ")";
            DataTable dtpono = new DataTable();
            dtpono = db.GetData(sql);

            string sqlfyear = "SELECT A.CODE,B.FINANCIALYEAR FROM [M_TPU_VENDOR] A,[P_APP_PONO_MASTER] B WHERE A.VENDORID=" + TPUID + "";
            DataTable dtfyear = new DataTable();
            dtfyear = db.GetData(sqlfyear);

            if (dtpono.Rows.Count == 0)
            {
                DataRow dr = dtpono.NewRow();
                dr["COUNTNUMBER"] = "000000";
                dr["FINANCIALYEAR"] = dtfyear.Rows[0]["FINANCIALYEAR"].ToString();
                dtpono.Rows.Add(dr);
                dtpono.Columns.Add("CODE");
                dtpono.Rows[0]["CODE"] = dtfyear.Rows[0]["CODE"].ToString();
            }
            else if (dtpono.Rows[0]["FINANCIALYEAR"] != dtfyear.Rows[0]["FINANCIALYEAR"])
            {
                DataRow dr = dtpono.NewRow();
                dr["COUNTNUMBER"] = "000000";
                dr["FINANCIALYEAR"] = dtfyear.Rows[0]["FINANCIALYEAR"].ToString();
                dtpono.Rows.Add(dr);
                dtpono.Columns.Add("CODE");
                dtpono.Rows[0]["CODE"] = dtfyear.Rows[0]["CODE"].ToString();
            }
            dtpono.Rows[0]["CODE"] = dtfyear.Rows[0]["CODE"].ToString();
            return dtpono;
        }

        public DataTable BindPOGrid(string pono)
        {
            if (pono == "")
            {
                dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
                return dtPORecord;
            }
            else
            {
                dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                return dteditporecord;
            }
        }
        public DataTable BindPackingSize(string PID)
        {
            string sql = "SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_MMUNIT] WHERE PRODUCTID='" + PID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackingSize()
        {
            dtpacking.Clear();
            string sql = "SELECT [PRODUCTID],[PRODUCTNAME],[PACKSIZEID_FROM],[PACKSIZEName_FROM],[PACKSIZEID_TO],[PACKSIZEName_TO],[CONVERSIONQTY] FROM [M_PRODUCT_UOM_MAP]";
            DataTable dt = new DataTable();
            dtpacking = db.GetData(sql);

            HttpContext.Current.Session["PACKINGSIZEDETAILS"] = dtpacking;
            return dtpacking;
        }

        public DataTable BindPODetailsBasedOnPONO(string pono, string finyear)
        {
            string sql = " SELECT B.PONO,convert(varchar(20),B.PODATE,103) AS PODATE,CATEGORYID,CATEGORYName,DIVISIONID,DIVISIONName,NATUREOFPRODUCTID,NATUREOFPRODUCTNAME," +
                         " ISNULL(A.MRP,0) AS MRP,ISNULL(A.TOTMRP,0) AS MRPVALUE,ISNULL(A.ASSESMENTPERCENTAGE,0) AS ASSESMENTPERCENTAGE,ISNULL(A.EXCISEPERCENTAGE,0) AS EXCISE,ISNULL(A.CSTPERCENTAGE,0) AS CST," +
                         " UOMID,UOMName AS UOMNAME,PRODUCTID,PRODUCTName,ISNULL(QTY,0) as PRODUCTQTY,ISNULL(RATE,0) as PRODUCTPRICE,ISNULL(PRODUCTAMOUNT,0) AS PRODUCTAMOUNT,CONVERT(VARCHAR(10),CAST(REQUIREDDate AS DATE),103) AS REQUIREDDate,CONVERT(VARCHAR(10),CAST(REQUIREDTODate AS DATE),103) AS REQUIREDTODate from dbo.T_MM_PODETAILS A " +
                         " INNER JOIN [T_MM_POHEADER] B on A.POID=B.POID where B.PONO='" + pono + "' AND B.FINYEAR='" + finyear + "'";
            DataTable dt = new DataTable();

            dteditporecord = db.GetData(sql);
            HttpContext.Current.Session["EDITPORECORDS"] = dteditporecord;
            return dteditporecord;
        }

        public DataTable BindPOFooterBasedOnPONO(string pono, string finyear)
        {
            string sql = " SELECT ISNULL(GROSSTOTAL,0) AS GROSSTOTAL,ISNULL(ADJUSTMENT,0) AS ADJUSTMENT,ISNULL(DISCOUNTPERCENTAGE,0) AS DISCOUNTPERCENTAGE,ISNULL(DISCOUNT,0) AS DISCOUNT," +
                         " ISNULL(PACKINGPERCENTAGE,0) AS PACKINGPERCENTAGE,ISNULL(PACKING,0) AS PACKING,ISNULL(EXERCISEPERCENTAGE,0) AS EXERCISEPERCENTAGE," +
                         " ISNULL(EXERCISE,0) AS EXERCISE,ISNULL(SALETAXPERCENTAGE,0) AS SALETAXPERCENTAGE,ISNULL(SALETAX,0) AS SALETAX,ISNULL(OTHERCHARGES,0) AS OTHERCHARGES," +
                         " ISNULL(TOTALAMOUNT,0) AS TOTALAMOUNT,ISNULL(NETTOTAL,0) AS NETTOTAL,ISNULL(MRPTOTAL,0) AS MRPTOTAL,H.VENDORID,H.REFERENCEPOID,H.REMARKS,H.REFRENCENO,H.QUOTDATE FROM [T_MM_POFOOTER] F " +
                         " INNER JOIN [T_MM_POHEADER] H ON F.POID=H.POID where H.PONO='" + pono + "' AND H.FINYEAR='" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPurchaseOrderGrid()
        {
            //---------- Create DataTable For Add Product which is show in GridView ----------- //

            dtPORecord.Clear();
            dtPORecord.Columns.Add("PRODUCTID");
            dtPORecord.Columns.Add("PRODUCTNAME");
            dtPORecord.Columns.Add("PRODUCTQTY");
            dtPORecord.Columns.Add("UOMID");
            dtPORecord.Columns.Add("UOMNAME");

            dtPORecord.Columns.Add("PRODUCTPRICE");
            dtPORecord.Columns.Add("PRODUCTAMOUNT");

            dtPORecord.Columns.Add("MRP");
            dtPORecord.Columns.Add("MRPVALUE");
            dtPORecord.Columns.Add("ASSESMENTPERCENTAGE");
            dtPORecord.Columns.Add("EXCISE");
            dtPORecord.Columns.Add("CST");

            dtPORecord.Columns.Add("REQUIREDDATE");
            dtPORecord.Columns.Add("REQUIREDTODATE");

            //---------- Create DataTable For hold Product value calculation which is show in GridView ----------- //

            dtPOTotalValue.Clear();
            dtPOTotalValue.Columns.Add("GROSSTOTAL");
            dtPOTotalValue.Columns.Add("MRPTOTAL");
            dtPOTotalValue.Columns.Add("EXCISETOTAL");
            dtPOTotalValue.Columns.Add("CSTTOTAL");

            dtPOTotalValue.Columns.Add("TOTAL");

            DataRow drv1 = dtPOTotalValue.NewRow();
            drv1["GROSSTOTAL"] = grosstotal; // assign 0 value as a initial value
            drv1["MRPTOTAL"] = MRPtotal;
            drv1["EXCISETOTAL"] = Excisetotal;
            drv1["CSTTOTAL"] = csttotal;
            drv1["TOTAL"] = csttotal;

            dtPOTotalValue.Rows.Add(drv1);
            HttpContext.Current.Session["GROSSTOTAL"] = dtPOTotalValue;
            HttpContext.Current.Session["PORECORDS"] = dtPORecord;
            return dtPORecord;
        }

        public int PORecordsCheck(string PID, string PackSizeID, string PDATE, string PTODATE, string hdnvalue)
        {
            int flag = 0;
            if (hdnvalue == "")
            {
                dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
                foreach (DataRow dr in dtPORecord.Rows)
                {
                    if (dr["PRODUCTID"].ToString() == PID && dr["REQUIREDDATE"].ToString() == PDATE && dr["REQUIREDTODATE"].ToString() == PTODATE)
                    {
                        flag = 1;
                        break;
                    }
                    else if (dr["PRODUCTID"].ToString() == PID && (dr["REQUIREDDATE"].ToString() != PDATE || dr["REQUIREDTODATE"].ToString() != PTODATE))
                    {
                        if (dr["PRODUCTID"].ToString() == PID && dr["UOMID"].ToString() == PackSizeID)
                        {
                            flag = 0;
                            break;
                        }
                        else
                        {
                            flag = 2;
                            break;
                        }
                    }
                }
            }
            else
            {
                String[] productid = PID.Split('#');
                PID = productid[0].Trim();

                dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                foreach (DataRow dr in dteditporecord.Rows)
                {
                    if (dr["PRODUCTID"].ToString() == PID && dr["REQUIREDDATE"].ToString() == PDATE && dr["REQUIREDTODATE"].ToString() == PTODATE)
                    {
                        flag = 1;
                        break;
                    }
                    else if (dr["PRODUCTID"].ToString() == PID && (dr["REQUIREDDATE"].ToString() != PDATE || dr["REQUIREDTODATE"].ToString() != PTODATE))
                    {
                        if (dr["PRODUCTID"].ToString() == PID && dr["UOMID"].ToString() == PackSizeID)
                        {
                            flag = 0;
                            break;
                        }
                        else
                        {
                            flag = 2;
                            break;
                        }
                    }
                }
            }
            return flag;
        }

        public int PORecordsDelete(string PID, string pono, string fromDate, string ToDate)
        {
            int delflag = 0;

            if (pono == "")
            {
                dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
                dtPOTotalValue = (DataTable)HttpContext.Current.Session["GROSSTOTAL"];
                decimal grossvalue = 0;
                decimal MRPtotalDelete = 0;
                decimal ExcisetotalDelete = 0;
                decimal csttotalDelete = 0;
                decimal totalDelete = 0;

                int i = dtPORecord.Rows.Count - 1;
                while (i >= 0)
                {
                    if (Convert.ToString(dtPORecord.Rows[i]["PRODUCTID"]) == PID && Convert.ToString(dtPORecord.Rows[i]["REQUIREDDATE"]) == fromDate && Convert.ToString(dtPORecord.Rows[i]["REQUIREDTODATE"]) == ToDate)
                    {
                        grossvalue = Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTAMOUNT"]);
                        MRPtotalDelete = Convert.ToDecimal(dtPORecord.Rows[i]["MRPVALUE"]);
                        ExcisetotalDelete = 0;
                        csttotalDelete = ((grossvalue + ExcisetotalDelete) * (Convert.ToDecimal(dtPORecord.Rows[i]["CST"]) / 100));
                        totalDelete = grossvalue;

                        dtPORecord.Rows[i].Delete();
                        dtPORecord.AcceptChanges();

                        grossvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["GROSSTOTAL"]) - grossvalue;
                        MRPtotalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["MRPTOTAL"]) - MRPtotalDelete;
                        ExcisetotalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["EXCISETOTAL"]) - ExcisetotalDelete;
                        csttotalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["CSTTOTAL"]) - csttotalDelete;
                        totalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["TOTAL"]) - totalDelete;
                        dtPOTotalValue.Rows[0]["GROSSTOTAL"] = grossvalue;
                        dtPOTotalValue.Rows[0]["MRPTOTAL"] = MRPtotalDelete;
                        dtPOTotalValue.Rows[0]["EXCISETOTAL"] = ExcisetotalDelete;
                        dtPOTotalValue.Rows[0]["CSTTOTAL"] = csttotalDelete;
                        dtPOTotalValue.Rows[0]["TOTAL"] = totalDelete;

                        delflag = 1;
                        break;
                    }
                    i--;
                }

                HttpContext.Current.Session["GROSSTOTAL"] = dtPOTotalValue;
                HttpContext.Current.Session["PORECORDS"] = dtPORecord;
            }
            else
            {
                dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                BindPOValues(pono);

                decimal grossvalue = 0;
                decimal MRPtotalDelete = 0;
                decimal ExcisetotalDelete = 0;
                decimal csttotalDelete = 0;
                decimal totalDelete = 0;

                int i = dteditporecord.Rows.Count - 1;
                while (i >= 0)
                {
                    if (Convert.ToString(dteditporecord.Rows[i]["PRODUCTID"]) == PID && Convert.ToString(dteditporecord.Rows[i]["REQUIREDDATE"]) == fromDate && Convert.ToString(dteditporecord.Rows[i]["REQUIREDTODATE"]) == ToDate)
                    {
                        grossvalue = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTAMOUNT"]);
                        MRPtotalDelete = Convert.ToDecimal(dteditporecord.Rows[i]["MRPVALUE"]);
                        ExcisetotalDelete = (MRPtotalDelete * (Convert.ToDecimal(dteditporecord.Rows[i]["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dteditporecord.Rows[i]["EXCISE"]) / 100));
                        csttotalDelete = ((grossvalue + ExcisetotalDelete) * (Convert.ToDecimal(dteditporecord.Rows[i]["CST"]) / 100));

                        totalDelete = grossvalue;
                        dteditporecord.Rows[i].Delete();
                        dteditporecord.AcceptChanges();
                        grossvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["GROSSTOTAL"]) - grossvalue;
                        MRPtotalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["MRPTOTAL"]) - MRPtotalDelete;
                        ExcisetotalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["EXCISETOTAL"]) - ExcisetotalDelete;
                        csttotalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["CSTTOTAL"]) - csttotalDelete;
                        totalDelete = Convert.ToDecimal(dtPOTotalValue.Rows[0]["TOTAL"]) - totalDelete;
                        dtPOTotalValue.Rows[0]["GROSSTOTAL"] = grossvalue;
                        dtPOTotalValue.Rows[0]["MRPTOTAL"] = MRPtotalDelete;
                        dtPOTotalValue.Rows[0]["EXCISETOTAL"] = ExcisetotalDelete;
                        dtPOTotalValue.Rows[0]["CSTTOTAL"] = csttotalDelete;
                        dtPOTotalValue.Rows[0]["TOTAL"] = totalDelete;

                        delflag = 1;
                        break;
                    }
                    i--;
                }

                HttpContext.Current.Session["GROSSTOTAL"] = dtPOTotalValue;

                HttpContext.Current.Session["EDITPORECORDS"] = dteditporecord;
            }
            return delflag;
        }

        public int POEditedRecordDELETE(string pono, string finyear, string ISAmendment)
        {
            int delflag = 0;
            string sqleditedprocdetail = "exec [sp_TPU_TotalPODelete] '" + pono + "','" + finyear + "','" + ISAmendment + "'";
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

        public int POEditedRecordDELETE(string pono, string finyear)
        {
            int delflag = 0;
            string sqleditedprocdetail = "exec [sp_MM_TotalPODelete] '" + pono + "','" + finyear + "'";
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

        public DataTable BindPOEditedGridRecords(string pono, string PID, string PNAME, decimal PQTY, string CONVERTIONQTY, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal MRPVALUE, decimal ASSESMENT, decimal EXCISE, decimal CST, string UOMID, string UOMNAME)
        {
            dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
            string sqlpdetails = "select CATID,CATNAME,DIVID,DIVNAME,NATUREID,NATURENAME,ID,NAME from [M_PRODUCT] where ID='" + PID + "'";
            DataTable dtpdetails = new DataTable();
            dtpdetails = db.GetData(sqlpdetails);

            DataRow dr = dteditporecord.NewRow();
            dr["PONO"] = pono;
            dr["CATEGORYID"] = dtpdetails.Rows[0]["CATID"].ToString();
            dr["CATEGORYName"] = dtpdetails.Rows[0]["CATNAME"].ToString();
            dr["DIVISIONID"] = dtpdetails.Rows[0]["DIVID"].ToString();
            dr["DIVISIONName"] = dtpdetails.Rows[0]["DIVNAME"].ToString();
            dr["UOMID"] = UOMID;
            dr["UOMNAME"] = UOMNAME;
            dr["NATUREOFPRODUCTID"] = dtpdetails.Rows[0]["NATUREID"].ToString();
            dr["NATUREOFPRODUCTNAME"] = dtpdetails.Rows[0]["NATURENAME"].ToString();

            dr["PRODUCTID"] = dtpdetails.Rows[0]["ID"].ToString();
            dr["PRODUCTName"] = dtpdetails.Rows[0]["NAME"].ToString();
            dr["PRODUCTQTY"] = PQTY;

            dr["PRODUCTPRICE"] = PPRICE.ToString("0.00");
            dr["PRODUCTAMOUNT"] = (Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");

            dr["MRP"] = MRP.ToString("0.00");
            dr["MRPVALUE"] = (Convert.ToDecimal(MRPVALUE)).ToString("0.00");
            dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
            dr["EXCISE"] = EXCISE;
            dr["CST"] = CST;

            dr["REQUIREDDATE"] = PDATE;
            dr["REQUIREDTODATE"] = PTODATE;

            dteditporecord.Rows.Add(dr);

            return dteditporecord;
        }

        public DataTable BindPOEditedGridRecords(string pono, string PID, string PNAME, decimal PQTY, string CONVERTIONQTY, string PPACKINGSIZE,
            string PPACKINGSIZENAME, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal ASSESMENT, decimal EXCISE, decimal CST)
        {
            dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
            String[] productid = PID.Split('#');
            PID = productid[0].Trim();

            string sqlpdetails = "select CATID,CATNAME,DIVID,DIVNAME,NATUREID,NATURENAME,UOMID,UOMNAME,ID,NAME from [M_PRODUCT] where ID='" + PID + "'";
            DataTable dtpdetails = new DataTable();
            dtpdetails = db.GetData(sqlpdetails);

            DataRow dr = dteditporecord.NewRow();
            dr["PONO"] = pono;
            dr["CATEGORYID"] = dtpdetails.Rows[0]["CATID"].ToString();
            dr["CATEGORYName"] = dtpdetails.Rows[0]["CATNAME"].ToString();
            dr["DIVISIONID"] = dtpdetails.Rows[0]["DIVID"].ToString();
            dr["DIVISIONName"] = dtpdetails.Rows[0]["DIVNAME"].ToString();
            //dr["PACKINGSIZEID"] = PPACKINGSIZE;
            //dr["PRODUCTPACKINGSIZE"] = PPACKINGSIZENAME;
            dr["NATUREOFPRODUCTID"] = dtpdetails.Rows[0]["NATUREID"].ToString();
            dr["NATUREOFPRODUCTNAME"] = dtpdetails.Rows[0]["NATURENAME"].ToString();
            dr["UOMID"] = dtpdetails.Rows[0]["UOMID"].ToString();
            dr["UOMName"] = dtpdetails.Rows[0]["UOMNAME"].ToString();
            dr["PRODUCTID"] = dtpdetails.Rows[0]["ID"].ToString();
            dr["PRODUCTName"] = dtpdetails.Rows[0]["NAME"].ToString();
            dr["PRODUCTQTY"] = PQTY;
            dr["PRODUCTCONVERSIONQTY"] = Convert.ToDecimal(CONVERTIONQTY);
            dr["PRODUCTPRICE"] = PPRICE.ToString("0.00");
            dr["PRODUCTAMOUNT"] = (PPRICE * Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");

            dr["MRP"] = MRP.ToString("0.00");
            dr["MRPVALUE"] = (MRP * Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");
            dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
            dr["EXCISE"] = EXCISE;
            dr["CST"] = CST;

            dr["REQUIREDDATE"] = PDATE;
            dr["REQUIREDTODATE"] = PTODATE;

            dteditporecord.Rows.Add(dr);

            return dteditporecord;
        }
        public decimal ConvertionQty(string pid, string ToPackSize, string qty)
        {
            decimal conqty = 0;
            string sql = "SELECT PACKSIZE FROM P_APPMASTER";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            string frompacksize = dt.Rows[0]["PACKSIZE"].ToString();

            string sql1 = "SELECT ISNULL(dbo.GetPackingSize_OnCall('" + pid + "','" + ToPackSize + "','" + frompacksize + "','" + Convert.ToDecimal(qty) + "'),0) AS CONVERTIONQTY";
            conqty = (decimal)db.GetSingleValue(sql1);

            return conqty;
        }

        public DataTable BindPurchaseOrderGridRecords
            (string PID, string PNAME, decimal PQTY, string CONVERTIONQTY, string UOMID, string UOMNAME, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal ASSESMENT, decimal EXCISE, decimal CST)
        {
            dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
            DataRow dr = dtPORecord.NewRow();
            dr["PRODUCTID"] = PID;
            dr["PRODUCTNAME"] = PNAME;
            dr["PRODUCTQTY"] = PQTY;
            dr["UOMID"] = UOMID;
            dr["UOMNAME"] = UOMNAME;

            dr["PRODUCTAMOUNT"] = (Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");
            dr["PRODUCTPRICE"] = PPRICE.ToString("0.00");

            dr["MRP"] = MRP.ToString("0.00");
            dr["MRPVALUE"] = 0;
            dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
            dr["EXCISE"] = EXCISE;
            dr["CST"] = CST;

            dr["REQUIREDDATE"] = PDATE;
            dr["REQUIREDTODATE"] = PTODATE;
            dtPORecord.Rows.Add(dr);

            return dtPORecord;
        }

        //public DataTable BindPurchaseOrderGridRecords(string PID, string PNAME, decimal PQTY, string UOMID, string UOMNAME,
        //    string CONVERTIONQTY, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal TOTALMRP, decimal ASSESMENT, decimal EXCISE, decimal CST)
        //{
        //    dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];

        //    DataRow dr = dtPORecord.NewRow();
        //    dr["PRODUCTID"] = PID;
        //    dr["PRODUCTNAME"] = PNAME;
        //    dr["PRODUCTQTY"] = PQTY;
        //    dr["UOMID"] = UOMID;
        //    dr["UOMNAME"] = UOMNAME;

        //    dr["PRODUCTAMOUNT"] = (Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");
        //    dr["PRODUCTPRICE"] = PPRICE.ToString("0.00");

        //    dr["MRP"] = MRP.ToString("0.00");
        //    dr["MRPVALUE"] = (TOTALMRP).ToString("0.00");
        //    dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
        //    dr["EXCISE"] = EXCISE;
        //    dr["CST"] = CST;

        //    dr["REQUIREDDATE"] = PDATE;
        //    dr["REQUIREDTODATE"] = PTODATE;
        //    dtPORecord.Rows.Add(dr);

        //    return dtPORecord;
        //}
        ////public DataTable BindPurchaseOrderGridRecords(string PID, string PNAME, decimal PQTY, string UOMID, string UOMNAME, 
        //    string CONVERTIONQTY, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal TOTALMRP, decimal ASSESMENT, decimal EXCISE, decimal CST)
        //{
        //    dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];

        //    DataRow dr = dtPORecord.NewRow();
        //    dr["PRODUCTID"] = PID;
        //    dr["PRODUCTNAME"] = PNAME;
        //    dr["PRODUCTQTY"] = PQTY;
        //    dr["UOMID"] = UOMID;
        //    dr["UOMNAME"] = UOMNAME;

        //    dr["PRODUCTAMOUNT"] = (Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");
        //    dr["PRODUCTPRICE"] = PPRICE.ToString("0.00");

        //    dr["MRP"] = MRP.ToString("0.00");
        //    dr["MRPVALUE"] = (TOTALMRP).ToString("0.00");
        //    dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
        //    dr["EXCISE"] = EXCISE;
        //    dr["CST"] = CST;

        //    dr["REQUIREDDATE"] = PDATE;
        //    dr["REQUIREDTODATE"] = PTODATE;
        //    dtPORecord.Rows.Add(dr);

        //    return dtPORecord;
        //}

        public DataTable BindPOValues(string hdnvalue)
        {
            decimal MRPValue = 0;
            decimal CST = 0;
            if (hdnvalue == "")
            {
                dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
                dtPOTotalValue = (DataTable)HttpContext.Current.Session["GROSSTOTAL"];

                grosstotal = 0;  // Grosstotal reset to 0 because below loop start calculation from top row to bottom row
                MRPtotal = 0;
                Excisetotal = 0;
                csttotal = 0;
                total = 0;
                foreach (DataRow dr in dtPORecord.Rows)
                {
                    grosstotal = grosstotal + (Convert.ToDecimal(dr["PRODUCTAMOUNT"]));
                    MRPValue = (Convert.ToDecimal(dr["MRPVALUE"]));
                    MRPtotal = MRPtotal + (Convert.ToDecimal(dr["MRPVALUE"]));
                    Excisetotal = Excisetotal + (MRPValue * (Convert.ToDecimal(dr["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dr["EXCISE"]) / 100));
                    CST = Convert.ToDecimal(dr["CST"]);
                }
                csttotal = ((grosstotal + Excisetotal) * (CST / 100));
                //total = grosstotal + Excisetotal + csttotal;
                total = grosstotal;
            }
            else
            {
                dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                dtPOTotalValue = (DataTable)HttpContext.Current.Session["GROSSTOTAL"];

                grosstotal = 0;  // Grosstotal reset to 0 because below loop start calculation from top row to bottom row
                MRPtotal = 0;
                Excisetotal = 0;
                csttotal = 0;
                total = 0;

                foreach (DataRow dr in dteditporecord.Rows)
                {
                    grosstotal = grosstotal + (Convert.ToDecimal(dr["PRODUCTAMOUNT"]));
                    MRPValue = (Convert.ToDecimal(dr["MRPVALUE"]));
                    MRPtotal = MRPtotal + (Convert.ToDecimal(dr["MRPVALUE"]));
                    Excisetotal = Excisetotal + (MRPValue * (Convert.ToDecimal(dr["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dr["EXCISE"]) / 100));
                    CST = Convert.ToDecimal(dr["CST"]);
                }

                csttotal = ((grosstotal + Excisetotal) * (CST / 100));
                total = grosstotal;
            }

            dtPOTotalValue.Rows[0]["GROSSTOTAL"] = grosstotal;
            dtPOTotalValue.Rows[0]["MRPTOTAL"] = MRPtotal;
            dtPOTotalValue.Rows[0]["EXCISETOTAL"] = Excisetotal;
            dtPOTotalValue.Rows[0]["CSTTOTAL"] = csttotal;
            dtPOTotalValue.Rows[0]["TOTAL"] = total;
            return dtPOTotalValue;
        }

        public DataTable TaxPercentage()
        {
            DataTable dt = new DataTable();
            string sql = " SELECT ISNULL(PERCENTAGE,0) AS PERCENTAGE" +
                         " FROM M_TAX" +
                         " WHERE ID IN(SELECT EXCISEID FROM P_APPMASTER)" +
                         " UNION ALL" +
                         " SELECT ISNULL(PERCENTAGE,0) AS PERCENTAGE " +
                         " FROM M_TAX" +
                         " WHERE ID IN(SELECT CST FROM P_APPMASTER)";
            dt = db.GetData(sql);
            return dt;
        }


        public DataTable DeletePOValues()
        {
            dtPOTotalValue = (DataTable)HttpContext.Current.Session["GROSSTOTAL"];
            return dtPOTotalValue;
        }

        public DataTable InsertPODetails(string podate, string tpuid, string tpuname, string remarks, string createdby,
          decimal grosstotal, decimal adjustment, decimal dispercent, decimal discount, decimal packpercent, decimal packing,
          decimal expercent, decimal exercise, decimal salepercent, decimal saletax, decimal nettotal, decimal othercharges, decimal totalamount, string hdnvalue,
          string finyear, decimal MRPTOTAL, string ReferencePOID, string ISAmendment, decimal CasePack, string OrderTypeID, string OrderTypeName
          )
        {
            int detailflag = 0;
            string flag = "";
            string poid = string.Empty;
            string pono = string.Empty;
            DataTable dtpono = new DataTable();

            if (hdnvalue == "")
            {
                dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
                flag = "A";

                string sqlprocpo = "exec [T_MM_POFOOTER] '" + flag + "','" + hdnvalue + "','" + podate + "','" + tpuid + "','" + tpuname + "','" + remarks + "','" + createdby + "'," + grosstotal + "," + adjustment + "," + dispercent + "," + discount + "," + packpercent + "," + packing + "," + expercent + "," + exercise + "," + salepercent + "," + saletax + "," + othercharges + "," + totalamount + "," + nettotal + ",'" + finyear + "'," + MRPTOTAL + ",'" + ReferencePOID + "','" + ISAmendment + "'," + CasePack + ",'" + OrderTypeID + "','" + OrderTypeName + "','Y'";
                dtpono = db.GetData(sqlprocpo);

                if (dtpono.Rows.Count > 0)
                {
                    //poid = dtpono.Rows[0]["POID"].ToString();
                    pono = dtpono.Rows[0]["PONO"].ToString();

                    for (int i = 0; i < dtPORecord.Rows.Count; i++)
                    {
                        String[] productidvalue = dtPORecord.Rows[i]["PRODUCTID"].ToString().Split('#');
                        string productid = productidvalue[0].Trim();
                        string productname = dtPORecord.Rows[i]["PRODUCTNAME"].ToString();
                        string productpackingsizeid = dtPORecord.Rows[i]["UOMID"].ToString();
                        string productpackingsizename = dtPORecord.Rows[i]["UOMNAME"].ToString();
                        decimal productqty = Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTQTY"].ToString());
                        decimal productconversionqty = 0;
                        decimal productprice = Convert.ToDecimal(Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTPRICE"].ToString()));
                        decimal productamount = Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTAMOUNT"].ToString());
                        string requireddate = dtPORecord.Rows[i]["REQUIREDDATE"].ToString();
                        string requiredtodate = dtPORecord.Rows[i]["REQUIREDTODATE"].ToString();

                        decimal MRP = Convert.ToDecimal(dtPORecord.Rows[i]["MRP"].ToString());
                        decimal MRPValue = Convert.ToDecimal(dtPORecord.Rows[i]["MRPVALUE"].ToString());
                        decimal Assesment = Convert.ToDecimal(dtPORecord.Rows[i]["ASSESMENTPERCENTAGE"].ToString());
                        decimal Excise = Convert.ToDecimal(dtPORecord.Rows[i]["EXCISE"].ToString());
                        decimal CST = Convert.ToDecimal(dtPORecord.Rows[i]["CST"].ToString());

                        string sqlprocdetail = "exec [sp_TPU_PODetails] '" + flag + "','" + pono + "','" + productid + "','" + productname + "','" + productpackingsizeid + "','" + productpackingsizename + "', " + productqty + ", " + productconversionqty + ", " + productprice + "," + productamount + ",'" + requireddate + "','" + requiredtodate + "','" + finyear + "'," + MRP + "," + MRPValue + "," + Assesment + "," + Excise + "," + CST + "";
                        int e = db.HandleData(sqlprocdetail);

                        if (e == 0)
                        {
                            detailflag = 0;
                            break;
                        }
                    }
                    detailflag = 1;
                }
                else
                {
                    detailflag = 0;
                }
            }
            else
            {
                dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                flag = "U";

                string sqlprocpo = "exec [sp_TPU_POHeader] '" + flag + "','" + hdnvalue + "','" + podate + "','" + tpuid + "','" + tpuname + "','" + remarks + "','" + createdby + "'," + grosstotal + "," + adjustment + "," + dispercent + "," + discount + "," + packpercent + "," + packing + "," + expercent + "," + exercise + "," + salepercent + "," + saletax + "," + othercharges + "," + totalamount + "," + nettotal + ",'" + finyear + "'," + MRPTOTAL + ",'" + ReferencePOID + "','" + ISAmendment + "'," + CasePack + ",'" + OrderTypeID + "','" + OrderTypeName + "'";
                dtpono = db.GetData(sqlprocpo);

                if (dtpono.Rows.Count > 0)
                {
                    string sql = "DELETE FROM dbo.T_MM_PODETAILS where POID IN(SELECT POID FROM T_MM_POHEADER WHERE PONO='" + hdnvalue + "' AND FINYEAR='" + finyear + "')";
                    int delflag = db.HandleData(sql);

                    for (int i = 0; i < dteditporecord.Rows.Count; i++)
                    {
                        String[] productidvalue = dteditporecord.Rows[i]["PRODUCTID"].ToString().Split('#');
                        string productid = productidvalue[0].Trim();
                        string productname = dteditporecord.Rows[i]["PRODUCTNAME"].ToString();
                        string productpackingsizeid = dteditporecord.Rows[i]["PACKINGSIZEID"].ToString();
                        string productpackingsizename = dteditporecord.Rows[i]["PRODUCTPACKINGSIZE"].ToString();
                        decimal productqty = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTQTY"].ToString());
                        decimal productconversionqty = 0;
                        decimal productprice = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTPRICE"].ToString());
                        decimal productamount = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTAMOUNT"].ToString());
                        string requireddate = dteditporecord.Rows[i]["REQUIREDDATE"].ToString();
                        string requiredtodate = dteditporecord.Rows[i]["REQUIREDTODATE"].ToString();

                        decimal MRP = Convert.ToDecimal(dteditporecord.Rows[i]["MRP"].ToString());
                        decimal MRPValue = Convert.ToDecimal(dteditporecord.Rows[i]["MRPVALUE"].ToString());
                        decimal Assesment = Convert.ToDecimal(dteditporecord.Rows[i]["ASSESMENTPERCENTAGE"].ToString());
                        decimal Excise = Convert.ToDecimal(dteditporecord.Rows[i]["EXCISE"].ToString());
                        decimal CST = Convert.ToDecimal(dteditporecord.Rows[i]["CST"].ToString());

                        string sqlprocdetail = "exec [sp_TPU_PODetails] '" + flag + "','" + hdnvalue + "','" + productid + "','" + productname + "','" + productpackingsizeid + "','" + productpackingsizename + "', " + productqty + ", " + productconversionqty + ", " + productprice + "," + productamount + ",'" + requireddate + "','" + requiredtodate + "','" + finyear + "'," + MRP + "," + MRPValue + "," + Assesment + "," + Excise + "," + CST + "";

                        int e = db.HandleData(sqlprocdetail);

                        if (e == 0)
                        {
                            detailflag = 0;
                            break;
                        }
                    }
                    //string sqlprocdetail = "exec [sp_TPU_PODetails] " + dteditporecord + "";

                    detailflag = 1;
                }
                else
                {
                    detailflag = 0;
                }
            }
            return dtpono;
        }

        public DataTable InsertPODetails(string podate, string tpuid, string tpuname, string remarks, string createdby, decimal grosstotal, decimal adjustment,
                                        decimal dispercent, decimal discount, decimal packpercent, decimal packing, decimal expercent, decimal exercise,
                                        decimal salepercent, decimal saletax, decimal nettotal, decimal othercharges, decimal totalamount, string hdnvalue,
                                        string finyear, decimal MRPTOTAL, string ReferencePOID, string ISAmendment, string QSTAG, string INDIENTID,
                                        string REFRENCENO, string strTermsID, string QUOTDATE)
        {
            int detailflag = 0;
            string flag = "";
            string poid = string.Empty;
            string pono = string.Empty;
            DataTable dtpono = new DataTable();

            if (hdnvalue == "")
            {
                dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDS"];
                flag = "A";

                string sqlprocpo = " exec [SP_MM_POHEADER] '" + flag + "','" + hdnvalue + "','" + podate + "','" + tpuid + "','" + tpuname + "','" + remarks + "'," +
                                   " '" + createdby + "'," + grosstotal + "," + adjustment + "," + dispercent + "," + discount + "," + packpercent + "," +
                                   " " + packing + "," + expercent + "," + exercise + "," + salepercent + "," + saletax + "," + othercharges + "," +
                                   " " + totalamount + "," + nettotal + ",'" + finyear + "'," + MRPTOTAL + ",'" + QSTAG + "','" + ReferencePOID + "','" + INDIENTID + "','" + REFRENCENO + "','" + strTermsID + "','" + QUOTDATE + "'";

                dtpono = db.GetData(sqlprocpo);

                if (dtpono.Rows.Count > 0)
                {
                    poid = dtpono.Rows[0]["POID"].ToString();

                    for (int i = 0; i < dtPORecord.Rows.Count; i++)
                    {
                        String[] productidvalue = dtPORecord.Rows[i]["PRODUCTID"].ToString().Split('#');
                        string productid = productidvalue[0].Trim();
                        string productname = dtPORecord.Rows[i]["PRODUCTNAME"].ToString();
                        decimal productqty = Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTQTY"].ToString());
                        string UOMID = dtPORecord.Rows[i]["UOMID"].ToString();
                        string UOMNAME = dtPORecord.Rows[i]["UOMNAME"].ToString();
                        decimal productprice = Convert.ToDecimal(Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTPRICE"].ToString()));
                        decimal productamount = Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTAMOUNT"].ToString());
                        string requireddate = dtPORecord.Rows[i]["REQUIREDDATE"].ToString();
                        string requiredtodate = dtPORecord.Rows[i]["REQUIREDTODATE"].ToString();

                        decimal MRP = Convert.ToDecimal(dtPORecord.Rows[i]["MRP"].ToString());
                        decimal MRPValue = Convert.ToDecimal(dtPORecord.Rows[i]["MRPVALUE"].ToString());
                        decimal Assesment = Convert.ToDecimal(dtPORecord.Rows[i]["ASSESMENTPERCENTAGE"].ToString());
                        decimal Excise = Convert.ToDecimal(dtPORecord.Rows[i]["EXCISE"].ToString());
                        decimal CST = Convert.ToDecimal(dtPORecord.Rows[i]["CST"].ToString());

                        string sqlprocdetail = "exec [sp_MM_PODetails] '" + flag + "','" + poid + "','" + productid + "','" + productname + "', " + productqty + "," +
                                              " '" + UOMID + "','" + UOMNAME + "'," + productprice + "," + productamount + ",'" + requireddate + "'," +
                                              " '" + requiredtodate + "','" + finyear + "'," + MRP + "," + MRPValue + "," + Assesment + "," + Excise + "," + CST + "";

                        int e = db.HandleData(sqlprocdetail);

                        if (e == 0)
                        {
                            detailflag = 0;
                            break;
                        }
                    }
                    detailflag = 1;
                }
                else
                {
                    detailflag = 0;
                }
            }
            else
            {
                dteditporecord = (DataTable)HttpContext.Current.Session["EDITPORECORDS"];
                flag = "U";

                string sqlprocpo = "exec [SP_MM_POHEADER] '" + flag + "','" + hdnvalue + "','" + podate + "','" + tpuid + "','" + tpuname + "','" + remarks + "'," +
                                   " '" + createdby + "'," + grosstotal + "," + adjustment + "," + dispercent + "," + discount + "," + packpercent + "," +
                                   " " + packing + "," + expercent + "," + exercise + "," + salepercent + "," + saletax + "," + othercharges + "," +
                                   " " + totalamount + "," + nettotal + ",'" + finyear + "'," + MRPTOTAL + ",'" + QSTAG + "','" + ReferencePOID + "', '" + INDIENTID + "','" + REFRENCENO + "','" + strTermsID + "','" + QUOTDATE + "'";

                dtpono = db.GetData(sqlprocpo);

                if (dtpono.Rows.Count > 0)
                {
                    string sql = "DELETE FROM dbo.T_MM_PODETAILS where POID='" + hdnvalue + "'";
                    int delflag = db.HandleData(sql);

                    for (int i = 0; i < dteditporecord.Rows.Count; i++)
                    {
                        string productid = dteditporecord.Rows[i]["PRODUCTID"].ToString();
                        string productname = dteditporecord.Rows[i]["PRODUCTNAME"].ToString();

                        decimal productqty = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTQTY"].ToString());
                        string UOMID = dteditporecord.Rows[i]["UOMID"].ToString();
                        string UOMNAME = dteditporecord.Rows[i]["UOMNAME"].ToString();
                        decimal productprice = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTPRICE"].ToString());
                        decimal productamount = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTAMOUNT"].ToString());
                        string requireddate = dteditporecord.Rows[i]["REQUIREDDATE"].ToString();
                        string requiredtodate = dteditporecord.Rows[i]["REQUIREDTODATE"].ToString();

                        decimal MRP = Convert.ToDecimal(dteditporecord.Rows[i]["MRP"].ToString());
                        decimal MRPValue = Convert.ToDecimal(dteditporecord.Rows[i]["MRPVALUE"].ToString());
                        decimal Assesment = Convert.ToDecimal(dteditporecord.Rows[i]["ASSESMENTPERCENTAGE"].ToString());
                        decimal Excise = Convert.ToDecimal(dteditporecord.Rows[i]["EXCISE"].ToString());
                        decimal CST = Convert.ToDecimal(dteditporecord.Rows[i]["CST"].ToString());

                        string sqlprocdetail = "exec [sp_MM_PODetails] '" + flag + "','" + hdnvalue + "','" + productid + "','" + productname + "', " + productqty + "," +
                                            " '" + UOMID + "','" + UOMNAME + "'," + productprice + "," + productamount + ",'" + requireddate + "','" + requiredtodate + "'," +
                                            " '" + finyear + "'," + MRP + "," + MRPValue + "," + Assesment + "," + Excise + "," + CST + "";

                        int e = db.HandleData(sqlprocdetail);

                        if (e == 0)
                        {
                            detailflag = 0;
                            break;
                        }
                    }
                    detailflag = 1;
                }
                else
                {
                    detailflag = 0;
                }
            }
            return dtpono;
        }
        public void ResetDataTables()
        {
            HttpContext.Current.Session["PORECORDS"] = null;
            HttpContext.Current.Session["EDITPORECORDS"] = null;
            HttpContext.Current.Session["PACKINGSIZEDETAILS"] = null;
            HttpContext.Current.Session["GROSSTOTAL"] = null;
            dteditporecord.Clear();
            dtpacking.Clear();
            dtPORecord.Clear();
            dtPOTotalValue.Clear();
        }

        public decimal ConvertionQty(string PRODUCTID, string FROMPACKSIZEID, string QTY, decimal Rate)
        {
            decimal RETURNVALUE = 0;
            decimal RETURNAMOUNT = 0;
            string sql = "SELECT UOMID,UNITVALUE FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            string TOPACKSIZE = dt.Rows[0]["UOMID"].ToString();
            decimal UnitValue = Convert.ToDecimal(dt.Rows[0]["UNITVALUE"].ToString());

            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "','" + FROMPACKSIZEID + "','" + TOPACKSIZE + "','" + QTY + "')),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            RETURNAMOUNT = (Rate / UnitValue) * RETURNVALUE;
            return RETURNAMOUNT;
        }

        public decimal ProductMRPTotal(string PRODUCTID, string FROMPACKSIZEID, string QTY, decimal MRP)
        {
            decimal RETURNVALUE = 0;
            decimal RETURNAMOUNT = 0;
            string sql = "SELECT UOMID,UNITVALUE FROM M_PRODUCT WHERE ID='" + PRODUCTID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);

            string TOPACKSIZE = dt.Rows[0]["UOMID"].ToString();
            decimal UnitValue = Convert.ToDecimal(dt.Rows[0]["UNITVALUE"].ToString());

            string SQL = "Select isnull(sum(dbo.GetPackingSize_OnCall('" + PRODUCTID + "','" + FROMPACKSIZEID + "','" + TOPACKSIZE + "','" + QTY + "')),0)";
            RETURNVALUE = (decimal)db.GetSingleValue(SQL);
            RETURNAMOUNT = (MRP / UnitValue) * RETURNVALUE;
            return RETURNAMOUNT;
        }
        public string FetchTPU(string POID)
        {
            string sql = "select TPUID from [T_MM_POHEADER] where POID='" + POID + "'";
            string TPU = string.Empty;
            TPU = (string)db.GetSingleValue(sql);
            return TPU;
        }

        public DataTable BindIndent(string vendorid, string fromdate, string todate)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " SELECT INDENTID,[INDENTNO],convert(varchar(20),[INDENTDATE],103) AS [INDENTDATE],DEPTNAME,VENDORID " +
                             " FROM VW_PO_INDENT WHERE VENDORID='" + vendorid + "' " +
                             " AND CONVERT(DATE,INDENTDATE,103) BETWEEN CONVERT(DATE,'" + fromdate + "',103)   AND CONVERT(DATE,'" + todate + "',103)";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable EditIndent(string poid)
        {
            string sql = " SELECT A.INDENTID,A.[INDENTNO],convert(varchar(20),A.INDENTDATE,103) AS INDENTDATE,A.DEPTNAME " +
                         " FROM  VW_PO_INDENT A  INNER JOIN T_PO_INDENT_DETAILS B ON A.INDENTID=B.INDENTID WHERE B.POID='" + poid + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProduct(string TPUID)
        {
            string sql = " SELECT DISTINCT P.ID + '#' + CAST(CAST(ISNULL(RMCOST  + TRANSFERCOST,0) AS DECIMAL(18,14))/CAST(PCS AS DECIMAL(18,14)) AS VARCHAR(100)) AS ID, NAME FROM M_PRODUCT P " +
                         " RIGHT JOIN [M_TPU__PRODUCT_RATESHEET] R ON P.ID=R.PRODUCTID  WHERE R.VENDORID='" + TPUID + "' order by Name";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductMM(string INDENTID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " SELECT [MATERIALID] ,NAME ,[UOMID] AS UOMID,UOMNAME,ISNULL(SUM([QTY])  ,0) AS QTY  ," +
                             " CONVERT(VARCHAR(10),CAST(REQUIREDFROMDATE AS DATE),103) AS REQUIREDFROMDATE,CONVERT(VARCHAR(10),CAST(REQUIREDTODATE AS DATE),103) AS REQUIREDTODATE" +
                             " FROM VW_PO_MATERIAL " +
                             " WHERE INDENTID IN ('" + INDENTID + "')" +
                             " GROUP BY [MATERIALID]   ,NAME  ,[UOMID] ,UOMNAME,REQUIREDFROMDATE,REQUIREDTODATE";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        #region Add By Rajeev (12-07-2017)
        public DataTable EditTerms(string MMPURCHASEID)
        {
            string sql = "SELECT MMPURCHASEORDERID,TERMSID FROM T_MMPURCHASEORDER_TERMS WHERE MMPURCHASEORDERID = '" + MMPURCHASEID + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTerms(string menuID)
        {
            string sql = " SELECT A.ID,A.DESCRIPTION" +
                         " FROM M_TERMSANDCONDITIONS AS A INNER JOIN M_TERMS_MENU_MAPPING AS B" +
                         " ON A.ID = B.TERMSID" +
                         " WHERE A.ISAPPROVED='Y'" +
                         " AND A.ISDELETED = 'N'" +
                         " AND B.MENUID = '" + menuID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        #endregion

        public DataTable BindPacksize()
        {
            string sql = "SELECT UOMID,UOMNAME FROM M_UOM WHERE UOMID NOT IN('21BC00F3-2D4F-497F-BF52-5A8324DD0C37','972E37AF-2A3C-4702-988A-7693E5B26081') ORDER BY UOMNAME DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int PORecordsCheckV2(string PID, string PackSizeID, string PDATE, string PTODATE, string hdnvalue)
        {
            int flag = 0;
            dtPORecord = (DataTable)HttpContext.Current.Session["PORECORDSV2"];
            foreach (DataRow dr in dtPORecord.Rows)
            {
                if (dr["PRODUCTID"].ToString() == PID && dr["REQUIREDDATE"].ToString() == PDATE && dr["REQUIREDTODATE"].ToString() == PTODATE)
                {
                    flag = 1;
                    break;
                }
                else if (dr["PRODUCTID"].ToString() == PID && (dr["REQUIREDDATE"].ToString() != PDATE || dr["REQUIREDTODATE"].ToString() != PTODATE))
                {
                    if (dr["PRODUCTID"].ToString() == PID && dr["PRODUCTPACKINGSIZEID"].ToString() == PackSizeID)
                    {
                        flag = 0;
                        break;
                    }
                    else
                    {
                        flag = 0;
                        break;
                    }
                }
            }
            return flag;
        }

        public string GetPUstatusV2(string POID)
        {
            string Sql = " IF  EXISTS(SELECT * FROM [T_TPU_PRODUCTION_DETAILS] where POID='" + POID + "') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }

        #region Insert-Update PO
        public string InsertJobOrderDetails(string hdnValue, string PODate, string TPUID, string TPUName, string Remarks, int CreatedBy, decimal GrossTotal,
                                            decimal ExciseTotal, decimal TotalCST, decimal TotalAmount, string FinYear, decimal MRPTotal, decimal TotalCase,
                                            string OrderTypeID, string OrderTypeName, string xml, string xmlRequestion, string FactoryID,string delivaryFromdate,string deliavryTodate)
        {

            string flag = "";
            string PONo = string.Empty;
            if (hdnValue == "")
            {
                flag = "A";
            }
            else
            {
                flag = "U";
            }
            try
            {
                string sqlprocPurchaseOrder = " EXEC [USP_WORKORDER_INSERT_UPDATE] '" + hdnValue + "','" + flag + "','" + PODate + "','" + TPUID + "','" + TPUName + "','" + Remarks + "'," +
                                              " " + CreatedBy + "," + GrossTotal + "," +
                                              " " + ExciseTotal + "," + TotalCST + "," + TotalAmount + "," +
                                              " '" + FinYear + "'," + MRPTotal + "," + TotalCase + "," +
                                              " '" + OrderTypeID + "','" + OrderTypeName + "','" + xml + "','" + xmlRequestion + "','" + FactoryID + "','"+ delivaryFromdate + "','"+ deliavryTodate + "'";
                DataTable dtPO = db.GetData(sqlprocPurchaseOrder);

                if (dtPO.Rows.Count > 0)
                {
                    PONo = dtPO.Rows[0]["PONO"].ToString();
                }
                else
                {
                    PONo = "";
                }
            }
            catch (Exception ex)
            {
                Convert.ToString(ex);
            }
            return PONo;
        }
        #endregion

        public DataTable FetchProductdetails(string tpuid, string PacksizeID, string DepotID)
        {
            string sql = "EXEC USP_WORKORDER_GRID_DETAILS '" + tpuid + "','" + PacksizeID + "','" + DepotID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataSet BindPurchaseOrderDetails(string POID, string DepotID, string UserID)
        {
            string sql = string.Empty;
            sql = "EXEC USP_WORK_ORDER_DETAILS '" + POID + "','" + DepotID + "','" + UserID + "'";
            DataSet dt = new DataSet();
            dt = db.GetDataInDataSet(sql);
            return dt;
        }
        public DataTable FetchBom(string ProductID, string DepotID,string UserID)
        {
            string sql = "EXEC USP_FETCHBOM_PRODUCTWISE '" + ProductID + "','" + DepotID + "','" + UserID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int DeleteWorkOrder(string pono, string finyear, string ISAmendment)
        {
            int delflag = 0;
            string sqleditedprocdetail = "EXEC [USP_WORKORDER_DELETE] '" + pono + "','" + finyear + "','" + ISAmendment + "'";
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

        public DataTable FetchMaxRate(string ProductID)
        {
            string sql = " USP_BIND_PLAIN_PRODUCT '" + ProductID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
