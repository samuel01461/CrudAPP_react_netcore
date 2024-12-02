using Azure.Core;
using CRUDApi.Data;
using CRUDApi.Models;
using CRUDApi.Repositories.interfaces;
using CRUDApi.Services.interfaces;
using CRUDApi.Services.responses.AuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUDApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsers _usersRepository; 
        private readonly IConfiguration _configuration;
        private readonly CrudDbContext _context;

        public AuthService(IUsers usersRepository, IConfiguration configuration, CrudDbContext context)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _context = context;
        }
        public async Task<LoginResponse> Login(string username, string password)
        {
            var user = await _usersRepository.GetUserByUsernameAsync(username);
            
            if (user.Any())
            {
                if (user.First().Password == password) {
                    return new LoginResponse { IsError = false, Error = null, User = user.First() };
                } else
                {
                    return new LoginResponse { IsError = true, Error = "Login incorrect" };
                }
            } else
            {
                return new LoginResponse { IsError = true, Error = "Login incorrect" };
            }
        }

        public async Task<string> GenerateRefreshToken(User user)
        {
            var refresh_token = Guid.NewGuid().ToString();
            var expires = DateTime.UtcNow.AddDays(7);
            var tokenObj = new Tokens { Refresh_token = refresh_token, Expires = expires, UserId = user.Id };
            
            _context.Tokens.Add(tokenObj);
            await _context.SaveChangesAsync();
            return refresh_token;
        }

        public async Task<bool> CheckRefreshToken(int userId, string refresh_token)
        {
            var tokens = await _context.Tokens.ToListAsync();
            var find = await _context.Tokens.Where(t => t.Refresh_token == refresh_token && t.UserId == userId).ToListAsync();
            if (find.Count > 0)
            {
                var token = find.First();
                if (token.Expires > DateTime.UtcNow && token.Used == 0)
                {
                    token.Used = 1;
                    _context.Entry(token).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                } else
                {
                    return false;
                }
            } else
            {
                return false;
            }
        }
        public async Task<TokenResponse> GenerateToken(User user)
        {
            var jwt_settings = _configuration.GetSection("JWT");
            var secret_key = jwt_settings.GetValue<string>("SigningKey");
            var audience = jwt_settings.GetValue<string>("Audience");
            var issuer = jwt_settings.GetValue<string>("Issuer");
            var expiration = jwt_settings.GetValue<int>("ExpiryInMinutes");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret_key));

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.UsersRoles.First().RolId.ToString()),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())
                }),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddMinutes(expiration),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(descriptor);
            var access_token = tokenHandler.WriteToken(token);
            var refresh_token = await GenerateRefreshToken(user);

            return new TokenResponse { IsError = false, Error = null, AccessToken = access_token, RefreshToken = refresh_token };
        }
    }
}
