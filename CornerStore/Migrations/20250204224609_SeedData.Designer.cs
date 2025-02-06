﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CornerStore.Migrations
{
    [DbContext(typeof(CornerStoreDbContext))]
    [Migration("20250204224609_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cashiers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Sarah",
                            LastName = "Johnson"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Michael",
                            LastName = "Brown"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Beverages"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Snacks"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Dairy"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Produce"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CashierId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("PaidOnDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CashierId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CashierId = 1,
                            PaidOnDate = new DateTime(2024, 2, 4, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CashierId = 2,
                            PaidOnDate = new DateTime(2024, 2, 4, 11, 45, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            CashierId = 1,
                            PaidOnDate = new DateTime(2024, 2, 4, 14, 15, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            OrderId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            ProductId = 2,
                            OrderId = 1,
                            Quantity = 1
                        },
                        new
                        {
                            ProductId = 3,
                            OrderId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            ProductId = 4,
                            OrderId = 2,
                            Quantity = 3
                        },
                        new
                        {
                            ProductId = 5,
                            OrderId = 3,
                            Quantity = 2
                        },
                        new
                        {
                            ProductId = 6,
                            OrderId = 3,
                            Quantity = 4
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "RefreshCo",
                            CategoryId = 1,
                            Price = 2.49m,
                            ProductName = "Cola"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "CrunchCo",
                            CategoryId = 2,
                            Price = 3.99m,
                            ProductName = "Potato Chips"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "FreshFarms",
                            CategoryId = 3,
                            Price = 4.99m,
                            ProductName = "Milk"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Nature's Best",
                            CategoryId = 4,
                            Price = 0.99m,
                            ProductName = "Bananas"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "PowerUp",
                            CategoryId = 1,
                            Price = 3.49m,
                            ProductName = "Energy Drink"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "SweetTreats",
                            CategoryId = 2,
                            Price = 1.99m,
                            ProductName = "Chocolate Bar"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.HasOne("CornerStore.Models.Cashier", "Cashier")
                        .WithMany()
                        .HasForeignKey("CashierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cashier");
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.HasOne("CornerStore.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CornerStore.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.HasOne("CornerStore.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
