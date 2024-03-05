using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MasEndpoint
    {
        public int EndpointId { get; set; }
        public string EndpointUrl { get; set; } = null!;
        public int Capacity { get; set; }
        public string? Remark { get; set; }
    }
}
