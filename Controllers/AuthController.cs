using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DSLManagement.Models;
using DSLManagement.Services;
using DSLManagement.Views;

namespace DSLManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result.Succeeded)
            {
                return Ok(new RegisterResult{ Succeeded = true });
            }
            else
            {
                return BadRequest(new RegisterResult { Errors = result.Errors.Select(e => e.Description).ToList() });
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result.Succeeded)
            {
                return Ok(new LoginResult { Succeeded = true, Token = result.Token });
            }
            else
            {
                return BadRequest(new LoginResult { Errors = result.Errors });
            }
        }
    }
}