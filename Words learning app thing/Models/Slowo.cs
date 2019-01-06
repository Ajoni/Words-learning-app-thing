using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Words_learning_app_thing.Models
{
    public class Slowo
    {
        [Key]
		public int Id { get; set; }
		public List<Slowo> Tlumaczenia { get; set; }
        public Jezyk Jezyk { get; set; }
        public string Zawartosc { get; set; }
    }
}