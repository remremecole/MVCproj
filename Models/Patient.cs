using System.ComponentModel.DataAnnotations;


namespace MVCproj.Models
{
public class Patient
{
public int Id { get; set; }
[Required, StringLength(50)] public string Prenom { get; set; } = string.Empty;
[StringLength(255)] public string? Adresse { get; set; }
[StringLength(100)] public string? Ville { get; set; }
[Required] public DateTime DateNaissance { get; set; }
[Required, StringLength(1)] public string Sexe { get; set; } = "M"; // 'M' ou 'F'
public decimal? Poids { get; set; }
[Required, StringLength(15)] public string NumeroSecu { get; set; } = string.Empty;
public DateTime? DateCreation { get; set; }
public DateTime? DateModification { get; set; }
}
}