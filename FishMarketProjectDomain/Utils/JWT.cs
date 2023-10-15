using FishMarketProjectDomain.Models.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FishMarketProjectDomain.Utils
{
    public class JWT
    {
        private readonly IConfiguration _configuration;
        public JWT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string CreateToken(UserRequest user, string privateKey)
        {
            List<Claim> claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
