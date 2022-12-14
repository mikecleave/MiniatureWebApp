using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniatureWebApp.Data;
using MiniatureWebApp.Models;

namespace MiniatureWebApp.Pages.Inspections
{
    public class CreateModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public CreateModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string powerStationName)
        {
            if (powerStationName != null)
            {
                var selectedPowerStationName = _context.PowerStations
                    .Where(p => p.Name == powerStationName);
                ViewData["PowerStationId"] = new SelectList(selectedPowerStationName, "Id", "Name");
            }
            else
            {
                ViewData["PowerStationId"] = new SelectList(_context.PowerStations, "Id", "Name");
            }
            

            List<string> statusList = new List<string>() {"Please select a status", "PASS", "FAIL", "SCHEDULED" };
            ViewData["StatusList"] = new SelectList(statusList);
            return Page();
        }

        [BindProperty]
        public Inspection Inspection { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                ViewData["PowerStationId"] = new SelectList(_context.PowerStations, "Id", "Name");
                List<string> statusList = new List<string>() { "Please select a status", "PASS", "FAIL", "SCHEDULED" };
                ViewData["StatusList"] = new SelectList(statusList);
                return Page();
            }

            _context.Inspections.Add(Inspection);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
