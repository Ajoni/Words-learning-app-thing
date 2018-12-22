using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Logic.Interfaces
{
	interface IBudowniczySesji
	{
        Sesja BudowanaSesja { get; }
        void Reset();
		void UstawJezykUczony(Jezyk jezyk);
		void UstawJezykZnany(Jezyk jezyk);
		void UstawStrategieTest();
		void UstawStrategieNauka();
		void UstawLatwyZestaw();
		void UstawSredniZestaw();
		void UstawTrudnyZestaw();
		void UstawBardzoTrudnyZestaw();		
	}
}
