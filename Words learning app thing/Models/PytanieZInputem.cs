using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.ViewModels;

namespace Words_learning_app_thing.Models
{
    public class PytanieZInputem : Pytanie
    {
        public new PytanieViewModel getViewModel()
        {
            PytanieViewModel model = base.getViewModel();
            model.pytanieZInputemViewModel = new PytanieZInputemViewModel();
            return model;
        }
    }
}