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

        public IList<Inspection> Inspections { get; set; } = default!;
        public string NameSort { get; set; }
        public string DateSort { get; set; }


        [BindProperty(SupportsGet = true)]
        public string inspectorNameFilter { get; set; }
        [BindProperty(SupportsGet = true)] 
        public string statusFilter { get; set; }

        public async Task OnGetAsync(string sortOrder, int powerStationIdFilter)
        {
            if (_context.Inspections != null)
            {


                IQueryable<Inspection> inspectionIQ = _context.Inspections
                    .Include(i => i.PowerStation);

                //If any of the filters for PowerStation are selected
                if (powerStationIdFilter != 0) {
                    ViewData["selectedPowerStationIdFilter"] = powerStationIdFilter;

                    inspectionIQ = _context.Inspections
                        .Where(i => i.PowerStationId==powerStationIdFilter)
                        .Include(i => i.PowerStation);
                }
                else if (inspectorNameFilter != null)
                {
                    inspectionIQ = _context.Inspections
                        .Where(i => i.InspectorName==inspectorNameFilter)
                        .Include(i => i.InspectorName);
                }
                else if (statusFilter != null)
                {
                    inspectionIQ = _context.Inspections
                        //.Where(i => i.Status==statusFilter)
                        .Include(i => i.Status);
                }


                //https://learn.microsoft.com/en-us/aspnet/core/data/ef-rp/sort-filter-page?view=aspnetcore-6.0
                NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                DateSort = sortOrder == "Date" ? "date_desc" : "Date";

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
                Inspections = await inspectionIQ.AsNoTracking().ToListAsync();          


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
