using Microsoft.AspNetCore.Mvc;

namespace Middleware.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    namespace MyApi.Controllers
    {
        [Authorize]
        [ApiController]
        [Route("api/[controller]")]
        public class TestController : ControllerBase
        {
            // GET api/test/badrequest?searchValue=somevalue
            [HttpGet("badrequest")]
            public IActionResult GetBadRequest([FromQuery] string searchValue)
            {
                if (string.IsNullOrEmpty(searchValue))
                {
                    // Return a 400 error response.
                    return BadRequest("Search value is required.");
                }
                return Ok(new { message = "Search value provided." });
            }

            // GET api/test/unauthorized
            [HttpGet("unauthorized")]
            public IActionResult GetUnauthorized()
            {
                // Return a 401 error response.
                return Unauthorized("Authentication is required.");
            }

            // GET api/test/forbidden
            [HttpGet("forbidden")]
            public IActionResult GetForbidden()
            {
                // Return a 403 error response.
                return Forbid();
            }

            // GET api/test/notfound
            [HttpGet("notfound")]
            public IActionResult GetNotFound()
            {
                // Return a 404 error response.
                return NotFound("The requested resource was not found.");
            }

            // GET api/test/internalerror
            [HttpGet("internalerror")]
            public IActionResult GetInternalServerError()
            {
                // Return a 500 error response.
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }

}
