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
    [Route("api/[controller]")]
    public class FacilityController : ControllerBase
    {
        private readonly FacilityService _facilityService;

        public FacilityController(FacilityService facilityService)
        {
            _facilityService = facilityService;
        }

        [HttpGet]
        public async Task<List<Facility>> GetFacilities()
        {
            return (await _facilityService.ReadAll());
        }

         /*[HttpGet("{id:int}")]
         public async Task<ActionResult<Facility>> GetFacility(int id)
         {
             try
             {
                 var result = await _facilityService.ReadById(id);
        
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