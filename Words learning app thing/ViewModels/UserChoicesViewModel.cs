using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
	public class UserChoicesViewModel
	{
		TypSesji SessionType { get; set; }
		Poziom Lvl { get; set; }
		Jezyk KnownLanguage { get; set; }
		Jezyk LearningLanguage { get; set; }
	}
}