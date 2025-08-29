namespace MVCproj.Models
{
    public class Contient
    {
        public int IdOrdonnance { get; set; }
        public int IdMedicament { get; set; }
        public string QuantitePrescrite { get; set; } = string.Empty;
        public string PosologiePrescrite { get; set; } = string.Empty;

        public Ordonnance? Ordonnance { get; set; }
        public Medicament? Medicament { get; set; }
    }

    public class Souffre
    {
        public int IdPatient { get; set; }
        public int IdAllergie { get; set; }
        public DateTime? DateDiagnostic { get; set; }
        public string? Severite { get; set; }
    }

    public class Possede
    {
        public int IdPatient { get; set; }
        public int IdAntecedent { get; set; }
        public DateTime? DateDiagnostic { get; set; }
        public string? Description { get; set; }
    }

    public class Incompatible
    {
        public int IdMedicament { get; set; }
        public int IdAllergie { get; set; }
        public string? NiveauRisque { get; set; }
        public string? Description { get; set; }
    }

    public class ContreIndique
    {
        public int IdMedicament { get; set; }
        public int IdAntecedent { get; set; }
        public string? NiveauRisque { get; set; }
        public string? Description { get; set; }
    }
}
