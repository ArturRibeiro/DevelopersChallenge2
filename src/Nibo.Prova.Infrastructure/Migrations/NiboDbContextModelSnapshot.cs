﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nibo.Prova.Infrastructure;

namespace Nibo.Prova.Infrastructure.Migrations
{
    [DbContext(typeof(NiboDbContext))]
    partial class NiboDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nibo.Prova.Domain.Models.Transactions.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnName("Amount")
                        .HasMaxLength(90);

                    b.Property<DateTime>("DatePosted")
                        .HasColumnName("DatePosted");

                    b.Property<string>("Memo")
                        .IsRequired()
                        .HasColumnName("Memo")
                        .HasMaxLength(90);

                    b.Property<short>("TransactionType")
                        .HasColumnName("Type");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
