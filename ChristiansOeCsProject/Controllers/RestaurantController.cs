using System;
using System.Net.Mime;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    public class RestaurantController : MyControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        //Http example:
        //https://localhost:5001/api/restaurant/restaurants
        [HttpGet("restaurants")]
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
        //https://localhost:5001/api/restaurant/1e8e902c-590f-4669-abe0-c85d3b87fbe8
        [HttpGet("{id}")]
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
        //https://localhost:5001/api/restaurant/create
        [HttpPost("create")]
        public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
            {
                return NotFound();
            }
            
            var createdRestaurant = await _restaurantService.Create(restaurant);

            return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurant.Id}, createdRestaurant);
        }

        [HttpPut("update")]
        public async Task<ActionResult<Restaurant>> Update(Restaurant restaurant, string id)
        {
            var existingRestaurant = _restaurantService.ReadById(id);
            
            if (existingRestaurant == null)
            {
                return NotFound();
            }
            
            existingRestaurant.Latitude = restaurant.Latitude;
            existingRestaurant.Longitude = restaurant.Longitude;
            existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Url = restaurant.Url;
            existingRestaurant.Open = restaurant.Open;
            existingRestaurant.Close = restaurant.Close;

            await _restaurantService.Update(existingRestaurant);

            return Ok();
        }

        [HttpDelete("delete/{id}")]
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