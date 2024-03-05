using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbTran.ApplicationDbContext
{
    public partial class TranTable
    {
        public int TableId { get; set; }
        public string? Ref { get; set; }
        public string? TableName { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? CreaterId { get; set; }
        public int? ShopId { get; set; }
    }
}
