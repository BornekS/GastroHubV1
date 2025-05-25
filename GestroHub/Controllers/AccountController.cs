using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using GestroHub.Models;

namespace GestroHub.Controllers
{
    public class AccountController : Controller
    {
        private static List<Korisnik> korisnici = new List<Korisnik>();
        private static int idCounter = 1;

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Korisnik model)
        {
            if (ModelState.IsValid)
            {
                model.Id = idCounter++;
                model.Potvrđen = false;

                korisnici.Add(model);
                Session["korisnik"] = model;

                try
                {
                    var mail = new MailMessage();
                    mail.To.Add(model.Email);
                    mail.From = new MailAddress("borna.cpp@gmail.com"); // tvoj gmail
                    mail.Subject = "Registracija - GestroHub";
                    mail.Body = $"Pozdrav {model.Ime},\n\nUspješno ste se registrirali na GestroHub.\n\nLijep pozdrav,\nGestroHub tim";

                    var smtp = new SmtpClient("smtp.gmail.com", 587)
                    {
                        Credentials = new NetworkCredential("borna.cpp@gmail.com", "matd mgeb fkch qgjv"),
                        EnableSsl = true
                    };

                    smtp.Send(mail);
                }
                catch
                {
                    // možeš logirati grešku ako želiš
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string lozinka)
        {
            // Provjera u privremenoj memorijskoj listi
            var korisnik = korisnici.FirstOrDefault(k => k.Email == email && k.Lozinka == lozinka);

            if (korisnik != null)
            {
                Session["korisnik"] = korisnik;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
