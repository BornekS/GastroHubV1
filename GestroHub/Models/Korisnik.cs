using System.ComponentModel.DataAnnotations;

namespace GestroHub.Models
{ 
public class Korisnik
{
    public int Id { get; set; }

    [Required]
    public string Ime { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Lozinka { get; set; }

    public bool Potvrđen { get; set; }
    public string Token { get; set; }
    }
}
