using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniatureWebApp.Data;
using MiniatureWebApp.Models;

namespace MiniatureWebApp.Pages.Inspections
{
    public class IndexModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public IndexModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }

        public IList<Inspection> Inspection { get; set; } = default!;
        public string NameSort { get; set; }
        public string DateSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public int powerStationIdFilter { get; set; }
        public string inspectorNameFilter { get; set; }
        public string statusFilter { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            if (_context.Inspections != null)
            {
                //If any of the options for PowerStation the filer are selected
                if (powerStationIdFilter != 0) {
                    //I need a way to recall everything in the OnGet since it is only called when the page loads. 
                    Console.WriteLine("Hello World!");
                    Debug.WriteLine("TESTINGGGGGG");
                    Debug.WriteLine(powerStationIdFilter);
                    //https://localhost:7046/Inspections?/PSId=Darlington

                    Inspection = await _context.Inspections
                        .Where(i => i.PowerStation.Id == powerStationIdFilter)
                        .Include(i => i.PowerStation).ToListAsync();
                }
                else if (inspectorNameFilter != null) {                    
                    Inspection = await _context.Inspections
                        .Where(i => i.InspectorName == inspectorNameFilter)
                        .Include(i => i.PowerStation).ToListAsync();
                }
                else {
                    Inspection = await _context.Inspections
                        .Include(i => i.PowerStation).ToListAsync();
                }


                //https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-6.0
                NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";

                IQueryable<Inspection> inspectionIQ = _context.Inspections
                    .Include(i => i.PowerStation);

                switch (sortOrder)
                {
                    case "name_desc":
                        inspectionIQ = inspectionIQ.OrderByDescending(s => s.PowerStation.Name);
                        break;
                    case "Date":
                        inspectionIQ = inspectionIQ.OrderBy(s => s.Date);
                        break;
                    case "date_desc":
                        inspectionIQ = inspectionIQ.OrderByDescending(s => s.Date);
                        break;
                    default:
                        inspectionIQ = inspectionIQ.OrderBy(s => s.PowerStation.Name);
                        break;
                }
                Inspection = await inspectionIQ.AsNoTracking().ToListAsync();
                                


                //I am pretty sure this line is no longer used. 
                ViewData["PowerStationNames"] = new SelectList(_context.PowerStations, "Id", "Name");


                //https://www.youtube.com/watch?v=lx72JkMVGqk&ab_channel=ASP.NETMVC
                //Filter by PowerStations
                var powerStations = _context.PowerStations.ToList();
                SelectList powerStationsSelectList = new SelectList(powerStations, "Id", "Name");
                ViewData["PowerStationSelectList"]= powerStationsSelectList;


                //Filter by Inspector Name
                List<string> inspectorNamesList = _context.Inspections
                    .Select(i => i.InspectorName)
                    .Distinct()
                    .ToList();

                List<SelectListItem> selectListItems = new List<SelectListItem>();
                foreach (var inspector in inspectorNamesList)
                {
                    SelectListItem selectListItem = new SelectListItem { Text = inspector, Value = inspector };
                    selectListItems.Add(selectListItem);
                }
                SelectList selectList = new SelectList(selectListItems, "Text", "Value");
                ViewData["InspectorNameList"] = selectList;
            }
        }
    }
}
