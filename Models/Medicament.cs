using System.ComponentModel.DataAnnotations;


namespace MVCproj.Models
{
public class Medicament
{
public int Id { get; set; }
[Required, StringLength(255)] public string Nom { get; set; } = string.Empty;
[StringLength(50)] public string? Quantite { get; set; }
[StringLength(255)] public string? Posologie { get; set; }
public string? Composition { get; set; }
[StringLength(100)] public string? Categorie { get; set; }
public DateTime? DateCreation { get; set; }
public DateTime? DateModification { get; set; }
}
}