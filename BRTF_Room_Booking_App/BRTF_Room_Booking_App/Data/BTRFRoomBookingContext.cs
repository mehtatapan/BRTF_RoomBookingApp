using BRTF_Room_Booking_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Data
{
    public class BTRFRoomBookingContext : DbContext
    {
        public BTRFRoomBookingContext(DbContextOptions<BTRFRoomBookingContext> options)
            : base(options)
        {
        }

        public DbSet<BookingTime> BookingTimes { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<RoomGroup> RoomGroups { get; set; }
        public DbSet<RoomUserGroupPermission> RoomUserGroupPermissions { get; set; }
        public DbSet<TermAndProgram> TermAndPrograms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("BT");

            //Prevent Cascade Delete from Role to User
            //so we are prevented from deleting a Role with
            //Users assigned
            modelBuilder.Entity<Role>()
                .HasMany<User>(d => d.Users)
                .WithOne(p => p.Role)
                .HasForeignKey(p => p.RoleID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from TermAndProgram to User
            //so we are prevented from deleting a TermAndProgram with
            //Users assigned
            modelBuilder.Entity<TermAndProgram>()
                .HasMany<User>(d => d.Users)
                .WithOne(p => p.TermAndProgram)
                .HasForeignKey(p => p.TermAndProgramID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from UserGroup to TermAndProgram
            //so we are prevented from deleting a UserGroup with
            //TermAndPrograms assigned
            modelBuilder.Entity<UserGroup>()
                .HasMany<TermAndProgram>(d => d.TermAndPrograms)
                .WithOne(p => p.UserGroup)
                .HasForeignKey(p => p.UserGroupID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from RoomGroup to Room
            //so we are prevented from deleting a RoomGroup with
            //Rooms assigned
            modelBuilder.Entity<RoomGroup>()
                .HasMany<Room>(d => d.Rooms)
                .WithOne(p => p.RoomGroup)
                .HasForeignKey(p => p.RoomGroupID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from Room to RoomBooking
            //so we are prevented from deleting a Room with
            //RoomBookings assigned
            modelBuilder.Entity<Room>()
                .HasMany<RoomBooking>(d => d.RoomBookings)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomID)
                .OnDelete(DeleteBehavior.Restrict);

            //Prevent Cascade Delete from BookingTime to RoomBooking
            //so we are prevented from deleting a BookingTime with
            //RoomBookings assigned
            modelBuilder.Entity<BookingTime>()
                .HasMany<RoomBooking>(d => d.BookingsStartingWithThisTime)
                .WithOne(p => p.StartTime)
                .HasForeignKey(p => p.StartTimeID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BookingTime>()
                .HasMany<RoomBooking>(d => d.BookingsEndingWithThisTime)
                .WithOne(p => p.EndTime)
                .HasForeignKey(p => p.EndTimeID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add a unique index to the Role Name
            modelBuilder.Entity<Role>()
                .HasIndex(p => p.RoleName)
                .IsUnique();

            //Add a unique index to the UserGroup Name
            modelBuilder.Entity<UserGroup>()
                .HasIndex(p => p.UserGroupName)
                .IsUnique();

            //Add a unique index to the combined Term and Program Code
            modelBuilder.Entity<TermAndProgram>()
                .HasIndex(p => new { p.ProgramLevel, p.ProgramCode })
                .IsUnique();

            //Add a unique index to the Username
            modelBuilder.Entity<User>()
                .HasIndex(p => p.Username)
                .IsUnique();

            //Add a unique index to the RoomGroup Name
            modelBuilder.Entity<RoomGroup>()
                .HasIndex(p => p.AreaName)
                .IsUnique();

            //Add a unique index to the combined RoomGroupID and UserGroupID in Permissions
            modelBuilder.Entity<RoomUserGroupPermission>()
                .HasIndex(p => new { p.RoomGroupID, p.UserGroupID })
                .IsUnique();

            //Add a unique index to the combined RoomGroupID and Room Name
            modelBuilder.Entity<Room>()
                .HasIndex(p => new { p.RoomGroupID, p.RoomName })
                .IsUnique();

            //Add a unique index to the combined Houre and Minute
            modelBuilder.Entity<BookingTime>()
                .HasIndex(p => new { p.MilitaryTimeHour, p.MilitaryTimeMinute })
                .IsUnique();
        }
    }
}
