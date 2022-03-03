using Microsoft.EntityFrameworkCore;
using PickPoint.DataBase.Order.Models;
using System;

namespace PickPoint.DataBase.Order
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options)
        {
        }

        /// <summary>
        /// Заказы
        /// </summary>
        public DbSet<Models.Order> Orders { get; set; }
        /// <summary>
        /// Товары в заказе
        /// </summary>
        public DbSet<OrderArticle> OrderArticles { get; set; }
        /// <summary>
        /// Постоматы
        /// </summary>
        public DbSet<ParcelLocker> ParcelLockers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("OrderNumbers");

            modelBuilder.Entity<Models.Order>()
                        .Property(o => o.Number)
                        .HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");

            modelBuilder.Entity<Models.Order>()
                        .Property(p => p.Price)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ParcelLocker>().HasData(
                            new ParcelLocker { Id = Guid.NewGuid(), Number = "1234-123", Address = "Test addres 1", IsActive = true },
                            new ParcelLocker { Id = Guid.NewGuid(), Number = "1234-124", Address = "Test addres 2", IsActive = true },
                            new ParcelLocker { Id = Guid.NewGuid(), Number = "1234-125", Address = "Test addres 3" }
                           );
        }

    }
}
