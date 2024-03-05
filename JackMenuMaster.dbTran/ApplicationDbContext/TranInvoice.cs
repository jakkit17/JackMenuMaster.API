using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbTran.ApplicationDbContext
{
    public partial class TranInvoice
    {
        public int InvoiceId { get; set; }
        public int? TableId { get; set; }
        public decimal? SellingPrice { get; set; }
        public decimal? Vat { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? Tip { get; set; }
        public decimal? Discount { get; set; }
        public decimal? NetTotal { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public int? CreaterId { get; set; }
        public string? Remark { get; set; }
        public int? ShopId { get; set; }
    }
}
