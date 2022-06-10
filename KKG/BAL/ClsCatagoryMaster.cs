using DAL;
using System;
using System.Data;
using System.Web;
using Utility;

namespace BAL
{
    public class ClsCatagoryMaster
    {
        DBUtils db = new DBUtils();

        public DataTable BindCatagoryMastergrid()
        {
            string sql = "select CATID, CATCODE, CATNAME, CATDESCRIPTION, IPSC, HSN,CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',ISNULL(HSNCODEDESCRIPTION,'') AS HSNCODEDESCRIPTION from M_CATEGORY order by CATNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }
        public DataTable BindCatagoryMastergrideditbyid(string catid)
        {
            string sql = "select CATID, CATCODE, CATNAME, CATDESCRIPTION, IPSC, HSN,CBU, DTOC, LMBU, LDTOM, STATUS, ISAPPROVED,  case when ACTIVE='True' then 'Active'  else 'InActive' end as 'ACTIVE',ISNULL(HSNCODEDESCRIPTION,'') AS HSNCODEDESCRIPTION from M_CATEGORY WHERE CATID='" + catid + "' ";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int SaveCatagoryMaster(string ID, string Code, string Name, string Description, string ipsc, string hsc, string Mode, string Active, string HSNCODEDESCRIPTION)
        {
            int result = 0;
            string sqlstr;
            string strcheck;
            string SQLHESNCODE = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                if (ID == "")
                {
                    if (Code != "")
                    {
                        strcheck = "select top 1 * from M_CATEGORY where CATCODE='" + Code + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_CATEGORY where CATNAME='" + Name + "'";
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
                        strcheck = "select top 1 * from M_CATEGORY where CATCODE='" + Code + "' and  CATID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
                        {
                            result = 2;
                            return result;
                        }
                    }
                    if (Name != "")
                    {
                        strcheck = "select top 1 * from M_CATEGORY where CATNAME='" + Name + "' and  CATID<>'" + ID + "'";
                        dt = db.GetData(strcheck);
                        if (dt.Rows.Count != 0)
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
                        string NEWID = Convert.ToString(Guid.NewGuid()).ToUpper();


                        sqlstr = "INSERT INTO M_CATEGORY ([CATID],[CATCODE],[CATNAME],[CATDESCRIPTION],[IPSC], [HSN],[CBU],[DTOC],[LMBU],[LDTOM],[STATUS],[ISAPPROVED],[ACTIVE],HSNCODEDESCRIPTION)" +
                           " values('" + NEWID + "',upper('" + Code + "'), '" + Name + "','" + Description + "','" + ipsc + "','" + hsc + "', " +
                            " '" + HttpContext.Current.Session["UserID"].ToString() + "',GETDATE(),'','','" + Mode + "','N','" + Active + "','" + HSNCODEDESCRIPTION + "')";
                        result = db.HandleData(sqlstr);

                        SQLHESNCODE = "INSERT INTO M_HSNCODE (CATID,HSNCODE) VALUES('" + NEWID + "','" + hsc + "')";
                        result = db.HandleData(SQLHESNCODE);

                    }
                    else
                    {
                        sqlstr = " UPDATE M_CATEGORY set CATCODE=upper('" + Code + "'),CATNAME='" + Name + "',CATDESCRIPTION='" + Description + "'," +
                                 " LMBU='" + HttpContext.Current.Session["UserID"].ToString() + "',LDTOM=GETDATE(),STATUS='" + Mode + "'," +
                                 " ACTIVE='" + Active + "',IPSC='" + ipsc + "', HSN='" + hsc + "',HSNCODEDESCRIPTION='" + HSNCODEDESCRIPTION + "' where CATID= '" + ID + "'";
                        result = db.HandleData(sqlstr);

                        SQLHESNCODE = "UPDATE M_HSNCODE SET HSNCODE='" + hsc + "' WHERE CATID= '" + ID + "'";
                        result = db.HandleData(SQLHESNCODE);

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
            string sqlstrHSN;
            try
            {

                sqlstr = "Delete from M_CATEGORY where CATID= '" + ID + "'";
                result = db.HandleData(sqlstr);

                sqlstrHSN = "Delete from M_HSNCODE where CATID= '" + ID + "'";
                result = db.HandleData(sqlstrHSN);
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
