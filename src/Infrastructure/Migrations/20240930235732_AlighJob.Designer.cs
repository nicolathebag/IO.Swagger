﻿// <auto-generated />
using System;
using Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240930235732_AlighJob")]
    partial class AlighJob
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Models.Defect", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"), 1L, 1);

                    b.Property<double?>("Delta")
                        .HasColumnType("float");

                    b.Property<long?>("Length")
                        .HasColumnType("bigint");

                    b.Property<string>("Level")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Mm")
                        .HasColumnType("bigint");

                    b.Property<int?>("Param")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Defects");
                });

            modelBuilder.Entity("Domain.Models.Job", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Domain.Models.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double?>("P1")
                        .HasColumnType("float");

                    b.Property<double?>("P2")
                        .HasColumnType("float");

                    b.Property<double?>("P3")
                        .HasColumnType("float");

                    b.Property<double?>("P4")
                        .HasColumnType("float");

                    b.Property<long?>("mm")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Measures");
                });
#pragma warning restore 612, 618
        }
    }
}
