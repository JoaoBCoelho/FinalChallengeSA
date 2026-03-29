using FinalChallengeSA.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinalChallengeSA.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureCustomersTable(modelBuilder);
            ConfigureProductsTable(modelBuilder);
            ConfigureOrdersTable(modelBuilder);
        }

        private static void ConfigureOrdersTable(ModelBuilder modelBuilder)
        {
            var order = modelBuilder.Entity<Order>();
            order.ToTable("Orders");
            order.HasKey(o => o.Id);
            order.Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");

            order.HasOne(o => o.Customer)
                 .WithMany()
                 .HasForeignKey(o => o.CustomerId)
                 .OnDelete(DeleteBehavior.Restrict);

            order.HasMany(o => o.Products)
                 .WithMany()
                 .UsingEntity(j =>
                 {
                     j.ToTable("OrderProducts");
                     j.Property<Guid>("OrderId");
                     j.Property<Guid>("ProductId");
                     j.HasKey("OrderId", "ProductId");
                 });
        }

        private static void ConfigureProductsTable(ModelBuilder modelBuilder)
        {
            var product = modelBuilder.Entity<Product>();
            product.ToTable("Products");
            product.HasKey(p => p.Id);
            product.Property(p => p.Name).IsRequired().HasMaxLength(200);
            product.Property(p => p.Description).HasMaxLength(500);
            product.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }

        private static void ConfigureCustomersTable(ModelBuilder modelBuilder)
        {
            var customer = modelBuilder.Entity<Customer>();
            customer.ToTable("Customers");
            customer.HasKey(c => c.Id);
            customer.Property(c => c.Name).IsRequired().HasMaxLength(200);
            customer.Property(c => c.Email).IsRequired().HasMaxLength(200);
        }
    }
}
