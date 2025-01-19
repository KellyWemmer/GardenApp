using GardenApp.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace GardenApp.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PlantsInfoController: ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        //PlantInfo Service 

        public PlantsInfoController(ApplicationDbContext context, ILogger<PlantsStartsController> logger /*Service*/)
        {
            _context = context;
            _logger = logger;
            //_plantsInfoService
        }

        
       

    }
}
