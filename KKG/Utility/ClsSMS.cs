using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DAL;

namespace Utility
{
    public class ClsSMS
    {
        DBUtils db = new DBUtils();

        #region ACCOUNTS_SMS
        public int ACCOUNTS_SMS(string AccEntryID)
        {
            int result = 0;
            string sqlstr;
            sqlstr = " INSERT INTO T_SMS  ([MOBILE],[MESSAGE])" +
                     " SELECT PARTYMOBILENO,SMSDESCRIPTION FROM VW_ACCSMS WHERE AccEntryID='" + AccEntryID + "'";
            result = db.HandleData(sqlstr);
            return result;
        }
        #endregion
    }
}
