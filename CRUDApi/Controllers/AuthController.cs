using CRUDApi.Controllers.request;
using CRUDApi.Models;
using CRUDApi.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsersService _usersService;
        public AuthController(IAuthService authService, IUsersService usersService)
        {
            _authService = authService;
            _usersService = usersService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _authService.Login(login.Username, login.Password);

            if (!user.IsError)
            {
                var token = await _authService.GenerateToken(user.User);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return Unauthorized();
            }

            var check = await _authService.CheckRefreshToken(request.UserId, request.Refresh_token);
            var user = await _usersService.GetUserAsync(request.UserId);

            if (check && user.User != null)
            {
                var token = await _authService.GenerateToken(user.User);
                return Ok(token);
            } else
            {
                return Unauthorized();
            }
        }
    }
}
