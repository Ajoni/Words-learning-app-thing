﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Words_learning_app_thing.Data;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Logic;
using Words_learning_app_thing.Logic.Interfaces;
using Words_learning_app_thing.Models;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
		IUOW _uow;
		IDirector _director;

		public HomeController(IUOW iUOW, IDirector director)
		{
			_uow = iUOW;
			_director = director;
		}

		public ActionResult Index()
        {
			return View();
        }

		[HttpPost]
		public ActionResult Index(UserChoicesViewModel viewModel)
		{
			
			return RedirectToAction("Test");
		}
	}
}