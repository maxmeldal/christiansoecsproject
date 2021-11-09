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
    [Route("api/[controller]")]
    public class AttractionController : ControllerBase
    {
        private readonly AttractionService _attractionService;

        public AttractionController(AttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        [HttpGet]
        public ActionResult GetAttractions()
        {
            try
            {
                return Ok(_attractionService.ReadAll());
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
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
    }
}