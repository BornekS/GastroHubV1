using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GestroHub.Models;

namespace GestroHub.Controllers
{
    public class ReceptController : Controller
    {
        public static List<Recept> recepti = new List<Recept>();
        private static int idCounter = 1;

        [HttpGet]
        public ActionResult Create()
        {
            var korisnik = Session["korisnik"] as Korisnik;
            if (korisnik == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Create(Recept model)
        {
            var korisnik = Session["korisnik"] as Korisnik;
            if (korisnik == null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                model.Id = idCounter++;
                model.AutorEmail = korisnik.Email;
                model.Namirnice = model.NamirniceInput?
                    .Split(',')
                    .Select(n => n.Trim().ToLower())
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .ToList() ?? new List<string>();
                recepti.Add(model);

                return RedirectToAction("Moji", "Recept");
            }

            return View(model);
        }

        public ActionResult Moji()
        {
            var korisnik = Session["korisnik"] as Korisnik;
            if (korisnik == null)
                return RedirectToAction("Index", "Home");

            var moji = recepti.Where(r => r.AutorEmail == korisnik.Email).ToList();
            return View(moji);
        }

        // GET: Recept/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var korisnik = Session["korisnik"] as Korisnik;
            var recept = recepti.FirstOrDefault(r => r.Id == id && r.AutorEmail == korisnik.Email);
            if (recept == null) return RedirectToAction("Moji");

            return View(recept);
        }

        // POST: Recept/Edit/5
        [HttpPost]
        public ActionResult Edit(Recept model)
        {
            var recept = recepti.FirstOrDefault(r => r.Id == model.Id);
            if (recept != null)
            {
                recept.Naziv = model.Naziv;
                recept.Opis = model.Opis;
                recept.Kategorija = model.Kategorija;
                recept.SlikaPutanja = model.SlikaPutanja;
            }

            return RedirectToAction("Moji");
        }

        // GET: Recept/Delete/5
        public ActionResult Delete(int id)
        {
            var recept = recepti.FirstOrDefault(r => r.Id == id);
            if (recept != null)
                recepti.Remove(recept);

            return RedirectToAction("Moji");
        }

        [HttpGet]
        public ActionResult SearchNamirnice()
        {
            // Izvuci sve unikatne namirnice iz svih recepata
            var sveNamirnice = recepti
                .SelectMany(r => r.Namirnice)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            ViewBag.SveNamirnice = sveNamirnice;

            return View(new List<Recept>());
        }

        [HttpPost]
        public ActionResult SearchNamirnice(List<string> odabraneNamirnice)
        {
            var sveNamirnice = recepti
                .SelectMany(r => r.Namirnice)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            ViewBag.SveNamirnice = sveNamirnice;

            if (odabraneNamirnice == null || !odabraneNamirnice.Any())
                return View(new List<Recept>());

            var rezultat = recepti
                .Where(r => odabraneNamirnice.All(n => r.Namirnice.Contains(n)))
                .ToList();

            return View(rezultat);
        }

    }
}
