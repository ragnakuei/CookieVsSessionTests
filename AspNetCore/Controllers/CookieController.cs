using System;
using System.Collections.Generic;
using AspNetCore.Models;
using KueiExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class CookieController : BaseController
    {
        public CookieController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }

        public IActionResult Index()
        {
            var result = new CookieDto
                         {
                             Value = Request.Cookies["TestCookie"],
                         };

            if (result.Value.IsNullOrWhiteSpace())
            {
                result.Value = "Cookie Value Is Empty";
            }

            return View("Index", result);
        }

        public IActionResult Set()
        {
            var dto = new KeyValuePair<string, string>("TestCookie", "TestCookieValue");

            Response.Cookies.Append(dto.Key,
                                    dto.Value,
                                    new CookieOptions
                                    {
                                        // Domain      = null,
                                        // Path        = null,
                                        Expires  = DateTimeOffset.Now.AddHours(1),
                                        Secure   = true,
                                        SameSite = SameSiteMode.Strict,
                                        HttpOnly = true,
                                        // MaxAge      = null,
                                        // IsEssential = false
                                    });
            return View("Redirect");
        }


        public IActionResult Remove()
        {
            var dto = new KeyValuePair<string, string>("TestCookie", string.Empty);

            Response.Cookies.Append(dto.Key,
                                    dto.Value,
                                    new CookieOptions
                                    {
                                        // Domain      = null,
                                        // Path        = null,
                                        Expires  = DateTimeOffset.Now.AddSeconds(-1),
                                        Secure   = true,
                                        SameSite = SameSiteMode.Strict,
                                        HttpOnly = true,
                                        // MaxAge      = null,
                                        // IsEssential = false
                                    });

            return View("Redirect");
        }
    }
}
