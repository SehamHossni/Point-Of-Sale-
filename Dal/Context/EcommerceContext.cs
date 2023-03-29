using Dal.Entities;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Context
{
   public class EcommerceContext:IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public EcommerceContext(DbContextOptions<EcommerceContext> option) : base(option)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<OrderProduct>().HasKey(p => new {p.OrderId,p.ProductId});
            base.OnModelCreating(modelBuilder);
        }
    }
}
