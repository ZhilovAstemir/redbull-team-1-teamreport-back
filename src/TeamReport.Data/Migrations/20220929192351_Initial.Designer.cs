﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeamReport.Data.Persistence;

#nullable disable

namespace redbull_team_1_teamreport_back.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220929192351_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Leadership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("LeaderId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaderId");

                    b.HasIndex("MemberId");

                    b.ToTable("Leaderships");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Else")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("High")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Low")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("Morale")
                        .HasColumnType("int");

                    b.Property<string>("MoraleComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stress")
                        .HasColumnType("int");

                    b.Property<string>("StressComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WeekId")
                        .HasColumnType("int");

                    b.Property<int>("Workload")
                        .HasColumnType("int");

                    b.Property<string>("WorkloadComment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("WeekId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Week", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Weeks");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Leadership", b =>
                {
                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Member", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderId");

                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");

                    b.Navigation("Leader");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Member", b =>
                {
                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Company", "Company")
                        .WithMany("Member")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Report", b =>
                {
                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId");

                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Week", "Week")
                        .WithMany()
                        .HasForeignKey("WeekId");

                    b.Navigation("Member");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Company", b =>
                {
                    b.Navigation("Member");
                });
#pragma warning restore 612, 618
        }
    }
}
