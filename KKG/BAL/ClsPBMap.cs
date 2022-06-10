using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsPBMap
    {
        DBUtils db = new DBUtils();

        public DataTable BindPBGrid()
        {
            string sql = "SELECT PRODUCTID, PRODUCTNAME, BUSNESSSEGMENTID, BUSINESSSEGMENTNAME FROM [M_PRODUCT_BUSINESSSEGMENT_MAP] ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        //public DataTable BindMasterById(string ID)
        //{
        //    string sql = "SELECT VENDORID, CODE, NAME,SUPLIEDITEMID, SUPLIEDITEM, CITYID, STATEID, COUNTRYID, DISTRICTID, CITYNAME, STATENAME, DISTRICTNAME, ADDRESS, PHONENO, MOBILENO, PINZIP, EMAILID, CONTACTPERSON, CSTNO, VATNO, TINNO, PANNO, BANKACNO,BANKID, BANKNAME, BRANCHID, BRANCHNAME, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED FROM [M_TPU_VENDOR] where VENDORID='" + ID + "' ";
        //    DataTable dt = new DataTable();
        //    dt = db.GetData(sql);
        //    return dt;
        //}
        public DataTable BindProductName()
        {
            string sql = "select ID,NAME from M_PRODUCT  order by ID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBSNameGrid()
        {
            string sql = "select BSID,BSNAME from M_BUSINESSSEGMENT  WHERE ACTIVE='True' ORDER BY BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBSNameGridbyid(string id)
        {
            string sql = "SELECT BUSNESSSEGMENTID AS BSID ,BUSINESSSEGMENTNAME AS BSNAME FROM M_PRODUCT_BUSINESSSEGMENT_MAP WHERE PRODUCTID='" + id + "' order by BUSNESSSEGMENTID";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public void DeletePBMapbyid(string id)
        {
            string sql = "DELETE FROM M_PRODUCT_BUSINESSSEGMENT_MAP WHERE PRODUCTID='" + id + "'";
            db.HandleData(sql);

            string sql1 = "DELETE FROM M_PRODUCT_TPU_MAP WHERE PRODUCTID='" + id + "' and Tag='T'";
            db.HandleData(sql1);

            string sql2 = "DELETE FROM M_PRODUCT_TPU_MAP WHERE PRODUCTID='" + id + "' and Tag='F'";
            db.HandleData(sql2);

        }

        //========================================Added By Sourav Mukherjee on 31/03/2016====================================================//

        public void DeleteGroupMappingbyid(string id, string bsid)
        {
            string sql = "DELETE FROM M_PRODUCT_GROUP_MAPPING WHERE PRODUCTID='" + id + "' AND BSID='" + bsid + "'";
            db.HandleData(sql);
        }
        //===================================================================================================================================//
        public int SavePBMap(string ID, string Pid, string Pname, string Bid, string Bname, string Mode)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Pname != "" && Bname != "")
                    {
                        strcheck = "select * from [M_PRODUCT_BUSINESSSEGMENT_MAP] WHERE PRODUCTID='" + Pid + "' AND BUSNESSSEGMENTID='" + Bid + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Pname != "" && Bname != "")
                    {
                        //strcheck = "select top 1 * from M_DIVISION where DIVCODE='" + Code + "' and  DIVID<>'" + ID + "'";
                        //dt = db.GetData(strcheck);
                        //if (dt.Rows.Count != 0)
                        //{
                        //    result = 2;
                        //    return result;
                        //}
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_PRODUCT_BUSINESSSEGMENT_MAP(PRODUCTID, PRODUCTNAME, BUSNESSSEGMENTID, BUSINESSSEGMENTNAME)" +
                           " values('" + Pid + "', '" + Pname + "','" + Bid + "','" + Bname + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        //sqlstr = "update M_DIVISION set DIVCODE='" + Code + "',DIVNAME='" + Name + "',DIVDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "' where DIVID= '" + ID + "'";
                        //result = db.HandleData(sqlstr);
                    }
                }

                else
                {
                    //result = 0;

                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        //=========================================Added by Sourav Mukherjee on 31/03/2016==============================================//

        public int SaveGroupMap(string ID, string XML)
        {
            int result = 0;
            string sqlstr;
            DataTable dt = new DataTable();
            try
            {
                sqlstr = "exec SP_PRODUCT_GROUP_MAPPING '" + ID + "','" + XML + "'";
                result = db.HandleData(sqlstr);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
        public DataTable EditProductGroup(string ID)
        {
            DataTable dt = new DataTable();
            string sqlstr = "EXEC SP_PRODUCT_GROUP_EDIT '" + ID + "'";
            dt = db.GetData(sqlstr);
            return dt;
        }

        //==============================================================================================================================//

        public int SaveTPUMap(string ID, string Pid, string Pname, string Bid, string Bname, string Mode, string Tag)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Pname != "" && Bname != "")
                    {
                        strcheck = "select * from [M_PRODUCT_TPU_MAP] WHERE PRODUCTID='" + Pid + "' AND VENDORID='" + Bid + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Pname != "" && Bname != "")
                    {
                        //strcheck = "select top 1 * from M_DIVISION where DIVCODE='" + Code + "' and  DIVID<>'" + ID + "'";
                        //dt = db.GetData(strcheck);
                        //if (dt.Rows.Count != 0)
                        //{
                        //    result = 2;
                        //    return result;
                        //}
                    }
                }
                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_PRODUCT_TPU_MAP(PRODUCTID, PRODUCTNAME, VENDORID, VENDORNAME,TAG)" +
                           " values('" + Pid + "', '" + Pname + "','" + Bid + "','" + Bname + "','" + Tag + "')";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        //sqlstr = "update M_DIVISION set DIVCODE='" + Code + "',DIVNAME='" + Name + "',DIVDESCRIPTION='" + Description + "',LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',ACTIVE='" + active + "' where DIVID= '" + ID + "'";
                        //result = db.HandleData(sqlstr);
                    }
                }
                else
                {
                    //result = 0;
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }

        /*Added By SAYAN DEY on 03.07.2018*/
        public int SaveTPUMap1(string Pid, string Pname, string Tag, string xml)
        {
            int result = 0;
            DataTable dt = new DataTable();
            try
            {
                string sql = string.Empty;
                sql = "EXEC SP_PRODUCT_TPU_MAPPING '" + Pid + "','" + Pname + "','" + Tag + "','" + xml + "'";
                db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return result;
        }
    }
}
