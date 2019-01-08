using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
    public class PytanieZWyboremViewModel
    {
        [Display(Name = "Przetłumacz:")]
        public string SlowoDoPrzetlumaczenia { get; set; }
        [Display(Name = "Twoja odpowiedź:")]
        public string OdpowiedzUzytkownika { get; set; }
        // Zarówno błędne jak i poprawna.
        public IEnumerable<Slowo> Odpowiedzi { get; set; }
        public Jezyk JezykUczony { get; set; }

        public bool JestPierwszymPytaniem { get; set; }
        public bool JestOstatnimPytaniem { get; set; }
    }
}