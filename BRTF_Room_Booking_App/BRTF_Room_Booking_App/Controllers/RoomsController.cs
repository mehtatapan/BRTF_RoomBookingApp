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
using Microsoft.AspNetCore.Authorization;
using BRTF_Room_Booking_App.ViewModels;
using Microsoft.AspNetCore.Http.Features;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace BRTF_Room_Booking_App.Controllers
{
    [Authorize(Roles = "Top-level Admin, Admin")]
    public class RoomsController : Controller
    {
        private readonly BTRFRoomBookingContext _context;

        public RoomsController(BTRFRoomBookingContext context)
        {
            _context = context;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string SearchRoom, int? RoomGroupID, int? page, int? pageSizeID,
            string actionButton, string sortDirection = "asc", string sortField = "Is Enabled", string EnabledFilterString = "All")
        {
            //Clear the sort/filter/paging URL Cookie for Controller
            CookieHelper.CookieSet(HttpContext, ControllerName() + "URL", "", -1);

            //Toggle the open/closed state of the 'Filter/Sort' button based on if something is currently being filtered
            ViewData["Filtering"] = ""; //Assume nothing is filtered initially

            PopulateDropDownLists(); //data for Room Filter DDL

            // Start with Includes but make sure your expression returns an
            // IQueryable<> so we can add filter and sort 
            // options later.
            var rooms = from t in _context.Rooms
                .Include(t => t.RoomGroup).Where(t =>
            (t.Enabled == true && EnabledFilterString == "True") || (t.Enabled == false && EnabledFilterString == "False") || (EnabledFilterString == "All"))
                .AsNoTracking()
                        select t;

            //  Filtering
            if (EnabledFilterString != "All")
            {
                ViewData["Filtering"] = " show ";
            }
            if (RoomGroupID.HasValue)
            {
                rooms = rooms.Where(r => r.RoomGroupID == RoomGroupID);
                ViewData["Filtering"] = " show ";
            }
            if (!String.IsNullOrEmpty(SearchRoom))
            {
                rooms = rooms.Where(r => r.RoomName.ToUpper().Contains(SearchRoom.ToUpper()));
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

            //Now we know which field and direction to sort by
            if (sortField == "Room")  //Sorting by Room Name
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms
                        .OrderBy(r => r.RoomName);
                }
                else
                {
                    rooms = rooms
                        .OrderByDescending(r => r.RoomName);
                }
            }
            else if (sortField == "Area") //Sorting by Area
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms
                        .OrderBy(r => r.RoomGroup);
                }
                else
                {
                    rooms = rooms
                        .OrderByDescending(r => r.RoomGroup);
                }
            }
            else //Sorting by Enabled
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms
                        .OrderByDescending(r => r.Enabled);
                }
                else
                {
                    rooms = rooms
                        .OrderBy(r => r.Enabled);
                }
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);

            var pagedData = await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

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
            PopulateDropDownLists(room);
            return View(room);
        }

        // GET: Room/Create
        public IActionResult Create()
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            PopulateDropDownLists();
            return View();
        }

        // POST: Room/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RoomName,RoomMaxHoursTotal,RoomGroupID,Enabled")] Room room)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(room);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Room created successfully!";
                    return RedirectToAction("Index");
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
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

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
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

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
                p => p.RoomGroupID, p => p.Enabled))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["Message"] = "Room edited successfully!";
                    return RedirectToAction("Details", new { roomToUpdate.ID });
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
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

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
            PopulateDropDownLists(room);
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //URL with the last filter, sort and page parameters for this controller
            ViewDataReturnURL();

            var room = await _context.Rooms
                .Include(t => t.RoomGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            try
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Room deleted successfully!";
                return Redirect(ViewData["returnURL"].ToString());
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

        //public IActionResult BookingSummary()
        //{
        //    //previous version
        //    var sumQ = _context.RoomBookings.Include(a => a.Room).ThenInclude(a=>a.RoomGroupID)
        //        .GroupBy(a => new { a.RoomID, a.Room.RoomName })
        //        .Select(grp => new BookingSummary
        //        {
        //            ID = grp.Key.RoomID,
        //            RoomName = grp.Key.RoomName,
        //            NumberOfAppointments = grp.Count(),
        //             //TotalHours= grp.Sum(a=> a.)
        //             TotalHours= grp.Sum(a=>a.)


        //        });
        //    return View(sumQ.AsNoTracking().ToList());
        //   // return View(_context.BookingSummaries.AsNoTracking().ToList());
        //}
        public IActionResult BookingSummary(DateTime? start, DateTime? end, string? SearchRoom, int? RoomGroupID)

        {
            ViewData["RoomGroupID"] = RoomGroupSelectList(RoomGroupID);
            //The next two lines should be removed as they
            //were just so I could test some date ranges without
            //passing values to the paraneters
            //start ??= DateTime.Parse("1700-03-01");
            //end ??= DateTime.Parse("2025-03-20");
            var filtered = _context.RoomBookings.Include(a => a.Room).ThenInclude(a => a.RoomGroup)
               .Where(a => a.StartDate >= start && a.EndDate <= end)
               .AsNoTracking()
               .ToList();
            if (start == null && end == null)
            {
                filtered = _context.RoomBookings.Include(a => a.Room).ThenInclude(a => a.RoomGroup)
               // .Where(a => a.StartDate >= start && a.EndDate <= end)
               .AsNoTracking()
               .ToList();

            }
            else if (start == null && end != null)
            {
                filtered = _context.RoomBookings.Include(a => a.Room).ThenInclude(a => a.RoomGroup)
                .Where(a => a.EndDate <= end)
               .AsNoTracking()
               .ToList();
            }
            else if (start != null && end == null)
            {
                filtered = _context.RoomBookings.Include(a => a.Room).ThenInclude(a => a.RoomGroup)
                .Where(a => a.StartDate >= start)
               .AsNoTracking()
               .ToList();
            }
            if (!String.IsNullOrEmpty(SearchRoom))
            {
                filtered = _context.RoomBookings.Include(a => a.Room).ThenInclude(a => a.RoomGroup)
               .Where(r => r.Room.RoomName.ToUpper().Contains(SearchRoom.ToUpper()))
              .AsNoTracking()
              .ToList();

            }
            if (RoomGroupID.HasValue)
            {
                filtered = _context.RoomBookings.Include(a => a.Room).ThenInclude(a => a.RoomGroup)
             .Where(r => r.Room.RoomGroupID == RoomGroupID)
             .AsNoTracking()
             .ToList();

            }



            //Now do the grouping
            var sumQ = filtered
                .GroupBy(a => new { a.RoomID, a.Room.RoomName, a.Room.RoomGroup.AreaName })
                .Select(grp => new BookingSummary
                {
                    ID = grp.Key.RoomID,
                    RoomName = grp.Key.RoomName,
                    NumberOfAppointments = grp.Count(),
                    TotalHours = (int)grp.Sum(a => a.EndDate.Subtract(a.StartDate).TotalHours),
                    RoomGroup = grp.Key.AreaName

                });



            return View(sumQ.ToList());

            //return View(_context.BookingSummaries.AsNoTracking().ToList());

        }

        [Authorize(Roles = "Admin")]
        public IActionResult DownloadBookings()
        {
            //Get the appointments
            var appts = from a in _context.BookingSummaries
                        .Include(a => a.RoomName)
                        .Include(a => a.NumberOfAppointments)
                        .Include(a => a.TotalHours)
                        orderby a.NumberOfAppointments
                        select new
                        {
                            Room = a.RoomName,
                            NumberOfAppointments = a.NumberOfAppointments,
                            TotalHours = a.TotalHours

                        };
            //How many rows?
            int numRows = appts.Count();

            if (numRows > 0) //We have data
            {
                //Create a new spreadsheet from scratch.
                using (ExcelPackage excel = new ExcelPackage())
                {

                    //Note: you can also pull a spreadsheet out of the database if you
                    //have saved it in the normal way we do, as a Byte Array in a Model
                    //such as the UploadedFile class.
                    //
                    // Suppose...
                    //
                    // var theSpreadsheet = _context.UploadedFiles.Include(f => f.FileContent).Where(f => f.ID == id).SingleOrDefault();
                    //
                    //    //Pass the Byte[] FileContent to a MemoryStream
                    //
                    // using (MemoryStream memStream = new MemoryStream(theSpreadsheet.FileContent.Content))
                    // {
                    //     ExcelPackage package = new ExcelPackage(memStream);
                    // }

                    var workSheet = excel.Workbook.Worksheets.Add("BookingSummaries");//RoomBookings?

                    //Note: Cells[row, column]
                    workSheet.Cells[3, 1].LoadFromCollection(appts, true);

                    //Set Style and backgound colour of headings
                    //using (ExcelRange headings = workSheet.Cells[3, 1, 3, 7])
                    //{
                    //    headings.Style.Font.Bold = true;
                    //    var fill = headings.Style.Fill;
                    //    fill.PatternType = ExcelFillStyle.Solid;
                    //    fill.BackgroundColor.SetColor(Color.LightBlue);
                    //}



                    //Autofit columns
                    workSheet.Cells.AutoFitColumns();
                    //Note: You can manually set width of columns as well
                    //workSheet.Column(7).Width = 10;

                    //Add a title and timestamp at the top of the report
                    workSheet.Cells[1, 1].Value = "Booking Report";
                    using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 6])
                    {
                        Rng.Merge = true; //Merge columns start and end range
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 18;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }


                    //Ok, time to download the Excel

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
                            Response.Headers["content-disposition"] = "attachment;  filename=Bookings.xlsx";
                            excel.SaveAs(memoryStream);
                            memoryStream.WriteTo(Response.Body);
                        }
                    }
                    else
                    {
                        try
                        {
                            Byte[] theData = excel.GetAsByteArray();
                            string filename = "Bookings.xlsx";
                            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            return File(theData, mimeType, filename);
                        }
                        catch (Exception)
                        {
                            return NotFound();
                        }
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
        private SelectList RoomGroupSelectList(int? selectedId)
        {
            return new SelectList(_context.RoomGroups
                .OrderBy(u => u.AreaName), "ID", "AreaName", selectedId);
        }

        private SelectList EnabledSelectList(int? selectedId)
        {
            return new SelectList(_context.RoomGroups
                .OrderBy(u => u.Enabled), "ID", "Enabled", selectedId);
        }

        private void PopulateDropDownLists(Room room = null)
        {
            ViewData["RoomGroupID"] = RoomGroupSelectList(room?.RoomGroupID);
            ViewData["Enabled"] = EnabledSelectList(room?.RoomGroupID);

        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        private void ViewDataReturnURL()
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, ControllerName());
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }

    }
}
