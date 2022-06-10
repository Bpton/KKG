using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DAL;
using System.Data;
using Utility;
using System.Globalization;


namespace Account
{
    public class ClsAccVoucherType
    {
        DBUtils db = new DBUtils();

        public DataTable BindVoucherGrid()
        {
            string sql = "SELECT ID,VOUCHERPREFIX,VOUCHERNAME FROM ACC_VOUCHERTYPES ORDER BY  VOUCHERPREFIX";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        public int SaveVouchertype(string ID,string VOUCHERPREFIX,string VOUCHERNAME,string Mode)
        {
            int result = 0;
           int vid=0;
            try
            {
                
                DataTable dt = new DataTable();
                string sql = string.Empty;
                if (ID == "")
                {
                    if (VOUCHERPREFIX == "")
                    {
                        string sqlchk = "SELECT TOP 1 * FROM ACC_VOUCHERTYPES WHERE VOUCHERPREFIX='" + VOUCHERPREFIX + "' ";
                        dt = db.GetData(sqlchk);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (VOUCHERNAME == "")
                    {
                        string sqlchk = "SELECT TOP 1 * FROM ACC_VOUCHERTYPES WHERE VOUCHERNAME='" + VOUCHERNAME + "' ";
                        dt = db.GetData(sqlchk);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                else
                {
                    //if (VOUCHERPREFIX != "")
                    //{
                    //    string sqlchk = "SELECT TOP 1 * FROM ACC_VOUCHERTYPES WHERE VOUCHERPREFIX='" + VOUCHERPREFIX + "' AND ID<>'" + ID + "' ";
                    //    dt = db.GetData(sqlchk);
                    //    if (dt.Rows.Count != 0)
                    //    {
                    //        result = 2;
                    //        return result;
                    //    }
                    //}
                    if (VOUCHERNAME != "")
                    {
                        string sqlchk = "SELECT TOP 1 * FROM ACC_VOUCHERTYPES WHERE VOUCHERNAME='" + VOUCHERNAME + "' AND ID<>'" + ID + "' ";
                        dt = db.GetData(sqlchk);
                        if (dt.Rows.Count !=0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        string sqlid = "SELECT MAX(CAST(ID AS INT))+1 FROM ACC_VOUCHERTYPES";
                        vid = (int)db.GetSingleValue(sqlid);
                        sql = "INSERT INTO ACC_VOUCHERTYPES (ID,VOUCHERPREFIX,VOUCHERNAME) VALUES(" + vid + ",'" + VOUCHERPREFIX + "','" + VOUCHERNAME + "') ";
                        result = db.HandleData(sql);
                    }
                    else
                    {
                        sql = "UPDATE  ACC_VOUCHERTYPES SET VOUCHERPREFIX='" + VOUCHERPREFIX + "',VOUCHERNAME='" + VOUCHERNAME + "' WHERE ID='" + ID + "' ";
                        result = db.HandleData(sql);
                    }
                }
                else
                {
                    result = 0;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        public DataTable VoucherTypeEdit(string ID)
        {
            string sql = "SELECT VOUCHERPREFIX,VOUCHERNAME FROM ACC_VOUCHERTYPES WHERE ID='" + ID + "'";
            DataTable dt = db.GetData(sql);
            return dt;
        }
    }
}
