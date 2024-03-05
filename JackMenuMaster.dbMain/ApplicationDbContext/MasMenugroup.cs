using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MasMenugroup
    {
        public int MenuGroupId { get; set; }
        public int? ShopId { get; set; }
        public string? GroupName { get; set; }
        public string? Remark { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreaterId { get; set; }
    }
}
