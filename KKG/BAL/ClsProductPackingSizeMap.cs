using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsProductPackingSizeMap
    {
        DBUtils db = new DBUtils();
        DataTable dtProductMappingRecords = new DataTable();  // Datatable for adding gridSalesforecast
        DataTable dteditProductMappingRecords = new DataTable();
        DataTable dteditProductMappingRecordsClone = new DataTable();


        public DataTable BindPPGrid()
        {
            string sql = "SELECT PRODUCTID, PRODUCTNAME, PACKSIZEID_FROM, PACKSIZEName_FROM, PACKSIZEID_TO, PACKSIZEName_TO, CONVERSIONQTY FROM M_PRODUCT_UOM_MAP ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindUOM()
        {
            string sql = " SELECT UOMID,UOMNAME FROM M_UOM ORDER BY UOMNAME ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindProductname()
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT order by ID ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackigsize()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE WHERE PSID<>'1' order by PSID ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindPackigsizewithpcs()
        {
            string sql = "SELECT PSID,PSNAME FROM M_PACKINGSIZE order by PSID ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveMaster(string Productid, string Productname, string Mode)
        {
            int result = 0;
            string sqlstr = string.Empty;



            DataTable dt = new DataTable();
            DataTable dtProductMappingRecords = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
            try
            {
                if (dt.Rows.Count == 0)
                {

                    if (Mode == "A")
                    {

                        sqlstr = "DELETE FROM M_PRODUCT_UOM_MAP WHERE PRODUCTID='" + Productid + "'";

                        result = db.HandleData(sqlstr);


                        for (int i = 0; i < dtProductMappingRecords.Rows.Count; i++)
                        {
                            sqlstr = "insert into M_PRODUCT_UOM_MAP(PRODUCTID, PRODUCTNAME, PACKSIZEID_FROM, PACKSIZEName_FROM, PACKSIZEID_TO, PACKSIZEName_TO, CONVERSIONQTY,GROSSWEIGHT,UOMID,UOMNAME)" +
                                     " values('" + Productid + "','" + Productname + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEID_FROM"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEName_FROM"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEID_TO"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEName_TO"].ToString() + "','" + Convert.ToDecimal(dtProductMappingRecords.Rows[i]["CONVERSIONQTY"]) + "'," + Convert.ToDecimal(dtProductMappingRecords.Rows[i]["GROSSWEIGHT"]) + ",'" + Convert.ToString(dtProductMappingRecords.Rows[i]["UOMID"]) + "','" + Convert.ToString(dtProductMappingRecords.Rows[i]["UOMNAME"]) + "')";
                            result = db.HandleData(sqlstr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public int SaveUnitMapping(string Productid, string Productname, string Mode)
        {
            int result = 0;
            string sqlstr = string.Empty;
            string sqlstr1 = string.Empty;
            DataTable dt = new DataTable();
            DataTable dtProductMappingRecords = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
            try
            {
                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        sqlstr = "DELETE FROM M_PRODUCT_UOM_MAP WHERE PRODUCTID='" + Productid + "'";
                        sqlstr1 = "DELETE FROM M_MATERIAL_UOM_MAP WHERE MATERIALID='" + Productid + "'";
                        result = db.HandleData(sqlstr);
                        result = db.HandleData(sqlstr1);

                        for (int i = 0; i < dtProductMappingRecords.Rows.Count; i++)
                        {
                            sqlstr = "insert into M_PRODUCT_UOM_MAP(PRODUCTID, PRODUCTNAME, PACKSIZEID_FROM, PACKSIZEName_FROM, PACKSIZEID_TO, PACKSIZEName_TO, CONVERSIONQTY,GROSSWEIGHT,UOMID,UOMNAME)" +
                                     " values('" + Productid + "','" + Productname + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEID_FROM"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEName_FROM"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEID_TO"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEName_TO"].ToString() + "','" + Convert.ToDecimal(dtProductMappingRecords.Rows[i]["CONVERSIONQTY"]) + "'," + Convert.ToDecimal(dtProductMappingRecords.Rows[i]["GROSSWEIGHT"]) + ",'" + Convert.ToString(dtProductMappingRecords.Rows[i]["UOMID"]) + "','" + Convert.ToString(dtProductMappingRecords.Rows[i]["UOMNAME"]) + "')";
                            result = db.HandleData(sqlstr);

                            sqlstr1 = "insert into M_MATERIAL_UOM_MAP(MATERIALID, MATERIALNAME, UOMID_FROM, UOMNAME_FROM, UOMID_TO, UOM_TO, CONVERSIONQTY)" +
                                     " values('" + Productid + "','" + Productname + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEID_FROM"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEName_FROM"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEID_TO"].ToString() + "','" + dtProductMappingRecords.Rows[i]["PACKSIZEName_TO"].ToString() + "','" + Convert.ToDecimal(dtProductMappingRecords.Rows[i]["CONVERSIONQTY"]) + "')";
                            result = db.HandleData(sqlstr1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable BindMappingDataTable()
        {
            dtProductMappingRecords.Clear();
            dtProductMappingRecords.Columns.Add("SLNO", typeof(string));
            dtProductMappingRecords.Columns.Add("PACKSIZEID_FROM");
            dtProductMappingRecords.Columns.Add("PACKSIZEName_FROM");
            dtProductMappingRecords.Columns.Add("PACKSIZEID_TO");
            dtProductMappingRecords.Columns.Add("PACKSIZEName_TO");
            dtProductMappingRecords.Columns.Add("CONVERSIONQTY");
            dtProductMappingRecords.Columns.Add("GROSSWEIGHT");
            dtProductMappingRecords.Columns.Add("UOMID");
            dtProductMappingRecords.Columns.Add("UOMNAME");
            HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"] = dtProductMappingRecords;
            return dtProductMappingRecords;
        }

        public int MappingRecordsCheck(string packingsizeFrom, string packingsizeTo, string hdnvalue)
        {
            int flag = 0;
            if (hdnvalue == "")
            {
                dtProductMappingRecords = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
                foreach (DataRow dr in dtProductMappingRecords.Rows)
                {
                    if (Convert.ToString(dr["PACKSIZEID_FROM"]) == packingsizeFrom && Convert.ToString(dr["PACKSIZEID_TO"].ToString()) == packingsizeTo)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            else
            {
                dtProductMappingRecords = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];
                foreach (DataRow dr in dtProductMappingRecords.Rows)
                {
                    if (Convert.ToString(dr["PACKSIZEID_FROM"]) == packingsizeFrom && Convert.ToString(dr["PACKSIZEID_TO"].ToString()) == packingsizeTo)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            return flag;
        }

        public void ResetDataTables()
        {
            dtProductMappingRecords.Clear();
            dteditProductMappingRecords.Clear();
            dteditProductMappingRecordsClone.Clear();
        }
        public DataTable BindMappingBasedOnProduct(string ProductID)
        {
            //  DataTable dtProductMappingRecords;

            string sql = " SELECT PACKSIZEID_FROM,PACKSIZEName_FROM,PACKSIZEID_TO,PACKSIZEName_TO,CONVERSIONQTY,ISNULL(GROSSWEIGHT,0) AS GROSSWEIGHT,UOMID,UOMNAME" +
                         " from M_PRODUCT_UOM_MAP" +
                         " WHERE PRODUCTID='" + ProductID + "'";
            dteditProductMappingRecords = db.GetData(sql);
            //dteditProductMappingRecordsClone = BindMappingDataTable();
            //HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"] = dteditProductMappingRecords;
            dtProductMappingRecords = ((DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"]).Clone();

            for (int i = 0; i < dteditProductMappingRecords.Rows.Count; i++)
            {
                DataRow dr = dtProductMappingRecords.NewRow();

                dr["SLNO"] = Guid.NewGuid();
                dr["PACKSIZEID_FROM"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["PACKSIZEID_FROM"]);
                dr["PACKSIZEName_FROM"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["PACKSIZEName_FROM"]);
                dr["PACKSIZEID_TO"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["PACKSIZEID_TO"]);
                dr["PACKSIZEName_TO"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["PACKSIZEName_TO"]);
                dr["CONVERSIONQTY"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["CONVERSIONQTY"]);

                dr["GROSSWEIGHT"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["GROSSWEIGHT"]);
                dr["UOMID"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["UOMID"]);
                dr["UOMNAME"] = Convert.ToString(dteditProductMappingRecords.Rows[i]["UOMNAME"]);

                dtProductMappingRecords.Rows.Add(dr);
                dtProductMappingRecords.AcceptChanges();
            }
            HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"] = dtProductMappingRecords;
            return dtProductMappingRecords;
        }

        public DataTable BindMappingGridRecords(string PACKSIZEID_TO, string PACKSIZEName_TO, string PACKSIZEID_FROM, string PACKSIZEName_FROM, string CONVERSIONQTY, string GrossWeight, string UOMID, string UOMNAME, string MODE)
        {
            if (MODE == "A")
            {
                dtProductMappingRecords = (DataTable)HttpContext.Current.Session["PRODUCTMAPPINGDETAILSRECORDS"];

                DataRow dr = dtProductMappingRecords.NewRow();
                dr["SLNO"] = Guid.NewGuid();
                dr["PACKSIZEID_FROM"] = PACKSIZEID_FROM;
                dr["PACKSIZEName_FROM"] = PACKSIZEName_FROM;
                dr["PACKSIZEID_TO"] = PACKSIZEID_TO;
                dr["PACKSIZEName_TO"] = PACKSIZEName_TO;
                dr["CONVERSIONQTY"] = CONVERSIONQTY;
                dr["GROSSWEIGHT"] = GrossWeight;
                dr["UOMID"] = UOMID;
                dr["UOMNAME"] = UOMNAME;
                dtProductMappingRecords.Rows.Add(dr);
                dtProductMappingRecords.AcceptChanges();
            }
            return dtProductMappingRecords;
        }
    }
}
