using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCproj.Models
{
    public class Ordonnance
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        [Required]
        public DateTime DateFin { get; set; }

        [StringLength(100)]
        public string? RefComplementaire { get; set; }

        public byte[]? Pdf { get; set; }

        public int IdMedecin { get; set; }
        public int IdPatient { get; set; }

        public Patient? Patient { get; set; }
        public List<Contient> Contenus { get; set; } = new();

        // ✅ Ajout des 2 propriétés manquantes
        [DataType(DataType.Date)]
        public DateTime DateCreation { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? DateModification { get; set; }
    }
}
