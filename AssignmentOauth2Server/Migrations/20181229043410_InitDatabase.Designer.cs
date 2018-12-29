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
    [Migration("20181229043410_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                    b.Property<long>("OwnerId");

                    b.Property<DateTime>("BirthDay");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("OwnerId");

                    b.ToTable("AccountInfomation");
                });

            modelBuilder.Entity("AssignmentOauth2Server.Models.Credential", b =>
                {
                    b.Property<long>("OwnerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessToken");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("ExpiredAt");

                    b.Property<string>("RefreshToken");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("OwnerId");

                    b.ToTable("Credential");
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
