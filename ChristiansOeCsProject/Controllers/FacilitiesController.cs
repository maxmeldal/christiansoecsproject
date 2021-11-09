using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet("facilities")]
        public async Task<List<Facility>> GetFacilities()
        {
            return (await _facilityService.ReadAll());
        }

         [HttpGet("facilities/{id}")]
         public async Task<ActionResult<Facility>> GetFacility(string id)
         {
             var facility = _facilityService.ReadById(id);
        
             if (facility == null)
             {
                 return NotFound();
             }
        
             return facility;
         }
    }
}