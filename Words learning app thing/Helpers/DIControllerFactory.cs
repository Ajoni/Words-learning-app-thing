using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Words_learning_app_thing.Helpers
{
	public class DIControllerFactory : DefaultControllerFactory
	{
		private readonly IoC _IoC;

		public DIControllerFactory()
		{
			_IoC = IoC.GetInstance();
		}

		protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
		{
			var constructor = controllerType.GetConstructors().First();

			var parameters = constructor
				.GetParameters()
				.Select(argument => _IoC.GetService(argument.ParameterType))
				.ToArray();

			return (IController)constructor.Invoke(parameters);
		}
	}
}