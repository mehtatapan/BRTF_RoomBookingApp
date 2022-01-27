using BRTF_Room_Booking_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRTF_Room_Booking_App.Data
{
    public static class BTSeedData
    {
        /// <summary>
        /// This method seeds data for testing purposes. Write your test data inside this method.
        /// </summary>
        public static void SeedTestData(IServiceProvider serviceProvider)
        {
            using (var context = new BTRFRoomBookingContext(
                serviceProvider.GetRequiredService<DbContextOptions<BTRFRoomBookingContext>>()))
            {
                Random random = new Random();   // To randomly generate data

                // Seed Roles
                if (!context.Roles.Any())
                {
                    var roles = new List<Role>
                    {
                        new Role { RoleName = "admin" },
                        new Role { RoleName = "user" }
                    };
                    context.Roles.AddRange(roles);
                    context.SaveChanges();
                }

                // Seed User Groups
                if (!context.UserGroups.Any())
                {
                    var userGroups = new List<UserGroup>
                    {
                        new UserGroup { UserGroupName = "admin" },
                        new UserGroup { UserGroupName = "combo1" },
                        new UserGroup { UserGroupName = "combo2" }
                    };
                    context.UserGroups.AddRange(userGroups);
                    context.SaveChanges();
                }

                // Seed Term and Programs
                if (!context.TermAndPrograms.Any())
                {
                    var termAndPrograms = new List<TermAndProgram>
                    {
                        new TermAndProgram {
                            ProgramName = "TV Production",
                            ProgramCode = "P0164",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "TV Production",
                            ProgramCode = "P0164",
                            ProgramLevel = 2,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO2").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Film Production",
                            ProgramCode = "P0165",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Film Production",
                            ProgramCode = "P0165",
                            ProgramLevel = 2,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO2").ID
                        }
                    };
                    context.TermAndPrograms.AddRange(termAndPrograms);
                    context.SaveChanges();
                }

                // Seed Users
                if (!context.Users.Any())
                {
                    int[] roleIDs = context.Roles.Where(r => !r.RoleName.ToUpper().Contains("ADMIN")).Select(r => r.ID).ToArray();  // DON'T SEED ANY ADMINS
                    int[] userGroupIDs = context.UserGroups.Where(u => !u.UserGroupName.ToUpper().Contains("ADMIN")).Select(u => u.ID).ToArray();   // DON'T SEED ANY ADMINS
                    int roleIDCount = roleIDs.Count();
                    int userGroupIDCount = userGroupIDs.Count();

                    string[] firstNames = new string[] { "Lyric", "Antoinette", "Kendal", "Vivian", "Ruth", "Jamison", "Emilia", "Natalee", "Yadiel", "Jakayla", "Lukas", "Moses", "Kyler", "Karla", "Chanel", "Tyler", "Camilla", "Quintin", "Braden", "Clarence" };
                    string[] lastNames = new string[] { "Watts", "Randall", "Arias", "Weber", "Stone", "Carlson", "Robles", "Frederick", "Parker", "Morris", "Soto", "Bruce", "Orozco", "Boyer", "Burns", "Cobb", "Blankenship", "Houston", "Estes", "Atkins", "Miranda", "Zuniga", "Ward", "Mayo", "Costa", "Reeves", "Anthony", "Cook", "Krueger", "Crane", "Watts", "Little", "Henderson", "Bishop" };
                    int firstNameCount = firstNames.Count();

                    foreach (string lastName in lastNames)
                    {
                        // Choose a random HashSet of 2 (unique) first names
                        HashSet<string> selectedFirstNames = new HashSet<string>();
                        while (selectedFirstNames.Count() < 2)
                        {
                            selectedFirstNames.Add(firstNames[random.Next(firstNameCount)]);
                        }

                        foreach (string firstName in selectedFirstNames)
                        {
                            // Construct User details
                            User user = new User()
                            {
                                Username = random.Next(4000000, 5000000).ToString(),
                                Password = "password",
                                FullName = firstName + " " + lastName,
                                Email = firstName[1].ToString().ToLower() + lastName.ToLower() + random.Next(1, 10).ToString() + "@ncstudents.niagaracollege.ca",
                                EmailBookingNotifications = true,
                                EmailCancelNotifications = true,
                                UserGroupID = userGroupIDs[random.Next(userGroupIDCount)],
                                RoleID = roleIDs[random.Next(roleIDCount)]
                            };
                            context.Users.Add(user);
                            try
                            {
                                // Could be duplicates
                                context.SaveChanges();
                            }
                            catch (Exception e)
                            {
                                var m = e.Message;
                                // So skip it and go on to the next
                            }
                        }
                    }
                }

                // Seed Room Groups
                if (!context.RoomGroups.Any())
                {
                    // Construct Room Group details
                    RoomGroup roomGroup = new RoomGroup()
                    {
                        AreaName = "MAC Lab V106",
                        Description = "Max Booking 6-hours\r\nAll MACs Contain:\r\nMS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools\r\n17 computers",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 6,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue
                    };
                    context.RoomGroups.Add(roomGroup);
                    context.SaveChanges();
                }

                // Seed Rooms
                if (!context.Rooms.Any())
                {
                    // Loop to seed 17 computers in MAC Lab V106
                    for (int i = 1; i <= 17; i++)
                    {
                        // Construct Room details
                        Room room = new Room()
                        {
                            RoomName = "Computer " + i.ToString(),
                            RoomMaxHoursTotal = Int32.MaxValue,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("MAC LAB V106")).ID
                        };
                        context.Rooms.Add(room);
                        context.SaveChanges();
                    }
                }

                // Seed Room Bookings
                if (!context.RoomBookings.Any())
                {
                    int[] userIDs = context.Users.Select(u => u.ID).ToArray();
                    int[] roomIDs = context.Rooms.Select(r => r.ID).ToArray();
                    int userIDCount = userIDs.Count();
                    int roomIDCount = roomIDs.Count();

                    // Add a Room Booking in the afternoon for each Room
                    for (int i = 0; i < roomIDCount; i++)
                    {
                        // Construct Room Booking details
                        RoomBooking roomBooking = new RoomBooking()
                        {
                            Date = DateTime.Today.AddDays(i),
                            RoomID = roomIDs[i],
                            UserID = userIDs[random.Next(userIDCount)],
                            StartTimeID = context.BookingTimes.FirstOrDefault(b => b.MilitaryTimeHour == 12).ID,
                            EndTimeID = context.BookingTimes.FirstOrDefault(b => b.MilitaryTimeHour == 13).ID
                        };
                        context.RoomBookings.Add(roomBooking);
                        try
                        {
                            // Could be duplicates
                            context.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            var m = e.Message;
                            // So skip it and go on to the next
                        }
                    }
                }

                // Seed Room User Group Permissions
                if (!context.RoomUserGroupPermissions.Any())
                {
                    int[] userGroupIDs = context.UserGroups.Select(u => u.ID).ToArray();

                    // Give all User Groups permission to access MAC Lab V106
                    foreach (int userGroupID in userGroupIDs)
                    {
                        // Construct Permission details
                        RoomUserGroupPermission roomUserGroupPermission = new RoomUserGroupPermission()
                        {
                            UserGroupID = userGroupID,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("MAC LAB V106")).ID
                        };
                        context.RoomUserGroupPermissions.Add(roomUserGroupPermission);
                        context.SaveChanges();
                    }
                }


            }
        }

        /// <summary>
        /// This method is for MANDATORY data only. ONLY write data in this method if it is NEEDED to run the application.
        /// </summary>
        public static void SeedMandatoryData(IServiceProvider serviceProvider)
        {
            using (var context = new BTRFRoomBookingContext(
                serviceProvider.GetRequiredService<DbContextOptions<BTRFRoomBookingContext>>()))
            {
                // Seed Global Settings
                if (!context.GlobalSettings.Any())
                {
                    // Construct Global Setting details
                    GlobalSetting globalSettings = new GlobalSetting()
                    {
                        StartOfTermDate = Convert.ToDateTime("2022-01-01"),
                        EndOfTermDate = Convert.ToDateTime("2022-04-30"),
                        LatestAllowableFutureBookingDay = 14,
                        EmailBookingNotificationsOverride = false,
                        PreventBookingNotificationsOverride = false,
                        EmailCancelNotificationsOverride = false,
                        PreventCancelNotificationsOverride = false
                    };
                    context.GlobalSettings.Add(globalSettings);
                    context.SaveChanges();
                }
                
                // Seed Booking Times
                if (!context.BookingTimes.Any())
                {
                    var bookingTimes = new List<BookingTime>
                    {
                        new BookingTime { MilitaryTimeHour = 0, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 1, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 2, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 3, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 4, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 5, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 6, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 7, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 8, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 9, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 10, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 11, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 12, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 13, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 14, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 15, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 16, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 17, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 18, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 19, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 20, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 21, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 22, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 23, MilitaryTimeMinute = 30 },
                        new BookingTime { MilitaryTimeHour = 24, MilitaryTimeMinute = 30 }
                    };
                    context.BookingTimes.AddRange(bookingTimes);
                    context.SaveChanges();
                }
            }
        }
    }
}
