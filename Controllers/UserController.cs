using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DSLManagement.Models;
using DSLManagement.Services;
using DSLManagement.Views;

namespace DSLManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) //
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetUsersAsync();
            return Ok(users);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserView>> GetUser(Guid id)
        {
            var user = await _userRepository.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        
        [HttpGet("username/{username}")]
        public async Task<ActionResult<UserView>> GetUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userRepository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
