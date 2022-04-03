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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
