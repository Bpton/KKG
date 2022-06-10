using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsTransporterBillV2_FAC
    {
        DBUtils db = new DBUtils();

        public DataTable BindTransporterByStock(string tag)
        {
            string sql = string.Empty;

            if (tag == "STKTRN")
            {
                sql = "SELECT DISTINCT TRANSPORTERID AS ID,TRANSPORTERNAME AS NAME from T_STOCKTRANSFER_HEADER  ORDER BY TRANSPORTERNAME";
            }
            else if (tag == "STKINV")
            {
                sql = "SELECT DISTINCT A.ID,A.NAME from M_TPU_TRANSPORTER A INNER JOIN T_SALEINVOICE_HEADER B ON  A.ID=B.TRANSPORTERID";
            }
            else
            {
                sql = "SELECT DISTINCT A.ID,A.NAME from M_TPU_TRANSPORTER A INNER JOIN T_STOCKDESPATCH_HEADER B ON  A.ID=B.TRANSPORTERID";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepot(string USERID)
        {
            string sql = " IF EXISTS( " +
                         " SELECT B.BRID AS BRID ,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "')  " +
                         " BEGIN " +
                         " SELECT B.BRID AS BRID ,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "' AND B.BRANCHTAG='D'  ORDER BY B.BRNAME " +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " SELECT DISTINCT BRID AS BRID ,BRPREFIX AS BRNAME FROM M_BRANCH WHERE BRANCHTAG='D'  ORDER BY BRNAME " +
                         " END ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindDepotForApproval(string USERID)
        {
            string sql = " IF EXISTS( " +
                         " SELECT B.BRID AS BRID,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "')  " +
                         " BEGIN " +
                         " SELECT B.BRID AS BRID,B.BRPREFIX AS BRNAME FROM M_TPU_USER_MAPPING A INNER JOIN M_BRANCH B ON A.TPUID=B.BRID WHERE A.USERID='" + USERID + "'   ORDER BY B.BRNAME " +
                         " END " +
                         " ELSE " +
                         " BEGIN " +
                         " SELECT DISTINCT BRID AS BRID ,BRPREFIX AS BRNAME FROM M_BRANCH   ORDER BY BRNAME " +
                         " END ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporter(string DEPOTID)
        {
            string sql = " SELECT '-1' as ID,'---- GROUP ----' as NAME,'1' AS SEQUENCE  UNION  SELECT GROUPID+'(GR)',GROUPNAME AS NAME,'2' AS SEQUENCE FROM M_TRANSPORTER_GROUP_HEADER" +
                         " UNION" +
                         " SELECT '-2' as ID,'---- OTHERS ----' as NAME,'3' AS SEQUENCE  UNION  SELECT CAST(ID AS VARCHAR(50)) AS ID,NAME  AS NAME,'4' AS SEQUENCE   FROM M_TPU_TRANSPORTER A " +
                         " INNER JOIN M_TRANSPOTER_DEPOT_MAP B ON A.ID=B.TRANSPOTERID" +
                         " WHERE DEPOTID='" + DEPOTID + "'" +
                         " ORDER BY SEQUENCE,NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindState()
        {
            string sql = " SELECT STATE_ID,STATE_NAME FROM M_REGION ORDER BY STATE_NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporter()
        {
            string sql = " SELECT '-1' as ID,'---- GROUP ----' as NAME,'1' AS SEQUENCE  UNION  SELECT GROUPID+'(GR)',GROUPNAME AS NAME,'2' AS SEQUENCE FROM M_TRANSPORTER_GROUP_HEADER" +
                         " UNION" +
                         " SELECT '-2' as ID,'---- OTHERS ----' as NAME,'3' AS SEQUENCE  UNION  SELECT CAST(ID AS VARCHAR(50)) AS ID,NAME  AS NAME,'4' AS SEQUENCE   FROM M_TPU_TRANSPORTER A WHERE A.ISAPPROVED='Y'" +
                         " ORDER BY SEQUENCE,NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCheakerTransporterbill(string FromDate, string ToDate)
        {
            DataTable dt = new DataTable();

            string sql = string.Empty;
            sql = "EXEC USP_BIND_TRANSPORTERBILL '1','','" + FromDate + "','" + ToDate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public string Getstatus(string TRANSPORTERBILLID)
        {

            string Sql = " IF EXISTS(SELECT 1 FROM [T_TRANSPORTER_BILL_HEADER] WHERE TRANSPORTERBILLID='" + TRANSPORTERBILLID + "'  AND DAYENDTAG='Y') BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public string Getstatusforedit(string TRANSPORTERBILLID)
        {

            string Sql = " IF EXISTS(SELECT 1 FROM [T_TRANSPORTER_BILL_HEADER] WHERE TRANSPORTERBILLID='" + TRANSPORTERBILLID + "' and ISVERIFIED ='Y' ) BEGIN " +
                         " SELECT '1'   END  ELSE    BEGIN	  SELECT '0'   END ";
            return ((string)db.GetSingleValue(Sql));
        }
        public string GetstatusChecking(string TRANSPORTERBILLID)
        {

            string Sql = "EXEC SP_TRANSPORTERBILL_DELETE_CHECKING '" + TRANSPORTERBILLID + "' ";
            return ((string)db.GetSingleValue(Sql));
        }
        public DataTable BindTransporterbill(string CBU, string Checker, string fromData, string ToDate)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_BIND_TRANSPORTERBILL '" + CBU + "','" + Checker + "','" + fromData + "','" + ToDate + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable EditTransporterbill(string ID)
        {
            string sql = "EXEC SP_TRANSPORTER_DETAILS '" + ID + "'";
            DataTable ds = new DataTable();
            ds = db.GetData(sql);
            return ds;
        }
        public DataTable BindLRNoByStock(string tag, string transporterid, string DEPOTID)
        {
            string sql = string.Empty;


            string Sql1 = "  DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '') + LRGRNO FROM T_TRANSPORTER_BILL_DETAILS INNER JOIN [T_TRANSPORTER_BILL_HEADER] " +
                "ON T_TRANSPORTER_BILL_HEADER.TRANSPORTERBILLID=T_TRANSPORTER_BILL_DETAILS.TRANSPORTERBILLID WHERE TRANSPORTERID='" + transporterid + "' AND T_TRANSPORTER_BILL_HEADER.DEPOTID='" + DEPOTID + "' " +
                 " select @Names";

            DataTable dt = new DataTable();
            dt = db.GetData(Sql1);
            string LRGR = "";

            if (dt.Rows.Count > 0)
            {
                LRGR = dt.Rows[0][0].ToString();
                // LRGRID = dt.Rows[0][1].ToString();

            }

            if (tag == "STKTRN")
            {
                //sql = "SELECT ISNULL(LRGRNO,' ')+' '+STOCKTRANSFERNO AS LRGRNO,LRGRNO AS LRID from T_STOCKTRANSFER_HEADER WHERE TRANSPORTERID='" + transporterid + "' "+
                //      "  AND LRGRNO <>'' and  ISNULL(LRGRNO,' ')+' '+STOCKTRANSFERNO  NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
                sql = "SELECT ISNULL(LRGRNO,' ') AS LRGRNO,STOCKTRANSFERID AS LRID from T_STOCKTRANSFER_HEADER WHERE TRANSPORTERID='" + transporterid + "' " +
                      "  /* AND LRGRNO <>'' and  ISNULL(LRGRNO,' ') NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) */ AND MOTHERDEPOTID='" + DEPOTID + "'";
            }
            else if (tag == "STKINV")
            {
                //sql = "SELECT ISNULL(LRGRNO,' ')+' '+SALEINVOICEPREFIX+'/'+SALEINVOICENO+'/'+SALEINVOICESUFFIX AS LRGRNO,LRGRNO AS LRID from T_SALEINVOICE_HEADER WHERE TRANSPORTERID='" + transporterid + "' AND LRGRNO <>'' "+
                //    " and  ISNULL(LRGRNO,' ')+' '+SALEINVOICEPREFIX+'/'+SALEINVOICENO+'/'+SALEINVOICESUFFIX NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
                sql = "SELECT ISNULL(LRGRNO,' ') AS LRGRNO,SALEINVOICEID AS LRID from T_SALEINVOICE_HEADER WHERE TRANSPORTERID='" + transporterid + "' /* AND LRGRNO <>'' " +
                    "   and  ISNULL(LRGRNO,' ') NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) */ and DEPOTID='" + DEPOTID + "'";
            }
            else if (tag == "STKDES")
            {
                //sql = "SELECT ISNULL(LRGRNO,' ')+' '+STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX AS LRGRNO,LRGRNO AS LRID from T_STOCKDESPATCH_HEADER WHERE TRANSPORTERID='" + transporterid + "'"+
                //    " AND LRGRNO <>'' and ISNULL(LRGRNO,' ')+' '+STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
                sql = "SELECT ISNULL(LRGRNO,' ') AS LRGRNO,STOCKDESPATCHID AS LRID from T_STOCKDESPATCH_HEADER WHERE TRANSPORTERID='" + transporterid + "'" +
                    " /* AND LRGRNO <>''  and ISNULL(LRGRNO,' ') NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',',')) */ and MOTHERDEPOTID='" + DEPOTID + "'";
            }
            // DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindLRNoByTransporterGroup(string tag, string transporterid)
        {
            string sql = string.Empty;
            string sqltransporter = string.Empty;

            sqltransporter = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '')+ TRANSPORTERID FROM M_TRANSPORTER_GROUP_DETAILS" +
                                    " WHERE GROUPID='" + transporterid + "'" +
                                    " select @Names";

            string Transporter = string.Empty;
            DataTable dttrans = db.GetData(sqltransporter);
            if (dttrans.Rows.Count > 0)
            {
                Transporter = dttrans.Rows[0][0].ToString();
            }

            string Sql1 = "  DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '') + LRGRNO FROM T_TRANSPORTER_BILL_DETAILS INNER JOIN [T_TRANSPORTER_BILL_HEADER] " +
                "ON T_TRANSPORTER_BILL_HEADER.TRANSPORTERBILLID=T_TRANSPORTER_BILL_DETAILS.TRANSPORTERBILLID WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',','))" +
                 " select @Names";

            DataTable dt = new DataTable();
            dt = db.GetData(Sql1);
            string LRGR = "";

            if (dt.Rows.Count > 0)
            {
                LRGR = dt.Rows[0][0].ToString();
                // LRGRID = dt.Rows[0][1].ToString();

            }

            if (tag == "STKTRN")
            {
                //sql = "SELECT ISNULL(LRGRNO,' ')+' '+STOCKTRANSFERNO AS LRGRNO,LRGRNO AS LRID from T_STOCKTRANSFER_HEADER WHERE TRANSPORTERID='" + transporterid + "' "+
                //      "  AND LRGRNO <>'' and  ISNULL(LRGRNO,' ')+' '+STOCKTRANSFERNO  NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
                sql = "SELECT ISNULL(LRGRNO,' ') AS LRGRNO,STOCKTRANSFERID AS LRID from T_STOCKTRANSFER_HEADER WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',','))" +
                      "  AND LRGRNO <>'' and  ISNULL(LRGRNO,' ') NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
            }
            else if (tag == "STKINV")
            {
                //sql = "SELECT ISNULL(LRGRNO,' ')+' '+SALEINVOICEPREFIX+'/'+SALEINVOICENO+'/'+SALEINVOICESUFFIX AS LRGRNO,LRGRNO AS LRID from T_SALEINVOICE_HEADER WHERE TRANSPORTERID='" + transporterid + "' AND LRGRNO <>'' "+
                //    " and  ISNULL(LRGRNO,' ')+' '+SALEINVOICEPREFIX+'/'+SALEINVOICENO+'/'+SALEINVOICESUFFIX NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
                sql = "SELECT ISNULL(LRGRNO,' ') AS LRGRNO,SALEINVOICEID AS LRID from T_SALEINVOICE_HEADER WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',',')) AND LRGRNO <>'' " +
                    " and  ISNULL(LRGRNO,' ') NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
            }
            else if (tag == "STKDES")
            {
                //sql = "SELECT ISNULL(LRGRNO,' ')+' '+STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX AS LRGRNO,LRGRNO AS LRID from T_STOCKDESPATCH_HEADER WHERE TRANSPORTERID='" + transporterid + "'"+
                //    " AND LRGRNO <>'' and ISNULL(LRGRNO,' ')+' '+STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
                sql = "SELECT ISNULL(LRGRNO,' ') AS LRGRNO,STOCKDESPATCHID AS LRID from T_STOCKDESPATCH_HEADER WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',','))" +
                    " AND LRGRNO <>'' and ISNULL(LRGRNO,' ') NOT IN (SELECT * from dbo.fnSplit('" + LRGR + "',','))";
            }
            // DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable Bindlrnowithrespectinvono(string tag, string invno)
        {
            string sql = string.Empty;
            if (tag == "STKTRN")
            {
                sql = "SELECT DISTINCT STOCKTRANSFERID AS LRID,ISNULL(LRGRNO,' ') AS LRGRNO FROM T_STOCKTRANSFER_HEADER" +
                    " WHERE STOCKTRANSFERID='" + invno + "' ";

            }
            else if (tag == "STKINV")
            {
                sql = "SELECT SALEINVOICEID as LRID,ISNULL(LRGRNO,' ') AS LRGRNO from T_SALEINVOICE_HEADER" +
                   " WHERE SALEINVOICEID='" + invno + "'";

            }
            else if (tag == "STKDES")
            {
                sql = "SELECT STOCKDESPATCHID as LRID,ISNULL(LRGRNO,' ') AS LRGRNO  from T_STOCKDESPATCH_HEADER" +
                      " WHERE STOCKDESPATCHID='" + invno + "'";

            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindAllInvoiceno(string tag, string transporterid, string DEPOTID)
        {
            string sql = string.Empty;


            if (tag == "STKTRN")
            {
                sql = "SELECT DISTINCT STOCKTRANSFERID AS INVID,STOCKTRANSFERNO AS INVNO FROM T_STOCKTRANSFER_HEADER" +
                    " WHERE TRANSPORTERID='" + transporterid + "' and MOTHERDEPOTID='" + DEPOTID + "' ";

            }
            else if (tag == "STKINV")
            {
                sql = " SELECT SALEINVOICEID as INVID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+' /'+SALEINVOICESUFFIX) AS INVNO from T_SALEINVOICE_HEADER" +
                      " WHERE TRANSPORTERID='" + transporterid + "' and DEPOTID='" + DEPOTID + "'" +
                      " UNION " +
                      " SELECT SALEINVOICEID as INVID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+' /'+SALEINVOICESUFFIX) AS INVNO from T_MM_SALEINVOICE_HEADER" +
                      " WHERE TRANSPORTERID='" + transporterid + "' and DEPOTID='" + DEPOTID + "'";

            }
            else if (tag == "STKDES")
            {
                sql = "SELECT STOCKDESPATCHID as INVID,(STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX)AS INVNO  from T_STOCKDESPATCH_HEADER" +
                      " WHERE TRANSPORTERID='" + transporterid + "' and MOTHERDEPOTID='" + DEPOTID + "' ";

            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindAllInvoicenoNEW(string tag, string transporterid, string DEPOTID)/*subho*/
        {
            string sql = string.Empty;
            string sqltransporter = string.Empty;


            sqltransporter = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '')+ STOCKBILLID FROM T_TRANSPORTER_BILL_DETAILS A INNER JOIN T_TRANSPORTER_BILL_HEADER B" +
                             " ON A.TRANSPORTERBILLID=B.TRANSPORTERBILLID WHERE DEPOTID='" + DEPOTID + "' AND B.TRANSPORTERID='" + transporterid + "' AND BILLTYPEID='" + tag + "'" +
                             " select @Names";


            DataTable dt1 = new DataTable();
            dt1 = db.GetData(sqltransporter);
            string STOCKTRANSFERID = "";

            if (dt1.Rows.Count > 0)
            {
                STOCKTRANSFERID = dt1.Rows[0][0].ToString();
                // LRGRID = dt.Rows[0][1].ToString();
            }

            if (tag == "STKTRN")
            {
                //sql = "SELECT DISTINCT STOCKTRANSFERID AS INVID,STOCKTRANSFERNO AS INVNO FROM T_STOCKTRANSFER_HEADER" +
                //    " WHERE TRANSPORTERID='" + transporterid + "' AND STOCKTRANSFERID NOT IN (SELECT * from dbo.fnSplit('" + STOCKTRANSFERID + "',',')) and MOTHERDEPOTID='" + DEPOTID + "' ";

                sql = "SELECT DISTINCT STOCKTRANSFERID AS INVID,CHALLANNO+'-('+STOCKTRANSFERNO+')'AS INVNO FROM T_STOCKTRANSFER_HEADER  WHERE STOCKTRANSFERID" +
                       "NOT IN(SELECT STOCKTRANSFERID FROM T_DEPORECEIVED_STOCK_HEADER A " +
                       "INNER JOIN  T_TRANSPORTER_BILL_DETAILS B ON A.STOCKDEPORECEIVEDID=B.STOCKBILLID)  AND MOTHERDEPOTID='" + DEPOTID + "'";


            }
            else if (tag == "STKINV")
            {
                //sql = "SELECT SALEINVOICEID as INVID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+' /'+SALEINVOICESUFFIX) AS INVNO from T_SALEINVOICE_HEADER" +
                //  " WHERE TRANSPORTERID='" + transporterid + "' AND SALEINVOICEID NOT IN (SELECT * from dbo.fnSplit('" + STOCKTRANSFERID + "',',')) AND DEPOTID='" + DEPOTID + "'";

                sql = " SELECT SALEINVOICEID as INVID,GETPASSNO+'-('+SALEINVOICEPREFIX+'/'+SALEINVOICENO+' /'+SALEINVOICESUFFIX+')' AS INVNO from T_SALEINVOICE_HEADER " +
                      " WHERE SALEINVOICEID NOT IN (SELECT STOCKBILLID FROM T_TRANSPORTER_BILL_DETAILS) AND DEPOTID='" + DEPOTID + "'";


            }
            else if (tag == "STKDES")
            {
                //sql = "SELECT STOCKDESPATCHID as INVID,(STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX)AS INVNO  from T_STOCKDESPATCH_HEADER" +
                //      " WHERE TRANSPORTERID='" + transporterid + "' AND STOCKDESPATCHID NOT IN (SELECT * from dbo.fnSplit('" + STOCKTRANSFERID + "',',')) and MOTHERDEPOTID='" + DEPOTID + "'";

                sql = "SELECT STOCKRECEIVEDID AS INVID,INVOICENO+' -('+STOCKRECEIVEDPREFIX+'/'+STOCKRECEIVEDNO+'/'+STOCKRECEIVEDSUFFIX+')' AS INVNO  from T_STOCKRECEIVED_HEADER " +
                      " WHERE STOCKRECEIVEDID NOT IN (SELECT STOCKBILLID FROM T_TRANSPORTER_BILL_DETAILS) and MOTHERDEPOTID='" + DEPOTID + "'";


            }
            /********************************INCLUDED BY SOURAV MUKHERJEE ON 15-11-2017************************/
            if (tag == "DPTRCVD")
            {

                //sql = "SELECT DISTINCT STOCKTRANSFERID AS INVID,STOCKTRANSFERNO AS INVNO FROM T_STOCKTRANSFER_HEADER" +
                //    " WHERE TRANSPORTERID='" + transporterid + "' AND STOCKTRANSFERID NOT IN (SELECT * from dbo.fnSplit('" + STOCKTRANSFERID + "',',')) and MOTHERDEPOTID='" + DEPOTID + "' ";


                sql = "SELECT DISTINCT STOCKDEPORECEIVEDID AS INVID, CHALLANNO + '-('+STOCKDEPORECEIVEDNO +')' AS INVNO FROM T_DEPORECEIVED_STOCK_HEADER " +
                       " WHERE STOCKTRANSFERID NOT IN (SELECT STOCKBILLID FROM T_TRANSPORTER_BILL_DETAILS) " +
                       " AND STOCKDEPORECEIVEDID NOT IN(SELECT STOCKBILLID FROM T_TRANSPORTER_BILL_DETAILS) AND RECEIVEDDEPOTID='" + DEPOTID + "' ";


            }
            /***************************************************************************************************/
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        /*TRANSPORTER ID IN WHERE CONDITION WAS REMOVED BY SOURAV MUKHERJEE ON 13-11-2017 04:22 PM AS PER INSTRUCTED BY MR. SANDIP BISWAS FOR POPULATING ALL INVOICES IRRESPECTIVE OF TRANSPORTER */
        public DataTable BindAllInvoicenoNEWV2(string tag, string transporterid, string DEPOTID, string transporterbillid)/*subho*/
        {
            string sql = string.Empty;
            string sqltransporter = string.Empty;
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();

            sqltransporter = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '')+ STOCKBILLID FROM T_TRANSPORTER_BILL_DETAILS  AS A " +
                             " INNER JOIN T_TRANSPORTER_BILL_HEADER AS B ON A.TRANSPORTERBILLID=B.TRANSPORTERBILLID WHERE DEPOTID='" + DEPOTID + "' AND B.TRANSPORTERID='" + transporterid + "' AND BILLTYPEID='" + tag + "'" +
                             " select @Names";

            DataTable dt1 = new DataTable();
            dt1 = db.GetData(sqltransporter);
            string STOCKTRANSFERID = "";

            if (dt1.Rows.Count > 0)
            {
                STOCKTRANSFERID = dt1.Rows[0][0].ToString();
                // LRGRID = dt.Rows[0][1].ToString();
            }
            sql = " EXEC [USP_BIND_TRANSPORTER_INVOICENO_FAC] '" + tag + "','" + DEPOTID + "','" + transporterbillid + "','" + finyear + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransGroupInvoiceno(string tag, string transporterid)
        {
            string sql = string.Empty;
            string sqltransporter = string.Empty;

            sqltransporter = " DECLARE @Names VARCHAR(MAX) SELECT @Names = COALESCE(@Names + ', ', '')+ TRANSPORTERID FROM M_TRANSPORTER_GROUP_DETAILS" +
                             " WHERE GROUPID='" + transporterid + "'" +
                             " select @Names";

            string Transporter = string.Empty;
            DataTable dttrans = db.GetData(sqltransporter);
            if (dttrans.Rows.Count > 0)
            {
                Transporter = dttrans.Rows[0][0].ToString();
            }
            if (tag == "STKTRN")
            {
                sql = "SELECT DISTINCT STOCKTRANSFERID AS INVID,STOCKTRANSFERNO AS INVNO FROM T_STOCKTRANSFER_HEADER" +
                    " WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',','))";
            }
            else if (tag == "STKINV")
            {
                sql = "SELECT SALEINVOICEID as INVID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+' /'+SALEINVOICESUFFIX) AS INVNO from T_SALEINVOICE_HEADER" +
                   " WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',','))";
            }
            else if (tag == "STKDES")
            {
                sql = "SELECT STOCKDESPATCHID as INVID,(STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX)AS INVNO  from T_STOCKDESPATCH_HEADER" +
                      " WHERE TRANSPORTERID IN (SELECT * from dbo.fnSplit('" + Transporter + "',','))";
            }
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BINDINVOICENOV2(string tag, string id)
        {
            string SQL = string.Empty;
            if (tag == "STKTRN")
            {
                SQL = " SELECT DISTINCT STOCKTRANSFERID AS INVID,STOCKTRANSFERNO AS INVNO FROM T_STOCKTRANSFER_HEADER" +
                      " WHERE STOCKTRANSFERID='" + id + "'";
            }
            else if (tag == "STKINV")
            {
                SQL = " SELECT SALEINVOICEID as INVID,(SALEINVOICEPREFIX+'/'+SALEINVOICENO+' /'+SALEINVOICESUFFIX) AS INVNO from T_SALEINVOICE_HEADER" +
                      " WHERE SALEINVOICEID='" + id + "'";
            }
            else if (tag == "STKDES")
            {
                SQL = " SELECT STOCKDESPATCHID as INVID,(STOCKDESPATCHPREFIX+'/'+STOCKDESPATCHNO+'/'+STOCKDESPATCHSUFFIX)AS INVNO " +
                      " FROM T_STOCKDESPATCH_HEADER WHERE STOCKDESPATCHID='" + id + "'";
            }
            DataTable DT = db.GetData(SQL);
            return DT;
        }
        public DataTable BindInvoiceNo(string tag, string LRNO)
        {
            string sql = string.Empty;
            {
                sql = "SELECT INVID,INVNO from Vw_LRNO WHERE LRGRNO='" + LRNO + "' AND TAG='" + tag + "'";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public string SaveTransporterbill(string ID, string Mode, string TransporterID, string TransporterName, string billdate, string billtype, string Remarks,
                                        string UserID, string finyear, string xml, string MenuId, decimal TOTALNETAMOUNT,
                                        decimal TOTALTDS, decimal TOTALGROSSWEIGHT, string depotid, string depotname, char IsTransferToHO, decimal TOTALBILLAMOUNT,
                                        decimal TOTALTDSDEDUCTABLE, decimal sumcgst, decimal sumsgst, decimal sumigst, decimal sumugst, string BILLINGFROMSTATEID, string Reversecharge)
        {
            string transporterbillno = string.Empty;
            string sql = string.Empty;

            sql = "EXEC SP_TRANSPORTERBILL_INSERT_UPDATE_GST '" + ID + "','" + Mode + "','" + TransporterID + "','" + TransporterName + "','" + billdate + "','" + billtype + "' ," +
                  " '" + Remarks + "','" + UserID + "','" + finyear + "','" + xml + "','" + MenuId + "'," + TOTALNETAMOUNT + "," +
                  " " + TOTALTDS + "," + TOTALGROSSWEIGHT + ",'" + depotid + "','" + depotname + "'," + IsTransferToHO + "," +
                  " " + TOTALBILLAMOUNT + "," + TOTALTDSDEDUCTABLE + "," + sumcgst + "," + sumsgst + "," + sumigst + "," + sumugst + ",'" + BILLINGFROMSTATEID + "','" + Reversecharge + "'";
            transporterbillno = (string)db.GetSingleValue(sql);
            return transporterbillno;
        }
        public int DeleteTransporterbill(string TransporterbillID)
        {
            string sql = string.Empty;
            int d = 0;
            try
            {
                sql = "EXEC SP_TRANSPORTERBILL_DELETE '" + TransporterbillID + "'";
                d = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "");
            }
            return d;
        }

        public decimal Billamt()
        {
            string amt = "SELECT SERVICE_TAX_EXEMPTION FROM P_APPMASTER";
            decimal getamt = (decimal)db.GetSingleValue(amt);
            return getamt;
        }

        public decimal TDSPercentage(string transporterid, decimal Value)
        {
            string sql = string.Empty;
            decimal tds = 0;
            string TDS_ACC_LEADGER = "";
            TDS_ACC_LEADGER = (string)db.GetSingleValue("SELECT TDS_194C_ACC_LEADGER FROM P_APPMASTER");
            sql = " SELECT CASE WHEN ISNULL(ISTDSDECLARE,'Y')='Y' THEN (SELECT dbo.fn_TaxEvalute_Other('" + TDS_ACC_LEADGER + "','" + transporterid + "','0','0','9A555D40-5E12-4F5C-8EE0-E085B5BAB169'," + Value.ToString() + " )) " +
                  " ELSE '0' END AS PERCENTAGE FROM M_TPU_TRANSPORTER  WHERE ID='" + transporterid + "' ";

            tds = (decimal)db.GetSingleValue(sql);
            return tds;
        }

        public string TDSid(string transporterid, decimal Value)
        {
            string sql = string.Empty;
            string sqlchk = string.Empty;
            decimal tds = 0;
            string TDSID = "";

            string TDS_ACC_LEADGER = "";

            TDS_ACC_LEADGER = (string)db.GetSingleValue("SELECT TDS_ACC_LEADGER FROM P_APPMASTER");

            sql = " SELECT CASE WHEN ISNULL(ISTDSDECLARE,'Y')='Y' THEN (SELECT dbo.fn_TaxEvalute_Other('" + TDS_ACC_LEADGER + "','0','0','0','0'," + Value.ToString() + " )) " +
                  " ELSE '0' END AS PERCENTAGE FROM M_TPU_TRANSPORTER  WHERE ID='" + transporterid + "' ";

            tds = (decimal)db.GetSingleValue(sql);

            sqlchk = "SELECT ID FROM M_TAX WHERE PERCENTAGE =" + tds + "";
            TDSID = (string)db.GetSingleValue(sqlchk);
            return TDSID;
        }
        public string TDSidFROMAPP(string transporterid, decimal Value)
        {
            string sql = string.Empty;
            string TDS_ACC_LEADGER = "";

            TDS_ACC_LEADGER = (string)db.GetSingleValue("SELECT TDS_194C_ACC_LEADGER FROM P_APPMASTER");
            return TDS_ACC_LEADGER;
        }
        public string ServiceTaxPercentageid(string transporterid, decimal Value)
        {
            string sql = string.Empty;
            string sqlchk = string.Empty;
            decimal tds = 0;
            string ServiceTaxPercentageid = "";

            string TDS_ACC_LEADGER = "";

            TDS_ACC_LEADGER = (string)db.GetSingleValue("SELECT SERVICETAX_TRANSPORT_ACC_LEDGER FROM P_APPMASTER");

            sql = " SELECT CASE WHEN ISNULL(SERVICETAX,'Y')='Y' THEN (SELECT dbo.fn_TaxEvalute_Other('" + TDS_ACC_LEADGER + "','0','0','0','0'," + Value.ToString() + " )) " +
              " ELSE '0' END AS PERCENTAGE FROM M_TPU_TRANSPORTER  WHERE ID='" + transporterid + "' ";

            tds = (decimal)db.GetSingleValue(sql);

            sqlchk = "SELECT ID FROM M_TAX WHERE PERCENTAGE =" + tds + "";
            ServiceTaxPercentageid = (string)db.GetSingleValue(sqlchk);
            return ServiceTaxPercentageid;
        }

        public decimal ServiceTaxPercentage(string transporterid, decimal Value)
        {
            string sql = string.Empty;
            decimal servicetax = 0;
            string TDS_ACC_LEADGER = "";
            TDS_ACC_LEADGER = (string)db.GetSingleValue("SELECT SERVICETAX_TRANSPORT_ACC_LEDGER FROM P_APPMASTER");
            sql = " SELECT CASE WHEN ISNULL(SERVICETAX,'Y')='Y' THEN (SELECT dbo.fn_TaxEvalute_Other('" + TDS_ACC_LEADGER + "','0','0','0','0'," + Value.ToString() + " )) " +
                " ELSE '0' END AS PERCENTAGE FROM M_TPU_TRANSPORTER  WHERE ID='" + transporterid + "' ";

            servicetax = (decimal)db.GetSingleValue(sql);
            return servicetax;
        }

        public int RejectTransporterBill(string BillID, string Note)
        {
            int result = 0;
            string sql = string.Empty;
            sql = " UPDATE T_TRANSPORTER_BILL_HEADER SET ISVERIFIED = 'R',VERIFIEDDATETIME = GETDATE(),NOTE = '" + Note + "'" +
                  " WHERE TRANSPORTERBILLID ='" + BillID + "'";
            result = db.HandleData(sql);

            if (result == 0)
            {
                result = 0;  // update unsuccessfull
            }
            else
            {
                result = 1;  // update successfull
            }

            return result;
        }
        public DataTable Bindlrgrnopopule()
        {
            string sql = string.Empty;
            {
                sql = "select LRGRNO from T_TRANSPORTER_BILL_DETAILS'";
            }

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public decimal RoundOff(decimal Value)
        {
            string sql = string.Empty;
            decimal returnvalue = 0;

            sql = " SELECT [dbo].[fn_RoundOff](" + Value + ") ";
            returnvalue = (decimal)db.GetSingleValue(sql);
            return returnvalue;
        }

        public DataTable BindGstPercentage(string deotid, string tranporterid, decimal value, string InvoiceId, string BillingType, int Fromstate, int Tostate)
        {
            string sql = " EXEC USP_TAXCALCULATE '" + deotid + "','" + tranporterid + "'," + value + ",'" + InvoiceId + "','" + BillingType + "','" + Fromstate + "','" + Tostate + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable AllTransporter()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "";

                sql = " SELECT '1' as ID,'---- GROUP ----' as NAME,'1' AS SEQUENCE  UNION  SELECT GROUPID +'(GR)',GROUPNAME AS NAME,'2' AS SEQUENCE FROM M_TRANSPORTER_GROUP_HEADER" +
                      " UNION" +
                      " SELECT '2' as ID,'---- OTHERS ----' as NAME,'3' AS SEQUENCE  UNION  SELECT CAST(ID AS VARCHAR(50)) AS ID,NAME  AS NAME,'4' AS SEQUENCE   FROM M_TPU_TRANSPORTER" +
                      " ORDER BY SEQUENCE,NAME";

                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindTransporterbyGroup(string transporterid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            string sqltransporter = string.Empty;
            sql = "SELECT TRANSPORTERID AS ID,TRANSPORTERNAME AS NAME FROM M_TRANSPORTER_GROUP_DETAILS WHERE GROUPID='" + transporterid + "' ORDER BY TRANSPORTERNAME";
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindGroupbyTransporter(string transporterid)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            string sqltransporter = string.Empty;
            sql = "SELECT GROUPID FROM M_TRANSPORTER_GROUP_DETAILS WHERE TRANSPORTERID='" + transporterid + "'";
            string GROUPID = Convert.ToString(db.GetSingleValue(sql));

            string sqlfinal = "SELECT TRANSPORTERID AS ID,TRANSPORTERNAME AS NAME FROM M_TRANSPORTER_GROUP_DETAILS WHERE GROUPID='" + GROUPID + "' ORDER BY TRANSPORTERNAME";
            dt = db.GetData(sqlfinal);
            return dt;
        }
        public string BindLRGRbytransporter(string LRGRNO, string Invoicetype)
        {
            string sql = string.Empty;
            string lrgrno = string.Empty;
            string sqltransporter = string.Empty;
            if (Invoicetype == "STKINV")
            {
                sql = "SELECT TRANSPORTERID FROM T_SALEINVOICE_HEADER WHERE SALEINVOICEID='" + LRGRNO + "'";
            }
            if (Invoicetype == "STKTRN")
            {
                sql = "SELECT TRANSPORTERID FROM T_STOCKTRANSFER_HEADER WHERE STOCKTRANSFERID='" + LRGRNO + "'";
            }
            if (Invoicetype == "STKDES")
            {
                sql = "SELECT TRANSPORTERID FROM T_STOCKDESPATCH_HEADER WHERE STOCKDESPATCHID='" + LRGRNO + "'";
            }
            lrgrno = Convert.ToString(db.GetSingleValue(sql));
            return lrgrno;
        }
        public int updatetransporter(string LRGRNO, string Invoicetype, string transporterid)
        {
            string sql = string.Empty;
            int lrgrno = 0;
            string sqltransporter = string.Empty;
            if (Invoicetype == "STKINV")
            {
                sql = "UPDATE T_SALEINVOICE_HEADER SET TRANSPORTERID ='" + transporterid + "' where SALEINVOICEID='" + LRGRNO + "'";
            }
            if (Invoicetype == "STKTRN")
            {
                sql = "UPDATE T_STOCKTRANSFER_HEADER SET TRANSPORTERID ='" + transporterid + "' where STOCKTRANSFERID='" + LRGRNO + "'";
            }
            if (Invoicetype == "STKDES")
            {
                sql = "UPDATE T_STOCKDESPATCH_HEADER SET TRANSPORTERID ='" + transporterid + "' where STOCKDESPATCHID='" + LRGRNO + "'";
            }
            lrgrno = db.HandleData(sql);
            return lrgrno;
        }

        public DataTable BindTransporterbillApproval(string CBU, string Checker, string fromData, string ToDate, string Depotid)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            if (Convert.ToString(HttpContext.Current.Session["USERTYPE"]).Trim() == "8977E291-5CEE-40A5-91D1-55A179EB6DCE")
            {
                sql = " SELECT A.TRANSPORTERBILLID AS SALEINVOICEID,TRANSPORTERBILLNO AS SALEINVOICENO,CONVERT(VARCHAR(10),CAST(TRANSPORTERBILLDATE AS DATE),103) AS SALEINVOICEDATE,TRANSPORTERNAME AS DISTRIBUTORNAME, " +
                      " CASE WHEN BILLTYPEID LIKE '%STKINV%' THEN 'SALE INVOICE' WHEN BILLTYPEID LIKE 'STKTRN' THEN 'STOCK TRANSFER' " +
                      " WHEN BILLTYPEID LIKE 'STKDES' THEN 'TPU DESPATCH' ELSE 'DEPOT RECEIVED' END AS 'BILLTYPE',CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                      " ISNULL(F.TOTALGROSSWEIGHT,0.00) AS TOTALGROSSWEIGHT,ISNULL(F.TOTALBILLAMOUNT,0.00) AS TOTALBILLAMOUNT,ISNULL(F.TOTALTDS,0.00)AS TOTALTDS," +
                      " ISNULL(F.TOTALSERVICETAX,0.00) + ISNULL(F.TOTALCGST,0.00) + ISNULL(F.TOTALSGST,0.00) + ISNULL(F.TOTALIGST,0.00) + ISNULL(F.TOTALUGST,0.00) + ISNULL(F.TOTALIGST,0.00)AS TAXVALUE ," +
                      " ISNULL(F.TOTALNETAMOUNT,0.00)AS TOTALSALEINVOICEVALUE,'' AS GETPASSNO,0 AS TOTALSPECIALDISCVALUE,0 AS GROSSREBATEVALUE,0 AS ROUNDOFFVALUE,ISNULL(REVERSECHARGE,'N') AS ISREVERSEAPPLICABLE FROM T_TRANSPORTER_BILL_HEADER A " +
                      " INNER JOIN T_TRANSPORTER_BILL_FOOTER F ON A.TRANSPORTERBILLID=F.TRANSPORTERBILLID WHERE ISVERIFIED <> 'Y'AND A.NEXTLEVELID='" + CBU + "' " +
                      " AND CONVERT(DATE,TRANSPORTERBILLDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromData + "') AND DBO.Convert_To_ISO('" + ToDate + "') AND  DEPOTID='" + Depotid + "'" +
                      " ORDER BY CONVERT(DATETIME, TRANSPORTERBILLDATE ) ASC";
            }
            else
            {
                sql = " SELECT A.TRANSPORTERBILLID AS SALEINVOICEID,TRANSPORTERBILLNO AS SALEINVOICENO,CONVERT(VARCHAR(10),CAST(TRANSPORTERBILLDATE AS DATE),103) AS SALEINVOICEDATE,TRANSPORTERNAME AS DISTRIBUTORNAME, " +
                      " CASE WHEN BILLTYPEID LIKE '%STKINV%' THEN 'SALE INVOICE' WHEN BILLTYPEID LIKE 'STKTRN' THEN 'STOCK TRANSFER' " +
                      " WHEN BILLTYPEID LIKE 'STKDES' THEN 'TPU DESPATCH' ELSE 'DEPOT RECEIVED' END AS 'BILLTYPE',CASE WHEN ISVERIFIED='N' THEN 'PENDING'  WHEN ISVERIFIED='R' THEN 'REJECTED' WHEN ISVERIFIED='H' THEN 'HOLD' ELSE 'APPROVED' END AS ISVERIFIEDDESC," +
                      " ISNULL(F.TOTALGROSSWEIGHT,0.00) AS TOTALGROSSWEIGHT,ISNULL(F.TOTALBILLAMOUNT,0.00) AS TOTALBILLAMOUNT,ISNULL(F.TOTALTDS,0.00)AS TOTALTDS," +
                      " ISNULL(F.TOTALSERVICETAX,0.00) + ISNULL(F.TOTALCGST,0.00) + ISNULL(F.TOTALSGST,0.00) + ISNULL(F.TOTALIGST,0.00) + ISNULL(F.TOTALUGST,0.00) + ISNULL(F.TOTALIGST,0.00)AS TAXVALUE ," +
                      " ISNULL(F.TOTALNETAMOUNT,0.00)AS TOTALSALEINVOICEVALUE,'' AS GETPASSNO,0 AS TOTALSPECIALDISCVALUE,0 AS GROSSREBATEVALUE,0 AS ROUNDOFFVALUE,ISNULL(REVERSECHARGE,'N') AS ISREVERSEAPPLICABLE FROM T_TRANSPORTER_BILL_HEADER A " +
                      " INNER JOIN T_TRANSPORTER_BILL_FOOTER F ON A.TRANSPORTERBILLID=F.TRANSPORTERBILLID WHERE ISVERIFIED <> 'Y' " +
                      " AND CONVERT(DATE,TRANSPORTERBILLDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromData + "') AND DBO.Convert_To_ISO('" + ToDate + "') AND  DEPOTID='" + Depotid + "'" +
                      " ORDER BY CONVERT(DATETIME, TRANSPORTERBILLDATE ) ASC";
            }
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindTransporterbyInvoice(string TAG, string InvoiceID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            if (TAG == "STKINV")
            {
                sql = " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_SALEINVOICE_HEADER A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE SALEINVOICEID='" + InvoiceID + "'" +
                      " UNION " +
                      " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_MM_SALEINVOICE_HEADER A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE SALEINVOICEID='" + InvoiceID + "'";
            }
            else if (TAG == "STKTRN")
            {
                sql = " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_STOCKTRANSFER_HEADER A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE STOCKTRANSFERID='" + InvoiceID + "'" +

                      /*Add By Rajeev (31-08-2018) For Invoice Wise Transporter Name*/
                      " UNION ALL" +

                      " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_STOCKDESPATCH_HEADER AS A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE STOCKDESPATCHID='" + InvoiceID + "'";
            }
            else if (TAG == "DPTRCVD")
            {
                sql = " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_DEPORECEIVED_STOCK_HEADER A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE STOCKDEPORECEIVEDID='" + InvoiceID + "'";
            }
            else
            {
                sql = " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_STOCKRECEIVED_HEADER A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE STOCKRECEIVEDID='" + InvoiceID + "'" +
                      " UNION " +
                      " SELECT TRANSPORTERID,NAME AS TRANSPORTERNAME,LRGRNO," +
                      " CASE WHEN CONVERT(VARCHAR(10), CAST(LRGRDATE AS DATE),103)='01/01/1900' THEN CONVERT(VARCHAR(10),CAST(GETDATE() AS DATE),103)" +
                      " ELSE " +
                      " CONVERT(VARCHAR(10),CAST(LRGRDATE AS DATE),103) END AS LRGRDATE" +
                      " FROM T_SALERETURN_HEADER A INNER JOIN M_TPU_TRANSPORTER B ON A.TRANSPORTERID=B.ID WHERE SALERETURNID='" + InvoiceID + "'";
            }

            dt = db.GetData(sql);
            return dt;
        }
        public string SaveTransporterbillV2(string ID, string Mode, string TransporterID, string TransporterName, string billdate, string billtype, string Remarks,
                                            string UserID, string finyear, string xml, string MenuId, decimal TOTALNETAMOUNT, decimal TOTALTDS, decimal TOTALGROSSWEIGHT,
                                             string depotid, string depotname, char IsTransferToHO, decimal TOTALBILLAMOUNT, decimal TOTALTDSDEDUCTABLE, decimal sumcgst,
                                             decimal sumsgst, decimal sumigst, decimal sumugst, string BILLINGFROMSTATEID, string Reversecharge, string GNGNO,
                                             string TDSAPPLICABLE, decimal TDSPECENTAGE, string TDSID, string VIRTUALdepotid, string EXPORTTAG, string ReasonID, string GSTReasonID)
        {
            string transporterbillno = string.Empty;
            string sql = string.Empty;

            sql = "EXEC SP_TRANSPORTERBILL_INSERT_UPDATE_GST_V2_FAC '" + ID + "','" + Mode + "','" + TransporterID + "','" + TransporterName + "','" + billdate + "','" + billtype + "' ," +
                  " '" + Remarks + "','" + UserID + "','" + finyear + "','" + xml + "','" + MenuId + "'," + TOTALNETAMOUNT + "," +
                  " " + TOTALTDS + "," + TOTALGROSSWEIGHT + ",'" + depotid + "','" + depotname + "'," + IsTransferToHO + "," +
                  " " + TOTALBILLAMOUNT + "," + TOTALTDSDEDUCTABLE + "," + sumcgst + "," + sumsgst + "," + sumigst + "," +
                  " " + sumugst + ",'" + BILLINGFROMSTATEID + "','" + Reversecharge + "','" + GNGNO + "','" + TDSAPPLICABLE + "', " +
                  " " + TDSPECENTAGE + ",'" + TDSID + "','" + VIRTUALdepotid + "','" + EXPORTTAG + "','" + ReasonID + "','" + GSTReasonID + "'";
            transporterbillno = (string)db.GetSingleValue(sql);
            return transporterbillno;
        }
        public int UpdateLrGrNoFromTransporterBill(string depoptid, string xml, string MODULEID)
        {
            int result = 0;
            string sql = "EXEC [USP_UPDATE_LRGRNO_FROM_TRANSPORTERBILL] '" + depoptid + "','" + xml + "','" + MODULEID + "'";
            DataSet ds = new DataSet();
            result = db.HandleData(sql);
            return result;
        }
        public DataTable checkLRGRNO(string TAG, string transporterID, string LRGRNO)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "SELECT LRGRNO FROM T_TRANSPORTER_BILL_DETAILS WHERE TRANSPORTERID='" + transporterID + "' AND LRGRNO='" + LRGRNO + "'";
            dt = db.GetData(sql);
            return dt;
        }
        public string checkLRGRNOV2(string TAG, string transporterID, string LRGRNO, string TRANSPORETERBILLID, string INVID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            string finyear = HttpContext.Current.Session["FINYEAR"].ToString().Trim();
            sql = " EXEC USP_LRGRCHECKING '" + TAG + "','" + transporterID + "','" + LRGRNO + "','" + TRANSPORETERBILLID + "','" + INVID + "','" + finyear + "'";
            return ((string)db.GetSingleValue(sql));
        }

        public decimal Edittdsid(string transporterid)
        {
            string sql = string.Empty;
            decimal TDSPERCENTRAGE = 0;
            sql = " SELECT TOP 1 TDSPERCENTRAGE  from T_TRANSPORTER_BILL_DETAILS WHERE TRANSPORTERBILLID='" + transporterid + "' ";
            TDSPERCENTRAGE = (decimal)db.GetSingleValue(sql);
            return TDSPERCENTRAGE;
        }
        public string GetReverseCharge(string transporterid)
        {
            string sql = string.Empty;
            string tag = string.Empty;
            sql = "SELECT ISNULL(REVERSECHARGE,'N') AS REVERSECHARGE FROM M_TPU_TRANSPORTER WHERE ID='" + transporterid + "'";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }
        public string GetIsTransferToHo(string transporterid)
        {
            string sql = string.Empty;
            string tag = string.Empty;
            sql = "SELECT ISNULL(ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO FROM M_TPU_TRANSPORTER WHERE ID='" + transporterid + "'";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }
        public DataTable BindExportDepot()
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = " SELECT KOLKATADEPOTID AS BRID,'KOLKATA' AS BRPREFIX FROM P_APPMASTER" +
                  " UNION ALL" +
                  " SELECT BRID,BRPREFIX FROM M_BRANCH WHERE ISUSEDFOREXPORT='Y' AND BRANCHTAG <> 'O'" +
                  " ORDER BY BRPREFIX";
            dt = db.GetData(sql);
            return dt;
        }
        public string ExportDepotChecking(string DEPOTID)
        {
            string sql = string.Empty;
            string tag = string.Empty;

            sql = " IF EXISTS (SELECT 1 FROM M_BRANCH WHERE EXPORT='Y'  AND BRID='" + DEPOTID + "') " +
             " BEGIN " +
             " 	SELECT '1' " +
             " END " +
             " ELSE " +
             " BEGIN " +
             "   SELECT '0' " +
             " END";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }

        public string ExportDepotCheckingEdit(string DEPOTID)
        {
            string sql = string.Empty;
            string tag = string.Empty;

            sql = " IF EXISTS (SELECT 1 FROM M_BRANCH WHERE ISUSEDFOREXPORT='Y'  AND BRID='" + DEPOTID + "') " +
             " BEGIN " +
             " 	SELECT '1' " +
             " END " +
             " ELSE " +
             " BEGIN " +
             "   SELECT '0' " +
             " END";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }

        public string ExportDepotInvoiceEdit(string TAG, string TRANSPORTERBILLID)
        {
            string sql = string.Empty;
            string depotid = string.Empty;

            sql = " USP_BINDDEPOTFOREXPORT '" + TAG + "','" + TRANSPORTERBILLID + "'";
            depotid = (string)db.GetSingleValue(sql);
            return depotid;
        }
        public string GetTdscheck(string transporterid)
        {
            string sql = string.Empty;
            string tag = string.Empty;
            sql = "SELECT ISNULL(ISTDSDECLARE,'N') AS REVERSECHARGE FROM M_TPU_TRANSPORTER WHERE ID='" + transporterid + "'";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }
        public DataTable BindReason(string MENUID)
        {
            string sql = string.Empty;
            DataTable dt = new DataTable();
            sql = "SELECT REASONID,NAME FROM M_REASON_MENU_MAPPING A INNER JOIN M_REASON B ON A.REASONID=B.ID WHERE MENUID='" + MENUID + "' ORDER BY NAME";
            dt = db.GetData(sql);
            return dt;
        }
        public string GetRCMheck(string transporterid)
        {
            string sql = string.Empty;
            string tag = string.Empty;
            sql = "SELECT ISNULL(REVERSECHARGE,'N') AS REVERSECHARGE FROM M_TPU_TRANSPORTER WHERE ID='" + transporterid + "'";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }

        public string GetAccEntryID(string transporterbillid)
        {
            string sql = string.Empty;
            string tag = string.Empty;
            sql = "select isnull(AccEntryID,'') as AccEntryID from acc_voucher_invoice_map where InvoiceID='" + transporterbillid + "'";
            tag = (string)db.GetSingleValue(sql);
            return tag;
        }
        public string GetToHOStatus(string TRANSPORTERBILLID)
        {
            string qury = " select isnull(ISTRANSFERTOHO,'N') AS ISTRANSFERTOHO from T_TRANSPORTER_BILL_HEADER where TRANSPORTERBILLID='" + TRANSPORTERBILLID + "'";
            string ISTRANSFERTOHO = (string)db.GetSingleValue(qury);
            return ISTRANSFERTOHO;
        }

        public DataTable BindTransporterbillall(string depotid, string fromData, string ToDate, string FinYear)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            sql = "EXEC USP_BIND_TRANSPORTERBILL_FORALLUSER_FAC '" + depotid + "','" + fromData + "','" + ToDate + "','" + FinYear + "'";
            dt = db.GetData(sql);
            return dt;
        }

        public string GetOfflineStatus(string BRID)
        {
            string qury = " SELECT ISNULL(OFFLINE,'') AS OFFLINE FROM M_BRANCH WHERE BRID='" + BRID + "'";
            string OFFLINE = (string)db.GetSingleValue(qury);
            return OFFLINE;
        }
        public DataTable BindGstPercentage_New(string deotid, string tranporterid, decimal value, string InvoiceId, string BillingType, int Fromstate, int Tostate, string RCM)
        {
            string sql = " EXEC USP_TAXCALCULATE_NEW '" + deotid + "','" + tranporterid + "'," + value + ",'" + InvoiceId + "','" + BillingType + "','" + Fromstate + "','" + Tostate + "','" + RCM + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        /* Add 01/01/2019 By D.Mondal*/
        public string GetRCMheck_bill(string TRANSPORTERBILLID)
        {
            string qury = " select isnull(REVERSECHARGE,'N') AS REVERSECHARGE from T_TRANSPORTER_BILL_HEADER where TRANSPORTERBILLID='" + TRANSPORTERBILLID + "'";
            string ISTRANSFERTOHO = (string)db.GetSingleValue(qury);
            return ISTRANSFERTOHO;
        }

    }
}
