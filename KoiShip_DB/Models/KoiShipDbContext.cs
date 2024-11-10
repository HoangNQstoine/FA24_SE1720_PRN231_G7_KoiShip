using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
        optionsBuilder.EnableSensitiveDataLogging();
    }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07B597D9DE");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UrlImg).HasColumnName("Url_IMG");
        });

        modelBuilder.Entity<KoiFish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__koiFish__3214EC07075B03F5");

            entity.ToTable("koiFish");

            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.ColorPattern).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UrlImg).HasColumnName("Url_IMG");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Category).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__koiFish__Categor__2E1BDC42");

            entity.HasOne(d => d.User).WithMany(p => p.KoiFishes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__koiFish__User_Id__2D27B809");
        });

        modelBuilder.Entity<Pricing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pricing__3214EC07D6BA7ABD");

            entity.ToTable("Pricing");

            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ShippingMethod).HasColumnName("shipping_method");
            entity.Property(e => e.WeightRange).HasColumnName("weight_range");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0731E683C7");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ShipMent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ShipMent__3214EC0794F45232");

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
                .HasConstraintName("FK__ShipMent__User_I__30F848ED");
        });

        modelBuilder.Entity<ShippingOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipping__3214EC07A2AE5100");

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
                .HasConstraintName("FK__ShippingO__Prici__35BCFE0A");

            entity.HasOne(d => d.ShipMent).WithMany(p => p.ShippingOrders)
                .HasForeignKey(d => d.ShipMentId)
                .HasConstraintName("FK__ShippingO__ShipM__34C8D9D1");

            entity.HasOne(d => d.User).WithMany(p => p.ShippingOrders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ShippingO__User___33D4B598");
        });

        modelBuilder.Entity<ShippingOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Shipping__3214EC07402A9EED");

            entity.HasIndex(e => e.KoiFishId, "UQ__Shipping__7588329A92787F90").IsUnique();

            entity.Property(e => e.KoiFishId).HasColumnName("KoiFish_Id");
            entity.Property(e => e.ShippingOrdersId).HasColumnName("ShippingOrders_Id");

            entity.HasOne(d => d.KoiFish).WithOne(p => p.ShippingOrderDetail)
                .HasForeignKey<ShippingOrderDetail>(d => d.KoiFishId)
                .HasConstraintName("FK__ShippingO__KoiFi__398D8EEE");

            entity.HasOne(d => d.ShippingOrders).WithMany(p => p.ShippingOrderDetails)
                .HasForeignKey(d => d.ShippingOrdersId)
                .HasConstraintName("FK__ShippingO__Shipp__3A81B327");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC0744C17265");

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
                .HasConstraintName("FK__User__Role_Id__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
