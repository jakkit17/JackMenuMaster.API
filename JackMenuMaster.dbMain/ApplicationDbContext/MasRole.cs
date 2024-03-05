using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MasRole
    {
        public int Id { get; set; }
        public string Role { get; set; } = null!;
        public string? Remark { get; set; }
    }
}
