using BRTF_Room_Booking_App.Models;
using Microsoft.AspNetCore.Identity;
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
                    var rooms = new List<Room>
                    {
                        new Room()
                        {
                            RoomName = "Red Camera 1",
                            RoomMaxHoursTotal = Int32.MaxValue,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("CAMERA TEST")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 13",
                            RoomMaxHoursTotal = 4,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 13 BRTF1435 & 3YR TV")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 15",
                            RoomMaxHoursTotal = 4,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 15 BRTF1435, TERM 5")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 6",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 6, 3RD YEAR ONLY")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 8 V204i",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 8 INSIDE NIAGARA")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 9",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 9, 10 AND 14, 2ND YEARS")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 10",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 9, 10 AND 14, 2ND YEARS")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 14",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 9, 10 AND 14, 2ND YEARS")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 1/2 Colour Suite",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDITS 1-5, 3RD YEAR FILM")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 3",
                            RoomMaxHoursTotal = 6,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDITS 1-5, 3RD YEAR FILM")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 4",
                            RoomMaxHoursTotal = 6,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDITS 1-5, 3RD YEAR FILM")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 5",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDITS 1-5, 3RD YEAR FILM")).ID
                        },
                        new Room()
                        {
                            RoomName = "Film Studio V001",
                            RoomMaxHoursTotal = Int32.MaxValue,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("FILM STUDIO V001")).ID
                        },
                        new Room()
                        {
                            RoomName = "Green Room",
                            RoomMaxHoursTotal = 12,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("GREEN ROOM")).ID
                        },
                        new Room()
                        {
                            RoomName = "Mixing Theatre V5",
                            RoomMaxHoursTotal = 8,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("MIXING THEATRE V105")).ID
                        },
                        new Room()
                        {
                            RoomName = "Studio & Talk A",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("RADIO RECORDING STUDIOS V109")).ID
                        },
                        new Room()
                        {
                            RoomName = "Studio B",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("RADIO RECORDING STUDIOS V109")).ID
                        },
                        new Room()
                        {
                            RoomName = "Studio C",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("RADIO RECORDING STUDIOS V109")).ID
                        },
                        new Room()
                        {
                            RoomName = "Studio D",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("RADIO RECORDING STUDIOS V109")).ID
                        },
                        new Room()
                        {
                            RoomName = "Annc. 2",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("RADIO RECORDING STUDIOS V109")).ID
                        },
                        new Room()
                        {
                            RoomName = "V2 TV Studio",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("TV STUDIO V002")).ID
                        },
                        new Room()
                        {
                            RoomName = "V2 GreenRoom",
                            RoomMaxHoursTotal = 6,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("TV STUDIO V002")).ID
                        },
                        new Room()
                        {
                            RoomName = "V1",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("TV STUDIO V002")).ID
                        },
                        new Room()
                        {
                            RoomName = "TV Studio Control Room",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("TV STUDIO V002")).ID
                        },
                        new Room()
                        {
                            RoomName = "V110",
                            RoomMaxHoursTotal = Int32.MaxValue,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V110")).ID
                        },
                        new Room()
                        {
                            RoomName = "Acting Lab V110",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V110 ACTING LAB")).ID
                        },
                        new Room()
                        {
                            RoomName = "V110g Acting Edit",
                            RoomMaxHoursTotal = 2,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V110 ACTING LAB")).ID
                        },
                        new Room()
                        {
                            RoomName = "V110f Acting Edit",
                            RoomMaxHoursTotal = Int32.MaxValue,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V110F ACTING EDIT")).ID
                        },
                        new Room()
                        {
                            RoomName = "V204p Production Planning",
                            RoomMaxHoursTotal = 1,
                            Enabled = true,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V204P PRODUCTION PLANNING")).ID
                        },
                        new Room()
                        {
                            RoomName = "Edit 16 Avid/P2/DigLotPrt",
                            RoomMaxHoursTotal = 4,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("EDIT 16 BRTF1435, TERM 5 TV")).ID
                        },
                        new Room()
                        {
                            RoomName = "MultiTrack V1j",
                            RoomMaxHoursTotal = 2,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("MULTITRACK V1J")).ID
                        },
                        new Room()
                        {
                            RoomName = "V011 Assignment/Offload",
                            RoomMaxHoursTotal = Int32.MaxValue,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V011 ASSIGNMENT/OFFLOAD")).ID
                        },
                        new Room()
                        {
                            RoomName = "V2 Acting",
                            RoomMaxHoursTotal = 1,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V2 AND S339 ACTING")).ID
                        },
                        new Room()
                        {
                            RoomName = "S339",
                            RoomMaxHoursTotal = 1,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V2 AND S339 ACTING")).ID
                        },
                        new Room()
                        {
                            RoomName = "V3 Demonstration Lab",
                            RoomMaxHoursTotal = 6,
                            Enabled = false,
                            RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("V3 DEMONSTRATION LAB")).ID
                        }
                    };
                    // Loop to seed 17 computers in MAC Lab V106
                    for (int i = 1; i <= 17; i++)
                {
                    // Construct Room details
                    Room room = new Room()
                    {
                        RoomName = "Computer " + i.ToString(),
                        RoomMaxHoursTotal = Int32.MaxValue,
                        Enabled = true,
                        RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("MAC LAB V106")).ID
                    };
                    rooms.Add(room);
                }
                //Loop to seed 8 computers in Radio Edit V109
                for (int i = 1; i <= 8; i++)
                {
                    //Construct Room details
                    Room room = new Room()
                    {
                        RoomName = "Audio Edit #" + i.ToString(),
                        RoomMaxHoursTotal = 4,
                        Enabled = true,
                        RoomGroupID = context.RoomGroups.FirstOrDefault(u => u.AreaName.ToUpper().Contains("RADIO EDIT SUITES V109")).ID
                    };
                    rooms.Add(room);
                }
                context.Rooms.AddRange(rooms);
                    context.SaveChanges();
                }

                // Seed many Room Bookings
                if (!context.RoomBookings.Any())
                {
                    int[] userIDs = context.Users.Select(u => u.ID).ToArray();
                    int[] roomIDs = context.Rooms.Select(r => r.ID).ToArray();
                    int userIDCount = userIDs.Count();
                    int roomIDCount = roomIDs.Count();

                    // Add a Room Booking in the afternoon for each Room
                    for (int j = 0; j < 3; j++)
                    {
                        for (int i = 0; i < roomIDCount; i++)
                        {
                            // Construct Room Booking details
                            RoomBooking roomBooking = new RoomBooking()
                            {
                                StartDate = DateTime.Now.AddDays((i + 1) * (j + 1)),
                                EndDate = DateTime.Now.AddDays((i + 1) * (j + 1)).AddHours(random.Next(1, 3)),
                                RoomID = roomIDs[i],
                                UserID = userIDs[random.Next(userIDCount)],
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
                }

                // Seed 1 single Room Bookings
                //if (!context.RoomBookings.Any())
                //{
                //    // Construct Room Booking details
                //    RoomBooking roomBooking = new RoomBooking()
                //    {
                //        StartDate = DateTime.Today.AddDays(-1),
                //        RoomID = context.Rooms.Where(r => r.RoomName.ToUpper().Contains("COMPUTER 10")).Select(r => r.ID).FirstOrDefault(),
                //        UserID = context.Users.Select(u => u.ID).FirstOrDefault(),
                //        StartTimeID = context.BookingTimes.FirstOrDefault(b => b.MilitaryTimeHour == 12 && b.MilitaryTimeMinute == 30).ID,
                //        EndTimeID = context.BookingTimes.FirstOrDefault(b => b.MilitaryTimeHour == 13 && b.MilitaryTimeMinute == 30).ID
                //    };
                //    context.RoomBookings.Add(roomBooking);
                //    context.SaveChanges();
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
        public static void SeedMandatoryData(ApplicationDbContext identityContext, IServiceProvider serviceProvider)
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

                // Seed User Groups
                if (!context.UserGroups.Any())
                {
                    var userGroups = new List<UserGroup>
                    {
                        new UserGroup { UserGroupName = "top-level admin" },
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
                            ProgramName = "Top-level Admin",
                            ProgramCode = "P0000",
                            ProgramLevel = 2,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "TOP-LEVEL ADMIN").ID
                        },
                        new TermAndProgram {
                            ProgramName = "Admin",
                            ProgramCode = "P0000",
                            ProgramLevel = 1,
                            UserGroupID = context.UserGroups.FirstOrDefault(u => u.UserGroupName.ToUpper() == "ADMIN").ID
                        },
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

                // Seed Users
                if (!context.Users.Any())
                {
                    //Create Users
                    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                    if (userManager.FindByNameAsync("topadmin").Result == null)
                    {
                        IdentityUser user = new IdentityUser
                        {
                            UserName = "topadmin",
                            Email = "topadmin@niagaracollege.ca"
                        };

                        IdentityResult result = userManager.CreateAsync(user, "password").Result;

                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Top-level Admin").Wait();

                            // Construct User details
                            User userDetails = new User()
                            {
                                Username = "topadmin",
                                FirstName = "Top-level Admin",
                                LastName = "Top-level Admin",
                                Email = "topadmin@niagaracollege.ca",
                                EmailBookingNotifications = true,
                                EmailCancelNotifications = true,
                                TermAndProgramID = context.TermAndPrograms.FirstOrDefault(b => b.ProgramName.ToUpper() == "TOP-LEVEL ADMIN").ID
                            };
                            context.Users.Add(userDetails);
                            context.SaveChanges();
                        }
                    }
                    if (userManager.FindByNameAsync("admin").Result == null)
                    {
                        IdentityUser user = new IdentityUser
                        {
                            UserName = "admin",
                            Email = "admin@niagaracollege.ca"
                        };

                        IdentityResult result = userManager.CreateAsync(user, "password").Result;

                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "Admin").Wait();

                            // Construct User details
                            User userDetails = new User()
                            {
                                Username = "admin",
                                FirstName = "Admin",
                                LastName = "Admin",
                                Email = "admin@niagaracollege.ca",
                                EmailBookingNotifications = true,
                                EmailCancelNotifications = true,
                                TermAndProgramID = context.TermAndPrograms.FirstOrDefault(b => b.ProgramName.ToUpper() == "ADMIN").ID
                            };
                            context.Users.Add(userDetails);
                            context.SaveChanges();
                        }
                    }
                    if (userManager.FindByNameAsync("user").Result == null)
                    {
                        IdentityUser user = new IdentityUser
                        {
                            UserName = "user",
                            Email = "user@ncstudents.niagaracollege.ca"
                        };

                        IdentityResult result = userManager.CreateAsync(user, "password").Result;

                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(user, "User").Wait();

                            // Construct User details
                            User userDetails = new User()
                            {
                                Username = "user",
                                FirstName = "User",
                                LastName = "User",
                                Email = "user@ncstudents.niagaracollege.ca",
                                EmailBookingNotifications = true,
                                EmailCancelNotifications = true,
                                TermAndProgramID = context.TermAndPrograms.FirstOrDefault(b => !b.ProgramName.ToUpper().Contains("ADMIN")).ID
                            };
                            context.Users.Add(userDetails);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
