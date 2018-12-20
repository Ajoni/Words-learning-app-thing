﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Words_learning_app_thing.Helpers
{
	public class IoC : IDependencyResolver
	{
		readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
		static private IoC _instance { get; set; } = new IoC();

		private IoC() {}

		public void Register<TContract, TImplementation>()
		{
			types[typeof(TContract)] = typeof(TImplementation);
		}

		public void Register(Type TContract, Type TImplementation)
		{
			types[TContract] = TImplementation;
		}

		public T Resolve<T>()
		{
			return (T)Resolve(typeof(T));
		}

		public object Resolve(Type contract)
		{
			Type implementation = types[contract];
			ConstructorInfo constructor = implementation.GetConstructors()[0];
			ParameterInfo[] constructorParameters = constructor.GetParameters();
			if (constructorParameters.Length == 0)
			{
				return Activator.CreateInstance(implementation);
			}

			List<object> parameters = new List<object>(constructorParameters.Length);
			foreach (ParameterInfo parameterInfo in constructorParameters)
			{
				parameters.Add(Resolve(parameterInfo.ParameterType));
			}

			return constructor.Invoke(parameters.ToArray());
		}

		public object GetService(Type serviceType)
		{
			Type implementation = types[serviceType];
			ConstructorInfo constructor = implementation.GetConstructors()[0];
			ParameterInfo[] constructorParameters = constructor.GetParameters();
			if (constructorParameters.Length == 0)
			{
				return Activator.CreateInstance(implementation);
			}

			List<object> parameters = new List<object>(constructorParameters.Length);
			foreach (ParameterInfo parameterInfo in constructorParameters)
			{
				parameters.Add(Resolve(parameterInfo.ParameterType));
			}

			return constructor.Invoke(parameters.ToArray());
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			throw new NotImplementedException();
		}

		static public IoC GetInstance()
		{
			return _instance;
		}
	}
}