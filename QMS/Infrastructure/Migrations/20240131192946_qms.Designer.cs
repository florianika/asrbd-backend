﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240131192946_qms")]
    partial class qms
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.ProcessOutputLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BldId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DwlId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("EntId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorLevel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualityAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualityMessageAl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualityMessageEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RuleId")
                        .HasColumnType("bigint");

                    b.Property<string>("Variable")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RuleId");

                    b.ToTable("ProcessOutputLogs");
                });

            modelBuilder.Entity("Domain.Rule", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CreatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DescriptionAl")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("DescriptionEn")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Expression")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocalId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NameAl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NameEn")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("QualityAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QualityMessageAl")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("QualityMessageEn")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Remark")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("RuleRequirement")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("RuleStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("UpdatedUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Variable")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("VersionRationale")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("LocalId")
                        .IsUnique();

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("Domain.ProcessOutputLog", b =>
                {
                    b.HasOne("Domain.Rule", "Rule")
                        .WithMany()
                        .HasForeignKey("RuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rule");
                });
#pragma warning restore 612, 618
        }
    }
}
