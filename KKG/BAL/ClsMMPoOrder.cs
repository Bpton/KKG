using DAL;
using Entitymodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsMMPoOrder
    {
        DBUtils db = new DBUtils();
        //PRAYAAS_23042019Entities PrayaasDB = new PRAYAAS_23042019Entities();
        KKGEntities ObjKKG = new KKGEntities();
        decimal grosstotal = 0;
        decimal MRPtotal = 0;
        decimal Excisetotal = 0;
        decimal csttotal = 0;
        decimal total = 0;
        decimal CgstValue = 0;
        decimal SgstValue = 0;
        decimal IgstValue = 0;
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

        public DataTable BindPO(string fromdate, string todate, string FinYear, string QSTAG, string Checker, string DepotID, string POTYPE, string USERID) /*potype  new add by p.basu on 06092019*/
        {
            string sql = string.Empty;
            sql = "EXEC USP_BIND_PURCHASE_ORDER '" + fromdate + "','" + todate + "','" + FinYear + "','" + QSTAG + "','" + Checker + "','" + DepotID + "','" + POTYPE + "','" + USERID + "'";
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
                    "       SELECT  CAST(ISNULL(R.RMCOST  + R.TRANSFERCOST,0)  AS VARCHAR(18)) AS RATE" +
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

        public DataTable BindVendor(string DepotID)
        {
            DepotID = DepotID.Replace(",", "','");
            string sql = "SELECT * FROM M_TPU_VENDOR WHERE SUPLIEDITEM <>'FG'  AND TAG='T' AND DEPARTMENTID in ('" + DepotID + "')   ORDER BY VENDORNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindProduct(string TPUID)
        {
            string sql = " USP_BIND_VENDOR_WISE_PRODUCT '"+ TPUID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPONODetails(int TPUID)
        {
            string sql = "SELECT SUBSTRING(PONO,8,6) AS COUNTNUMBER,B.FINANCIALYEAR FROM [T_TPU_POHEADER] A,[P_APP_PONO_MASTER] B WHERE PONO IN(SELECT MAX(PONO) AS PONO FROM dbo.T_TPU_POHEADER WHERE TPUID=" + TPUID + ")";
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
        public DataTable BindPackingSize(string PID, string DIVID)
        {
            string sql = " IF EXISTS(SELECT 1 FROM  M_PRIMARY_SUB_ITEM_TYPE WHERE CAST (PRIMARYITEMTYPEID AS VARCHAR(50))='" + DIVID + "') " +
                         " BEGIN " +
                         " SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_MMUNIT] WHERE PRODUCTID='" + PID + "'" +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " SELECT PSID AS PACKSIZEID_FROM,PSNAME AS PACKSIZEName_FROM FROM [Vw_SALEUNIT] WHERE PRODUCTID='" + PID + "'" +
                         " END";
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

        public DataTable BindPODetailsBasedOnPONO(string poID, string finyear) /*REJECTIONOTE NEW ADDED BY P.BASU ON 05092019*/
        {
            string sql = "EXEC[USP_BIND_PODDETAILS_EDIT] '" + poID + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dteditporecord = db.GetData(sql);
            HttpContext.Current.Session["EDITPORECORDS"] = dteditporecord;
            return dteditporecord;
        }

        public DataTable BindPOFooterBasedOnPONO(string pono, string finyear) /*new add by p.basu on 12032020 for tax */
        {
            string sql = "USP_BIND_FOOTER_PO '"+ pono + "','"+ finyear + "'";
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
            dtPORecord.Columns.Add("LASTRATE");  /*last rate*/
            dtPORecord.Columns.Add("MAXRATE");  /*max rate*/
            dtPORecord.Columns.Add("AVGRATE");  /*avg rate*/
            dtPORecord.Columns.Add("MINRATE");  /*min rate*/

            dtPORecord.Columns.Add("CGST");
            dtPORecord.Columns.Add("CGSTVALUE");
            dtPORecord.Columns.Add("CGSTTAXID");
            /*CGST*/
            dtPORecord.Columns.Add("SGST");
            dtPORecord.Columns.Add("SGSTVALUE");
            dtPORecord.Columns.Add("SGSTTAXID");
            /*SGST*/
            dtPORecord.Columns.Add("IGST");
            dtPORecord.Columns.Add("IGSTVALUE");
            dtPORecord.Columns.Add("IGSTTAXID");
            /*IGST*/


            dtPORecord.Columns.Add("REQUIREDDATE");
            dtPORecord.Columns.Add("REQUIREDTODATE");

            //---------- Create DataTable For hold Product value calculation which is show in GridView ----------- //

            dtPOTotalValue.Clear();
            dtPOTotalValue.Columns.Add("GROSSTOTAL");
            dtPOTotalValue.Columns.Add("MRPTOTAL");
            dtPOTotalValue.Columns.Add("EXCISETOTAL");
            dtPOTotalValue.Columns.Add("CSTTOTAL");
            dtPOTotalValue.Columns.Add("TOTAL");
            dtPOTotalValue.Columns.Add("CGSTVALUE");
            dtPOTotalValue.Columns.Add("SGSTVALUE");
            dtPOTotalValue.Columns.Add("IGSTVALUE");

            DataRow drv1 = dtPOTotalValue.NewRow();
            drv1["GROSSTOTAL"] = grosstotal; // assign 0 value as a initial value
            drv1["MRPTOTAL"] = MRPtotal;
            drv1["EXCISETOTAL"] = Excisetotal;
            drv1["CSTTOTAL"] = csttotal;
            drv1["TOTAL"] = csttotal;
            
            drv1["SGSTVALUE"] = SgstValue;
            drv1["IGSTVALUE"] = IgstValue;


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
                decimal cgstvalue = 0;
                decimal sgstvalue = 0;
                decimal igstvalue = 0;

                


                int i = dtPORecord.Rows.Count - 1;
                while (i >= 0)
                {
                    if (Convert.ToString(dtPORecord.Rows[i]["PRODUCTID"]) == PID && Convert.ToString(dtPORecord.Rows[i]["REQUIREDDATE"]) == fromDate && Convert.ToString(dtPORecord.Rows[i]["REQUIREDTODATE"]) == ToDate)
                    {
                        grossvalue = Convert.ToDecimal(dtPORecord.Rows[i]["PRODUCTAMOUNT"]);
                       
                        MRPtotalDelete = Convert.ToDecimal(dtPORecord.Rows[i]["MRPVALUE"]);
                        cgstvalue = Convert.ToDecimal(dtPORecord.Rows[i]["CGSTVALUE"]);
                        sgstvalue = Convert.ToDecimal(dtPORecord.Rows[i]["SGSTVALUE"]);
                        igstvalue = Convert.ToDecimal(dtPORecord.Rows[i]["IGSTVALUE"]);
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
                        cgstvalue= Convert.ToDecimal(dtPOTotalValue.Rows[0]["CGSTVALUE"]) - cgstvalue;
                        sgstvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["SGSTVALUE"]) - sgstvalue;
                        igstvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["IGSTVALUE"]) - igstvalue;
                        dtPOTotalValue.Rows[0]["GROSSTOTAL"] = grossvalue;
                        dtPOTotalValue.Rows[0]["MRPTOTAL"] = MRPtotalDelete;
                        dtPOTotalValue.Rows[0]["EXCISETOTAL"] = ExcisetotalDelete;
                        dtPOTotalValue.Rows[0]["CSTTOTAL"] = csttotalDelete;
                        dtPOTotalValue.Rows[0]["TOTAL"] = totalDelete;
                        dtPOTotalValue.Rows[0]["CGSTVALUE"] = cgstvalue;
                        dtPOTotalValue.Rows[0]["SGSTVALUE"] = sgstvalue;
                        dtPOTotalValue.Rows[0]["IGSTVALUE"] = igstvalue;

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
                decimal cgstvalue = 0;
                decimal sgstvalue = 0;
                decimal igstvalue = 0;

                int i = dteditporecord.Rows.Count - 1;
                while (i >= 0)
                {
                    if (Convert.ToString(dteditporecord.Rows[i]["PRODUCTID"]) == PID && Convert.ToString(dteditporecord.Rows[i]["REQUIREDDATE"]) == fromDate && Convert.ToString(dteditporecord.Rows[i]["REQUIREDTODATE"]) == ToDate)
                    {
                        grossvalue = Convert.ToDecimal(dteditporecord.Rows[i]["PRODUCTAMOUNT"]);
                        MRPtotalDelete = Convert.ToDecimal(dteditporecord.Rows[i]["MRPVALUE"]);
                        cgstvalue = Convert.ToDecimal(dteditporecord.Rows[i]["CGSTVALUE"]);
                        sgstvalue = Convert.ToDecimal(dteditporecord.Rows[i]["SGSTVALUE"]);
                        igstvalue = Convert.ToDecimal(dteditporecord.Rows[i]["IGSTVALUE"]);
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
                        cgstvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["CGSTVALUE"]) - cgstvalue;
                        sgstvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["SGSTVALUE"]) - sgstvalue;
                        igstvalue = Convert.ToDecimal(dtPOTotalValue.Rows[0]["IGSTVALUE"]) - igstvalue;
                        dtPOTotalValue.Rows[0]["GROSSTOTAL"] = grossvalue;
                        dtPOTotalValue.Rows[0]["MRPTOTAL"] = MRPtotalDelete;
                        dtPOTotalValue.Rows[0]["EXCISETOTAL"] = ExcisetotalDelete;
                        dtPOTotalValue.Rows[0]["CSTTOTAL"] = csttotalDelete;
                        dtPOTotalValue.Rows[0]["TOTAL"] = totalDelete;
                        dtPOTotalValue.Rows[0]["CGSTVALUE"] = cgstvalue;
                        dtPOTotalValue.Rows[0]["SGSTVALUE"] = sgstvalue;
                        dtPOTotalValue.Rows[0]["IGSTVALUE"] = igstvalue;

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
        public int POEditedRecordDELETE(string pono, string finyear)
        {
            int delflag = 0;
            string sqleditedprocdetail = "EXEC [SP_MM_TOTALPODELETE] '" + pono + "','" + finyear + "'";
            int e = db.HandleData(sqleditedprocdetail);

            if (e == 0)
            {
                delflag = 0;  // delete unsuccessfull
            }
            else if (e == -1)
            {
                delflag = -1;  // delete unsuccessfull Because pono is allready exists in GRN.
            }
            else
            {
                delflag = 1;  // delete successfull
            }
            return delflag;
        }
        public DataTable BindPOEditedGridRecords(string pono, string PID, string PNAME, decimal PQTY, string CONVERTIONQTY, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal MRPVALUE, decimal ASSESMENT, decimal EXCISE, decimal CST, string UOMID, string UOMNAME, decimal LASTRATE, decimal MAXRATE, decimal AVGRATE, decimal MINRATE, decimal CGST,decimal CGSTVALUE, string CGSTTAXID, decimal SGST,decimal SGSTVALUE, string SGSTTAXID, decimal IGST,decimal IGSTVALUE, string IGSTTAXID)
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

            dr["PRODUCTPRICE"] = PPRICE.ToString("0.000");
            dr["PRODUCTAMOUNT"] = (Convert.ToDecimal(CONVERTIONQTY)).ToString("0.00");

            dr["MRP"] = MRP.ToString("0.00");
            dr["MRPVALUE"] = (Convert.ToDecimal(MRPVALUE)).ToString("0.00");
            dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
            dr["EXCISE"] = EXCISE;
            dr["CST"] = CST;
            dr["LASTRATE"] = LASTRATE;
            dr["MAXRATE"] = MAXRATE;
            dr["AVGRATE"] = AVGRATE;
            dr["MINRATE"] = MINRATE;
            dr["CGST"] = CGST;
            dr["CGSTVALUE"] = CGSTVALUE;
            dr["CGSTTAXID"] = CGSTTAXID;
            dr["SGST"] = SGST;
            dr["SGSTVALUE"] = SGSTVALUE;
            dr["SGSTTAXID"] = SGSTTAXID;
            dr["IGST"] = IGST;
            dr["IGSTVALUE"] = IGSTVALUE;
            dr["IGSTTAXID"] = IGSTTAXID;

            dr["REQUIREDDATE"] = PDATE;
            dr["REQUIREDTODATE"] = PTODATE;

            dteditporecord.Rows.Add(dr);
            return dteditporecord;
        }
        public DataTable BindPurchaseOrderGridRecords(string PID, string PNAME, decimal PQTY, string UOMID, string UOMNAME, string CONVERTIONQTY, string PDATE, string PTODATE, decimal PPRICE, decimal MRP, decimal TOTALMRP, decimal ASSESMENT, decimal EXCISE, decimal CST, decimal LASTRATE, decimal MAXRATE, decimal AVGRATE, decimal MINRATE, decimal CGST,decimal CGSTVALUE, string CGSTTAXID, decimal SGST,decimal SGSTVALUE,string SGSTTAXID, decimal IGST,decimal IGSTVALUE, string IGSTTAXID)
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
            dr["MRPVALUE"] = (TOTALMRP).ToString("0.00");
            dr["ASSESMENTPERCENTAGE"] = ASSESMENT;
            dr["EXCISE"] = EXCISE;
            dr["CST"] = CST;
            dr["LASTRATE"] = LASTRATE;
            dr["MAXRATE"] = MAXRATE;
            dr["AVGRATE"] = AVGRATE;
            dr["MINRATE"] = MINRATE;
            dr["CGST"] = CGST;
            dr["CGSTVALUE"] = CGSTVALUE;
            dr["CGSTTAXID"] = CGSTTAXID;
            dr["SGST"] = SGST;
            dr["SGSTVALUE"] = SGSTVALUE;
            dr["SGSTTAXID"] = SGSTTAXID;
            dr["IGST"] = IGST;
            dr["IGSTVALUE"] = IGSTVALUE;
            dr["IGSTTAXID"] = IGSTTAXID;

            dr["REQUIREDDATE"] = PDATE;
            dr["REQUIREDTODATE"] = PTODATE;
            dtPORecord.Rows.Add(dr);

            return dtPORecord;
        }
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
                decimal Cgst = 0;
                decimal Sgst = 0;
                decimal Igst = 0;
                

                foreach (DataRow dr in dtPORecord.Rows)
                {
                    grosstotal = grosstotal + (Convert.ToDecimal(dr["PRODUCTAMOUNT"]));
                    MRPValue = (Convert.ToDecimal(dr["MRPVALUE"]));
                    MRPtotal = MRPtotal + (Convert.ToDecimal(dr["MRPVALUE"]));
                    Excisetotal = Excisetotal + (MRPValue * (Convert.ToDecimal(dr["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dr["EXCISE"]) / 100));
                    CST = Convert.ToDecimal(dr["CST"]);
                    Cgst= Convert.ToDecimal(dr["CGST"]);
                    Sgst = Convert.ToDecimal(dr["SGST"]);
                    Igst = Convert.ToDecimal(dr["IGST"]);
                }
                CgstValue = (grosstotal * Cgst) / 100;
                SgstValue = (grosstotal * Sgst) / 100;
                IgstValue = (grosstotal * Igst) / 100;
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
                decimal Cgst = 0;
                decimal Sgst = 0;
                decimal Igst = 0;
                

                foreach (DataRow dr in dteditporecord.Rows)
                {
                    grosstotal = grosstotal + (Convert.ToDecimal(dr["PRODUCTAMOUNT"]));
                    //total= grosstotal + (Convert.ToDecimal(dr["PRODUCTAMOUNT"])) + (Convert.ToDecimal(dr["CGSTVALUE"])) + (Convert.ToDecimal(dr["SGSTVALUE"])) + (Convert.ToDecimal(dr["IGSTVALUE"]));
                    MRPValue = (Convert.ToDecimal(dr["MRPVALUE"]));
                    MRPtotal = MRPtotal + (Convert.ToDecimal(dr["MRPVALUE"]));
                    Excisetotal = Excisetotal + (MRPValue * (Convert.ToDecimal(dr["ASSESMENTPERCENTAGE"]) / 100) * (Convert.ToDecimal(dr["EXCISE"]) / 100));
                    CST = Convert.ToDecimal(dr["CST"]);
                    Cgst = Convert.ToDecimal(dr["CGST"]); /*new add for taxamnt cal*/
                    Sgst = Convert.ToDecimal(dr["SGST"]); /*new add for taxamnt cal*/
                    Igst = Convert.ToDecimal(dr["IGST"]); /*new add for taxamnt cal*/
                }
                CgstValue = (grosstotal * Cgst) / 100; /*new add for taxamnt cal*/
                SgstValue = (grosstotal * Sgst) / 100; /*new add for taxamnt cal*/
                IgstValue = (grosstotal * Igst) / 100; /*new add for taxamnt cal*/
                csttotal = ((grosstotal + Excisetotal) * (CST / 100));
                total = grosstotal;
            }
            dtPOTotalValue.Rows[0]["GROSSTOTAL"] = grosstotal;
            dtPOTotalValue.Rows[0]["MRPTOTAL"] = MRPtotal;
            dtPOTotalValue.Rows[0]["EXCISETOTAL"] = Excisetotal;
            dtPOTotalValue.Rows[0]["CSTTOTAL"] = csttotal;
            dtPOTotalValue.Rows[0]["TOTAL"] = total;
            dtPOTotalValue.Rows[0]["CGSTVALUE"] = CgstValue; /*new add for taxamnt cal*/
            dtPOTotalValue.Rows[0]["SGSTVALUE"] = SgstValue; /*new add for taxamnt cal*/
            dtPOTotalValue.Rows[0]["IGSTVALUE"] = IgstValue; /*new add for taxamnt cal*/
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

        public DataTable InsertPODetails(string podate, string tpuid, string tpuname, string remarks, string createdby, decimal grosstotal, decimal adjustment,
                                        decimal dispercent, decimal discount, decimal packpercent, decimal packing, decimal expercent, decimal exercise,
                                        decimal salepercent, decimal saletax, decimal nettotal, decimal othercharges, decimal totalamount, string hdnvalue,
                                        string finyear, decimal MRPTOTAL, string ReferencePOID, string ISAmendment, string QSTAG, string ModuleID, string INDIENTID,
                                        string REFRENCENO, string strTermsID, string QUOTDATE, string ShipingAddress, string CURRENCYID, string CURRENCYTYPE,
                                        string DEPOTID, string rejection, string terms, string ENTRYFROM) /*terms added*/
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

                string sqlprocpo = " EXEC [SP_MM_POHEADER_FAC] '" + flag + "','" + hdnvalue + "','" + podate + "','" + tpuid + "','" + tpuname + "','" + remarks + "'," +
                                   " '" + createdby + "'," + grosstotal + "," + adjustment + "," + dispercent + "," + discount + "," + packpercent + "," +
                                   " " + packing + "," + expercent + "," + exercise + "," + salepercent + "," + saletax + "," + othercharges + "," +
                                   " " + totalamount + "," + nettotal + ",'" + finyear + "'," + MRPTOTAL + ",'" + QSTAG + "','" + ModuleID + "','" + ShipingAddress + "','" +
                                   " " + CURRENCYID + "','" + CURRENCYTYPE + "','" + DEPOTID + "','" +
                                   " " + ReferencePOID + "','" + INDIENTID + "','" + REFRENCENO + "','" + strTermsID + "','" + QUOTDATE + "','" + rejection + "','" + terms + "','" + ENTRYFROM + "'";

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
                        decimal LASTRATE = Convert.ToDecimal(dtPORecord.Rows[i]["LASTRATE"].ToString());
                        decimal MAXRATE = Convert.ToDecimal(dtPORecord.Rows[i]["MAXRATE"].ToString());
                        decimal MINRATE = Convert.ToDecimal(dtPORecord.Rows[i]["MINRATE"].ToString());
                        decimal AVGRATE = Convert.ToDecimal(dtPORecord.Rows[i]["AVGRATE"].ToString());
                        decimal CGST = Convert.ToDecimal(dtPORecord.Rows[i]["CGST"].ToString());
                        decimal CGSTVALUE = Convert.ToDecimal(dtPORecord.Rows[i]["CGSTVALUE"].ToString());
                        string CGSTTAXID = (dtPORecord.Rows[i]["CGSTTAXID"].ToString());
                        decimal SGST = Convert.ToDecimal(dtPORecord.Rows[i]["SGST"].ToString());
                        decimal SGSTVALUE = Convert.ToDecimal(dtPORecord.Rows[i]["SGSTVALUE"].ToString());
                        string SGSTTAXID = (dtPORecord.Rows[i]["SGSTTAXID"].ToString());
                        decimal IGST = Convert.ToDecimal(dtPORecord.Rows[i]["IGST"].ToString());
                        decimal IGSTVALUE = Convert.ToDecimal(dtPORecord.Rows[i]["IGSTVALUE"].ToString());
                        string IGSTTAXID = (dtPORecord.Rows[i]["IGSTTAXID"].ToString());

                        string sqlprocdetail = "EXEC [SP_MM_PODETAILS_FAC] '" + flag + "','" + poid + "','" + productid + "','" + productname + "', " +
                         "" + productqty + ",'" + UOMID + "','" + UOMNAME + "'," + productprice + "," + productamount + ",'" + requireddate + "'," +
                         "'" + requiredtodate + "','" + finyear + "'," + MRP + "," + MRPValue + "," + Assesment + "," + Excise + "," + CST + "," +
                         "'" + LASTRATE + "','" + MAXRATE + "','" + MINRATE + "','" + AVGRATE + "','" + CGST + "','"+CGSTVALUE+"','" + CGSTTAXID + "'," +
                         "'" + SGST + "','"+SGSTVALUE+"','" + SGSTTAXID + "','" + IGST + "','"+IGSTVALUE+"','" + IGSTTAXID + "'";
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
                string sqlprocpo = "EXEC [SP_MM_POHEADER_FAC] '" + flag + "','" + hdnvalue + "','" + podate + "','" + tpuid + "','" + tpuname + "','" + remarks + "'," +
                                   " '" + createdby + "'," + grosstotal + "," + adjustment + "," + dispercent + "," + discount + "," + packpercent + "," +
                                   " " + packing + "," + expercent + "," + exercise + "," + salepercent + "," + saletax + "," + othercharges + "," +
                                   " " + totalamount + "," + nettotal + ",'" + finyear + "'," + MRPTOTAL + ",'" + QSTAG + "','','" + ShipingAddress + "','" +
                                   " " + CURRENCYID + "','" + CURRENCYTYPE + "','" + DEPOTID + "','" +
                                   " " + ReferencePOID + "', '" + INDIENTID + "','" + REFRENCENO + "','" + strTermsID + "','" + QUOTDATE + "','" + rejection + "','" + terms + "','" + ENTRYFROM + "'";

                dtpono = db.GetData(sqlprocpo);

                if (dtpono.Rows.Count > 0)
                {
                    string sql = "DELETE FROM dbo.T_MM_PODETAILS where POID='" + hdnvalue + "'";
                    int delflag = db.HandleData(sql);

                    string SQL1 = "DELETE FROM dbo.T_MM_POITEMWISE_TAX where POID='" + hdnvalue + "'";
                    int delflag1 = db.HandleData(SQL1);

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
                        decimal LASTRATE = Convert.ToDecimal(dteditporecord.Rows[i]["LASTRATE"].ToString());
                        decimal MAXRATE = Convert.ToDecimal(dteditporecord.Rows[i]["MAXRATE"].ToString());
                        decimal MINRATE = Convert.ToDecimal(dteditporecord.Rows[i]["MINRATE"].ToString());
                        decimal AVGRATE = Convert.ToDecimal(dteditporecord.Rows[i]["AVGRATE"].ToString());
                        decimal CGST = Convert.ToDecimal(dteditporecord.Rows[i]["CGST"].ToString());
                        decimal CGSTVALUE = Convert.ToDecimal(dteditporecord.Rows[i]["CGSTVALUE"].ToString());
                        string CGSTTAXID = dteditporecord.Rows[i]["CGSTTAXID"].ToString();
                        decimal SGST = Convert.ToDecimal(dteditporecord.Rows[i]["SGST"].ToString());
                        decimal SGSTVALUE = Convert.ToDecimal(dteditporecord.Rows[i]["SGSTVALUE"].ToString());
                        string SGSTTAXID = dteditporecord.Rows[i]["SGSTTAXID"].ToString();
                        decimal IGST = Convert.ToDecimal(dteditporecord.Rows[i]["IGST"].ToString());
                        decimal IGSTVALUE = Convert.ToDecimal(dteditporecord.Rows[i]["IGSTVALUE"].ToString());
                        string IGSTTAXID = dteditporecord.Rows[i]["IGSTTAXID"].ToString();

                        string sqlprocdetail = "EXEC [SP_MM_PODETAILS_FAC] '" + flag + "','" + hdnvalue + "','" + productid + "','" + productname + "', " + productqty + "," +
                                               " '" + UOMID + "','" + UOMNAME + "'," + productprice + "," + productamount + ",'" + requireddate + "','" + requiredtodate + "'," +
                                               " '" + finyear + "'," + MRP + "," + MRPValue + "," + Assesment + "," + Excise + "," + CST + ",'" + LASTRATE + "','" + MAXRATE + "'," +
                                               " '" + MINRATE + "','" + AVGRATE + "','" + CGST + "','"+ CGSTVALUE + "','" + CGSTTAXID + "','" + SGST + "','"+ SGSTVALUE + "','" + SGSTTAXID + "','" + IGST + "','"+ IGSTVALUE + "','" + IGSTTAXID + "'";
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
            string sql = " SELECT A.INDENTID,A.[INDENTNO],convert(varchar(20),A.INDENTDATE,103) AS INDENTDATE  ,A.DEPTNAME " +
                        " FROM  VW_PO_INDENT A  INNER JOIN T_PO_INDENT_DETAILS B ON A.INDENTID=B.INDENTID WHERE B.POID='" + poid + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductMM(string INDENTID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " SELECT [MATERIALID],NAME,[UOMID] AS UOMID,UOMNAME,ISNULL(SUM([QTY])  ,0) AS QTY  ," +
                             " CONVERT(VARCHAR(10),CAST(REQUIREDFROMDATE AS DATE),103) AS REQUIREDFROMDATE,CONVERT(VARCHAR(10),CAST(REQUIREDTODATE AS DATE),103) AS REQUIREDTODATE" +
                             " FROM VW_PO_MATERIAL " +
                             " WHERE INDENTID IN ('" + INDENTID + "')" +
                             " GROUP BY [MATERIALID],NAME,[UOMID],UOMNAME,REQUIREDFROMDATE,REQUIREDTODATE";
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

        public string CheckVerify(string MMPURCHASEID)
        {
            string Flag = "";
            string sql = "SELECT ISVERIFIED FROM T_MM_POHEADER WHERE POID = '" + MMPURCHASEID + "' ";
            Flag = (string)db.GetSingleValue(sql);
            return Flag;
        }
        #endregion

        public DataSet BindAllRate(string productid, string tpuid, string date, string finyear) /*new add for rate by p.basu on 11/09/2019*/
        {
            string sql = " EXEC USP_BIND_PURCHASERATE_MAX_MIN_AVG '" + productid + "','" + tpuid + "','" + date + "','" + finyear + "'";
            DataSet ds = new DataSet();
            ds = db.GetDataInDataSet(sql);
            return ds;
        }

        public VENDOR_AND_CURRENCY VendorWihtCurrency()
        {
            var command = ObjKKG.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[USP_BIND_VENDOR_AND_CURRENCY]";
            command.CommandType = CommandType.StoredProcedure;
            ObjectParameter[] param1 = new ObjectParameter[1];
            ObjKKG.Database.Connection.Open();
            var reader = command.ExecuteReader();
            List<VENDORID> _Vendorid = ((IObjectContextAdapter)ObjKKG).ObjectContext.Translate<VENDORID>(reader).ToList();
            reader.NextResult();

            List<CURRENCYID> _Currency = ((IObjectContextAdapter)ObjKKG).ObjectContext.Translate<CURRENCYID>(reader).ToList();

            VENDOR_AND_CURRENCY domainentity = new VENDOR_AND_CURRENCY();
            domainentity.Vendorid = _Vendorid;
            domainentity.CURRENCY = _Currency;
            ObjKKG.Database.Connection.Close();
            return domainentity;
        }

        public PACKSIZE_MRP_AND_RATE PackSize_MrpWihtRate(string ProductID, string Tpuid, string Podate)
        {
            var command = ObjKKG.Database.Connection.CreateCommand();
            command.CommandText = "[dbo].[USP_BIND_PACKSIZE_MRP_AND_RATE]";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@P_PRODUCTID", ProductID));
            command.Parameters.Add(new SqlParameter("@P_TPUID", Tpuid));
            command.Parameters.Add(new SqlParameter("@P_FROMDATE", Podate));
            ObjectParameter[] param1 = new ObjectParameter[1];
            ObjKKG.Database.Connection.Open();
            var reader = command.ExecuteReader();
            List<PACKSIZEID_FROM> _PackSizeid = ((IObjectContextAdapter)ObjKKG).ObjectContext.Translate<PACKSIZEID_FROM>(reader).ToList();
            reader.NextResult();

            List<MRP> _Mrp = ((IObjectContextAdapter)ObjKKG).ObjectContext.Translate<MRP>(reader).ToList();
            reader.NextResult();

            List<RATE> _Rate = ((IObjectContextAdapter)ObjKKG).ObjectContext.Translate<RATE>(reader).ToList();

            PACKSIZE_MRP_AND_RATE domainentity = new PACKSIZE_MRP_AND_RATE();
            domainentity.PackSize = _PackSizeid;
            domainentity.Mrp = _Mrp;
            domainentity.Rate = _Rate;
            ObjKKG.Database.Connection.Close();
            return domainentity;
        }

        public DataTable Bind_Sms_Mobno(string mode, string poid)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " EXEC USP_GET_SMS_MOBILENO_FAC_PO '" + mode + "','" + poid + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable BindPODetailsBasedOnPONO(string pono, string finyear, string MODE) /*INLINE TO SP BY P.BASU ON 31082019*/
        {
            string sql = "USP_BIND_PODETAILS_BASED_ONPONO '" + pono + "','" + finyear + "','" + MODE + "'";
            DataTable dt = new DataTable();

            dteditporecord = db.GetData(sql);

            HttpContext.Current.Session["EDITPORECORDS"] = dteditporecord;
            return dteditporecord;
        }
        public DataTable editdatadtl(string pono, string finyear) /*INLINE TO SP BY P.BASU ON 31082019*/
        {
            string sql = "usp_bindpohdr '" + pono + "','" + finyear + "'";
            DataTable dt = new DataTable();

            dteditporecord = db.GetData(sql);

            // HttpContext.Current.Session["EDITPORECORDS"] = dteditporecord;
            return dteditporecord;
        }
        public DataTable BINDVENDORNAME(string VENDORID)  /*15102019*/
        {
            string sql = "SELECT VENDORID,VENDORNAME FROM M_TPU_VENDOR WHERE VENDORID ='" + VENDORID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable bindproductfromid(string PRODUCTID) /*15102019*/
        {
            string sql = "SELECT ID,PRODUCTALIAS FROM M_PRODUCT WHERE ID='" + PRODUCTID + "' AND P.ACTIVE='T'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable checkproduct(string PRODUCTID, string vendorid, string mode) /*21102019 by p.basu*/
        {
            string sql = "EXEC[USP_PRODUCT_WITH_VENDOR_CHECK] '" + PRODUCTID + "','" + vendorid + "','" + mode + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable CREATESMS(string poid, string mode)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " EXEC USP_AFTERAPPROVAL_GEN_SMS '" + poid + "','" + mode + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable BindTaxPercentage(string vendorid, string productid, string depotid) /**new add on 24112019 for category wise tax checking**/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " EXEC USP_TAX_FROM_CATEGORY '" + vendorid + "','" + productid + "','" + depotid + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public string UploadFileForPo(string POID, string FILENAME, string MODE)
        {
            string UPLOADNO = string.Empty;
            string sql = " USP_UPLOAD_POQUOTATION_COMPARATIVE_UPLOAD '" + POID + "','" + FILENAME + "','" + MODE + "'";
            DataTable dtACK = db.GetData(sql);
            if (dtACK.Rows.Count > 0)
            {
                UPLOADNO = dtACK.Rows[0]["UPLOADNO"].ToString();
            }
            else
            {
                UPLOADNO = "";
            }
            return UPLOADNO;
        }

        #region Fetch poquation
        public DataTable FetchPoDocument(string poid)/*added on 25112019 by p.basu Quotation upload*/
        {
            string sql = "Select UPLOADID,POID from T_MM_POQUOTATION where uploadforid='1' and POID='" + poid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Fetch Comparative 
        public DataTable FetchPoDocumentComparative(string poid)/*added on 25112019 by p.basu Comparative  upload*/
        {
            string sql = "Select UPLOADID,POID from T_MM_POQUOTATION where uploadforid='2' and POID='" + poid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        #endregion
        public DataTable DepotidPoid(string poid) /*new added by p.basu for validation on 18/12/2019*/
        {
            string sql = "SELECT POID,FACTORYID FROM T_MM_POHEADER WHERE POID='" + poid + "'";
            DataTable DT = new DataTable();
            DT = db.GetData(sql);
            return DT;
        }

        public DataTable BindVendorCurrencey(string mode) /*new added by p.basu  17/03/2020*/
        {
            string sql = "EXEC [USP_BIND_VENDOR_AND_CURRENCY_V2] '" + mode+"'";
            DataTable DT = new DataTable();
            DT = db.GetData(sql);
            return DT;
        }
        public decimal Getconvertionqty(string PRODUCTID) /*new added by p.basu 17/03/2020*/
        {
            decimal converqty = 0;
            string SQL = "usp_converttionqty_kkg '" + PRODUCTID + "'";
            converqty = (decimal)db.GetSingleValue(SQL);
            return converqty;
        }
        public decimal GetPurchasecost(string tpuid,string PRODUCTID) /*new added by p.basu 17/03/2020*/
        {
            decimal rate = 0;
            string SQL = " DECLARE @V_RATE VARCHAR(20) SET @V_RATE = (SELECT MAX(ISNULL(R.RMCOST + R.TRANSFERCOST, 0))   FROM[M_TPU__PRODUCT_RATESHEET]  AS R " +
                         " with(nolock)WHERE R.VENDORID = '"+ tpuid + "' AND R.PRODUCTID = '"+ PRODUCTID + "' )IF(@V_RATE = '' OR @V_RATE IS NULL) BEGIN SET @V_RATE = 0 END" +
                         " ELSE BEGIN SET @V_RATE = @V_RATE END  SELECT CAST(@V_RATE AS DECIMAL(18,2)) AS RATE ";
            rate = (decimal)db.GetSingleValue(SQL);
            return rate;
        }

        /*purchase order dashbord*/
        public DataTable FetchPurchaseOrder(string mode,string finyear) /*new added by p.basu  13/01/2021*/
        {
            string sql = "EXEC [usp_purchase_order_modewise] '" + mode + "','"+finyear+"'";
            DataTable DT = new DataTable();
            DT = db.GetData(sql);
            return DT;
        }
    }
}
