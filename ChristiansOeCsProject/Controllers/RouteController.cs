using System;
using Microsoft.AspNetCore.Mvc;
using ChristiansOeCsProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChristiansOeCsProject.Service;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly RouteService _routeService;

        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<Route>>> GetRoutes()
        {
            return null;
        }

        // [HttpGet("{id:int}")]
        // public async Task<ActionResult<Route>> GetRoute(int id)
        // {
        //     try
        //     {
        //         var result = await _routeService.ReadById(id);
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