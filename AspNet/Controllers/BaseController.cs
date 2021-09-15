using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace AspNet.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        protected HttpSessionStateBase Session => HttpContext.Session;
    }
}
