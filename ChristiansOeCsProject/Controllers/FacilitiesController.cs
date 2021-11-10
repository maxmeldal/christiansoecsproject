using System;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }
        //Http example:
        //https://localhost:5001/api/facilities
        [HttpGet("api/facilities")]
        public async Task<ActionResult> GetFacilities()
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

         //Http example:
         //https://localhost:5001/api/facilities/ED79tdWc3e82jaQhNLt5
         [HttpGet("api/facilities/{id}")]
         public async Task<ActionResult<Facility>> GetFacility(string id)
         {
             var facility =  _facilityService.ReadById(id);
        
             if (facility == null)
             {
                 //could also use badRequest()
                 return NotFound();
             }
        
             return Ok(facility);
         }
         
         
         //Http example:
         //https://localhost:5001/api/create/facility
         [HttpPost("api/create/facility")]
         public async Task<ActionResult<Facility>> Create(Attraction attraction)
         {
             return CreatedAtAction(nameof(GetFacility), new {}, attraction);
         }

         [HttpPut("api/update/facility")]
         public async Task<ActionResult<Facility>> Update(Facility facility)
         {
             if (facility != null)
             {
                 await _facilityService.Update(facility);
             }

             return NotFound();
         }

         [HttpDelete("api/delete/facility/{id}")]
         public async Task<ActionResult<Trip>> Delete(string id)
         {
             if (_facilityService.ReadById(id) != null)
             {
                 _facilityService.Delete(id);
             }

             return NotFound();
         }
    }
}