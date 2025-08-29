using MVCproj.Models;


namespace MVCproj.Services
{
public interface IOrdonnanceService
{
Task<Ordonnance> CreerAsync(Ordonnance ord, IEnumerable<(int medicamentId, string qte, string posologie)> lignes);
Task<byte[]> GenererPdfAsync(int ordonnanceId);
}
}