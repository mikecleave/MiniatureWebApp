using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniatureWebApp.Data;
using MiniatureWebApp.Models;

namespace MiniatureWebApp.Pages.PowerStations
{
    public class DeleteModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public DeleteModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public PowerStation PowerStation { get; set; }

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
            else 
            {
                PowerStation = powerstation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PowerStations == null)
            {
                return NotFound();
            }
            var powerstation = await _context.PowerStations.FindAsync(id);

            if (powerstation != null)
            {
                PowerStation = powerstation;
                _context.PowerStations.Remove(PowerStation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
