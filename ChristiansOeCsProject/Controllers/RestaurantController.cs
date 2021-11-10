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
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        //Http example:
        //https://localhost:5001/api/restaurants
        [HttpGet("api/restaurants")]
        public async Task<ActionResult> GetRestaurants()
        {
            try
            {
                return Ok(_restaurantService.ReadAll().Result);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        //Http example:
        //https://localhost:5001/api/restaurant/id
        [HttpGet("api/restaurant/{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(string id)
        {
            var restaurant = _restaurantService.ReadById(id);
        
            if (restaurant == null)
            {
                //could also use badRequest()
                return NotFound();
            }
        
            return Ok(restaurant);
        }
        
        
        //Http example:
        //https://localhost:5001/api/create/restaurant
        [HttpPost("api/create/restaurant")]
        public async Task<ActionResult<Restaurant>> Create(Restaurant restaurant)
        {
            return CreatedAtAction(nameof(GetRestaurant), new {}, restaurant);
        }

        [HttpPut("api/update/restaurant")]
        public async Task<ActionResult<Restaurant>> Update(Restaurant restaurant)
        {
            if (restaurant != null)
            {
                await _restaurantService.Update(restaurant);
            }

            return NotFound();
        }

        [HttpDelete("api/delete/restaurant/{id}")]
        public async Task<ActionResult<Restaurant>> Delete(string id)
        {
            if (_restaurantService.ReadById(id) != null)
            {
                _restaurantService.Delete(id);
            }

            return NotFound();
        }
    }
}