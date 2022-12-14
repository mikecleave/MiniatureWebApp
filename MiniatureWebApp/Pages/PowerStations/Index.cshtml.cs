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
    public class IndexModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public IndexModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }

        public IList<PowerStation> PowerStation { get;set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.PowerStations != null)
            {
                var powerStations = from p in _context.PowerStations
                             select p;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    powerStations = powerStations
                        .Where(p => p.Name.Contains(SearchString));
                }
                PowerStation = await powerStations.ToListAsync();
            }
        }
    }
}
