using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
    public class WordAddTranslationViewModel
    {
        public Slowo slowo { get; set; }
        public IEnumerable<SelectListItem> PossibleWords { get; set; }


        public int thisWordId { get; set; }
        public int translationId { get; set; }
    }
}