﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using redbull_team_1_teamreport_back.Data.Persistence;

#nullable disable

namespace redbull_team_1_teamreport_back.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221002200529_initial3")]
    partial class initial3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LeadershipMember", b =>
                {
                    b.Property<int>("LeadershipsId")
                        .HasColumnType("int");

                    b.Property<int>("MembersId")
                        .HasColumnType("int");

                    b.HasKey("LeadershipsId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("LeadershipMember");
                });

            modelBuilder.Entity("LeadershipMember1", b =>
                {
                    b.Property<int>("LeadersId")
                        .HasColumnType("int");

                    b.Property<int>("MembershipsId")
                        .HasColumnType("int");

                    b.HasKey("LeadersId", "MembershipsId");

                    b.HasIndex("MembershipsId");

                    b.ToTable("LeadershipMember1");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Leadership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("Leadership", (string)null);
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

                    b.ToTable("Member", (string)null);
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

                    b.ToTable("Report", (string)null);
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

                    b.ToTable("Week", (string)null);
                });

            modelBuilder.Entity("LeadershipMember", b =>
                {
                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Leadership", null)
                        .WithMany()
                        .HasForeignKey("LeadershipsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LeadershipMember1", b =>
                {
                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Member", null)
                        .WithMany()
                        .HasForeignKey("LeadersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Leadership", null)
                        .WithMany()
                        .HasForeignKey("MembershipsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                        .WithMany("Reports")
                        .HasForeignKey("MemberId");

                    b.HasOne("redbull_team_1_teamreport_back.Domain.Entities.Week", "Week")
                        .WithMany("Reports")
                        .HasForeignKey("WeekId");

                    b.Navigation("Member");

                    b.Navigation("Week");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Company", b =>
                {
                    b.Navigation("Member");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Member", b =>
                {
                    b.Navigation("Reports");
                });

            modelBuilder.Entity("redbull_team_1_teamreport_back.Domain.Entities.Week", b =>
                {
                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
