using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlantandBiologyRecognition.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlantandBiologyRecognition.BLL.Utils
{
    public class JwtUtil
    {
        private readonly IConfiguration _configuration;

        public JwtUtil(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Accept user and role names directly
        public string GenerateJwtToken(User user, IList<string> userRoles)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:Secret"] ?? "your_default_secret";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };

            // Add role claim if available
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var expires = DateTime.UtcNow.AddHours(6);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "issuer",
                audience: _configuration["Jwt:Audience"] ?? "audience",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: credentials
            );

            return jwtHandler.WriteToken(token);
        }
    }
}

