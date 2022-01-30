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

namespace BRTF_Room_Booking_App.Controllers
{
    public class RoomBookingsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public RoomBookingsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: RoomBookings
        public async Task<IActionResult> Index(int? page, int? pageSizeID)
        {
            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var roombookings = from r in _context.RoomBookings
                               .Include(r => r.Room)
                               .Include(r => r.User)
                               .Include(r => r.StartTime)
                               .Include(r => r.EndTime)
                               select r;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<RoomBooking>.CreateAsync(roombookings.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: RoomBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .Include(r => r.StartTime)
                .Include(r => r.EndTime)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // GET: RoomBookings/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: RoomBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SpecialNotes,Date,RoomID,UserID,StartTimeID,EndTimeID")] RoomBooking roomBooking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(roomBooking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDropDownLists(roomBooking);
            return View(roomBooking);
        }

        // GET: RoomBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings.FindAsync(id);
            if (roomBooking == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(roomBooking);
            return View(roomBooking);
        }

        // POST: RoomBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the RoomBooking to update
            var roomBookingToUpdate = await _context.RoomBookings.FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (roomBookingToUpdate == null)
            {
                return NotFound();
            }
            //, [Bind("ID,Date,RoomID,UserID,StartTimeID,EndTimeID")] RoomBooking roomBooking
            // Try updating it with the values posted
            if (await TryUpdateModelAsync<RoomBooking>(roomBookingToUpdate, "",
                p => p.SpecialNotes, p => p.Date, p => p.RoomID, p => p.UserID,
                p => p.StartTimeID, p => p.EndTimeID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
            PopulateDropDownLists(roomBookingToUpdate);
            return View(roomBookingToUpdate);
        }

        // GET: RoomBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .Include(r => r.StartTime)
                .Include(r => r.EndTime)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomBooking == null)
            {
                return NotFound();
            }

            return View(roomBooking);
        }

        // POST: RoomBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomBooking = await _context.RoomBookings
                .Include(r => r.Room)
                .Include(r => r.User)
                .Include(r => r.StartTime)
                .Include(r => r.EndTime)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.RoomBookings.Remove(roomBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                //Note: there is really no reason a delete should fail if you can "talk" to the database.
                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
            }
            return View(roomBooking);
        }

        //This is a twist on the PopulateDropDownLists approach
        //  Create methods that return each SelectList separately
        //  and one method to put them all into ViewData.
        //This approach allows for AJAX requests to refresh
        //DDL Data at a later date.
        private SelectList RoomSelectList(int? selectedId)
        {
            return new SelectList(_context.Rooms
                .OrderBy(r => r.RoomName.Length)
                .ThenBy(r => r.RoomName), "ID", "RoomName", selectedId);
        }
        private SelectList UserSelectList(int? selectedId)
        {
            return new SelectList(_context.Users
                .OrderBy(u => u.Username), "ID", "Username", selectedId);
        }
        private SelectList StartTimeSelectList(int? selectedId)
        {
            return new SelectList(_context.BookingTimes
                .OrderBy(t => t.MilitaryTimeHour)
                .ThenBy(t => t.MilitaryTimeMinute), "ID", "TwelveHourTimeSummary", selectedId);
        }
        private SelectList EndTimeSelectList(int? selectedId)
        {
            return new SelectList(_context.BookingTimes
                .OrderBy(t => t.MilitaryTimeHour)
                .ThenBy(t => t.MilitaryTimeMinute), "ID", "TwelveHourTimeSummary", selectedId);
        }
        private void PopulateDropDownLists(RoomBooking roomBooking = null)
        {   
            ViewData["RoomID"] = RoomSelectList(roomBooking?.RoomID);
            ViewData["UserID"] = UserSelectList(roomBooking?.UserID);
            ViewData["StartTimeID"] = StartTimeSelectList(roomBooking?.StartTimeID);
            ViewData["EndTimeID"] = EndTimeSelectList(roomBooking?.EndTimeID);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool RoomBookingExists(int id)
        {
            return _context.RoomBookings.Any(e => e.ID == id);
        }
    }
}
