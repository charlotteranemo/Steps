﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Steps.Data;

namespace Steps.Migrations
{
    [DbContext(typeof(StepsContext))]
    [Migration("20210221140950_NewString2")]
    partial class NewString2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Steps.Models.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailStr")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmailId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("Steps.Models.Fitspo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Blurb")
                        .IsRequired()
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<DateTime>("DateOfPost")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Post")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Fitspos");
                });

            modelBuilder.Entity("Steps.Models.Login", b =>
                {
                    b.Property<int>("LoginId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LoginId");

                    b.ToTable("Login");
                });
#pragma warning restore 612, 618
        }
    }
}
