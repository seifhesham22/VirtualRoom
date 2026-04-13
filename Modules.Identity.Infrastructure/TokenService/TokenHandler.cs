using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Infrastructure.IdentityModules;
using Shared.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Modules.Identity.Infrastructure.TokenService
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _config;

        public TokenHandler(IConfiguration config) => _config = config;

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var claims = new List<Claim>
            {
                new(AppClaims.UserId, user.Id.ToString()),
                new(AppClaims.Role, user.Role.ToString()),
                new(AppClaims.ProfileId, user.ProfileId.ToString())
            };
            if (user.FacultyId.HasValue)
            {
                claims.Add(new(AppClaims.FacultyId, user.FacultyId.ToString()));
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:issuer"],
                audience: _config["Jwt:audience"],
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddHours(8)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
