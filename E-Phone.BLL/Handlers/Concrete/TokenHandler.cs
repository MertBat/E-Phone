using E_Phone.BLL.DTOs.Auth;
using E_Phone.BLL.Handlers.Abstract;
using E_Phone.Core.Entities;
using E_Phone.Core.IRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Phone.BLL.Handlers.Concrete
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }        

        public TokenDTO CreateAccessToken(User user)
        {
            
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new()
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
                };
            DateTime expirationTime = DateTime.UtcNow.AddDays(1);

            JwtSecurityToken securityToken = new
                (
                audience: _configuration["JwtSettings:Audience"],
                issuer: _configuration["JwtSettings:Issuer"],
                expires: expirationTime,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler securityTokenHandler = new();
            TokenDTO token = new();
            token.Token = securityTokenHandler.WriteToken(securityToken);

            return token;
        }

        public int GetIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ReadJwtToken(token);
            int userId = Convert.ToInt32(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            return userId;
        }

        
    }
}
