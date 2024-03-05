using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbTran.ApplicationDbContext
{
    public partial class TranInvoiceItem
    {
        public int InvoiceId { get; set; }
        public int? MenuItemId { get; set; }
        public string? Remark { get; set; }
        public string? OrderRef { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? Qauntity { get; set; }
        public string? MenuItemName { get; set; }
        public decimal? Price { get; set; }
    }
}
