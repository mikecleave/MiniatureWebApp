using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Diagnostics;
using System.Text.Json;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiniatureWebApp.Data;
using MiniatureWebApp.Models;


namespace MiniatureWebApp.Pages.PowerStations
{
    public class MapModel : PageModel
    {
        private readonly MiniatureWebApp.Data.MiniatureWebAppContext _context;

        public MapModel(MiniatureWebApp.Data.MiniatureWebAppContext context)
        {
            _context = context;
        }
        public IList<PowerStation> PowerStation { get; set; } = default!;

        public async Task OnGetAsync()
        {
            PowerStation = await _context.PowerStations.ToListAsync();
            string powerStaionJsonString = JsonSerializer.Serialize(PowerStation);
            ViewData["powerStaionJsonString"] = powerStaionJsonString;

            Console.WriteLine(powerStaionJsonString);
            Debug.WriteLine(powerStaionJsonString);
        }
    }
}
