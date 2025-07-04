﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GestroHub.Models;

namespace GestroHub.Controllers
{
    public class HomeController : Controller
    {
        // Privremeni recepti (bez baze)
        private static List<Recept> recepti = new List<Recept>
        {
            new Recept { Id = 1, Naziv = "Palačinke", Opis = "Brze palačinke", Kategorija = "Desert", SlikaPutanja = "recept1.jpg" },
            new Recept { Id = 2, Naziv = "Juha od bundeve", Opis = "Topla juha", Kategorija = "Juha", SlikaPutanja = "recept2.jpg" },
            new Recept { Id = 3, Naziv = "Tjestenina", Opis = "Talijanska tjestenina", Kategorija = "Glavno jelo", SlikaPutanja = "recept3.jpg" }
        };

        public ActionResult Index(string naziv)
        {
            var rezultati = recepti;

            if (!string.IsNullOrEmpty(naziv))
            {
                rezultati = recepti
                    .Where(r => r.Naziv.ToLower().Contains(naziv.ToLower()))
                    .ToList();
            }

            return View(rezultati);
        }
    }
}
