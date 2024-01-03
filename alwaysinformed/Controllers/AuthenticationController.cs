using alwaysinformed_bll.Models.GET;
using alwaysinformed_bll.Services;
using alwaysinformed_dal.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace alwaysinformed.Controllers
{
    [EnableCors("AllowLocalhost")]
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly Dictionary<int,string> userRoles = new()
        {
            { 7, "admin" },
            { 8, "reader" },
            { 9, "author" }
        };
        
        private readonly UserService service;
        private readonly IConfiguration configuration;

        public AuthenticationController(UserService service,
            IConfiguration configuration) 
        {
            this.service = service;
            this.configuration = configuration;
        }
        public class AuthenticationModel 
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }
        public class TokenResponse
        {
            public string token; 
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<TokenResponse>> AuthenticateAsync(AuthenticationModel body)
        {
            var user = await ValidateUserCredetialsAsync(body.UserName, body.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:Secret"]));

            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var userRole = userRoles[user.UserRole];
            var claimsForToken = new List<Claim>()
            {
                new Claim("id",user.UserId.ToString()),
                new Claim("userName",user.Username),
                new Claim("userRole",userRole),
                new Claim("userPhoto",user.UserPhoto ?? string.Empty),
                new Claim("userEmail",user.Email)
            };

            
            var jwtToken = new JwtSecurityToken(

                configuration["Authentication:Issuer"],
                configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);
            return new TokenResponse()
            {
                token = tokenToReturn
            };
        }

        private async Task<UserGetDto?> ValidateUserCredetialsAsync(string? userName, string? userPassword)
        {
            var user = await service.GetByUsernameAndPasswordAsync(userName, userPassword);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
    
}