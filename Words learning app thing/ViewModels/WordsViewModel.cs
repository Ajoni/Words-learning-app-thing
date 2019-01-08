using System.Collections.Generic;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
    public class WordsViewModel
    {
        public WordsViewModel()
        {
        }

        public IEnumerable<Slowo> Words { get; set; }
        public int LanguageId { get; set; }
    }
}