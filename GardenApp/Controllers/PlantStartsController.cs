using GardenApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PlantStartsController : ControllerBase
    {
        private static List<PlantStartModel> _plantStarts = new List<PlantStartModel>();

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
            var plantStart = _plantStarts.FirstOrDefault(x => x.Id == id);
            if (plantStart == null)
            {
                return NotFound();
            }
            return Ok(plantStart);
        }

        [HttpPost]
        public ActionResult<PlantStartModel> CreatePlantStart(PlantStartModel newPlantStart)
        {
            newPlantStart.Id = _plantStarts.Any() ? _plantStarts.Max(x => x.Id) + 1 : 1;
            _plantStarts.Add(newPlantStart);
            return CreatedAtAction(nameof(GetPlantStart), new { id = newPlantStart.Id }, newPlantStart);
        }





    }
}
