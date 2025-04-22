using Microsoft.AspNetCore.Mvc;

using AuthApp.Services;
using AuthApp.Models;


namespace AuthApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] User user) // ADD [FromBody]
        {
            if (user == null)
                return BadRequest("User object is null");

            Console.WriteLine($"Received: {user.Username}, {user.Email}");
            var result = await _service.Register(user);
            return result ? Ok("Registered successfully") : BadRequest("Registration failed");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Email and password are required");
            }

            var user = await _service.Login(loginDto.Email, loginDto.Password);
            if (user != null)
                return Ok(new { Message = "Login successful", User = user });

            return Unauthorized("Invalid credentials");
        }
    }
}
