using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Helpers
{
	public enum TypSesji {
		Nauka = 0,
		Test = 1
	}
	public enum Poziom {
        [Display(Name = "Łatwy")]
        Latwy = 0,
        [Display(Name = "Średni")]
        Sredni,
        [Display(Name = "Trudny")]
        Trudny,
        [Display(Name = "Bardzo Trudny")]
        BardzoTrudny,
	}
	public enum RodzajInputu
	{
        [Display(Name = "Wpisywanie słowa")]
        TextInput = 0,
        [Display(Name = "Wybieranie z możliwych odpowiedzi")]
        Select,
	}

    public enum TrybWybieraniaSesji
    {
        Automatyczny = 0,
        Manualny = 1
    }
}