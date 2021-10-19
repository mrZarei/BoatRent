﻿// <auto-generated />
using System;
using BoatRent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BoatRent.Data.Migrations
{
    [DbContext(typeof(RentDbContext))]
    [Migration("20211019085634_InitialDB")]
    partial class InitialDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BoatRent.Data.Models.Boat", b =>
                {
                    b.Property<string>("BoatNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BoatType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BoatNumber");

                    b.ToTable("Boats");
                });

            modelBuilder.Entity("BoatRent.Data.Models.RentBoat", b =>
                {
                    b.Property<string>("BookingNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BoatNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsReturned")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BookingNumber");

                    b.HasIndex("BoatNumber");

                    b.ToTable("RentBoat");
                });

            modelBuilder.Entity("BoatRent.Data.Models.RentBoat", b =>
                {
                    b.HasOne("BoatRent.Data.Models.Boat", "Boat")
                        .WithMany()
                        .HasForeignKey("BoatNumber");

                    b.Navigation("Boat");
                });
#pragma warning restore 612, 618
        }
    }
}
