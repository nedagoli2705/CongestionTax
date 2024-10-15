﻿// <auto-generated />
using System;
using CongestionTax.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CongestionTax.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241014220619_SeedVehicleTypes")]
    partial class SeedVehicleTypes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CongestionTax.Domain.Entities.TollPass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("TollPasses");
                });

            modelBuilder.Entity("CongestionTax.Domain.Entities.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsExempt")
                        .HasColumnType("bit");

                    b.Property<string>("VehicleType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsExempt = false,
                            VehicleType = "Car"
                        },
                        new
                        {
                            Id = 2,
                            IsExempt = true,
                            VehicleType = "Motorbike"
                        },
                        new
                        {
                            Id = 3,
                            IsExempt = true,
                            VehicleType = "Bus"
                        },
                        new
                        {
                            Id = 4,
                            IsExempt = true,
                            VehicleType = "Emergency"
                        },
                        new
                        {
                            Id = 5,
                            IsExempt = true,
                            VehicleType = "Diplomat"
                        },
                        new
                        {
                            Id = 6,
                            IsExempt = true,
                            VehicleType = "Military"
                        },
                        new
                        {
                            Id = 7,
                            IsExempt = true,
                            VehicleType = "Foreign"
                        });
                });

            modelBuilder.Entity("CongestionTax.Domain.Entities.TollPass", b =>
                {
                    b.HasOne("CongestionTax.Domain.Entities.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });
#pragma warning restore 612, 618
        }
    }
}
