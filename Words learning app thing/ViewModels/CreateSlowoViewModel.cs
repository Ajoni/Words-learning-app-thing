using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Models;
using System.Web.Mvc;

namespace Words_learning_app_thing.ViewModels
{
    public class CreateSlowoViewModel
    {
        public string Slowo { get; set; }
        public int JezykId { get; set; }
        public Jezyk Jezyk { get; set; }
    }
}