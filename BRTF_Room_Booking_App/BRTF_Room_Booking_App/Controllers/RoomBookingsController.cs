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
using Microsoft.AspNetCore.Identity;

namespace BRTF_Room_Booking_App.Controllers
{
    [Authorize(Roles = "Top-level Admin,Admin,User")]
    public class RoomBookingsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;
        private readonly ApplicationDbContext _identityContext;
        private readonly UserManager<IdentityUser> _userManager;

        public RoomBookingsController(BTRFRoomBookingContext context, ApplicationDbContext identityContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _userManager = userManager;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index(int? page, int? pageSizeID, /* Paging */
            int? RoomGroupID, int? RoomID, string SearchAfterDate, string SearchBeforeDate, string SearchUsername, string SearchFullName, /* Filters/Search */
            string actionButton, string sortDirection = "asc", string sortField = "Start Date" /*Sorting*/)
        {
            //Toggle the open/closed state of the collapse depending on if something is being filtered
            ViewData["Filtering"] = ""; //Assume nothing is filtered

            //Change colour of the button when filtering by setting this default
            ViewData["Filter"] = "btn-outline-secondary";

            //NOTE: make sure this array has matching values to the column headings
            string[] sortOptions = new[] { "Start Date" };

            ViewData["RoomGroupID"] = RoomGroupSelectList(RoomGroupID);    // Room data is loaded separately from other dropdownlists, since it is sometimes connected to a multiselect
            ViewData["RoomID"] = RoomSelectList(RoomGroupID, RoomID);

            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var roombookings = from r in _context.RoomBookings
                               .Include(r => r.Room)
                               .Include(r => r.User)
                               select r;

            bool filtered = false;

            //Add as many filters as needed
            if (!String.IsNullOrEmpty(SearchAfterDate) && DateTime.TryParse(SearchAfterDate, out DateTime afterDate))
            {
                roombookings = roombookings.Where(r => afterDate <= r.StartDate);
                
                ViewData["Filtering"] = "show";
                ViewData["Filter"] = "btn-danger";
                filtered = true;
            }
            if (!String.IsNullOrEmpty(SearchBeforeDate) && DateTime.TryParse(SearchBeforeDate, out DateTime beforeDate))
            {
                roombookings = roombookings.Where(r => r.StartDate <= beforeDate);
                ViewData["Filtering"] = "show";
                ViewData["Filter"] = "btn-danger";
                filtered = true;
            }
            if (!String.IsNullOrEmpty(SearchUsername))
            {
                roombookings = roombookings.Where(r => r.User.Username.ToUpper().Contains(SearchUsername.ToUpper()));
                ViewData["Filtering"] = "show";
                ViewData["Filter"] = "btn-danger";
                filtered = true;
            }
            if (!String.IsNullOrEmpty(SearchFullName))
            {
                roombookings = roombookings.Where(r => (r.User.FirstName + " " + r.User.LastName).ToUpper().Contains(SearchFullName.ToUpper())
                                                    || (r.User.FirstName + " " + r.User.MiddleName + " " + r.User.LastName).ToUpper().Contains(SearchFullName.ToUpper()));
                ViewData["Filtering"] = "show";
                ViewData["Filter"] = "btn-danger";
                filtered = true;
            }
            if (RoomGroupID.HasValue)
            {
                roombookings = roombookings.Where(r => r.Room.RoomGroupID == RoomGroupID);
                ViewData["Filtering"] = " show ";
                ViewData["Filter"] = "btn-danger";
                filtered = true;
            }
            if (RoomID.HasValue)
            {
                roombookings = roombookings.Where(r => r.RoomID == RoomID);
                ViewData["Filtering"] = " show ";
                ViewData["Filter"] = "btn-danger";
                filtered = true;
            }
            GetRoomsJSON(roombookings, filtered);
            GetBookingsJSON(roombookings);

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
            // IMPORTANT NOTE: "roomBooking" variable is ONLY used to validate model state. DO NOT ADD "roomBooking"

            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            // Get global settings
            var globalSettings = _context.GlobalSettings.FirstOrDefault();

            // Default all Repeat controls to Off
            ViewData["chkRepeat"] = "";
            ViewData["RepeatContainer"] = "";
            ViewData["RepeatInterval"] = "";
            ViewData["DaysOfWeekContainer"] = "";
            ViewData["Monday"] = "";
            ViewData["Tuesday"] = "";
            ViewData["Wednesday"] = "";
            ViewData["Thursday"] = "";
            ViewData["Friday"] = "";
            ViewData["Saturday"] = "";
            ViewData["Sunday"] = "";
            ViewData["RepeatEndDate"] = "";

            // Validate Repeat controls if checkbox is On
            if (chkRepeat == "on")
            {
                // Store values for Repeat controls so they will re-appear after postback
                ViewData["chkRepeat"] = "checked";
                ViewData["RepeatContainer"] = "show";
                ViewData["RepeatInterval"] = RepeatInterval;
                if (RepeatType == "Weeks") ViewData["DaysOfWeekContainer"] = "show";
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
                else if (RepeatInterval == "Weeks"
                    && (Monday != "on")
                    && (Tuesday != "on")
                    && (Wednesday != "on")
                    && (Thursday != "on")
                    && (Friday != "on")
                    && (Saturday != "on")
                    && (Sunday != "on"))
                {
                    ModelState.AddModelError("", "You must check \"On\" the days of the week upon which you want to repeat your booking.");
                }

                if (RepeatEndDate == null)
                {
                    ModelState.AddModelError("", "You must set a Repeat End Date.");
                }
                else if (Convert.ToDateTime(RepeatEndDate) <= roomBooking.StartDate)
                {
                    ModelState.AddModelError("", "Repeat End Date must be after Start Date.");
                }
                else if (Convert.ToDateTime(RepeatEndDate) > globalSettings.EndOfTermDate)
                {
                    ModelState.AddModelError("", "Repeat End Date must be before End of Term date: " + globalSettings.EndOfTermDate.ToShortDateString() + ".");
                }
                else if (Convert.ToDateTime(RepeatEndDate) > roomBooking.StartDate.AddDays(globalSettings.LatestAllowableFutureBookingDay))
                {
                    ModelState.AddModelError("", "You cannot Repeat a Booking more than " + globalSettings.LatestAllowableFutureBookingDay.ToString()
                        + " days in the future from your selected Start Date. Your selected Start Date is " + roomBooking.StartDate.ToShortDateString()
                        + ", so your latest allowable Repeat End Date is " + roomBooking.StartDate.AddDays(globalSettings.LatestAllowableFutureBookingDay).ToShortDateString() + ".");
                }
            }

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

            // Check that Start Time is not in the past
            if (roomBooking.StartDate < DateTime.Today)
            {
                ModelState.AddModelError("StartDate", "Start Time can not be in the past.");
            }
            if (roomBooking.StartDate < globalSettings.StartOfTermDate)
            {
                ModelState.AddModelError("StartDate", "Start Date can not be before the Start of Term date: " + globalSettings.StartOfTermDate.ToShortDateString() + ".");
            }
            if (roomBooking.EndDate > globalSettings.EndOfTermDate)
            {
                ModelState.AddModelError("EndDate", "End Date can not be after the End of Term date: " + globalSettings.EndOfTermDate.ToShortDateString() + ".");
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

            try
            {
                if (ModelState.IsValid)
                {
                    // Overall list of bookings to be added
                    List<RoomBooking> overallNewBookingsToAdd = new List<RoomBooking>(); // During loop, add generated bookings to this list, then add to _context after the loop

                    // Overall results for time conflict detection
                    bool overallIsTimeConflictFound = false;
                    List<RoomBooking> overallTimeConflictFeedback = new List<RoomBooking>();

                    // Overall results for violating total booking time for a Room
                    bool overallIsRoomTimeViolationFound = false;
                    List<IDictionary<string, string>> overallRoomTimeFeedback = new List<IDictionary<string, string>>();

                    // Overall results for violating total booking time for an Area
                    bool overallIsAreaTimeViolationFound = false;
                    List<IDictionary<string, string>> overallAreaTimeFeedback = new List<IDictionary<string, string>>();
                    HashSet<int> overallListOfAreaIDsBooked = new HashSet<int>();

                    // Overall results for violating max length per single Booking
                    bool overallIsSingleBookingLengthViolationFound = false;
                    List<IDictionary<string, string>> overallSingleBookingLengthFeedback = new List<IDictionary<string, string>>();

                    // Overall results for violating max number of separate Bookings for an Area
                    bool overallIsAreaNumberOfBookingsViolationFound = false;
                    List<IDictionary<string, string>> overallAreaNumberOfBookingsFeedback = new List<IDictionary<string, string>>();

                    // Overall results for violating blackout time
                    bool overallIsBlackoutViolationFound = false;
                    List<RoomBooking> overallBlackoutViolationFeedback = new List<RoomBooking>();
                    List<int> overallRoomBlackoutTimes = new List<int>();

                    // Make a Bookings for each Room selected
                    foreach (string roomID in selectedOptions)
                    {
                        // Get User's pre-existing booked time in this Room
                        var thisRoom = _context.Rooms.Include(r => r.RoomGroup).Where(r => r.ID == Convert.ToInt32(roomID)).FirstOrDefault();
                        var existingBookingsForThisRoom = _context.RoomBookings
                            .Where(b => (b.UserID == roomBooking.UserID) && (b.RoomID == Convert.ToInt32(roomID)) && (b.EndDate >= DateTime.Today));

                        // Tally up the User's existing time
                        TimeSpan existingTotalTimeInThisRoom = GetTotalDurationOfBookings(existingBookingsForThisRoom.ToList());

                        // Initialize storage variable for tallying the new time added by new bookings
                        TimeSpan timeAddedByNewBookings = TimeSpan.Zero;

                        // Store this Area ID so we know to check this area's rules afterwards
                        overallListOfAreaIDsBooked.Add(thisRoom.RoomGroupID);

                        // We will check their existing time totals and compare after generating their new bookings

                        if (chkRepeat == "on") // Check if we should generate repeat Bookings, "on" means we generate repeat bookings
                        {
                            // Stores values of this room for time conflict detection
                            bool isTimeConflictFoundForThisRoom;
                            List<RoomBooking> timeConflictFeedbackForThisRoom;

                            // Stores result for max single booking time violation
                            bool isSingleBookingLengthViolationFoundForThisRoom;
                            List<IDictionary<string, string>> singleBookingLengthFeedbackForThisRoom;

                            // Stores values of this room for blackout time detection
                            bool isBlackoutViolationFoundForThisRoom;
                            List<RoomBooking> blackoutViolationFeedbackForThisRoom;
                            List<int> blackoutTimesForThisRoom;

                            // Generate bookings to add for this room
                            List<RoomBooking> newBookingsForThisRoom = GenerateRepeatBookings(roomBooking.SpecialNotes, roomBooking.UserID, Convert.ToInt32(roomID),
                                    roomBooking.StartDate, roomBooking.EndDate, Convert.ToDateTime(RepeatEndDate), Convert.ToInt32(RepeatInterval), RepeatType,
                                    Monday == "on", Tuesday == "on", Wednesday == "on", Thursday == "on", Friday == "on", Saturday == "on", Sunday == "on",
                                    out isTimeConflictFoundForThisRoom, out timeConflictFeedbackForThisRoom, out timeAddedByNewBookings,
                                    out isSingleBookingLengthViolationFoundForThisRoom, out singleBookingLengthFeedbackForThisRoom,
                                    out isBlackoutViolationFoundForThisRoom, out blackoutViolationFeedbackForThisRoom, out blackoutTimesForThisRoom);

                            // Check for time conflicts
                            if (isTimeConflictFoundForThisRoom == true)
                            {
                                overallIsTimeConflictFound = true;    // ONLY flag the overall result if a time conflict is found
                            }
                            overallTimeConflictFeedback.AddRange(timeConflictFeedbackForThisRoom); // ALWAYS track conflict feedback so far, in case there was a conflict in a different room

                            // Check if the time length for a single Booking was exceeded
                            if (isSingleBookingLengthViolationFoundForThisRoom == true)
                            {
                                overallIsSingleBookingLengthViolationFound = true;    // ONLY flag the overall result if a violation for single booking length is found
                            }
                            overallSingleBookingLengthFeedback.AddRange(singleBookingLengthFeedbackForThisRoom); // ALWAYS track single booking length feedback so far, in case there was a violation in a different room

                            // Check if any booking violates an existing booking's blackout time
                            if (isBlackoutViolationFoundForThisRoom == true)
                            {
                                overallIsBlackoutViolationFound = true;    // ONLY flag the overall result if a blackout violation is found
                            }
                            overallBlackoutViolationFeedback.AddRange(blackoutViolationFeedbackForThisRoom); // ALWAYS track blackout feedback so far, in case there was a violation in a different room
                            overallRoomBlackoutTimes.AddRange(blackoutTimesForThisRoom);    // Record every blackout time so we can give feedback to the User

                            overallNewBookingsToAdd.AddRange(newBookingsForThisRoom);   // Append generated bookings to the list of bookings to add to _context at the end

                            // Wait until the end to add bookings to _context
                            // Wait until the end to save changes to _context
                        }
                        else //The user has not asked to generate repeat bookings, so make only generate 1 booking
                        {
                            // Stores results of this room for time conflict detection
                            RoomBooking timeConflictFeedbackForThisRoom;

                            // Make a single Booking for each Room selected
                            RoomBooking newBookingForThisRoom = new RoomBooking()
                            {
                                UserID = roomBooking.UserID,
                                SpecialNotes = roomBooking.SpecialNotes,
                                StartDate = roomBooking.StartDate,
                                EndDate = roomBooking.EndDate,
                                RoomID = Convert.ToInt32(roomID),
                                Room = _context.Rooms.Where(r => r.ID == Convert.ToInt32(roomID)).FirstOrDefault(),
                                ApprovalStatus = (thisRoom.RoomGroup.NeedsApproval ? "Pending" : "Approved")
                            };

                            timeAddedByNewBookings = newBookingForThisRoom.EndDate - newBookingForThisRoom.StartDate;   // Track how much time was added by this new Booking

                            // Check for time conflicts
                            if (!RoomIsAvailable(newBookingForThisRoom, out timeConflictFeedbackForThisRoom))
                            {
                                overallIsTimeConflictFound = true;    // ONLY flag the overall result if a time conflict is found
                            }
                            overallTimeConflictFeedback.Add(timeConflictFeedbackForThisRoom); // ALWAYS track conflict feedback so far, in case there was a conflict in a different room

                            // After generating the new bookings for this room, we compare it to the User's previously existing time

                            // Check if the time length for a single Booking was exceeded
                            var thisArea = _context.RoomGroups.Where(r => r.ID == newBookingForThisRoom.Room.RoomGroupID).FirstOrDefault();
                            if (thisArea.MaxHoursPerSingleBooking != null)
                            {
                                if ((newBookingForThisRoom.EndDate - newBookingForThisRoom.StartDate).TotalHours > thisArea.MaxHoursPerSingleBooking)
                                {
                                    overallIsSingleBookingLengthViolationFound = true;   // ONLY flag the overall result if a violation for single booking length is found
                                }
                            }
                            // ALWAYS track single booking length feedback so far, in case there was an exception in a different room
                            IDictionary<string, string> singleBookingLengthFeedbackForThisRoom = new Dictionary<string, string>();
                            singleBookingLengthFeedbackForThisRoom.Add("RoomName", newBookingForThisRoom.Room.RoomName);
                            singleBookingLengthFeedbackForThisRoom.Add("MaxHoursSingleBooking", thisArea.MaxHoursPerSingleBooking.ToString());
                            singleBookingLengthFeedbackForThisRoom.Add("BookingStartDate", newBookingForThisRoom.StartDate.ToShortDateString());
                            singleBookingLengthFeedbackForThisRoom.Add("BookingStartTime", newBookingForThisRoom.StartDate.ToShortTimeString());
                            singleBookingLengthFeedbackForThisRoom.Add("BookingEndDate", newBookingForThisRoom.EndDate.ToShortDateString());
                            singleBookingLengthFeedbackForThisRoom.Add("BookingEndTime", newBookingForThisRoom.EndDate.ToShortTimeString());
                            singleBookingLengthFeedbackForThisRoom.Add("HoursBooked", (newBookingForThisRoom.EndDate - newBookingForThisRoom.StartDate).TotalHours.ToString());
                            overallSingleBookingLengthFeedback.Add(singleBookingLengthFeedbackForThisRoom);

                            // Check if the booking violates an existing booking's blackout time
                            if (BookingViolatesBlackoutTime(newBookingForThisRoom, out RoomBooking blackoutFeedbackForThisRoom))
                            {
                                overallIsBlackoutViolationFound = true;    // ONLY flag the overall result if a blackout time violation is found
                                overallBlackoutViolationFeedback.Add(blackoutFeedbackForThisRoom); // ALWAYS track blackout feedback so far, in case there was a violation in a different room
                            }
                            else
                            {
                                overallBlackoutViolationFeedback.Add(null); // Add blanks when there is no violation, so User can know if some of their Bookings did not violate while others did violate
                            }
                            overallRoomBlackoutTimes.Add(thisArea.BlackoutTime);    // Record every blackout time so we can give feedback to the User


                            overallNewBookingsToAdd.Add(newBookingForThisRoom);   // Append generated bookings to the list of bookings to add to _context at the end

                            // Wait until the end to add bookings to _context
                            // Wait until the end to save changes to _context
                        }

                        // After generating new bookings for this room, we compare the new hour total to the maximum hours allowed for this room
                        if (thisRoom.RoomMaxHoursTotal != null)
                        {
                            if ((existingTotalTimeInThisRoom + timeAddedByNewBookings).TotalHours > thisRoom.RoomMaxHoursTotal)
                            {
                                overallIsRoomTimeViolationFound = true;   // ONLY flag the overall result if a room hour total exceeds max allowed hours for this room
                            }
                        }
                        // ALWAYS track room hour feedback so far, in case there was an exception in a different room
                        IDictionary<string, string> totalTimeFeedbackForThisRoom = new Dictionary<string, string>();
                        totalTimeFeedbackForThisRoom.Add("RoomName", thisRoom.RoomName);
                        totalTimeFeedbackForThisRoom.Add("MaxHoursForRoom", thisRoom.RoomMaxHoursTotal.ToString());
                        totalTimeFeedbackForThisRoom.Add("ExistingHoursForRoom", existingTotalTimeInThisRoom.TotalHours.ToString());
                        totalTimeFeedbackForThisRoom.Add("NewHoursForRoom", timeAddedByNewBookings.TotalHours.ToString());
                        totalTimeFeedbackForThisRoom.Add("NewTotalHoursForRoom", (existingTotalTimeInThisRoom + timeAddedByNewBookings).TotalHours.ToString());
                        overallRoomTimeFeedback.Add(totalTimeFeedbackForThisRoom);
                    }

                    // After every Room booking is done generating, we can check if the Area's max hours were exceeded 
                    foreach (int areaID in overallListOfAreaIDsBooked)
                    {
                        // Get User's pre-existing booked time in this Area
                        var thisArea = _context.RoomGroups.Where(r => r.ID == areaID).FirstOrDefault();
                        var existingBookingsForThisArea = _context.RoomBookings.Include(b => b.Room)
                            .Where(b => (b.UserID == roomBooking.UserID) && (b.Room.RoomGroupID == areaID) && (b.EndDate >= DateTime.Today));

                        // Tally User's existing booked time in this area
                        TimeSpan existingTotalTimeInThisArea = GetTotalDurationOfBookings(existingBookingsForThisArea.ToList());

                        // Get User's new hours from new bookings
                        var newBookingsInThisArea = overallNewBookingsToAdd.Where(b => (b.Room.RoomGroupID == areaID) && (b.EndDate >= DateTime.Today));

                        // Tally up the new time added by User's new bookings
                        TimeSpan timeAddedByNewBookings = GetTotalDurationOfBookings(newBookingsInThisArea.ToList());

                        // Check if the max Area time was exceeded
                        if (thisArea.MaxHoursTotal != null)
                        {
                            if ((existingTotalTimeInThisArea + timeAddedByNewBookings).TotalHours > thisArea.MaxHoursTotal)
                            {
                                overallIsAreaTimeViolationFound = true;   // ONLY flag the overall result if a area time exceeds max allowed hours for this area
                            }
                        }
                        // ALWAYS track Area time feedback so far, in case there was an exception in a different Area
                        IDictionary<string, string> totalTimeFeedbackForThisArea = new Dictionary<string, string>();
                        totalTimeFeedbackForThisArea.Add("AreaName", thisArea.AreaName);
                        totalTimeFeedbackForThisArea.Add("MaxHoursForArea", thisArea.MaxHoursTotal.ToString());
                        totalTimeFeedbackForThisArea.Add("ExistingHoursForArea", existingTotalTimeInThisArea.TotalHours.ToString());
                        totalTimeFeedbackForThisArea.Add("NewHoursForArea", timeAddedByNewBookings.TotalHours.ToString());
                        totalTimeFeedbackForThisArea.Add("NewTotalHoursForArea", (existingTotalTimeInThisArea + timeAddedByNewBookings).TotalHours.ToString());
                        overallAreaTimeFeedback.Add(totalTimeFeedbackForThisArea);

                        // We can also tally how many bookings there are, to see if we exceeded the max amount of allowed separate bookings in this area
                        if (thisArea.MaxNumberOfBookings != null)
                        {
                            if ((existingBookingsForThisArea.Count() + newBookingsInThisArea.Count()) > thisArea.MaxNumberOfBookings)
                            {
                                overallIsAreaNumberOfBookingsViolationFound = true;   // ONLY flag the overall result if the number of bookings exceeds the max allowed for this area
                            }
                        }
                        // ALWAYS track booking count feedback so far, in case there was an exception in a different area
                        IDictionary<string, string> numberOfBookingsFeedbackForThisArea = new Dictionary<string, string>();
                        numberOfBookingsFeedbackForThisArea.Add("AreaName", thisArea.AreaName);
                        numberOfBookingsFeedbackForThisArea.Add("MaxBookingsForArea", thisArea.MaxNumberOfBookings.ToString());
                        numberOfBookingsFeedbackForThisArea.Add("ExistingBookingsForArea", existingBookingsForThisArea.Count().ToString());
                        numberOfBookingsFeedbackForThisArea.Add("NewBookingsForArea", newBookingsInThisArea.Count().ToString());
                        numberOfBookingsFeedbackForThisArea.Add("NewTotalBookingsForArea", (existingBookingsForThisArea.Count() + newBookingsInThisArea.Count()).ToString());
                        overallAreaNumberOfBookingsFeedback.Add(numberOfBookingsFeedbackForThisArea);
                    }

                    // Get User's role so we only apply rule violations to non-admins
                    User bookingUser = _context.Users.Where(u => u.ID == roomBooking.UserID).FirstOrDefault();
                    var identityUser = _identityContext.Users.FirstOrDefault(p => p.UserName == bookingUser.Username);
                    string identityUserRole = _userManager.GetRolesAsync(identityUser).Result.FirstOrDefault();
                    bool userIsNotAdmin = ((identityUserRole != "Admin") && (identityUserRole != "Top-level Admin"));

                    // ONLY store feedback in TempData if a violation occurred
                    if (overallIsTimeConflictFound == true)
                    {
                        // Send feedback to User if any time conflicts detected overall
                        TempData["YourBookings"] = overallNewBookingsToAdd;
                        TempData["TimeConflictedBookings"] = overallTimeConflictFeedback;

                        throw new DbUpdateException("Booking time conflict violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsRoomTimeViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the time total for a specific room
                        TempData["RoomHoursViolation"] = overallRoomTimeFeedback;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsAreaTimeViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the time total for an area
                        TempData["AreaHoursViolation"] = overallAreaTimeFeedback;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsSingleBookingLengthViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the maximum time for a single booking
                        TempData["SingleBookingLengthViolation"] = overallSingleBookingLengthFeedback;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsAreaNumberOfBookingsViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the maximum number of bookings allowed in this area
                        TempData["AreaBookingCountViolation"] = overallAreaNumberOfBookingsFeedback;

                        throw new DbUpdateException("Total Booking count violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsBlackoutViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the minimum blackout time allowed for a room
                        TempData["YourBookings"] = overallNewBookingsToAdd;
                        TempData["BlackoutTimeViolation"] = overallBlackoutViolationFeedback;
                        TempData["BlackoutTimeValues"] = overallRoomBlackoutTimes;

                        throw new DbUpdateException("Blackout time violation.");    // Break before bookings are added or saved
                    }

                    //_context.Add(roomBooking);    // DO NOT ADD "roomBooking" variable. "roomBooking" variable is ONLY used to validate model state
                    _context.RoomBookings.AddRange(overallNewBookingsToAdd);  // Add new bookings to _context
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
                else if (dex.GetBaseException().Message == "Blackout time violation.")
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

            // Disable Approval Status select
            if (!User.IsInRole("Top-level Admin") || roomBooking.ApprovalStatus == "Approved")
            {
                ViewData["DisableSelectApprovalStatus"] = true;
            }
            else
            {
                ViewData["DisableSelectApprovalStatus"] = false;
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
        public async Task<IActionResult> Edit(int id, int RoomGroupID, DateTime StartDate)
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
            if (StartDate < DateTime.Today)
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

            // Make a copy of the old Booking for comparison later
            RoomBooking roomBookingPreviousState = new RoomBooking();
            roomBookingPreviousState.ID = roomBookingToUpdate.ID;
            roomBookingPreviousState.SpecialNotes = roomBookingToUpdate.SpecialNotes;
            roomBookingPreviousState.StartDate = roomBookingToUpdate.StartDate;
            roomBookingPreviousState.EndDate = roomBookingToUpdate.EndDate;
            roomBookingPreviousState.RoomID = roomBookingToUpdate.RoomID;
            roomBookingPreviousState.Room = roomBookingToUpdate.Room;
            roomBookingPreviousState.UserID = roomBookingToUpdate.UserID;
            roomBookingPreviousState.User = roomBookingToUpdate.User;

            if (User.IsInRole("User"))
            {
                if (User.Identity.Name != roomBookingToUpdate.User.Username)
                {
                    TempData["Message"] = "You are not authorized to edit another User's Bookings.";
                    return Redirect(ViewData["returnURL"].ToString());
                }
            }

            // Disable Approval Status select
            if (!User.IsInRole("Top-level Admin") || roomBookingToUpdate.ApprovalStatus == "Approved")
            {
                ViewData["DisableSelectApprovalStatus"] = true;
            }
            else
            {
                ViewData["DisableSelectApprovalStatus"] = false;
            }

            // Try updating it with the values posted
            if (await TryUpdateModelAsync<RoomBooking>(roomBookingToUpdate, "",
                p => p.SpecialNotes, p => p.StartDate, p => p.EndDate, p => p.RoomID, p => p.UserID, p => p.ApprovalStatus))
            {
                try
                {
                    // Overall list of bookings updated
                    // We only update 1 booking, but we use a list because the controls for giving user feedback works with lists
                    List<RoomBooking> overallBookingsUpdated = new List<RoomBooking>();
                    overallBookingsUpdated.Add(roomBookingToUpdate);

                    // Overall results for time conflict detection
                    bool overallIsTimeConflictFound = false;
                    List<RoomBooking> overallTimeConflictFeedback = new List<RoomBooking>();

                    // Overall results for violating total booking time for a Room
                    bool overallIsRoomTimeViolationFound = false;
                    List<IDictionary<string, string>> overallRoomTimeFeedback = new List<IDictionary<string, string>>();

                    // Overall results for violating total booking time for an Area
                    bool overallIsAreaTimeViolationFound = false;
                    List<IDictionary<string, string>> overallAreaTimeFeedback = new List<IDictionary<string, string>>();

                    // Overall results for violating max length per single Booking
                    bool overallIsSingleBookingLengthViolationFound = false;
                    List<IDictionary<string, string>> overallSingleBookingLengthFeedback = new List<IDictionary<string, string>>();

                    // Overall results for violating blackout time
                    bool overallIsBlackoutViolationFound = false;
                    List<RoomBooking> overallBlackoutViolationFeedback = new List<RoomBooking>();
                    List<int> overallRoomBlackoutTimes = new List<int>();

                    // Check for time conflicts
                    RoomBooking timeConflictFeedback;
                    if (!RoomIsAvailable(roomBookingToUpdate, out timeConflictFeedback, roomBookingToUpdate.ID))
                    {
                        overallIsTimeConflictFound = true;    // ONLY flag the overall result if a time conflict is found
                    }
                    overallTimeConflictFeedback.Add(timeConflictFeedback); // ALWAYS track conflict feedback so far, in case there was a conflict in a different room

                    // Get User's pre-existing booked time in this Area
                    var thisRoom = _context.Rooms.Where(r => r.ID == roomBookingToUpdate.RoomID).FirstOrDefault();
                    var thisArea = _context.RoomGroups.Where(r => r.ID == thisRoom.RoomGroupID).FirstOrDefault();
                    var existingBookingsForThisArea = _context.RoomBookings.Include(b => b.Room)
                        .Where(b => (b.UserID == roomBookingToUpdate.UserID)
                               && (b.Room.RoomGroupID == thisArea.ID)
                               && (b.EndDate >= DateTime.Today)
                               && (b.ID != roomBookingToUpdate.ID) /* Exclude this updated booking from the query, since we will compare using local variables */
                              );

                    // Tally User's existing booked time in this area
                    TimeSpan existingTotalTimeInThisArea = GetTotalDurationOfBookings(existingBookingsForThisArea.ToList());

                    // Tally up the new time added by User's updated bookings
                    TimeSpan timeAddedByBookingAfterUpdate = roomBookingToUpdate.EndDate - roomBookingToUpdate.StartDate;
                    // Tally up the old time added by User's booking before it was updated
                    TimeSpan timeAddedByBookingBeforeUpdate = roomBookingPreviousState.EndDate - roomBookingPreviousState.StartDate;

                    // Check if the max Area time was exceeded
                    if (thisArea.MaxHoursTotal != null)
                    {
                        if ((existingTotalTimeInThisArea + timeAddedByBookingAfterUpdate).TotalHours > thisArea.MaxHoursTotal)
                        {
                            overallIsAreaTimeViolationFound = true;   // ONLY flag the overall result if a area time exceeds max allowed hours for this area
                        }
                    }
                    // ALWAYS track Area time feedback so far, in case there was an exception in a different Area
                    IDictionary<string, string> totalTimeFeedbackForThisArea = new Dictionary<string, string>();
                    totalTimeFeedbackForThisArea.Add("AreaName", thisArea.AreaName);
                    totalTimeFeedbackForThisArea.Add("MaxHoursForArea", thisArea.MaxHoursTotal.ToString());
                    totalTimeFeedbackForThisArea.Add("PreviousBookingDuration", (timeAddedByBookingBeforeUpdate).TotalHours.ToString());
                    totalTimeFeedbackForThisArea.Add("PreviousTotalHoursForArea", (existingTotalTimeInThisArea + timeAddedByBookingBeforeUpdate).TotalHours.ToString());
                    totalTimeFeedbackForThisArea.Add("NewBookingDuration", timeAddedByBookingAfterUpdate.TotalHours.ToString());
                    totalTimeFeedbackForThisArea.Add("NewTotalHoursForArea", (existingTotalTimeInThisArea + timeAddedByBookingAfterUpdate).TotalHours.ToString());
                    overallAreaTimeFeedback.Add(totalTimeFeedbackForThisArea);

                    // Get User's pre-existing booked time in this Room
                    var existingBookingsForThisRoom = _context.RoomBookings
                        .Where(b => (b.UserID == roomBookingToUpdate.UserID)
                               && (b.RoomID == roomBookingToUpdate.RoomID)
                               && (b.EndDate >= DateTime.Today)
                               && (b.ID != roomBookingToUpdate.ID) /* Exclude this updated booking from the query, since we will compare using local variables */
                              );

                    // Tally up the User's existing time
                    TimeSpan existingTotalTimeInThisRoom = GetTotalDurationOfBookings(existingBookingsForThisRoom.ToList());

                    // Compare the new hour total to the maximum hours allowed for this room
                    if (thisRoom.RoomMaxHoursTotal != null)
                    {
                        if ((existingTotalTimeInThisRoom + timeAddedByBookingAfterUpdate).TotalHours > thisRoom.RoomMaxHoursTotal)
                        {
                            overallIsRoomTimeViolationFound = true;   // ONLY flag the overall result if a room hour total exceeds max allowed hours for this room
                        }
                    }
                    // ALWAYS track room hour feedback so far, in case there was an exception in a different room
                    IDictionary<string, string> totalTimeFeedbackForThisRoom = new Dictionary<string, string>();
                    totalTimeFeedbackForThisRoom.Add("RoomName", thisRoom.RoomName);
                    totalTimeFeedbackForThisRoom.Add("MaxHoursForRoom", thisRoom.RoomMaxHoursTotal.ToString());
                    totalTimeFeedbackForThisRoom.Add("PreviousBookingDuration", (timeAddedByBookingBeforeUpdate).TotalHours.ToString());
                    totalTimeFeedbackForThisRoom.Add("PreviousTotalHoursForRoom", (existingTotalTimeInThisRoom + timeAddedByBookingBeforeUpdate).TotalHours.ToString());
                    totalTimeFeedbackForThisRoom.Add("NewBookingDuration", timeAddedByBookingAfterUpdate.TotalHours.ToString());
                    totalTimeFeedbackForThisRoom.Add("NewTotalHoursForRoom", (existingTotalTimeInThisRoom + timeAddedByBookingAfterUpdate).TotalHours.ToString());
                    overallRoomTimeFeedback.Add(totalTimeFeedbackForThisRoom);

                    // Check if the time length for a single Booking was exceeded
                    if (thisArea.MaxHoursPerSingleBooking != null)
                    {
                        if ((roomBookingToUpdate.EndDate - roomBookingToUpdate.StartDate).TotalHours > thisArea.MaxHoursPerSingleBooking)
                        {
                            overallIsSingleBookingLengthViolationFound = true;   // ONLY flag the overall result if a violation for single booking length is found
                        }
                    }
                    // ALWAYS track single booking length feedback so far, in case there was an exception in a different room
                    IDictionary<string, string> singleBookingLengthFeedbackForThisRoom = new Dictionary<string, string>();
                    singleBookingLengthFeedbackForThisRoom.Add("AreaName", thisArea.AreaName);
                    singleBookingLengthFeedbackForThisRoom.Add("MaxHoursSingleBooking", thisArea.MaxHoursPerSingleBooking.ToString());
                    singleBookingLengthFeedbackForThisRoom.Add("PreviousBookingDuration", (timeAddedByBookingBeforeUpdate).TotalHours.ToString());
                    singleBookingLengthFeedbackForThisRoom.Add("NewBookingDuration", timeAddedByBookingAfterUpdate.TotalHours.ToString());
                    overallSingleBookingLengthFeedback.Add(singleBookingLengthFeedbackForThisRoom);

                    // Check if the booking violates an existing booking's blackout time
                    if (BookingViolatesBlackoutTime(roomBookingToUpdate, out RoomBooking blackoutFeedbackForThisRoom, roomBookingToUpdate.ID))
                    {
                        overallIsBlackoutViolationFound = true;    // ONLY flag the overall result if a blackout time violation is found
                        overallBlackoutViolationFeedback.Add(blackoutFeedbackForThisRoom); // ALWAYS track blackout feedback so far, in case there was a violation in a different room
                    }
                    else
                    {
                        overallBlackoutViolationFeedback.Add(null); // Add blanks when there is no violation, so User can know if some of their Bookings did not violate while others did violate
                    }
                    overallRoomBlackoutTimes.Add(thisArea.BlackoutTime);    // Record every blackout time so we can give feedback to the User

                    // Get User's role so we only apply rule violations to non-admins
                    User bookingUser = _context.Users.Where(u => u.ID == roomBookingToUpdate.UserID).FirstOrDefault();
                    var identityUser = _identityContext.Users.FirstOrDefault(p => p.UserName == bookingUser.Username);
                    string identityUserRole = _userManager.GetRolesAsync(identityUser).Result.FirstOrDefault();
                    bool userIsNotAdmin = ((identityUserRole != "Admin") && (identityUserRole != "Top-level Admin"));

                    // ONLY store feedback in TempData if a violation occurred
                    if (overallIsTimeConflictFound == true)
                    {
                        // Send feedback to User if any time conflicts detected overall
                        TempData["YourBookings"] = overallBookingsUpdated;
                        TempData["TimeConflictedBookings"] = overallTimeConflictFeedback;

                        throw new DbUpdateException("Booking time conflict violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsRoomTimeViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the time total for a specific room
                        TempData["RoomHoursViolation"] = overallRoomTimeFeedback;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsAreaTimeViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the time total for an area
                        TempData["AreaHoursViolation"] = overallAreaTimeFeedback;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsSingleBookingLengthViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the maximum time for a single booking
                        TempData["SingleBookingLengthViolation"] = overallSingleBookingLengthFeedback;

                        throw new DbUpdateException("Booking time total violation.");    // Break before bookings are added or saved
                    }
                    if (overallIsBlackoutViolationFound == true && userIsNotAdmin)
                    {
                        // Send feedback to User if their new bookings violate the minimum blackout time allowed for a room
                        TempData["YourBookings"] = overallBookingsUpdated;
                        TempData["BlackoutTimeViolation"] = overallBlackoutViolationFeedback;
                        TempData["BlackoutTimeValues"] = overallRoomBlackoutTimes;

                        throw new DbUpdateException("Blackout time violation.");    // Break before bookings are added or saved
                    }

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
                    else if (dex.GetBaseException().Message == "Blackout time violation.")
                    {
                        // We handle this violation by storing feedback into TempData earlier, so don't need to do anything here
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
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

            if (User.IsInRole("User") || User.IsInRole("Admin"))
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

            if (User.IsInRole("User") || User.IsInRole("Admin"))
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

        [HttpGet]
        public JsonResult GetArea(int? ID)
        {
            var area = _context.RoomGroups.Where(r => r.ID == ID.GetValueOrDefault()).FirstOrDefault();
            return Json(area);
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

        /// <summary>
        /// Generates a List of Room Bookings, repeated from the StartDate until the RepeatEndDate, skipping weeks or days according to the "Interval".
        /// Will only book days of the week that are "Included".
        /// Outputs Area/Room rules feedback through output parameters.
        /// </summary>
        /// <param name="isTimeConflictFound">Outputs "True" if there is a time conflict. Outputs "False" if there are no conflicts.</param>
        /// <param name="timeConflictFeedback">Output list of existing Bookings that may be conflicted by new Bookings.</param>
        /// <param name="timeAddedByGeneratedBookings">TimeSpan of the sum of the total duration of all generated Bookings.</param>
        /// <param name="isSingleBookingViolationFound">Outputs "True" if a generated Booking exceeds the maximum allowed length for a single Booking. Outputs "False" if there are no length violations.</param>
        /// <param name="singleBookingFeedback">Output dictionary of Bookings that may exceed the maximum length for a single Booking.</param>
        /// <returns>Generated list of Room Bookings to add.</returns>
        private List<RoomBooking> GenerateRepeatBookings(string SpecialNotes, int UserID, int RoomID,
            DateTime StartDate, DateTime EndDate, DateTime RepeatEndDate, int RepeatInterval, string RepeatType,
            bool IncludeMonday, bool IncludeTuesday, bool IncludeWednesday, bool IncludeThursday, bool IncludeFriday, bool IncludeSaturday, bool IncludeSunday,
            out bool isTimeConflictFound, out List<RoomBooking> timeConflictFeedback, out TimeSpan timeAddedByGeneratedBookings,
            out bool isSingleBookingViolationFound, out List<IDictionary<string, string>> singleBookingFeedback,
            out bool isBlackoutViolationFound, out List<RoomBooking> blackoutViolationFeedback, out List<int> blackoutTimesForGeneratedBookings)
        {
            // Storage variables to give feedback for validation purposes
            bool isTimeConflictDetected = false;
            List<RoomBooking> timeConflictFeedbackToReturn = new List<RoomBooking>();
            TimeSpan totalTimeOfGeneratedBookings = TimeSpan.Zero;

            bool isSingleBookingViolationDetected = false;
            List<IDictionary<string, string>> singleBookingLengthFeedbackToReturn = new List<IDictionary<string, string>>();

            bool isBlackoutViolationDetected = false;
            List<RoomBooking> blackoutViolationFeedbackToReturn = new List<RoomBooking>();
            List<int> blackoutTimesToReturn = new List<int>();

            //Main list of generated bookings to return
            List<RoomBooking> overallNewBookingsToAdd = new List<RoomBooking>();

            TimeSpan duration = EndDate - StartDate;    // Duration of booking to repeat when generating bookings
            for (DateTime day = StartDate; day.Date <= RepeatEndDate; day = day.AddDays((RepeatType == "Days") ? RepeatInterval : 1))
            {
                totalTimeOfGeneratedBookings += duration;   // Track the tally of time added

                // Check that this day of the week is checked "On" before adding booking
                if ((day.DayOfWeek == DayOfWeek.Monday && IncludeMonday)    // Generate booking if the variable day of the week is one of the included days of the week
                    || (day.DayOfWeek == DayOfWeek.Tuesday && IncludeTuesday)
                    || (day.DayOfWeek == DayOfWeek.Wednesday && IncludeWednesday)
                    || (day.DayOfWeek == DayOfWeek.Thursday && IncludeThursday)
                    || (day.DayOfWeek == DayOfWeek.Friday && IncludeFriday)
                    || (day.DayOfWeek == DayOfWeek.Saturday && IncludeSaturday)
                    || (day.DayOfWeek == DayOfWeek.Sunday && IncludeSunday)
                    || (RepeatType == "Days"))  // Generate booking if the RepeatType is Days, since it includes all days of the week
                {
                    // Get Room data
                    var thisRoom = _context.Rooms.Include(r => r.RoomGroup).Where(r => r.ID == RoomID).FirstOrDefault();

                    // Generate booking
                    RoomBooking newBooking = new RoomBooking
                    {
                        UserID = UserID,
                        SpecialNotes = SpecialNotes,
                        StartDate = day,
                        EndDate = day + duration,
                        RoomID = RoomID,
                        Room = thisRoom,
                        ApprovalStatus = (thisRoom.RoomGroup.NeedsApproval ? "Pending" : "Approved")
                    };

                    // Check for booking time conflicts
                    if (!RoomIsAvailable(newBooking, out RoomBooking timeConflictFeedbackForThisRoom))
                    {
                        isTimeConflictDetected = true;
                        timeConflictFeedbackToReturn.Add(timeConflictFeedbackForThisRoom);
                    }
                    else
                    {
                        // Add blanks when there is no conflict, so User can know if some of their Bookings did not conflict while others did conflict
                        timeConflictFeedbackToReturn.Add(null);
                    }

                    // Check if the length of time for a single Booking was exceeded
                    var thisArea = _context.RoomGroups.Where(r => r.ID == newBooking.Room.RoomGroupID).FirstOrDefault();
                    if (thisArea.MaxHoursPerSingleBooking != null)
                    {
                        if (duration.TotalHours > thisArea.MaxHoursPerSingleBooking)
                        {
                            isSingleBookingViolationDetected = true;   // ONLY flag the overall result if a booking time exceeds max allowed hours for a single booking
                        }
                    }
                    // ALWAYS track single booking length feedback so far, in case there was an exception in a different room
                    IDictionary<string, string> singleBookingLengthFeedbackForThisRoom = new Dictionary<string, string>();
                    singleBookingLengthFeedbackForThisRoom.Add("RoomName", newBooking.Room.RoomName);
                    singleBookingLengthFeedbackForThisRoom.Add("MaxHoursSingleBooking", thisArea.MaxHoursPerSingleBooking.ToString());
                    singleBookingLengthFeedbackForThisRoom.Add("BookingStartDate", newBooking.StartDate.ToShortDateString());
                    singleBookingLengthFeedbackForThisRoom.Add("BookingStartTime", newBooking.StartDate.ToShortTimeString());
                    singleBookingLengthFeedbackForThisRoom.Add("BookingEndDate", newBooking.EndDate.ToShortDateString());
                    singleBookingLengthFeedbackForThisRoom.Add("BookingEndTime", newBooking.EndDate.ToShortTimeString());
                    singleBookingLengthFeedbackForThisRoom.Add("HoursBooked", (newBooking.EndDate - newBooking.StartDate).TotalHours.ToString());
                    singleBookingLengthFeedbackToReturn.Add(singleBookingLengthFeedbackForThisRoom);

                    // Check for blackout time violations
                    if (BookingViolatesBlackoutTime(newBooking, out RoomBooking blackoutFeedbackForThisRoom))
                    {
                        isBlackoutViolationDetected = true;    // ONLY flag the overall result if a blackout time violation is found
                        blackoutViolationFeedbackToReturn.Add(blackoutFeedbackForThisRoom); // ALWAYS track blackout feedback so far, in case there was a violation in a different room
                    }
                    else
                    {
                        blackoutViolationFeedbackToReturn.Add(null); // Add blanks when there is no violation, so User can know if some of their Bookings did not violate while others did violate
                    }
                    blackoutTimesToReturn.Add(thisArea.BlackoutTime);    // Record every blackout time so we can give feedback to the User

                    // Add booking to list to return
                    overallNewBookingsToAdd.Add(newBooking);
                }

                // Check the repeat interval, since we may need to skip weeks
                if ((RepeatType == "Weeks")
                    && (day.DayOfWeek == DayOfWeek.Saturday))
                    day = day.AddDays(7 * (RepeatInterval - 1));   // Add days according to week interval at the end of the week
            }

            // Return violation results:
            // -Time conflict violations
            isTimeConflictFound = isTimeConflictDetected;
            timeConflictFeedback = timeConflictFeedbackToReturn;
            // -Total time of new bookings
            timeAddedByGeneratedBookings = totalTimeOfGeneratedBookings;
            // -Hour check for length of single bookings
            isSingleBookingViolationFound = isSingleBookingViolationDetected;
            singleBookingFeedback = singleBookingLengthFeedbackToReturn;
            // -Blackout time violations
            isBlackoutViolationFound = isBlackoutViolationDetected;
            blackoutViolationFeedback = blackoutViolationFeedbackToReturn;
            blackoutTimesForGeneratedBookings = blackoutTimesToReturn;

            return overallNewBookingsToAdd;   // Return list of generated bookings
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
        public void GetRoomsJSON(IQueryable<RoomBooking> bookings, bool filtered)
        {
            var rooms = bookings
                .Include(r => r.Room)
                .ThenInclude(r => r.RoomGroup)
                .Select(r => r.Room)
                .Distinct();

            if (!filtered)
            {
                rooms = from r in _context.Rooms
                        .Include(r => r.RoomGroup)
                        select r;
            }

            List<RoomJSON> roomList = new List<RoomJSON>();

            foreach (Room r in rooms)
            {
                roomList.Add(new RoomJSON { id = r.ID, building = r.RoomGroup.AreaName, title = r.RoomName });
            }

            ViewData["RoomList"] = JsonConvert.SerializeObject(roomList);
        }

        // Get a list of all bookings and return them as a JSON array
        public void GetBookingsJSON(IQueryable<RoomBooking> bookings)
        {        
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
        /// <param name="bookingIdToIgnore">Does not check "newBooking" against existing Bookings with this ID. Useful when editing a Booking,
        /// since you don't want to check the edited Booking against its old existing values.</param>
        /// <returns>True if there is no time conflict. False if there is a time conflict.</returns>
        private bool RoomIsAvailable(RoomBooking newBooking, out RoomBooking conflictBooking, int bookingIdToIgnore = -1)
        {
            List<RoomBooking> existingBookings =
                new List<RoomBooking>(_context.RoomBookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => (b.RoomID == newBooking.RoomID)
                            && (b.ID != bookingIdToIgnore || bookingIdToIgnore == -1)));

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

        /// <summary>
        /// Checks if a new Booking violates the blackout time of an existing Booking.
        /// </summary>
        /// <param name="newBooking">New Booking to compare to existing Bookings and check for blackout violations.</param>
        /// <param name="conflictBooking">Output the existing Booking that the new Booking violates, if a violation exists.</param>
        /// <param name="bookingIdToIgnore">Does not check "newBooking" against existing Bookings with this ID. Useful when editing a Booking,
        /// since you don't want to check the edited Booking against its old existing values.</param>
        /// <returns>True if there is a blackout violation. False if there is no blackout violation.</returns>
        private bool BookingViolatesBlackoutTime(RoomBooking newBooking, out RoomBooking conflictBooking, int bookingIdToIgnore = -1)
        {
            // Look up this area's blackout time
            int blackoutTimeForThisArea = _context.RoomGroups.Where(r => r.ID == newBooking.Room.RoomGroupID).Select(r => r.BlackoutTime).FirstOrDefault();

            // There is no need to compare Bookings if the blackout time is zero
            if (blackoutTimeForThisArea <= 0)
            {
                conflictBooking = null; // Return no conflict result
                return false;
            }

            // Get this user's bookings for this room
            int currentUserID = _context.Users.Where(u => u.Username == User.Identity.Name).Select(u => u.ID).FirstOrDefault();

            List<RoomBooking> existingBookings =
                new List<RoomBooking>(_context.RoomBookings
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => (b.RoomID == newBooking.RoomID)
                       && (b.UserID == currentUserID)
                        && (b.ID != bookingIdToIgnore || bookingIdToIgnore == -1)));

            // Compare bookings the same way we do for booking conflicts, but add the blackout time to the end date
            foreach (RoomBooking existingBooking in existingBookings)
            {
                if ((newBooking.StartDate >= existingBooking.StartDate && newBooking.StartDate < existingBooking.EndDate.AddHours(blackoutTimeForThisArea)) /* Start of new Booking is within an existing Booking's blackout time */
                 || (newBooking.EndDate.AddHours(blackoutTimeForThisArea) > existingBooking.StartDate && newBooking.EndDate.AddHours(blackoutTimeForThisArea) <= existingBooking.EndDate.AddHours(blackoutTimeForThisArea)) /* End of a new Booking is within an existing Booking's blackout time */
                 || (newBooking.StartDate <= existingBooking.StartDate && newBooking.EndDate.AddHours(blackoutTimeForThisArea) >= existingBooking.EndDate.AddHours(blackoutTimeForThisArea))) /* New Booking occurs on top of an existing Booking's blackout time */
                {
                    conflictBooking = existingBooking;
                    return true;
                }
            }
            conflictBooking = null;
            return false;
        }

        /// <summary>
        /// Returns the total duration of an IQueryable of Room Bookings.
        /// </summary>
        /// <param name="bookingsToAddUp">IQueryable of Room Bookings to add up the duration of.</param>
        /// <returns></returns>
        private TimeSpan GetTotalDurationOfBookings(List<RoomBooking> bookingsToAddUp)
        {
            TimeSpan sum = TimeSpan.Zero;

            // Tally up Booking time
            foreach (var booking in bookingsToAddUp)
            {
                TimeSpan duration = booking.EndDate - booking.StartDate;
                sum += duration;
            }

            return sum;
        }
    }
}
