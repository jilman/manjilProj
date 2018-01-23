﻿// <auto-generated />
using Entity.Connection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Entity.Migrations
{
    [DbContext(typeof(ConnectionDB))]
    partial class ConnectionModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entity.Model.PortfolioAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("EntryDate");

                    b.Property<int>("EntryUserID");

                    b.HasKey("Id");

                    b.ToTable("PortfolioAccount");
                });

            modelBuilder.Entity("Entity.Model.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EntryDate");

                    b.Property<int>("EntryUserID");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Entity.Model.StockReceiveDetl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MastId");

                    b.Property<DateTime>("OwnershipDate");

                    b.Property<int>("Qty");

                    b.Property<decimal>("Rate");

                    b.Property<int>("StockId");

                    b.HasKey("Id");

                    b.HasIndex("MastId");

                    b.HasIndex("StockId");

                    b.ToTable("StockReceiveDetl");
                });

            modelBuilder.Entity("Entity.Model.StockReceiveMast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EntryDate");

                    b.Property<int>("EntryUserID");

                    b.Property<int>("PortfolioAcId");

                    b.Property<string>("Remarks")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("ValueDate");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioAcId");

                    b.ToTable("StockReceiveMast");
                });

            modelBuilder.Entity("Entity.Model.StockReceiveDetl", b =>
                {
                    b.HasOne("Entity.Model.StockReceiveMast", "StockRecieveMasts")
                        .WithMany("StockReceiveDetls")
                        .HasForeignKey("MastId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Entity.Model.Stock", "Stocks")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Entity.Model.StockReceiveMast", b =>
                {
                    b.HasOne("Entity.Model.PortfolioAccount", "PortfolioAccounts")
                        .WithMany()
                        .HasForeignKey("PortfolioAcId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
