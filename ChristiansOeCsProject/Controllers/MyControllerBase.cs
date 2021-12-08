using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{   
    /**
     * Denne klasse arver fra ControllerBase som er abstrakt klasse der ikke har view support, og bliver anvendt i alle Controller klasserne
     * Route attributten, er det der definerer endpoint for controlleren ([controller], betyder den tager navnet for controlleren)
     * Produces attributten betyder at den siger til frameworket at den give formattet JSON som output
     */
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class MyControllerBase : ControllerBase
    {
    }
}