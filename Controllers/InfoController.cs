
using Microsoft.AspNetCore.Mvc;

namespace jwtaccount_two.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class InfoController : ControllerBase

    {
        [HttpGet("me")]
        public ActionResult Profile()
        {
            return Ok(
                "Thank you"
            );
        }
    }
}