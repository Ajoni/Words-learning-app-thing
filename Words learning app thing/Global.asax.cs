using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Words_learning_app_thing.Helpers;

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
			//ioc.Register<Interface1, Enums>();
			//ioc.Register<IControllerFactory, ControllerFactory>();
			//ioc.Register<ITempDataProviderFactory, ITempDataProviderFactory>();
			//ioc.Register<IControllerActivator, ControllerA>();
			//DependencyResolver.SetResolver(ioc);
			ControllerBuilder.Current.SetControllerFactory(typeof(DIControllerFactory));
		}
    }
}
