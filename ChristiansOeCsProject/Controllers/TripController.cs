using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;

namespace ChristiansOeCsProject.Controllers
{
    public class TripController : MyControllerBase
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }

        //Http example:
        //https://localhost:5001/api/trip/trips
        [HttpGet("trips")]
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
        [HttpGet("{id}")]
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
        //https://localhost:5001/api/trip/create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Trip> Create([FromBody] Trip trip)
        {
            if (trip != null) 
            {
               //_tripService.Create(trip);
             
               //return CreatedAtAction(nameof(GetTrip), new { id = entity.Id}, entity);
            }
            
            return NotFound();
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<Trip>> Update(Trip trip, string id)
        {
            var existingTrip = _tripService.ReadById(id);
            
            if (existingTrip == null)
            {
                return NotFound();
            }
            
           // var updateTrip = existingTrip with
           // {
                
           // }

            return NoContent();
            
        }

        [HttpDelete("delete/{id}")]
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