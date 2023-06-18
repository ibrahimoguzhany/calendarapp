using CalendarApp.BLL.ManagerServices.Abstracts;
using CalendarApp.BLL.ManagerServices.Concretes;
using CalendarApp.COMMON.Results;
using CalendarApp.ENTITIES.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CalendarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;
        private readonly ITokenManager _tokenManager;
        public AccountController(IAccountService accountService, ITokenService tokenService, ITokenManager tokenManager)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _tokenManager = tokenManager;
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterDto data)
        {
            Result result = _accountService.Register(data).Result;

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto data)
        {
            IActionResult response = Unauthorized();
            User user = _accountService.Login(data).Result.Data;
            if (user != null)
            {
                string tokenStr = _tokenService.GenerateJWT(user);

                Response.Cookies.Append("X-Access-Token", tokenStr, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.Now.AddMinutes(30) });
                Response.Cookies.Append("X-Email", data.UserNameOrEmail, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.Now.AddMonths(1) });
                Response.Cookies.Append("X-Refresh-Token", user.RefreshToken.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.Now.AddMonths(1) });

                response = Ok(new
                {
                    token = tokenStr,
                    refreshToken = user.RefreshToken.ToString()
                });
            }

            return response;
        }
        [AllowAnonymous]
        [HttpPost("Refresh")]
        public IActionResult Refresh(RefreshTokenDto data)
        {
            IActionResult response = Unauthorized();
            User user = _accountService.GetRefreshLogin(data).Result.Data;
            if (user != null)
            {
                string tokenStr = _tokenService.GenerateJWT(user);

                Response.Cookies.Append("X-Access-Token", tokenStr, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.Now.AddMinutes(30) });
                Response.Cookies.Append("X-Email", user.Email, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.Now.AddMonths(1) });
                Response.Cookies.Append("X-Refresh-Token", user.RefreshToken.ToString(), new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.None, Secure = true, Expires = DateTime.Now.AddMonths(1) });

                response = Ok(new
                {
                    token = tokenStr,
                    refreshToken = user.RefreshToken.ToString()
                });
            }

            return response;
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _tokenManager.DeactivateCurrentAsync();
            return NoContent();
        }
    }
}
