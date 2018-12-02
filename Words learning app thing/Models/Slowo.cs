using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public class Slowo
    {
        public List<Slowo> Tlumaczenia { get; set; }
        public Jezyk Jezyk { get; set; }
    }
}