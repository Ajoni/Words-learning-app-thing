using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public interface IStrategiaSesji
    {
		bool CzyMozeDoKolejnego(Pytanie obecne);
	}
}