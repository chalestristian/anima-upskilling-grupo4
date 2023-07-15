using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Models;
using api.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // POST: api/auth/getToken
        [HttpPost("getToken")]
        public IActionResult GetToken(Aplicacao loginModel)
        {
            var app = _context.Aplicacoes.FirstOrDefault(a => a.AppKey == loginModel.AppKey && a.SecretKey == loginModel.SecretKey);
            if (app == null)
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(app.Id.ToString());
            return Ok(new { Token = token });
        }

        // POST: api/auth/refreshToken
        [HttpPost("refreshToken")]
        public IActionResult RefreshToken(string token)
        {
            var refreshToken = GenerateJwtToken(token);
            return Ok(new { Token = refreshToken });
        }

        private string GenerateJwtToken(string subject)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, subject) }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
