using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words_learning_app_thing.Models;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Logic.Interfaces
{
	public interface IDirector
	{
		void Stworz(UserChoicesViewModel userChoices);
		void Akceptuj(IBudowniczySesji budowniczySesji);
	}
}
