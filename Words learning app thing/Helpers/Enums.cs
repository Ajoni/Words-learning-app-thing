using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Helpers
{
	public enum TypSesji {
		Nauka = 0,
		Test = 1
	}
	public enum Poziom {
		Latwy = 0,
		Sredni,
		Trudny,
		BardzoTrudny,
	}
	public enum RodzajInputu
	{
		TextInput = 0,
		Select,
	}
    // Gdy Tryb jest Automatyczny, poziom trudności sesji wybierany jest na podstawie postępów
    public enum TrybWybieraniaSesji
    {
        Automatyczny = 0,
        Manualny = 1
    }
}