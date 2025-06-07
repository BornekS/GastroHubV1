using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GestroHub.Models
{
    public class BazaDbContext : DbContext
    {
        public BazaDbContext() : base("name=BazaDbContext") { }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Recept> Recepti { get; set; }
    }
}
