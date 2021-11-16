using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

namespace ChristiansOeCsProject.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class MyControllerBase : ControllerBase
    {
    }
}