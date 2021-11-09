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

        /*[HttpGet]
        public async Task<ActionResult<List<Attraction>>> GetAttractions()
        {
            try
            {
                return (await _attraction.ReadAll()).ToList();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }*/

        /*[HttpGet("{id:int}")]
        public async Task<ActionResult<Attraction>> GetAttraction(int id)
        {
            try
            {
                var result = await _attractionService.ReadById(id);
       
                if (result == null)
                {
                    return null;
                }
       
                return result;
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }*/
    }
}