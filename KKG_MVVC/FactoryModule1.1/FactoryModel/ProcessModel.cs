using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace FactoryModel
{
    public class FactoryProcessModel
    {
        public string FLAG { get; set; }
        public string Processid { get; set; }
        public string Processcode { get; set; }
        public string Processname { get; set; }
        public string ProcessDesc { get; set; }
        public string active { get; set; }
        public string Bomid { get; set; }
        public string Bomcode { get; set; }
        public string BomName { get; set; }
        public string BomDesc { get; set; }
        public string ProductID { get; set; }
        public string TabProcessname { get; set; }
        public List<SequenceTypeList> SequenceTypeList { get; set; }
        public List<InputMaterialTypeList> InputMaterialTypeList { get; set; }
        public List<OutputMaterialTypeList> OutputMaterialTypeList { get; set; }
        public List<FGMaterialTypeList> FGMaterialTypeList { get; set; }
        public List<WorkstationTypeList> WorkstationTypeList { get; set; }
        public List<ResourceTypeList> ResourceTypeList { get; set; }
        public List<QCTypeList> QCTypeList { get; set; }

    }
    public class ProcessMasterList
    {
        public string ProcessID { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string ProcessDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModificationDate { get; set; }
        public string IsApproved { get; set; }
        public string Active { get; set; }
        public string ProcessTotalDuration { get; set; }

    }
    public class ProcessEdit
    {
        public string ProcessID { get; set; }
        public string ProcessCode { get; set; }
        public string ProcessName { get; set; }
        public string ProcessDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModificationDate { get; set; }
        public string IsApproved { get; set; }
        public string Active { get; set; }
        public string ProcessTotalDuration { get; set; }

    }

    public class BomProductList
    {
        public string PRODUCTID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string PRODUCTNAME { get; set; }
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string UNIT { get; set; }
        public string MRP { get; set; }
        public string DTOC { get; set; }
        public string CBU { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }
    }

    public class BomMasterList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessFrameworkCode { get; set; }
        public string ProcessFrameworkName { get; set; }
        public string ProcessFrameworkDescription { get; set; }
        public string CreatedBy { get; set; }
        public string CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModificationDate { get; set; }
        public string IsApproved { get; set; }
        public string ItemID { get; set; }
        public string Active { get; set; }
        public string ProcessTotalDuration { get; set; }

    }

    public class MaterialInputSourceList
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string UNIT { get; set; }
        public string MRP { get; set; }
        public string DTOC { get; set; }
        public string CBU { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }

    }

    public class MaterialOutputSourceList
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string DIVID { get; set; }
        public string DIVNAME { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string UNIT { get; set; }
        public string MRP { get; set; }
        public string DTOC { get; set; }
        public string CBU { get; set; }
        public string STATUS { get; set; }
        public string TYPE { get; set; }

    }

    public class WorkstationSourceList
    {
        public string WorkStationID { get; set; }
        public string WorkStationCode { get; set; }
        public string WorkStationName { get; set; }
    }

    public class ResourceSourceList
    {
        public string UTID { get; set; }
        public string UTNAME { get; set; }
    }

    public class QCSourceList
    {
        public string QCID { get; set; }
        public string QCCode { get; set; }
        public string QCName { get; set; }
    }

    public class InputUomList
    {
        public string INPUTUOMID { get; set; }
        public string INPUTUOMDESCRIPTION { get; set; }
    }

    public class OutputUomList
    {
        public string OUTPUTUOMID { get; set; }
        public string OUTPUTUOMDESCRIPTION { get; set; }
    }

    public class ProcessSequenceList
    {
        public List<MaterialInputSourceList> MaterialInputSourceList { get; set; }
        public List<MaterialOutputSourceList> MaterialOutputSourceList { get; set; }
        public List<WorkstationSourceList> WorkstationSourceList { get; set; }
        public List<ResourceSourceList> ResourceSourceList { get; set; }
        public List<QCSourceList> QCSourceList { get; set; }
        public List<InputUomList> InputUomList { get; set; }
        public List<OutputUomList> OutputUomList { get; set; }
    }

    public class SequenceTypeList
    {
        public string ProcessID { get; set; }
        public int TotalDurationInHour { get; set; }
    }

    public class InputMaterialTypeList
    {
        public string ProcessID { get; set; }
        public string ItemID { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal REFQty { get; set; }
    }

    public class OutputMaterialTypeList
    {
        public string ProcessID { get; set; }
        public string ItemID { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal REFQty { get; set; }
    }

    public class FGMaterialTypeList
    {
        public string ProcessID { get; set; }
        public string ItemID { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal REFQty { get; set; }
    }

    public class WorkstationTypeList
    {
        public string ProcessID { get; set; }
        public string WorkstationID { get; set; }
        public int DurationInHour { get; set; }
    }

    public class ResourceTypeList
    {
        public string ProcessID { get; set; }
        public string ResourceID { get; set; }
        public int NoOfResource { get; set; }
    }

    public class QCTypeList
    {
        public string ProcessID { get; set; }
        public string QCID { get; set; }
    }

    public class FrameworkHeaderList
    {
        public string FrameworkID { get; set; }
        public string FrameworkName { get; set; }
        public string FrameworkCode { get; set; }
        public string FrameworkDesc { get; set; }
        public string ProductID { get; set; }
    }

    public class FrameworkSequenceList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string ProcessName { get; set; }
        public string ProcessCode { get; set; }
        public string TotalDurationInHour { get; set; }
    }

    public class FrameworkInputMaterialList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string Code { get; set; }
        public string ItemID { get; set; }
        public string Product { get; set; }
        public string UnitID { get; set; }
        public string UOMDESCRIPTION { get; set; }
        public string Qty { get; set; }
        public string RefQty { get; set; }
        public string TYPE { get; set; }
        public string ProductType { get; set; }
    }

    public class FrameworkOutputMaterialList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string Code { get; set; }
        public string ItemID { get; set; }
        public string Product { get; set; }
        public string UnitID { get; set; }
        public string UOMDESCRIPTION { get; set; }
        public string Qty { get; set; }
        public string RefQty { get; set; }
        public string TYPE { get; set; }
        public string ProductType { get; set; }
    }

    public class FrameworkFGMaterialList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string Code { get; set; }
        public string ItemID { get; set; }
        public string Product { get; set; }
        public string UnitID { get; set; }
        public string UOMDESCRIPTION { get; set; }
        public string Qty { get; set; }
        public string RefQty { get; set; }
        public string TYPE { get; set; }
        public string ProductType { get; set; }
    }

    public class FrameworkWorkstationList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string WorkstationID { get; set; }
        public string WorkStationCode { get; set; }
        public string WorkStationName { get; set; }
        public string DurationInHour { get; set; }
    }

    public class FrameworkResourceList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string ResourceID { get; set; }
        public string UTNAME { get; set; }
        public string NoOfResource { get; set; }
    }

    public class FrameworkQCList
    {
        public string ProcessFrameworkID { get; set; }
        public string ProcessID { get; set; }
        public string QCID { get; set; }
        public string QCCode { get; set; }
        public string QCName { get; set; }
    }

    public class FrameworkProcessList
    {
        public string ProcessID { get; set; }
    }

    public class FrameworkEditList
    {
        public List<FrameworkHeaderList> FrameworkHeaderList { get; set; }
        public List<FrameworkSequenceList> FrameworkSequenceList { get; set; }
        public List<FrameworkInputMaterialList> FrameworkInputMaterialList { get; set; }
        public List<FrameworkOutputMaterialList> FrameworkOutputMaterialList { get; set; }
        public List<FrameworkFGMaterialList> FrameworkFGMaterialList { get; set; }
        public List<FrameworkWorkstationList> FrameworkWorkstationList { get; set; }
        public List<FrameworkResourceList> FrameworkResourceList { get; set; }
        public List<FrameworkQCList> FrameworkQCList { get; set; }
        public List<FrameworkProcessList> FrameworkProcessList { get; set; }
    }

    public class TYPES
    {
        public string TYPENAME { get; set; }
        public string ProcessID { get; set; }
        public string Bomid { get; set; }
        public string BomName { get; set; }
    }
}
