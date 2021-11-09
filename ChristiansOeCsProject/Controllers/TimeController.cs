using ChristiansOeCsProject.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class TimeController : ControllerBase
    {
        private readonly TimeService _timeService;

        public TimeController(TimeService timeService)
        {
            _timeService = timeService;
        }

        //Http example:
        //https://localhost:5001/api/time?distance=1.2
        [HttpGet("api/time")]
        public ActionResult<double> DistanceToMinutes(double distance)
        {
            var toMinutes = _timeService.DistanceToMinutes(distance);
            
            if (distance < 0)
            {
                //could also use badRequest()
                return NotFound();
            }

            return Ok(toMinutes);
        }
    }
}