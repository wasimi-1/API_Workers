﻿using API_Workers.Data;
using API_Workers.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace API_Workers.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _db;
        private string _SecretKey;

        public UsersController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _SecretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }

        [HttpPost("UserLogin")]
        public async Task<UserLoginResponse> Login(UserLoginRequest logindetails)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.Username.ToLower() == logindetails.Username.ToLower() && u.Password == logindetails.Password);
            if (user == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            UserLoginResponse loginResponse = new UserLoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                UserDetails = user
            };

            return loginResponse;
        }
    }
}
