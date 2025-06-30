using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PlantandBiologyRecognition.DAL.Models;
using PlantandBiologyRecognition.DAL.Repositories.Interfaces;
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
        private readonly IUnitOfWork<AppDbContext> _unitOfWork;

        public JwtUtil(IConfiguration configuration, IUnitOfWork<AppDbContext> unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["Jwt:Secret"] ?? "your_default_secret";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Await the asynchronous method to get the result before using LINQ
            var userRolesTask = _unitOfWork.GetRepository<Userrole>()
                .GetListAsync(predicate: r => r.UserId == user.UserId);
            var userRoles = userRolesTask.Result.Select(r => r.RoleName).ToList();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("UserId", user.UserId.ToString())
            };

            // Add role claim if available
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
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

