using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FactoryModel;

namespace FactoryDatacontext
{
    public class FactoryProcessContext
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["Factconn"].ConnectionString);

        public List<MessageModel> ProcessMasterInsertUpdate(FactoryProcessModel process, int Createdby)
        {
            
            FactoryProcessModel processmodel = new FactoryProcessModel();
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_PROCESS_INSERT_UPDATE_DELETE_V2 ",
                                                        new
                                                        {
                                                            ProcessID = process.Processid,
                                                            ProcessCode = process.Processcode,
                                                            ProcessName = process.Processname,
                                                            ProcessDescription = process.ProcessDesc,
                                                            CreatedBy = Createdby,
                                                            ModifiedBy = Createdby,
                                                            Mode = process.FLAG,
                                                            Active = process.active
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<ProcessMasterList> BindProcessMasterGrid(string processid)
        {
            List<ProcessMasterList> processGrid = new List<ProcessMasterList>();
            try
            {
                processGrid = _db.Query<ProcessMasterList>("SP_FetchProcessRecord ", new { ProcessID = processid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return processGrid;
        }

        public List<MessageModel> DeleteProcess(string processid)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("USP_PROCESS_INSERT_UPDATE_DELETE_V2 ",
                                                    new
                                                    {
                                                        ProcessID = processid,
                                                        Mode = "D"
                                                    }, 
                                                    commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<ProcessEdit> EditProcess(string processid)
        {
            List<ProcessEdit> editprocess = new List<ProcessEdit>();
            try
            {
                editprocess = _db.Query<ProcessEdit>("SP_FetchProcessRecord ", new { ProcessID = processid}, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
            }
            return editprocess;
        }

        public List<BomProductList> GetBomProduct()
        {
            List<BomProductList> bomproduct = new List<BomProductList>();

            try
            {
                bomproduct = _db.Query<BomProductList>("USP_GET_BOM_PRODUCT ", commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return bomproduct;
        }
        

        public List<BomMasterList> BindFrameworkGrid()
        {
            List<BomMasterList> frameworkGrid = new List<BomMasterList>();
            try
            {
                frameworkGrid = _db.Query<BomMasterList>("USP_GET_FRAMEWORK_LIST_V2 ",commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return frameworkGrid;
        }

        public ProcessSequenceList ProcessSequence(string userID)
        {
            ProcessSequenceList processSequence = new ProcessSequenceList();
            try
            {
                var reader = _db.QueryMultiple("USP_BIND_PROCESS_SEQUENCE_V2", new { P_USERID = userID }, commandType: CommandType.StoredProcedure);
                var vinputmaterial = reader.Read<MaterialInputSourceList>().ToList();
                var voutputmaterial = reader.Read<MaterialOutputSourceList>().ToList();
                var vworkstation = reader.Read<WorkstationSourceList>().ToList();
                var vresource = reader.Read<ResourceSourceList>().ToList();
                var vqc = reader.Read<QCSourceList>().ToList();
                var vinputuom = reader.Read<InputUomList>().ToList();
                var voutputuom = reader.Read<OutputUomList>().ToList();


                processSequence.MaterialInputSourceList = vinputmaterial;
                processSequence.MaterialOutputSourceList = voutputmaterial;
                processSequence.WorkstationSourceList = vworkstation;
                processSequence.ResourceSourceList = vresource;
                processSequence.QCSourceList = vqc;
                processSequence.InputUomList = vinputuom;
                processSequence.OutputUomList = voutputuom;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return processSequence;
        }

        public List<MessageModel> BomInsertUpdate(FactoryProcessModel bom, int CreatedBy)


        {
            DataTable dtSequence;
            DataTable dtInputMaterial;
            DataTable dtOutputMaterial;
            DataTable dtFGMaterial;
            //DataTable dtWorkstation;
            //DataTable dtResource;
            //DataTable dtQC;
            dtSequence = SequenceTypeListDetails(bom.SequenceTypeList);
            dtInputMaterial = InputMaterialTypeListDetails(bom.InputMaterialTypeList);
            dtOutputMaterial = OutputMaterialTypeListDetails(bom.OutputMaterialTypeList);
            dtFGMaterial = FGMaterialTypeListDetails(bom.FGMaterialTypeList);
            //dtWorkstation = WorkstationTypeListDetails(bom.WorkstationTypeList);
            //dtResource = ResourceTypeListDetails(bom.ResourceTypeList);
            //dtQC = QCTypeListDetails(bom.QCTypeList);
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_PROCESS_FRAMEWORK_INSERT_UPDATE_V2 ",
                                                        new
                                                        {
                                                            P_PROCESSFRAMEWORKNAME = bom.BomName,
                                                            P_PROCESSFRAMEWORKCODE = bom.Bomcode,
                                                            P_PROCESSFRAMEWORKDESCRIPTION = bom.BomDesc,
                                                            P_PROCESSFRAMEWORKPRODUCT = bom.ProductID,
                                                            P_CREATEDBY = CreatedBy,
                                                            P_MODIFIEDBY = CreatedBy,
                                                            TempTableSequence = dtSequence.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_SEQUENCE"),
                                                            TempTableInputMaterial = dtInputMaterial.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_INPUT_MATERIAL"),
                                                            TempTableOutputMaterial = dtOutputMaterial.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_OUTPUT_MATERIAL"),
                                                            TempTableFGMaterial = dtFGMaterial.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_FG_MATERIAL"),
                                                            //TempTableWorkstation = dtWorkstation.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_WORKSTATION"),
                                                            //TempTableResource = dtResource.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_RESOURCE"),
                                                            //TempTableQC = dtQC.AsTableValuedParameter("Type_PROCESS_FRAMEWORK_QC"),
                                                            P_FLAG = bom.FLAG,
                                                            P_PROCESSFRAMEWORKID = bom.Bomid,
                                                            P_PROCESSID = bom.Processid
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public DataTable SequenceTypeListDetails(List<SequenceTypeList> SequenceTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("TotalDurationInHour", typeof(Int32));

            int count = 1;
            foreach (var item in SequenceTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.TotalDurationInHour
                            );
                count++;
            }
            return dt;
        }

        public DataTable InputMaterialTypeListDetails(List<InputMaterialTypeList> InputMaterialTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("ItemID", typeof(string));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("REFQty", typeof(decimal));

            int count = 1;
            foreach (var item in InputMaterialTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.ItemID,
                            item.Qty,
                            item.Unit,
                            item.REFQty
                            );
                count++;
            }
            return dt;
        }

        public DataTable OutputMaterialTypeListDetails(List<OutputMaterialTypeList> OutputMaterialTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("ItemID", typeof(string));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("REFQty", typeof(decimal));

            int count = 1;
            foreach (var item in OutputMaterialTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.ItemID,
                            item.Qty,
                            item.Unit,
                            item.REFQty
                            );
                count++;
            }
            return dt;
        }

        public DataTable FGMaterialTypeListDetails(List<FGMaterialTypeList> FGMaterialTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("ItemID", typeof(string));
            dt.Columns.Add("Qty", typeof(decimal));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("REFQty", typeof(decimal));

            int count = 1;
            foreach (var item in FGMaterialTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.ItemID,
                            item.Qty,
                            item.Unit,
                            item.REFQty
                            );
                count++;
            }
            return dt;
        }

        public DataTable WorkstationTypeListDetails(List<WorkstationTypeList> WorkstationTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("WorkstationID", typeof(string));
            dt.Columns.Add("DurationInHour", typeof(Int32));

            int count = 1;
            foreach (var item in WorkstationTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.WorkstationID,
                            item.DurationInHour
                            );
                count++;
            }
            return dt;
        }

        public DataTable ResourceTypeListDetails(List<ResourceTypeList> ResourceTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("ResourceID", typeof(string));
            dt.Columns.Add("NoOfResource", typeof(Int32));

            int count = 1;
            foreach (var item in ResourceTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.ResourceID,
                            item.NoOfResource
                            );
                count++;
            }
            return dt;
        }

        public DataTable QCTypeListDetails(List<QCTypeList> QCTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProcessID", typeof(string));
            dt.Columns.Add("QCID", typeof(string));

            int count = 1;
            foreach (var item in QCTypeList)
            {
                dt.Rows.Add(item.ProcessID,
                            item.QCID
                            );
                count++;
            }
            return dt;
        }

        public FrameworkEditList EditFramework(string FrameworkID)
        {
            FrameworkEditList editFramework = new FrameworkEditList();
            try
            {
                var reader = _db.QueryMultiple("USP_PROCESS_FRAMEWORK_DETAILS_V2", new { P_FRAMEWORKID = FrameworkID }, commandType: CommandType.StoredProcedure);

                var vheader = reader.Read<FrameworkHeaderList>().ToList();
                var vsequence = reader.Read<FrameworkSequenceList>().ToList();
                var vinputmaterial = reader.Read<FrameworkInputMaterialList>().ToList();
                var voutputmaterial = reader.Read<FrameworkOutputMaterialList>().ToList();
                var vfgmaterial = reader.Read<FrameworkFGMaterialList>().ToList();
                var vworkstation = reader.Read<FrameworkWorkstationList>().ToList();
                var vresource = reader.Read<FrameworkResourceList>().ToList();
                var vqc = reader.Read<FrameworkQCList>().ToList();
                var vprocess = reader.Read<FrameworkProcessList>().ToList();

                editFramework.FrameworkHeaderList = vheader;
                editFramework.FrameworkSequenceList = vsequence;
                editFramework.FrameworkInputMaterialList = vinputmaterial;
                editFramework.FrameworkOutputMaterialList = voutputmaterial;
                editFramework.FrameworkFGMaterialList = vfgmaterial;
                editFramework.FrameworkWorkstationList = vworkstation;
                editFramework.FrameworkResourceList = vresource;
                editFramework.FrameworkQCList = vqc;
                editFramework.FrameworkProcessList = vprocess;

                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return editFramework;
        }

        public List<MessageModel> DeleteFramework(string frameworkid)
        {
            List<MessageModel> response = new List<MessageModel>();
            try
            {
                response = _db.Query<MessageModel>("USP_PROCESS_FRAMEWORK_DELETE_V2 ", new{ P_FRAMEWORKID = frameworkid},commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

        public List<TYPES> GetInputOutPutTypes(string MODE)
        {
            List<TYPES> TYPES = new List<TYPES>();

            try
            {
                TYPES = _db.Query<TYPES>("USP_GET_ITEM_TYPES ", new { P_MODE = MODE }, commandType: CommandType.StoredProcedure).ToList();


            }
            catch (Exception ex)
            {
            }
            return TYPES;
        }

        public ProcessSequenceList LoadTypeWise(string TYPE,string userid)
        {
            ProcessSequenceList processSequence = new ProcessSequenceList();
            try
            {
                var reader = _db.QueryMultiple("[USP_BIND_PRODUCT_TYPE_WISE]", new { P_TYPE = TYPE, P_USERID= userid }, commandType: CommandType.StoredProcedure);
                var vinputmaterial = reader.Read<MaterialInputSourceList>().ToList();
                var voutputmaterial = reader.Read<MaterialOutputSourceList>().ToList();
                var vinputuom = reader.Read<InputUomList>().ToList();
                var voutputuom = reader.Read<OutputUomList>().ToList();

                processSequence.MaterialInputSourceList = vinputmaterial;
                processSequence.MaterialOutputSourceList = voutputmaterial;
                processSequence.InputUomList = vinputuom;
                processSequence.OutputUomList = voutputuom;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return processSequence;
        }
        public ProcessSequenceList LoadIdWise(string PRODUCTID, string userid)
        {
            ProcessSequenceList processSequence = new ProcessSequenceList();
            try
            {
                var reader = _db.QueryMultiple("[USP_BIND_PRODUCT_PRODUCTID_WISE]", new { P_PRODUCTID = PRODUCTID, P_USERID= userid }, commandType: CommandType.StoredProcedure);
               
                var voutputmaterial = reader.Read<MaterialOutputSourceList>().ToList();
                var voutputuom = reader.Read<OutputUomList>().ToList();
                processSequence.MaterialOutputSourceList = voutputmaterial;
                processSequence.OutputUomList = voutputuom;
                reader.Dispose();
            }
            catch (Exception ex)
            {

            }
            return processSequence;
        }

        public List<MessageModel> NameCheck(string MODE, string NAME)
        {
            List<MessageModel> response = new List<MessageModel>();


            try
            {

                response = _db.Query<MessageModel>("USP_PROCEDURE_FOR_KKG_MODE_WISE ",
                                                        new
                                                        {
                                                            P_MODE= MODE,
                                                            P_NAME= NAME
                                                        },
                                                        commandType: CommandType.StoredProcedure).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

    }
}
