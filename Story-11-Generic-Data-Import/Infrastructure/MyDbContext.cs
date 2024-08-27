using Microsoft.EntityFrameworkCore;
using DataImport.Domain.Entities;

namespace DataImport.Infrastructure;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Default");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");
            entity.HasKey(e => e.Id);

            entity.HasMany(e => e.PurchaseLists)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .HasPrincipalKey(e => e.Id);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");
            entity.HasKey(e => e.Id);

            entity.HasMany(e => e.PurchaseLists)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.ProductId)
                .HasPrincipalKey(e => e.Id);
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.ToTable("Purchase");
            entity.HasKey(e => e.Id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
