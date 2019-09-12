﻿// <auto-generated />
using System;
using FileSystem.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FileSystem.Migrations
{
    [DbContext(typeof(Postgres))]
    partial class PostgresModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FileSystem.Entities.Credentials", b =>
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

            modelBuilder.Entity("FileSystem.Entities.User", b =>
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

                    b.Property<int>("StatusId")
                        .HasColumnName("status_id")
                        .HasColumnType("INT");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("FileSystem.Entities.UserStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("SERIAL");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("VARCHAR(30)");

                    b.HasKey("Id");

                    b.ToTable("user_statuses");
                });

            modelBuilder.Entity("FileSystem.Entities.Credentials", b =>
                {
                    b.HasOne("FileSystem.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FileSystem.Entities.User", b =>
                {
                    b.HasOne("FileSystem.Entities.UserStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
