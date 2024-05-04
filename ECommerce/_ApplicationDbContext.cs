using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Promotion> Promotions { get; set; } // Nouvelle DbSet pour la table Promotion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18, 2)"); // Spécifie le type de colonne pour la propriété Amount

            modelBuilder.Entity<Promotion>()
        .Property(p => p.MontantReduction)
        .HasColumnType("decimal(18, 2)"); // 

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18, 2)"); // Spécifie le type de colonne pour la propriété Total

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // Spécifie le type de colonne pour la propriété Price

            modelBuilder.Entity<OrderLine>()
                .Property(ol => ol.Price)
                .HasColumnType("decimal(18, 2)"); // Spécifie le type de colonne pour la propriété Price dans OrderLine

            modelBuilder.Entity<ProductInPromotion>()
                .Property(pip => pip.ReductionAmount)
                .HasColumnType("decimal(18, 2)"); // Spécifie le type de colonne pour la propriété ReductionAmount
        }
    }
}
