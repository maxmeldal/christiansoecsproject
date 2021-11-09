using System;
using System.Threading.Tasks;
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
         public async Task<ActionResult> GetFacility(string id)
         {
             var facility =  _facilityService.ReadById(id);
        
             if (facility == null)
             {
                 //could also use badRequest()
                 return NotFound();
             }
        
             return Ok(facility);
         }
    }
}