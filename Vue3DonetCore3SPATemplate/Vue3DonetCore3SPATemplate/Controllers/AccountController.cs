using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vue3DonetCore3SPATemplate.Models;

namespace Vue3DonetCore3SPATemplate.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public bool IsLogin()
        { 
            return User.Identity.IsAuthenticated;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticationUser authenticationUser)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            if (authenticationUser.Email != "test@auth.com")
            {
                ModelState.AddModelError("Email", "Invalid email");
                return BadRequest(ModelState);
                //return BadRequestapiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var user = new AuthenticationUser()
            {
                Email = authenticationUser.Email,
                Name = "Chocolate Cookie"
            };

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

            var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                // Refreshing the authentication session should be allowed.

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                //ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(5),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                IssuedUtc = DateTimeOffset.UtcNow,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok("ok");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("ok");
        }

        [HttpGet]
        [Authorize]
        public UserInfo GetUserInfo()
        {
            var userInfo = new UserInfo(User.Claims.ToList());
            return userInfo;
        }

        /// <summary>
        /// for refreshing session
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult KeepAlive()
        {
            return Ok();
        }
    }
}
