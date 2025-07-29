using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; }


        public DiscountContext(DbContextOptions<DiscountContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon() { Id = 1, ProductName = "Iphone 16 pro max", Description = "Iphone pro max made in china", Amount = 10, IsActive = true, Code = "XX1" },

                new Coupon() { Id = 2, ProductName = "samsung s24 Ultra", Description = "samsung made in china", Amount = 20, IsActive = true, Code = "XX2" }

                );
        }

    }
}
