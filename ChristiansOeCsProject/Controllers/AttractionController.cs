using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    public class AttractionController : MyControllerBase
    {
        private readonly AttractionService _attractionService;

        public AttractionController(AttractionService attractionService)
        {
            _attractionService = attractionService;
        }
        
        //Http example:
        //https://localhost:5001/api/attraction/attractions
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
        //Http example:
        //https://localhost:5001/api/attraction/A6AAxTP7Yjs7HehuiDAN
        [HttpGet("{id}")]
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
        //https://localhost:5001/api/attraction/create
        [HttpPost("create")]
        public async Task<ActionResult<Attraction>> Create([FromBody] Attraction attraction)
        {
            if (attraction == null)
            {
                return NotFound();
            }

            var createdAttraction = await _attractionService.Create(attraction);

            return CreatedAtAction(nameof(GetAttraction), new {id = createdAttraction.Id}, createdAttraction);
        }

        [HttpPut("update{id}")]
        public async Task<ActionResult<Attraction>> Update(Attraction attraction, string id)
        {
            var existingAttraction = _attractionService.ReadById(id);

            if (existingAttraction == null)
            {
                return NotFound();
            }

            existingAttraction.Latitude = attraction.Latitude;
            existingAttraction.Longitude = attraction.Longitude;
            existingAttraction.Name = attraction.Name;

            await _attractionService.Update(attraction);

            return Ok();
        }

        //Http example:
        //https://localhost:5001/api/attraction/delete/A6AAxTP7Yjs7HehuiDAN
        [HttpDelete("delete/{id}")]
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