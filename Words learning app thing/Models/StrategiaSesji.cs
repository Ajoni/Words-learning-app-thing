using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public interface StrategiaSesji
    {
        void KolejnePytanie(Sesja context);
        void PoprzedniePytanie(Sesja context);
    }
}