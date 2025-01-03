using GardenApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PlantStartsController : ControllerBase
    {
        private static List<PlantStartModel> _plantStarts = new List<PlantStartModel>();

        private readonly ILogger _logger;

        public PlantStartsController(ILogger<PlantStartsController> logger)
        {
            _logger = logger;
        }

        //Need getByYear
        //Need getByRecommendedStartDate or ActualStartDate?
        //Need to getByPreferredMethod = True?

        [HttpGet]
        public ActionResult<List<PlantStartModel>> GetPlantStarts()
        {
            return Ok(_plantStarts);
        }

        [HttpGet("Id")]
        public ActionResult<PlantStartModel> GetPlantStart(int id)
        {
            PlantStartModel? plantStart = _plantStarts.FirstOrDefault(x => x.Id == id);
            if (plantStart == null)
            {
                _logger.LogWarning("No plantStarts found for plantStartId: {id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returning plantStart for plantStartId: {id}", id);
            return Ok(plantStart);
        }

        [HttpGet("{YearId}")]
        public ActionResult<List<PlantStartModel>> GetPlantStartByYear(int yearId)
        {
            List<PlantStartModel> plantStartsByYear = _plantStarts.Where(p => p.YearId == yearId).ToList();
            if (!plantStartsByYear.Any())
            {
                _logger.LogWarning("No plantStarts found for YearId: yearId", yearId);
                return NotFound();
            }
            _logger.LogInformation("{Count} plantStarts returned for YearId: {yearId}", yearId);
            return Ok(plantStartsByYear);
        }

        [HttpPost]
        public ActionResult<PlantStartModel> CreatePlantStart(PlantStartModel newPlantStart)
        {
            newPlantStart.Id = _plantStarts.Any() ? _plantStarts.Max(x => x.Id) + 1 : 1;
            _plantStarts.Add(newPlantStart);
            return CreatedAtAction(nameof(GetPlantStart), new { id = newPlantStart.Id }, newPlantStart);
        }

        [HttpPut("Id")]
        public ActionResult<PlantStartModel> UpdatePlantStart(int id, PlantStartModel plantStartToUpdate)
        {
            PlantStartModel? existingPlantStart = _plantStarts.FirstOrDefault(i  => i.Id == id); 
            if (existingPlantStart == null)
            {
                return NotFound();
            }
            existingPlantStart.PlantInfoId = plantStartToUpdate.PlantInfoId;
            existingPlantStart.YearId = plantStartToUpdate.YearId;
            existingPlantStart.RecommendedStartDate = plantStartToUpdate.RecommendedStartDate;
            existingPlantStart.ActualStartDate = plantStartToUpdate.ActualStartDate;
            existingPlantStart.SeedlingEnvironment = plantStartToUpdate.SeedlingEnvironment;
            existingPlantStart.GerminationRate = plantStartToUpdate.GerminationRate;
            existingPlantStart.Issues = plantStartToUpdate.Issues;
            existingPlantStart.IssuesFixes = plantStartToUpdate.IssuesFixes;
            existingPlantStart.IsPreferredMethod = plantStartToUpdate.IsPreferredMethod;

            return NoContent();
        }

        [HttpDelete("Id")]
        public ActionResult<PlantStartModel> DeletePlantStart(int id)
        {
            PlantStartModel? plantStartToDelete = _plantStarts.FirstOrDefault(i => i.Id == id);  
            if (plantStartToDelete == null)
            {
                return NotFound();
            }
            _plantStarts.Remove(plantStartToDelete);
            return NoContent();
        }







    }
}
