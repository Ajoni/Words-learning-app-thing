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
		public TypSesji SessionType { get; set; }
		public Poziom Lvl { get; set; }
		public Jezyk ZnanyJezyk { get; set; }
		public Jezyk UczonyJezyk { get; set; }
		public RodzajInputu WybranyInput { get; set; }
	}
}