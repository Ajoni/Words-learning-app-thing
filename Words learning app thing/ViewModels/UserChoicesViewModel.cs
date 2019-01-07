using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
	public class UserChoicesViewModel
	{
		public TypSesji SessionType { get; set; }
		public Poziom Lvl { get; set; }
        public bool CanEditLevel { get; set; } = true;

		public Jezyk ZnanyJezyk { get; set; }
        public int ZnanyJezykId { get; set; }

		public Jezyk UczonyJezyk { get; set; }
        public int UczonyJezykId { get; set; }

        public RodzajInputu WybranyInput { get; set; }
        public IEnumerable<SelectListItem> Jezyki { get; set; }

        public void SetLevel(int progress)
        {
            if (progress > 20)
            {
                Lvl = Poziom.BardzoTrudny;
            }
            else if (progress > 15)
            {
                Lvl = Poziom.Trudny;
            }
            else if (progress > 10)
            {
                Lvl = Poziom.Sredni;
            }
            else
            {
                Lvl = Poziom.Latwy;
            }
        }
    }    
}