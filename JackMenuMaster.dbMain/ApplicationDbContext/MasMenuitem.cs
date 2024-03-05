using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MasMenuitem
    {
        public int MenuItemId { get; set; }
        public int? MenuGroupId { get; set; }
        public string? Subject { get; set; }
        public decimal? Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreaterId { get; set; }
        public string? ImageUrl { get; set; }
    }
}
