using System;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    public class DistanceController : ControllerBase
    {
        private readonly DistanceService _distanceService;

        public DistanceController(DistanceService distanceService)
        {
            _distanceService = distanceService;
        }

        /*[HttpGet]
        public async Task<ActionResult> getDistance(double lat, double lon)
        {
            try
            {
                return _distanceService.Distance(lat, lon);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }*/
    
}
}