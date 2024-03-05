using System;
using System.Collections.Generic;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class MasUser
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? BillingAddress { get; set; }
        public int? EndpointId { get; set; }
        public int Status { get; set; }
        public DateTime RecordDate { get; set; }
        public int? RecorderId { get; set; }
        public int? RoleId { get; set; }
        public int? ShopId { get; set; }
    }
}
