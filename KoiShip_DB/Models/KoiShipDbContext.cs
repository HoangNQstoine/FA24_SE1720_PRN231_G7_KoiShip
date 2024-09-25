using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KoiShip_DB.Data.Models;

public partial class KoiShipDbContext : DbContext
{
    public KoiShipDbContext()
    {
    }

    public KoiShipDbContext(DbContextOptions<KoiShipDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<KoiFish> KoiFishes { get; set; }

    public virtual DbSet<Pricing> Pricings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<ShipMent> ShipMents { get; set; }

    public virtual DbSet<ShippingOrder> ShippingOrders { get; set; }

    public virtual DbSet<ShippingOrderDetail> ShippingOrderDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=KoiShip_DB;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070BC6C43E");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UrlImg).HasColumnName("Url_IMG");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__koiFish__3214EC070F975522");

            entity.ToTable("koiFish");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.ColorPattern).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UrlImg).HasColumnName("Url_IMG");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__koiFish__Categor__1273C1CD");

            entity.HasOne(d => d.User).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__koiFish__User_Id__117F9D94");
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pricing__3214EC0707F6335A");

            entity.ToTable("Pricing");

            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ShippingMethod).HasColumnName("shipping_method");
            entity.Property(e => e.WeightRange).HasColumnName("weight_range");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC077F60ED59");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ShipMent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShipMent__3214EC0715502E78");

            entity.ToTable("ShipMent");

            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("End_Date");
            entity.Property(e => e.EstimatedArrivalDate).HasColumnType("datetime");
            entity.Property(e => e.HealthCheck).HasColumnName("health_check");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("Start_Date");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Vehicle).HasColumnName("vehicle");

            entity.HasOne(d => d.User).WithMany(p => p.ShipMents)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShipMent__User_I__173876EA");
        });

        modelBuilder.Entity<ShippingOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipping__3214EC071A14E395");

            entity.Property(e => e.AdressTo).HasColumnName("Adress_To");
            entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PricingId).HasColumnName("Pricing_Id");
            entity.Property(e => e.ShipMentId).HasColumnName("ShipMent_Id");
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnName("Total_Price");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Pricing).WithMany(p => p.ShippingOrders)
                .HasForeignKey(d => d.PricingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShippingO__Prici__1DE57479");

            entity.HasOne(d => d.ShipMent).WithMany(p => p.ShippingOrders)
                .HasForeignKey(d => d.ShipMentId)
                .HasConstraintName("FK__ShippingO__ShipM__1CF15040");

            entity.HasOne(d => d.User).WithMany(p => p.ShippingOrders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShippingO__User___1BFD2C07");
        });

        modelBuilder.Entity<ShippingOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipping__3214EC0720C1E124");

            entity.HasIndex(e => e.KoiFishId, "UQ__Shipping__7588329A239E4DCF").IsUnique();

            entity.Property(e => e.KoiFishId).HasColumnName("KoiFish_Id");
            entity.Property(e => e.ShippingOrdersId).HasColumnName("ShippingOrders_Id");

            entity.HasOne(d => d.KoiFish).WithOne(p => p.ShippingOrderDetail)
                .HasForeignKey<ShippingOrderDetail>(d => d.KoiFishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShippingO__KoiFi__25869641");

            entity.HasOne(d => d.ShippingOrders).WithMany(p => p.ShippingOrderDetails)
                .HasForeignKey(d => d.ShippingOrdersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShippingO__Shipp__267ABA7A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0703317E3D");

            entity.ToTable("User");

            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__Role_Id__0519C6AF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
