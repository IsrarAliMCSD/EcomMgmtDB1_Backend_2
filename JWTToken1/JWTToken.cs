using Code_EcomMgmtDB1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Code_EcomMgmtDB1.JWTToken1
{
    public class JWTToken
    {
        string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
        //string keyBytes = Convert.FromHexString(key);  


        public string GetToken(User user)
        {
            var securityKey = new Microsoft
                 .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                            (securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);
            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
           {
                { "UserId", user.UserId},
                { "UserName", user.UserName},
                { "RoleName", user.Role?.RoleName},
                { "EmailAddress", user.EmailAddress}
           };
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            return tokenString;
        }
    }
}
