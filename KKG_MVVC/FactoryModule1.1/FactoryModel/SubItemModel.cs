using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubItemModel
{
    public class SubItemModel
    {

    }
    public class BindSubItemGrid
    {
        public char MODE { get; set; }
        public Int64 SUBTYPEID { get; set; }
        public Int64 PRIMARYITEMTYPEID { get; set; }
        public string ITEMDESC { get; set; }
        public string ALTERNATECODE { get; set; }
        public string SUBITEMCODE { get; set; }
        public string SUBITEMNAME { get; set; }
        public string SUBITEMDESC { get; set; }
        public string STATUS { get; set; }        
        public string ITEMOWNER { get; set; }        
        public string BRID { get; set; }
        public string BRPREFIX { get; set; }
        public string HSE { get; set; }        
    }

    public class CRUDSubItem
    {
        public char MODE { get; set; }
        public Int64 SUBTYPEID { get; set; }
        public Int64 PRIMARYITEMTYPEID { get; set; }
        public string SUBITEMCODE { get; set; }
        public string SUBITEMNAME { get; set; }
        public string SUBITEMDESC { get; set; }
        public string ACTIVE { get; set; }
        public string HSE { get; set; }
        public string ITEMOWNER { get; set; }
        public string BRID { get; set; }
    }

    public class EditList
    {        
        public Int64 PRIMARYITEMTYPEID { get; set; }
        public string SUBITEMCODE { get; set; }
        public string SUBITEMNAME { get; set; }
        public string SUBITEMDESC { get; set; }
        public string STATUS { get; set; }
        public string ITEMOWNER { get; set; }
        public string BRID { get; set; }
        public string BRPREFIX { get; set; }
        public string HSE { get; set; }
    }

    public class SubItemEditList
    {
        public List<EditList> EditList { get; set; }
    }
}
