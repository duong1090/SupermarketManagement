using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SupermarketManagement.Models;

namespace SupermarketManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleAllow>().HasKey(x => new { x.RoleID, x.OperatingID });
        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Bill_Detail> Bill_Details { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ImportReceipt> ImportReceipts { get; set; }
        public DbSet<Operating> Operatings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt_Detail> Receipt_Details { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleAllow> RoleAllows { get; set; }
        public DbSet<User> Users { get; set; }
       

    }
}
