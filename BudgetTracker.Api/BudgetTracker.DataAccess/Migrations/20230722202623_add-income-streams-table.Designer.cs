﻿// <auto-generated />
using System;
using BudgetTracker.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BudgetTracker.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230722202623_add-income-streams-table")]
    partial class addincomestreamstable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BudgetTracker.DataAccess.Models.Account", b =>
                {
                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnOrder(2);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProfilePictureUrl")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"));

                    b.HasKey("Email");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BudgetTracker.DataAccess.Models.IncomeStream", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"));

                    b.Property<int>("AccountUid")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Uid");

                    b.ToTable("IncomeStreams");
                });

            modelBuilder.Entity("BudgetTracker.DataAccess.Models.Transaction", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"));

                    b.Property<int>("AccountUid")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BalanceAfterPurchase")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("BalanceBeforePurchase")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PlaceOfPurchase")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("TimeOfPurchase")
                        .HasColumnType("datetime2");

                    b.HasKey("Uid");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
