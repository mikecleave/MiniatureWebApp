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
    public class DetailsModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public DetailsModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }

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
    }
}
