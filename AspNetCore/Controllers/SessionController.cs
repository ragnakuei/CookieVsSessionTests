using System.Collections.Generic;
using KueiExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    [Route("[controller]")]
    public class SessionController : BaseController
    {
        public SessionController(IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
        }

        [HttpPost, Route("[action]")]
        public IActionResult GetId()
        {
            var result = Session.Id;
            return Ok(result);
        }

        [HttpPost, Route("[action]")]
        public IActionResult Get([FromBody]KeyValuePair<string, string> dto)
        {
            var result = Session.GetString(dto.Key);
            if (result.IsNullOrWhiteSpace())
            {
                return NoContent();
            }

            return Ok(result);
        }

        [HttpPost, Route("[action]")]
        public IActionResult Set([FromBody]KeyValuePair<string, string> dto)
        {
            Session.SetString(dto.Key, dto.Value);
            return Ok();
        }

        [HttpPost, Route("[action]")]
        public IActionResult Remove([FromBody]KeyValuePair<string, string> dto)
        {
            Session.Remove(dto.Key);
            return Ok();
        }

        [HttpPost, Route("[action]")]
        public IActionResult Clear()
        {
            Session.Clear();
            // HttpContext.Response.Cookies.Append(".AspNetCore.Session",
            //                                     string.Empty,
            //                                     new CookieOptions
            //                                     {
            //                                         // Domain      = null,
            //                                         // Path        = null,
            //                                         Expires  = DateTimeOffset.Now.AddSeconds(-1),
            //                                         Secure   = true,
            //                                         SameSite = SameSiteMode.Strict,
            //                                         HttpOnly = true,
            //                                         // MaxAge      = null,
            //                                         // IsEssential = false
            //                                     });

            return Ok();
        }
    }
}
