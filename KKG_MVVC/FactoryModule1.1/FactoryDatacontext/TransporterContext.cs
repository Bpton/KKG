using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
  public  class TransporterContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);
        public List<Depomodel> BindDepot(string userid)
        {
            List<Depomodel> Depomodel = new List<Depomodel>();
            try
            {
                Depomodel = _db.Query<Depomodel>("usp_Binddepo_transporter", new { p_userid = userid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Transportermodel> BindTransporter(string depoid)
        {
            List<Transportermodel> Depomodel = new List<Transportermodel>();
            try
            {
                Depomodel = _db.Query<Transportermodel>("usp_BindTransporter", new { p_depoid = depoid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Transportermodel> BindTransporterv2(string depoid)
        {
            List<Transportermodel> Depomodel = new List<Transportermodel>();
            try
            {
                Depomodel = _db.Query<Transportermodel>("usp_BindTransporterv2", new { p_depoid = depoid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<Depomodel> BindExportDepot()
        {
            List<Depomodel> Depomodel = new List<Depomodel>();
            try
            {
                Depomodel = _db.Query<Depomodel>("usp_Exportdepo",  commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Depomodel;
        }
        public List<messageresponse> ExportDepotChecking(string depoid)
        {
            List<messageresponse> res = new List<messageresponse>();
            try
            {
                res = _db.Query<messageresponse>("usp_ExportChecking", new { p_depoid = depoid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public List<messageresponse> GetOfflineStatus(string depoid)
        {
            List<messageresponse> res = new List<messageresponse>();
            try
            {
                res = _db.Query<messageresponse>("usp_Oflinestatus", new { p_depoid = depoid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public List<messageresponse> GetTdscheck(string transporterid)
        {
            List<messageresponse> res = new List<messageresponse>();
            try
            {
                res = _db.Query<messageresponse>("USp_GetTDScheck", new { p_transporterid = transporterid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public List<messageresponse> GetIsTransferToHo(string transporterid)
        {
            List<messageresponse> res = new List<messageresponse>();
            try
            {
                res = _db.Query<messageresponse>("USp_GetTransfertoho", new { p_transporterid = transporterid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public List<messageresponse> GetReverseCharge(string transporterid)
        {
            List<messageresponse> res = new List<messageresponse>();
            try
            {
                res = _db.Query<messageresponse>("USp_GetTreversecharge", new { p_transporterid = transporterid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return res;
        }
        public List<Transporterbyinvoice> BindTransporterbyInvoice(string tag,string invoiceid)
        {
            List<Transporterbyinvoice> Transporterbyinvoice = new List<Transporterbyinvoice>();
            try
            {
                Transporterbyinvoice = _db.Query<Transporterbyinvoice>("USp_GetTransporterbyimvoice_fac", new { p_tag = tag , p_invoiceid =invoiceid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transporterbyinvoice;
        }


        public List<Transporterbyinvoice> BindTransporterbyInvoicev2(string tag, string invoiceid)
        {
            List<Transporterbyinvoice> Transporterbyinvoice = new List<Transporterbyinvoice>();
            try
            {
                Transporterbyinvoice = _db.Query<Transporterbyinvoice>("USp_GetTransporterbyimvoice", new { p_tag = tag, p_invoiceid = invoiceid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transporterbyinvoice;
        }

        public List<Invoicenomodel> BindAllInvoicenoNEWV2depot(string tag, string depoid, string transporterbillid, string finyr)
        {
            List<Invoicenomodel> Invoicenomodel = new List<Invoicenomodel>();
            try
            {
                Invoicenomodel = _db.Query<Invoicenomodel>("USP_BIND_INVOICENO_ERPv2", new { p_tag = tag, P_DEPOTID = depoid, P_TRANSPORTERBILLID = transporterbillid, P_FINYEAR = finyr }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Invoicenomodel;
        }
        public List<Invoicenomodel> BindAllInvoicenoNEWV2(string tag, string depoid,string transporterbillid,string finyr)
        {
            List<Invoicenomodel> Invoicenomodel = new List<Invoicenomodel>();
            try
            {
                Invoicenomodel = _db.Query<Invoicenomodel>("USP_BIND_TRANSPORTER_INVOICENO_FAC", new { p_tag = tag, P_DEPOTID = depoid, P_TRANSPORTERBILLID= transporterbillid, P_FINYEAR=finyr }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Invoicenomodel;
        }
        
        public List<GSTpercentage> BindGstPercentage_New(string depoid, string tranporterid, string bvalue, string InvoiceId, string BillingType, string Fromstate, string Tostate, string RCM)
        {
            decimal value = decimal.Parse(bvalue);
            int frmst = Int32.Parse(Fromstate);
            int tost = Int32.Parse(Tostate);
            List<GSTpercentage> GSTpercentage = new List<GSTpercentage>();
            try
            {
                GSTpercentage = _db.Query<GSTpercentage>("USP_TAXCALCULATE_NEW", new { P_DEPOTID = depoid, P_TRANSPORTER = tranporterid, p_VALUE = value, p_INVOICEID = InvoiceId, p_BILLINGTYPE= BillingType , p_FROMSTATE =frmst, p_TOSTATE =tost , p_RCM =RCM}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return GSTpercentage;
        }
        public List<GSTpercentage> BindGstPercentage(string depoid, string tranporterid, string bvalue, string InvoiceId, string BillingType, string Fromstate, string Tostate)
        {
            decimal value = decimal.Parse(bvalue);
            int frmst = Int32.Parse(Fromstate);
            int tost = Int32.Parse(Tostate);
            List<GSTpercentage> GSTpercentage = new List<GSTpercentage>();
            try
            {
                GSTpercentage = _db.Query<GSTpercentage>("USP_TAXCALCULATE", new { P_DEPOTID = depoid, P_TRANSPORTER = tranporterid, p_VALUE = value, p_INVOICEID = InvoiceId, p_BILLINGTYPE = BillingType, p_FROMSTATE = frmst, p_TOSTATE = tost }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return GSTpercentage;
        }
        public List<GSTpercentage> TDSPercentage(string tranporterid, string bvalue,string billdate)
        {
            decimal value = decimal.Parse(bvalue);
            
            List<GSTpercentage> GSTpercentage = new List<GSTpercentage>();
            try
            {
                GSTpercentage = _db.Query<GSTpercentage>("USP_GetTdspercentage_transporter", new {P_TRANSPORTER = tranporterid, p_VALUE = value, p_billdate = billdate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return GSTpercentage;
        }
        public List<messageresponse> SaveTransporterbillV2fac(Transporterbillfac trans,   string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {

                response = _db.Query<messageresponse>("SP_TRANSPORTERBILL_INSERT_UPDATE_GST_V3_FAC",
                                                            new
                                                            {
                                                                p_TRANSPORTERBILLID = trans.ID,
                                                                p_FLAG = trans.Mode,
                                                                p_TRANSPORTERID = trans.TransporterID,
                                                                p_TRANSPORTERNAME = trans.TransporterName,
                                                                p_BILLDATE = trans.billdate,
                                                                p_BILLTYPEID = trans.billtype,
                                                                p_REMARKS = trans.Remarks,
                                                                p_CREATEDBY = trans.UserID,
                                                                p_FINYEAR = trans.finyear,
                                                                p_TransporterBillDetails = xml,
                                                                p_MODULEID = trans.MenuId,
                                                                p_TOTALNETAMOUNT = trans.TOTALNETAMOUNT,
                                                                p_TOTALTDS = trans.TOTALTDS,
                                                                p_TOTALGROSSWEIGHT = trans.TOTALGROSSWEIGHT,
                                                                p_DEPOTID = trans.depotid,
                                                                p_DEPOTNAME = trans.depotname,
                                                                p_ISTRANSFERTOHO = trans.IsTransferToHO,
                                                                p_TOTALBILLAMOUNT = trans.TOTALBILLAMOUNT,
                                                                p_TOTALTDSDEDUCTABLEAMOUNT = trans.TOTALTDSDEDUCTABLE,
                                                                p_TOTALCGST = trans.sumcgst,
                                                                p_TOTALSGST = trans.sumsgst,
                                                                p_TOTALIGST = trans.sumigst,
                                                                p_TOTALUGST = trans.sumugst,
                                                                p_BILLINGFROMSTATEID = trans.BILLINGFROMSTATEID,
                                                                p_REVERSECHARGE = trans.Reversecharge,
                                                                p_GNGNO = trans.GNGNO,
                                                                p_TDSAPPLICABLE = trans.TDSAPPLICABLE,
                                                                p_TDSPECENTAGE = trans.TDSPECENTAGE,
                                                                p_TDSID = trans.TDSID,
                                                                p_virtualdepotid = trans.VIRTUALdepotid,
                                                                p_EXPORTTAG = trans.EXPORTTAG,
                                                                p_reasonid = trans.ReasonID,
                                                                p_GSTREASONID = trans.GSTReasonID,
                                                            }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch(Exception ex)
            {

            }
            return response;
        }
        public List<messageresponse> SaveTransporterbillV2(Transporterbillfac trans, string xml)

        {
            List<messageresponse> response = new List<messageresponse>();
            try
            {

                response = _db.Query<messageresponse>("SP_TRANSPORTERBILL_INSERT_UPDATE_GST_V3",
                                                            new
                                                            {
                                                                p_TRANSPORTERBILLID = trans.ID,
                                                                p_FLAG = trans.Mode,
                                                                p_TRANSPORTERID = trans.TransporterID,
                                                                p_TRANSPORTERNAME = trans.TransporterName,
                                                                p_BILLDATE = trans.billdate,
                                                                p_BILLTYPEID = trans.billtype,
                                                                p_REMARKS = trans.Remarks,
                                                                p_CREATEDBY = trans.UserID,
                                                                p_FINYEAR = trans.finyear,
                                                                p_TransporterBillDetails = xml,
                                                                p_MODULEID = trans.MenuId,
                                                                p_TOTALNETAMOUNT = trans.TOTALNETAMOUNT,
                                                                p_TOTALTDS = trans.TOTALTDS,
                                                                p_TOTALGROSSWEIGHT = trans.TOTALGROSSWEIGHT,
                                                                p_DEPOTID = trans.depotid,
                                                                p_DEPOTNAME = trans.depotname,
                                                                p_ISTRANSFERTOHO = trans.IsTransferToHO,
                                                                p_TOTALBILLAMOUNT = trans.TOTALBILLAMOUNT,
                                                                p_TOTALTDSDEDUCTABLEAMOUNT = trans.TOTALTDSDEDUCTABLE,
                                                                p_TOTALCGST = trans.sumcgst,
                                                                p_TOTALSGST = trans.sumsgst,
                                                                p_TOTALIGST = trans.sumigst,
                                                                p_TOTALUGST = trans.sumugst,
                                                                p_BILLINGFROMSTATEID = trans.BILLINGFROMSTATEID,
                                                                p_REVERSECHARGE = trans.Reversecharge,
                                                                p_GNGNO = trans.GNGNO,
                                                                p_TDSAPPLICABLE = trans.TDSAPPLICABLE,
                                                                p_TDSPECENTAGE = trans.TDSPECENTAGE,
                                                                p_TDSID = trans.TDSID,
                                                                p_virtualdepotid = trans.VIRTUALdepotid,
                                                                p_EXPORTTAG = trans.EXPORTTAG,
                                                                p_reasonid = trans.ReasonID,
                                                                p_GSTREASONID = trans.GSTReasonID,
                                                            }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public List<Transporterbillshow> BindTransporterbillall(string depotid, string fromData, string ToDate, string FinYear)
        {
            List<Transporterbillshow> Transporterbillshow = new List<Transporterbillshow>();
            try
            {
                Transporterbillshow = _db.Query<Transporterbillshow>("USP_BIND_TRANSPORTERBILL_FORALLUSER_FAC", new { p_DEPOTID = depotid, P_FROMDATE = fromData, P_TODATE = ToDate, P_FINYEAR = FinYear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transporterbillshow;
        }
        public List<Transporterbillshow> BindTransporterbillalldepot(string depotid, string fromData, string ToDate, string FinYear)
        {
            List<Transporterbillshow> Transporterbillshow = new List<Transporterbillshow>();
            try
            {
                Transporterbillshow = _db.Query<Transporterbillshow>("USP_BIND_TRANSPORTERBILL_FORALLUSER_v2", new { p_DEPOTID = depotid, P_FROMDATE = fromData, P_TODATE = ToDate }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transporterbillshow;
        }
        public List<messageresponse> DeleteTransporterbill(string TransporterbillID)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("SP_TRANSPORTERBILL_DELETEv2", new { p_TRANSPORTERBILLID = TransporterbillID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<messageresponse> DeleteTransporterbilldepot(string TransporterbillID)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("SP_TRANSPORTERBILL_DELETEdepotv2", new { p_TRANSPORTERBILLID = TransporterbillID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<messageresponse> Getstatusforedit(string TransporterbillID)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("USP_Getstatsuforedit_transporter", new { p_TRANSPORTERBILLID = TransporterbillID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<messageresponse> CheckTDSDelete(string TransporterbillID)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("SP_CHECK_TDS_DELETEv2", new { P_ACCENTRYID = TransporterbillID }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<Transporterbillhdr> EditTransporterbill(string ID)
        {
            List<Transporterbillhdr> Transporterbillhdr = new List<Transporterbillhdr>();
            try
            {
                Transporterbillhdr = _db.Query<Transporterbillhdr>("sp_TRANSPORTER_DETAILS", new { p_TRANSPORTERBILLID = ID}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return Transporterbillhdr;
        }
        public List<messageresponse> GetAccEntryID(string transporterbillid)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("USP_GetAccEntryID_transporter", new { p_TRANSPORTERBILLID = transporterbillid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<messageresponse> checkLRGRNOV2(string TAG, string transporterID, string LRGRNO, string TRANSPORETERBILLID, string INVID,string finyear)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("USP_LRGRCHECKINGv2", new { P_TAG = TAG,  P_transporterID = transporterID, P_LRGRNO = LRGRNO, P_TRANSPORETERBILLID = TRANSPORETERBILLID, P_INVID = INVID, P_FINYEAR = finyear }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }
        public List<messageresponse> Edittdsid(string transporterid)
        {
            List<messageresponse> messageresponse = new List<messageresponse>();
            try
            {
                messageresponse = _db.Query<messageresponse>("USP_GetTdspercentageedit_transporter", new { P_TRANSPORTER = transporterid }, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {

            }
            return messageresponse;
        }

    }

}
