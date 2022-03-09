using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BRTF_Room_Booking_App.Data;
using BRTF_Room_Booking_App.Models;
using BRTF_Room_Booking_App.Utilities;
using BRTF_Room_Booking_App.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace BRTF_Room_Booking_App.Controllers
{
    [Authorize(Roles = "Top-level Admin,Admin,User")]
    public class RoomBookingsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public RoomBookingsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index(int? page, int? pageSizeID, /* Paging */
            int? RoomGroupID, int? RoomID, string SearchAfterDate, string SearchBeforeDate, string SearchUsername, string SearchFullName, /* Filters/Search */
            string actionButton, string sortDirection = "asc", string sortField = "Start Date" /*Sorting*/)
        {
            //Toggle the open/closed state of the collapse depending on if something is being filtered
            ViewData["Filtering"] = ""; //Assume nothing is filtered

            //NOTE: make sure this array has matching values to the column headings
            string[] sortOptions = new[] { "Start Date" };

            ViewData["RoomGroupID"] = RoomGroupSelectList(RoomGroupID);    // Room data is loaded separately from other dropdownlists, since it is sometimes connected to a multiselect
            ViewData["RoomID"] = RoomSelectList(RoomGroupID, RoomID);
            GetRoomsJSON();
            GetBookingsJSON();

            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var roombookings = from r in _context.RoomBookings
                               .Include(r => r.Room)
                               .Include(r => r.User)
                               select r;

            //Add as many filters as needed
            if (!String.IsNullOrEmpty(SearchAfterDate) && DateTime.TryParse(SearchAfterDate, out DateTime afterDate))
            {
                roombookings = roombookings.Where(r => afterDate <= r.StartDate);
                ViewData["Filtering"] = "show";
            }
            if (!String.IsNullOrEmpty(SearchBeforeDate) && DateTime.TryParse(SearchBeforeDate, out DateTime beforeDate))
            {
                roombookings = roombookings.Where(r => r.StartDate <= beforeDate);
                ViewData["Filtering"] = "show";
            }
            if (!String.IsNullOrEmpty(SearchUsername))
            {
                roombookings = roombookings.Where(r => r.User.Username.ToUpper().Contains(SearchUsername.ToUpper()));
                ViewData["Filtering"] = "show";
            }
            if (!String.IsNullOrEmpty(SearchFullName))
            {
                roombookings = roombookings.Where(r => (r.User.FirstName + " " + r.User.LastName).ToUpper().Contains(SearchFullName.ToUpper())
                                                    || (r.User.FirstName + " " + r.User.MiddleName + " " + r.User.LastName).ToUpper().Contains(SearchFullName.ToUpper()));
                ViewData["Filtering"] = "show";
            }
            if (RoomGroupID.HasValue)
            {
                roombookings = roombookings.Where(r => r.Room.RoomGroupID == RoomGroupID);
                ViewData["Filtering"] = " show ";
            }
            if (RoomID.HasValue)
            {
                roombookings = roombookings.Where(r => r.RoomID == RoomID);
                ViewData["Filtering"] = " show ";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
            {
                if (sortOptions.Contains(actionButton))//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }
            //Now we know which field and direction to sort by
            if (sortField == "Start Date")
            {
                if (sortDirection == "asc")
                {
                    roombookings = roombookings
                        .OrderBy(p => p.StartDate);
                }
                else
                {
                    roombookings = roombookings
                        .OrderByDescending(p => p.StartDate);
                }
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<RoomBooking>.CreateAsync(roombookings.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: RoomBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room).ThenInclude(r => r.RoomGroup)
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User"))
            {
                if (User.Identity.Name != roomBooking.User.Username)
                {
                    TempData["Message"] = "You are not authorized to view another User's Booking details.";
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }

            ViewData["RoomGroupID"] = RoomGroupSelectList(roomBooking.Room.RoomGroupID);    // Room data is loaded separately from other dropdownlists, since it is sometimes connected to a multiselect
            ViewData["RoomID"] = RoomSelectList(roomBooking.Room.RoomGroupID, roomBooking.RoomID);
            PopulateDropDownLists(roomBooking);
            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public IActionResult Create()
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            // Disable User selection field for non-Admins
            if (User.IsInRole("Top-level Admin") || User.IsInRole("Admin"))
            {
                ViewData["UserIdDisabled"] = false;
            }
            else
            {
                ViewData["UserIdDisabled"] = true;
            }

            RoomBooking blankBooking = new RoomBooking();
            blankBooking.UserID = _context.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();

            var roomGroupSelectList = PermittedRoomGroupSelectList();    // Generate the Select List of rooms before putting it in ViewData, so that the populated room data can match the select list
            ViewData["RoomGroupID"] = roomGroupSelectList;
            PopulateSelectedRoomData(Convert.ToInt32(roomGroupSelectList.FirstOrDefault().Value));
            ViewData["RoomID"] = RoomSelectList(Convert.ToInt32(roomGroupSelectList.FirstOrDefault().Value));
            ViewData["RepeatType"] = RepeatTypeSelectList();
            PopulateDropDownLists(blankBooking);
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,SpecialNotes,StartDate,EndDate")] RoomBooking roomBooking,
            string[] selectedOptions, int RoomGroupID, string chkRepeat, string RepeatInterval, string RepeatType,
            string Monday, string Tuesday, string Wednesday, string Thursday, string Friday, string Saturday, string Sunday,
            string RepeatEndDate, int RoomID)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            // Disable User selection field for non-Admins
            if (User.IsInRole("Top-level Admin") || User.IsInRole("Admin"))
            {
                ViewData["UserIdDisabled"] = false;
            }
            else
            {
                ViewData["UserIdDisabled"] = true;
                roomBooking.UserID = _context.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();
            }

            // IMPORTANT NOTE: "roomBooking" variable is ONLY used to validate model state. DO NOT ADD "roomBooking"

            // Default all Repeat controls to Off
            ViewData["chkRepeat"] = "";
            ViewData["RepeatContainer"] = "";
            ViewData["RepeatInterval"] = "";
            ViewData["Monday"] = "";
            ViewData["Tuesday"] = "";
            ViewData["Wednesday"] = "";
            ViewData["Thursday"] = "";
            ViewData["Friday"] = "";
            ViewData["Saturday"] = "";
            ViewData["Sunday"] = "";
            ViewData["RepeatEndDate"] = "";

            // Check that Start Time is not in the past
            if (roomBooking.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Start Date can not be in the past.");
            }

            // Validation for Admin's Room selection boxes
            if (User.IsInRole("Top-level Admin") || User.IsInRole("Admin"))
            {
                // Add model error if selected options is empty or cannot be cast as Int
                if (selectedOptions.Count() == 0)
                {
                    ModelState.AddModelError("RoomID", "You must select a Room.");
                }
                else
                {
                    int t;
                    if (!Int32.TryParse(selectedOptions[0], out t))
                    {
                        // t failed to be set with the converted integer
                        // Add here a message to user about the wrong input....
                        ModelState.AddModelError("RoomID", "You must select a valid Room.");
                    }
                }
            }
            else if (User.IsInRole("User"))
            {
                // If the User is only a regular User, they can only select 1 Room
                // We loop later on, to make a Booking for every Room in "selectedOptions"
                // To make the loop only run once for 1 Room, we will make "selectedOptions" be the room the regular User selected
                selectedOptions = new string[] { RoomID.ToString() };
            }

            // Validate Repeat controls if checkbox is On
            if (chkRepeat == "on")
            {
                // Store values for Repeat controls so they will re-appear after postback
                ViewData["chkRepeat"] = "checked";
                ViewData["RepeatContainer"] = "show";
                ViewData["RepeatInterval"] = RepeatInterval;
                if (Monday == "on") ViewData["Monday"] = "checked";
                if (Tuesday == "on") ViewData["Tuesday"] = "checked";
                if (Wednesday == "on") ViewData["Wednesday"] = "checked";
                if (Thursday == "on") ViewData["Thursday"] = "checked";
                if (Friday == "on") ViewData["Friday"] = "checked";
                if (Saturday == "on") ViewData["Saturday"] = "checked";
                if (Sunday == "on") ViewData["Sunday"] = "checked";
                ViewData["RepeatEndDate"] = RepeatEndDate;

                // Add model errors for any Repeat controls that are not set correctly
                int t;
                if (RepeatInterval == null)
                {
                    ModelState.AddModelError("", "You must set a Repeat Interval (e.g. Every 1 Days).");
                }
                else if (RepeatInterval.Contains(".") || !Int32.TryParse(RepeatInterval, out t))
                {
                    ModelState.AddModelError("", "Repeat Interval must be a whole number (No decimals).");
                }
                else if (t < 1)
                {
                    ModelState.AddModelError("", "Repeat Interval cannot be less than 1.");
                }

                if (RepeatEndDate == null)
                {
                    ModelState.AddModelError("", "You must set a Repeat End Date.");
                }
                else if (Convert.ToDateTime(RepeatEndDate) <= roomBooking.StartDate)
                {
                    ModelState.AddModelError("", "Repeat End Date must be after Start Date.");
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Overall list of bookings to be added
                    List<RoomBooking> bookingsToAdd = new List<RoomBooking>();

                    // Overall results for time conflict detection
                    bool overallTimeConflictFound = false;
                    List<RoomBooking> overallTimeConflictedBookings = new List<RoomBooking>();

                    // Overall results for violating total booking time for a Room
                    bool overallRoomTimeViolationFound = false;
                    List<IDictionary<string, string>> overallRoomHours = new List<IDictionary<string, string>>();

                    // Overall results for violating total booking time for an Area
                    bool overallAreaTimeViolationFound = false;
                    List<IDictionary<string, string>> overallAreaHours = new List<IDictionary<string, string>>();
                    HashSet<int> areaIDsBooked = new HashSet<int>();

                    // Overall results for violating max time per single Booking
                    bool overallSingleBookingLengthViolationFound = false;
                    List<IDictionary<string, string>> overallSingleBookingHours = new List<IDictionary<string, string>>();

                    // Overall results for violating max number of separate Bookings for an Area
                    bool overallAreaBookingNumberViolationFound = false;
                    List<IDictionary<string, string>> overallAreaBookingCounts = new List<IDictionary<string, string>>();

                    // Make a Bookings for each Room selected
                    foreach (string roomID in selectedOptions)
                    {
                        // Get User's pre-existing booked time in this Room
                        var thisRoom = _context.Rooms.Where(r => r.ID == Convert.ToInt32(roomID)).FirstOrDefault();
                        var existingBookingsForThisRoom = _context.RoomBookings
                            .Where(b => (b.UserID == roomBooking.UserID) && (b.RoomID == Convert.ToInt32(roomID)) && (b.StartDate >= DateTime.Now));

                        TimeSpan existingTotalTimeInThisRoom = TimeSpan.Zero;
                        TimeSpan timeAddedByNewBookings = TimeSpan.Zero;

                        // Tally up the User's existing time
                        foreach(var existingBookingForThisRoom in existingBookingsForThisRoom)
                        {
                            TimeSpan duration = existingBookingForThisRoom.EndDate - existingBookingForThisRoom.StartDate;
                            existingTotalTimeInThisRoom += duration;
                        }

                        // Store this Area ID so we know to check it afterwards
                        areaIDsBooked.Add(thisRoom.RoomGroupID);

                        // We will check how their existing time totals compare after generating their new bookings

                        if (chkRepeat == "on") // Check if we should make repeat Bookings, "on" means we make repeat bookings
                        {
                            // Stores values of this room for time conflict detection
                            bool timeConflictFoundForThisRoom;
                            List<RoomBooking> timeConflictedBookingsForThisRoom;

                            // Stores result for max single booking time violation
                            bool singleBookingLengthViolationFoundForThisRoom;
                            List<IDictionary<string, string>> singleBookingFeedbackForThisRoom;

                            // Generate bookings to add for this room
                            List<RoomBooking> bookingsForThisRoom = GenerateRepeatBookings(roomBooking.SpecialNotes, roomBooking.UserID, Convert.ToInt32(roomID),
                                    roomBooking.StartDate, roomBooking.EndDate, Convert.ToDateTime(RepeatEndDate), Convert.ToInt32(RepeatInterval), RepeatType,
                                    Monday == "on", Tuesday == "on", Wednesday == "on", Thursday == "on", Friday == "on", Saturday == "on", Sunday == "on",
                                    out timeConflictFoundForThisRoom, out timeConflictedBookingsForThisRoom, out timeAddedByNewBookings,
                                    out singleBookingLengthViolationFoundForThisRoom, out singleBookingFeedbackForThisRoom);


                            if (timeConflictFoundForThisRoom == true)
                            {
                                overallTimeConflictFound = true;    // ONLY flag the overall result if a time conflict is found
                            }
                            overallTimeConflictedBookings.AddRange(timeConflictedBookingsForThisRoom); // ALWAYS track conflicts so far, in case there was a conflict in a different room

                            if (singleBookingLengthViolationFoundForThisRoom == true)
                            {
                                overallSingleBookingLengthViolationFound = true;    // ONLY flag the overall result if a violation of single booking length is found
                            }
                            overallSingleBookingHours.AddRange(singleBookingFeedbackForThisRoom); // ALWAYS track conflicts so far, in case there was a violation in a different room

                            bookingsToAdd.AddRange(bookingsForThisRoom);   // Append generated bookings to the list of bookings to add to _context at the end

                            // Wait until the end to add bookings to _context
                            // Wait until the end to save changes to _context
                        }
                        else //The user has not asked to make repeat bookings, so make only 1 booking
                        {
                            // Stores results of this room for time conflict detection
                            RoomBooking timeConflictedBookingForThisRoom;

                            // Make a single Booking for each Room selected
                            RoomBooking bookingForThisRoom = new RoomBooking()
                            {
                                UserID = roomBooking.UserID,
                                SpecialNotes = roomBooking.SpecialNotes,
                                StartDate = roomBooking.StartDate,
                                EndDate = roomBooking.EndDate,
                                RoomID = Convert.ToInt32(roomID),
                                Room = _context.Rooms.Where(r => r.ID == Convert.ToInt32(roomID)).FirstOrDefault()
                            };

                            //TimeSpan duration = booking.EndDate - booking.StartDate;  // Sample code for skipping past conflicts
                            //while (!RoomIsAvailable(booking, out RoomBooking conflict))
                            //{
                            //    booking.StartDate = conflict.EndDate.AddSeconds(1);
                            //    booking.EndDate = booking.StartDate + duration;
                            //}

                            if (!RoomIsAvailable(bookingForThisRoom, out timeConflictedBookingForThisRoom))
                            {
                                overallTimeConflictFound = true;    // ONLY flag the overall result if a time conflict is found
                            }
                            overallTimeConflictedBookings.Add(timeConflictedBookingForThisRoom); // ALWAYS track conflicts so far, in case there was a conflict in a different room

                            // After generating the new bookings for this room, we compare it to the User's previously existing time
                            timeAddedByNewBookings = bookingForThisRoom.EndDate - bookingForThisRoom.StartDate;

                            // Check if the length for a single Booking was exceeded
                            var thisArea = _context.RoomGroups.Where(r => r.ID == bookingForThisRoom.Room.RoomGroupID).FirstOrDefault();
                            if (thisArea.MaxHoursPerSingleBooking != null)
                            {
                                if ((bookingForThisRoom.EndDate - bookingForThisRoom.StartDate).TotalHours > thisArea.MaxHoursPerSingleBooking)
                                {
                                    overallSingleBookingLengthViolationFound = true;   // ONLY flag the overall result if a booking time exceeds max allowed hours for this area
                                }
                            }
                            // ALWAYS track time so far, in case there was an exception in a different room
                            IDictionary<string, string> timeResultForThisBooking = new Dictionary<string, string>();
                            timeResultForThisBooking.Add("RoomName", bookingForThisRoom.Room.RoomName);
                            timeResultForThisBooking.Add("MaxHoursSingleBooking", thisArea.MaxHoursPerSingleBooking.ToString());
                            timeResultForThisBooking.Add("BookingDate", bookingForThisRoom.StartDate.ToShortDateString());
                            timeResultForThisBooking.Add("BookingTime", bookingForThisRoom.StartDate.ToShortTimeString() + " - " + bookingForThisRoom.EndDate.ToShortTimeString());
                            timeResultForThisBooking.Add("HoursBooked", (bookingForThisRoom.EndDate - bookingForThisRoom.StartDate).TotalHours.ToString());
                            overallSingleBookingHours.Add(timeResultForThisBooking);

                            bookingsToAdd.Add(bookingForThisRoom);   // Append generated bookings to the list of bookings to add to _context at the end

                            // Wait until the end to add bookings to _context
                            // Wait until the end to save changes to _context
                        }

                        // After generating new bookings for this room, we check the new time that was added
                        if (thisRoom.RoomMaxHoursTotal != null)
                        {
                            if ((existingTotalTimeInThisRoom + timeAddedByNewBookings).TotalHours > thisRoom.RoomMaxHoursTotal)
                            {
                                overallRoomTimeViolationFound = true;   // ONLY flag the overall result if a room time exceeds max allowed hours for this room
                            }
                        }
                        // ALWAYS track time so far, in case there was an exception in a different room
                        IDictionary<string, string> timeResultForThisRoom = new Dictionary<string, string>();
                        timeResultForThisRoom.Add("RoomName", thisRoom.RoomName);
                        timeResultForThisRoom.Add("MaxHoursForRoom", thisRoom.RoomMaxHoursTotal.ToString());
                        timeResultForThisRoom.Add("ExistingHoursForRoom", existingTotalTimeInThisRoom.TotalHours.ToString());
                        timeResultForThisRoom.Add("NewHoursForRoom", timeAddedByNewBookings.TotalHours.ToString());
                        timeResultForThisRoom.Add("NewTotalHoursForRoom", (existingTotalTimeInThisRoom + timeAddedByNewBookings).TotalHours.ToString());
                        overallRoomHours.Add(timeResultForThisRoom);
                    }

                    // We can check if the Area's hours were exceeded after each Room booking is done generating
                    foreach (int areaID in areaIDsBooked)
                    {
                        // Get User's pre-existing booked time in this Area
                        var thisArea = _context.RoomGroups.Where(r => r.ID == areaID).FirstOrDefault();
                        var existingBookingsForThisArea = _context.RoomBookings.Include(b => b.Room)
                            .Where(b => (b.UserID == roomBooking.UserID) && (b.Room.RoomGroupID == areaID) && (b.StartDate >= DateTime.Now));

                        TimeSpan existingTotalTimeInThisArea = TimeSpan.Zero;
                        TimeSpan timeAddedByNewBookings = TimeSpan.Zero;

                        // Tally up the User's existing time
                        foreach (var existingBookingForThisArea in existingBookingsForThisArea)
                        {
                            TimeSpan duration = existingBookingForThisArea.EndDate - existingBookingForThisArea.StartDate;
                            existingTotalTimeInThisArea += duration;
                        }

                        // Get User's new hours from new bookings
                        var newBookingsInThisArea = bookingsToAdd.Where(b => (b.Room.RoomGroupID == areaID) && (b.StartDate >= DateTime.Now));

                        // Tally up the User's new time
                        foreach (var newBookingInThisArea in newBookingsInThisArea)
                        {
                            TimeSpan duration = newBookingInThisArea.EndDate - newBookingInThisArea.StartDate;
                            timeAddedByNewBookings += duration;
                        }

                        // Check if the time was exceeded
                        if (thisArea.MaxHoursTotal != null)
                        {
                            if ((existingTotalTimeInThisArea + timeAddedByNewBookings).TotalHours > thisArea.MaxHoursTotal)
                            {
                                overallAreaTimeViolationFound = true;   // ONLY flag the overall result if a area time exceeds max allowed hours for this area
                            }
                        }
                        // ALWAYS track time so far, in case there was an exception in a different area
                        IDictionary<string, string> timeResultForThisArea = new Dictionary<string, string>();
                        timeResultForThisArea.Add("AreaName", thisArea.AreaName);
                        timeResultForThisArea.Add("MaxHoursForArea", thisArea.MaxHoursTotal.ToString());
                        timeResultForThisArea.Add("ExistingHoursForArea", existingTotalTimeInThisArea.TotalHours.ToString());
                        timeResultForThisArea.Add("NewHoursForArea", timeAddedByNewBookings.TotalHours.ToString());
                        timeResultForThisArea.Add("NewTotalHoursForArea", (existingTotalTimeInThisArea + timeAddedByNewBookings).TotalHours.ToString());
                        overallAreaHours.Add(timeResultForThisArea);

                        // We can also tally how many bookings there are to see if we exceeded the max amount of allowed separate bookings
                        if (thisArea.MaxNumberOfBookings != null)
                        {
                            if ((existingBookingsForThisArea.Count() + newBookingsInThisArea.Count()) > thisArea.MaxNumberOfBookings)
                            {
                                overallAreaBookingNumberViolationFound = true;   // ONLY flag the overall result if a number of bookings exceeds max allowed for this area
                            }
                        }
                        // ALWAYS track count information so far, in case there was an exception in a different area
                        IDictionary<string, string> countResultForThisArea = new Dictionary<string, string>();
                        countResultForThisArea.Add("AreaName", thisArea.AreaName);
                        countResultForThisArea.Add("MaxBookingsForArea", thisArea.MaxNumberOfBookings.ToString());
                        countResultForThisArea.Add("ExistingBookingsForArea", existingBookingsForThisArea.Count().ToString());
                        countResultForThisArea.Add("NewBookingsForArea", newBookingsInThisArea.Count().ToString());
                        countResultForThisArea.Add("NewTotalBookingsForArea", (existingBookingsForThisArea.Count() + newBookingsInThisArea.Count()).ToString());
                        overallAreaBookingCounts.Add(countResultForThisArea);
                    }

                    // ONLY store feedback in TempData if a violation occurred
                    if (overallTimeConflictFound == true)
                    {
                        // Send feedback to User if any time conflicts detected overall
                        TempData["YourBookings"] = bookingsToAdd;
                        TempData["TimeConflictedBookings"] = overallTimeConflictedBookings;

                        throw new DbUpdateException("Booking time conflict violation.");    // Break before bookings are added or saved
                    }
                    if (overallRoomTimeViolationFound == true)
                    {
                        // Send feedback to User if their new bookings violate the time total for a specific room
                        TempData["RoomHoursViolation"] = overallRoomHours;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallAreaTimeViolationFound == true)
                    {
                        // Send feedback to User if their new bookings violate the time total for an area
                        TempData["AreaHoursViolation"] = overallAreaHours;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallSingleBookingLengthViolationFound == true)
                    {
                        // Send feedback to User if their new bookings violate the maximum time for a single booking
                        TempData["SingleBookingLengthViolation"] = overallSingleBookingHours;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallAreaBookingNumberViolationFound == true)
                    {
                        // Send feedback to User if their new bookings violate the maximum number of bookings allowed in this area
                        TempData["AreaBookingCountViolation"] = overallAreaBookingCounts;

                        throw new DbUpdateException("Total Booking count violation.");    // Break before bookings are added or saved
                    }

                    //_context.Add(roomBooking);    // DO NOT ADD "roomBooking" variable. "roomBooking" variable is ONLY used to validate model state
                    _context.RoomBookings.AddRange(bookingsToAdd);  // Add bookings to _context
                    await _context.SaveChangesAsync();              // Save changes to _context
                    TempData["Message"] = "Booking was created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message == "Booking time conflict violation.")
                {
                    // We handle this violation by storing feedback into TempData earlier, so don't need to do anything here
                }
                else if (dex.GetBaseException().Message == "Booking time total violation.")
                {
                    // We handle this violation by storing feedback into TempData earlier, so don't need to do anything here
                }
                else if (dex.GetBaseException().Message == "Total Booking count violation.")
                {
                    // We handle this violation by storing feedback into TempData earlier, so don't need to do anything here
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            ViewData["RoomGroupID"] = PermittedRoomGroupSelectList(RoomGroupID); // Reload room data separately from other dropdownlists, since it is connected to a multiselect
            PopulateSelectedRoomData(RoomGroupID, selectedOptions);
            ViewData["RoomID"] = RoomSelectList(RoomGroupID, RoomID);
            ViewData["RepeatType"] = RepeatTypeSelectList(RepeatType);
            PopulateDropDownLists();
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            // Disable User selection field for non-Admins
            if (User.IsInRole("Top-level Admin") || User.IsInRole("Admin"))
            {
                ViewData["UserIdDisabled"] = false;
            }
            else
            {
                ViewData["UserIdDisabled"] = true;
            }

            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User"))
            {
                if (User.Identity.Name != roomBooking.User.Username)
                {
                    TempData["Message"] = "You are not authorized to edit another User's Bookings.";
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }

            ViewData["RoomGroupID"] = PermittedRoomGroupSelectList(roomBooking.Room.RoomGroupID);
            ViewData["RoomID"] = RoomSelectList(roomBooking.Room.RoomGroupID, roomBooking.RoomID);
            PopulateDropDownLists(roomBooking);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int RoomGroupID, DateTime StartDate, DateTime EndDate)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            // Disable User selection field for non-Admins
            if (User.IsInRole("Top-level Admin") || User.IsInRole("Admin"))
            {
                ViewData["UserIdDisabled"] = false;
            }
            else
            {
                ViewData["UserIdDisabled"] = true;
            }

            // Check that Start Time is not in the past
            if (StartDate < DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Start Date can not be in the past.");
            }

            // Get the RoomBooking to update
            var roomBookingToUpdate = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (id != roomBookingToUpdate.ID)
            {
                return NotFound();
            }

            if (User.IsInRole("User"))
            {
                if (User.Identity.Name != roomBookingToUpdate.User.Username)
                {
                    TempData["Message"] = "You are not authorized to edit another User's Bookings.";
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }

            // Try updating it with the values posted
            if (await TryUpdateModelAsync<RoomBooking>(roomBookingToUpdate, "",
                p => p.SpecialNotes, p => p.StartDate, p => p.EndDate, p => p.RoomID, p => p.UserID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Booking was edited successfully!";
                    return RedirectToAction("Details", new { roomBookingToUpdate.ID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomBookingExists(roomBookingToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            ViewData["RoomGroupID"] = PermittedRoomGroupSelectList(RoomGroupID);
            ViewData["RoomID"] = RoomSelectList(RoomGroupID, roomBookingToUpdate.RoomID);
            PopulateDropDownLists(roomBookingToUpdate);
            return View(roomBookingToUpdate);
        }

        // GET: RoomBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room).ThenInclude(r => r.RoomGroup)
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            if (User.IsInRole("User"))
            {
                if (User.Identity.Name != roomBooking.User.Username)
                {
                    TempData["Message"] = "You are not authorized to delete another User's Bookings.";
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }

            ViewData["RoomGroupID"] = RoomGroupSelectList(roomBooking.Room.RoomGroupID);
            ViewData["RoomID"] = RoomSelectList(roomBooking.Room.RoomGroupID, roomBooking.RoomID);
            PopulateDropDownLists(roomBooking);
            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room).ThenInclude(r => r.RoomGroup)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (User.IsInRole("User"))
            {
                if (User.Identity.Name != roomBooking.User.Username)
                {
                    TempData["Message"] = "You are not authorized to delete another User's Bookings.";
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }

            try
            {
                _context.RoomBookings.Remove(roomBooking);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Booking was deleted successfully!";
                return RedirectToAction(nameof(Index));
            }

            catch (DbUpdateException)
            {
                //Note: there is really no reason a delete should fail if you can "talk" to the database.
                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
            }
            ViewData["RoomGroupID"] = RoomGroupSelectList(roomBooking.Room.RoomGroupID);
            ViewData["RoomID"] = RoomSelectList(roomBooking.Room.RoomGroupID, roomBooking.RoomID);
            PopulateDropDownLists(roomBooking);
            return View(roomBooking);
        }

        [HttpGet]
        public JsonResult GetRooms(int? ID)
        {
            return Json(RoomSelectList(ID, null));
        }

        private bool RoomBookingExists(int id)
        {
            return _context.RoomBookings.Any(e => e.ID == id);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        /// <returns></returns>
        /// <summary>
        /// Generates a List of Room Bookings, repeated from the StartDate until the RepeatEndDate, skipping weeks or days according to the "Interval".
        /// Will only book days of the week that are "Included".
        /// </summary>
        /// <param name="timeConflictedBookings">Output list of existing Bookings that are conflicted by times of new Bookings.</param>
        /// <returns>Generated list of Room Bookings to add.</returns>
        private List<RoomBooking> GenerateRepeatBookings(string SpecialNotes, int UserID, int RoomID,
            DateTime StartDate, DateTime EndDate, DateTime RepeatEndDate, int RepeatInterval, string RepeatType,
            bool IncludeMonday, bool IncludeTuesday, bool IncludeWednesday, bool IncludeThursday, bool IncludeFriday, bool IncludeSaturday, bool IncludeSunday,
            out bool timeConflictFound, out List<RoomBooking> timeConflictedBookings, out TimeSpan timeAddedByGeneratedBookings,
            out bool singleBookingViolationFound, out List<IDictionary<string, string>> singleBookingFeedback)
        {
            // Storage variables to give feedback for validation purposes
            bool timeConflictsDetected = false;
            List<RoomBooking> existingConflictedBookings = new List<RoomBooking>();
            TimeSpan totalTimeOfGeneratedBookings = TimeSpan.Zero;

            bool singleBookingViolationDetected = false;
            List<IDictionary<string, string>> singleBookingLengthFeedback = new List<IDictionary<string, string>>();

            List<RoomBooking> bookingsToAdd = new List<RoomBooking>();  //Main list of generated bookings to return

            TimeSpan duration = EndDate - StartDate;
            for (DateTime day = StartDate; day.Date <= RepeatEndDate; day = day.AddDays((RepeatType == "Days") ? RepeatInterval : 1))
            {
                totalTimeOfGeneratedBookings += duration;

                // Check that this day of the week is checked On before adding booking
                if ((day.DayOfWeek == DayOfWeek.Monday && IncludeMonday)
                    || (day.DayOfWeek == DayOfWeek.Tuesday && IncludeTuesday)
                    || (day.DayOfWeek == DayOfWeek.Wednesday && IncludeWednesday)
                    || (day.DayOfWeek == DayOfWeek.Thursday && IncludeThursday)
                    || (day.DayOfWeek == DayOfWeek.Friday && IncludeFriday)
                    || (day.DayOfWeek == DayOfWeek.Saturday && IncludeSaturday)
                    || (day.DayOfWeek == DayOfWeek.Sunday && IncludeSunday))
                {
                    RoomBooking newBooking = new RoomBooking
                    {
                        UserID = UserID,
                        SpecialNotes = SpecialNotes,
                        StartDate = day,
                        EndDate = day + duration,
                        RoomID = RoomID,
                        Room = _context.Rooms.Where(r => r.ID == RoomID).FirstOrDefault()
                    };

                    // Sample code for skipping past conflicts
                    //while (!RoomIsAvailable(booking, out RoomBooking conflict))
                    //{
                    //    booking.StartDate = conflict.EndDate.AddSeconds(1);
                    //    booking.EndDate = booking.StartDate + duration;
                    //}

                    if (!RoomIsAvailable(newBooking, out RoomBooking conflict))
                    {
                        timeConflictsDetected = true;
                        existingConflictedBookings.Add(conflict);
                    }
                    else
                    {
                        // Add blanks when there is no conflict, so User can know if some of their Bookings did not conflict while others did conflict
                        existingConflictedBookings.Add(null);
                    }

                    // Check if the length for a single Booking was exceeded
                    var thisArea = _context.RoomGroups.Where(r => r.ID == newBooking.Room.RoomGroupID).FirstOrDefault();
                    if (thisArea.MaxHoursPerSingleBooking != null)
                    {
                        if (duration.TotalHours > thisArea.MaxHoursPerSingleBooking)
                        {
                            singleBookingViolationDetected = true;   // ONLY flag the overall result if a booking time exceeds max allowed hours for this area
                        }
                    }
                    // ALWAYS track time so far, in case there was an exception in a different room
                    IDictionary<string, string> timeResultForThisBooking = new Dictionary<string, string>();
                    timeResultForThisBooking.Add("RoomName", newBooking.Room.RoomName);
                    timeResultForThisBooking.Add("MaxHoursSingleBooking", thisArea.MaxHoursPerSingleBooking.ToString());
                    timeResultForThisBooking.Add("BookingDate", newBooking.StartDate.ToShortDateString());
                    timeResultForThisBooking.Add("BookingTime", newBooking.StartDate.ToShortTimeString() + " - " + newBooking.EndDate.ToShortTimeString());
                    timeResultForThisBooking.Add("HoursBooked", duration.TotalHours.ToString());
                    singleBookingLengthFeedback.Add(timeResultForThisBooking);

                    bookingsToAdd.Add(newBooking);
                }

                if ((RepeatType == "Weeks")
                    && (day.DayOfWeek == DayOfWeek.Saturday))
                    day = day.AddDays(7 * (RepeatInterval - 1));   // Add days according to week interval at the end of the week
            }

            // Return violation results:
            // -Time conflict violations
            timeConflictFound = timeConflictsDetected;
            timeConflictedBookings = existingConflictedBookings;
            // -Total time of new bookings
            timeAddedByGeneratedBookings = totalTimeOfGeneratedBookings;
            // -Hour check for length of single bookings
            singleBookingViolationFound = singleBookingViolationDetected;
            singleBookingFeedback = singleBookingLengthFeedback;

            return bookingsToAdd;   // Return list of generated bookings
        }

        private void PopulateSelectedRoomData(int? RoomGroupID = null, string[] selectedOptions = null)
        {
            // Get all Rooms within this area
            var allOptions = _context.Rooms.Where(r => r.RoomGroupID == RoomGroupID.GetValueOrDefault() && r.Enabled == true);

            // Remember which options were selected so we can re-select them
            int[] currentOptionsHS = new int[0];
            if (selectedOptions != null)    // Check each condition of selectedOptions one-at-a-time to avoid null and out-of-bounds errors
            {
                if (selectedOptions.Count() > 0)
                {
                    // Test that the first selected option can be converted to Int before converting the rest
                    int t;
                    if (Int32.TryParse(selectedOptions[0], out t))
                    {
                        // t has been successfully set with the converted integer
                        // Convert the rest of the array here
                        currentOptionsHS = Array.ConvertAll(selectedOptions, s => int.Parse(s));
                    }
                }
            }

            // Make two lists. One of selected options, and one of available options
            var selected = new List<ListOptionVM>();
            var available = new List<ListOptionVM>();
            foreach (var s in allOptions)
            {
                if (currentOptionsHS.Contains(s.ID))
                {
                    selected.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.RoomName
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.RoomName
                    });
                }
            }

            // Default text if there is no data found
            if (allOptions.Count() == 0)
            {
                available.Add(new ListOptionVM
                {
                    ID = 0,
                    DisplayText = "There are no Rooms available in this Area."
                });
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.DisplayText), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.DisplayText), "ID", "DisplayText");
        }

        //This is a twist on the PopulateDropDownLists approach
        //  Create methods that return each SelectList separately
        //  and one method to put them all into ViewData.
        //This approach allows for AJAX requests to refresh
        //DDL Data at a later date.
        private SelectList RepeatTypeSelectList(string selectedId = null)
        {
            List<SelectListItem> repeatTypes = new List<SelectListItem>();
            repeatTypes.Add(new SelectListItem() { Text = "Days", Value = "Days" });
            repeatTypes.Add(new SelectListItem() { Text = "Weeks", Value = "Weeks" });

            return new SelectList(repeatTypes, "Value", "Text", selectedId);
        }
        private SelectList RoomGroupSelectList(int? selectedId = null)
        {
            return new SelectList(_context.RoomGroups
                .Where(d => d.Enabled == true)
                .OrderBy(d => d.AreaName), "ID", "AreaName", selectedId);
        }
        private SelectList PermittedRoomGroupSelectList(int? selectedId = null)
        {
            string loggedInUsername = User.Identity.Name;   // Get logged-in User's name

            User loggedInUser = _context.Users.Where(u => u.Username == loggedInUsername).FirstOrDefault(); // Pull the User object by the logged-in User's name

            // Get logged-in User's User Group
            int loggedInUserGroupID = _context.TermAndPrograms.Where(t => t.ID == loggedInUser.TermAndProgramID).Select(t => t.UserGroupID).FirstOrDefault();

            bool loggedInUserIsAdmin = User.IsInRole("Top-level Admin") || User.IsInRole("Admin");  // Determine whether logged-in User as an Admin

            return new SelectList(_context.RoomGroups
                .Include(r => r.RoomUserGroupPermissions)
                .Where(r => (r.Enabled == true) /* Only show Enabled Areas */
                         && (r.RoomUserGroupPermissions.Where(p => p.UserGroupID == loggedInUserGroupID).Count() > 0 /* Don't show Areas that a permission for the logged-in User's Group doesn't exist */
                          || loggedInUserIsAdmin /* Show Areas regardless of permission if the User is an Admin */))
                .OrderBy(r => r.AreaName), "ID", "AreaName", selectedId);
        }
        private SelectList RoomSelectList(int? RoomGroupID = null, int? selectedId = null)
        {
            var query = from c in _context.Rooms.Include(c => c.RoomGroup)
                        where c.RoomGroupID == RoomGroupID.GetValueOrDefault() && c.Enabled == true
                        select c;
            return new SelectList(query.OrderBy(p => p.RoomName), "ID", "RoomName", selectedId);
        }
        private SelectList UserSelectList(int? selectedId)
        {
            return new SelectList(_context.Users
                .OrderBy(u => u.Username), "ID", "FullName", selectedId);
        }

        private void PopulateDropDownLists(RoomBooking roomBooking = null)
        {
            ViewData["UserID"] = UserSelectList(roomBooking?.UserID);
        }

        
        // Get a list of all rooms and return them as a JSON array
        public void GetRoomsJSON()
        {
            var rooms = from r in _context.Rooms
                        .Include(r => r.RoomGroup)
                               select r;

            List<RoomJSON> roomList = new List<RoomJSON>();

            foreach (Room r in rooms)
            {
                roomList.Add(new RoomJSON { id = r.ID, building = r.RoomGroup.AreaName, title = r.RoomName });
            }

            ViewData["RoomList"] = JsonConvert.SerializeObject(roomList);
        }

        // Get a list of all bookings and return them as a JSON array
        public void GetBookingsJSON()
        {
            var bookings = from r in _context.RoomBookings
                        .Include(r => r.Room)
                        .Include(r => r.User)
                        select r;

            List<BookingJSON> bookingList = new List<BookingJSON>();

            foreach (RoomBooking r in bookings)
            {
                bookingList.Add(new BookingJSON { id = r.ID, resourceId = r.RoomID, title = r.User.FullName, start = r.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"), end = r.EndDate.ToString("yyyy-MM-ddTHH:mm:ss") });
            }

            ViewData["BookingList"] = JsonConvert.SerializeObject(bookingList);
        }

        /// <summary>
        /// Checks if a new Booking has a time conflict with an existing Booking.
        /// </summary>
        /// <param name="newBooking">New Booking to compare to existing Bookings and check for time conflicts.</param>
        /// <param name="conflictBooking">Output the existing Booking that conflicts with the new Booking, if a conflict exists.</param>
        /// <returns>True if there is no time conflict. False if there is a time conflict.</returns>
        public bool RoomIsAvailable(RoomBooking newBooking, out RoomBooking conflictBooking)
        {
            List<RoomBooking> existingBookings =
                new List<RoomBooking>(_context.RoomBookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => b.RoomID == newBooking.RoomID));

            foreach (RoomBooking existingBooking in existingBookings)
            {
                if ((newBooking.StartDate >= existingBooking.StartDate && newBooking.StartDate < existingBooking.EndDate) /* Start of new Booking is within an existing Booking */
                 || (newBooking.EndDate > existingBooking.StartDate && newBooking.EndDate <= existingBooking.EndDate) /* End of a new Booking is within an existing Booking */
                 || (newBooking.StartDate <= existingBooking.StartDate && newBooking.EndDate >= existingBooking.EndDate)) /* New Booking occurs on top of an existing Booking */
                {
                    conflictBooking = existingBooking;
                    return false;
                }
            }
            conflictBooking = null;
            return true;
        }

    }
}
