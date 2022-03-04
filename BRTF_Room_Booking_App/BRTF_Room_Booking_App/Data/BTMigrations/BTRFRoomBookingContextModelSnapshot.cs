﻿// <auto-generated />
using System;
using BRTF_Room_Booking_App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BRTF_Room_Booking_App.Data.BTMigrations
{
    [DbContext(typeof(BTRFRoomBookingContext))]
    partial class BTRFRoomBookingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22");

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.GlobalSetting", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EmailBookingNotificationsOverride")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EmailCancelNotificationsOverride")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndOfTermDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("LatestAllowableFutureBookingDay")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PreventBookingNotificationsOverride")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("PreventCancelNotificationsOverride")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartOfTermDate")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("GlobalSettings");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomGroupID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("RoomMaxHoursTotal")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("RoomGroupID", "RoomName")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.RoomBooking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoomID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SpecialNotes")
                        .HasColumnType("TEXT")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("RoomID");

                    b.HasIndex("UserID");

                    b.ToTable("RoomBookings");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.RoomGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AreaName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<int>("BlackoutTime")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT")
                        .HasMaxLength(1000);

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxHoursPerSingleBooking")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxHoursTotal")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxNumberOfBookings")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("AreaName")
                        .IsUnique();

                    b.ToTable("RoomGroups");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.RoomUserGroupPermission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoomGroupID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserGroupID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserGroupID");

                    b.HasIndex("RoomGroupID", "UserGroupID")
                        .IsUnique();

                    b.ToTable("RoomUserGroupPermissions");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.TermAndProgram", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProgramCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(5);

                    b.Property<int>("ProgramLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProgramName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("UserGroupID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("UserGroupID");

                    b.HasIndex("ProgramLevel", "ProgramCode")
                        .IsUnique();

                    b.ToTable("TermAndPrograms");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<bool>("EmailBookingNotifications")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EmailCancelNotifications")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.Property<string>("MiddleName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("TermAndProgramID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("TermAndProgramID");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.UserGroup", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserGroupName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.HasKey("ID");

                    b.HasIndex("UserGroupName")
                        .IsUnique();

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.Room", b =>
                {
                    b.HasOne("BRTF_Room_Booking_App.Models.RoomGroup", "RoomGroup")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomGroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.RoomBooking", b =>
                {
                    b.HasOne("BRTF_Room_Booking_App.Models.Room", "Room")
                        .WithMany("RoomBookings")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BRTF_Room_Booking_App.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.RoomUserGroupPermission", b =>
                {
                    b.HasOne("BRTF_Room_Booking_App.Models.RoomGroup", "RoomGroup")
                        .WithMany("RoomUserGroupPermissions")
                        .HasForeignKey("RoomGroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BRTF_Room_Booking_App.Models.UserGroup", "UserGroup")
                        .WithMany("RoomUserGroupPermissions")
                        .HasForeignKey("UserGroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.TermAndProgram", b =>
                {
                    b.HasOne("BRTF_Room_Booking_App.Models.UserGroup", "UserGroup")
                        .WithMany("TermAndPrograms")
                        .HasForeignKey("UserGroupID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("BRTF_Room_Booking_App.Models.User", b =>
                {
                    b.HasOne("BRTF_Room_Booking_App.Models.TermAndProgram", "TermAndProgram")
                        .WithMany("Users")
                        .HasForeignKey("TermAndProgramID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
