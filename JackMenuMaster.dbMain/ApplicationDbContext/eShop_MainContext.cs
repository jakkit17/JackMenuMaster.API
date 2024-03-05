using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JackMenuMaster.dbMain.ApplicationDbContext
{
    public partial class eShop_MainContext : DbContext
    {
        public eShop_MainContext()
        {
        }

        public eShop_MainContext(DbContextOptions<eShop_MainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MapShopEndpoint> MapShopEndpoints { get; set; } = null!;
        public virtual DbSet<MapUserShop> MapUserShops { get; set; } = null!;
        public virtual DbSet<MasEndpoint> MasEndpoints { get; set; } = null!;
        public virtual DbSet<MasMenugroup> MasMenugroups { get; set; } = null!;
        public virtual DbSet<MasMenuitem> MasMenuitems { get; set; } = null!;
        public virtual DbSet<MasRole> MasRoles { get; set; } = null!;
        public virtual DbSet<MasShop> MasShops { get; set; } = null!;
        public virtual DbSet<MasUser> MasUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=localhost;Database=eShop_Main;User ID=jakkit;Password=root17;");
                var connectionStrings = new JackMenuMaster.Services.AppSettingReader().LoadClass<ConnectionStrings>("appsettings.dbMain.json", "ConnectionStrings");
                optionsBuilder.UseSqlServer(connectionStrings?.Configuration ?? "");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_CI_AS");

            modelBuilder.Entity<MapShopEndpoint>(entity =>
            {
                entity.HasKey(e => new { e.ShopId, e.EndpointId })
                    .HasName("PK__map_shop__2F7051535245F130");

                entity.ToTable("map_shop_endpoint");

                entity.Property(e => e.ShopId).HasColumnName("Shop_ID");

                entity.Property(e => e.EndpointId).HasColumnName("Endpoint_ID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.MapShopEndpoints)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Shop_ID");
            });

            modelBuilder.Entity<MapUserShop>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ShopId })
                    .HasName("PK__map_User__35AD18FD01D50B5F");

                entity.ToTable("map_user_shop");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.Property(e => e.ShopId).HasColumnName("Shop_ID");
            });

            modelBuilder.Entity<MasEndpoint>(entity =>
            {
                entity.HasKey(e => e.EndpointId)
                    .HasName("PK__mas_endp__3214EC27BAF6EEF4");

                entity.ToTable("mas_endpoint");

                entity.Property(e => e.EndpointId)
                    .ValueGeneratedNever()
                    .HasColumnName("Endpoint_ID");

                entity.Property(e => e.EndpointUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Endpoint_URL");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasMenugroup>(entity =>
            {
                entity.HasKey(e => e.MenuGroupId)
                    .HasName("PK__mas_menu__3214EC277241C458");

                entity.ToTable("mas_menugroup");

                entity.Property(e => e.MenuGroupId)
                    .ValueGeneratedNever()
                    .HasColumnName("MenuGroupID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreaterId).HasColumnName("CreaterID");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .UseCollation("Thai_CI_AI");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .UseCollation("Thai_CI_AI");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .UseCollation("Thai_CI_AI");
            });

            modelBuilder.Entity<MasMenuitem>(entity =>
            {
                entity.HasKey(e => e.MenuItemId)
                    .HasName("PK__mas_menu__3214EC271EBD6AEB");

                entity.ToTable("mas_menuitem");

                entity.Property(e => e.MenuItemId)
                    .ValueGeneratedNever()
                    .HasColumnName("MenuItem_ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CreaterId).HasColumnName("CreaterID");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .UseCollation("Thai_CI_AI");

                entity.Property(e => e.MenuGroupId).HasColumnName("MenuGroupID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .UseCollation("Thai_CI_AI");
            });

            modelBuilder.Entity<MasRole>(entity =>
            {
                entity.ToTable("mas_role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasShop>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK__mas_Shop__3214EC27B5A2DB01");

                entity.ToTable("mas_shop");

                entity.Property(e => e.ShopId)
                    .ValueGeneratedNever()
                    .HasColumnName("Shop_ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LineId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LineID");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MasUser>(entity =>
            {
                entity.ToTable("mas_user");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasColumnType("ntext");

                entity.Property(e => e.BillingAddress)
                    .HasColumnType("ntext")
                    .HasColumnName("Billing_Address");

                entity.Property(e => e.EndpointId).HasColumnName("Endpoint_ID");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RecordDate).HasColumnType("datetime");

                entity.Property(e => e.RecorderId).HasColumnName("Recorder_ID");

                entity.Property(e => e.RoleId).HasColumnName("Role_ID");

                entity.Property(e => e.ShopId).HasColumnName("Shop_ID");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
