using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class ProductMasterModel
    {
        public string ProductName { get; set; }
        public string ProductShortName { get; set; }
        public string ProductCode { get; set; }
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string CATID { get; set; }
        public string CATNAME { get; set; }
        public string NOPID { get; set; }
        public string NOPNAME { get; set; }
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
        public string UNITCAPACITY { get; set; }
        public string FRGID { get; set; }
        public string FRGNAME { get; set; }
        public string ITID { get; set; }
        public string ITNAME { get; set; }
        public string BESTBEUSE { get; set; }
        public string INRRS { get; set; }
        public string SINGAPOREDOLLAR { get; set; }
        public string NEPALESERUPEE { get; set; }
        public string BANGLADESHITAKA { get; set; }
        public string BRANDCATNAME { get; set; }
        public string PRIMARYPRODUCT { get; set; }
        public string ASSESSABLE { get; set; }
        public string MINSTOCKLEVEL { get; set; }
        public string CSDINDEXNO { get; set; }
        public string INCSINDEXNO { get; set; }
        public string CSDPRINT { get; set; }
        public string CPCPRINT { get; set; }
        public string INCSPRINT { get; set; }
        public string EXPARAPRINT { get; set; }
        public string GTPRINT { get; set; }
        public string MTPRINT { get; set; }
        public string EXPORTPRINT { get; set; }
        public string BARCODE { get; set; }
        public string BRID { get; set; }
        public string BRNAME { get; set; }
    }


    public class EmployeeDetails
    {
        public  int EmployeId { get; set; }
        public  string EmployeeName { get; set; }
        public  string Designation { get; set; }
        public  string Ismaxsalary { get; set; }
        public  string Isminsalary { get; set; }
        public  decimal salary { get; set; }
        
    }
}
