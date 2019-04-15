using Data.Models;

namespace WebApplication2.Helper
{
    public interface IUtilService
    {

        void SaveCustomLogin(string uniqueId, string userId);

        void SaveAccessToken(string token, string refreshToken, string UID, string userId);

        Token GetRefreshToken(string uniqueId);

    }
}