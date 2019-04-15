using Data.Models;
using System;
using System.Linq;

namespace WebApplication2.Helper
{
    public class UtilService : IUtilService
    {
        private ApplicationDbContext _context;

        public UtilService(ApplicationDbContext context)
        {
            _context = context;
        }


        public void SaveCustomLogin(string uniqueId,string userId)
        {
            var customLogin = new CustomUserLogin()
            {
                UniqueId = uniqueId,
                UserId = userId,
            };
            _context.CustomUserLogins.Add(customLogin);
            _context.SaveChanges();
        }
        public void SaveAccessToken(string token, string refreshToken, string UID, string userId)
        {
            const int expiry = 5;

            //user hasnt logged in with this device
            if (!_context.CustomUserLogins.Any(a => a.UniqueId == UID))
            {
                SaveCustomLogin(UID, userId);
            }


            var newUser = _context.Tokens.FirstOrDefault(a => a.UniqueId == UID);

            if (newUser != null)
            {
                newUser.AccessToken = token;
                newUser.RefreshToken = refreshToken;
                newUser.RefreshTokenExpiryDate = DateTime.Now.AddMinutes(expiry);
                _context.SaveChanges();
                return;
            }

            var newtoken = new Token()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                RefreshTokenExpiryDate = DateTime.Now.AddMinutes(expiry),
                UniqueId = UID,
                UserId = userId
            };

            

            _context.Tokens.Add(newtoken);
            _context.SaveChanges();

        }

        public Token GetRefreshToken(string uniqueId)
        {
            return _context.Tokens.FirstOrDefault(a => a.UniqueId == uniqueId);
        }
        
    }
}
