﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReserveIt.Data;

namespace ReserveIt.Migrations
{
    [DbContext(typeof(ResContext))]
    [Migration("20211206152101_ReserveIt")]
    partial class ReserveIt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReserveIt.Models.ConferenceRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ResourceTimeZone")
                        .HasColumnType("int");

                    b.Property<int>("ResourceType")
                        .HasColumnType("int");

                    b.Property<int>("SeatingProvided")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ConferenceRooms");
                });

            modelBuilder.Entity("ReserveIt.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReservationResponseId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReservationResponseId");

                    b.HasIndex("ResourceId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("ReserveIt.Models.ReservationResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ResourceId");

                    b.ToTable("ReservationDtos");
                });

            modelBuilder.Entity("ReserveIt.Models.Reservation", b =>
                {
                    b.HasOne("ReserveIt.Models.ReservationResponse", null)
                        .WithMany("Reservations")
                        .HasForeignKey("ReservationResponseId");

                    b.HasOne("ReserveIt.Models.ConferenceRoom", "ConferenceRoom")
                        .WithMany("Reservations")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConferenceRoom");
                });

            modelBuilder.Entity("ReserveIt.Models.ReservationResponse", b =>
                {
                    b.HasOne("ReserveIt.Models.ConferenceRoom", null)
                        .WithMany("ReservationDtos")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ReserveIt.Models.ConferenceRoom", b =>
                {
                    b.Navigation("ReservationDtos");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("ReserveIt.Models.ReservationResponse", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
