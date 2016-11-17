using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models.Domain;

namespace SportsStore.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(MapProduct);
            modelBuilder.Entity<City>(MapCity);
            modelBuilder.Entity<Customer>(MapCustomer);
            modelBuilder.Ignore<Cart>();
            modelBuilder.Ignore<CartLine>();
            modelBuilder.Entity<Order>(MapOrder);
            modelBuilder.Entity<OrderLine>(MapOrderLine);
            modelBuilder.Entity<Category>(MapCategory);
        }

        private static void MapProduct(EntityTypeBuilder<Product> p)
        {
            p.ToTable("Product");
            p.HasKey(t => t.ProductId);
            p.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        private static void MapCity(EntityTypeBuilder<City> c)
        {
            c.ToTable("City");
            c.HasKey(t => t.Postalcode);
            c.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);
        }

        private void MapCustomer(EntityTypeBuilder<Customer> c)
        {
            c.ToTable("Customer");
            c.HasKey(t => t.CustomerId);
            c.Property(t => t.CustomerName).IsRequired().HasMaxLength(20);
            c.Property(t => t.Name).IsRequired().HasMaxLength(100);
            c.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
            c.Property(t => t.Street).IsRequired().HasMaxLength(100);

            c.HasOne(t => t.City)
                .WithMany()
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            c.HasMany(t => t.Orders)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }

        private static void MapOrderLine(EntityTypeBuilder<OrderLine> ol)
        {
            ol.ToTable("OrderLine");
            ol.HasKey(t => new { t.OrderId, t.ProductId });

            ol.HasOne(t => t.Product)
                .WithMany()
                .IsRequired()
                .HasForeignKey(t => t.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void MapOrder(EntityTypeBuilder<Order> o)
        {
            o.ToTable("Order");
            o.HasKey(t => t.OrderId);
            o.Property(t => t.ShippingStreet).IsRequired().HasMaxLength(100);

            o.HasOne(t => t.ShippingCity)
                .WithMany().IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            o.HasMany(t => t.OrderLines)
                .WithOne()
                .IsRequired()
                .HasForeignKey(t => t.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
  
        private static void MapCategory(EntityTypeBuilder<Category> c)
        {
            c.ToTable("Category");
            c.HasKey(t => t.CategoryId);
            c.Property(t => t.Name).IsRequired().HasMaxLength(100);

            c.HasMany(t => t.Products)
                .WithOne(t => t.Category)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

      


    }
}