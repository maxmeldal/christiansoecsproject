using System;
using System.Net.Mime;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    public class FacilityController : MyControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }
        //Http example:
        //https://localhost:5001/api/facility/facilities
        [HttpGet("facilities")]
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
         //https://localhost:5001/api/facility/ED79tdWc3e82jaQhNLt5
         [HttpGet("{id}")]
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
         //https://localhost:5001/api/facility/create
         //Http example:
         //https://localhost:5001/api/facility/create
         [HttpPost("create")]
         public async Task<ActionResult<Facility>> Create([FromBody] Facility facility)
         {
             if (facility == null)
             { 

                 return NotFound();
             }
             var createFacility = await _facilityService.Create(facility);


             return CreatedAtAction(nameof(GetFacility), new {id = createFacility.Id}, createFacility);
         }
         //Http example:
         //https://localhost:5001/api/facility/update
         [HttpPut("update")]
         public async Task<ActionResult<Facility>> Update(Facility facility)
         {
             if (facility != null)
             {
                 return Ok(await _facilityService.Update(facility));
             }

             return NotFound();
         }

         //Http example:
         //https://localhost:5001/api/facility/delete/id
         [HttpDelete("delete/{id}")]
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