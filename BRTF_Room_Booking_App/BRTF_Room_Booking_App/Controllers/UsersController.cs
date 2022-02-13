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
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using System.Text;

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
        public async Task<IActionResult> Index(string SearchName, string SearchEmail, int? UserGroupID, int? page, int? pageSizeID, 
            string actionButton, string sortDirection = "asc", string sortField = "Full Name")
        {
            //Toggle the open/closed state of the collapse depending on if something is being filtered
            ViewData["Filtering"] = ""; //Assume nothing is filtered

            PopulateDropDownLists(); //data for User Filter DDL

            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var users = from u in _context.Users
                .Include(u => u.Role)
                .Include(u => u.TermAndProgram).ThenInclude(t => t.UserGroup)
                .AsNoTracking()
                select u;

            //adding filters
            if (UserGroupID.HasValue)
            {
                users = users.Where(u => u.TermAndProgram.UserGroupID == UserGroupID);
                ViewData["Filtering"] = " show ";
            }
            if (!String.IsNullOrEmpty(SearchName))
            {
                users = users.Where(u => u.LastName.ToUpper().Contains(SearchName.ToUpper()) ||
                                    u.MiddleName.ToUpper().Contains(SearchName.ToUpper()) ||
                                    u.FirstName.ToUpper().Contains(SearchName.ToUpper()));
                ViewData["Filtering"] = " show ";
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                users = users.Where(u => u.Email.ToUpper().Contains(SearchEmail.ToUpper()));
                ViewData["Filtering"] = " show ";
            }

            //Before sorting, you need to check to see if there has been a change to of filter/sort
            if (!String.IsNullOrEmpty(actionButton)) //the form has been submitted
            {
                if (actionButton != "Filter")
                {
                    if (actionButton == sortField) //reversing on the same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton; //sorting by the button that was clicked
                }
            }
            //now the sort field and direction is known
            if (sortField == "Full Name")
            {
                if (sortDirection == "asc")
                {
                    users = users
                        .OrderBy(u => u.FirstName)
                        .ThenBy(u => u.LastName);
                }
                else
                {
                    users = users
                        .OrderByDescending(u => u.FirstName)
                        .ThenByDescending(u => u.LastName); 
                }
            }
            else //sorting by Term and Program
            {
                if (sortDirection == "asc")
                {
                    users = users
                        .OrderBy(u => u.TermAndProgram.UserGroup.UserGroupName);
                }
                else
                {
                    users = users
                        .OrderByDescending(u => u.TermAndProgram.UserGroup.UserGroupName);
                }  
            }
            //now to set the sort for the next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

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
                .Include(u => u.TermAndProgram).ThenInclude(t => t.UserGroup)
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
                    TempData["AlertMessage"] = "User Created Successfully!";
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
            //Get the full TermAndProgram object for the User and then populate DDL
            user.TermAndProgram = await _context.TermAndPrograms.FindAsync(user.TermAndProgramID);
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

            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.TermAndProgram).ThenInclude(t => t.UserGroup)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

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
        public async Task<IActionResult> Edit(int id, string Password)
        {
            // Get the User to update
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(p => p.ID == id);

            // Check that you got it or exit with a not found error
            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Check if we should update password
            if (String.IsNullOrEmpty(Password))
            {
                // Password is empty, do nothing
            }
            else
            {
                // Try updating password
                await TryUpdateModelAsync<User>(userToUpdate, "", p => p.Password);
            }

            // Try updating it with the values posted
            if (await TryUpdateModelAsync<User>(userToUpdate, "",
                p => p.Username, p => p.FirstName, p => p.MiddleName, p => p.LastName,
                p => p.Email, p => p.EmailBookingNotifications, p => p.EmailCancelNotifications,
                p => p.TermAndProgramID, p => p.RoleID)
                )
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "User Edited Successfully!";
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
                .Include(u => u.TermAndProgram).ThenInclude(t => t.UserGroup)
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
                .Include(u => u.TermAndProgram).ThenInclude(t => t.UserGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "User Deleted Successfully!";
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
            
            string uploadMessage = "";
            int i = 0;//Counter for inserted records
            int j = 0;//Counter for duplicates

            try
            {
                //ExcelPackage excel;
                var csvExcel = new StringBuilder();
                using (var reader = new StreamReader(theExcel.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        csvExcel.AppendLine(reader.ReadLine());
                }

                string excel = csvExcel.ToString();
                //var workSheet = excel.Workbook.Worksheets[0];
                //var start = workSheet.Dimension.Start;
                //var end = workSheet.Dimension.End;

                //check for duplicates
                var existingUsers = (_context.Users
                    .Select(c => new { email = c.Email }))
                    .ToList().Select(c => c.email).ToHashSet();


                //Start a new list to hold imported objects
                List<User> users = new List<User>();

                //Read the contents of the CSV file
                //string csvData = System.IO.File.ReadAllText(filePath);

                int firow = 1;

                //Execute a loop over the rows
                foreach (string row in excel.Split("\r\n"))
                {
                    if (firow == 1)
                    {
                        firow++;
                        continue;
                    }
                    if (!string.IsNullOrEmpty(row))
                    {
                        {
                            //Look for Term and Program or create it if it doesnt exist
                            //Query all Terms and Programs
                            var programs = from p in _context.TermAndPrograms
                                           select p;

                            string progCode = row.Split("\t")[5].ToUpper();
                            int progLevel = Convert.ToInt32(row.Split("\t")[8]);

                            //Check if we have the specific combination of Program Code and Level already
                            programs = programs.Where(p => p.ProgramCode.ToUpper().Equals(progCode)
                                                        && p.ProgramLevel.Equals(progLevel));
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
                                    ProgramCode = row.Split("\t")[5],
                                    ProgramName = row.Split("\t")[6],
                                    ProgramLevel = int.Parse(row.Split("\t")[8]),
                                    UserGroupID = 1
                                };
                                _context.TermAndPrograms.Add(prog);
                                _context.SaveChanges();

                                //Re-query the programs the same way so we can get the ID of the newly added Term and Program
                                programs = from p in _context.TermAndPrograms
                                           select p;

                                programs = programs.Where(p => p.ProgramCode.ToUpper().Equals(progCode)
                                                            && p.ProgramLevel.Equals(progLevel));

                                progID = programs.FirstOrDefault().ID;
                            }

                            //Row by row...
                            User u = new User
                            {
                                FirstName = row.Split("\t")[2],
                                MiddleName = row.Split("\t")[3],
                                LastName = row.Split("\t")[4],
                                TermAndProgramID = progID,
                                Email = row.Split("\t")[7],
                                EmailBookingNotifications = true,
                                EmailCancelNotifications = true,
                                RoleID = 2,
                                Username = row.Split("\t")[1],
                                Password = "password"
                            };
                            //count the duplicates
                            if (existingUsers.Contains(u.Email))
                            {
                                j++;
                            }
                            else
                            {
                                users.Add(u);
                                i++;
                            }
                        }
                    }

                    //for (int row = start.Row + 1; row <= end.Row; row++)
                    //{
                    //    //Look for Term and Program or create it if it doesnt exist
                    //    //Query all Terms and Programs
                    //    var programs = from p in _context.TermAndPrograms
                    //                   select p;

                    //    //Check if we have the specific combination of Program Code and Level already
                    //    programs = programs.Where(p => p.ProgramCode.ToUpper().Equals(workSheet.Cells[row, 5].Text.ToUpper())
                    //                                && p.ProgramLevel.Equals(int.Parse(workSheet.Cells[row, 8].Text)));
                    //    int progID;


                    //    if (programs.Count() > 0)
                    //    {
                    //        //If we have matches, we grab the ID (FirstOrDefault as there should only be one result) so we can assign it on the User
                    //        progID = programs.FirstOrDefault().ID;
                    //    }
                    //    else
                    //    {
                    //        //If there are no matches, we create a new TermAndProgram object from the CSV file and add it to the context
                    //        TermAndProgram prog = new TermAndProgram
                    //        {
                    //            ProgramCode = workSheet.Cells[row, 5].Text,
                    //            ProgramName = workSheet.Cells[row, 6].Text,
                    //            ProgramLevel = int.Parse(workSheet.Cells[row, 8].Text),
                    //            UserGroupID = 1
                    //        };
                    //        _context.TermAndPrograms.Add(prog);
                    //        _context.SaveChanges();

                    //        //Re-query the programs the same way so we can get the ID of the newly added Term and Program
                    //        programs = from p in _context.TermAndPrograms
                    //                   select p;

                    //        programs = programs.Where(p => p.ProgramCode.ToUpper().Equals(workSheet.Cells[row, 5].Text.ToUpper())
                    //                                    && p.ProgramLevel.Equals(int.Parse(workSheet.Cells[row, 8].Text)));

                    //        progID = programs.FirstOrDefault().ID;
                    //    }

                    //    // Row by row...
                    //    User u = new User
                    //    {
                    //        //  ID = int.Parse(workSheet.Cells[row, 1].Text),
                    //        FirstName = workSheet.Cells[row, 2].Text,
                    //        MiddleName = workSheet.Cells[row, 3].Text,
                    //        LastName = workSheet.Cells[row, 4].Text,
                    //        TermAndProgramID = progID,
                    //        Email = workSheet.Cells[row, 7].Text,
                    //        EmailBookingNotifications = true,
                    //        EmailCancelNotifications = true,
                    //        RoleID = 2,
                    //        Username = workSheet.Cells[row, 1].Text,
                    //        Password = "password"
                    //    };
                    //     //count the duplicates
                    //    if (existingUsers.Contains(u.Email))
                    //    {
                    //        j++;
                    //    }
                    //    else
                    //    {
                    //        users.Add(u);
                    //        i++;
                    //    }
                    //}
                    uploadMessage = "There were " + j + " duplicate(s). Total Users created: " + i;
                    _context.Users.AddRange(users);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                //  uploadMessage = "Failed to import data.  Check that you selected the correct file in the correct format.";
            }
            TempData["Message"] = uploadMessage;
            return RedirectToAction("Index");

        }

        //This is a twist on the PopulateDropDownLists approach
        //  Create methods that return each SelectList separately
        //  and one method to put them all into ViewData.
        //This approach allows for AJAX requests to refresh
        //DDL Data at a later date.
        private SelectList UserGroupsSelectList(int? selectedId)
        {
            return new SelectList(_context.UserGroups
                .OrderBy(g => g.UserGroupName), "ID", "UserGroupName", selectedId);
        }
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
            ViewData["UserGroupID"] = UserGroupsSelectList(user?.TermAndProgram.UserGroupID);
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
