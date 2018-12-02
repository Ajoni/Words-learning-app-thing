using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public class Sesja
    {
        public StrategiaSesji Strategia { get; set; }
        public List<Pytanie> Pytaniea { get; set; }
        public int Wynik { get; set; }
    }
}