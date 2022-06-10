using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryModel
{
    public class ScrapModel
    {
        public char MODE { get; set; }
        public Int64 SCRAPID { get; set; }
        public string SCRAPNO { get; set; }
        public string SCRAPDATE { get; set; }
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string USERTYPEID { get; set; }
        public string USERTYPE { get; set; }
        public string BRANCHID { get; set; }
        public string REMARKS { get; set; }
        public string CREATEDBY { get; set; }
        public string CREATEDDATE { get; set; }
        public string MODIFIEDBY { get; set; }
        public string MODIFIEDDATE { get; set; }
        public string FINYEAR { get; set; }
        public string ISVERIFIED { get; set; }
        public string NEXTLEVELID { get; set; }
        public string VERIFIEDDATETIME { get; set; }
        public string PRIMARYID { get; set; }
        public string SUBID { get; set; }
        public string PRODUCTID { get; set; }
        public string UOMID { get; set; }
        public decimal SCRAPQTY { get; set; }
        public List<ScrapProductDetails> ScrapProductDetails { get; set; }
    }

    public class ScrapProductDetails
    {
        public string PRIMARYID { get; set; }
        public string PRIMARYNAME { get; set; }
        public string SUBID { get; set; }
        public string SUBITEMNAME { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string UOMID { get; set; }
        public string UOM { get; set; }
        public decimal SCRAPQTY { get; set; }
        public decimal RECVDQTY { get; set; }
        public decimal ALLREADYRECVDQTY { get; set; }
    }

    public class UserDepartmentList
    {
        public string UTID { get; set; }
        public string UTNAME { get; set; }
    }

    public class UserList
    {
        public string USERID { get; set; }
        public string USERNAME { get; set; }
    }

    public class ScrapSubItem
    {
        public string SUBTYPEID { get; set; }
        public string SUBITEMNAME { get; set; }
    }

    public class ScrapProduct
    {
        public string ID { get; set; }
        public string PRODUCTALIAS { get; set; }        
    }

    public class ScrapProductUOM
    {        
        public string UOMID { get; set; }
        public string UOMNAME { get; set; }
    }

    public class BindScrapRequest
    {        
        public Int64 SCRAPID { get; set; }
        public string SCRAPNO { get; set; }
        public string SCRAPDATE { get; set; }
        public string USERTYPE { get; set; }
        public string USERNAME { get; set; }
        public string STATUS { get; set; }
        public string ENTRYUSER { get; set; }        
    }

    public class ScrapHeaderRequestEdit
    {        
        public Int64 SCRAPID { get; set; }
        public string SCRAPNO { get; set; }
        public string SCRAPDATE { get; set; }
        public string USERID { get; set; }
        public string USERNAME { get; set; }
        public string USERTYPEID { get; set; }
        public string USERTYPE { get; set; }        
        public string REMARKS { get; set; }
    }

    public class ScrapDetailsRequestEdit
    {
        public string PRIMARYID { get; set; }
        public string PRIMARYNAME { get; set; }
        public string SUBID { get; set; }
        public string SUBITEMNAME { get; set; }
        public string PRODUCTID { get; set; }
        public string PRODUCTNAME { get; set; }
        public string UOMID { get; set; }
        public string UOM { get; set; }
        public decimal SCRAPQTY { get; set; }
        public decimal RECVDQTY { get; set; }
        public decimal ALLREADYRECVDQTY { get; set; }
    }

    public class ScrapEdit
    {
        public List<ScrapHeaderRequestEdit> ScrapHeaderRequestEdit { get; set; }
        public List<ScrapDetailsRequestEdit> ScrapDetailsRequestEdit { get; set; }        
    }
}
