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
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.AspNetCore.Http.Features;

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
            string uploadMessage = "";//Return status message for application
            int insertedCount = 0;//Counter for inserted records
            int duplicateCount = 0;//Counter for duplicate records
            int skippedCount = 0;//Counter for skipped records (Example: non-BRTF students should be skipped)
            HashSet<string> skippedPrograms = new HashSet<string>();

            try
            {
                ExcelPackage excel = new ExcelPackage();

                // Check the file extension of uploaded file
                if (theExcel.FileName.ToLower().Contains(".xlsx"))
                {
                    // If it is an Excel file, read it directly via memory stream
                    using (var memoryStream = new MemoryStream())
                    {
                        await theExcel.CopyToAsync(memoryStream);
                        excel = new ExcelPackage(memoryStream);
                    }
                }
                else if (theExcel.FileName.ToLower().Contains(".csv"))
                {
                    // If it is a .CSV, convert contents to a string, then load it into a new Worksheet

                    // Create StringBuilder to convert .CSV contents to string
                    var readResult = new StringBuilder();
                    using (var streamReader = new StreamReader(theExcel.OpenReadStream()))
                    {
                        while (streamReader.Peek() >= 0)
                        {
                            string currentLine = await streamReader.ReadLineAsync();// Read each row from .CSV line-by-line

                            // Check currentLine for the character " (quotation)
                            while (currentLine.Contains("\""))
                            {
                                // The character " (quotation) denotes a value that contains a , (comma)
                                // This is dangerous because the delimiter of a .CSV (comma-separated values) is a , (comma)
                                // EPPlus reading .CSV values is primitive, so the safest thing to do is replace the , (comma) with a ⸴ (comma symbol)

                                // Use Regex to find pattern that begins and ends with " (quotation)
                                var match = Regex.Match(currentLine, "\".*?\"");

                                // Pull only the value from the Regex match by removing the " (quotation)
                                // Then replace the , (comma) with a ⸴ (comma symbol)
                                string value = match.Value.Replace("\"", "").Replace(",", "٬");

                                // Inside the original line, replace the value with the same value where the , (comma) is replaced with a ٬ (comma symbol)
                                currentLine = currentLine.Replace(match.Value, value);
                            }

                            readResult.AppendLine(currentLine);
                        }
                    }
                    string theContents = readResult.ToString(); // Store StringBuilder's reading of .CSV contents into string

                    // Create a new WorkSheet
                    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Sheet 1");

                    // Load the CSV data into cell A1
                    worksheet.Cells["A1"].LoadFromText(theContents);
                }
                else
                {
                    // The uploaded file is neither an Excel file nor a .CSV file
                    // so throw an error
                    throw new Exception("Uploaded file is incorrect type.");
                }

                // Set the Worksheet that EPPlus will read values from
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                // Get existing Usernames to check for duplicates
                var existingUsers = (_context.Users
                    .Select(c => new { Username = c.Username }))
                    .ToList().Select(c => c.Username).ToHashSet();

                // Get an IQueryable all Terms and Programs
                var programs = from p in _context.TermAndPrograms
                               select p;

                // Get ID for Role = user
                int userRoleID = _context.Roles.Where(r => r.RoleName.ToUpper() == "USER").Select(r => r.ID).FirstOrDefault();

                // Loop to read values row-by-row starting from the 2nd row (We skip the 1st row which is only column headings)
                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    // Skip blank rows
                    if (String.IsNullOrEmpty(workSheet.Cells[row, 1].Text) || !Int32.TryParse(workSheet.Cells[row, 1].Text, out int testStuID)) continue;

                    // Look up student's Term and Program, or skip this student if their program is not found (Their program is not part of BRTF)

                    // Check if we have the specific combination of Program Code and Level already
                    var newUserProgram = programs.Where(p => p.ProgramCode.ToUpper().Equals(workSheet.Cells[row, 7].Text.ToUpper())
                                                && p.ProgramLevel.Equals(int.Parse(workSheet.Cells[row, 9].Text)));
                    int newUserProgID;

                    if (newUserProgram.Count() > 0)
                    {
                        //If we have matches, we grab the ID (FirstOrDefault as there should only be one result) so we can assign it on the User
                        newUserProgID = newUserProgram.FirstOrDefault().ID;
                    }
                    else
                    {
                        skippedCount++; // Skip this row if their program is not found (Their program is not part of BRTF)
                        skippedPrograms.Add(workSheet.Cells[row, 7].Text + " Level " + workSheet.Cells[row, 9].Text + (!string.IsNullOrEmpty(workSheet.Cells[row, 8].Text) ? " - " + workSheet.Cells[row, 8].Text : ""));
                        continue;
                    }

                    // Row by row...

                    // Check if Username already exists
                    string newUsername = workSheet.Cells[row, 1].Text;

                    if (existingUsers.Contains(newUsername))
                    {
                        // Update existing Users

                        // Get the User to update
                        var userToUpdate = await _context.Users.FirstOrDefaultAsync(p => p.Username == newUsername);

                        userToUpdate.FirstName = workSheet.Cells[row, 2].Text;
                        userToUpdate.MiddleName = workSheet.Cells[row, 3].Text;
                        userToUpdate.LastName = workSheet.Cells[row, 4].Text;
                        userToUpdate.TermAndProgramID = newUserProgID;
                        userToUpdate.Email = workSheet.Cells[row, 6].Text;
                        userToUpdate.Username = newUsername;

                        // Update the model in the database (Note: We don't Save Changes here. All changes will be saved at once at the end)
                        await TryUpdateModelAsync(userToUpdate);

                        duplicateCount++;
                    }
                    else
                    {
                        // Insert new Users

                        // For new Users, we set their password using their birthdate
                        // Excel stores dates in multiple ways, which are not easily parsed with a single method
                        // As such, we will first attempt to parse the date as a string, then if that fails, as a number
                        string dobCell = workSheet.Cells[row, 5].Text.Replace("٬", ",");    // In case the date is supposed to have a comma in it, turn the comma symbol back into a comma
                        DateTime dob = new DateTime();
                        if (!DateTime.TryParse(dobCell, out dob))//Attempt to parse cell contents as string
                        {
                            // It failed, so try parsing the cell contents as a number
                            if (Double.TryParse(dobCell, out double dobCellAsDouble))
                            {
                                dob = DateTime.FromOADate(dobCellAsDouble);
                            }
                            else
                            {
                                // DOB failed to be parsed as a string and as a number
                                // so throw an error
                                throw new Exception("DOB on row " + row.ToString() + " is not a readable format.");
                            }
                        }

                        // Using the student's DOB, we generate a password in the format of YEAR, MONTH, then DAY
                        // Example: October 1, 1990 would be "19901001"
                        string newPassword = dob.ToString("yyyyMMdd");

                        // Build User
                        User newUser = new User
                        {
                            FirstName = workSheet.Cells[row, 2].Text,
                            MiddleName = workSheet.Cells[row, 3].Text,
                            LastName = workSheet.Cells[row, 4].Text,
                            TermAndProgramID = newUserProgID,
                            Email = workSheet.Cells[row, 6].Text,
                            EmailBookingNotifications = true,
                            EmailCancelNotifications = true,
                            RoleID = userRoleID,
                            Username = newUsername,
                            Password = newPassword
                        };

                        // Insert User (Note: We don't Save Changes here. All changes will be saved at once at the end)
                        _context.Users.Add(newUser);

                        insertedCount++;
                    }
                }
                // Generate status message to return and save changes
                uploadMessage = insertedCount + " new Users were created.<br />"
                                + ((duplicateCount > 0) ? duplicateCount + " existing Users were updated.<br />" : "")
                                + ((skippedCount > 0) ? skippedCount + " student datas were skipped (Students whose Term and Program are not in the database get skipped).<br />The following Terms and Programs were skipped:<ul>" : "");
                foreach (string s in skippedPrograms)
                {
                    uploadMessage += "<li>" + s + "</li>";
                }
                uploadMessage += ((skippedCount > 0) ? "</ul>Please add these Terms and Programs if you wish to include them." : "");
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
                uploadMessage = "Failed to import data. Check that your selected file is in the correct format.";
            }
            TempData["Message"] = uploadMessage;
            return RedirectToAction("Index");
        }

        public IActionResult DownloadExampleExcel()
        {
            //Create a new spreadsheet from scratch.
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("ImportStudent");

                // Manually write contents as plain string
                var excelContents = new StringBuilder();
                excelContents.AppendLine("ID,First Name,Middle Name,Last Name,DOB,CAMP Email,Acad Plan,Description,Strt Level");
                excelContents.AppendLine("4362670,Cat,Sun,Al-Bahrani,10/1/1990,cstevens@ncstudents.niagaracollege.ca,P0122,Broadcasting: Radio٬ TV & Film,01");
                excelContents.AppendLine("9311011,Sterling,,Archer,8/22/2011,sarcher4@ncstudents.niagaracollege.ca,P0163,Presentation / Radio,01");
                excelContents.AppendLine("9508725,Matt,Elmi Johanna,Parker,2/23/2011,maparker1@ncstudents.niagaracollege.ca,P0164,TV Production,03");
                excelContents.AppendLine("4407393,James,Bullough,Lansing,5/22/2011,jblansing@ncstudents.niagaracollege.ca,P0165,Film Production,05");
                excelContents.AppendLine("4105233,Judy,Ugonna,Garland,8/22/2011,jgarland@ncstudents.niagaracollege.ca,P0198,Acting for TV & Film,02");
                excelContents.AppendLine("4242885,Marge,,Simpson,2/23/2011,masimpson@ncstudents.niagaracollege.ca,P0795,Digital Photography,01");
                excelContents.AppendLine("4035763,Sarah,Wing-Hay,Slean,5/22/2011,sslean13@ncstudents.niagaracollege.ca,P6801,Joint BSc Game Programming,04");
                excelContents.AppendLine("4444312,Morag,Leah-Grace,Smith,8/22/2011,msmith11@ncstudents.niagaracollege.ca,P6800,Join BA Game Design,06");
                excelContents.AppendLine("4398123,Akira,Kaur,Kurosawa,2/23/2011,akurosawa@ncstudents.niagaracollege.ca,P0441,Game Development,03");
                excelContents.AppendLine("9470695,Zhuo Chang,,Wu,2/23/2011,zcwu@ncstudents.niagaracollege.ca,P0474,CST – Network and Cloud Tech,03");

                // Load Excel contents into Worksheet starting at first cell of Worksheet
                workSheet.Cells["A1"].LoadFromText(excelContents.ToString());

                // Format the DOB column as date
                workSheet.Cells[1, 5, 11, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                //Autofit columns
                workSheet.Cells.AutoFitColumns();

                //Download the Excel

                //I usually stream the response back to avoid possible
                //out of memory errors on the server if you have a large spreadsheet.
                //NOTE: Since .NET Core 3 most Web Servers disallow sync IO so we
                //need to temporarily change the setting for the server.
                //If we can't then we will try to build the file and return a FileContentResult
                var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (syncIOFeature != null)
                {
                    syncIOFeature.AllowSynchronousIO = true;
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.Headers["content-disposition"] = "attachment;  filename=ImportStudent.xlsx";
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.Body);
                    }
                }
                else
                {
                    try
                    {
                        Byte[] theData = excel.GetAsByteArray();
                        string filename = "ImportStudent.xlsx";
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        return File(theData, mimeType, filename);
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
            }
            return NotFound();
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
