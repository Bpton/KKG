using System.Collections.Generic;

namespace BAL
{
    public class DataModel
    {
    }
    #region DispatchAmtPcs_Calculation Section
    public class DispatchAmtPcs_Calculation
    {
        public List<Assesment> ASSESMENT { get; set; }
        public List<QtyPcs> QTYINPCS { get; set; }
        public List<NetWeight> NETWEIGHT { get; set; }
        public List<GrossWeight> GROSSWEIGHT { get; set; }
        public List<Hsn> HSNCODE { get; set; }
    }
    public class Assesment
    {
        public decimal ASSESMENT { get; set; }
    }

    public class QtyPcs
    {
        public decimal QTYINPCS { get; set; }
    }
    public class NetWeight
    {
        public string NETWEIGHT { get; set; }
    }
    public class GrossWeight
    {
        public string GROSSWEIGHT { get; set; }
    }
    public class Hsn
    {
        public string HSNCODE { get; set; }
    }
    #endregion

    #region VENDOR_AND_CURRENCY Section
    public class VENDOR_AND_CURRENCY
    {
        public List<VENDORID> Vendorid { get; set; }
        public List<CURRENCYID> CURRENCY { get; set; }
    }

    public class VENDORID
    {
        public string Vendorid { get; set; }
        public string VendorName { get; set; }
    }
    public class CURRENCYID
    {
        public string Currencyid { get; set; }
        public string Currencytype { get; set; }
    }
    #endregion

    #region PACKSIZE_MRP_AND_RATE Section
    public class PACKSIZE_MRP_AND_RATE
    {
        public List<PACKSIZEID_FROM> PackSize { get; set; }
        public List<MRP> Mrp { get; set; }
        public List<RATE> Rate { get; set; }
    }

    public class PACKSIZEID_FROM
    {
        public string PackSizeid_From { get; set; }
        public string PackSizeName_From { get; set; }
    }

    public class MRP
    {
        public decimal Mrp { get; set; }
        public decimal AssessablePercent { get; set; }
    }
    public class RATE
    {
        public string Rate { get; set; }
    }


    public class DATEWISERATE
    {
        public string PODATE { get; set; }
        public decimal LASTRATE { get; set; }
    }
    public class MAXRATEVAL
    {
        public decimal MAXRATE { get; set; }

    }
    public class MINRATEVAL
    {
        public decimal MINRATE { get; set; }

    }
    public class AVGRATEVAL
    {
        public decimal AVGRATE { get; set; }

    }
    public class MIN_MAX_AVGRATE
    {
        public List<DATEWISERATE> _datewiseratelist { get; set; }
        public List<MAXRATEVAL> _maxratelist { get; set; }
        public List<MINRATEVAL> _minratelist { get; set; }
        public List<AVGRATEVAL> _avgratelist { get; set; }
    }
    #endregion
    public class POTERMS
    {
        public string TERMS { get; set; }

    }
}
