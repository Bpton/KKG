using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utility
{
    public class CreateLogFiles
    {
        private string sLogFormat;
        private string sErrorTime;

        public CreateLogFiles()
        {
            //sLogFormat used to create log files format :
            // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
            try
            {
                sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
                //this variable used to create log filename format "
                //for example filename : ErrorLogYYYYMMDD
                string sYear = DateTime.Now.Year.ToString();
                string sMonth = DateTime.Now.Month.ToString();
                string sDay = DateTime.Now.Day.ToString();
                if (Convert.ToInt32(sMonth) < 10)
                    sMonth = "0" + sMonth;
                if (Convert.ToInt32(sDay) < 10)
                    sDay = "0" + sDay;
                sErrorTime = "_" + sDay + "_" + sMonth + "_" + sYear;
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }

        public void ErrorLog(string sPathName, string sErrMsg)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Inetpub\wwwroot\Loyalty\ErrorLog\" + sErrorTime, true);
                sw.WriteLine("Date :" + sLogFormat + "Error :" + sErrMsg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                string strErr = ex.Message;
            }
        }
    }
}
