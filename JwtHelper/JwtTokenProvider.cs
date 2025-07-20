using Code_EcomMgmtDB1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Code_EcomMgmtDB1.JwtHelper
{

    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly IConfiguration _configs;

        public JwtTokenProvider(IConfiguration configs)
        {
            _configs = configs;
        }
        public Tokens GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            //  var tokenKey = Encoding.UTF8.GetBytes(_configs["JwtConfig:JwtKey"]);
            //var tokenKey = Encoding.UTF8.GetBytes("my_secret_key_12345");
            var tokenKey = Encoding.UTF8.GetBytes("s3cure_and_l0ng_256bit_key_example_12345678");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("Name", user.UserName),
                    new Claim("Roles",user.Role?.RoleName),
                    new Claim("Role",user.Role?.RoleName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), UserData = user };
        }

    }
}
