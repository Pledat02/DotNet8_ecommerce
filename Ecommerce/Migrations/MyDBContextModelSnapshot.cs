﻿// <auto-generated />
using System;
using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.Data.Account", b =>
                {
                    b.Property<int>("id_account")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_account"));

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("id_account");

                    b.ToTable("Accounts", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.Bill", b =>
                {
                    b.Property<int>("id_order")
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("address")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("city")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("firstname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("lastname")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("payment_method")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("shipping")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

                    b.Property<int?>("Staffid_staff")
                        .HasColumnType("int");

                    b.Property<int?>("Userid_user")
                        .HasColumnType("int");

                    b.Property<int>("id_voucher_user")
                        .HasColumnType("int");

                    b.Property<DateTime>("order_time")
                        .HasColumnType("datetime2");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id_order");

                    b.HasIndex("Staffid_staff");

                    b.HasIndex("Userid_user");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Ecommerce.Data.OrderDetail", b =>
                {
                    b.Property<int>("id_order")
                        .HasColumnType("int");

                    b.Property<int>("id_product")
                        .HasColumnType("int");

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

                    b.Property<int>("average_rate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

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

                    b.Property<decimal>("percent_discount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(3,2)")
                        .HasDefaultValue(0m);

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

                    b.Property<int>("id_account")
                        .HasColumnType("int");

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

                    b.HasIndex("id_account")
                        .IsUnique();

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

                    b.HasIndex("name_company")
                        .IsUnique();

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fullname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("id_account")
                        .HasColumnType("int");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("id_user");

                    b.HasIndex("id_account")
                        .IsUnique();

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
                    b.Property<int>("id_voucher_User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_voucher_User"));

                    b.Property<int?>("id_order")
                        .HasColumnType("int");

                    b.Property<int?>("id_user")
                        .HasColumnType("int");

                    b.Property<int?>("id_voucher")
                        .HasColumnType("int");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.HasKey("id_voucher_User");

                    b.HasIndex("id_order")
                        .IsUnique()
                        .HasFilter("[id_order] IS NOT NULL");

                    b.HasIndex("id_user");

                    b.HasIndex("id_voucher");

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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Ecommerce.Data.Order", b =>
                {
                    b.HasOne("Ecommerce.Data.Staff", null)
                        .WithMany("Orders")
                        .HasForeignKey("Staffid_staff");

                    b.HasOne("Ecommerce.Data.User", null)
                        .WithMany("Orders")
                        .HasForeignKey("Userid_user");
                });

            modelBuilder.Entity("Ecommerce.Data.OrderDetail", b =>
                {
                    b.HasOne("Ecommerce.Data.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("id_order")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("id_product")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Data.Product", b =>
                {
                    b.HasOne("Ecommerce.Data.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("id_category")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("id_supplier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Ecommerce.Data.Staff", b =>
                {
                    b.HasOne("Ecommerce.Data.Account", "Account")
                        .WithOne("Staff")
                        .HasForeignKey("Ecommerce.Data.Staff", "id_account")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Ecommerce.Data.StaffRole", b =>
                {
                    b.HasOne("Ecommerce.Data.Role", "Role")
                        .WithMany("StaffRoles")
                        .HasForeignKey("id_role")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecommerce.Data.Staff", "Staff")
                        .WithMany("StaffRoles")
                        .HasForeignKey("id_staff")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Ecommerce.Data.User", b =>
                {
                    b.HasOne("Ecommerce.Data.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("Ecommerce.Data.User", "id_account")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Ecommerce.Data.Voucher_User", b =>
                {
                    b.HasOne("Ecommerce.Data.Order", "Order")
                        .WithOne("Voucher_User")
                        .HasForeignKey("Ecommerce.Data.Voucher_User", "id_order")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ecommerce.Data.User", "User")
                        .WithMany("Voucher_Users")
                        .HasForeignKey("id_user")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Ecommerce.Data.Voucher", "Voucher")
                        .WithMany("Voucher_Users")
                        .HasForeignKey("id_voucher")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Order");

                    b.Navigation("User");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("Ecommerce.Data.Account", b =>
                {
                    b.Navigation("Staff")
                        .IsRequired();

                    b.Navigation("User")
                        .IsRequired();
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

                    b.Navigation("Voucher_User")
                        .IsRequired();
                });

            modelBuilder.Entity("Ecommerce.Data.Product", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("OrderDetails");
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
                    b.Navigation("Voucher_Users");
                });
#pragma warning restore 612, 618
        }
    }
}
