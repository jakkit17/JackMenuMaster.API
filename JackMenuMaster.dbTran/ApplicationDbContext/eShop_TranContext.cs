using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JackMenuMaster.dbTran.ApplicationDbContext
{
    public partial class eShop_TranContext : DbContext
    {
        public eShop_TranContext()
        {
        }

        public eShop_TranContext(DbContextOptions<eShop_TranContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MasBillItemStatus> MasBillItemStatuses { get; set; } = null!;
        public virtual DbSet<MasBillStatus> MasBillStatuses { get; set; } = null!;
        public virtual DbSet<TranBill> TranBills { get; set; } = null!;
        public virtual DbSet<TranBillItem> TranBillItems { get; set; } = null!;
        public virtual DbSet<TranInvoice> TranInvoices { get; set; } = null!;
        public virtual DbSet<TranInvoiceItem> TranInvoiceItems { get; set; } = null!;
        public virtual DbSet<TranTable> TranTables { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=localhost;Database=eShop_Tran;User ID=jakkit;Password=root17;");
                var connectionStrings = new JackMenuMaster.Services.AppSettingReader().LoadClass<ConnectionStrings>("appsettings.dbTran.json", "ConnectionStrings");
                optionsBuilder.UseSqlServer(connectionStrings?.Configuration ?? "");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Thai_CI_AI");

            modelBuilder.Entity<MasBillItemStatus>(entity =>
            {
                entity.HasKey(e => e.BillItemStatusId)
                    .HasName("PK__mas_item__71EC88D7239B9119_copy1");

                entity.ToTable("mas_bill_item_status");

                entity.Property(e => e.BillItemStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("Bill_Item_Status_ID");

                entity.Property(e => e.BillItemStatusName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Item_Status_Name");
            });

            modelBuilder.Entity<MasBillStatus>(entity =>
            {
                entity.HasKey(e => e.BillStatusId)
                    .HasName("PK__mas_item__71EC88D7239B9119");

                entity.ToTable("mas_bill_status");

                entity.Property(e => e.BillStatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("Bill_Status_ID");

                entity.Property(e => e.BillStatusName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("Bill_Status_Name");
            });

            modelBuilder.Entity<TranBill>(entity =>
            {
                entity.HasKey(e => e.BillId)
                    .HasName("PK__tran_ord__3214EC27590DEBD2_copy1");

                entity.ToTable("tran_bill");

                entity.Property(e => e.BillId)
                    .ValueGeneratedNever()
                    .HasColumnName("Bill_ID");

                entity.Property(e => e.OrderRef)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TableId).HasColumnName("Table_ID");
            });

            modelBuilder.Entity<TranBillItem>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__tran_ord__3214EC27590DEBD2");

                entity.ToTable("tran_bill_items");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Order_ID");

                entity.Property(e => e.BillId).HasColumnName("Bill_ID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItem_ID");

                entity.Property(e => e.MenuItemName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MenuItem_Name");

                entity.Property(e => e.OrderRef)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TranInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("PK__tran_inv__3214EC2759E27F04");

                entity.ToTable("tran_invoice");

                entity.Property(e => e.InvoiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Invoice_ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreaterId).HasColumnName("CreaterID");

                entity.Property(e => e.Discount).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.NetTotal).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SellingPrice).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.ServiceCharge).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.ShopId).HasColumnName("Shop_ID");

                entity.Property(e => e.TableId).HasColumnName("Table_ID");

                entity.Property(e => e.Tip).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Vat).HasColumnType("decimal(19, 4)");
            });

            modelBuilder.Entity<TranInvoiceItem>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("PK__tran_ord__3214EC27590DEBD2_copy2");

                entity.ToTable("tran_invoice_items");

                entity.Property(e => e.InvoiceId)
                    .ValueGeneratedNever()
                    .HasColumnName("Invoice_ID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItem_ID");

                entity.Property(e => e.MenuItemName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("MenuItem_Name");

                entity.Property(e => e.OrderRef)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TranTable>(entity =>
            {
                entity.HasKey(e => e.TableId)
                    .HasName("PK__tran_tab__3214EC27E0BB5B00");

                entity.ToTable("tran_table");

                entity.Property(e => e.TableId)
                    .ValueGeneratedNever()
                    .HasColumnName("Table_ID");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.CreaterId).HasColumnName("CreaterID");

                entity.Property(e => e.Ref)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
