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
                        new UserGroup { UserGroupName = "combo2" },
                        new UserGroup { UserGroupName = "tv3" },
                        new UserGroup { UserGroupName = "tv4" },
                        new UserGroup { UserGroupName = "tv5" },
                        new UserGroup { UserGroupName = "tv6" },
                        new UserGroup { UserGroupName = "film3" },
                        new UserGroup { UserGroupName = "film4" },
                        new UserGroup { UserGroupName = "film5" },
                        new UserGroup { UserGroupName = "film6" },
                        new UserGroup { UserGroupName = "pres2" },
                        new UserGroup { UserGroupName = "pres3" },
                        new UserGroup { UserGroupName = "pres4" },
                        new UserGroup { UserGroupName = "pres5" },
                        new UserGroup { UserGroupName = "pres6" },
                        new UserGroup { UserGroupName = "acting" },
                        new UserGroup { UserGroupName = "acting3" },
                        new UserGroup { UserGroupName = "acting4" },
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
                            ProgramName = "TV Production",
                            ProgramCode = "P0164",
                            ProgramLevel = 3,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "TV3").ID
                        },
                        new TermAndProgram {
                            ProgramName = "TV Production",
                            ProgramCode = "P0164",
                            ProgramLevel = 4,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "TV4").ID
                        },
                        new TermAndProgram {
                            ProgramName = "TV Production",
                            ProgramCode = "P0164",
                            ProgramLevel = 5,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "TV5").ID
                        },
                        new TermAndProgram {
                            ProgramName = "TV Production",
                            ProgramCode = "P0164",
                            ProgramLevel = 6,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "TV6").ID
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
                        },
                        new TermAndProgram {
                            ProgramName = "Film Production",
                            ProgramCode = "P0165",
                            ProgramLevel = 3,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "FILM3").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Film Production",
                            ProgramCode = "P0165",
                            ProgramLevel = 4,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "FILM4").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Film Production",
                            ProgramCode = "P0165",
                            ProgramLevel = 5,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "FILM5").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Film Production",
                            ProgramCode = "P0165",
                            ProgramLevel = 6,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "FILM6").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Broadcasting: Radio, TV & Film",
                            ProgramCode = "P0122",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Presentation / Radio",
                            ProgramCode = "P0163",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Presentation / Radio",
                            ProgramCode = "P0163",
                            ProgramLevel = 2,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "PRES2").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Presentation / Radio",
                            ProgramCode = "P0163",
                            ProgramLevel = 3,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "PRES3").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Presentation / Radio",
                            ProgramCode = "P0163",
                            ProgramLevel = 4,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "PRES4").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Presentation / Radio",
                            ProgramCode = "P0163",
                            ProgramLevel = 5,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "PRES5").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Presentation / Radio",
                            ProgramCode = "P0163",
                            ProgramLevel = 6,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "PRES6").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Acting for TV & Film",
                            ProgramCode = "P0198",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "ACTING").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Acting for TV & Film",
                            ProgramCode = "P0198",
                            ProgramLevel = 2,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "ACTING").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Digital Photography",
                            ProgramCode = "P0795",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Joint BSc Game Programming",
                            ProgramCode = "P6801",
                            ProgramLevel = 4,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Joint BA Game Design",
                            ProgramCode = "P6800",
                            ProgramLevel = 6,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Game Development",
                            ProgramCode = "P0441",
                            ProgramLevel = 3,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        },
                        new TermAndProgram {
                            ProgramName = "CST - Network and Cloud Tech",
                            ProgramCode = "P0474",
                            ProgramLevel = 3,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "COMBO1").ID
                        }
                    };
                    context.TermAndPrograms.AddRange(termAndPrograms);
                    context.SaveChanges();
                }

                // Seed many Users
                //if (!context.Users.Any())
                //{
                //    int[] roleIDs = context.Roles.Where(r => !r.RoleName.ToUpper().Contains("ADMIN")).Select(r => r.ID).ToArray();  // DON'T SEED ANY ADMINS
                //    int[] termAndProgramIDs = context.TermAndPrograms.Select(u => u.ID).ToArray();   // DON'T SEED ANY ADMINS
                //    int roleIDCount = roleIDs.Count();
                //    int termAndProgramIDCount = termAndProgramIDs.Count();

                //    string[] firstNames = new string[] { "Lyric", "Antoinette", "Kendal", "Vivian", "Ruth", "Jamison", "Emilia", "Natalee", "Yadiel", "Jakayla", "Lukas", "Moses", "Kyler", "Karla", "Chanel", "Tyler", "Camilla", "Quintin", "Braden", "Clarence" };
                //    string[] lastNames = new string[] { "Watts", "Randall", "Arias", "Weber", "Stone", "Carlson", "Robles", "Frederick", "Parker", "Morris", "Soto", "Bruce", "Orozco", "Boyer", "Burns", "Cobb", "Blankenship", "Houston", "Estes", "Atkins", "Miranda", "Zuniga", "Ward", "Mayo", "Costa", "Reeves", "Anthony", "Cook", "Krueger", "Crane", "Watts", "Little", "Henderson", "Bishop" };
                //    int firstNameCount = firstNames.Count();

                //    foreach (string lastName in lastNames)
                //    {
                //        // Choose a random HashSet of 2 (unique) first names
                //        HashSet<string> selectedFirstNames = new HashSet<string>();
                //        while (selectedFirstNames.Count() < 2)
                //        {
                //            selectedFirstNames.Add(firstNames[random.Next(firstNameCount)]);
                //        }

                //        foreach (string firstName in selectedFirstNames)
                //        {
                //            // Construct User details
                //            User user = new User()
                //            {
                //                Username = random.Next(4000000, 5000000).ToString(),
                //                Password = "password",
                //                FirstName = firstName,
                //                LastName = lastName,
                //                Email = firstName[1].ToString().ToLower() + lastName.ToLower() + random.Next(1, 10).ToString() + "@ncstudents.niagaracollege.ca",
                //                EmailBookingNotifications = true,
                //                EmailCancelNotifications = true,
                //                TermAndProgramID = termAndProgramIDs[random.Next(termAndProgramIDCount)],
                //                RoleID = roleIDs[random.Next(roleIDCount)]
                //            };
                //            context.Users.Add(user);
                //            try
                //            {
                //                // Could be duplicates
                //                context.SaveChanges();
                //            }
                //            catch (Exception e)
                //            {
                //                var m = e.Message;
                //                // So skip it and go on to the next
                //            }
                //        }
                //    }
                //}

                // Seed 1 single User
                if (!context.Users.Any())
                {
                    int[] roleIDs = context.Roles.Where(r => !r.RoleName.ToUpper().Contains("ADMIN")).Select(r => r.ID).ToArray();  // DON'T SEED ANY ADMINS
                    int[] termAndProgramIDs = context.TermAndPrograms.Select(u => u.ID).ToArray();   // DON'T SEED ANY ADMINS
                    int roleIDCount = roleIDs.Count();
                    int termAndProgramIDCount = termAndProgramIDs.Count();

                    // Construct User details
                    User user = new User()
                    {
                        Username = random.Next(3000000, 4000000).ToString(),
                        Password = "password",
                        FirstName = "Dogs",
                        MiddleName = "Sun",
                        LastName = "Al-Bahran",
                        Email = "dalbahran@ncstudents.niagaracollege.ca",
                        EmailBookingNotifications = true,
                        EmailCancelNotifications = true,
                        TermAndProgramID = termAndProgramIDs[random.Next(termAndProgramIDCount)],
                        RoleID = roleIDs[random.Next(roleIDCount)]
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                }

                // Seed Room Groups
                if (!context.RoomGroups.Any())
                {
                    // Construct Room Group details
                    var roomGroups = new List<RoomGroup>
                    {
                    new RoomGroup()
                    {
                        AreaName = "Edit 13 BRTF1435 & 3yr TV",
                        Description = "Max Booking 4 hours\r\nThis suite contains:\r\nMedia Composer, Adobe Suite, DaVinci Resolve, Pro Tools\r\n4th Term Film/TV or 5th Term TV students",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 4,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = 3,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Edit 15 BRTF1435, Term 5",
                        Description = "Max Booking 4 hours\r\nThis suite contains:\r\nCreative Suite, Media Composer, DaVinci Resolve, Pro Tools\r\n4th Term Film/TV or 5th Term TV students",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 4,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Edit 6, 3rd Year only",
                        Description = "Max Booking 6 hours\r\nThis suite contains:\r\nCreative Suite, Media Composer, DaVinci Resolve, Pro Tools",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 6,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = 2,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Edit 8 Inside Niagara",
                        Description = "Max Booking 2 hours\r\nThis suite contains:\r\nCreative Suite, Media Composer, DaVinci Resolve\r\n3rd Term Presentation or 4th Term TV",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 2,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Edit 9, 10 and 14, 2nd Years",
                        Description = "Max Booking 6 hours\r\nThis suite contains:\r\nCreative Suite, Media Composer, DaVinci Resolve, Pro Tools\r\n2nd Year students and 3rd Year Presentation",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 6,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Edits 1-5, 3rd Year Film",
                        Description = "Max Booking 6 hours\r\nThis suite contains:\r\nCreative Suite, Media Composer, DaVinci Resolve, Pro Tools\r\n3rd Year Film only. Other students won't be approved without a signed building pass.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 6,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = 3,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Film Studio V001",
                        Description = "",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = Int32.MaxValue,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Green Room",
                        Description = "Max Booking 12 hours\r\nRoom typically for teams preparing for a TV or film shoot",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 12,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "MAC Lab V106",
                        Description = "Max Booking 6 hours\r\nAll MACs Contain:\r\nMS Office, Adobe Suite, Media Composer, DaVinci Resolve, Pro Tools\r\n17 computers",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 6,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Mixing Theatre V105",
                        Description = "Max Booking 8 hours\r\nAvailable after class until midnight Monday to Friday. Weekends off limits. Approval from Luke Hutton before use.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 8,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Radio Edit Suites V109",
                        Description = "Max Booking 4 hours\r\n8 computers",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 4,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Radio Recording Studios V109",
                        Description = "Max Booking 2 hours\r\nAll studios have:\r\nAccess for phone interviews. Announce booth 1 is used for news and sports. Announce booth 2 is used for voice tracking.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 2,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "TV Studio V002",
                        Description = "Max Booking 18-hours\r\nDifferent rooms have varying booking times. Please check to confirm:\r\n1st year students may reserve the studio as per instructor instructors.\r\nAll others can obtain approval through Alysha Henderson",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 18,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "V110",
                        Description = "",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = Int32.MaxValue,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "V110 Acting Lab",
                        Description = "Max Booking 2-hours\r\nBookings is off-limits from 12:30am to the end of classes, Monday-Friday.\r\nFor exceptions, approval must be granted by Lori Ravensborg.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 2,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "V110f Acting Edit",
                        Description = "",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = Int32.MaxValue,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "V204p Production Planning",
                        Description = "Max Booking 1-hour\r\nBRTF Project Meeting Room\r\nOnly available Monday-Friday between 8:30am-5:30pm.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 1,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = true
                    },
                    new RoomGroup()
                    {
                        AreaName = "Camera Test",
                        Description = "",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = Int32.MaxValue,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = false
                    },
                    new RoomGroup()
                    {
                        AreaName = "Edit 16 BRTF1435, Term 5 TV",
                        Description = "Max Booking 4-hours\r\nAll studios have:\r\nP2 Reader, Digitize/Log/Print Deck, SoundTrack, Avid, Final Cut Pro, DiffMerge, AdobeCS Suite, Aspera Connect\r\n4th term Film/TV or 5th term TV students only.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 4,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = false
                    },
                    new RoomGroup()
                    {
                        AreaName = "MultiTrack V1j",
                        Description = "Max Booking 2-hours\r\nAll studios have:\r\nMixing Board, Attached Audio Booth, SoundTrack Pro\r\n*Note* SoundTrack Pro is on all of the edit suites and Mac Lab",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 2,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = false
                    },
                    new RoomGroup()
                    {
                        AreaName = "V011 Assignment/Offload",
                        Description = "Not bookable for meetings.\r\nOpen Access space for assignment finishing or media transfer.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = Int32.MaxValue,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = false
                    },
                    new RoomGroup()
                    {
                        AreaName = "V2 and S339 Acting",
                        Description = "Max Booking 1-hour.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 1,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = false
                    },
                    new RoomGroup()
                    {
                        AreaName = "V3 Demonstration Lab",
                        Description = "Max Booking 6-hours.",
                        BlackoutTime = 1,
                        MaxHoursPerSingleBooking = 6,
                        MaxHoursTotal = Int32.MaxValue,
                        MaxNumberOfBookings = Int32.MaxValue,
                        Enabled = false
                    }
                    };

                    context.RoomGroups.AddRange(roomGroups);
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

                    // Add 1 room to Camera Test area
                    Room cameraRoom = new Room()
                    {
                        RoomName = "Red Camera 1",
                        RoomMaxHoursTotal = Int32.MaxValue,
                        RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("CAMERA TEST")).ID
                    };
                    context.Rooms.Add(cameraRoom);
                    context.SaveChanges();
                }

                // Seed Room Bookings
                //if (!context.RoomBookings.Any())
                //{
                //    int[] userIDs = context.Users.Select(u => u.ID).ToArray();
                //    int[] roomIDs = context.Rooms.Select(r => r.ID).ToArray();
                //    int userIDCount = userIDs.Count();
                //    int roomIDCount = roomIDs.Count();

                //    // Add a Room Booking in the afternoon for each Room
                //    for (int i = 0; i < roomIDCount; i++)
                //    {
                //        // Construct Room Booking details
                //        RoomBooking roomBooking = new RoomBooking()
                //        {
                //            StartDate = DateTime.Today.AddDays(i),
                //            RoomID = roomIDs[i],
                //            UserID = userIDs[random.Next(userIDCount)],
                //            StartTimeID = context.BookingTimes.FirstOrDefault(b => b.MilitaryTimeHour == 12).ID,
                //            EndTimeID = context.BookingTimes.FirstOrDefault(b => b.MilitaryTimeHour == 13).ID
                //        };
                //        context.RoomBookings.Add(roomBooking);
                //        try
                //        {
                //            // Could be duplicates
                //            context.SaveChanges();
                //        }
                //        catch (Exception e)
                //        {
                //            var m = e.Message;
                //            // So skip it and go on to the next
                //        }
                //    }
                //}

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
