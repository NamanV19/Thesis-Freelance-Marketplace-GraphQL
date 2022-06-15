using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderService.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Payment>().HasKey(k => new { k.Id });
            modelBuilder.Entity<Review>().HasKey(k => new { k.Id });

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Payment)
                .WithOne(payment => payment.Order)
                .HasForeignKey<Payment>(payment => payment.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(order => order.Review)
                .WithOne(review => review.Order)
                .HasForeignKey<Review>(review => review.OrderId);

            modelBuilder.Entity<Payment>()
                .HasOne(payment => payment.Order)
                .WithOne(order => order.Payment);

            modelBuilder.Entity<Review>()
                .HasOne(review => review.Order)
                .WithOne(order => order.Review);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
