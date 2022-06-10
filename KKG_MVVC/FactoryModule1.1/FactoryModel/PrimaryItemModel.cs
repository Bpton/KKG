using System;
using System.Collections.Generic;

namespace PrimaryItemModel
{
    public class PrimaryItemModel
    {
    }
    public class BindPrimaryItemGrid
    {
        public char MODE { get; set; }
        public Int64 ID { get; set; }
        public string ALTERNATECODE { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEM_Name { get; set; }
        public string ITEMDESC { get; set; }
        public string PREDEFINE { get; set; }
        public string STATUS { get; set; }
        public string ACTIVE { get; set; }
        public string ITEMOWNER { get; set; }
        public string ISSERVICE { get; set; }        
    }
    public class CRUDPrimaryItem
    {
        public char MODE { get; set; }
        public Int64 ID { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEM_Name { get; set; }
        public string ITEMDESC { get; set; }
        public string PREDEFINE { get; set; }
        public string ACTIVE { get; set; }
        public string ISSERVICE { get; set; }
        public string ITEMOWNER { get; set; }
    }
    
    public class HeaderEditList
    {
        public string ITEMCODE { get; set; }
        public string ITEM_Name { get; set; }
        public string ITEMDESC { get; set; }
        public string ACTIVE { get; set; }
        public string ISSERVICE { get; set; }
        public string ITEMOWNER { get; set; }
    }

    public class PrimaryEditList
    {
        public List<HeaderEditList> HeaderEditList { get; set; }
    }
}
