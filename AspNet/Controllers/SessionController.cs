using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using AspNet.Models;
using KueiExtensions;

namespace AspNet.Controllers
{
    public class SessionController : BaseController
    {
        public ActionResult Index()
        {
            var result = new SessionDto
                         {
                             SessionId    = Session.SessionID,
                             SessionValue = Session["TestSession"]?.ToString(),
                         };

            if (result.SessionValue.IsNullOrWhiteSpace())
            {
                result.SessionValue = "Session Value Is Empty";
            }

            return View("Index", result);
        }

        public ActionResult Set()
        {
            var dto = new KeyValuePair<string, string>("TestSession", "TestSessionValue");

            Session.Add(dto.Key, dto.Value);

            return Index();
        }


        public ActionResult Remove()
        {
            var dto = new KeyValuePair<string, string>("TestSession", string.Empty);

            Session.Remove(dto.Key);

            return Index();
        }

        public ActionResult RemoveAll()
        {
            Session.RemoveAll();

            return Index();
        }

        public ActionResult Abandon()
        {
            Session.Abandon();

            return Index();
        }

        public ActionResult Clear()
        {
            Session.Clear();

            return Index();
        }

        public ActionResult ExpireSessionCookie()
        {
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", null)
                                 {
                                     // Path      = null,
                                     Secure    = true,
                                     Shareable = false,
                                     HttpOnly  = true,
                                     // Domain    = null,
                                     Expires  = DateTime.Now.AddDays(-1),
                                     SameSite = SameSiteMode.Strict
                                 });

            return RedirectToAction("Index");
        }
    }
}
