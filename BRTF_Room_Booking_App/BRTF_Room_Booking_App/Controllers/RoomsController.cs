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
    public class RoomsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public RoomsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(int? page, int? pageSizeID)
        {
            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var rooms = from t in _context.Rooms
                .Include(t => t.RoomGroup)
                        select t;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(t => t.RoomGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RoomName,RoomMaxHoursTotal,RoomGroupID")] Room room)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(room);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Rooms.RoomName"))
                {
                    ModelState.AddModelError("RoomName", "Unable to save changes. This Room with this Name already exists.");
                    //  ModelState.AddModelError("ProgramLevel", "Unable to save changes. This Program Code and Level combination already exists.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDropDownLists(room);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(room);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the TermAndProgram to update
            var roomToUpdate = await _context.Rooms.FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (roomToUpdate == null)
            {
                return NotFound();
            }

            // Try updating it with the values posted
            if (await TryUpdateModelAsync<Room>(roomToUpdate, "",
                p => p.RoomName, p => p.RoomMaxHoursTotal,
                p => p.RoomGroupID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(roomToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Rooms.RoomName"))
                    {
                        ModelState.AddModelError("RoomName", "Unable to save changes. Room with this name already exists.");
                        //   ModelState.AddModelError("ProgramLevel", "Unable to save changes. This Program Code and Level combination already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            PopulateDropDownLists(roomToUpdate);
            return View(roomToUpdate);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termAndProgram = await _context.Rooms
                .Include(t => t.RoomGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (termAndProgram == null)
            {
                return NotFound();
            }

            return View(termAndProgram);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms
                .Include(t => t.RoomGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete. You cannot delete a Room that has a Room Group assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(room);
        }

        //This is a twist on the PopulateDropDownLists approach
        //  Create methods that return each SelectList separately
        //  and one method to put them all into ViewData.
        //This approach allows for AJAX requests to refresh
        //DDL Data at a later date.
        private SelectList RoomGroupSelectList(int? selectedId)
        {
            return new SelectList(_context.RoomGroups
                .OrderBy(u => u.AreaName), "ID", "AreaName", selectedId);
        }
        private void PopulateDropDownLists(Room room = null)
        {
            ViewData["RoomGroupID"] = RoomGroupSelectList(room?.RoomGroupID);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }

    }
}
