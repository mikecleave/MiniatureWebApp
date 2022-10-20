using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiniatureWebApp.Models;

namespace MiniatureWebApp.Data
{
    public class MiniatureWebAppContext : DbContext
    {
        public MiniatureWebAppContext (DbContextOptions<MiniatureWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<MiniatureWebApp.Models.PowerStation> PowerStations { get; set; } = default!;

        public DbSet<MiniatureWebApp.Models.Inspection> Inspections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PowerStation>().ToTable("PowerStation");
            modelBuilder.Entity<Inspection>().ToTable("Inspection");
        }
    }
}
