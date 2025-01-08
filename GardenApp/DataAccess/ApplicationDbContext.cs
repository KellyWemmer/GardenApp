using GardenApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenApp.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<PlantInfoModel> PlantInfo { get; set; }    
        public DbSet<PlantStartModel> PlantStart { get; set; }
        public DbSet<PlantSuccessModel> PlantSuccess { get; set; }
        public DbSet<YearModel> Year { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
        }
    }
}
