using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IList<Inspection> Inspection { get;set; } = default!;


        [BindProperty(SupportsGet = true)]
        public int PSId { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.Inspections != null)
            {
                //I need a way to recall everything in the OnGet since it is only called when the page loads. 
                Console.WriteLine("Hello World!");
                Debug.WriteLine("TESTINGGGGGG");
                Debug.WriteLine(PSId);
                //https://localhost:7046/Inspections?/PSId=Darlington
                Inspection = await _context.Inspections
                    .Where(i => i.PowerStation.Id == PSId) //Need to find a way to update this so it filters by the selection in the dropdown.
                    .Include(i => i.PowerStation).ToListAsync();

                ViewData["PowerStationNames"] = new SelectList(_context.PowerStations, "Id", "Name");

                //List<string> statusList = new List<string>() { "Please select a status", "PASS", "FAIL", "SCHEDULED" };

                


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
