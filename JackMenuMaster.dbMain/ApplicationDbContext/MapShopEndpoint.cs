using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MapShopEndpoint
    {
        public int ShopId { get; set; }
        public int EndpointId { get; set; }

        public virtual MasShop Shop { get; set; } = null!;
    }
}
