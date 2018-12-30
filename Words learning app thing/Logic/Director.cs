using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Logic.Interfaces;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Logic
{
	public class Director : IDirector
	{
		private IBudowniczySesji budowniczySesji;

		public Director() {}

		public void Akceptuj(IBudowniczySesji budowniczySesji)
		{
			this.budowniczySesji = budowniczySesji;
		}

		public void Stworz(UserChoicesViewModel userChoices)
		{
			switch (userChoices.Lvl)
			{
				case Poziom.Latwy:
					budowniczySesji.UstawLatwyZestaw();
					break;
				case Poziom.Sredni:
					budowniczySesji.UstawSredniZestaw();
					break;
				case Poziom.Trudny:
					budowniczySesji.UstawTrudnyZestaw();
					break;
				case Poziom.BardzoTrudny:
					budowniczySesji.UstawBardzoTrudnyZestaw();
					break;
				default:
					throw new ArgumentException($"Brak ustalonego zachowania dla poziomu {userChoices.Lvl}");
			}
			switch (userChoices.SessionType)
			{
				case TypSesji.Nauka:
					budowniczySesji.UstawStrategieNauka();
					break;
				case TypSesji.Test:
					budowniczySesji.UstawStrategieTest();
					break;
				default:
					throw new ArgumentException($"Nieznany rodzaj sesji {userChoices.SessionType}");
			}
			budowniczySesji.UstawJezykUczony(userChoices.UczonyJezyk);
			budowniczySesji.UstawJezykZnany(userChoices.ZnanyJezyk);
		}
	}
}	