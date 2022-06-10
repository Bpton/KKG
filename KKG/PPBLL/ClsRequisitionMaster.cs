using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace PPBLL
{
    public class ClsRequisitionMaster
    {
        DBUtils db = new DBUtils();
        public DataTable BindSuppliedItem()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT ID AS ITEMID,ITEM_Name AS ITEMNAME from M_SUPLIEDITEM WHERE ACTIVE='Y' ORDER BY ITEMNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable BindSuppliedItemmodewise(string mode)/*add by p.basu for mode wise supplieditem on 25-11-2020*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "USP_BIND_SUPPLIEDITEM_MODE_WISE'" + mode + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable BindDepartment()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT DEPTID,DEPTNAME FROM M_DEPARTMENT  Order BY DEPTNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable LoadMaterial(string ItemID)/*new add by p.basu for product category wise 24-11-2020*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "USP_BIND_MATERIAL_CATEGORY_WISE '" + ItemID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable FetchBomDetailsForView(string ItemID,string storeId,decimal qty)/*new add by p.basu for product category wise 24-11-2020*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "USP_BIND_FETCH_DETAILS '" + ItemID + "','" + storeId + "',"+ qty + "";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable LoadMaterial(string ItemID,string userid)/*new add by p.basu for product category wise 24-11-2020*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "USP_BIND_FG_STORE_WISE '" + ItemID + "','"+ userid + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable LoadUnit()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT UOMID,UOMNAME FROM M_UOM ORDER BY UOMNAME";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable LoadRequisition(string fromdate, string todate, string UserID, string DepotID, string FinYear)
        {
            DataTable dt = new DataTable();
            try
            {
                String sql = "EXEC USP_BIND_REQUISITION '" + fromdate + "','" + todate + "','" + UserID + "','" + DepotID + "','" + FinYear + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }


        public DataTable LoadIndent(string fromdate, string todate, string UserID, string isfromRequisition)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Empty;

                if (isfromRequisition == "Y")
                {
                    sql = " SELECT A.INDENTID,INDENTPREFIX + INDENTNUM + INDENTSUFFIX AS INDENTNO,CONVERT(VARCHAR(10),CAST(INDENTDATE AS DATE),103) AS INDENTDATE," +
                                " COUNT(B.MATERIALID) AS TOTALMATERIAL FROM T_INDENT_HEADER A INNER JOIN T_INDENT_DETAILS B ON A.INDENTID=B.INDENTID" +
                                " WHERE CREATEDBY='" + UserID + "' AND ISFROMREQUISITION='Y' AND CONVERT(DATE,INDENTDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                                " GROUP BY A.INDENTID,INDENTPREFIX,INDENTNUM,INDENTSUFFIX,INDENTDATE ORDER BY INDENTDATE DESC";
                }
                else
                {
                    //MRP_PREFIX+'/'+MRP_NO+'/'+MRP_SUFFIX
                    sql = " SELECT A.INDENTID,INDENTPREFIX+ '/' + INDENTNUM +'/'+ INDENTSUFFIX AS INDENTNO,CONVERT(VARCHAR(10),CAST(INDENTDATE AS DATE),103) AS INDENTDATE," +
                                " COUNT(B.MATERIALID) AS TOTALMATERIAL,A.MRPNO FROM T_INDENT_HEADER A INNER JOIN T_INDENT_DETAILS B ON A.INDENTID=B.INDENTID" +
                                " WHERE CREATEDBY='" + UserID + "' AND ISFROMREQUISITION='N' AND CONVERT(DATE,INDENTDATE,103) BETWEEN DBO.Convert_To_ISO('" + fromdate + "') AND DBO.Convert_To_ISO('" + todate + "')" +
                                " GROUP BY A.INDENTID,INDENTPREFIX,INDENTNUM,INDENTSUFFIX,INDENTDATE,A.MRPNO ORDER BY INDENTDATE DESC";
                }
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataTable LoadRequisitionDetails(string fromdate, string todate, string UserID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = " SELECT A.INDENTID,INDENTPREFIX + INDENTNUM + INDENTSUFFIX AS INDENTNO,CONVERT(VARCHAR(10),CAST(INDENTDATE AS DATE),103) AS INDENTDATE," +
                             " COUNT(B.MATERIALID) AS TOTALMATERIAL FROM T_INDENT_HEADER A INNER JOIN T_INDENT_DETAILS B ON A.INDENTID=B.INDENTID" +
                             " WHERE CREATEDBY='" + UserID + "' GROUP BY A.INDENTID,INDENTPREFIX,INDENTNUM,INDENTSUFFIX,INDENTDATE ORDER BY INDENTDATE DESC";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataSet EditRequisition(string RequisitionID)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "EXEC [USP_T_REQUISITION_EDIT] '" + RequisitionID + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return ds;
        }

        public DataSet EditIndent(string IndentID)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "EXEC [USP_T_INDENT_EDIT] '" + IndentID + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return ds;
        }

        public string InsertRequisitionDetails(string requisitionid, string RequisitionDate, string remarks, string createdby, string finyear, string xml,
                                                string departid, string departname, string FactorID,string storeId)
        {
            string requisitionno = string.Empty;
            try
            {
                string sql = "EXEC [USP_T_REQUISITION_INSERT_UPDATE] '" + requisitionid + "','" + RequisitionDate + "','" + remarks + "','" + createdby + "'," +
                                " '" + finyear + "','" + xml + "','" + departid + "','" + departname + "','" + FactorID + "','"+ storeId + "'";
                DataTable dt = db.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    requisitionno = dt.Rows[0]["REQUISITIONNO"].ToString();
                }
                else
                {
                    requisitionno = "";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return requisitionno;
        }
        public string InsertIndentDetails(string Indentid, string IndentDate, char IsFromRequisition, string remarks, string createdby, string finyear, string xml, string SelectedRequisition, string FactoryID)
        {
            string Indentno = string.Empty;
            try
            {
                string sql = "EXEC [USP_T_INDENT_INSERT_UPDATE] '" + Indentid + "','" + IndentDate + "','" + IsFromRequisition + "','" + remarks + "','" + createdby + "','" + finyear + "','" + xml + "','" + SelectedRequisition + "','" + FactoryID + "'";
                DataTable dt = db.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    Indentno = dt.Rows[0]["INDENTNO"].ToString();
                }
                else
                {
                    Indentno = "";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return Indentno;
        }
        public int DeleteRequisition(string Requisitionid)
        {
            int delflag = 0;
            try
            {
                string sql = "EXEC [USP_T_REQUISITION_DELETE] '" + Requisitionid + "'";

                int e = db.HandleData(sql);

                if (e == 0)
                {
                    delflag = 0;  // delete unsuccessfull
                }
                else
                {
                    delflag = 1;  // delete successfull
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return delflag;
        }
        public int DeleteIndent(string Indentid)
        {
            int delflag = 0;
            try
            {
                string sql = "EXEC [USP_T_INDENT_DELETE] '" + Indentid + "'";

                int e = db.HandleData(sql);

                if (e == 0)
                {
                    delflag = 0;  // delete unsuccessfull
                }
                else
                {
                    delflag = 1;  // delete successfull
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return delflag;
        }

        public DataTable RequisitionDetails(string RequisitionID)
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "EXEC [USP_REQUISITION_DETAILS] '" + RequisitionID + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        /*production unit start by p.basu on 21-11-2020 fort kkg*/

        public DataTable LoadUnit_Productwise(string mode, string productId)/*productwise uom bind*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "USP_PRODUCTION_MODULE_KKG_MODE_WISE '" + mode + "','" + productId + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable getStockQtyProductWise(string depotid, string productId, string storelocationid)/*productwise uom bind*/
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT dbo.fn_Rpt_Stock('" + depotid + "','" + productId + "','" + storelocationid + "')";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }


        public string getSuppliedItem(string mode, string productid)/*new add by p.basu for supplieditem from product 25-11-2020*/
        {
            string result = string.Empty;

            if (mode == "Id")
            {
                string sql = "select distinct ID  from M_SUPLIEDITEM with(nolock)  where itemcode =(select distinct type from M_PRODUCT with(nolock) where id='" + productid + "') ";
                DataTable dt = db.GetData(sql);
                result = dt.Rows[0]["ID"].ToString();
            }
            else if (mode == "Name")
            {
                string sql = "select distinct ITEM_Name  from M_SUPLIEDITEM with(nolock) where itemcode =(select distinct type from M_PRODUCT with(nolock) where id='" + productid + "')";
                DataTable dt = db.GetData(sql);
                result = dt.Rows[0]["ITEM_Name"].ToString();
            }
            return result;
        }

        /*production order start*/
        public string InsertUpdateProDetails(string poid, string podate, string remarks, string createdby, string finyear, string xml,
                                               string departid, string departname, string FactorID,string toStoreLocationId)
        {
            string requisitionno = string.Empty;
            try
            {
                string sql = "EXEC [USP_TBL_PRODUCTION_INSERT_UPDATE] '" + poid + "','" + podate + "','" + remarks + "','" + createdby + "'," +
                                " '" + finyear + "','" + xml + "','" + departid + "','" + departname + "','" + FactorID + "','" + toStoreLocationId + "'";
                DataTable dt = db.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    requisitionno = dt.Rows[0]["PRODUCTIONNO"].ToString();
                }
                else
                {
                    requisitionno = "";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return requisitionno;
        }

        public DataTable LoadProductionOrder(string fromdate, string todate, string UserID, string DepotID, string FinYear)
        {
            DataTable dt = new DataTable();
            try
            {
                String sql = "EXEC USP_BIND_PRODUCTION_ORDER '" + fromdate + "','" + todate + "','" + UserID + "','" + DepotID + "','" + FinYear + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public DataSet EditProductionOrder(string productionId)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "EXEC [USP_TBL_PRODUCTION_ORDER_EDIT] '" + productionId + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return ds;
        }

        public DataTable getstoreid(string id)
        {
            DataTable dt = new DataTable();
            try
            {
                string mode = "fromLocation";
                String sql = "EXEC [USP_CHECK_STATUS] '" + mode + "','" + id + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        /*new work start by p.basu on 09-02-2021*/
        #region requisition
        public DataTable fetchRequisition(string mode, string userId, string fromDate, string toDate)
        {
            DataTable dt = new DataTable();
            try
            {
                String sql = "EXEC USP_BIND_REQUISITION_DETAILS_MODE_WISE '" + mode + "','" + userId + "','" + fromDate + "','" + toDate + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable FetchRequistionDetails(string Id, string userid, string storeid)
        {
            DataTable dt = new DataTable();
            try
            {
                String sql = "EXEC USP_FETCH_REQUISITION_DETAILS '" + Id + "','" + userid + "','" + storeid + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }
        public DataTable loadstorelocationUserWise(string mode,string deptId)/*newadd*/
        {
            DataTable dt = new DataTable();
            try
            {
                String sql = "EXEC USP_LOAD_STORE_DEPARTMENTWISE '" + mode + "','" + deptId + "'";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return dt;
        }

        public string insertUpdateBulkRequistion(string bulkRequistionId, string requId, string pId, string pName, string mainDepartment, string toDepartment, string storeLocation, string xmlRequistion, string createdByUserid, string finYear)
        {
            string requisitionno = string.Empty;
            try
            {
                string sql = "USP_INSERT_UPDATE_BULK_REQUISTION '" + bulkRequistionId + "','" + requId + "','" + pId + "','" + pName + "','" + mainDepartment + "','" + toDepartment + "','" + storeLocation + "','" + xmlRequistion + "','" + createdByUserid + "','" + finYear + "'";
                DataTable dt = db.GetData(sql);

                if (dt.Rows.Count > 0)
                {
                    requisitionno = dt.Rows[0]["REQUISTIONNO"].ToString();
                }
                else
                {
                    requisitionno = "";
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return requisitionno;
        }

        public DataSet fetchBulkRequistionDetails(string id)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "EXEC [USP_EDIT_BULK_REQUISTION] '" + id + "'";
                ds = db.GetDataInDataSet(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :SQL Error");
            }
            return ds;
        }


        #endregion
    }
}
