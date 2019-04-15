using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebApplication2.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using WebApplication2.Extensions;
using Data.Models;
using WebApplication2.Helper;
using Microsoft.AspNetCore.Cors;

namespace WebApplication2.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilService _utilService;

        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, IUtilService utilService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _utilService = utilService;
        }

        [Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var user = new IdentityUser()
            {
                Email = model.Username,
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new { message = result.Errors.FirstOrDefault().Description });
            }

            return Ok(user.UserName);
        }

        [Route("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(error: new { message = "Invalid Parameters" });
            }

            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return BadRequest(error: new { message = "Invalid Email" });
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!checkPassword)
            {
                return BadRequest(error: new { message = "Invalid Password" });
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var uniqueId = string.IsNullOrEmpty(model.UniqueId)? Util.GenerateUniqueId() : model.UniqueId;

            //if request doesnt contain uniqueId generate one and save it
            if (string.IsNullOrEmpty(model.UniqueId))
            {
                _utilService.SaveCustomLogin(uniqueId, user.Id);
                
            }


            var token = Util.GenerateAccessToken(claims,_configuration).ToString();
            var refreshToken = Util.GenerateRefreshToken(_configuration);
            

            _utilService.SaveAccessToken(token, refreshToken, uniqueId, user.Id);

            return Ok(new { access_token = token, refresh_token = refreshToken, UID = uniqueId, userId = user.Id });

        }

        [Route("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var principal = Util.GetPrincipalFromExpiredToken(model.token, _configuration);
                var username = principal.Identity.Name;

                var user = await _userManager.FindByNameAsync(username);
                var savedToken = _utilService.GetRefreshToken(model.uniqueId);

                if (savedToken.RefreshToken != model.refreshToken)
                {
                    //throw new SecurityTokenException("Invalid refresh token");
                    return Forbid();
                }

                if(DateTime.Now > savedToken.RefreshTokenExpiryDate)
                {
                    return Forbid();
                }

                var newJwtToken = Util.GenerateAccessToken(principal.Claims, _configuration).ToString();
                var newRefreshToken = Util.GenerateRefreshToken(_configuration);

                _utilService.SaveAccessToken(newJwtToken, newRefreshToken, model.uniqueId, user.Id);

                return Ok(new
                {
                    access_token = newJwtToken,
                    refresh_token = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                return Forbid();
            }
            
        }

    }
}