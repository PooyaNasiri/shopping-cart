using Application.Interfaces.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Persistence.Context
{
    public class DataBaseContext:DbContext,IDataBaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemove);
            modelBuilder.Entity<Cart>().HasQueryFilter(p=>!p.IsRemove);
            modelBuilder.Entity<Cart>().HasMany(p => p.Items).WithOne(p => p.Cart).HasForeignKey(p => p.CartId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>().HasData(new List<Product>()
            {
                new Product(){Id=1,Name="A",Price=50,DiscountPrice=130,DiscountQuorum=3,InsertTime=DateTime.Now,IsRemove=false},
                new Product(){Id=2,Name="B",Price=30,DiscountPrice=45,DiscountQuorum=2,InsertTime=DateTime.Now,IsRemove=false},
                new Product(){Id=3,Name="C",Price=20,DiscountPrice=20,DiscountQuorum=1,InsertTime=DateTime.Now,IsRemove=false},
                new Product(){Id=4,Name="D",Price=15,DiscountPrice=15,DiscountQuorum=1,InsertTime=DateTime.Now,IsRemove=false},
            });
        }
    }
}
