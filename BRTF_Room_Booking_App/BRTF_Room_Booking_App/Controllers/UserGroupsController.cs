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
    public class UserGroupsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public UserGroupsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: UserGroups
        public async Task<IActionResult> Index(int? page, int? pageSizeID)
        {
            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var userGroups = from u in _context.UserGroups
                             .Include(r => r.RoomUserGroupPermissions).ThenInclude(r => r.RoomGroup)
                             select u;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<UserGroup>.CreateAsync(userGroups.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: UserGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups
                .Include(r => r.RoomUserGroupPermissions).ThenInclude(r => r.RoomGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // GET: UserGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserGroupName")] UserGroup userGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(userGroup);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: UserGroups.UserGroupName"))
                {
                    ModelState.AddModelError("UserGroupName", "Unable to save changes. A User Group with this Name already exists.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the UserGroup to update
            var userGroupToUpdate = await _context.UserGroups.FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (userGroupToUpdate == null)
            {
                return NotFound();
            }
            
            // Try updating it with the values posted
            if (await TryUpdateModelAsync<UserGroup>(userGroupToUpdate, "",
                p => p.UserGroupName))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGroupExists(userGroupToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: UserGroups.UserGroupName"))
                    {
                        ModelState.AddModelError("UserGroupName", "Unable to save changes. A User Group with this Name already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            return View(userGroupToUpdate);
        }

        // GET: UserGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups
                .Include(r => r.RoomUserGroupPermissions).ThenInclude(r => r.RoomGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userGroup = await _context.UserGroups
                .Include(r => r.RoomUserGroupPermissions).ThenInclude(r => r.RoomGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.UserGroups.Remove(userGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete. You cannot delete a User Group that has Terms and Programs assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(userGroup);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool UserGroupExists(int id)
        {
            return _context.UserGroups.Any(e => e.ID == id);
        }
    }
}
