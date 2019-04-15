using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApplication2.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetIssuerSecurityKey(this IConfiguration configuration)
        {
            var result = configuration.GetValue<string>("Authentication:JwtBearer:SecurityKey");
            return result;
        }

        public static string GetIssuer(this IConfiguration configuration)
        {
            var result = configuration.GetValue<string>("Authentication:JwtBearer:Issuer");
            return result;
        }

        public static string GetAudience(this IConfiguration configuration)
        {
            var result = configuration.GetValue<string>("Authentication:JwtBearer:Audience");
            return result;
        }

        public static string GetDefaultPolicy(this IConfiguration configuration)
        {
            var result = configuration.GetValue<string>("Policies:Default");
            return result;
        }

        public static SymmetricSecurityKey GetSymmetricSigningKey(this IConfiguration configuration)
        {
            var securityKey = configuration.GetIssuerSecurityKey();
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            
        }

        public static SigningCredentials GetSigningCredentials(this IConfiguration configuration)
        {
            return new SigningCredentials(configuration.GetSymmetricSigningKey(), SecurityAlgorithms.HmacSha256);
        }
    }
}
