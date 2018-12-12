using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public class Question
    {
        public int Id { get; set; }
        public Word ToTranslate { get; set; }
	}
}