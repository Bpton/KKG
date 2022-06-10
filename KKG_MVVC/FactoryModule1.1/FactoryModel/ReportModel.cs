using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class ReportModel
    {
        public string Date { get; set; }
        public string VchNo { get; set; }
        public string VType { get; set; }
        public string Party { get; set; }
        public string BatchNo { get; set; }
        public string ManfDate { get; set; }
        public string ExpDate { get; set; }
        public decimal MRP { get; set; }
        public decimal Opening { get; set; }
        public decimal Inward { get; set; }
        public decimal Outward { get; set; }
        public decimal Balance { get; set; }
        public decimal INTRANSIST { get; set; }
        public string Depot { get; set; }
    }
    public class ReportBranch
    {
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }
    public class ReportProductlias
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }
    public class ReportStorelocation
    {
        public string STORELOCATIONID { get; set; }
        public string STORELOCATIONNAME { get; set; }
    }
    public class ReportJcMaster
    {
        public string JCID { get; set; }
        public string JCNAME { get; set; }
    }
    public class ReportTimeSpan
    {
        public string TIMESPAN { get; set; }
        public string STARTDATE { get; set; }
        public string ENDDATE { get; set; }
    }
    public class LedgerReportModel
    {
        public string AccEntryID { get; set; }
        public string REGIONNAME { get; set; }
        public string VOUCHERDATE { get; set; }
        public string LedgerName { get; set; }
        public string VoucherTypeNAME { get; set; }
        public string VoucherNo { get; set; }
        public string DOCNO { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string Balance { get; set; }
        public string Balance_DR_CR { get; set; }
        public string Narration { get; set; }
    }

    //Added By Aisha Pain for E-invoice report(04-12-2020)
    public class EinvoicePendingReportModel
    {
        public string AckNo { get; set; }
        public string AckDt { get; set; }
        public string DOCNO { get; set; }
        public string DATE { get; set; }
        public string DEPOT { get; set; }
        public string partyname { get; set; }
    }

}
