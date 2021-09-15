using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BaseController(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ISession Session => _contextAccessor.HttpContext.Session;
    }
}
