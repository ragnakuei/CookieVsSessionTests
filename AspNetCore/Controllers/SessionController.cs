using System;
using System.Collections.Generic;
using AspNetCore.Models;
using KueiExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class SessionController : BaseController
    {
        public SessionController(IHttpContextAccessor contextAccessor)
            : base(contextAccessor)
        {
        }

        public IActionResult Index()
        {
            var result = new SessionDto
                         {
                             SessionId    = Session.Id,
                             SessionValue = Session.GetString("TestSession"),
                         };

            if (result.SessionValue.IsNullOrWhiteSpace())
            {
                result.SessionValue = "Session Value Is Empty";
            }

            return View("Index", result);
        }

        public IActionResult Set()
        {
            var dto = new KeyValuePair<string, string>("TestSession", "TestSessionValue");

            Session.SetString(dto.Key, dto.Value);

            return Index();
        }


        public IActionResult Remove()
        {
            var dto = new KeyValuePair<string, string>("TestSession", string.Empty);

            Session.Remove(dto.Key);

            return Index();
        }

        public IActionResult Clear()
        {
            Session.Clear();

            return Index();
        }

        public IActionResult ExpireSessionCookie()
        {
            Session.Clear();
            Response.Cookies.Append("TestSession",
                                    string.Empty,
                                    new CookieOptions
                                    {
                                        // Domain      = null,
                                        // Path        = null,
                                        Expires  = DateTimeOffset.Now.AddDays(-1),
                                        Secure   = true,
                                        SameSite = SameSiteMode.Strict,
                                        HttpOnly = true,
                                        // MaxAge      = null,
                                        // IsEssential = false
                                    });

            // 最好用 Redirect 的方式來完整清掉 Session Id
            return RedirectToAction("Index");
        }
    }
}
