using System.Web.Mvc;
using GestroHub.Models;

namespace GestroHub.Controllers
{
    public class KorisnikController : Controller
    {
        public ActionResult Profil()
        {
            var korisnik = Session["korisnik"] as Korisnik;
            if (korisnik == null)
                return RedirectToAction("Index", "Home");

            return View(korisnik);
        }
    }
}