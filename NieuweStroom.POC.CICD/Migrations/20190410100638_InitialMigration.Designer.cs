﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NieuweStroom.POC.CICD.Context;

namespace NieuweStroom.POC.CICD.Migrations
{
    [DbContext(typeof(NieuweStroomContext))]
    [Migration("20190410100638_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NieuweStroom.POC.CICD.Models.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("AmountExcVat");

                    b.Property<double>("AmountIncVat");

                    b.Property<double>("AmountVat");

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<Guid?>("DocumentId");

                    b.Property<DateTimeOffset>("IntervalEnd");

                    b.Property<DateTimeOffset>("IntervalStart");

                    b.Property<DateTimeOffset?>("InvoiceDate");

                    b.Property<int>("InvoiceNumber");

                    b.Property<int>("InvoiceTypeId");

                    b.Property<Guid?>("MeterId");

                    b.Property<int>("PublicUtilityId");

                    b.HasKey("Id");

                    b.ToTable("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
