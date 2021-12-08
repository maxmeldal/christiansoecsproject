using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    /**
     * Denne klasse er en rest service der anvender construktor injektion
     */
    public class DistanceController : MyControllerBase
    {
        private readonly DistanceService _distanceService;

        public DistanceController(DistanceService distanceService)
        {
            _distanceService = distanceService;
        }
        /**
        * Denne metode laver en HTTP GET request og returnere status koden 200 via Ok() og giver ens afstand til færgen på christians Ø fra ens nuværende position i et JSON format
        * 
        * Http example:
        * https://localhost:5001/api/distance/?lat=55.31&lon=15.19 (lokalt)
        * https://csrestapp.azurewebsites.net/api/distance/?lat=55.31&lon=15.19 (server)
        */
        [HttpGet]
        public ActionResult<double> GetDistance(double lat, double lon)
        {
             return Ok(_distanceService.Distance(lat, lon));
        }
    
}
}