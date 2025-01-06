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

        //Need a list of All years to be able to search them
        private readonly List<YearModel> _yearModels = new List<YearModel>();

        //Need to getByPreferredMethod = True?

        [HttpGet]
        public IActionResult GetPlantStarts()
        {
            if (_plantStarts == null || !_plantStarts.Any())
            {
                _logger.LogWarning("No plantStarts found");
                return NotFound();
            }

            _logger.LogInformation("{Count} plantStarts found", _plantStarts.Count());
            return Ok(_plantStarts);
        }

        [HttpGet("PlantStart/{id}")]
        public IActionResult GetPlantStart(int id)
        {
            PlantStartModel? plantStart = _plantStarts.FirstOrDefault(x => x.Id == id);
            if (plantStart == null)
            {
                _logger.LogWarning("No plantStart found for plantStartId: {id}", id);
                return NotFound();
            }
            _logger.LogInformation("Returning plantStart for plantStartId: {id}", id);
            return Ok(plantStart);
        }

        [HttpGet("Year/{yearId}")]
        public IActionResult GetPlantStartByYear(int yearId)
        {
            List<PlantStartModel> plantStartsByYear = _plantStarts.Where(p => p.YearId == yearId).ToList();
            YearModel? yearModel = _yearModels.FirstOrDefault(y => y.Id == yearId);
            if (plantStartsByYear == null || !plantStartsByYear.Any())
            {
                if(yearModel != null)
                {
                    //No plantStarts found for specific year
                    _logger.LogWarning("No plantStarts found for Year: {@PlantStarts}", _plantStarts);
                    return NotFound();
                }
                else
                {
                    _logger.LogWarning("No year found for {@YearModels}", _yearModels);
                    return NotFound();
                }
            }
            else
            {
                //There are plantStarts found
                if (yearModel != null)
                {
                _logger.LogInformation("{Count} plantStarts returned for YearId: {@PlantStarts}", _plantStarts.Count());
                return Ok(plantStartsByYear);

                }
                else
                {
                    //Year not found
                    _logger.LogWarning("No year found for {@YearModels}", _yearModels);
                    return NotFound(plantStartsByYear);
                }

            }
        }

        [HttpGet("Month/{month}")]
        public IActionResult GetPlantStartsByMonth(int month)
        {
            List<PlantStartModel> plantStartsByMonth = _plantStarts.Where(p => p.ActualIndoorStartDate.Month == month).ToList();
            if (plantStartsByMonth == null || !plantStartsByMonth.Any())
            {
                _logger.LogWarning("No plantStarts found for month {Month}", month);
                return NotFound();
            }
            else
            {
                _logger.LogInformation("{Count} plantStarts were found for month: month", plantStartsByMonth.Count);
                return Ok(plantStartsByMonth);
            }
        }

        [HttpGet("Method/{preferredMethod}")]
        public IActionResult GetPlantStartByPreferredMethod(bool preferredMethod)
        {
            List<PlantStartModel> preferredPlantStartMethods = _plantStarts.Where(p => p.IsPreferredMethod == true).ToList();
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
        public IActionResult CreatePlantStart([FromBody] PlantStartModel newPlantStart)
        {

            try
            {
                newPlantStart.Id = _plantStarts.Any() ? _plantStarts.Max(x => x.Id) + 1 : 1;
                _plantStarts.Add(newPlantStart);
                _logger.LogInformation("Successfully added newPlantStart: {@NewPlantStart}", newPlantStart);
                return CreatedAtAction(nameof(GetPlantStart), new { id = newPlantStart.Id }, newPlantStart);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating newPlantStart:{@NewPlantStart", newPlantStart);
                return StatusCode(500, "An error occurred while creating newPlantStart");
               
            }

        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlantStart(int id, [FromBody] PlantStartModel plantStartToUpdate)
        {
            PlantStartModel? existingPlantStart = _plantStarts.FirstOrDefault(i  => i.Id == id); 
            if (existingPlantStart == null)
            {
                _logger.LogWarning("No plantStart found to update for Id: {id}", id);
                return NotFound();
            }
            existingPlantStart.PlantInfoId = plantStartToUpdate.PlantInfoId;
            existingPlantStart.YearId = plantStartToUpdate.YearId;
            existingPlantStart.RecommendedIndoorStartDate = plantStartToUpdate.RecommendedIndoorStartDate;
            existingPlantStart.ActualIndoorStartDate = plantStartToUpdate.ActualIndoorStartDate;
            existingPlantStart.SeedlingEnvironment = plantStartToUpdate.SeedlingEnvironment;
            existingPlantStart.GerminationRate = plantStartToUpdate.GerminationRate;
            existingPlantStart.Issues = plantStartToUpdate.Issues;
            existingPlantStart.IssuesFixes = plantStartToUpdate.IssuesFixes;
            existingPlantStart.IsPreferredMethod = plantStartToUpdate.IsPreferredMethod;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlantStart(int id)
        {
            PlantStartModel? plantStartToDelete = _plantStarts.FirstOrDefault(i => i.Id == id);  
            if (plantStartToDelete == null)
            {
                _logger.LogWarning("No plantStart found to delete for Id; {id}", id);
                return NotFound();
            }
            _plantStarts.Remove(plantStartToDelete);
            _logger.LogInformation("plantStart was deleted for Id: {id}", id);
            return NoContent();
        }







    }
}
