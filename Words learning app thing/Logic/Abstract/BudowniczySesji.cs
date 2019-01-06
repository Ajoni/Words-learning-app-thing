using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Data.Repositories;
using Words_learning_app_thing.Logic.Interfaces;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Logic.Abstract
{
	public abstract class BudowniczySesji : IBudowniczySesji
	{
		public Sesja BudowanaSesja { get; private set; } = new Sesja();
        protected SlowoRepo _slowoRepo { get; set; }

        public void Reset()
		{
			BudowanaSesja = new Sesja();
		}		

		public void UstawJezykZnany(Jezyk language)
		{
			BudowanaSesja.ZnanyJezyk = language;
		}

		public void UstawJezykUczony(Jezyk language)
		{
			BudowanaSesja.UczonyJezyk = language;
		}

		public void UstawStrategieNauka()
		{
			BudowanaSesja.Strategia = new LearnStrategy();
		}
        
		public void UstawStrategieTest()
		{
			BudowanaSesja.Strategia = new TestStrategy();
		}

        public abstract void UstawLatwyZestaw();
        public abstract void UstawSredniZestaw();        
        public abstract void UstawTrudnyZestaw();
        public abstract void UstawBardzoTrudnyZestaw();
	}
}