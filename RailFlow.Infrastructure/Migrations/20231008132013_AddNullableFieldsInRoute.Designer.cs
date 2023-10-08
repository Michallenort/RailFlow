﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RailFlow.Infrastructure.DAL;

#nullable disable

namespace RailFlow.Infrastructure.Migrations
{
    [DbContext(typeof(TrainDbContext))]
    [Migration("20231008132013_AddNullableFieldsInRoute")]
    partial class AddNullableFieldsInRoute
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Railflow.Core.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Route", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("EndStationId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<Guid>("StartStationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TrainId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EndStationId");

                    b.HasIndex("StartStationId");

                    b.HasIndex("TrainId")
                        .IsUnique();

                    b.ToTable("Routes");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Station", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Stop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("ArrivalHour")
                        .HasColumnType("time without time zone");

                    b.Property<TimeOnly>("DepartureHour")
                        .HasColumnType("time without time zone");

                    b.Property<Guid>("RouteId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StationId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.HasIndex("StationId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Train", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Capacity")
                        .HasColumnType("integer");

                    b.Property<float?>("MaxSpeed")
                        .HasColumnType("real");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Trains");
                });

            modelBuilder.Entity("Railflow.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Route", b =>
                {
                    b.HasOne("Railflow.Core.Entities.Station", "EndStation")
                        .WithMany()
                        .HasForeignKey("EndStationId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Railflow.Core.Entities.Station", "StartStation")
                        .WithMany()
                        .HasForeignKey("StartStationId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.HasOne("Railflow.Core.Entities.Train", "Train")
                        .WithOne("AssignedRoute")
                        .HasForeignKey("Railflow.Core.Entities.Route", "TrainId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .IsRequired();

                    b.Navigation("EndStation");

                    b.Navigation("StartStation");

                    b.Navigation("Train");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Station", b =>
                {
                    b.OwnsOne("Railflow.Core.ValueObjects.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(25)
                                .HasColumnType("character varying(25)")
                                .HasColumnName("City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(25)
                                .HasColumnType("character varying(25)")
                                .HasColumnName("Country");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Street");

                            b1.HasKey("Id");

                            b1.ToTable("Addresses", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("Id");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Railflow.Core.Entities.Stop", b =>
                {
                    b.HasOne("Railflow.Core.Entities.Route", "Route")
                        .WithMany("Stops")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Railflow.Core.Entities.Station", "Station")
                        .WithMany("Stops")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Route");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("Railflow.Core.Entities.User", b =>
                {
                    b.HasOne("Railflow.Core.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Route", b =>
                {
                    b.Navigation("Stops");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Station", b =>
                {
                    b.Navigation("Stops");
                });

            modelBuilder.Entity("Railflow.Core.Entities.Train", b =>
                {
                    b.Navigation("AssignedRoute");
                });
#pragma warning restore 612, 618
        }
    }
}