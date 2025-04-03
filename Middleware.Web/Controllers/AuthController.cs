using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Middleware.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Replace this with your actual user validation logic
            if (request.Username != "testuser" || request.Password != "password")
            {
                return Unauthorized("Invalid credentials");
            }

            // Create claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            // Generate a JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ax77Gavz+MUOTziFa0e+OOz8jbCATvC52tieTMNRH1A=")); // Must match the key in Program.cs
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your-issuer",
                audience: "your-audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        [Authorize]
        [HttpGet("data")]
        public IActionResult GetSecureData()
        {
            // The user is authenticated and User.Identity.Name is available
            return Ok(new { message = $"Hello {User.Identity.Name}, you have access to secure data!" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
