﻿// <auto-generated />
using System;
using AssignmentOauth2Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AssignmentOauth2Server.Migrations
{
    [DbContext(typeof(AssignmentOauth2ServerContext))]
    [Migration("20190103070832_InitDatabaseV1")]
    partial class InitDatabaseV1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AssignmentOauth2Server.Controllers.Login", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("DeletedAt");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Salt");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.AccountInfomation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<long>("OwnerId");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId")
                        .IsUnique();

                    b.ToTable("AccountInfomation");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("IntendTime");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.Classes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClassId");

                    b.Property<long>("OwnerId");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.Credential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessToken");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("ExpiredAt");

                    b.Property<long>("OwnerId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Credential");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndTime");

                    b.Property<DateTime>("IntendTime");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartTime");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Subject");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.AccountInfomation", b =>
                {
                    b.HasOne("AssignmentOauth2Server.Models.Account", "Account")
                        .WithOne("AccountInfomation")
                        .HasForeignKey("AssignmentOauth2Server.Models.AccountInfomation", "OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
