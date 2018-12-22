using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.ViewModels
{
	public class QuestionWithInputViewModel
	{
		public Slowo ToTranslate { get; set; }
		public string Answer { get; set; }
	}
}