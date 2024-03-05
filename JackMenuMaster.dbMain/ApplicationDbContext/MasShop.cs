using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MasShop
    {
        public MasShop()
        {
            MapShopEndpoints = new HashSet<MapShopEndpoint>();
        }

        public int ShopId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
        public int? PhoneNumber { get; set; }
        public string? LineId { get; set; }
        public int? Latitude { get; set; }
        public int? Longtitude { get; set; }

        public virtual ICollection<MapShopEndpoint> MapShopEndpoints { get; set; }
    }
}
