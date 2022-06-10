using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using FactoryModel;
using System.Data;

using System.Globalization;
using System.Web;
namespace FactoryDatacontext
{
    public class ClsVoucherentry
    {
        DBUtils db = new DBUtils();

        public string InsertBillTagingJournalVoucherDetails(Accountsmodel accmodel, string finyr, string userid, string xml, string costcenter, string gstdetail)
        {
            string flag = string.Empty;
            string VoucherNo = string.Empty;
            string Voucherdetails = string.Empty;

            string sql = "EXEC [SP_ACC_VOUCHER] '" + accmodel.vouchertypeid + "','" + accmodel.vouchertypename + "','" + accmodel.BRID + "','" + accmodel.BRNAME + "','" + accmodel.paymentmode + "','" + accmodel.voucherdate + "','" + finyr + "','" + accmodel.narration + "','" + userid + "','" + accmodel.mode + "','" + accmodel.accid + "','" + xml + "','" + accmodel.isvoucherappliocable + "','" + "" + "','" + costcenter + "','" + accmodel.isfrompage + "','" + gstdetail + "','" + accmodel.billno + "','" + accmodel.billdate + "','" + accmodel.grnno + "','" + accmodel.grdate + "','" + accmodel.vehicleno + "','" + accmodel.transport + "','" + accmodel.waybillno + "','" + accmodel.waybilldate + "', @p_OnlyBillTagFromPage='" + accmodel.billtargetfrompage + "', @p_DrCr= '" + accmodel.drcrtds + "',@p_GSTVoucherType='" + accmodel.gstvpuchertypeid + "'";
            Voucherdetails = (string)db.GetSingleValue(sql);

            return Voucherdetails;
        }
        public string SaveTransporterbillV2(Transporterbillfac tran,string xml)
        {
            string transporterbillno = string.Empty;
            string sql = string.Empty;

            sql = "EXEC SP_TRANSPORTERBILL_INSERT_UPDATE_GST_V2_FAC '" + tran.ID + "','" + tran.Mode + "','" + tran.TransporterID + "','" + tran.TransporterName + "','" + tran.billdate + "','" + tran.billtype + "' ," +
                                                         " '" + tran.Remarks + "','" + tran.UserID + "','" + tran.finyear + "','" + xml + "','" + tran.MenuId + "'," + tran.TOTALNETAMOUNT + "," +
                                                        " " + tran.TOTALTDS + "," + tran.TOTALGROSSWEIGHT + ",'" + tran.depotid + "','" + tran.depotname + "'," + tran.IsTransferToHO + "," +
                                                        " " + tran.TOTALBILLAMOUNT + "," + tran.TOTALTDSDEDUCTABLE + "," + tran.sumcgst + "," + tran.sumsgst + "," + tran.sumigst + "," +
                                                        " " + tran.sumugst + ",'" + tran.BILLINGFROMSTATEID + "','" + tran.Reversecharge + "','" + tran.GNGNO + "','" + tran.TDSAPPLICABLE + "', " +
                                                        " " + tran.TDSPECENTAGE + ",'" + tran.TDSID + "','" + tran.VIRTUALdepotid + "','" + tran.EXPORTTAG + "','" + tran.ReasonID + "','" + tran.GSTReasonID + "'";
            transporterbillno = (string)db.GetSingleValue(sql);
            return transporterbillno;
        }
        public DataSet VoucherDetails(string AccEntryID)
        {
            DataSet ds = new DataSet();
            string sql = "EXEC SP_ACC_VOUCHERDETAILS '" + AccEntryID + "'";
            ds = db.GetDataInDataSet(sql);
            return ds;
        }
    }
}
