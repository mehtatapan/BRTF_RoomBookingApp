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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;

namespace BRTF_Room_Booking_App.Controllers
{
    public class UsersController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public UsersController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(int? page, int? pageSizeID)
        {
            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var users = from u in _context.Users
                .Include(u => u.Role)
                .Include(u => u.TermAndProgram).ThenInclude(t => t.UserGroup)
                select u;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TermAndProgram)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Username,Password,FirstName,MiddleName,LastName,Email,EmailBookingNotifications,EmailCancelNotifications,TermAndProgramID,RoleID")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Users.Username"))
                {
                    ModelState.AddModelError("Username", "Unable to save changes. A User with this Username already exists.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDropDownLists(user);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            PopulateDropDownLists(user);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            // Get the User to update
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (userToUpdate == null)
            {
                return NotFound();
            }
            
            // Try updating it with the values posted
            if (await TryUpdateModelAsync<User>(userToUpdate, "",
                p => p.Username, p => p.Password, p => p.FirstName, p => p.MiddleName, p => p.LastName,
                p => p.Email, p => p.EmailBookingNotifications, p => p.EmailCancelNotifications,
                p => p.TermAndProgramID, p => p.RoleID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.ID))
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
                    if (dex.GetBaseException().Message.Contains("UNIQUE constraint failed: Users.Username"))
                    {
                        ModelState.AddModelError("Username", "Unable to save changes. A User with this Username already exists.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                    }
                }
            }
            PopulateDropDownLists(userToUpdate);
            return View(userToUpdate);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TermAndProgram)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TermAndProgram)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                //Note: there is really no reason a delete should fail if you can "talk" to the database.
                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            //Note: This is a very basic example and has 
            //no ERROR HANDLING.  It also assumes that
            //duplicate values are allowed, both in the 
            //uploaded data and the DbSet.
            ExcelPackage excel;
            using (var memoryStream = new MemoryStream())
            {
                await theExcel.CopyToAsync(memoryStream);
                excel = new ExcelPackage(memoryStream);
            }
            var workSheet = excel.Workbook.Worksheets[0];
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;

            //Start a new list to hold imported objects
            List<User> users = new List<User>();

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                //Look for Term and Program or create it if it doesnt exist
                //Query all Terms and Programs
                var programs = from p in _context.TermAndPrograms
                               select p;

                //Check if we have the specific combination of Program Code and Level already
                programs = programs.Where(p => p.ProgramCode.ToUpper().Equals(workSheet.Cells[row, 5].Text.ToUpper())
                                            && p.ProgramLevel.Equals(int.Parse(workSheet.Cells[row, 8].Text)));
                int progID;


                if (programs.Count() > 0)
                {
                    //If we have matches, we grab the ID (FirstOrDefault as there should only be one result) so we can assign it on the User
                    progID = programs.FirstOrDefault().ID;
                }
                else
                {
                    //If there are no matches, we create a new TermAndProgram object from the CSV file and add it to the context
                    TermAndProgram prog = new TermAndProgram
                    {
                        ProgramCode = workSheet.Cells[row, 5].Text,
                        ProgramName = workSheet.Cells[row, 6].Text,
                        ProgramLevel = int.Parse(workSheet.Cells[row, 8].Text),
                        UserGroupID = 1
                    };
                    _context.TermAndPrograms.Add(prog);
                    _context.SaveChanges();

                    //Re-query the programs the same way so we can get the ID of the newly added Term and Program
                    programs = from p in _context.TermAndPrograms
                               select p;

                    programs = programs.Where(p => p.ProgramCode.ToUpper().Equals(workSheet.Cells[row, 5].Text.ToUpper())
                                                && p.ProgramLevel.Equals(int.Parse(workSheet.Cells[row, 8].Text)));

                    progID = programs.FirstOrDefault().ID;
                }

                // Row by row...
                User u = new User
                {
                    ID = int.Parse(workSheet.Cells[row, 1].Text),
                    FirstName = workSheet.Cells[row, 2].Text,
                    MiddleName = workSheet.Cells[row, 3].Text,
                    LastName = workSheet.Cells[row, 4].Text,
                    TermAndProgramID = progID,
                    Email = workSheet.Cells[row, 7].Text,
                    EmailBookingNotifications = true,
                    EmailCancelNotifications = true,
                    RoleID = 2,
                    Username = workSheet.Cells[row, 1].Text,
                    Password = "password"
                };
                users.Add(u);
            }
            _context.Users.AddRange(users);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //This is a twist on the PopulateDropDownLists approach
        //  Create methods that return each SelectList separately
        //  and one method to put them all into ViewData.
        //This approach allows for AJAX requests to refresh
        //DDL Data at a later date.
        private SelectList RoleSelectList(int? selectedId)
        {
            return new SelectList(_context.Roles
                .OrderBy(r => r.RoleName), "ID", "RoleName", selectedId);
        }
        private SelectList TermAndProgramSelectList(int? selectedId)
        {
            return new SelectList(_context.TermAndPrograms
                .OrderBy(u => u.ProgramCode).ThenBy(u => u.ProgramLevel), "ID", "TermAndProgramSummary", selectedId);
        }
        private void PopulateDropDownLists(User user = null)
        {
            ViewData["RoleID"] = RoleSelectList(user?.RoleID);
            ViewData["TermAndProgramID"] = TermAndProgramSelectList(user?.TermAndProgramID);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
