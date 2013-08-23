using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Conekt.Billing.Controllers
{
    public class DemoController : Controller
    {
        public ActionResult Index()
        {
			return View("Demo");
        }

		[HttpPost]
		public ActionResult Create(string json)
		{
			return PartialView("Demo");
		}
    }
}
