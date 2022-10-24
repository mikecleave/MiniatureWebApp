using System;
using System.Collections.Generic;
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


        public async Task OnGetAsync()
        {
            if (_context.Inspections != null)
            {

                ViewData["PowerStationNames"] = new SelectList(_context.PowerStations, "Id", "Name");
                
                List<string> statusList = new List<string>() { "Please select a status", "PASS", "FAIL", "SCHEDULED" };

                Inspection = await _context.Inspections
                    //.Where(i => i.PowerStation.Id == 1)
                    .Include(i => i.PowerStation).ToListAsync();


                //https://www.youtube.com/watch?v=lx72JkMVGqk&ab_channel=ASP.NETMVC
                //Filter by PowerStations
                var getPowerStations = _context.PowerStations.ToList();
                SelectList PowerStations = new SelectList(getPowerStations, "Id", "Name");
                ViewData["PowerStationList"]= PowerStations;

                //Filter by Inspector Name
                var getInspectorName = _context.Inspections.Distinct().ToList(); //.Distinct() does not seem to be working...
                SelectList InspectorNames = new SelectList(getInspectorName, "Id", "InspectorName");
                ViewData["InspectorNameList"] = InspectorNames;

                //Filter by Status
                var getStatus= _context.Inspections.Distinct().ToList(); //.Distinct() does not seem to be working...
                SelectList Status = new SelectList(getStatus, "Id", "Status");
                ViewData["StatusList"] = Status;
            }
        }
    }
}
