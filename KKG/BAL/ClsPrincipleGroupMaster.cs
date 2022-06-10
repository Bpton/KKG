using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsPrincipleGroupMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindCatagoryMastergrid()
        {
            string sql = "select DIS_CATID, DIS_CATCODE, DIS_CATNAME, DIS_CATDESCRIPTION,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, " +
                         " case when ACTIVE='T' then 'Active'  else 'InActive' end as 'ACTIVE'," +
                         " case when ISFINANCE_HO='Y' then 'Yes'  else 'No' end as 'ISFINANCE_HO'," +
                         " case when ISCHAIN='Y' then 'Yes'  else 'No' end as 'ISCHAIN'," +
                         " CURRENCYNAME,PREDEFINED from M_DISTRIBUTER_CATEGORY";
            // string sql = "select DIS_CATID, DIS_CATCODE, DIS_CATNAME, DIS_CATDESCRIPTION,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED, ACTIVE FROM M_DISTRIBUTER_CATEGORY" ;

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindEdidGridByid(string id)
        {
            string sql = "select DIS_CATID, DIS_CATCODE, DIS_CATNAME, DIS_CATDESCRIPTION,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, CBU, DTOC, LMBU, LDTOM, STATUS, "+
                         " ISAPPROVED, ACTIVE,CURRENCYID,CURRENCYNAME,ISNULL(ISFINANCE_HO,'N') AS ISFINANCE_HO, "+
                         " isnull(ISCHAIN,'N') as ISCHAIN,ISNULL(ISCLAIMCHAIN,'N') AS ISCLAIMCHAIN,ISNULL(REFERENCELEDGERID_TOHO,'0') AS REFERENCELEDGERID_TOHO " +
                         " FROM M_DISTRIBUTER_CATEGORY WHERE DIS_CATID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindBusinessSegment()
        {
            string sql = "select BSID,BSNAME from M_BUSINESSSEGMENT order by BSNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCurrency()
        {
            string sql = "SELECT CURRENCYID,CURRENCYNAME FROM M_CURRENCY order by CURRENCYNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindLedger()
        {
            string sql = " SELECT ID,NAME FROM ACC_ACCOUNTSINFO A INNER JOIN Acc_AccountGroup B ON A.ACTGRPCODE=B.CODE " +
                         " WHERE B.PARENTID IN('8a11fa2b-ab74-44f4-85e1-c9a1a055961e') ORDER BY NAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCatagoryMaster(string ID, string Code, string Name, string Description, string Busineessegmentid,string businesssegmentname, string Mode,
                                     string Active, string Currencyid, string Currencyname, string transfertoho, string chain, string claimchain, string ISPOSTING_TOHO_REFERENCELEDGERID)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_DISTRIBUTER_CATEGORY where DIS_CATCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_DISTRIBUTER_CATEGORY where DIS_CATNAME='" + Name + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }
                else
                {
                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_DISTRIBUTER_CATEGORY where DIS_CATCODE='" + Code + "' and  DIS_CATID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_DISTRIBUTER_CATEGORY where DIS_CATNAME='" + Name + "' and  DIS_CATID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 3;
                            return result;
                        }
                    }
                }

                //if (Active == "true")
                //{
                //    Active = "T";
                //}
                //else
                //{
                //    Active = "F";
                //}
                if (dt.Rows.Count == 0)
                {
                    if (Mode == "A")
                    {
                        sqlstr = "insert into M_DISTRIBUTER_CATEGORY ([DIS_CATID],[DIS_CATCODE],[DIS_CATNAME],[DIS_CATDESCRIPTION],[BUSINESSSEGMENTID], [BUSINESSSEGMENTNAME],"+
                                 " [CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE],CURRENCYID,CURRENCYNAME,ISFINANCE_HO ,"+
                                 " ISCHAIN,ISCLAIMCHAIN,REFERENCELEDGERID_TOHO ) " +
                           " values(NEWID(),'" + Code + "', '" + Name + "','" + Description + "','"+Busineessegmentid+"','"+businesssegmentname+"', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + Active + "',"+
                            " '" + Currencyid + "','" + Currencyname + "','" + transfertoho + "','" + chain + "','" + claimchain + "','" + ISPOSTING_TOHO_REFERENCELEDGERID + "' )";
                        result = db.HandleData(sqlstr);
                    }
                    else
                    {
                        sqlstr = "update M_DISTRIBUTER_CATEGORY set DIS_CATCODE='" + Code + "',DIS_CATNAME='" + Name + "',DIS_CATDESCRIPTION='" + Description + "',"+
                                " BUSINESSSEGMENTID='" + Busineessegmentid + "',BUSINESSSEGMENTNAME='" + businesssegmentname + "',"+
                                " LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "',"+
                                " ACTIVE='" + Active + "',CURRENCYID='" + Currencyid + "',CURRENCYNAME='" + Currencyname + "',ISFINANCE_HO='" + transfertoho + "', "+
                                " ISCHAIN='" + chain + "',ISCLAIMCHAIN='" + claimchain + "',REFERENCELEDGERID_TOHO='" + ISPOSTING_TOHO_REFERENCELEDGERID + "'  where DIS_CATID= '" + ID + "'";
                        result = db.HandleData(sqlstr);
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
        public int DeleteCatagoryMaster(string ID)
        {
            int result = 0;
            string sqlstr;
            try
            {
                sqlstr = "[SP_PRINCIPAL_GROUP_DELETE] '" + ID + "'";
                result = db.HandleData(sqlstr);
            }

            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;
        }


        public DataTable BindDivision()
        {
            string sql = "SELECT DIVID, DIVNAME FROM M_DIVISION  WHERE ACTIVE='True' ORDER BY DIVNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCategory()
        {
            string sql = "SELECT CATID, CATNAME FROM M_CATEGORY  WHERE ACTIVE='True' ORDER BY CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindGridProduct(string catid,string divid )
        {
            string sql = "SELECT ID,NAME FROM M_PRODUCT WHERE CATID='" + catid + "' AND DIVID='" + divid + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int Saveproductmapping(string groupid,string xml)
        {
            string sql = string.Empty;
            int result=0;
            sql = "[SP_GROUP_PRODUCT_MAPPING] '" + groupid + "','" + xml + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable Productmapping(string BSID, string GROUPID)
        {
            string sql = string.Empty;
            sql = " SELECT ISNULL(PRODUCTID,'') AS PRODUCTID,ISNULL(PRODUCTNAME,'') AS PRODUCTNAME,ISNULL(BSID,'') AS BSID,ISNULL(BSNAME,'') AS BSNAME,"+
                  " ISNULL(GROUPID,'') AS GROUPID,ISNULL(GROUPNAME,'') AS GROUPNAME,ISNULL(BRANDID,'') AS BRANDID,ISNULL(BRANDNAME,'') AS BRANDNAME,"+
                  " ISNULL(CATEGORYID,'') AS CATEGORYID,ISNULL(CATEGORYNAME,'') AS CATEGORYNAME"+
                  " FROM M_PRODUCT_GROUP_MAPPING" +
                  " WHERE BSID='" + BSID + "' AND GROUPID='" + GROUPID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindCatagoryRetailChain()
        {
            string sql = "select CHAINID, CHAINCODE, CHAINNAME, CHAINDESCRIPTION,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, CBU, DTOC, LMBU, LDTOM, ISACTIVE, " +
                         " case when ISACTIVE='Y' then 'Active' else 'InActive' end as 'ACTIVE' from M_RETAIL_CHAIN ORDER BY CHAINNAME";

            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable BindEdidGridRetailChain(string id)
        {
            string sql = "select CHAINID, CHAINCODE, CHAINNAME, CHAINDESCRIPTION,BUSINESSSEGMENTID, BUSINESSSEGMENTNAME, CBU, DTOC, LMBU, LDTOM, " +
                         " ISACTIVE FROM M_RETAIL_CHAIN WHERE CHAINID='" + id + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveRetailchainMaster(string ID, string Code, string Name, string Description, string Busineessegmentid, string businesssegmentname, string Mode, string Active)
        {
            int result = 0;
            string sqlstr;

            DataTable dt = new DataTable();
            try
            {
                if (Mode == "A")
                {
                    sqlstr = "INSERT INTO M_RETAIL_CHAIN ([CHAINID],[CHAINCODE] ,[CHAINNAME],[CHAINDESCRIPTION] ,[BUSINESSSEGMENTID] ,[BUSINESSSEGMENTNAME],[CBU],[DTOC],[LMBU],[LDTOM],[ISACTIVE])"
                              + " values(NEWID(),'" + Code + "','" + Name + "','" + Description + "','" + Busineessegmentid + "','" + businesssegmentname + "','" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'',GETDATE(),'" + Active + "')";
                    result = db.HandleData(sqlstr);
                }
                else
                {
                    sqlstr = " UPDATE M_RETAIL_CHAIN SET CHAINCODE = '" + Code + "', CHAINNAME = '" + Name + "', CHAINDESCRIPTION = '" + Description + "', BUSINESSSEGMENTID = '" + Busineessegmentid + "'," +
                             " BUSINESSSEGMENTNAME ='" + businesssegmentname + "',LMBU= '" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),ISACTIVE='" + Active + "' where CHAINID = '" + ID + "'";

                    result = db.HandleData(sqlstr);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }

            return result;

        }
        public DataTable BindProduct()
        {
            string sql = "SELECT CATNAME,ID AS PRODUCTID,PRODUCTALIAS AS PRODUCTNAME,ACTIVE FROM M_PRODUCT A "+
                           " INNER JOIN M_PRODUCT_BUSINESSSEGMENT_MAP B ON A.ID = B.PRODUCTID " +
                           " WHERE B.BUSNESSSEGMENTID = '7F62F951-9D1F-4B8D-803B-74EEBA468CEE' "+
                          " AND A.DIVID NOT IN('6B869907-14E0-44F6-9BA5-57F4DA726686') "+
                          " AND A.MRP <> '0' AND ACTIVE = 'T'ORDER BY CATNAME, PRODUCTALIAS";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int ProductLaunchinsertupdate(string LAUNCHID, string LAUNCHNAME, string LAUNCHCODE, string FROMDATE, string TODATE, string CBU, string STATUS, string MODE, string FILEDETAILS)
        {
            string sql = string.Empty;
            int result = 0;
            sql = "[USP_INSERT_UPDATE_PRODUCT_LAUNCH] '" + LAUNCHID + "','" + LAUNCHNAME + "','" + LAUNCHCODE + "','" + FROMDATE + "','" + TODATE + "','" + CBU + "','" + STATUS + "','" + MODE + "','" + FILEDETAILS + "'";
            result = db.HandleData(sql);
            return result;
        }

        public DataTable LaunchDetails()
        {
            string sql = " SELECT LAUNCHID,LAUNCHNAME,LAUNCHCODE,CONVERT(VARCHAR(10),FROMDATE,103) AS FROMDATE,CONVERT(VARCHAR(10),TODATE,103) AS TODATE, " +
                         " CASE WHEN[STATUS] = 'A' THEN 'Active' ELSE 'Inactive' END AS[STATUS] FROM M_LAUNCH ORDER BY DTOC DESC";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public DataTable EditLaunchProductDetails(string LaunchID)
        {
            string sql = " SELECT DISTINCT PRODUCTID ,PRODUCTNAME ,A.LAUNCHID,B.LAUNCHCODE,B.LAUNCHNAME,"+
                          " CONVERT(VARCHAR(10), B.FROMDATE, 103) AS FROMDATE, CONVERT(VARCHAR(10), B.TODATE, 103) AS TODATE FROM M_LAUNCH_PRODUCT_MAPPING A"+
                          " INNER JOIN M_LAUNCH B ON A.LAUNCHID = B.LAUNCHID WHERE B.LAUNCHID = '" + LaunchID + "'";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
    }
}
