using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Models
{
	public class PytanieZWyborem : Pytanie
	{
		public List<Slowo> BledneOdp { get; set; }

        public PytanieZWyborem(Slowo s, Jezyk jezyk, List<Slowo> bledne)
        {
            this.DoPrzetlumaczenia = s;
            this.JezykUczony = jezyk;
            this.BledneOdp = bledne;
        }

        public new PytanieViewModel getViewModel()
        {
            PytanieViewModel model = base.getViewModel();
            model.pytanieZWyboremViewModel = new PytanieZWyboremViewModel();
            return model;
        }
    }
}