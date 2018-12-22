using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public abstract class Pytanie
    {
        public int Id { get; set; }
        public Slowo DoPrzetlumaczenia { get; set; }
        public string OdpowiedzUzytkownika { get; set; }
		public bool CzyOdpowiedzianoPoprawnie()
        {
            return OdpowiedzUzytkownika.Equals(DoPrzetlumaczenia.Zawartosc, StringComparison.InvariantCultureIgnoreCase);
        }
	}
}