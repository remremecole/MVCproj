using Microsoft.EntityFrameworkCore;
using MVCproj.Data;
using MVCproj.Models;


namespace MVCproj.Services
{
public class MedicamentService : IMedicamentService
{
private readonly MedManagerContext _ctx;
public MedicamentService(MedManagerContext ctx) => _ctx = ctx;


public async Task<IEnumerable<Medicament>> GetAllAsync(string? search = null)
{
var q = _ctx.Medicaments.AsQueryable();
if (!string.IsNullOrWhiteSpace(search))
q = q.Where(m => m.Nom.Contains(search) || (m.Categorie != null && m.Categorie.Contains(search)));
return await q.OrderBy(m => m.Nom).ToListAsync();
}
}
}