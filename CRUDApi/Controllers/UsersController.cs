using CRUDApi.Models;
using CRUDApi.Services.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersAsync()
        {
            return Ok(await _usersService.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(int id)
        {
            return Ok(await _usersService.GetUserAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(await _usersService.CreateUserAsync(user));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(await _usersService.UpdateUserAsync(user));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            return Ok(await _usersService.DeleteUserAsync(id));
        }
    }
}
