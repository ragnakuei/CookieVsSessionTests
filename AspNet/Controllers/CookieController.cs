using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AspNet.Models;
using KueiExtensions;

namespace AspNet.Controllers
{
    public class CookieController : BaseController
    {
        public ActionResult Index()
        {
            var result = new CookieDto
                         {
                             Value = Request.Cookies?.Get("TestCookie")?.Value,
                         };

            if (result.Value.IsNullOrWhiteSpace())
            {
                result.Value = "Cookie Value Is Empty";
            }

            return View("Index", result);
        }

        public ActionResult Set()
        {
            var dto = new KeyValuePair<string, string>("TestCookie", "TestCookieValue");

            Response.Cookies.Add(new HttpCookie(dto.Key, dto.Value)
                                 {
                                     // Path      = null,
                                     Secure    = true,
                                     Shareable = false,
                                     HttpOnly  = true,
                                     // Domain    = null,
                                     // Expires  = DateTime.Now.AddSeconds(10),
                                     SameSite = SameSiteMode.Strict
                                 });

            return Index();
        }


        public ActionResult Remove()
        {
            var dto = new KeyValuePair<string, string>("TestCookie", string.Empty);

            Response.Cookies.Add(new HttpCookie(dto.Key, dto.Value)
                                 {
                                     // Path      = null,
                                     Secure    = true,
                                     Shareable = false,
                                     HttpOnly  = true,
                                     // Domain    = null,
                                     Expires   = default,
                                     SameSite = SameSiteMode.Strict
                                 });
            return Index();
        }
    }
}
