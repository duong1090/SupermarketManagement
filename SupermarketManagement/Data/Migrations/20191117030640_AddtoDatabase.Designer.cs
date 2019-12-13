﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SupermarketManagement.Data;

namespace SupermarketManagement.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191117030640_AddtoDatabase")]
    partial class AddtoDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SupermarketManagement.Models.Bill", b =>
                {
                    b.Property<int>("BillID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerID");

                    b.Property<DateTime>("Date");

                    b.Property<int>("StaffID");

                    b.Property<float>("TotalMoney");

                    b.HasKey("BillID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("StaffID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Bill_Detail", b =>
                {
                    b.Property<int>("Bill_DetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BillID");

                    b.Property<int>("ProductAmount");

                    b.Property<int>("ProductID");

                    b.Property<float>("TotalPrie");

                    b.HasKey("Bill_DetailID");

                    b.HasIndex("BillID");

                    b.ToTable("Bill_Details");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired();

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Point");

                    b.Property<int>("Rate");

                    b.Property<string>("Sex");

                    b.Property<bool>("Status");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SupermarketManagement.Models.ImportReceipt", b =>
                {
                    b.Property<int>("ImportReceiptID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("StaffID");

                    b.Property<int>("Total");

                    b.HasKey("ImportReceiptID");

                    b.HasIndex("StaffID");

                    b.ToTable("ImportReceipts");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Operating", b =>
                {
                    b.Property<int>("OperatingID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Detail");

                    b.HasKey("OperatingID");

                    b.ToTable("Operatings");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<int?>("Bill_DetailID");

                    b.Property<int>("CategoryID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<float>("Price");

                    b.Property<string>("Producer");

                    b.Property<int?>("Receipt_DetailID");

                    b.Property<bool>("Status");

                    b.HasKey("ProductID");

                    b.HasIndex("Bill_DetailID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("Receipt_DetailID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Receipt_Detail", b =>
                {
                    b.Property<int>("Receipt_DetailID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<int>("ImportReceiptID");

                    b.Property<DateTime>("ManufactureDate");

                    b.Property<int>("ProductID");

                    b.HasKey("Receipt_DetailID");

                    b.ToTable("Receipt_Details");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SupermarketManagement.Models.RoleAllow", b =>
                {
                    b.Property<int>("RoleID");

                    b.Property<int>("OperatingID");

                    b.Property<bool>("Alow");

                    b.HasKey("RoleID", "OperatingID");

                    b.HasAlternateKey("OperatingID", "RoleID");

                    b.ToTable("RoleAllows");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Staff", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("RoleID");

                    b.Property<string>("Sex");

                    b.Property<bool>("Status");

                    b.HasKey("StaffID");

                    b.HasIndex("RoleID");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("SupermarketManagement.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PassWord");

                    b.Property<int>("StaffID");

                    b.HasKey("ID");

                    b.HasIndex("StaffID")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SupermarketManagement.Models.Bill", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Customer", "Customer")
                        .WithMany("Bills")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupermarketManagement.Models.Staff", "Staff")
                        .WithMany("Bills")
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupermarketManagement.Models.Bill_Detail", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Bill", "Bill")
                        .WithMany("Bill_Details")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupermarketManagement.Models.ImportReceipt", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Staff", "Staff")
                        .WithMany("ImportReceipts")
                        .HasForeignKey("StaffID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupermarketManagement.Models.Product", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Bill_Detail", "Bill_Detail")
                        .WithMany("Products")
                        .HasForeignKey("Bill_DetailID");

                    b.HasOne("SupermarketManagement.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupermarketManagement.Models.Receipt_Detail", "Receipt_Detail")
                        .WithMany("Products")
                        .HasForeignKey("Receipt_DetailID");
                });

            modelBuilder.Entity("SupermarketManagement.Models.RoleAllow", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Operating", "Operating")
                        .WithMany("RoleAllows")
                        .HasForeignKey("OperatingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SupermarketManagement.Models.Role", "Role")
                        .WithMany("RoleAllows")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupermarketManagement.Models.Staff", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Role", "Role")
                        .WithMany("Staffs")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SupermarketManagement.Models.User", b =>
                {
                    b.HasOne("SupermarketManagement.Models.Staff", "Staff")
                        .WithOne("Users")
                        .HasForeignKey("SupermarketManagement.Models.User", "StaffID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
