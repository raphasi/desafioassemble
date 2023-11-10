using Microsoft.EntityFrameworkCore;
using ShopTFTEC.API.Models;

namespace ShopTFTEC.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product>? Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 1,
            CouponCode = "VSHOP_PROMO_10",
            Discount = 10
        });
        mb.Entity<Coupon>().HasData(new Coupon
        {
            CouponId = 2,
            CouponCode = "VSHOP_PROMO_20",
            Discount = 20
        });
        //category
        mb.Entity<Category>().HasKey(c => c.CategoryId);

        mb.Entity<Category>().
             Property(c => c.Name).
               HasMaxLength(100).
                    IsRequired();

        //Product
        mb.Entity<Product>().
           Property(c => c.Name).
             HasMaxLength(100).
               IsRequired();

        mb.Entity<Product>().
          Property(c => c.Description).
               HasMaxLength(255).
                   IsRequired();

        mb.Entity<Product>().
          Property(c => c.ImageURL).
              HasMaxLength(255).
                  IsRequired();

        mb.Entity<Product>().
           Property(c => c.Price).
             HasPrecision(12, 2);

        mb.Entity<Category>()
          .HasMany(g => g.Products)
            .WithOne(c => c.Category)
            .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Category>().HasData(
            new Category
            {
                CategoryId = 1,
                Name = "Material Escolar",
            },
            new Category
            {
                CategoryId = 2,
                Name = "Acessórios",
            }
        );

        //CartHeader
        mb.Entity<CartHeader>().
             Property(c => c.UserId).
             HasMaxLength(255).
                 IsRequired();

        mb.Entity<CartHeader>().
           Property(c => c.CouponCode).
              HasMaxLength(100);
    }
}
