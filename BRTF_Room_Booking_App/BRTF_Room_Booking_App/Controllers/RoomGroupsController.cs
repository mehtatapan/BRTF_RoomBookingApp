﻿using System;
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
    public class RoomGroupsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public RoomGroupsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: RoomGroups
        public async Task<IActionResult> Index(string SearchName, int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "Area Name", string EnabledFilterString = "True")
        {
            var roomGroups = from u in _context.RoomGroups.Where(u=> 
            (u.Enabled == true && EnabledFilterString == "True")||(u.Enabled == false && EnabledFilterString == "False")||(EnabledFilterString == "All"))
                             select u;

            //  Filtering
            if (EnabledFilterString != "")
            {
                ViewData["Filtering"] = " show ";
            }
            if (!String.IsNullOrEmpty(SearchName))
            {
                roomGroups = roomGroups.Where(r => r.AreaName.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["Filtering"] = " show ";
            }

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
            {
                if (actionButton != "Filter")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }


            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<RoomGroup>.CreateAsync(roomGroups.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: RoomGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomGroup = await _context.RoomGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomGroup == null)
            {
                return NotFound();
            }

            return View(roomGroup);
        }

        // GET: RoomGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,AreaName,Description,BlackoutTime,MaxHoursPerSingleBooking,MaxHoursTotal,MaxNumberOfBookings,Enabled")] RoomGroup roomGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(roomGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: RoomGroups.AreaName"))
                {
                    ModelState.AddModelError("AreaName", "Unable to save changes. A Room Group with this Name already exists.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(roomGroup);
        }

        // GET: RoomGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomGroup = await _context.RoomGroups.FindAsync(id);
            if (roomGroup == null)
            {
                return NotFound();
            }
            return View(roomGroup);
        }

        // POST: RoomGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var roomGroupToUpdate = await _context.RoomGroups.FirstOrDefaultAsync(rg => rg.ID == id);

            if (roomGroupToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<RoomGroup>(roomGroupToUpdate, "",
                r => r.AreaName, r => r.Description, r => r.BlackoutTime,
                r => r.MaxHoursPerSingleBooking, r => r.MaxHoursTotal,
                r => r.MaxNumberOfBookings, r => r.Enabled))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomGroupExists(roomGroupToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: RoomGroups.AreaName"))
                    {
                        ModelState.AddModelError("AreaName", "Unable to save changes. An area with this Name already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            return View(roomGroupToUpdate);

        }

        // GET: RoomGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomGroup = await _context.RoomGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (roomGroup == null)
            {
                return NotFound();
            }

            return View(roomGroup);
        }

        // POST: RoomGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomGroup = await _context.RoomGroups.FindAsync(id);
            try
            {
                _context.RoomGroups.Remove(roomGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete. You cannot delete a Room Group that has Rooms assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(roomGroup);
        }
        private SelectList EnabledSelectList(int? selectedId)
        {
            return new SelectList(_context.RoomGroups
                .OrderBy(u => u.Enabled), "ID", "Enabled", selectedId);
        }

        private void PopulateDropDownLists(RoomGroup roomGroup = null)
        {
            ViewData["Enabled"] = EnabledSelectList(roomGroup?.ID);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool RoomGroupExists(int id)
        {
            return _context.RoomGroups.Any(e => e.ID == id);
        }
    }
}
