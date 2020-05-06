﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderDeliveryMonitor.DataAccessLibrary.Context;

namespace OrderDeliveryMonitor.DataAccessLibrary.Migrations
{
    [DbContext(typeof(OrderDeliveryMonitorDataContext))]
    [Migration("20200506214946_DataBase")]
    partial class DataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("OrderDeliveryMonitor.Model.Operation.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AwaitingEnd");

                    b.Property<DateTime?>("AwaitingStart");

                    b.Property<string>("EstacaoVendaCaixa");

                    b.Property<DateTime?>("Finished");

                    b.Property<string>("OrderNumber");

                    b.Property<DateTime?>("PreparingEnd");

                    b.Property<DateTime?>("PreparingStart");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OrderDeliveryMonitor.Model.Operation.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<string>("Product");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("OrderDeliveryMonitor.Model.Operation.OrderItem", b =>
                {
                    b.HasOne("OrderDeliveryMonitor.Model.Operation.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
