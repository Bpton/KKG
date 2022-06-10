using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
  public  class TransporterModel
    {
    }
    public class Depomodel
    {

        public string BRID { get; set; }
        public string BRNAME { get; set; }
        public string BRPREFIX { get; set; }
    }
    public class Transportermodel
    {

        public string ID { get; set; }
        public string NAME { get; set; }
    }
    public class Transporterbyinvoice
    {

        public string TRANSPORTERID { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string LRGRNO { get; set; }
        public string LRGRDATE { get; set; }
    }
    public class Invoicenomodel
    {

        public string INVNO { get; set; }
        public string INVID { get; set; }
       
    }
    public class GSTpercentage
    {

        public string CGSTID { get; set; }
        public string CGST_PERCENTAGE { get; set; }
        public string SGSTID { get; set; }
        public string SGST_PERCENTAGE { get; set; }
        public string IGST_PERCENTAGE { get; set; }
        public string IGSTID { get; set; }
        public string PERCENTAGE { get; set; }
        
    }
    public class Transporterbillfac
    {

        public string ID { get; set; }
        public string Mode { get; set; }
        public string TransporterID { get; set; }
        public string TransporterName { get; set; }
        public string billdate { get; set; }
        public string billtype { get; set; }
        public string Remarks { get; set; }
        public string UserID { get; set; }
        public string finyear { get; set; }
        public string MenuId { get; set; }
        public decimal TOTALNETAMOUNT { get; set; }
        public decimal TOTALTDS { get; set; }
        public decimal TOTALGROSSWEIGHT { get; set; }
        public string depotid { get; set; }
        public string depotname { get; set; }
        public string IsTransferToHO { get; set; }
        public decimal TOTALBILLAMOUNT { get; set; }
        public decimal TOTALTDSDEDUCTABLE { get; set; }
        public decimal sumcgst { get; set; }
        public decimal sumsgst { get; set; }

        public decimal sumigst { get; set; }
        public decimal sumugst { get; set; }
        public string BILLINGFROMSTATEID { get; set; }
        public string  Reversecharge { get; set; }
        public string GNGNO { get; set; }
        public string TDSAPPLICABLE { get; set; }
        public decimal TDSPECENTAGE { get; set; }
        public string TDSID { get; set; }
        public string VIRTUALdepotid { get; set; }
        public string EXPORTTAG { get; set; }
        public string ReasonID { get; set; }
        public string GSTReasonID { get; set; }

    }
    public class Transporterbillshow
    {

        public string TRANSPORTERBILLID { get; set; }
        public string DEPOTNAME { get; set; }
        public string TRANSPORTERBILLNO { get; set; }
        public string ENTRYDATE { get; set; }
        public string BILLNO { get; set; }
        public string VOUCHERNO { get; set; }
         public string TRANSPORTERBILLDATE { get; set; }
        public string TRANSPORTERNAME { get; set; }
        public string BILLTYPE { get; set; }
        public string TOTALGROSSWEIGHT { get; set; }
        public string TOTALBILLAMOUNT { get; set; }
        public string TOTALTDS { get; set; }
        public string TOTALSERVICETAX { get; set; }
        public string TOTALNETAMOUNT { get; set; }
        public string DEDUCTABLETDSAMT { get; set; }
        public string NETAMT { get; set; }
        public string TOTALSERVICETAXRCM { get; set; }

        public string ISVERIFIEDDESC { get; set; }
        public string DAYEND { get; set; }
        public string USERNAME { get; set; }
        public string APPROVALPERSONNAME { get; set; }
      
    }
    public class Transporterbillhdr
    {

        public string TRANSPORTERBILLNO { get; set; }
        public string TRANSPORTERBILLDATE { get; set; }
        public string GNGNO { get; set; }

         public string TDS_REASONID { get; set; }
   
        public string GST_REASONID { get; set; }
        public string BILLTYPEID { get; set; }
        public string EXPORTTAG { get; set; }
        public string DEPOTID { get; set; }
        public string VIRTUALDEPOTID { get; set; }
        public string TRANSPORTERID { get; set; }
        public string BILLINGFROMSTATEID { get; set; }
        public string ISTRANSFERTOHO { get; set; }
        public string REVERSECHARGE { get; set; }
        public string TDSAPPLICABLE { get; set; }
        public string TDSPECENTAGE { get; set; }

        public string TOTALNETAMOUNT { get; set; }
        public string TOTALGROSSWEIGHT { get; set; }
        public string TOTALTDS { get; set; }
        public string TOTALTDSDEDUCTABLE { get; set; }
        public string TOTALBILLAMOUNT { get; set; }
        public string TOTALCGST { get; set; }
        public string TOTALSGST { get; set; }
        public string TOTALIGST { get; set; }
        public string REMARKS { get; set; }
        public string BILLNO { get; set; }

    }
    }
