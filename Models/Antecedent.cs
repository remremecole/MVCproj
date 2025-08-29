using System.ComponentModel.DataAnnotations;


namespace MVCproj.Models
{
public class Antecedent
{
public int Id { get; set; }
[Required, StringLength(100)] public string Nom { get; set; } = string.Empty;
public string? Description { get; set; }
public DateTime? DateCreation { get; set; }
}
}