using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Words_learning_app_thing.Data;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Logic;
using Words_learning_app_thing.Logic.Interfaces;

namespace Words_learning_app_thing
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			var ioc = IoC.GetInstance();
			ioc.Register<IUOW, UOW>();
			ioc.Register<IDirector, Director>();
			ControllerBuilder.Current.SetControllerFactory(typeof(DIControllerFactory));
		}
    }
}
