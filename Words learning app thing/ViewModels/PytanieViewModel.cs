using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.ViewModels
{
    public class PytanieViewModel
    {
        public PytanieZInputemViewModel pytanieZInputemViewModel { get; set; }
        public PytanieZWyboremViewModel pytanieZWyboremViewModel { get; set; }

        public int NumerPytania { get; set; }
        public int LiczbaPytań { get; set; }

        public bool JestPierwszymPytaniem { get; set; }
        public bool JestOstatnimPytaniem { get; set; }
        public bool MozeZakonczyc { get; set; }
    }
}