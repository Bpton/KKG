using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using Utility;
using System.Globalization;
using System.Web;
using System.Data.SqlClient;


namespace Account
{
    public  class ClsAccGrp
    {
        DBUtils db = new DBUtils();

        public DataTable BindGrid(string strSearch)
        {
            DataTable dt = new DataTable();

            try
            {
                string sql = "select tbl2.parentId, grptype.Id,tbl2.Code,grptype.Name AS ParentGroup,tbl1.grpName "+
                    "AS [PrimaryGroup],tbl2.grpName AS [ChildGroup],tbl2.IsPrimary,tbl2.sequence from [Acc_AccountGroup] tbl1 " +
                    "RIGHT JOIN [Acc_AccountGroup] tbl2 ON tbl1.Code=tbl2.ParentId " +
                    "inner join [Acc_AccountGroupType] grptype on tbl2.typeId=grptype.Id";
                if (strSearch != "")
                {
                    sql += " WHERE tbl2.grpName LIKE '%" + strSearch + "%' ";
                }
                sql += " order by grptype.Name,tbl1.grpName,tbl2.grpName ";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }
        
        public DataTable BindLedger()
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = " select * from Acc_AccountGroup order by sequence ";
                //where Code in(select parentId from [Acc_AccountGroup])";
                 dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindLedgerPry()
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = " select name,id from Acc_AccountGroupType order by sequence";
                 dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;
        }

