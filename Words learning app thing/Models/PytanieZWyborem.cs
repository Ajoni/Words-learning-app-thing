using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Models
{
	public class PytanieZWyborem : Pytanie
	{
		public List<Slowo> BledneOdp { get; set; }
        private List<Slowo> Odpowiedzi { get; set; }

        public PytanieZWyborem(Slowo s, Jezyk jezyk, List<Slowo> bledne)
        {
            this.DoPrzetlumaczenia = s;
            this.JezykUczony = jezyk;
            this.BledneOdp = bledne;

            // Odpowiedź poprawna jest losowo wmieszana pomiędzy błędne odpowiedzi.
            var r = new Random();
            int indeksPoprawnej = r.Next(bledne.Count + 1);
            Odpowiedzi = bledne.GetRange(0, indeksPoprawnej);
            Odpowiedzi.Add(DoPrzetlumaczenia.Tlumaczenia[0]);
            Odpowiedzi.AddRange(bledne.GetRange(indeksPoprawnej, bledne.Count - indeksPoprawnej));
        }

        public override PytanieViewModel getViewModel()
        {
            PytanieViewModel model = base.getViewModel();
            model.pytanieZWyboremViewModel = new PytanieZWyboremViewModel()
            {
                SlowoDoPrzetlumaczenia = DoPrzetlumaczenia.Zawartosc,
                OdpowiedzUzytkownika = OdpowiedzUzytkownika,
                JezykUczony = JezykUczony,
                Odpowiedzi = Odpowiedzi
            };
            return model;
        }

         
    }
}