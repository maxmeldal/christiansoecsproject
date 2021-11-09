using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class TripController : ControllerBase
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }
        
        //Http example:
        //https://localhost:5001/api/trips
        [HttpGet("api/trips")]
        public async Task<ActionResult> GetTrips()
        {
            try
            {
                return Ok(_tripService.ReadAll().Result);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        //Http example:
        //https://localhost:5001/api/trip/DgXf06FNGLTODjEaOKsR
        [HttpGet("api/trip/{id}")]
        public async Task<ActionResult> GetRoute(string id)
        {
            var trip =  _tripService.ReadById(id);
        
            if (trip == null)
            {
                //could also use badRequest()
                return NotFound();
            }
        
            return Ok(trip);
        }
    }
}