using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private string _authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        public AccountController(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        [HttpPost, Route("[action]")]
        public async Task<IActionResult> Login()
        {
            await LoginAsync();
            return Ok();
        }

        [HttpPost, Route("[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(_authenticationScheme);

            return Ok();
        }

        private async Task LoginAsync()
        {
            var claims = new List<Claim>
                         {
                             new Claim(ClaimTypes.Name,     "Test"),
                             new Claim(ClaimTypes.Role,     "TestRole"),
                         };

            var claimsIdentity = new ClaimsIdentity(claims, _authenticationScheme);

            var authProperties = new AuthenticationProperties
                                 {
                                     AllowRefresh = true,
                                     ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(10),
                                     IsPersistent = false,
                                     //IssuedUtc = <DateTimeOffset>,
                                     //RedirectUri = <string>
                                 };

            await HttpContext.SignInAsync(_authenticationScheme,
                                          new ClaimsPrincipal(claimsIdentity),
                                          authProperties);
        }
    }
}
