using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Extensions;

namespace WebApplication2.Helper
{
    public class Util
    {
        public static object GenerateAccessToken(IEnumerable<Claim> claims , IConfiguration _configuration)
        {
            var token = new JwtSecurityToken
                (
                    issuer: _configuration.GetIssuer(),
                    audience: _configuration.GetAudience(),
                    expires: DateTime.Now.AddMinutes(1),
                    claims: claims,
                    signingCredentials: _configuration.GetSigningCredentials()
                );

            var validToken = new JwtSecurityTokenHandler().WriteToken(token);
            return validToken;
        }

        public static string GenerateRefreshToken(IConfiguration _configuration)
        {
            var issuer = _configuration.GetIssuer();
            var audience = _configuration.GetAudience();
            var symmetricKey = _configuration.GetSymmetricSigningKey();

            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration configuration)
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = configuration.GetSymmetricSigningKey(),
                ValidAudience = configuration.GetAudience(),
                ValidIssuer = configuration.GetIssuer(),
                ValidateLifetime = true

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;

        }

        
        public static string GenerateUniqueId()
        {
            
            return Guid.NewGuid().ToString();
        }

        
    }
}
