﻿// <auto-generated />
using System;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20240729093531_InitDB")]
    partial class InitDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Data.Bill", b =>
                {
                    b.Property<int>("id_order")
                        .HasColumnType("int");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("payment_method")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_order");

                    b.ToTable("Bills", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Category", b =>
                {
                    b.Property<int>("id_category")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_category"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id_category");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Comment", b =>
                {
                    b.Property<int>("id_comment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_comment"));

                    b.Property<DateTime>("created_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("id_product")
                        .HasColumnType("int");

                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("id_comment");

                    b.HasIndex("id_product");

                    b.HasIndex("id_user");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Order", b =>
                {
                    b.Property<int>("id_order")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_order"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("id_staff")
                        .HasColumnType("int");

                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("order_time")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_order");

                    b.HasIndex("id_staff");

                    b.HasIndex("id_user");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.OrderDetail", b =>
                {
                    b.Property<int>("id_order")
                        .HasColumnType("int");

                    b.Property<int>("id_product")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("total_price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id_order", "id_product");

                    b.HasIndex("id_product");

                    b.ToTable("OrderDetails", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Product", b =>
                {
                    b.Property<int>("id_product")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_product"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("id_category")
                        .HasColumnType("int");

                    b.Property<int>("id_supplier")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("quantity_in_stock")
                        .HasColumnType("int");

                    b.Property<string>("url_image")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("id_product");

                    b.HasIndex("id_category");

                    b.HasIndex("id_supplier");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.ProductVoucher", b =>
                {
                    b.Property<int>("id_product")
                        .HasColumnType("int");

                    b.Property<int>("id_voucher")
                        .HasColumnType("int");

                    b.HasKey("id_product", "id_voucher");

                    b.HasIndex("id_voucher");

                    b.ToTable("ProductVouchers", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Role", b =>
                {
                    b.Property<int>("id_role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_role"));

                    b.Property<string>("role_name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_role");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Staff", b =>
                {
                    b.Property<int>("id_staff")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_staff"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("name_staff")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("position")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("sex")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("type_staff")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_staff");

                    b.ToTable("Staffs", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.StaffRole", b =>
                {
                    b.Property<int>("id_staff")
                        .HasColumnType("int");

                    b.Property<int>("id_role")
                        .HasColumnType("int");

                    b.HasKey("id_staff", "id_role");

                    b.HasIndex("id_role");

                    b.ToTable("StaffRoles", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Supplier", b =>
                {
                    b.Property<int>("id_supplier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_supplier"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("name_company")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id_supplier");

                    b.ToTable("Suppliers", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.User", b =>
                {
                    b.Property<int>("id_user")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_user"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id_user");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Voucher", b =>
                {
                    b.Property<int>("id_voucher")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_voucher"));

                    b.Property<DateTime>("finish_date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("percent_discount")
                        .HasColumnType("decimal(5,2)");

                    b.Property<DateTime>("start_date")
                        .HasColumnType("datetime2");

                    b.HasKey("id_voucher");

                    b.ToTable("Vouchers", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Voucher_User", b =>
                {
                    b.Property<int>("id_voucher")
                        .HasColumnType("int");

                    b.Property<int>("id_user")
                        .HasColumnType("int");

                    b.HasKey("id_voucher", "id_user");

                    b.HasIndex("id_user");

                    b.ToTable("Voucher_Users", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Bill", b =>
                {
                    b.HasOne("Ecommerce.Data.Order", "Order")
                        .WithOne("Bill")
                        .HasForeignKey("Ecommerce.Data.Bill", "id_order")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Ecommerce.Data.Comment", b =>
                {
                    b.HasOne("Ecommerce.Data.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("id_product")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ecommerce.Data.Order", b =>
                {
                    b.HasOne("Ecommerce.Data.Staff", "Staff")
                        .WithMany("Orders")
                        .HasForeignKey("id_staff")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Staff");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ecommerce.Data.OrderDetail", b =>
                {
                    b.HasOne("Ecommerce.Data.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("id_order")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("id_product")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Data.Product", b =>
                {
                    b.HasOne("Ecommerce.Data.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("id_category")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("id_supplier")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Ecommerce.Data.ProductVoucher", b =>
                {
                    b.HasOne("Ecommerce.Data.Product", "Product")
                        .WithMany("ProductVouchers")
                        .HasForeignKey("id_product")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Voucher", "Voucher")
                        .WithMany("ProductVouchers")
                        .HasForeignKey("id_voucher")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Ecommerce.Data.StaffRole", b =>
                {
                    b.HasOne("Ecommerce.Data.Role", "Role")
                        .WithMany("StaffRoles")
                        .HasForeignKey("id_role")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Staff", "Staff")
                        .WithMany("StaffRoles")
                        .HasForeignKey("id_staff")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Ecommerce.Data.Voucher_User", b =>
                {
                    b.HasOne("Ecommerce.Data.User", "User")
                        .WithMany("Voucher_Users")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Voucher", "Voucher")
                        .WithMany("Voucher_Users")
                        .HasForeignKey("id_voucher")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Ecommerce.Data.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce.Data.Order", b =>
                {
                    b.Navigation("Bill")
                        .IsRequired();

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Ecommerce.Data.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("OrderDetails");

                    b.Navigation("ProductVouchers");
                });

            modelBuilder.Entity("Ecommerce.Data.Role", b =>
                {
                    b.Navigation("StaffRoles");
                });

            modelBuilder.Entity("Ecommerce.Data.Staff", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("StaffRoles");
                });

            modelBuilder.Entity("Ecommerce.Data.Supplier", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce.Data.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");

                    b.Navigation("Voucher_Users");
                });

            modelBuilder.Entity("Ecommerce.Data.Voucher", b =>
                {
                    b.Navigation("ProductVouchers");

                    b.Navigation("Voucher_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
