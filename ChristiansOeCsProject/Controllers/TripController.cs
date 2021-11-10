using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
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
        public async Task<ActionResult<Trip>> GetTrip(string id)
        {
            var trip = _tripService.ReadById(id);

            if (trip == null)
            {
                //could also use badRequest()
                return NotFound();
            }

            return Ok(trip);
        }

        //Http example:
        //https://localhost:5001/api/create/trip
        [HttpPost("api/create/trip")]
        public async Task<ActionResult<Trip>> Create(Trip trip)
        {
            return CreatedAtAction(nameof(GetTrip), new {}, trip);
        }

        [HttpPut("api/update/trip")]
        public async Task<ActionResult<Trip>> Update(Trip trip)
        {
            if (trip != null)
            {
                await _tripService.Update(trip);
            }

            return NotFound();
        }

        [HttpDelete("api/delete/trip/{id}")]
        public async Task<ActionResult<Trip>> Delete(string id)
        {
            if (_tripService.ReadById(id) != null)
            {
                _tripService.Delete(id);
            }

            return NotFound();
        }
}
}