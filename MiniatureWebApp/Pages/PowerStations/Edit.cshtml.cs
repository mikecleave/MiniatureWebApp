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

namespace MiniatureWebApp.Pages.PowerStations
{
    public class EditModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public EditModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PowerStation PowerStation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PowerStations == null)
            {
                return NotFound();
            }

            var powerstation = await _context.PowerStations.FirstOrDefaultAsync(m => m.Id == id);
            if (powerstation == null)
            {
                return NotFound();
            }
            PowerStation = powerstation;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PowerStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PowerStationExists(PowerStation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PowerStationExists(int id)
        {
          return _context.PowerStations.Any(e => e.Id == id);
        }
    }
}
