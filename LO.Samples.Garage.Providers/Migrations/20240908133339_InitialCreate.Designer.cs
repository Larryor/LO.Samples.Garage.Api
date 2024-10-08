﻿// <auto-generated />
using System;
using LO.Samples.Garage.Providers.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LO.Samples.Garage.Providers.Migrations
{
    [DbContext(typeof(GarageDbContext))]
    [Migration("20240908133339_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LO.Samples.Garage.Providers.Tables.BranchTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Branches");

                    b.HasData(
                        new
                        {
                            Id = new Guid("110a9e97-9477-41d1-b4aa-98c2b999e34c"),
                            CreatedAtUTC = new DateTime(2024, 9, 7, 15, 9, 8, 0, DateTimeKind.Unspecified),
                            LastModifiedAtUTC = new DateTime(2024, 9, 7, 15, 9, 8, 0, DateTimeKind.Unspecified),
                            Name = "DefaultBranch1"
                        });
                });

            modelBuilder.Entity("LO.Samples.Garage.Providers.Tables.CustomerTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastModifiedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LO.Samples.Garage.Providers.Tables.VehicleTable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastModifiedAtUTC")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("LO.Samples.Garage.Providers.Tables.VehicleTable", b =>
                {
                    b.HasOne("LO.Samples.Garage.Providers.Tables.BranchTable", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LO.Samples.Garage.Providers.Tables.CustomerTable", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Branch");

                    b.Navigation("Customer");
                });
#pragma warning restore 612, 618
        }
    }
}
