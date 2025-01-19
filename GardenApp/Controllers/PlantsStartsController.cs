using GardenApp.DataAccess;
using GardenApp.Models;
using GardenApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GardenApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PlantsStartsController : ControllerBase
    {
        //private static List<PlantStartModel> _plantStarts = new List<PlantStartModel>();

        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly PlantsStartsService _plantStartsService;

        public PlantsStartsController(ApplicationDbContext context, ILogger<PlantsStartsController> logger, PlantsStartsService plantStartService)
        {
            _context = context;
            _logger = logger;
            _plantStartsService = plantStartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plantStarts = await _plantStartsService.GetPlantStarts();
            if (plantStarts == null || !plantStarts.Any())
            {
                _logger.LogWarning("No plantStarts found");
                return NotFound();
            }

            _logger.LogInformation("{Count} plantStarts found", plantStarts.Count());
            return Ok(plantStarts);
        }

        [HttpGet("PlantStart/{id}")]
        public async Task<IActionResult> GetPlantStart(int id)
        {
            var plantStart = await _plantStartsService.GetPlantStart(id);
            if (plantStart == null)
            {
                _logger.LogWarning("No plantStart found for plantStartId: {id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returning plantStart for plantStartId: {id}", id);
            return Ok(plantStart);
        }
        //Get by recommended dates
        [HttpGet("RecYear/{year}")]
        public async Task<IActionResult> GetPlantStartByRecommendedStartYear(int year)
        {
            var plantStartsByYear = await _plantStartsService.GetPlantStartsByRecommendedStartYear(year);
            if (plantStartsByYear == null)
            {
                _logger.LogWarning("No plantStarts found for recommended start year: {Year}", year);
                return NotFound();
            }
            else
            {
                _logger.LogInformation("{Count} plantStarts returned for recommended start year: {Year}", plantStartsByYear.Count(), year);
                return Ok(plantStartsByYear);
            }
        }

        [HttpGet("RecMonth/{month}")]
        public async Task<IActionResult> GetPlantStartsByRecommendedStartMonth(int month)
        {
            var plantStartsByMonth = await _plantStartsService.GetPlantStartsByRecommendedStartMonth(month);
            if (plantStartsByMonth == null || !plantStartsByMonth.Any())
            {
                _logger.LogWarning("No plantStarts found for recommended start month: {Month}", month);
                return NotFound();
            }
            else
            {
                _logger.LogInformation("{Count} plantStarts were found for recommended start month: {Month}", plantStartsByMonth.Count(), month);
                return Ok(plantStartsByMonth);
            }
        }
        //Get by actual dates
        [HttpGet("ActYear/{year}")]
        public async Task<IActionResult> GetPlantStartByActualStartYear(int year)
        {
            var plantStartsByYear = await _plantStartsService.GetPlantStartsByActualStartYear(year);
            if (plantStartsByYear == null)
            {
                _logger.LogWarning("No plantStarts found for actual start year: {Year}", year);
                return NotFound();
            }
            else
            {
                _logger.LogInformation("{Count} plantStarts returned for actual start year: {Year}", plantStartsByYear.Count(), year);
                return Ok(plantStartsByYear);
            }
        }

        [HttpGet("ActMonth/{month}")]
        public async Task<IActionResult> GetPlantStartsByActualStartMonth(int month)
        {
            var plantStartsByMonth = await _plantStartsService.GetPlantStartsByActualStartMonth(month);
            if (plantStartsByMonth == null || !plantStartsByMonth.Any())
            {
                _logger.LogWarning("No plantStarts found for actual start month: {Month}", month);
                return NotFound();
            }
            else
            {
                _logger.LogInformation("{Count} plantStarts were found for actual start month: {Month}", plantStartsByMonth.Count(), month);
                return Ok(plantStartsByMonth);
            }
        }

        [HttpGet("Method/{preferredMethod}")]
        public async Task<IActionResult> GetPlantStartByPreferredMethod(bool preferredMethod)
        {
            List<PlantStartModel> preferredPlantStartMethods = await _context.PlantStart.Where(p => p.IsPreferredMethod == preferredMethod).ToListAsync();
            if (preferredPlantStartMethods == null || !preferredPlantStartMethods.Any())
            {
                _logger.LogWarning("No plantStarts preferred methods were found");
                return NotFound();
            }
            else
            {
                _logger.LogInformation("{Count} plantStarts found for preferredMethod", preferredPlantStartMethods.Count());
                return Ok(preferredPlantStartMethods);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlantStart([FromBody] PlantStartModel newPlantStart)
        {
            try
            {
                await _context.PlantStart.AddAsync(newPlantStart);
                await _context.SaveChangesAsync();
                //newPlantStart.Id = _plantStarts.Any() ? _plantStarts.Max(x => x.Id) + 1 : 1;
                //_plantStarts.Add(newPlantStart);
                _logger.LogInformation("Successfully added newPlantStart: {@NewPlantStart}", newPlantStart);
                return CreatedAtAction(nameof(GetPlantStart), new { id = newPlantStart.Id }, newPlantStart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating newPlantStart:{@NewPlantStart}", newPlantStart);
                return StatusCode(500, "An error occurred while creating newPlantStart");
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlantStart(int id, [FromBody] PlantStartModel plantStartToUpdate)
        {
            PlantStartModel? existingPlantStart = _context.PlantStart.FirstOrDefault(i  => i.Id == id); 
            if (existingPlantStart == null)
            {
                _logger.LogWarning("No plantStart found to update for Id: {id}", id);
                return NotFound();
            }
            existingPlantStart.PlantInfoId = plantStartToUpdate.PlantInfoId;
            existingPlantStart.RecommendedIndoorStartDate = plantStartToUpdate.RecommendedIndoorStartDate;
            existingPlantStart.ActualIndoorStartDate = plantStartToUpdate.ActualIndoorStartDate;
            existingPlantStart.SeedlingEnvironment = plantStartToUpdate.SeedlingEnvironment;
            existingPlantStart.GerminationRate = plantStartToUpdate.GerminationRate;
            existingPlantStart.Issues = plantStartToUpdate.Issues;
            existingPlantStart.IssuesFixes = plantStartToUpdate.IssuesFixes;
            existingPlantStart.IsPreferredMethod = plantStartToUpdate.IsPreferredMethod;

            _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantStart(int id)
        {
            PlantStartModel? plantStartToDelete = await _context.PlantStart.FirstOrDefaultAsync(i => i.Id == id);  
            if (plantStartToDelete == null)
            {
                _logger.LogWarning("No plantStart found to delete for Id; {id}", id);
                return NotFound();
            }
            _context.PlantStart.Remove(plantStartToDelete);
            await _context.SaveChangesAsync();
            _logger.LogInformation("plantStart was deleted for Id: {id}", id);
            return NoContent();
        }







    }
}
