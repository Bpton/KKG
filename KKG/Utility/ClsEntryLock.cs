using System.Data;
using DAL;
using System;

namespace Utility
{
    public class ClsEntryLock
    {
        DBUtils db = new DBUtils();
        DataTable dt = new DataTable();
        public bool EntryLock(string EntryDt, string FinYear)
        {
            bool _bvalue = false;
            string FirstFinYear = FinYear.Substring(0, 4);
            string SecondFinYear = FinYear.Substring(5, 4);
            string FirstFinYearDt = "01/04/" + FirstFinYear;
            string SecondFinYearDt = "31/03/" + SecondFinYear;

            string sql = " SELECT FROMDATE,TODATE FROM DATEWISE_ENTRYLOCK WHERE FROMDATE<=CONVERT(DATE, '" + EntryDt + "',103) AND TODATE>=CONVERT(DATE, '" + EntryDt + "',103) " +
                         " AND CONVERT(DATE, '" + EntryDt + "',103) BETWEEN CONVERT(DATE, '" + FirstFinYearDt + "',103) AND CONVERT(DATE, '" + SecondFinYearDt + "',103)";
            dt = db.GetData(sql);
            if (dt.Rows.Count > 0)
            {
                return _bvalue = true;
            }
            else
            {
                return _bvalue = false;
            }
        }
    }
}
