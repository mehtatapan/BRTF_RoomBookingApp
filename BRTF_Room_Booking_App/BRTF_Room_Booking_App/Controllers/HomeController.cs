using BRTF_Room_Booking_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BRTF_Room_Booking_App.Data;
using BRTF_Room_Booking_App.Utilities;
using BRTF_Room_Booking_App.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BRTF_Room_Booking_App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly BTRFRoomBookingContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, BTRFRoomBookingContext context)
        {
            _logger = logger;
            _context = context;
        }

        public bool canApprove(int userID, int roomGroupID)
        {
            var approver = _context.RoomGroupApprovers
                .Where(a => a.UserID == userID && a.RoomGroupID == roomGroupID)
                .FirstOrDefault();

            return approver == null ? false : true;
        }

        public async Task<IActionResult> Index()
        {
            //var roombookings = from r in _context.RoomBookings
            //                   .Include(r => r.Room)
            //                   .Include(r => r.User)
            //                   select r;
            //return View(roombookings);

            if (User.IsInRole("Top-level Admin"))
            {
                var bookings = from r in _context.RoomBookings
                           .Include(r => r.Room)
                           .ThenInclude(r => r.RoomGroup)
                           .ThenInclude(r => r.RoomGroupApprovers)
                           .Include(r => r.User)
                           .Where(r => r.ApprovalStatus == "Pending" && r.Room.RoomGroup.NeedsApproval == true)
                               select r;

                return View(await bookings.ToListAsync());
            }
            else if (User.IsInRole("Admin"))
            {
                User currentUser = await _context.Users
                .Where(u => u.Username == this.HttpContext.User.Identity.Name)
                .FirstOrDefaultAsync();

                var bookings = from r in _context.RoomBookings
                           .Include(r => r.Room)
                           .ThenInclude(r => r.RoomGroup)
                           .ThenInclude(r => r.RoomGroupApprovers)
                           .Include(r => r.User)
                           .Where(r => r.ApprovalStatus == "Pending" && r.Room.RoomGroup.NeedsApproval)
                           select r;

                List<RoomBooking> approverBookings = new List<RoomBooking>();
                foreach (RoomBooking r in bookings)
                {
                    if (canApprove(currentUser.ID, r.Room.RoomGroupID)) {
                        approverBookings.Add(r);
                    }
                }

                return View(approverBookings);
            }
            else
            {
                User currentUser = await _context.Users
                .Where(u => u.Username == this.HttpContext.User.Identity.Name)
                .FirstOrDefaultAsync();

                var bookings = from r in _context.RoomBookings
                               .Include(r => r.Room)
                               .ThenInclude(r => r.RoomGroup)
                               .Include(r => r.User)
                               .Where(r => r.User == currentUser && r.StartDate > DateTime.Now)
                               select r;

                return View(await bookings.ToListAsync());
            }
        }

        public async Task<IActionResult> Approve(int? id)
        {
            var roomBookingToUpdate = await _context.RoomBookings
            .FirstOrDefaultAsync(p => p.ID == id);

            roomBookingToUpdate.ApprovalStatus = "Approved";

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Deny(int? id)
        {
            var roomBookingToUpdate = await _context.RoomBookings
            .FirstOrDefaultAsync(p => p.ID == id);

            roomBookingToUpdate.ApprovalStatus = "Denied";

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }

        public IActionResult Reports()
        {
            return View();
        }

        public async Task<IActionResult> UsersBookings(int? page, int? pageSizeID, /* Paging */
            string SearchAfterDate, string SearchBeforeDate, string sortDirectionCheck, string sortFieldID, /* Filters/Search */
            string actionButton, string sortDirection = "asc", string sortField = "Start Date" /*Sorting*/)
        {
            //Clear the sort/filter/paging URL Cookie for Controller
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            //Toggle the background colour of the filter button depending on if something is being filtered
            ViewData["Filtering"] = "btn-outline-secondary"; //Assume nothing is filtered

            //NOTE: make sure this array has matching values to the column headings being sorted
            string[] sortOptions = new[] { "Start Date", "Approval Status" };

            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            User currentUser = await _context.Users
                .Where(u => u.Username == this.HttpContext.User.Identity.Name)
                .FirstOrDefaultAsync();

            var bookings = from r in _context.RoomBookings
                           .Include(r => r.Room)
                           .ThenInclude(r => r.RoomGroup)
                           .Include(r => r.User)
                           .Where(r => r.User == currentUser && r.StartDate > DateTime.Now)
                           select r;

            //bool filtered = false;

            //Add as many filters as needed
            if (!String.IsNullOrEmpty(SearchAfterDate) && DateTime.TryParse(SearchAfterDate, out DateTime afterDate))
            {
                bookings = bookings.Where(r => afterDate <= r.StartDate);

                ViewData["Filtering"] = "btn-danger";
            }
            if (!String.IsNullOrEmpty(SearchBeforeDate) && DateTime.TryParse(SearchBeforeDate, out DateTime beforeDate))
            {
                bookings = bookings.Where(r => r.StartDate <= beforeDate);
                ViewData["Filtering"] = "btn-danger";
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
                else //Sort by the controls in the filter area
                {
                    sortDirection = String.IsNullOrEmpty(sortDirectionCheck) ? "asc" : "desc";
                    sortField = sortFieldID;
                }
            }
            //Now we know which field and direction to sort by
            if (sortField == "Start Date")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderBy(p => p.StartDate);
                }
                else
                {
                    bookings = bookings
                        .OrderByDescending(p => p.StartDate);
                }
            }
            else //Sorting by Approval Status
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderBy(p => p.ApprovalStatus);
                }
                else
                {
                    bookings = bookings
                        .OrderByDescending(p => p.ApprovalStatus);
                }
            }

            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //SelectList for Sorting Options
            ViewBag.sortFieldID = new SelectList(sortOptions, sortField.ToString());

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<RoomBooking>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);

            //return View(await bookings.ToListAsync());
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
