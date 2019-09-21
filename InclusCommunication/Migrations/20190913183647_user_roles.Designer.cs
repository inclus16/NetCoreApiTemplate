﻿// <auto-generated />
using System;
using InclusCommunication.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InclusCommunication.Migrations
{
    [DbContext(typeof(Postgres))]
    [Migration("20190913183647_user_roles")]
    partial class user_roles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("InclusCommunication.Entities.Credentials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("SERIAL");

                    b.Property<string>("Login")
                        .HasColumnName("login")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("VARCHAR(256)");

                    b.Property<int>("UserId")
                        .HasColumnName("user_id")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("credentials");
                });

            modelBuilder.Entity("InclusCommunication.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("SERIAL");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("RoleId")
                        .HasColumnName("role_id")
                        .HasColumnType("INT");

                    b.Property<int>("StatusId")
                        .HasColumnName("status_id")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StatusId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("InclusCommunication.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("SERIAL");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR(20)");

                    b.HasKey("Id");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("InclusCommunication.Entities.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("SERIAL");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("InclusCommunication.Entities.Credentials", b =>
                {
                    b.HasOne("InclusCommunication.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InclusCommunication.Entities.User", b =>
                {
                    b.HasOne("InclusCommunication.Entities.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InclusCommunication.Entities.UserStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}