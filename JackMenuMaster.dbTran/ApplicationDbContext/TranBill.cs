using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbTran.ApplicationDbContext
{
    public partial class TranBill
    {
        public int BillId { get; set; }
        public int? TableId { get; set; }
        public string? Status { get; set; }
        public string? Remark { get; set; }
        public string? OrderRef { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}
