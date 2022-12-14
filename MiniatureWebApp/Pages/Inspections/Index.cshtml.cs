using System;
using System.Collections.Generic;
using System.Diagnostics; //Need this to use Debug.WriteLine("Hello World!");
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
        public IList<Inspection> Inspections { get; set; } = default!; //Method Syntax

        public async Task OnGetAsync(string sortOrder, string powerStationNameFilter, string inspectorNameFilter, string statusFilter)
        {
            if (_context.Inspections != null)
            {
                //Debug.WriteLine("Hello World!");
                IQueryable<Inspection> inspectionIQ = _context.Inspections
                    .Include(i => i.PowerStation);

                //If any of the filters for PowerStation are selected
                if (powerStationNameFilter != null) {
                    ViewData["selectedPowerStationNameFilter"] = powerStationNameFilter;
                    inspectionIQ = inspectionIQ
                        .Where(i => i.PowerStation.Name == powerStationNameFilter);
                }
                
                if (inspectorNameFilter != null) {
                    ViewData["selectedInspectorNameFilter"] = inspectorNameFilter;
                    inspectionIQ = inspectionIQ
                        .Where(i => i.InspectorName == inspectorNameFilter);                        
                }
                
                if (statusFilter != null)
                {
                    Status thisStatus = (Status)Enum.Parse(typeof(Status), statusFilter.ToString());
                    if (statusFilter != null)
                    {
                        inspectionIQ = inspectionIQ
                            .Where(i => i.Status == thisStatus);
                    }
                }

                //https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-6.0
                Debug.WriteLine(sortOrder + "TESTING");
                                
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
                //use .ToList only at the end to convert it back from a queryable object to a model object. 
                Inspections = await inspectionIQ.AsNoTracking().Take(100).ToListAsync();          

                ViewData["PowerStationNames"] = new SelectList(_context.PowerStations, "Id", "Name");

                //https://www.youtube.com/watch?v=lx72JkMVGqk&ab_channel=ASP.NETMVC
                //Create the dropdown filter for PowerStations
                var powerStations = _context.PowerStations.ToList();
                SelectList powerStationsSelectList = new SelectList(powerStations, "Name", "Name");
                ViewData["PowerStationSelectList"]= powerStationsSelectList;

                //Create the dropdown filter for Inspector Name
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
