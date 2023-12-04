using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL.Interface;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;

        private readonly string _expiresDays;

        public JwtService(string secretKey, string expiresDays)
        {
            _secretKey = secretKey;
            _expiresDays = expiresDays;
        }

        public string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var token = new JwtSecurityToken(claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToUInt32(_expiresDays)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

