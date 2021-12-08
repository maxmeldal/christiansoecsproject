using System;
using System.Net.Mime;
using System.Threading.Tasks;
using ChristiansOeCsProject.Entities;
using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    /**
     * Denne klasse er en rest service der anvender construktor injektion
     * Alle metoder laver HTTP request: GET, POST, DELETE og PUT og retunere tilpassende status koder 
     */
    public class RestaurantController : MyControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /**
         * Denne metode retunere enten status koden 200 via Ok() metoden og giver en liste af restauranter i JSON format, eller status koden 500 hvis den fejler
         * 
         * Http example:
         * https://localhost:5001/api/restaurant/restaurants (lokalt)
         * https://csrestapp.azurewebsites.net/api/restaurant/restaurants (server)
         */
        [HttpGet("restaurants")]
        public ActionResult GetRestaurants()
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

        /**
         * Denne metode retunere enten status koden 200 via Ok() metoden og giver en restauranter i et JSON format, eller status koden 404 hvis restaurant med det id ikke eksistere
         * Hvis det hele fejler retunere den status koden 500
         * 
         * Http example:
         * https://localhost:5001/api/restaurant/id(lokalt)
         * https://csrestapp.azurewebsites.net/api/restaurant/id (server)
         */
        [HttpGet("{id}")]
        public ActionResult<Restaurant> GetRestaurant(string id)
        {
            var restaurant = _restaurantService.ReadById(id);
        
            if (restaurant == null)
            {
                //could also use badRequest()
                return NotFound();
            }
        
            return Ok(restaurant);
        }
        
        /**
         * Denne metode retunere enten status koden 201 hvis restaurant bliver oprettet, eller status koden 404 hvis den restaurant den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/restaurant/create (lokalt)
         * https://csrestapp.azurewebsites.net/api/restaurant/create (server)
         */
        [HttpPost("create")]
        public async Task<ActionResult<Restaurant>> Create([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
            {
                //could also use badRequest()
                return NotFound();
            }
            
            Restaurant createdRestaurant = await _restaurantService.Create(restaurant);

            return CreatedAtAction(nameof(GetRestaurant), new { id = createdRestaurant.Id}, createdRestaurant);
        }

        /**
         * Denne metode retunere et updateret restaurant objekt og giver status koden 200 via Ok() metoden, eller status koden 404 hvis den restaurant den får med er null
         * Metoden udføres asynkront så user operations kan fortsætte
         * 
         * Http example:
         * https://localhost:5001/api/attraction/create (lokalt)
         * https://csrestapp.azurewebsites.net/api/attraction/id (server)
         */
        [HttpPut("update")]
        public async Task<ActionResult<Restaurant>> Update(Restaurant restaurant)
        {
            if (restaurant != null)
            {
                return Ok(await _restaurantService.Update(restaurant));
            }
            //could also use badRequest()
            return NotFound();
        }

        /**
         * Denne metode sletter en restaurant ud fra et bestemt id, eller status koden 404 hvis restauranten ikke eksistere
         * 
         * Http example:
         * https://localhost:5001/api/restaurant/delete/id (lokalt)
         * https://csrestapp.azurewebsites.net/api/restaurant/delete/id (server)
         */
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Restaurant>> Delete(string id)
        {
            if (_restaurantService.ReadById(id) != null)
            {
                _restaurantService.Delete(id);
            }
            //could also use badRequest()
            return NotFound();
        }
    }
}