using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
	public class UserChoicesViewModel
	{
        [Display(Name="Tryb Sesji")]
		public TypSesji SessionType { get; set; }
        [Display(Name = "Poziom trudności")]
        public Poziom Lvl { get; set; }
        public bool CanEditLevel { get; set; } = true;

        [Display(Name = "Język który znasz")]
        public Jezyk ZnanyJezyk { get; set; }
        [Display(Name = "Język który znasz")]
        public int ZnanyJezykId { get; set; }

        [Display(Name = "Język którego się uczysz")]
        public Jezyk UczonyJezyk { get; set; }
        [Display(Name = "Język którego się uczysz")]
        public int UczonyJezykId { get; set; }

        [Display(Name = "Sposób udzielania odpowiedzi")]
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