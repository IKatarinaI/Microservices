using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController()
        {
        }

        [HttpPost]
        public ActionResult TestInboundConnection()
        { 
            return Ok("Inbound test from CommandsService.");
        }
    }
}
