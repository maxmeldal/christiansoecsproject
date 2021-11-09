using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantService _restaurantService;

        public RestaurantController(RestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
        {
            try
            {
                return (await _restaurantService.ReadAll()).ToList();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }*/

        /*[HttpGet("{id:int}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            try
            {
                var result = await _restaurantService.ReadById(id);
       
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