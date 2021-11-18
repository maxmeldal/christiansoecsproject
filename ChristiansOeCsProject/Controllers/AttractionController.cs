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
        [HttpGet("attractionsss")]
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
        public async Task<ActionResult<Attraction>> Create(Attraction attraction)
        {
            return CreatedAtAction(nameof(GetAttraction), new {}, attraction);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Attraction>> Update(Attraction attraction)
        {
            if (attraction != null)
            {
                await _attractionService.Update(attraction);
            }

            return NotFound();
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