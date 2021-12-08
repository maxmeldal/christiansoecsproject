using System;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    /**
     * Denne klasse er en rest service der anvender construktor injektion
     * Alle metoder laver HTTP request: GET, POST, DELETE og PUT og retunere tilpassende status koder 
     */
    public class AttractionController : MyControllerBase
    {
        private readonly AttractionService _attractionService;

        public AttractionController(AttractionService attractionService)
        {
            _attractionService = attractionService;
        }
        
        /**
         * Denne metode returnere enten status koden 200 via Ok() metoden og giver en liste af attraktioner i JSON format, eller status koden 500 hvis den fejler
         * 
         * Http example:
         * https://localhost:5001/api/attraction/attractions (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/attractions (server)
         */
        [HttpGet("attractions")]
        public ActionResult GetAttractions()
        {
            try
            {
                return Ok(_attractionService.ReadAll().Result);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        /**
         * Denne metode returnere enten status koden 200 via Ok() metoden og giver en attraktion i et JSON format, eller status koden 404 hvis attraktion med det id ikke eksistere
         * Hvis det hele fejler retunere den status koden 500
         * 
         * Http example:
         * https://localhost:5001/api/attraction/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/id (server)
         */
        [HttpGet("{id}")]
        public ActionResult GetAttraction(string id)
        {
            try
            {
                Attraction attraction = _attractionService.ReadById(id);
       
                if (attraction == null)
                {
                    //could also use badRequest()
                    return NotFound();
                }
       
                return Ok(attraction);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        /**
         * Denne metode returnere enten status koden 201 hvis attraktion bliver oprettet, eller status koden 404 hvis den attraktion den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/attraction/create (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/id (server)
         */
        [HttpPost("create")]
        public async Task<ActionResult<Attraction>> Create([FromBody] Attraction attraction)
        {
            if (attraction == null)
            {
                //could also use badRequest()
                return NotFound();
            }
            
            Attraction createdAttraction = await _attractionService.Create(attraction);

            return CreatedAtAction(nameof(GetAttraction), new {id = createdAttraction.Id}, createdAttraction);
        }

        /**
         * Denne metode returnere et updateret attraktion objekt og giver status koden 200 via Ok() metoden, eller status koden 404 hvis den attraktion den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/attraction/create (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/id (server)
         */
        [HttpPut("update")]
        public async Task<ActionResult<Attraction>> Update(Attraction attraction)
        {
            if (attraction != null)
            {
                return Ok(await _attractionService.Update(attraction));
            }
            //could also use badRequest()
            return NotFound();
        }

        
        /**
         * Denne metode sletter en attraktion ud fra et bestemt id, eller status koden 404 hvis attraktionen ikke eksistere
         * 
         * Http example:
         * https://localhost:5001/api/attraction/delete/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/delete/id (server)
         */
        [HttpDelete("delete/{id}")]
        public ActionResult<Trip> Delete(string id)
        {
            if (_attractionService.ReadById(id) != null)
            {
                _attractionService.Delete(id);
            }
            //could also use badRequest()
            return NotFound();
        }
    }
}