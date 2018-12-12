﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public class Word
    {
		public int Id { get; set; }
		public List<Word> Translations { get; set; }
        public Language Language { get; set; }
    }
}