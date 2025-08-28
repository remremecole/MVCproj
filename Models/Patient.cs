using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MVCproj.Models
{
    [Table("patient")]
    public class Patient
    {
        [Key]
        [Column("id_patient")]
        public int Id { get; set; }

        [Required]
        [Column("prenom", TypeName = "varchar(50)")]
        public string Prenom { get; set; } = string.Empty;

        [Column("adresse", TypeName = "varchar(255)")]
        public string? Adresse { get; set; }

        [Column("ville", TypeName = "varchar(100)")]
        public string? Ville { get; set; }

        [Required]
        [Column("date_naissance", TypeName = "date")]
        public DateTime DateNaissance { get; set; }

        [Required]
        [Column("sexe")]
        public string Sexe { get; set; } = "M"; // 'M' ou 'F'

        [Column("poids", TypeName = "decimal(5,2)")]
        public decimal? Poids { get; set; }

        [Required]
        [Column("numero_secu", TypeName = "varchar(15)")]
        public string NumeroSecu { get; set; } = string.Empty;

        [Column("date_creation")]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [Column("date_modification")]
        public DateTime DateModification { get; set; } = DateTime.Now;
    }
}