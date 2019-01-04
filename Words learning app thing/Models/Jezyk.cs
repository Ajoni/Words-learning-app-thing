using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
    public class Jezyk
    {
        [Key]
        public int Id { get; set; }
        public string Nazwa { get; set; }
    }
}