        public DataTable BindParentTree(string typeid)
        {

            string sql = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                sql = "SELECT [Code],[grpName] FROM  [Acc_AccountGroup] where [parentId] IS NULL and typeid='" + typeid + "' order by sequence";
                dt = db.GetData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @"ErrorLog\ErrorLog", ex.Message + " Path :GridView Error");
            }
            return dt;

        }

        public DataTable BindChildIdTree( string NodeID)
        {
            DataTable dt = new DataTable();
            try
            {
                //string Sql = " select Code,grpName,typeId from Acc_AccountGroup where parentId ='" + child + "',typeId ='" + TypId + "'";
                string Sql = " select Code,grpName from Acc_AccountGroup where parentid='" + NodeID + "' order by sequence";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return dt;
        }

        public DataTable BindLastChildIdTree(string NodeID)
        {
            DataTable dt = new DataTable();
            try
            {
                //string Sql = " select Code,grpName,typeId from Acc_AccountGroup where parentId ='" + child + "',typeId ='" + TypId + "'";
                string Sql = " SELECT Id AS Code,name as grpName FROM Acc_AccountsInfo where actGrpCode='" + NodeID + "' order by name";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return dt;
        }

        public DataTable BindSUBChildIdTree(string ParentID)
        {
            DataTable dt = new DataTable();
            try
            {
                //string Sql = " select Code,grpName,typeId from Acc_AccountGroup where parentId ='" + child + "',typeId ='" + TypId + "'";
                string Sql = " select Code,grpName,typeId from Acc_AccountGroup where ParentID ='" + ParentID + "' order by sequence";
                dt = db.GetData(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return dt;
        }

        public DataTable BindChildLevel(string TypId)
        {
            DataTable dt = new DataTable();
            try
            {
                string Sql = " select distinct level from Acc_AccountGroup where typeId ='" + TypId + "'";
                dt = db.GetData(Sql);
            }
            catch(Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return dt;
        }

        public string BindTypId(string CodId)
        {
            string Sql = "";
            try
            {
                 Sql = " select typeId from Acc_AccountGroup where code='" + CodId + "'";
                Sql = (string)db.GetSingleValue(Sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return Sql;
        }

        public string BindLevel(string CodId)
        {
            string Sql ="";
            try
            {
                Sql = "  IF  EXISTS(select [level] from Acc_AccountGroup where code='" + CodId +"') BEGIN SELECT '1' AS  [level]  END  ELSE    BEGIN	  SELECT '0' AS [level]  END ";
                Sql = db.GetSingleValue(Sql).ToString();
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return Sql;
        }

        public int SaveGrp(string mode, string Code, string typeid, string grpName, string PID, string Isprm, string PreDe, int level, int sequence)
        {
            int result = 0;
            try
            {
                string sql1 = string.Empty;
                if (mode == "A")
                {
                    sql1 = "select grpName from Acc_AccountGroup where grpName= '" + grpName + "'";
                }
                else
                {
                    sql1 = "select grpName from Acc_AccountGroup where grpName= '" + grpName + "' AND code<>'" + Code + "'";
                }
                DataTable dt = db.GetData(sql1);
                if (dt.Rows.Count > 0)
                {
                    result = -2;
                }
                else
                {
                    string sql = string.Empty;

                    if (mode == "A")
                    {
                        if (PID != null)
                        {
                            sql = " Insert into Acc_AccountGroup ([Code] ,[typeId] ,[grpName] ,[parentId] ,[isPrimary] ,[preDefined],[level],sequence)" +
                                  " Values" +
                                  " ('" + Code + "','" + typeid + "','" + grpName + "','" + PID + "','" + Isprm + "','" + PreDe + "'," + level + "," + sequence + ")";
                        }
                        else
                        {
                            sql = " Insert into Acc_AccountGroup ([Code] ,[typeId] ,[grpName] ,[isPrimary] ,[preDefined],[level],sequence)" +
                                  " Values" +
                                  " ('" + Code + "','" + typeid + "','" + grpName + "','" + Isprm + "','" + PreDe + "'," + level + "," + sequence + ")";
                        }
                    }
                    else if (mode == "E")
                    {
                        if (PID != null)
                        {
                            sql = "UPDATE Acc_AccountGroup SET [grpName]='" + grpName + "',[parentId]='" + PID + "',[isPrimary]='" + Isprm + "', [typeId]='" + typeid + "',sequence=" + sequence + "  WHERE code='" + Code + "'";
                        }
                        else
                        {
                            sql = "UPDATE Acc_AccountGroup SET [grpName]='" + grpName + "',[parentId]=NULL,[isPrimary]='" + Isprm + "', [typeId]='" + typeid + "',sequence=" + sequence + "  WHERE code='" + Code + "'";
                        }
                    }
                    else
                    {
                        sql = "DELETE FROM Acc_AccountGroup WHERE code='" + Code + "'";
                    }
                    result = db.HandleData(sql);
                }
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            return result;
            
        }


        public DataTable Bindvouchertype()
        {
            string sql = "SELECT ID,VOUCHERNAME FROM DBO.ACC_VOUCHERTYPES ORDER BY VOUCHERNAME";
            DataTable dt = new DataTable();
            dt = db.GetData(sql);
            return dt;
        }

        public int Deleteaccgroup(string VoucherTypeID, string Tag)
        {
            int result = 0;
            string sql = "DELETE FROM Acc_Param WHERE VoucherTypeID='" + VoucherTypeID + "' AND Type_Dr_Cr='" + Tag + "'";
            result = db.HandleData(sql);
            return result;
        }

        public int InsertAccGroupMapping(string AccGrpId, string VoucherTypeID, string Type_Dr_Cr,string ID)
        {
            int result = 0;
            try
            {

                string sql = "INSERT INTO Acc_Param(AccGrpId,VoucherTypeID,Type_Dr_Cr) VALUES('" + AccGrpId + "','" + VoucherTypeID + "','" + Type_Dr_Cr + "')";
                result = db.HandleData(sql);
            }
            catch (Exception ex)
            {
                CreateLogFiles Errlog = new CreateLogFiles();
                Errlog.ErrorLog(HttpContext.Current.Request.PhysicalApplicationPath + @" Errorlog\Errorlog", ex.Message + " Path : GridView Error");
            }
            
    
            return result;
        }

        public DataTable Populateaccgroupmappingdeit(string VoucherTypeID)
        {
            string sql = "SELECT DISTINCT  AccGrpId,Type_Dr_Cr,id from Acc_Param where VoucherTypeID='" + VoucherTypeID + "' and Type_Dr_Cr='D' ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        public DataTable Populateaccgroupmappingcredit(string VoucherTypeID)
        {
            string sql = "SELECT DISTINCT  AccGrpId,Type_Dr_Cr,id from Acc_Param where VoucherTypeID='" + VoucherTypeID + "' and Type_Dr_Cr='C' ";
            DataTable dt = db.GetData(sql);
            return dt;
        }

        #region Load Ledger For Grn Approval Page
        public DataTable LoadLedger()
        {
            string sql = " DECLARE @P_APPLEDGER VARCHAR(350)" +
                         " SELECT @P_APPLEDGER= [LEDGER-MM-GRN] FROM P_APPMASTER " +
                         " SELECT ID,NAME FROM ACC_ACCOUNTSINFO " +
                         " WHERE ID IN ( SELECT * FROM DBO.FNSPLIT(@P_APPLEDGER,',')) ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #endregion

        #region Load Ledger For Stock Transfer Received Approval Page
        public DataTable StkReceiptLoadLedger()
        {
            // string sql = "SELECT [STKRECEIPT_ACC_LEADGER] FROM P_APPMASTER";
            string sql = " DECLARE @P_APPLEDGER VARCHAR(350)" +
                         " SELECT @P_APPLEDGER= [STKRECEIPT_ACC_LEADGER] FROM P_APPMASTER " +
                         " SELECT ID,NAME FROM ACC_ACCOUNTSINFO " +
                         " WHERE ID IN ( SELECT * FROM DBO.FNSPLIT(@P_APPLEDGER,',')) ";
            DataTable dt = db.GetData(sql);
            return dt;
        }
        #endregion
    }
}
