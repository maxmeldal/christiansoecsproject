using System.Net.Mime;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    public class DistanceController : MyControllerBase
    {
        private readonly DistanceService _distanceService;

        public DistanceController(DistanceService distanceService)
        {
            _distanceService = distanceService;
        }
        
        //Http example:
        //https://localhost:5001/api/distance/?lat=55.31&lon=15.19
        [HttpGet]
        public ActionResult<double> GetDistance(double lat, double lon)
        {
             return Ok(_distanceService.Distance(lat, lon));
        }
    
}
}