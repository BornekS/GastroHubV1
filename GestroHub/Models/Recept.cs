using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GestroHub.Models
{
    public class Recept
    {
        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        public string Opis { get; set; }

        public string Kategorija { get; set; }

        public string SlikaPutanja { get; set; }
    }
}
