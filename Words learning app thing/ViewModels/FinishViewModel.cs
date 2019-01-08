using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Helpers;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
    public class FinishViewModel
    {
        public List<Pytanie> Pytania { get; set; }
        public TypSesji TypSesji { get; set; }
    }
}