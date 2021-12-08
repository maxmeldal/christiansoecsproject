using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;

namespace ChristiansOeCsProject.Controllers
{
    /**
     * Denne klasse er en rest service der anvender construktor injektion
     * Alle metoder laver HTTP request: GET, POST, DELETE og PUT og retunere tilpassende status koder 
     */
    public class TripController : MyControllerBase
    {
        private readonly TripService _tripService;

        public TripController(TripService tripService)
        {
            _tripService = tripService;
        }

        /**
         * Denne metode returnere enten status koden 200 via Ok() metoden og giver en liste af turer i JSON format, eller status koden 500 hvis den fejler
         * 
         * Http example:
         * https://localhost:5001/api/trip/trips (lokalt)
         * https://csrestapp.azurewebsites.net/api/trip/trips (server)
         */
        [HttpGet("trips")]
        public ActionResult GetTrips()
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

        /**
         * Denne metode returnere enten status koden 200 via Ok() metoden og giver en tur i et JSON format, eller status koden 404 hvis tur med det id ikke eksistere
         * Hvis det hele fejler retunere den status koden 500
         * 
         * Http example:
         * https://localhost:5001/api/trip/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/trip/id (server)
         */
        [HttpGet("{id}")]
        public ActionResult<Trip> GetTrip(string id)
        {
            var trip = _tripService.ReadById(id);

            if (trip == null)
            {
                //could also use badRequest()
                return NotFound();
            }

            return Ok(trip);
        }

        /**
         * Denne metode returnere enten status koden 201 hvis tur bliver oprettet, eller status koden 404 hvis den tur den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/trip/create (lokalt)
         * https://csrestapp.azurewebsites.net/api/trip/create (server)
         */
        [HttpPost("create")]
        public async Task<ActionResult<Trip>> Create([FromBody] Trip trip)
        {
            if (trip == null)
            {
                //could also use badRequest()
                return NotFound();
            }
            
            Trip createdTrip = await _tripService.Create(trip);

            return CreatedAtAction(nameof(GetTrip), new { id = createdTrip.Id}, createdTrip);
        }
        
        /**
         * Denne metode returnere et updateret tur objekt og giver status koden 200 via Ok() metoden, eller status koden 404 hvis den tur den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/trip/update (lokalt)
         * https://csrestapp.azurewebsites.net/api/trip/update (server)
         */
        [HttpPut("update")]
        public async Task<ActionResult<Trip>> Update(Trip trip)
        {
            if (trip != null)
            {
                return Ok(await _tripService.Update(trip));
            }
            //could also use badRequest()
            return NotFound();
        }

        /**
         * Denne metode sletter en tur ud fra et bestemt id, eller status koden 404 hvis turen ikke eksistere
         * 
         * Http example:
         * https://localhost:5001/api/attraction/delete/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/delete/id (server)
         */
        [HttpDelete("delete/{id}")]
        public ActionResult<Trip> Delete(string id)
        {
            if (_tripService.ReadById(id) != null)
            {
                _tripService.Delete(id);
            }
            //could also use badRequest()
            return NotFound();
        }
}
}