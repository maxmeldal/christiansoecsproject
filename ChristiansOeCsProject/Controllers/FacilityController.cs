using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacilityController
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<FacilityService>>> GetFacilities()
        {
            return null;
        }

        // [HttpGet("{id:int}")]
        // public async Task<ActionResult<FacilityService>> GetFacility(int id)
        // {
        //     try
        //     {
        //         var result = await _facilityService.ReadById(id);
        //
        //         if (result == null)
        //         {
        //             return NotFound();
        //         }
        //
        //         return result;
        //     }
        //     catch (Exception)
        //     {
        //         return StatusCode(500, "Error");
        //     }
        // }
    }
}