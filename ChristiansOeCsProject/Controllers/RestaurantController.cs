using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("restaurants")]
        public async Task<List<Restaurant>> GetRestaurants()
        {
            return (await _restaurantService.ReadAll()).ToList();
        }

        [HttpGet("restaurant/{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(string id)
        {
            var restaurant = _restaurantService.ReadById(id);
        
            if (restaurant == null)
            {
                return NotFound();
            }
        
            return restaurant;
        }
    }
}