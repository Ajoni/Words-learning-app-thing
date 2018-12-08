using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.Helpers;

namespace Words_learning_app_thing.Controllers
{
    public class HomeController : Controller
    {
		Interface1 A;

		public ActionResult Index()
        {
            return View();
        }

		public HomeController(Interface1 a)
		{
			A = a;
		}

	}
}