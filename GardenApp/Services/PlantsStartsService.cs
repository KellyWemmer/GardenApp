using GardenApp.DataAccess;
using GardenApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GardenApp.Services
{
    public class PlantsStartsService
    {
        private readonly ApplicationDbContext _context;

        public PlantsStartsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlantStartModel>> GetPlantStarts()
        {
            return await _context.PlantStart.ToListAsync(); 
        }

        public async Task<PlantStartModel?> GetPlantStart(int id)
        {
            return await _context.PlantStart.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PlantStartModel>> GetPlantStartsByYear(int year)
        {
            return await _context.PlantStart.Where(p => p.ActualIndoorStartDate.Year == year).ToListAsync();
        }

        public async Task<IEnumerable<PlantStartModel>> GetPlantStartsByMonth(int month)
        {
            return await _context.PlantStart.Where(p => p.ActualIndoorStartDate.Month == month).ToListAsync();
        }


    }
}
