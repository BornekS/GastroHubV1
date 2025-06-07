using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GestroHub.Models
{ 
public class Recept
{
    [Key] // Primarni ključ u bazi
    public int Id { get; set; }

    [Required]
    public string Naziv { get; set; }

    [Required]
    public string Opis { get; set; }

    public string Kategorija { get; set; }

    public string SlikaPutanja { get; set; }

    public string AutorEmail { get; set; }

    // NEĆE se spremiti u bazu (samo pomoć za unos)
    [NotMapped]
    public string NamirniceInput { get; set; }

    // SPREMA se u bazu kao string (JSON)
    public string NamirniceJSON { get; set; }

    // NEĆE se spremati – generira se iz JSON-a
    [NotMapped]
    public List<string> Namirnice
    {
        get => string.IsNullOrEmpty(NamirniceJSON)
            ? new List<string>()
            : Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(NamirniceJSON);
        set => NamirniceJSON = Newtonsoft.Json.JsonConvert.SerializeObject(value);
    }
    }
}
