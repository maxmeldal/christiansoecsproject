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
    public class FacilityController : MyControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }
        
        /**
         * Denne metode returnere enten status koden 200 via Ok() og giver en faciliteter i et JSON format, eller status koden 500 hvis den fejler
         * 
         * Http example:
         * https://localhost:5001/api/facility/facilities
         * https://csrestapp.azurewebsites.net/api/facility/facilities (server)
         */
        [HttpGet("facilities")]
        public ActionResult GetFacilities()
        {
            try
            {
                return Ok(_facilityService.ReadAll().Result);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /**
         * Denne metode returnere enten status koden 200 via Ok() og giver en facilitet i et JSON format, eller status koden 404 hvis faciliteten med det id ikke eksistere
         * Hvis det hele fejler retunere den status koden 500
         * 
         * Http example:
         * https://localhost:5001/api/facility/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/facility/id (server)
         */
        [HttpGet("{id}")]
         public ActionResult<Facility> GetFacility(string id)
         {
             Facility facility =  _facilityService.ReadById(id);
        
             if (facility == null)
             {
                 //could also use badRequest()
                 return NotFound();
             }
        
             return Ok(facility);
         }
         
        /**
         * Denne metode returnere enten status koden 201 hvis facilitet bliver oprettet, eller status koden 404 hvis den facilitet den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/facility/create (lokalt)
         * https://csrestapp.azurewebsites.net/api/facility/create (server)
         */
        [HttpPost("create")]
         public async Task<ActionResult<Facility>> Create([FromBody] Facility facility)
         {
             if (facility == null)
             { 

                 return NotFound();
             }
             Facility createFacility = await _facilityService.Create(facility);


             return CreatedAtAction(nameof(GetFacility), new {id = createFacility.Id}, createFacility);
         }
        
        /**
         * Denne metode returnere et updateret facilitet objekt og giver status koden 200 via Ok() metoden, eller status koden 404 hvis den facilitet den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/facility/update (lokalt)
         * https://csrestapp.azurewebsites.net/api/facility/update (server)
         */
        [HttpPut("update")]
         public async Task<ActionResult<Facility>> Update(Facility facility)
         {
             if (facility != null)
             {
                 return Ok(await _facilityService.Update(facility));
             }

             return NotFound();
         }
        
        /**
         * Denne metode sletter en facilitet ud fra et bestemt id, eller returnere status koden 404 hvis faciliteten ikke eksistere
         * 
         * Http example:
         * https://localhost:5001/api/facility/delete/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/facility/delete/id (server)
         */
        [HttpDelete("delete/{id}")]
         public ActionResult<Trip> Delete(string id)
         {
             if (_facilityService.ReadById(id) != null)
             {
                 _facilityService.Delete(id);
             }

             return NotFound();
         }
    }
}