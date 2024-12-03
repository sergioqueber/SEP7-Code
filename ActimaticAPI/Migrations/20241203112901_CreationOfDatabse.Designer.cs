﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ActimaticAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241203112901_CreationOfDatabse")]
    partial class CreationOfDatabse
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Model.Activity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AwardedPoints")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ReportId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StaffId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.HasIndex("StaffId");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("Model.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Model.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmissionsSaved")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Model.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Availability")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PointsRequired")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ReportId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("Model.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Points")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TeamId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReportUser", b =>
                {
                    b.Property<int>("ActiveParticipantsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReportsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ActiveParticipantsId", "ReportsId");

                    b.HasIndex("ReportsId");

                    b.ToTable("ReportUser");
                });

            modelBuilder.Entity("RewardTeam", b =>
                {
                    b.Property<int>("TeamRewardsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TeamsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TeamRewardsId", "TeamsId");

                    b.HasIndex("TeamsId");

                    b.ToTable("RewardTeam");
                });

            modelBuilder.Entity("RewardUser", b =>
                {
                    b.Property<int>("RewardsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StaffId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RewardsId", "StaffId");

                    b.HasIndex("StaffId");

                    b.ToTable("RewardUser");
                });

            modelBuilder.Entity("Model.CarPool", b =>
                {
                    b.HasBaseType("Model.Activity");

                    b.Property<string>("CarType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Distance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmptySeats")
                        .HasColumnType("INTEGER");

                    b.ToTable("CarPools", (string)null);
                });

            modelBuilder.Entity("Model.SavingFood", b =>
                {
                    b.HasBaseType("Model.Activity");

                    b.Property<int>("PackagesSaved")
                        .HasColumnType("INTEGER");

                    b.ToTable("SavingFoods", (string)null);
                });

            modelBuilder.Entity("Model.Stairs", b =>
                {
                    b.HasBaseType("Model.Activity");

                    b.Property<int>("Floors")
                        .HasColumnType("INTEGER");

                    b.ToTable("Stairs", (string)null);
                });

            modelBuilder.Entity("Model.Transport", b =>
                {
                    b.HasBaseType("Model.Activity");

                    b.Property<int>("Distance")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmissionsSaved")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Transports", (string)null);
                });

            modelBuilder.Entity("Model.Volunteering", b =>
                {
                    b.HasBaseType("Model.Activity");

                    b.Property<int>("Hours")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.ToTable("Volunteerings", (string)null);
                });

            modelBuilder.Entity("Model.Activity", b =>
                {
                    b.HasOne("Model.Report", "Report")
                        .WithMany("CompletedActivities")
                        .HasForeignKey("ReportId");

                    b.HasOne("Model.User", "Staff")
                        .WithMany("Activities")
                        .HasForeignKey("StaffId");

                    b.Navigation("Report");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Model.Reward", b =>
                {
                    b.HasOne("Model.Report", null)
                        .WithMany("AwardedRewards")
                        .HasForeignKey("ReportId");
                });

            modelBuilder.Entity("Model.Team", b =>
                {
                    b.HasOne("Model.Department", "Department")
                        .WithMany("Teams")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.HasOne("Model.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Team", "Team")
                        .WithMany("Staff")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("ReportUser", b =>
                {
                    b.HasOne("Model.User", null)
                        .WithMany()
                        .HasForeignKey("ActiveParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Report", null)
                        .WithMany()
                        .HasForeignKey("ReportsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RewardTeam", b =>
                {
                    b.HasOne("Model.Reward", null)
                        .WithMany()
                        .HasForeignKey("TeamRewardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.Team", null)
                        .WithMany()
                        .HasForeignKey("TeamsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RewardUser", b =>
                {
                    b.HasOne("Model.Reward", null)
                        .WithMany()
                        .HasForeignKey("RewardsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Model.User", null)
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Model.Department", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Model.Report", b =>
                {
                    b.Navigation("AwardedRewards");

                    b.Navigation("CompletedActivities");
                });

            modelBuilder.Entity("Model.Team", b =>
                {
                    b.Navigation("Staff");
                });

            modelBuilder.Entity("Model.User", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}
