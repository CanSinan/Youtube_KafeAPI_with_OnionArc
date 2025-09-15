using KafeAPI.Application.Dtos.AuthDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KafeAPI.Application.Helpers
{
    public class TokenHelpers
    {
        private readonly IConfiguration _configuration;

        public TokenHelpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(TokenDto dto)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creadentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("_e",dto.Email),
                new Claim("_u",dto.Id),
                new Claim("_r",dto.Role),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creadentials
                );
            var resultToken = new JwtSecurityTokenHandler().WriteToken(token);

            return resultToken;
        }
    }
}
