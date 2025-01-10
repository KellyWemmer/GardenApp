using GardenApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenApp.DataAccess
{
    public class ApplicationDbContext:DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration): base(options)
        {
            _configuration = configuration;
        }
        public DbSet<PlantInfoModel> PlantInfo { get; set; }    
        public DbSet<PlantStartModel> PlantStart { get; set; }
        public DbSet<PlantSuccessModel> PlantSuccess { get; set; }
        public DbSet<YearModel> Year { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer();
            }
         
        }
    }
}
