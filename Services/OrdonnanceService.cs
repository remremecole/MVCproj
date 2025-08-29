using Microsoft.EntityFrameworkCore;
using MVCproj.Data;
using MVCproj.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace MVCproj.Services
{
public class IatrogeneException : Exception
{
public IatrogeneException(string message) : base(message) {}
}


public class OrdonnanceService : IOrdonnanceService
{
private readonly MedManagerContext _ctx;
public OrdonnanceService(MedManagerContext ctx) => _ctx = ctx;


public async Task<Ordonnance> CreerAsync(Ordonnance ord, IEnumerable<(int medicamentId, string qte, string posologie)> lignes)
{
// Vérif règles métier : date_fin > date_debut
if (ord.DateFin <= ord.DateDebut)
throw new ArgumentException("La date de fin doit être postérieure à la date de début.");


// Récup allergies / antécédents du patient
var allergiesIds = await _ctx.Souffres.Where(s => s.IdPatient == ord.IdPatient).Select(s => s.IdAllergie).ToListAsync();
var antecedentsIds = await _ctx.Possedes.Where(p => p.IdPatient == ord.IdPatient).Select(p => p.IdAntecedent).ToListAsync();


            // Vérifier incompatibilités médicamenteuses
            foreach (var (medId, _, _) in lignes)
            {
                bool allergieKO = await _ctx.Incompatibles.AnyAsync(i => i.IdMedicament == medId && allergiesIds.Contains(i.IdAllergie));
                if (allergieKO) throw new IatrogeneException("Médicament incompatible avec une allergie du patient");


                bool antecedentKO = await _ctx.ContreIndiques.AnyAsync(ci => ci.IdMedicament == medId && antecedentsIds.Contains(ci.IdAntecedent));
                if (antecedentKO) throw new IatrogeneException("Médicament contre-indiqué avec un antécédent du patient");
            }
_ctx.Ordonnances.Add(ord);
await _ctx.SaveChangesAsync();


foreach (var (medId, qte, pos) in lignes)
{
_ctx.Contients.Add(new Contient
{
IdOrdonnance = ord.Id,
IdMedicament = medId,
QuantitePrescrite = qte,
PosologiePrescrite = pos
});
}
await _ctx.SaveChangesAsync();
return ord;
}


public async Task<byte[]> GenererPdfAsync(int ordonnanceId)
{
var ord = await _ctx.Ordonnances
.Include(o => o.Contenus)
.ThenInclude(c => c.Medicament)
.Include(o => o.Patient)
.FirstAsync(o => o.Id == ordonnanceId);


byte[] pdf = Document.Create(container =>
{
container.Page(page =>
{
page.Margin(30);
page.Header().Text("Ordonnance").FontSize(20).Bold();
page.Content().Column(col =>
{
col.Item().Text($"Patient: {ord.Patient?.Prenom} (#{ord.IdPatient})");
col.Item().Text($"Période: {ord.DateDebut:dd/MM/yyyy} → {ord.DateFin:dd/MM/yyyy}");
col.Item().LineHorizontal(1);
foreach (var l in ord.Contenus)
{
col.Item().Text($"• {l.Medicament?.Nom} — {l.QuantitePrescrite} — {l.PosologiePrescrite}");
}
});
page.Footer().AlignRight().Text($"GSB / MedManager — {DateTime.Now:dd/MM/yyyy}");
});
}).GeneratePdf();


return pdf;
}
}
}