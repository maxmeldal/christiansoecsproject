using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class AttractionController : ControllerBase
    {
        private readonly AttractionService _attractionService;

        public AttractionController(AttractionService attractionService)
        {
            _attractionService = attractionService;
        }
        
        //Http example:
        //https://localhost:5001/api/attractions
        [HttpGet("api/attactions")]
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
        //Http example:
        //https://localhost:5001/api/facilities/8d16be31-a7ff-45ff-907d-6cbcab477c40
        [HttpGet("api/attraction/{id}")]
        public ActionResult GetAttraction(string id)
        {
            try
            {
                var attraction = _attractionService.ReadById(id);
       
                if (attraction == null)
                {
                    return NotFound();
                }
       
                return Ok(attraction);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        //Http example:
        //https://localhost:5001/api/create/attaction
        [HttpPost("api/create/attaction")]
        public async Task<ActionResult<Attraction>> Create(Attraction attraction)
        {
            return CreatedAtAction(nameof(GetAttraction), new {}, attraction);
        }

        [HttpPut("api/update/attraction")]
        public async Task<ActionResult<Attraction>> Update(Attraction attraction)
        {
            if (attraction != null)
            {
                await _attractionService.Update(attraction);
            }

            return NotFound();
        }

        [HttpDelete("api/delete/trip/{id}")]
        public async Task<ActionResult<Trip>> Delete(string id)
        {
            if (_attractionService.ReadById(id) != null)
            {
                _attractionService.Delete(id);
            }

            return NotFound();
        }
    }
}