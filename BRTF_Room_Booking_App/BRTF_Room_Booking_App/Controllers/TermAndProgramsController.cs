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
    public class TermAndProgramsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public TermAndProgramsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: TermAndPrograms
        public async Task<IActionResult> Index(int? page, int? pageSizeID)
        {
            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var termAndPrograms = from t in _context.TermAndPrograms
                .Include(t => t.UserGroup)
                select t;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<TermAndProgram>.CreateAsync(termAndPrograms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: TermAndPrograms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termAndProgram = await _context.TermAndPrograms
                .Include(t => t.UserGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (termAndProgram == null)
            {
                return NotFound();
            }

            return View(termAndProgram);
        }

        // GET: TermAndPrograms/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: TermAndPrograms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ProgramName,ProgramCode,ProgramLevel,UserGroupID")] TermAndProgram termAndProgram)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(termAndProgram);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: TermAndPrograms.ProgramLevel, TermAndPrograms.ProgramCode"))
                {
                    ModelState.AddModelError("ProgramCode", "Unable to save changes. This Program Code and Level combination already exists.");
                    ModelState.AddModelError("ProgramLevel", "Unable to save changes. This Program Code and Level combination already exists.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDropDownLists(termAndProgram);
            return View(termAndProgram);
        }

        // GET: TermAndPrograms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termAndProgram = await _context.TermAndPrograms.FindAsync(id);
            if (termAndProgram == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(termAndProgram);
            return View(termAndProgram);
        }

        // POST: TermAndPrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the TermAndProgram to update
            var termAndProgramToUpdate = await _context.TermAndPrograms.FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (termAndProgramToUpdate == null)
            {
                return NotFound();
            }
            
            // Try updating it with the values posted
            if (await TryUpdateModelAsync<TermAndProgram>(termAndProgramToUpdate, "",
                p => p.ProgramName, p => p.ProgramCode, p => p.ProgramLevel,
                p => p.UserGroupID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TermAndProgramExists(termAndProgramToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: TermAndPrograms.ProgramLevel, TermAndPrograms.ProgramCode"))
                    {
                        ModelState.AddModelError("ProgramCode", "Unable to save changes. This Program Code and Level combination already exists.");
                        ModelState.AddModelError("ProgramLevel", "Unable to save changes. This Program Code and Level combination already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            PopulateDropDownLists(termAndProgramToUpdate);
            return View(termAndProgramToUpdate);
        }

        // GET: TermAndPrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termAndProgram = await _context.TermAndPrograms
                .Include(t => t.UserGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (termAndProgram == null)
            {
                return NotFound();
            }

            return View(termAndProgram);
        }

        // POST: TermAndPrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var termAndProgram = await _context.TermAndPrograms
                .Include(t => t.UserGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.TermAndPrograms.Remove(termAndProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete. You cannot delete a Term and Program that has Users assigned.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(termAndProgram);
        }

        //This is a twist on the PopulateDropDownLists approach
        //  Create methods that return each SelectList separately
        //  and one method to put them all into ViewData.
        //This approach allows for AJAX requests to refresh
        //DDL Data at a later date.
        private SelectList UserGroupSelectList(int? selectedId)
        {
            return new SelectList(_context.UserGroups
                .OrderBy(u => u.UserGroupName), "ID", "UserGroupName", selectedId);
        }
        private void PopulateDropDownLists(TermAndProgram termAndProgram = null)
        {
            ViewData["UserGroupID"] = UserGroupSelectList(termAndProgram?.UserGroupID);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool TermAndProgramExists(int id)
        {
            return _context.TermAndPrograms.Any(e => e.ID == id);
        }
    }
}
