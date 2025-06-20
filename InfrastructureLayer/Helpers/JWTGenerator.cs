using ApplicationLayer.Interfaces;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Helpers
{
    internal class JWTGenerator : IJwtGenerator
    {
        private readonly IConfiguration _configuration;

        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string JWTTokenGenerate(User user)
        {
            // Retrive JWT settings from configuration
            var jwtSettings = _configuration.GetSection("JWT");

            // Get the secret signing key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key missing")));

            // Define signing credentials using HMAC-SHA256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Define claims to embed in the token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString())
            };

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!)),
                signingCredentials: credentials
            );

            // Return the serialized token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

