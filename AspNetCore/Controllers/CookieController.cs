using System;
using System.Collections.Generic;
using KueiExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [Route("api/[controller]")]
    public class CookieController : BaseController
    {
        public CookieController(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        // public IActionResult Get(string key)
        // {
        //     // var result = Session.GetString(key);
        //
        //
        //
        //     return Ok(result);
        // }

        [HttpPost, Route("[action]")]
        public IActionResult Set([FromBody]KeyValuePair<string, string> dto)
        {
            HttpContext.Response.Cookies.Append(dto.Key,
                                                dto.Value,
                                                new CookieOptions
                                                {
                                                    // Domain      = null,
                                                    // Path        = null,
                                                    // Expires  = DateTimeOffset.Now.AddMinutes(10),
                                                    Secure   = true,
                                                    SameSite = SameSiteMode.Strict,
                                                    HttpOnly = true,
                                                    // MaxAge      = null,
                                                    // IsEssential = false
                                                });

            return Ok();
        }


        [HttpPost, Route("[action]")]
        public IActionResult Get([FromBody]KeyValuePair<string, string> dto)
        {
            var cookieValue = HttpContext.Request.Cookies[dto.Key];

            if (cookieValue.IsNullOrWhiteSpace())
            {
                return NoContent();
            }

            return Ok(cookieValue);
        }

        [HttpPost, Route("[action]")]
        public IActionResult Remove([FromBody]KeyValuePair<string, string> dto)
        {
            HttpContext.Response.Cookies.Append(dto.Key,
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

            return Ok();
        }
    }
}
