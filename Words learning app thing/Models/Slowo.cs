using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public class Slowo
    {
        [Key]
		public int Id { get; set; }
		public List<Slowo> Tlumaczenia { get; set; }
        public Jezyk Jezyk { get; set; }
        public String Zawartosc { get; set; }
    }
}