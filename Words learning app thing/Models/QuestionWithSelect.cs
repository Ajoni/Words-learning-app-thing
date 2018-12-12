using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
	public class QuestionWithSelect : Question
	{
		public List<Word> Choices { get; set; }
		public string Answer { get; set; }
	}
}